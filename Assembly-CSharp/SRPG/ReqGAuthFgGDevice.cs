// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGAuthFgGDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqGAuthFgGDevice : WebAPI
  {
    public ReqGAuthFgGDevice(string deviceID, string mail, string password, Network.ResponseCallback response)
    {
      this.name = "gauth/fggid/device";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"idfv\":\"");
      stringBuilder.Append(deviceID);
      stringBuilder.Append("\",\"email\":\"");
      stringBuilder.Append(mail);
      stringBuilder.Append("\",\"password\":\"");
      stringBuilder.Append(password);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public class Json_FggDevice
    {
      public string device_id;
      public string secret_key;
    }
  }
}
