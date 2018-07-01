// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemShop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqItemShop : WebAPI
  {
    public ReqItemShop(string iname, Network.ResponseCallback response)
    {
      this.name = "shop";
      this.body = WebAPI.GetRequestString("\"iname\":\"" + iname + "\"");
      this.callback = response;
    }
  }
}
