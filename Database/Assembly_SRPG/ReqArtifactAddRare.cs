// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactAddRare
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqArtifactAddRare : WebAPI
  {
    public ReqArtifactAddRare(long iid, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\":");
      stringBuilder.Append(iid);
      this.name = "unit/job/artifact/rare/add";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
