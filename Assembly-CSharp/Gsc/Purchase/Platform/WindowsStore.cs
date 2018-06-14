// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.Platform.WindowsStore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Auth;
using Gsc.Network;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsc.Purchase.Platform
{
  public class WindowsStore : FlowWithPurchaseKit
  {
    public WindowsStore(PurchaseHandler handler)
      : base(handler)
    {
    }

    protected override IWebTask CreateFulfillmentTask(PurchaseKit.PurchaseResponse response)
    {
      List<Gsc.Purchase.API.PAS.WindowsStore.Fulfillment.PurchaseData_t> purchaseDataList = new List<Gsc.Purchase.API.PAS.WindowsStore.Fulfillment.PurchaseData_t>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Gsc.Purchase.Platform.WindowsStore.\u003CCreateFulfillmentTask\u003Ec__AnonStorey15C taskCAnonStorey15C = new Gsc.Purchase.Platform.WindowsStore.\u003CCreateFulfillmentTask\u003Ec__AnonStorey15C();
      foreach (PurchaseKit.PurchaseData purchaseData in response.Values)
      {
        // ISSUE: reference to a compiler-generated field
        taskCAnonStorey15C.purchase = purchaseData;
        // ISSUE: reference to a compiler-generated method
        ProductInfo productInfo = ((IEnumerable<ProductInfo>) PurchaseFlow.ProductList).Where<ProductInfo>(new Func<ProductInfo, bool>(taskCAnonStorey15C.\u003C\u003Em__27)).FirstOrDefault<ProductInfo>();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        purchaseDataList.Add(new Gsc.Purchase.API.PAS.WindowsStore.Fulfillment.PurchaseData_t(productInfo == null ? (string) null : productInfo.CurrencyCode, productInfo == null ? 0.0f : productInfo.Price, taskCAnonStorey15C.purchase.Data0, taskCAnonStorey15C.purchase.ID));
      }
      WebTask<Gsc.Purchase.API.PAS.WindowsStore.Fulfillment, Gsc.Purchase.API.Response.Fulfillment> webTask = new Gsc.Purchase.API.PAS.WindowsStore.Fulfillment(Session.DefaultSession.DeviceID, purchaseDataList).ToWebTask(WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel);
      webTask.OnResponse(new VoidCallbackWithError<Gsc.Purchase.API.Response.Fulfillment>(((FlowWithPurchaseKit) this).OnFulfillmentResponse));
      return (IWebTask) webTask;
    }
  }
}
