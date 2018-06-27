// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAlterCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqAlterCheck : WebAPI
  {
    public ReqAlterCheck(string hash, Network.ResponseCallback response)
    {
      this.name = "master/md5";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"md5\":\"");
      stringBuilder.Append(hash);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
