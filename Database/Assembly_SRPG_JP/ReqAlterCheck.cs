// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAlterCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
