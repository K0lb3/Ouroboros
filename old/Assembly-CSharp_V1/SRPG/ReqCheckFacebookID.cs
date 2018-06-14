// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCheckFacebookID
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqCheckFacebookID : WebAPI
  {
    public ReqCheckFacebookID(string udid, Network.ResponseCallback response)
    {
      this.name = "player/chkfb";
      this.body = "{";
      ReqCheckFacebookID reqCheckFacebookId1 = this;
      reqCheckFacebookId1.body = reqCheckFacebookId1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqCheckFacebookID reqCheckFacebookId2 = this;
      reqCheckFacebookId2.body = reqCheckFacebookId2.body + "\"device_id\":\"" + udid + "\"";
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
