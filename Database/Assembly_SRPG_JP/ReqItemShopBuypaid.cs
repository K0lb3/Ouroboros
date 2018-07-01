// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqItemShopBuypaid : WebAPI
  {
    public ReqItemShopBuypaid(string iname, int id, int buynum, Network.ResponseCallback response)
    {
      this.name = "shop/buy";
      this.body = "\"iname\":\"" + iname + "\",";
      ReqItemShopBuypaid reqItemShopBuypaid1 = this;
      reqItemShopBuypaid1.body = reqItemShopBuypaid1.body + "\"id\":" + id.ToString() + ",";
      ReqItemShopBuypaid reqItemShopBuypaid2 = this;
      reqItemShopBuypaid2.body = reqItemShopBuypaid2.body + "\"buynum\":" + buynum.ToString();
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
