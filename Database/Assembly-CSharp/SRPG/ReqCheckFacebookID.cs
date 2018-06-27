// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCheckFacebookID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
