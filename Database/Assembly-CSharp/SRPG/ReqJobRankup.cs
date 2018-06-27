// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobRankup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqJobRankup : WebAPI
  {
    public ReqJobRankup(long iid_job, Network.ResponseCallback response)
    {
      this.name = "unit/job/equip/lvup/";
      this.body = WebAPI.GetRequestString("\"iid\":" + (object) iid_job);
      this.callback = response;
    }
  }
}
