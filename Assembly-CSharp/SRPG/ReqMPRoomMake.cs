// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoomMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMPRoomMake : WebAPI
  {
    public ReqMPRoomMake(string iname, string comment, string passCode, bool isPrivate, bool limit, int unitlv, bool clear, Network.ResponseCallback response)
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
      ReqMPRoomMake reqMpRoomMake5 = this;
      reqMpRoomMake5.body = reqMpRoomMake5.body + ",\"req_at\":" + (object) Network.GetServerTime();
      ReqMPRoomMake reqMpRoomMake6 = this;
      reqMpRoomMake6.body = reqMpRoomMake6.body + ",\"limit\":" + (object) (!limit ? 0 : 1);
      ReqMPRoomMake reqMpRoomMake7 = this;
      reqMpRoomMake7.body = reqMpRoomMake7.body + ",\"unitlv\":" + (object) unitlv;
      ReqMPRoomMake reqMpRoomMake8 = this;
      reqMpRoomMake8.body = reqMpRoomMake8.body + ",\"clear\":" + (object) (!clear ? 0 : 1);
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
