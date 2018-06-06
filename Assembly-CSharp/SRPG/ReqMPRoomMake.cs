// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoomMake
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMPRoomMake : WebAPI
  {
    public ReqMPRoomMake(string iname, string comment, string passCode, bool isPrivate, Network.ResponseCallback response)
    {
      this.name = "btl/room/make";
      this.body = string.Empty;
      ReqMPRoomMake reqMpRoomMake1 = this;
      reqMpRoomMake1.body = reqMpRoomMake1.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      ReqMPRoomMake reqMpRoomMake2 = this;
      reqMpRoomMake2.body = reqMpRoomMake2.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
      ReqMPRoomMake reqMpRoomMake3 = this;
      reqMpRoomMake3.body = reqMpRoomMake3.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
      ReqMPRoomMake reqMpRoomMake4 = this;
      reqMpRoomMake4.body = reqMpRoomMake4.body + ",\"private\":" + (object) (!isPrivate ? 0 : 1);
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public int roomid;
      public string app_id;
      public string token;
    }
  }
}
