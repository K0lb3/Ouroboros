// Decompiled with JetBrains decompiler
// Type: SRPG.ReqProductBuy
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqProductBuy : WebAPI
  {
    public ReqProductBuy(string productID, string receipt, string transactionID, Network.ResponseCallback response)
    {
      this.name = "product/buy";
      this.body = string.Empty;
      ReqProductBuy reqProductBuy1 = this;
      reqProductBuy1.body = reqProductBuy1.body + "\"productid\":\"" + productID + "\",";
      ReqProductBuy reqProductBuy2 = this;
      reqProductBuy2.body = reqProductBuy2.body + "\"receipt\":\"" + receipt + "\",";
      ReqProductBuy reqProductBuy3 = this;
      reqProductBuy3.body = reqProductBuy3.body + "\"transactionid\":\"" + transactionID + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
