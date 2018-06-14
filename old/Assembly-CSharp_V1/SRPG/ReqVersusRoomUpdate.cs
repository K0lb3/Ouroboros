// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusRoomUpdate
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusRoomUpdate : WebAPI
  {
    public ReqVersusRoomUpdate(int roomID, string comment, string iname, Network.ResponseCallback response)
    {
      this.name = "vs/friendmatch/update";
      this.body = string.Empty;
      ReqVersusRoomUpdate versusRoomUpdate1 = this;
      versusRoomUpdate1.body = versusRoomUpdate1.body + "\"roomid\":" + (object) roomID;
      ReqVersusRoomUpdate versusRoomUpdate2 = this;
      versusRoomUpdate2.body = versusRoomUpdate2.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
      ReqVersusRoomUpdate versusRoomUpdate3 = this;
      versusRoomUpdate3.body = versusRoomUpdate3.body + ",\"quest\":\"" + iname + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
