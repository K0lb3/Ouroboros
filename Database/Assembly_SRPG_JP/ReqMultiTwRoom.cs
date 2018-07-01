// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiTwRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMultiTwRoom : WebAPI
  {
    public ReqMultiTwRoom(string fuid, string iname, int floor, Network.ResponseCallback response)
    {
      this.name = "btl/multi/tower/room";
      this.body = string.Empty;
      ReqMultiTwRoom reqMultiTwRoom = this;
      reqMultiTwRoom.body = reqMultiTwRoom.body + "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public MultiPlayAPIRoom[] rooms;
    }
  }
}
