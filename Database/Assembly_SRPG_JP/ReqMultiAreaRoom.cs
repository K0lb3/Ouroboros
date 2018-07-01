// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiAreaRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ReqMultiAreaRoom : WebAPI
  {
    public ReqMultiAreaRoom(string fuid, string[] iname, Vector2 location, Network.ResponseCallback response)
    {
      this.name = "btl/room/areaquest";
      this.body = string.Empty;
      if (!string.IsNullOrEmpty(fuid))
      {
        ReqMultiAreaRoom reqMultiAreaRoom = this;
        reqMultiAreaRoom.body = reqMultiAreaRoom.body + "\"fuid\":\"" + JsonEscape.Escape(fuid) + "\"";
      }
      if (iname != null && iname.Length > 0)
      {
        if (!string.IsNullOrEmpty(this.body))
          this.body += ",";
        this.body += "\"iname\":[";
        for (int index = 0; index < iname.Length; ++index)
        {
          if (index != 0)
            this.body += ",";
          ReqMultiAreaRoom reqMultiAreaRoom = this;
          reqMultiAreaRoom.body = reqMultiAreaRoom.body + "\"" + JsonEscape.Escape(iname[index]) + "\"";
        }
        this.body += "]";
      }
      if (!string.IsNullOrEmpty(this.body))
        this.body += ",";
      this.body += "\"location\":{";
      ReqMultiAreaRoom reqMultiAreaRoom1 = this;
      reqMultiAreaRoom1.body = reqMultiAreaRoom1.body + "\"lat\":" + (object) (float) location.x + ",";
      ReqMultiAreaRoom reqMultiAreaRoom2 = this;
      reqMultiAreaRoom2.body = reqMultiAreaRoom2.body + "\"lng\":" + (object) (float) location.y + "}";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public MultiPlayAPIRoom[] rooms;
    }
  }
}
