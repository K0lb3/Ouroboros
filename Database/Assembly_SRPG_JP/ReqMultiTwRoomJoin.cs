// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwRoomJoin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMultiTwRoomJoin : WebAPI
  {
    public ReqMultiTwRoomJoin(int roomID, Network.ResponseCallback response, bool LockRoom = false, int floor = 0, bool isInv = false)
    {
      this.name = "btl/multi/tower/join";
      this.body = string.Empty;
      ReqMultiTwRoomJoin reqMultiTwRoomJoin1 = this;
      reqMultiTwRoomJoin1.body = reqMultiTwRoomJoin1.body + "\"roomid\":" + (object) roomID + ",";
      this.body += "\"pwd\":";
      this.body += !LockRoom ? "\"0\"" : "\"1\"";
      ReqMultiTwRoomJoin reqMultiTwRoomJoin2 = this;
      reqMultiTwRoomJoin2.body = reqMultiTwRoomJoin2.body + ",\"floor\":" + (object) floor;
      this.body += ",\"inv\":";
      this.body += (string) (object) (!isInv ? 0 : 1);
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Quest
    {
      public string iname;
      public int floor;
    }

    public class Response
    {
      public string app_id;
      public string token;
      public ReqMultiTwRoomJoin.Quest quest;
    }
  }
}
