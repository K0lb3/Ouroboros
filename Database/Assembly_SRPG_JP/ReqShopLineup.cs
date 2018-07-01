// Decompiled with JetBrains decompiler
// Type: SRPG.ReqShopLineup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqShopLineup : WebAPI
  {
    public ReqShopLineup(string shop_name, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "shop/itemlist";
      stringBuilder.Append("\"shopName\":\"");
      stringBuilder.Append(shop_name);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
