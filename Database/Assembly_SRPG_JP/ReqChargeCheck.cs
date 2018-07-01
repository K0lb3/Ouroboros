// Decompiled with JetBrains decompiler
// Type: SRPG.ReqChargeCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqChargeCheck : WebAPI
  {
    public ReqChargeCheck(PaymentManager.Product[] products, bool isPurchase, Network.ResponseCallback response)
    {
      this.name = "charge/check";
      this.body = string.Empty;
      this.body += "\"targets\":[";
      for (int index = 0; index < products.Length; ++index)
      {
        this.body += "{";
        ReqChargeCheck reqChargeCheck1 = this;
        reqChargeCheck1.body = reqChargeCheck1.body + "\"product_id\":\"" + products[index].productID + "\",";
        ReqChargeCheck reqChargeCheck2 = this;
        reqChargeCheck2.body = reqChargeCheck2.body + "\"price\":" + (object) products[index].sellPrice;
        this.body += "}";
        if (index != products.Length - 1)
          this.body += ",";
      }
      this.body += "],";
      this.body += "\"currency\":\"JPY\",";
      ReqChargeCheck reqChargeCheck = this;
      reqChargeCheck.body = reqChargeCheck.body + "\"is_purchase\":" + (!isPurchase ? "0" : "1");
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
