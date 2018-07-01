// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraCondsUnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class TobiraCondsUnitParam
  {
    private string mId;
    private string mUnitIname;
    private int mLevel;
    private int mAwakeLevel;
    private string mJobIname;
    private int mJobLevel;
    private TobiraParam.Category mCategory;
    private int mTobiraLv;
    private TobiraCondsUnitParam.ConditionsDetail mConditionsDetail;

    public string Id
    {
      get
      {
        return this.mId;
      }
    }

    public string UnitIname
    {
      get
      {
        return this.mUnitIname;
      }
    }

    public int Level
    {
      get
      {
        return this.mLevel;
      }
    }

    public int AwakeLevel
    {
      get
      {
        return this.mAwakeLevel;
      }
    }

    public string JobIname
    {
      get
      {
        return this.mJobIname;
      }
    }

    public int JobLevel
    {
      get
      {
        return this.mJobLevel;
      }
    }

    public TobiraParam.Category TobiraCategory
    {
      get
      {
        return this.mCategory;
      }
    }

    public int TobiraLv
    {
      get
      {
        return this.mTobiraLv;
      }
    }

    public bool IsSelfUnit
    {
      get
      {
        return string.IsNullOrEmpty(this.mUnitIname);
      }
    }

    public void Deserialize(JSON_TobiraCondsUnitParam json)
    {
      if (json == null)
        return;
      this.mId = json.id;
      this.mUnitIname = json.unit_iname;
      this.mLevel = json.lv;
      this.mAwakeLevel = json.awake_lv;
      this.mJobIname = json.job_iname;
      this.mJobLevel = json.job_lv;
      this.mCategory = (TobiraParam.Category) json.category;
      this.mTobiraLv = json.tobira_lv;
      this.UpdateConditionsFlag();
    }

    private void UpdateConditionsFlag()
    {
      if (this.IsSelfUnit)
        this.mConditionsDetail |= TobiraCondsUnitParam.ConditionsDetail.IsSelf;
      if (this.Level > 0)
        this.mConditionsDetail |= TobiraCondsUnitParam.ConditionsDetail.IsUnitLv;
      if (!string.IsNullOrEmpty(this.JobIname) && this.mJobLevel > 0)
        this.mConditionsDetail |= TobiraCondsUnitParam.ConditionsDetail.IsJobLv;
      if (this.mAwakeLevel > 0)
        this.mConditionsDetail |= TobiraCondsUnitParam.ConditionsDetail.IsAwake;
      if (this.mTobiraLv <= 0)
        return;
      this.mConditionsDetail |= TobiraCondsUnitParam.ConditionsDetail.IsTobiraLv;
    }

    public bool HasFlag(TobiraCondsUnitParam.ConditionsDetail flag)
    {
      return (this.mConditionsDetail & flag) != (TobiraCondsUnitParam.ConditionsDetail) 0;
    }

    [Flags]
    public enum ConditionsDetail : long
    {
      IsSelf = 1,
      IsUnitLv = 2,
      IsAwake = 4,
      IsJobLv = 8,
      IsTobiraLv = 16, // 0x0000000000000010
    }
  }
}
