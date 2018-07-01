// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGetSessionID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqGetSessionID : WebAPI
  {
    public ReqGetSessionID(string udid, string udid_, string romver, Network.ResponseCallback response)
    {
      this.name = "getsid";
      this.body = "{\"ticket\":" + (object) Network.TicketID + ",\"param\":{";
      ReqGetSessionID reqGetSessionId1 = this;
      reqGetSessionId1.body = reqGetSessionId1.body + "\"udid\":\"" + udid + "\",";
      ReqGetSessionID reqGetSessionId2 = this;
      reqGetSessionId2.body = reqGetSessionId2.body + "\"udid_\":\"" + udid_ + "\",";
      ReqGetSessionID reqGetSessionId3 = this;
      reqGetSessionId3.body = reqGetSessionId3.body + "\"romver\":\"" + romver + "\"";
      this.body += "}}";
      this.callback = response;
    }
  }
}
