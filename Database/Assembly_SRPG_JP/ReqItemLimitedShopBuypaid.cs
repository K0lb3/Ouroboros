// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemLimitedShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqItemLimitedShopBuypaid : WebAPI
  {
    public ReqItemLimitedShopBuypaid(string shopname, int id, int buynum, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "shop/limited/buy";
      stringBuilder.Append("\"shopName\":\"" + shopname + "\",");
      stringBuilder.Append("\"id\":" + id.ToString() + ",");
      stringBuilder.Append("\"buynum\":" + buynum.ToString());
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
