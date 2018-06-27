// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
