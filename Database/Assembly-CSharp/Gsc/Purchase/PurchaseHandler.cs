// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.PurchaseHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Core;
using Gsc.Network;
using Gsc.Purchase.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gsc.Purchase
{
  public class PurchaseHandler
  {
    public static readonly PurchaseHandler Instance = new PurchaseHandler();
    public const WebTaskAttribute TASK_ATTRIBUTES = WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel;
    private static LogKit.Logger logger;
    private IPurchaseFlowListener flowListener;
    private bool enabledInactiveCallback;
    private string[] productIds;
    private IPurchaseFlowImpl impl;
    private float _lastResumed;

    private PurchaseHandler()
    {
      this.HasCreditLimit = false;
      this.impl = (IPurchaseFlowImpl) new GooglePlay(this);
    }

    public bool initialized { get; private set; }

    public bool unavailabled { get; private set; }

    public ProductInfo[] ProductList { get; private set; }

    public bool HasCreditLimit { get; private set; }

    public float CreditLimit { get; private set; }

    public static void Log(int type, string tag, string message)
    {
      Debug.Log((object) (type.ToString() + ":" + tag + ":" + message));
      if (PurchaseHandler.logger == null)
        PurchaseHandler.logger = LogKit.Logger.CreateLogger("nativebase.purchaseflow_unity");
      PurchaseHandler.logger.Post(tag, type != 0 ? LogKit.LogLevel.Error : LogKit.LogLevel.Info, message);
    }

    public bool isProcessing { get; private set; }

    public static void Initialize()
    {
      GameObject gameObject = ((Component) RootObject.Instance).get_gameObject();
      if (!Object.op_Equality((Object) gameObject.GetComponent<PurchaseHandler.Observer>(), (Object) null))
        return;
      gameObject.AddComponent<PurchaseHandler.Observer>();
      PurchaseHandler.Instance.isProcessing = false;
    }

    public void Init(string[] productIds)
    {
      this.productIds = productIds;
      this.Init();
    }

    private void Init()
    {
      this.impl.Init(this.productIds);
    }

    public void UpdateProducts(string[] productIds)
    {
      if (productIds != null)
        this.productIds = productIds;
      this.UpdateProducts();
    }

    private void UpdateProducts()
    {
      if (this.isProcessing)
        return;
      this.impl.UpdateProducts(this.productIds);
    }

    public void Resume()
    {
      if (this.isProcessing || this.impl == null || WebQueue.defaultQueue.isPause)
        return;
      float unscaledTime = Time.get_unscaledTime();
      if ((double) (unscaledTime - this._lastResumed) <= 60.0)
        return;
      this._lastResumed = unscaledTime;
      this.impl.Resume();
    }

    public void Launch(IPurchaseFlowListener listener, bool enableInactiveCallback)
    {
      this.flowListener = listener;
      this.enabledInactiveCallback = enableInactiveCallback;
      this.RenderProducts();
    }

    public bool Purchase(string productId)
    {
      if (this.isProcessing)
        return false;
      ProductInfo product = ((IEnumerable<ProductInfo>) this.ProductList).Where<ProductInfo>((Func<ProductInfo, bool>) (x => x.ID == productId)).First<ProductInfo>();
      if (!product.enabled)
      {
        this.OnPurchaseResult(ResultCode.OverCreditLimit, (FulfillmentResult) null);
        return false;
      }
      this.isProcessing = true;
      return this.impl.Purchase(product);
    }

    public void Confirm(ProductInfo product)
    {
      ListenerSupport.Call<PurchaseFlow, ProductInfo>(this.enabledInactiveCallback, new Action<PurchaseFlow, ProductInfo>(this.flowListener.Confirm), PurchaseFlow.Instance, product);
    }

    public void Confirmed(bool isOK)
    {
      if (isOK && this.impl.Confirmed())
        return;
      this.OnPurchaseResult(ResultCode.Canceled, (FulfillmentResult) null);
    }

    public void InputBirthday(int year, int month, int date)
    {
      int birthYear = year;
      new Gsc.Purchase.API.App.ChargeAge(month, birthYear) { BirthDay = date }.ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel).OnResponse((VoidCallbackWithError<Gsc.Purchase.API.Response.ChargeAge>) ((response, error) =>
      {
        if (error == null)
          this.RenderProducts();
        else
          ListenerSupport.Call<PurchaseFlow>(this.enabledInactiveCallback, new Action<PurchaseFlow>(this.flowListener.OnInvalidBirthday), PurchaseFlow.Instance);
      }));
    }

    private void RenderProducts()
    {
      if (!this.HasCreditLimit || this.ProductList == null || this.ProductList.Length == 0)
        ListenerSupport.Call<PurchaseFlow, ProductInfo[]>(this.enabledInactiveCallback, new Action<PurchaseFlow, ProductInfo[]>(this.flowListener.OnProducts), PurchaseFlow.Instance, this.ProductList);
      else
        new Gsc.Purchase.API.App.ChargeV2Limit(((IEnumerable<ProductInfo>) this.ProductList).Select<ProductInfo, float>((Func<ProductInfo, float>) (x => x.Price)).ToList<float>(), this.ProductList[0].CurrencyCode).ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel).OnResponse((VoidCallbackWithError<Gsc.Purchase.API.Response.ChargeV2Limit>) ((response, error) =>
        {
          if (error == null)
          {
            this.HasCreditLimit = response.HasCreditLimit;
            this.CreditLimit = response.CreditLimit;
            ListenerSupport.Call<PurchaseFlow, ProductInfo[]>(this.enabledInactiveCallback, new Action<PurchaseFlow, ProductInfo[]>(this.flowListener.OnProducts), PurchaseFlow.Instance, this.ProductList);
          }
          else
            ListenerSupport.Call<PurchaseFlow>(this.enabledInactiveCallback, new Action<PurchaseFlow>(this.flowListener.InputBirthday), PurchaseFlow.Instance);
        }));
    }

    public void OnInitResult(ResultCode resultCode)
    {
      Debug.Log((object) (nameof (OnInitResult) + (object) resultCode));
      switch (resultCode)
      {
        case ResultCode.Succeeded:
          this.initialized = true;
          PurchaseFlow.Listener.OnInitialized();
          break;
        case ResultCode.Unavailabled:
          this.unavailabled = true;
          break;
        default:
          RootObject.Instance.DelayInvoke(new Action(this.Init), 1f);
          break;
      }
    }

    public void OnProductResult(ResultCode resultCode, ProductInfo[] products)
    {
      if (resultCode == ResultCode.Succeeded && products != null)
        this.ProductList = products;
      else
        RootObject.Instance.DelayInvoke(new Action(this.UpdateProducts), 1f);
    }

    public void OnPurchaseResult(ResultCode resultCode, FulfillmentResult result)
    {
      this.isProcessing = false;
      if (resultCode == ResultCode.Succeeded)
      {
        foreach (FulfillmentResult.OrderInfo succeededTransaction in result.SucceededTransactions)
          this.impl.Consume(succeededTransaction.TransactionId);
        foreach (FulfillmentResult.OrderInfo duplicatedTransaction in result.DuplicatedTransactions)
          this.impl.Consume(duplicatedTransaction.TransactionId);
        if (result.SucceededTransactions.Length == 0 && result.DuplicatedTransactions.Length == 0)
          resultCode = ResultCode.AlreadyOwned;
      }
      if (this.flowListener != null)
        ListenerSupport.CallResult(this.enabledInactiveCallback, (IPurchaseResultListener) this.flowListener, resultCode, result);
      ListenerSupport.CallResult(this.enabledInactiveCallback, (IPurchaseResultListener) PurchaseFlow.Listener, resultCode, result);
      bool flag = resultCode == ResultCode.Succeeded;
      if (this.flowListener != null)
        ListenerSupport.Call<bool>(this.enabledInactiveCallback, new Action<bool>(((IPurchaseResultListener) this.flowListener).OnFinished), flag);
      ListenerSupport.Call<bool>(this.enabledInactiveCallback, new Action<bool>(((IPurchaseResultListener) PurchaseFlow.Listener).OnFinished), flag);
      if (!flag || this.flowListener == null)
        return;
      this.RenderProducts();
    }

    public class Observer : MonoBehaviour
    {
      private int cachedInstanceId;

      public Observer()
      {
        base.\u002Ector();
      }

      private void Awake()
      {
        this.cachedInstanceId = ((Object) this).GetInstanceID();
      }

      private void OnApplicationPause(bool pauseState)
      {
        if (pauseState)
          return;
        PurchaseFlow.Resume();
      }
    }
  }
}
