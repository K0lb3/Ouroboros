// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemGuerrillaShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqItemGuerrillaShopBuypaid : WebAPI
  {
    public ReqItemGuerrillaShopBuypaid(int id, int buynum, Network.ResponseCallback response)
    {
      this.name = "shop/guerrilla/buy";
      ReqItemGuerrillaShopBuypaid guerrillaShopBuypaid1 = this;
      guerrillaShopBuypaid1.body = guerrillaShopBuypaid1.body + "\"id\":" + id.ToString() + ",";
      ReqItemGuerrillaShopBuypaid guerrillaShopBuypaid2 = this;
      guerrillaShopBuypaid2.body = guerrillaShopBuypaid2.body + "\"buynum\":" + buynum.ToString();
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
