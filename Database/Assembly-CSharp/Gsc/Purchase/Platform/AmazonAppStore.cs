// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.AmazonAppStore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Core;
using Gsc.Network;
using Gsc.Purchase.API.Gacct.AmazonAppStore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace Gsc.Purchase.Platform
{
  public class AmazonAppStore : FlowWithPurchaseKit
  {
    private bool done;

    public AmazonAppStore(PurchaseHandler handler)
      : base(handler)
    {
    }

    [DebuggerHidden]
    public IEnumerator InitProducts(string[] productIds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Gsc.Purchase.Platform.AmazonAppStore.\u003CInitProducts\u003Ec__IteratorA() { productIds = productIds, \u003C\u0024\u003EproductIds = productIds, \u003C\u003Ef__this = this };
    }

    public override void Init(string[] productIds)
    {
      this.done = false;
      DebugUtility.Log("HERE");
      // ISSUE: method pointer
      ((AndroidJavaObject) ((AndroidJavaObject) new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity")).Call("runOnUiThread", new object[1]
      {
        (object) new AndroidJavaRunnable((object) this, __methodptr(\u003CInit\u003Em__17))
      });
      RootObject.Instance.StartCoroutine(this.InitProducts(productIds));
    }

    protected override IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Gsc.Purchase.Platform.AmazonAppStore.\u003CCreateFulfillmentTask\u003Ec__AnonStorey152 taskCAnonStorey152 = new Gsc.Purchase.Platform.AmazonAppStore.\u003CCreateFulfillmentTask\u003Ec__AnonStorey152();
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey152.response = response;
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey152.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey152.count = 0;
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey152.hasError = false;
      IWebTask webTask1 = (IWebTask) null;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey152.succeededTransactions = new FulfillmentResult.OrderInfo[taskCAnonStorey152.response.Values.Length];
      // ISSUE: reference to a compiler-generated field
      for (int index = 0; index < taskCAnonStorey152.succeededTransactions.Length; ++index)
      {
        // ISSUE: reference to a compiler-generated field
        PurchaseKit.PurchaseData purchaseData = taskCAnonStorey152.response.Values[index];
        // ISSUE: reference to a compiler-generated field
        taskCAnonStorey152.succeededTransactions[index] = new FulfillmentResult.OrderInfo(0, 0, purchaseData.ProductId, purchaseData.ID);
      }
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Gsc.Purchase.Platform.AmazonAppStore.\u003CCreateFulfillmentTask\u003Ec__AnonStorey151 taskCAnonStorey151 = new Gsc.Purchase.Platform.AmazonAppStore.\u003CCreateFulfillmentTask\u003Ec__AnonStorey151();
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey151.\u003C\u003Ef__ref\u0024338 = taskCAnonStorey152;
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey151.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      foreach (PurchaseKit.PurchaseData purchaseData in taskCAnonStorey152.response.Values)
      {
        // ISSUE: reference to a compiler-generated field
        taskCAnonStorey151.purchase = purchaseData;
        // ISSUE: reference to a compiler-generated method
        ProductInfo productInfo = ((IEnumerable<ProductInfo>) PurchaseFlow.ProductList).Where<ProductInfo>(new Func<ProductInfo, bool>(taskCAnonStorey151.\u003C\u003Em__18)).FirstOrDefault<ProductInfo>();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        WebTask<Verify, Verify.Response> webTask2 = new Verify(taskCAnonStorey152.response.Meta.Data0, productInfo == null ? (string) null : productInfo.CurrencyCode, productInfo == null ? 0.0f : productInfo.Price, taskCAnonStorey151.purchase.ID) { IsBundle = taskCAnonStorey151.purchase.ProductId.Contains("bundle") }.ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
        webTask1 = (IWebTask) webTask2;
        // ISSUE: reference to a compiler-generated method
        webTask2.OnResponse(new VoidCallbackWithError<Verify.Response>(taskCAnonStorey151.\u003C\u003Em__19));
      }
      return webTask1;
    }
  }
}
