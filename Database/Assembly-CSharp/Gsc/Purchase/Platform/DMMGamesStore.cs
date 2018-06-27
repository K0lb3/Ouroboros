// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.DMMGamesStore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Core;
using Gsc.Network;
using Gsc.Purchase.API.PAS.DMMGamesStore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Gsc.Purchase.Platform
{
  public class DMMGamesStore : IPurchaseFlowImpl
  {
    private readonly PurchaseHandler handler;
    private ProductInfo processProduct;

    public DMMGamesStore(PurchaseHandler handler)
    {
      this.handler = handler;
    }

    private static int ViewerID
    {
      get
      {
        return Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.ViewerId;
      }
    }

    private static string OnetimeToken
    {
      get
      {
        return Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.OnetimeToken;
      }
    }

    public void Init(string[] productIds)
    {
      this.UpdateProducts(productIds);
    }

    public void Resume()
    {
      Gsc.Purchase.Platform.DMMGamesStore.InnerFlow.Resume(this.handler);
    }

    public bool Purchase(ProductInfo product)
    {
      this.processProduct = product;
      this.handler.Confirm(product);
      return true;
    }

    public bool Confirmed()
    {
      if (this.processProduct == null)
        return false;
      PurchaseHandler.Log(0, "dmmgamesstore.Purchase", "product_id: " + this.processProduct.ID);
      Gsc.Purchase.Platform.DMMGamesStore.InnerFlow.Purchase(this.handler, this.processProduct.ID);
      this.processProduct = (ProductInfo) null;
      return true;
    }

    public void UpdateProducts(string[] productIds)
    {
      new Gsc.Purchase.API.App.ProductList().ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel).OnResponse((VoidCallbackWithError<Gsc.Purchase.API.Response.ProductList>) ((r, e) =>
      {
        if (e != null)
        {
          if (!this.handler.initialized)
            this.handler.OnInitResult(ResultCode.Failed);
          else
            this.handler.OnProductResult(ResultCode.Failed, (ProductInfo[]) null);
        }
        else
        {
          Gsc.Purchase.API.Response.ProductList.ProductData_t[] array = ((IEnumerable<Gsc.Purchase.API.Response.ProductList.ProductData_t>) r.Products).Where<Gsc.Purchase.API.Response.ProductList.ProductData_t>((Func<Gsc.Purchase.API.Response.ProductList.ProductData_t, bool>) (x =>
          {
            if (x.Currency == "JPY")
              return Array.IndexOf<string>(productIds, x.ProductId) >= 0;
            return false;
          })).ToArray<Gsc.Purchase.API.Response.ProductList.ProductData_t>();
          ProductInfo[] products = new ProductInfo[array.Length];
          for (int index = 0; index < array.Length; ++index)
          {
            Gsc.Purchase.API.Response.ProductList.ProductData_t productDataT = array[index];
            products[index] = new ProductInfo(productDataT.ProductId, productDataT.Name, productDataT.Description, productDataT.LocalizedPrice, productDataT.Currency, productDataT.Price);
          }
          this.handler.OnProductResult(ResultCode.Succeeded, products);
          if (this.handler.initialized)
            return;
          this.handler.OnInitResult(ResultCode.Succeeded);
          this.Resume();
        }
      }));
    }

    public void Consume(string transactionId)
    {
      PurchaseHandler.Log(0, "dmmgamesstore.Consume", "transaction_id: " + transactionId);
      new Consume(Gsc.Purchase.Platform.DMMGamesStore.ViewerID, Gsc.Purchase.Platform.DMMGamesStore.OnetimeToken, transactionId).Cast();
    }

    private class InnerFlow
    {
      private const float TIMEOUT_SECONDS = 30f;
      private readonly PurchaseHandler handler;
      private string waitingPaymentId;
      private string paymentStatus;

      private InnerFlow(PurchaseHandler handler)
      {
        this.handler = handler;
      }

      public static void Purchase(PurchaseHandler handler, string productId)
      {
        RootObject.Instance.StartCoroutine(new Gsc.Purchase.Platform.DMMGamesStore.InnerFlow(handler)._Purchase(productId));
      }

      public static void Resume(PurchaseHandler handler)
      {
        RootObject.Instance.StartCoroutine(Gsc.Purchase.Platform.DMMGamesStore.InnerFlow._Resume(handler));
      }

      [DebuggerHidden]
      private IEnumerator _Purchase(string productId)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new Gsc.Purchase.Platform.DMMGamesStore.InnerFlow.\u003C_Purchase\u003Ec__IteratorB() { productId = productId, \u003C\u0024\u003EproductId = productId, \u003C\u003Ef__this = this };
      }

      [DebuggerHidden]
      private IEnumerator _GetPaymentId(string productId)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new Gsc.Purchase.Platform.DMMGamesStore.InnerFlow.\u003C_GetPaymentId\u003Ec__IteratorC() { productId = productId, \u003C\u0024\u003EproductId = productId, \u003C\u003Ef__this = this };
      }

      [DebuggerHidden]
      private IEnumerator _WaitPurchase()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new Gsc.Purchase.Platform.DMMGamesStore.InnerFlow.\u003C_WaitPurchase\u003Ec__IteratorD() { \u003C\u003Ef__this = this };
      }

      [DebuggerHidden]
      private static IEnumerator _Resume(PurchaseHandler handler)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new Gsc.Purchase.Platform.DMMGamesStore.InnerFlow.\u003C_Resume\u003Ec__IteratorE() { handler = handler, \u003C\u0024\u003Ehandler = handler };
      }

      private static IWebTask CreateFulfillmentTask(PurchaseHandler handler, List<string> paymentIds)
      {
        List<Gsc.Purchase.API.PAS.DMMGamesStore.Fulfillment.PurchaseData_t> purchaseDataList = new List<Gsc.Purchase.API.PAS.DMMGamesStore.Fulfillment.PurchaseData_t>(paymentIds.Count);
        for (int index = 0; index < paymentIds.Count; ++index)
          purchaseDataList.Add(new Gsc.Purchase.API.PAS.DMMGamesStore.Fulfillment.PurchaseData_t(Gsc.Purchase.Platform.DMMGamesStore.ViewerID, Gsc.Purchase.Platform.DMMGamesStore.OnetimeToken, paymentIds[index]));
        WebTask<Gsc.Purchase.API.PAS.DMMGamesStore.Fulfillment, Gsc.Purchase.API.Response.Fulfillment> webTask = new Gsc.Purchase.API.PAS.DMMGamesStore.Fulfillment(Gsc.Auth.Session.DefaultSession.DeviceID, purchaseDataList).ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
        webTask.OnResponse((VoidCallbackWithError<Gsc.Purchase.API.Response.Fulfillment>) ((response, error) =>
        {
          if (error != null)
            handler.OnPurchaseResult(ResultCode.AlreadyOwned, (FulfillmentResult) null);
          else
            handler.OnPurchaseResult(ResultCode.Succeeded, response.Result);
        }));
        return (IWebTask) webTask;
      }
    }
  }
}
