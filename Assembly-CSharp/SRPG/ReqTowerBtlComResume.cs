// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerBtlComResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqTowerBtlComResume : WebAPI
  {
    public ReqTowerBtlComResume(long btlid, Network.ResponseCallback response)
    {
      this.name = "tower/btl/resume";
      this.body = "\"btlid\":" + (object) btlid;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
