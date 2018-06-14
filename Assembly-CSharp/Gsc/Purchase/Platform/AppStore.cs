// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.AppStore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using Gsc.Purchase.API.Gacct.AppStore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsc.Purchase.Platform
{
  public class AppStore : FlowWithPurchaseKit
  {
    public AppStore(PurchaseHandler handler)
      : base(handler)
    {
    }

    protected override IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Gsc.Purchase.Platform.AppStore.\u003CCreateFulfillmentTask\u003Ec__AnonStorey154 taskCAnonStorey154 = new Gsc.Purchase.Platform.AppStore.\u003CCreateFulfillmentTask\u003Ec__AnonStorey154();
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey154.response = response;
      // ISSUE: reference to a compiler-generated field
      taskCAnonStorey154.\u003C\u003Ef__this = this;
      List<Verify.PurchaseData_t> receipts = new List<Verify.PurchaseData_t>();
      bool flag = false;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Gsc.Purchase.Platform.AppStore.\u003CCreateFulfillmentTask\u003Ec__AnonStorey153 taskCAnonStorey153 = new Gsc.Purchase.Platform.AppStore.\u003CCreateFulfillmentTask\u003Ec__AnonStorey153();
      // ISSUE: reference to a compiler-generated field
      foreach (PurchaseKit.PurchaseData purchaseData in taskCAnonStorey154.response.Values)
      {
        // ISSUE: reference to a compiler-generated field
        taskCAnonStorey153.purchase = purchaseData;
        // ISSUE: reference to a compiler-generated method
        ProductInfo productInfo = ((IEnumerable<ProductInfo>) PurchaseFlow.ProductList).Where<ProductInfo>(new Func<ProductInfo, bool>(taskCAnonStorey153.\u003C\u003Em__1A)).FirstOrDefault<ProductInfo>();
        // ISSUE: reference to a compiler-generated field
        receipts.Add(new Verify.PurchaseData_t(productInfo == null ? (string) null : productInfo.CurrencyCode, productInfo == null ? 0.0f : productInfo.Price, taskCAnonStorey153.purchase.ID));
        // ISSUE: reference to a compiler-generated field
        flag |= taskCAnonStorey153.purchase.ProductId.Contains("bundle");
      }
      // ISSUE: reference to a compiler-generated field
      WebTask<Verify, Verify.Response> webTask = new Verify(receipts, taskCAnonStorey154.response.Meta.Data0) { IsBundle = flag }.ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
      // ISSUE: reference to a compiler-generated method
      webTask.OnResponse(new VoidCallbackWithError<Verify.Response>(taskCAnonStorey154.\u003C\u003Em__1B));
      return (IWebTask) webTask;
    }

    private void OnFulfillmentResult(FulfillmentResult result, IErrorResponse error)
    {
      if (error != null)
        this.handler.OnPurchaseResult(ResultCode.AlreadyOwned, (FulfillmentResult) null);
      else if ((result.SucceededTransactions == null || result.SucceededTransactions.Length == 0) && (result.DuplicatedTransactions == null || result.SucceededTransactions.Length == 0))
        this.Resume();
      else
        this.handler.OnPurchaseResult(ResultCode.Succeeded, result);
    }
  }
}
