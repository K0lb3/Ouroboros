// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class AbilityParam
  {
    public string iname;
    public string name;
    public string expr;
    public string icon;
    public EAbilityType type;
    public EAbilitySlot slot;
    public OInt lvcap;
    public bool is_fixed;
    public LearningSkill[] skills;
    public string[] condition_units;
    public EUseConditionsType units_conditions_type;
    public string[] condition_jobs;
    public EUseConditionsType jobs_conditions_type;
    public string condition_birth;
    public ESex condition_sex;
    public EElement condition_element;
    public OInt condition_raremin;
    public OInt condition_raremax;
    public EAbilityTypeDetail type_detail;

    public bool Deserialize(JSON_AbilityParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.icon = json.icon;
      this.type = (EAbilityType) json.type;
      this.slot = (EAbilitySlot) json.slot;
      this.lvcap = (OInt) Math.Max(json.cap, 1);
      this.is_fixed = json.fix != 0;
      int length = 0;
      string[] strArray = new string[10]
      {
        json.skl1,
        json.skl2,
        json.skl3,
        json.skl4,
        json.skl5,
        json.skl6,
        json.skl7,
        json.skl8,
        json.skl9,
        json.skl10
      };
      for (int index = 0; index < strArray.Length && !string.IsNullOrEmpty(strArray[index]); ++index)
        ++length;
      if (length > 0)
      {
        int[] numArray = new int[10]
        {
          json.lv1,
          json.lv2,
          json.lv3,
          json.lv4,
          json.lv5,
          json.lv6,
          json.lv7,
          json.lv8,
          json.lv9,
          json.lv10
        };
        this.skills = new LearningSkill[length];
        for (int index = 0; index < length; ++index)
        {
          this.skills[index] = new LearningSkill();
          this.skills[index].iname = strArray[index];
          this.skills[index].locklv = numArray[index];
        }
      }
      this.condition_units = (string[]) null;
      if (json.units != null && json.units.Length > 0)
      {
        this.condition_units = new string[json.units.Length];
        for (int index = 0; index < json.units.Length; ++index)
          this.condition_units[index] = json.units[index];
      }
      this.units_conditions_type = (EUseConditionsType) json.units_cnds_type;
      this.condition_jobs = (string[]) null;
      if (json.jobs != null && json.jobs.Length > 0)
      {
        this.condition_jobs = new string[json.jobs.Length];
        for (int index = 0; index < json.jobs.Length; ++index)
          this.condition_jobs[index] = json.jobs[index];
      }
      this.jobs_conditions_type = (EUseConditionsType) json.jobs_cnds_type;
      this.condition_birth = json.birth;
      this.condition_sex = (ESex) json.sex;
      this.condition_element = (EElement) json.elem;
      this.condition_raremin = (OInt) json.rmin;
      this.condition_raremax = (OInt) json.rmax;
      this.type_detail = (EAbilityTypeDetail) json.type_detail;
      return true;
    }

    public int GetRankCap()
    {
      return (int) this.lvcap;
    }

    public bool CheckEnableUseAbility(UnitData self, int job_index)
    {
      if (this.condition_units != null)
      {
        bool flag = Array.Find<string>(this.condition_units, (Predicate<string>) (p => p == self.UnitParam.iname)) != null;
        if (!(this.units_conditions_type != EUseConditionsType.Match ? !flag : flag))
          return false;
      }
      if (this.condition_jobs != null)
      {
        JobData job = self.GetJobData(job_index);
        if (job == null)
          return false;
        bool flag = Array.Find<string>(this.condition_jobs, (Predicate<string>) (p => p == job.JobID)) != null;
        if (!flag && !string.IsNullOrEmpty(job.Param.origin))
        {
          JobParam originJobParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(job.Param.origin);
          if (originJobParam != null)
            flag = Array.Find<string>(this.condition_jobs, (Predicate<string>) (p => p == originJobParam.iname)) != null;
        }
        if (!(this.jobs_conditions_type != EUseConditionsType.Match ? !flag : flag))
          return false;
      }
      return (string.IsNullOrEmpty(this.condition_birth) || !((string) self.UnitParam.birth != this.condition_birth)) && (this.condition_sex == ESex.Unknown || self.UnitParam.sex == this.condition_sex) && (this.condition_element == EElement.None || self.Element == this.condition_element) && ((int) this.condition_raremax == 0 || (int) this.condition_raremin <= self.Rarity && (int) this.condition_raremax >= self.Rarity && (int) this.condition_raremax >= (int) this.condition_raremin);
    }

    public List<UnitParam> FindConditionUnitParams(MasterParam masterParam = null)
    {
      List<UnitParam> unitParamList = new List<UnitParam>();
      if (this.condition_units != null)
      {
        for (int index = 0; index < this.condition_units.Length; ++index)
        {
          if (!string.IsNullOrEmpty(this.condition_units[index]))
          {
            UnitParam unitParam = (UnitParam) null;
            string conditionUnit = this.condition_units[index];
            if (masterParam == null)
            {
              GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
                unitParam = instanceDirect.GetUnitParam(conditionUnit);
            }
            else
              unitParam = masterParam.GetUnitParam(conditionUnit);
            if (unitParam != null)
              unitParamList.Add(unitParam);
          }
        }
      }
      return unitParamList;
    }

    public List<JobParam> FindConditionJobParams(MasterParam masterParam = null)
    {
      List<JobParam> jobParamList = new List<JobParam>();
      if (this.condition_jobs != null)
      {
        for (int index = 0; index < this.condition_jobs.Length; ++index)
        {
          if (!string.IsNullOrEmpty(this.condition_jobs[index]))
          {
            JobParam jobParam = (JobParam) null;
            string conditionJob = this.condition_jobs[index];
            if (masterParam == null)
            {
              GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
                jobParam = instanceDirect.GetJobParam(conditionJob);
            }
            else
              jobParam = masterParam.GetJobParam(conditionJob);
            if (jobParam != null)
              jobParamList.Add(jobParam);
          }
        }
      }
      return jobParamList;
    }

    public static string TypeDetailToSpriteSheetKey(EAbilityTypeDetail typeDetail)
    {
      string str = string.Empty;
      switch (typeDetail)
      {
        case EAbilityTypeDetail.Job_Unique:
        case EAbilityTypeDetail.Job_Basic:
        case EAbilityTypeDetail.Job_Support:
        case EAbilityTypeDetail.Job_Reaction:
          str = "ABILITY_TITLE_NORMAL";
          break;
        case EAbilityTypeDetail.MasterAbility:
          str = "ABILITY_TITLE_MASTER";
          break;
        case EAbilityTypeDetail.WeaponAbility:
          str = "ABILITY_TITLE_WEAPON";
          break;
        case EAbilityTypeDetail.VisionAbility:
          str = "ABILITY_TITLE_VISION";
          break;
      }
      return str;
    }
  }
}
