// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusLine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
