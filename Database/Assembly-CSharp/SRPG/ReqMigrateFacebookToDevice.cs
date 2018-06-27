// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMigrateFacebookToDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMigrateFacebookToDevice : WebAPI
  {
    public ReqMigrateFacebookToDevice(string secretkey, string udid, string accesstoken, Network.ResponseCallback response)
    {
      this.name = "gauth/facebook/sso/migrate";
      this.body = "{";
      ReqMigrateFacebookToDevice facebookToDevice1 = this;
      facebookToDevice1.body = facebookToDevice1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqMigrateFacebookToDevice facebookToDevice2 = this;
      facebookToDevice2.body = facebookToDevice2.body + "\"secret_key\":\"" + secretkey + "\",";
      ReqMigrateFacebookToDevice facebookToDevice3 = this;
      facebookToDevice3.body = facebookToDevice3.body + "\"device_id\":\"" + udid + "\",";
      ReqMigrateFacebookToDevice facebookToDevice4 = this;
      facebookToDevice4.body = facebookToDevice4.body + "\"access_token\":\"" + accesstoken + "\"";
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
