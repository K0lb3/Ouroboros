// Decompiled with JetBrains decompiler
// Type: SRPG.ReqEventShopItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqEventShopItemList : WebAPI
  {
    public ReqEventShopItemList(string shop_name, Network.ResponseCallback response)
    {
      this.name = "shop/event";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"shopName\":\"");
      stringBuilder.Append(shop_name);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
