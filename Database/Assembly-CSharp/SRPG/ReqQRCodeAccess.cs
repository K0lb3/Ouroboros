// Decompiled with JetBrains decompiler
// Type: SRPG.ReqQRCodeAccess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqQRCodeAccess : WebAPI
  {
    public ReqQRCodeAccess(int campaign, string serial, Network.ResponseCallback response)
    {
      this.name = "qr/serial";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"campaign_id\":");
      stringBuilder.Append(campaign);
      stringBuilder.Append(",");
      stringBuilder.Append("\"code\":\"");
      stringBuilder.Append(serial);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
