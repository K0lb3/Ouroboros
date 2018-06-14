// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqArtifactSet : WebAPI
  {
    public ReqArtifactSet(long iid_job, long iid_artifact, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid_job\":");
      stringBuilder.Append(iid_job);
      stringBuilder.Append(",\"iid_artifact\":");
      stringBuilder.Append(iid_artifact);
      this.name = "unit/job/artifact/set";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public ReqArtifactSet(long iid_job, long[] iid_artifact, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid_job\":");
      stringBuilder.Append(iid_job);
      stringBuilder.Append(",\"iid_artifact\":");
      stringBuilder.Append(iid_artifact[0]);
      stringBuilder.Append(",\"iid_artifacts\":[");
      for (int index = 0; index < iid_artifact.Length; ++index)
      {
        if (index > 0)
          stringBuilder.Append(',');
        stringBuilder.Append(iid_artifact[index]);
      }
      stringBuilder.Append(']');
      this.name = "unit/job/artifact/set";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public ReqArtifactSet(long iid_unit, long iid_job, long[] iid_artifact, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid_unit\":");
      stringBuilder.Append(iid_unit);
      stringBuilder.Append(",\"iid_job\":");
      stringBuilder.Append(iid_job);
      stringBuilder.Append(",\"iid_artifacts\":[");
      for (int index = 0; index < iid_artifact.Length; ++index)
      {
        if (index > 0)
          stringBuilder.Append(',');
        stringBuilder.Append(iid_artifact[index]);
      }
      stringBuilder.Append(']');
      this.name = "unit/job/artifact/set";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
