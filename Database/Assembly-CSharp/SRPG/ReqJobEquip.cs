// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqJobEquip : WebAPI
  {
    public ReqJobEquip(long iid_job, long id_equip, Network.ResponseCallback response)
    {
      this.name = "unit/job/equip/set";
      this.body = "\"iid\":" + (object) iid_job + ",";
      ReqJobEquip reqJobEquip = this;
      reqJobEquip.body = reqJobEquip.body + "\"id_equip\":" + (object) id_equip;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
