// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMPRoomUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMPRoomUpdate : WebAPI
  {
    public ReqMPRoomUpdate(int roomID, string comment, string passCode, Network.ResponseCallback response)
    {
      this.name = "btl/room/update";
      this.body = string.Empty;
      ReqMPRoomUpdate reqMpRoomUpdate1 = this;
      reqMpRoomUpdate1.body = reqMpRoomUpdate1.body + "\"roomid\":" + (object) roomID;
      ReqMPRoomUpdate reqMpRoomUpdate2 = this;
      reqMpRoomUpdate2.body = reqMpRoomUpdate2.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
      ReqMPRoomUpdate reqMpRoomUpdate3 = this;
      reqMpRoomUpdate3.body = reqMpRoomUpdate3.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
