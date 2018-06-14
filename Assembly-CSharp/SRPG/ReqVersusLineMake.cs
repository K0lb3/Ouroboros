// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLineMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
