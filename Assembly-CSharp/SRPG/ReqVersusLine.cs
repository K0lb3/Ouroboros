// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLine
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
