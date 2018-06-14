// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class AbilityParam
  {
    private string localizedNameID;
    private string localizedExprID;
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
    public string[] condition_jobs;
    public string condition_birth;
    public ESex condition_sex;
    public EElement condition_element;
    public OInt condition_raremin;
    public OInt condition_raremax;

    protected void localizeFields(string language)
    {
      this.init();
      this.name = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedNameID);
      this.expr = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedExprID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.iname, "NAME");
      this.localizedExprID = this.GetType().GenerateLocalizedID(this.iname, "EXPR");
    }

    public void Deserialize(string language, JSON_AbilityParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

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
      string[] strArray = new string[10]{ json.skl1, json.skl2, json.skl3, json.skl4, json.skl5, json.skl6, json.skl7, json.skl8, json.skl9, json.skl10 };
      for (int index = 0; index < strArray.Length && !string.IsNullOrEmpty(strArray[index]); ++index)
        ++length;
      if (length > 0)
      {
        int[] numArray = new int[10]{ json.lv1, json.lv2, json.lv3, json.lv4, json.lv5, json.lv6, json.lv7, json.lv8, json.lv9, json.lv10 };
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
      this.condition_jobs = (string[]) null;
      if (json.jobs != null && json.jobs.Length > 0)
      {
        this.condition_jobs = new string[json.jobs.Length];
        for (int index = 0; index < json.jobs.Length; ++index)
          this.condition_jobs[index] = json.jobs[index];
      }
      this.condition_birth = json.birth;
      this.condition_sex = (ESex) json.sex;
      this.condition_element = (EElement) json.elem;
      this.condition_raremin = (OInt) json.rmin;
      this.condition_raremax = (OInt) json.rmax;
      return true;
    }

    public int GetRankCap()
    {
      return (int) this.lvcap;
    }

    public bool CheckEnableUseAbility(UnitData self, int job_index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AbilityParam.\u003CCheckEnableUseAbility\u003Ec__AnonStorey17D abilityCAnonStorey17D = new AbilityParam.\u003CCheckEnableUseAbility\u003Ec__AnonStorey17D();
      // ISSUE: reference to a compiler-generated field
      abilityCAnonStorey17D.self = self;
      // ISSUE: reference to a compiler-generated method
      if (this.condition_units != null && Array.Find<string>(this.condition_units, new Predicate<string>(abilityCAnonStorey17D.\u003C\u003Em__118)) == null)
        return false;
      if (this.condition_jobs != null)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        AbilityParam.\u003CCheckEnableUseAbility\u003Ec__AnonStorey17E abilityCAnonStorey17E = new AbilityParam.\u003CCheckEnableUseAbility\u003Ec__AnonStorey17E();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        abilityCAnonStorey17E.job = abilityCAnonStorey17D.self.GetJobData(job_index);
        // ISSUE: reference to a compiler-generated field
        if (abilityCAnonStorey17E.job == null)
          return false;
        // ISSUE: reference to a compiler-generated method
        if (Array.Find<string>(this.condition_jobs, new Predicate<string>(abilityCAnonStorey17E.\u003C\u003Em__119)) == null)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          AbilityParam.\u003CCheckEnableUseAbility\u003Ec__AnonStorey17F abilityCAnonStorey17F = new AbilityParam.\u003CCheckEnableUseAbility\u003Ec__AnonStorey17F();
          // ISSUE: reference to a compiler-generated field
          if (string.IsNullOrEmpty(abilityCAnonStorey17E.job.Param.origin))
            return false;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          abilityCAnonStorey17F.originJobParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(abilityCAnonStorey17E.job.Param.origin);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          if (abilityCAnonStorey17F.originJobParam == null || Array.Find<string>(this.condition_jobs, new Predicate<string>(abilityCAnonStorey17F.\u003C\u003Em__11A)) == null)
            return false;
        }
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return (string.IsNullOrEmpty(this.condition_birth) || !((string) abilityCAnonStorey17D.self.UnitParam.birth != this.condition_birth)) && (this.condition_sex == ESex.Unknown || abilityCAnonStorey17D.self.UnitParam.sex == this.condition_sex) && (this.condition_element == EElement.None || abilityCAnonStorey17D.self.Element == this.condition_element) && ((int) this.condition_raremax == 0 || (int) this.condition_raremin <= abilityCAnonStorey17D.self.Rarity && (int) this.condition_raremax >= abilityCAnonStorey17D.self.Rarity && (int) this.condition_raremax >= (int) this.condition_raremin);
    }
  }
}
