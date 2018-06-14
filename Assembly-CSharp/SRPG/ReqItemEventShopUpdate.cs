// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemEventShopUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqItemEventShopUpdate : WebAPI
  {
    public ReqItemEventShopUpdate(string iname, string costiname, Network.ResponseCallback response)
    {
      this.name = "shop/update";
      this.body = "\"iname\":\"" + iname + "\",";
      ReqItemEventShopUpdate itemEventShopUpdate = this;
      itemEventShopUpdate.body = itemEventShopUpdate.body + "\"costiname\":\"" + costiname + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
