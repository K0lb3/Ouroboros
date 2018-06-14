// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGetAccessToken
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqGetAccessToken : WebAPI
  {
    public ReqGetAccessToken(string deviceid, string secretkey, Network.ResponseCallback response)
    {
      this.name = "gauth/accesstoken";
      this.body = "{";
      ReqGetAccessToken reqGetAccessToken1 = this;
      reqGetAccessToken1.body = reqGetAccessToken1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqGetAccessToken reqGetAccessToken2 = this;
      reqGetAccessToken2.body = reqGetAccessToken2.body + "\"device_id\":\"" + deviceid + "\",";
      ReqGetAccessToken reqGetAccessToken3 = this;
      reqGetAccessToken3.body = reqGetAccessToken3.body + "\"secret_key\":\"" + secretkey + "\"";
      if (!string.IsNullOrEmpty(GameManager.GetIDFA))
      {
        ReqGetAccessToken reqGetAccessToken4 = this;
        reqGetAccessToken4.body = reqGetAccessToken4.body + ",\"idfa\":\"" + GameManager.GetIDFA + "\"";
      }
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
