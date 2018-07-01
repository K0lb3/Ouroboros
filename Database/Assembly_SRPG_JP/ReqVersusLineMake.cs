// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLineMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusLineMake : WebAPI
  {
    public ReqVersusLineMake(string roomname, Network.ResponseCallback response)
    {
      this.name = "vs/friendmatch/line/make";
      this.body = string.Empty;
      ReqVersusLineMake reqVersusLineMake = this;
      reqVersusLineMake.body = reqVersusLineMake.body + "\"token\":\"" + JsonEscape.Escape(roomname) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
