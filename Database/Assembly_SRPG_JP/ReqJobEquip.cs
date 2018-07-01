// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
