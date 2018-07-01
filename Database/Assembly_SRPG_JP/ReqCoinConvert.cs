// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCoinConvert
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqCoinConvert : WebAPI
  {
    public ReqCoinConvert(Network.ResponseCallback response)
    {
      this.name = "shop/event/coinconvert";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }
  }
}
