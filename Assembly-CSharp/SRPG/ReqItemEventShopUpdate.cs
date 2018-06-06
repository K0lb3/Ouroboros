// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemEventShopUpdate
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
