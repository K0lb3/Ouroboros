// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobClassChange
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/UnitJobClassChange")]
  public class UnitJobClassChange : MonoBehaviour
  {
    public string PrevJobID;
    public string NextJobID;
    public GameObject PrevJob;
    public GameObject NextJob;
    [NonSerialized]
    public int CurrentRank;
    public Text NewRank;
    public Text OldRank;

    public UnitJobClassChange()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      JobParam data1 = (JobParam) null;
      JobParam data2 = (JobParam) null;
      if (string.IsNullOrEmpty(this.PrevJobID) && string.IsNullOrEmpty(this.NextJobID))
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
        if (unitDataByUniqueId == null)
          return;
        JobData jobData = (JobData) null;
        for (int index = 0; index < unitDataByUniqueId.Jobs.Length; ++index)
        {
          if (unitDataByUniqueId.Jobs[index] != null && unitDataByUniqueId.Jobs[index].UniqueID == (long) GlobalVars.SelectedJobUniqueID)
            jobData = unitDataByUniqueId.Jobs[index];
        }
        if (jobData == null)
          return;
        JobData baseJob = unitDataByUniqueId.GetBaseJob(jobData.JobID);
        if (baseJob != null)
          data1 = baseJob.Param;
        data2 = jobData.Param;
      }
      if (!string.IsNullOrEmpty(this.PrevJobID))
        data1 = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(this.PrevJobID);
      if (!string.IsNullOrEmpty(this.NextJobID))
        data2 = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(this.NextJobID);
      if (Object.op_Inequality((Object) this.NextJob, (Object) null))
      {
        DataSource.Bind<JobParam>(this.NextJob, data2);
        GameParameter.UpdateAll(this.NextJob);
      }
      if (Object.op_Inequality((Object) this.PrevJob, (Object) null))
      {
        DataSource.Bind<JobParam>(this.PrevJob, data1);
        GameParameter.UpdateAll(this.PrevJob);
      }
      if (Object.op_Inequality((Object) this.NewRank, (Object) null))
        this.NewRank.set_text(this.CurrentRank.ToString());
      if (!Object.op_Inequality((Object) this.OldRank, (Object) null))
        return;
      this.OldRank.set_text((this.CurrentRank - 1).ToString());
    }
  }
}
