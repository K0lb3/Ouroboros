// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.FlowWithPurchaseKit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Core;
using Gsc.Network;
using Gsc.Purchase.API.Response;
using System;
using UnityEngine;

namespace Gsc.Purchase.Platform
{
  public abstract class FlowWithPurchaseKit : IPurchaseFlowImpl, IPurchaseListener
  {
    protected readonly PurchaseHandler handler;

    public FlowWithPurchaseKit(PurchaseHandler handler)
    {
      this.handler = handler;
    }

    public virtual void Init(string[] productIds)
    {
      GameObject gameObject = ((Component) NativeRootObject.Instance).get_gameObject();
      PurchaseKit.Init(productIds, (IPurchaseListener) this, gameObject, new PurchaseKit.Logger(PurchaseHandler.Log), new IntPtr());
    }

    public virtual void Resume()
    {
      PurchaseKit.Resume();
    }

    public virtual bool Purchase(ProductInfo product)
    {
      return PurchaseKit.Purchase(product.ID);
    }

    public virtual bool Confirmed()
    {
      return false;
    }

    public virtual void UpdateProducts(string[] productIds)
    {
      PurchaseKit.UpdateProducts(productIds);
    }

    public virtual void Consume(string transactionId)
    {
      PurchaseKit.Consume(transactionId);
    }

    private static ResultCode GetResultCode(int resultCode)
    {
      int num = resultCode;
      switch (num)
      {
        case 0:
          return ResultCode.Succeeded;
        case 2:
          return ResultCode.Unavailabled;
        default:
          if (num == 16)
            return ResultCode.Canceled;
          if (num == 17)
            return ResultCode.AlreadyOwned;
          return num == 32 ? ResultCode.Deferred : ResultCode.Failed;
      }
    }

    public virtual void OnInitResult(int resultCode)
    {
      DebugUtility.LogWarning("FlowWithPurchaseKit::OnInitResult: " + (object) resultCode);
      this.handler.OnInitResult(FlowWithPurchaseKit.GetResultCode(resultCode));
    }

    public virtual void OnProductResult(int resultCode, PurchaseKit.ProductResponse response)
    {
      DebugUtility.LogWarning("FlowWithPurchaseKit::OnProductResult: " + (object) resultCode);
      if (resultCode == 0 && response != null)
      {
        ProductInfo[] products = new ProductInfo[response.Values.Length];
        DebugUtility.LogWarning("FlowWithPurchaseKit::OnProductResult: response.values len " + (object) response.Values.Length);
        for (int index = 0; index < response.Values.Length; ++index)
        {
          PurchaseKit.ProductData productData = response.Values[index];
          products[index] = new ProductInfo(productData.ID, productData.LocalizedTitle, productData.LocalizedDescription, productData.LocalizedPrice, productData.Currency, (float) productData.Price);
        }
        this.handler.OnProductResult(ResultCode.Succeeded, products);
      }
      else
        this.handler.OnProductResult(FlowWithPurchaseKit.GetResultCode(resultCode), (ProductInfo[]) null);
    }

    public virtual void OnPurchaseResult(int resultCode, PurchaseKit.PurchaseResponse response)
    {
      DebugUtility.LogWarning("FlowWithPurchaseKit::OnPurchaseResult: " + (object) resultCode);
      if (resultCode == 0 && response != null)
      {
        DebugUtility.LogWarning("FlowWithPurchaseKit::OnPurchaseResult: " + (object) response.Values.Length);
        this.CreateFulfillmentTask(response);
      }
      else
        this.handler.OnPurchaseResult(FlowWithPurchaseKit.GetResultCode(resultCode), (FulfillmentResult) null);
    }

    protected void OnFulfillmentResponse(Fulfillment response, IErrorResponse error)
    {
      if (error != null)
      {
        DebugUtility.LogWarning("FlowWithPurchaseKit::OnFulfillmentResponse: ERROR");
        this.handler.OnPurchaseResult(ResultCode.AlreadyOwned, (FulfillmentResult) null);
      }
      else
      {
        DebugUtility.LogWarning("FlowWithPurchaseKit::OnFulfillmentResponse: " + (object) response.Result);
        this.handler.OnPurchaseResult(ResultCode.Succeeded, response.Result);
      }
    }

    protected abstract IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response);
  }
}
