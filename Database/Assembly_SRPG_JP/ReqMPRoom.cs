// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMPRoom : WebAPI
  {
    public ReqMPRoom(string fuid, string iname, Network.ResponseCallback response)
    {
      this.name = "btl/room";
      this.body = string.Empty;
      if (!string.IsNullOrEmpty(fuid))
      {
        ReqMPRoom reqMpRoom = this;
        reqMpRoom.body = reqMpRoom.body + "\"fuid\":\"" + JsonEscape.Escape(fuid) + "\"";
      }
      if (!string.IsNullOrEmpty(iname))
      {
        if (!string.IsNullOrEmpty(this.body))
          this.body += ",";
        ReqMPRoom reqMpRoom = this;
        reqMpRoom.body = reqMpRoom.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      }
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public MultiPlayAPIRoom[] rooms;
    }
  }
}
