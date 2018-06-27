// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.GooglePlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using Gsc.Purchase.API.Gacct.GooglePlay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsc.Purchase.Platform
{
  public class GooglePlay : FlowWithPurchaseKit
  {
    public GooglePlay(PurchaseHandler handler)
      : base(handler)
    {
    }

    protected override IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Gsc.Purchase.Platform.GooglePlay.\u003CCreateFulfillmentTask\u003Ec__AnonStorey15B taskCAnonStorey15B = new Gsc.Purchase.Platform.GooglePlay.\u003CCreateFulfillmentTask\u003Ec__AnonStorey15B();
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey15B.response = response;
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey15B.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey15B.count = 0;
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey15B.hasError = false;
      IWebTask webTask1 = (IWebTask) null;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey15B.succeededTransactions = new FulfillmentResult.OrderInfo[taskCAnonStorey15B.response.Values.Length];
      // ISSUE: reference to a compiler-generated field
      for (int index = 0; index < taskCAnonStorey15B.succeededTransactions.Length; ++index)
      {
        // ISSUE: reference to a compiler-generated field
        PurchaseKit.PurchaseData purchaseData = taskCAnonStorey15B.response.Values[index];
        // ISSUE: reference to a compiler-generated field
        taskCAnonStorey15B.succeededTransactions[index] = new FulfillmentResult.OrderInfo(0, 0, purchaseData.ProductId, purchaseData.ID);
      }
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Gsc.Purchase.Platform.GooglePlay.\u003CCreateFulfillmentTask\u003Ec__AnonStorey15A taskCAnonStorey15A = new Gsc.Purchase.Platform.GooglePlay.\u003CCreateFulfillmentTask\u003Ec__AnonStorey15A();
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey15A.\u003C\u003Ef__ref\u0024347 = taskCAnonStorey15B;
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey15A.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      foreach (PurchaseKit.PurchaseData purchaseData in taskCAnonStorey15B.response.Values)
      {
        // ISSUE: reference to a compiler-generated field
        taskCAnonStorey15A.purchase = purchaseData;
        // ISSUE: reference to a compiler-generated method
        ProductInfo productInfo = ((IEnumerable<ProductInfo>) PurchaseFlow.ProductList).Where<ProductInfo>(new Func<ProductInfo, bool>(taskCAnonStorey15A.\u003C\u003Em__25)).FirstOrDefault<ProductInfo>();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        WebTask<Verify, Verify.Response> webTask2 = new Verify(productInfo == null ? (string) null : productInfo.CurrencyCode, productInfo == null ? 0.0f : productInfo.Price, taskCAnonStorey15A.purchase.Data1, taskCAnonStorey15A.purchase.Data0) { IsBundle = taskCAnonStorey15A.purchase.ProductId.Contains("bundle") }.ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
        webTask1 = (IWebTask) webTask2;
        // ISSUE: reference to a compiler-generated method
        webTask2.OnResponse(new VoidCallbackWithError<Verify.Response>(taskCAnonStorey15A.\u003C\u003Em__26));
      }
      return webTask1;
    }
  }
}
