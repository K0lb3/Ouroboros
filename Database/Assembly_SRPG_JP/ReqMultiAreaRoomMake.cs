// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiAreaRoomMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ReqMultiAreaRoomMake : WebAPI
  {
    public ReqMultiAreaRoomMake(string iname, string comment, string passCode, bool isPrivate, bool limit, int unitlv, bool clear, Vector2 location, Network.ResponseCallback response)
    {
      this.name = "btl/room/areaquest/make";
      this.body = string.Empty;
      ReqMultiAreaRoomMake multiAreaRoomMake1 = this;
      multiAreaRoomMake1.body = multiAreaRoomMake1.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      ReqMultiAreaRoomMake multiAreaRoomMake2 = this;
      multiAreaRoomMake2.body = multiAreaRoomMake2.body + ",\"comment\":\"" + JsonEscape.Escape(comment) + "\"";
      ReqMultiAreaRoomMake multiAreaRoomMake3 = this;
      multiAreaRoomMake3.body = multiAreaRoomMake3.body + ",\"pwd\":\"" + JsonEscape.Escape(passCode) + "\"";
      ReqMultiAreaRoomMake multiAreaRoomMake4 = this;
      multiAreaRoomMake4.body = multiAreaRoomMake4.body + ",\"private\":" + (object) (!isPrivate ? 0 : 1);
      ReqMultiAreaRoomMake multiAreaRoomMake5 = this;
      multiAreaRoomMake5.body = multiAreaRoomMake5.body + ",\"req_at\":" + (object) Network.GetServerTime();
      ReqMultiAreaRoomMake multiAreaRoomMake6 = this;
      multiAreaRoomMake6.body = multiAreaRoomMake6.body + ",\"limit\":" + (object) (!limit ? 0 : 1);
      ReqMultiAreaRoomMake multiAreaRoomMake7 = this;
      multiAreaRoomMake7.body = multiAreaRoomMake7.body + ",\"unitlv\":" + (object) unitlv;
      ReqMultiAreaRoomMake multiAreaRoomMake8 = this;
      multiAreaRoomMake8.body = multiAreaRoomMake8.body + ",\"clear\":" + (object) (!clear ? 0 : 1);
      this.body += ",\"location\":{";
      ReqMultiAreaRoomMake multiAreaRoomMake9 = this;
      multiAreaRoomMake9.body = multiAreaRoomMake9.body + "\"lat\":" + (object) (float) location.x + ",";
      ReqMultiAreaRoomMake multiAreaRoomMake10 = this;
      multiAreaRoomMake10.body = multiAreaRoomMake10.body + "\"lng\":" + (object) (float) location.y + "}";
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
