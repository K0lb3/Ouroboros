// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGAuthMigrate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqGAuthMigrate : WebAPI
  {
    public ReqGAuthMigrate(string secretKey, string deviceID, string passcode, Network.ResponseCallback response)
    {
      this.name = "gauth/migrate";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"secret_key\":\"");
      stringBuilder.Append(secretKey);
      stringBuilder.Append("\",\"device_id\":\"");
      stringBuilder.Append(deviceID);
      stringBuilder.Append("\",\"passcode\":\"");
      stringBuilder.Append(passcode);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
