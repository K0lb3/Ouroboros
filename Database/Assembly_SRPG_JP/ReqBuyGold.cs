// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBuyGold
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBuyGold : WebAPI
  {
    public ReqBuyGold(int coin, Network.ResponseCallback response)
    {
      this.name = "shop/gold/buy";
      this.body = WebAPI.GetRequestString("\"coin\":" + coin.ToString());
      this.callback = response;
    }
  }
}
