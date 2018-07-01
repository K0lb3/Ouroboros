// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqArtifactAdd : WebAPI
  {
    public ReqArtifactAdd(string iname, Network.ResponseCallback response, string trophyprog = null, string bingoprog = null)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append(iname);
      stringBuilder.Append('"');
      if (!string.IsNullOrEmpty(trophyprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(trophyprog);
      }
      if (!string.IsNullOrEmpty(bingoprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(bingoprog);
      }
      this.name = "unit/job/artifact/add";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
