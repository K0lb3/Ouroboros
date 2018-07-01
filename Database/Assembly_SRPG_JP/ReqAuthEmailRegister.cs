// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAuthEmailRegister
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqAuthEmailRegister : WebAPI
  {
    public ReqAuthEmailRegister(string email, string password, string device_id, string secret_key, string udid, Network.ResponseCallback response)
    {
      this.name = "auth/email/register";
      this.body = "{";
      ReqAuthEmailRegister authEmailRegister1 = this;
      authEmailRegister1.body = authEmailRegister1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      ReqAuthEmailRegister authEmailRegister2 = this;
      authEmailRegister2.body = authEmailRegister2.body + "\"email\":\"" + email + "\",";
      ReqAuthEmailRegister authEmailRegister3 = this;
      authEmailRegister3.body = authEmailRegister3.body + "\"password\":\"" + password + "\",";
      this.body += "\"disable_validation_email\":true,";
      ReqAuthEmailRegister authEmailRegister4 = this;
      authEmailRegister4.body = authEmailRegister4.body + "\"device_id\":\"" + device_id + "\",";
      ReqAuthEmailRegister authEmailRegister5 = this;
      authEmailRegister5.body = authEmailRegister5.body + "\"secret_key\":\"" + secret_key + "\",";
      ReqAuthEmailRegister authEmailRegister6 = this;
      authEmailRegister6.body = authEmailRegister6.body + "\"udid\":\"" + udid + "\"";
      this.body += "}";
      this.callback = response;
    }
  }
}
