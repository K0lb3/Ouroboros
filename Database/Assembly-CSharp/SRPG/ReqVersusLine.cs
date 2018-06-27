// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusLine : WebAPI
  {
    public ReqVersusLine(string roomname, Network.ResponseCallback response)
    {
      this.name = "vs/friendmatch/line/recruitment";
      this.body = string.Empty;
      ReqVersusLine reqVersusLine = this;
      reqVersusLine.body = reqVersusLine.body + "\"token\":\"" + JsonEscape.Escape(roomname) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
