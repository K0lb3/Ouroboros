// Decompiled with JetBrains decompiler
// Type: SRPG.ReqShopLineup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
