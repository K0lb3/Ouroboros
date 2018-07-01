// Decompiled with JetBrains decompiler
// Type: SRPG.JobSetParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class JobSetParam
  {
    public string iname;
    public string job;
    public int lock_rarity;
    public int lock_awakelv;
    public JobSetParam.JobLock[] lock_jobs;
    public string jobchange;
    public string target_unit;

    public bool Deserialize(JSON_JobSetParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.job = json.job;
      this.jobchange = json.cjob;
      this.target_unit = json.target_unit;
      this.lock_rarity = json.lrare;
      this.lock_awakelv = json.lplus;
      this.lock_jobs = (JobSetParam.JobLock[]) null;
      int length = 0;
      if (!string.IsNullOrEmpty(json.ljob1))
        ++length;
      if (!string.IsNullOrEmpty(json.ljob2))
        ++length;
      if (!string.IsNullOrEmpty(json.ljob3))
        ++length;
      if (length > 0)
      {
        this.lock_jobs = new JobSetParam.JobLock[length];
        int index = 0;
        if (!string.IsNullOrEmpty(json.ljob1))
        {
          this.lock_jobs[index] = new JobSetParam.JobLock();
          this.lock_jobs[index].iname = json.ljob1;
          this.lock_jobs[index].lv = json.llv1;
          ++index;
        }
        if (!string.IsNullOrEmpty(json.ljob2))
        {
          this.lock_jobs[index] = new JobSetParam.JobLock();
          this.lock_jobs[index].iname = json.ljob2;
          this.lock_jobs[index].lv = json.llv2;
          ++index;
        }
        if (!string.IsNullOrEmpty(json.ljob3))
        {
          this.lock_jobs[index] = new JobSetParam.JobLock();
          this.lock_jobs[index].iname = json.ljob3;
          this.lock_jobs[index].lv = json.llv3;
          int num = index + 1;
        }
      }
      return true;
    }

    public bool ContainsJob(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return false;
      if (iname == this.job)
        return true;
      if (this.jobchange == null)
        return false;
      if (iname == this.jobchange)
        return true;
      return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetJobSetParam(this.jobchange).ContainsJob(iname);
    }

    public class JobLock
    {
      public string iname;
      public int lv;
    }
  }
}
