// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.AUMarket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using Gsc.Purchase.API.Gacct.AUMarket;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gsc.Purchase.Platform
{
  public class AUMarket : FlowWithPurchaseKit
  {
    public const int RESULT_AUMARKET_OVER_CREDIT_LIMIT = 49;

    public AUMarket(PurchaseHandler handler)
      : base(handler)
    {
    }

    public override void Init(string[] productIds)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      ((AndroidJavaObject) ((AndroidJavaObject) new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity")).Call("runOnUiThread", new object[1]
      {
        (object) new AndroidJavaRunnable((object) new Gsc.Purchase.Platform.AUMarket.\u003CInit\u003Ec__AnonStorey14B()
        {
          productIds = productIds,
          \u003C\u003Ef__this = this
        }, __methodptr(\u003C\u003Em__10))
      });
    }

    public override void UpdateProducts(string[] productIds)
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
          base.Init((string[]) null);
        }
      }));
    }

    public override void Resume()
    {
    }

    protected override IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Gsc.Purchase.Platform.AUMarket.\u003CCreateFulfillmentTask\u003Ec__AnonStorey14E taskCAnonStorey14E = new Gsc.Purchase.Platform.AUMarket.\u003CCreateFulfillmentTask\u003Ec__AnonStorey14E();
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey14E.response = response;
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey14E.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      List<Verify.PurchaseData_t> purchaseDataList = new List<Verify.PurchaseData_t>(taskCAnonStorey14E.response.Values.Length);
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Gsc.Purchase.Platform.AUMarket.\u003CCreateFulfillmentTask\u003Ec__AnonStorey14D taskCAnonStorey14D = new Gsc.Purchase.Platform.AUMarket.\u003CCreateFulfillmentTask\u003Ec__AnonStorey14D();
      // ISSUE: reference to a compiler-generated field
      foreach (PurchaseKit.PurchaseData purchaseData in taskCAnonStorey14E.response.Values)
      {
        // ISSUE: reference to a compiler-generated field
        taskCAnonStorey14D.purchase = purchaseData;
        // ISSUE: reference to a compiler-generated method
        ProductInfo productInfo = ((IEnumerable<ProductInfo>) PurchaseFlow.ProductList).Where<ProductInfo>(new Func<ProductInfo, bool>(taskCAnonStorey14D.\u003C\u003Em__12)).FirstOrDefault<ProductInfo>();
        // ISSUE: reference to a compiler-generated field
        purchaseDataList.Add(new Verify.PurchaseData_t(productInfo.CurrencyCode, productInfo.Price, taskCAnonStorey14D.purchase.ID));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      WebTask<Verify, Verify.Response> webTask = new Verify(taskCAnonStorey14E.response.Meta.Data1, taskCAnonStorey14E.response.Meta.Data0, purchaseDataList).ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
      // ISSUE: reference to a compiler-generated method
      webTask.OnResponse(new VoidCallbackWithError<Verify.Response>(taskCAnonStorey14E.\u003C\u003Em__13));
      return (IWebTask) webTask;
    }
  }
}
