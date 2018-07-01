// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_JobLv
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ConditionsResult_JobLv : ConditionsResult_Unit
  {
    public string mCondsJobIname;
    public int mCondsJobLv;
    public JobData mJobData;
    public JobParam mJobParam;

    public ConditionsResult_JobLv(UnitData unitData, UnitParam unitParam, string condsJobIname, int condsJobLv)
      : base(unitData, unitParam)
    {
      this.mCondsJobLv = condsJobLv;
      this.mCondsJobIname = condsJobIname;
      this.mTargetValue = condsJobLv;
      JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(condsJobIname);
      if (jobParam != null)
        this.mJobParam = jobParam;
      if (unitData != null)
      {
        foreach (JobData job in unitData.Jobs)
        {
          if (job.Param.iname == this.mCondsJobIname)
          {
            this.mJobData = job;
            this.mIsClear = job.Rank >= this.mCondsJobLv;
            this.mCurrentValue = job.Rank;
            break;
          }
        }
      }
      else
        this.mIsClear = false;
    }

    public override string text
    {
      get
      {
        return LocalizedText.Get("sys.TOBIRA_CONDITIONS_JOB_LEVEL", (object) this.unitName, (object) this.mJobParam.name, (object) this.mCondsJobLv);
      }
    }

    public override string errorText
    {
      get
      {
        if (this.mJobData != null)
          return string.Format("ユニット「{0}」を所持していません", (object) this.unitName);
        return string.Format("ユニット「{0}」のジョブ「{1}」が解放されていません", (object) this.unitName, (object) this.mJobParam.name);
      }
    }
  }
}
