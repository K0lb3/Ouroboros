// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGetDeviceID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqGetDeviceID : WebAPI
  {
    public ReqGetDeviceID(string secretkey, string udid, Network.ResponseCallback response)
    {
      this.name = "gauth/register";
      this.body = "{";
      ReqGetDeviceID reqGetDeviceId1 = this;
      reqGetDeviceId1.body = reqGetDeviceId1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqGetDeviceID reqGetDeviceId2 = this;
      reqGetDeviceId2.body = reqGetDeviceId2.body + "\"secret_key\":\"" + secretkey + "\",";
      ReqGetDeviceID reqGetDeviceId3 = this;
      reqGetDeviceId3.body = reqGetDeviceId3.body + "\"udid\":\"" + udid + "\"";
      if (!string.IsNullOrEmpty(GameManager.GetIDFA))
      {
        ReqGetDeviceID reqGetDeviceId4 = this;
        reqGetDeviceId4.body = reqGetDeviceId4.body + ",\"idfa\":\"" + GameManager.GetIDFA + "\"";
      }
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
