// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobRankup
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
