// Decompiled with JetBrains decompiler
// Type: SRPG.ReqLogin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  public class ReqLogin : WebAPI
  {
    public ReqLogin(Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"device\":\"");
      stringBuilder.Append(SystemInfo.get_deviceModel());
      stringBuilder.Append("\",");
      string str = AssetManager.Format.ToPath().Replace("/", string.Empty);
      stringBuilder.Append("\"dlc\":\"");
      stringBuilder.Append(str);
      stringBuilder.Append("\"");
      this.name = "login";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
