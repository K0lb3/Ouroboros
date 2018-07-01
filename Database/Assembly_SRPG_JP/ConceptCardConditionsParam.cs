// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardConditionsParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class ConceptCardConditionsParam
  {
    public string iname;
    public string unit_group;
    public EUseConditionsType units_conditions_type;
    public string job_group;
    public EUseConditionsType jobs_conditions_type;
    public ESex sex;
    public int[] birth_id;
    private Dictionary<EElement, int> conditions_elements;
    private int element_sum;

    public bool Deserialize(JSON_ConceptCardConditionsParam json)
    {
      this.iname = json.iname;
      this.unit_group = json.un_group;
      this.units_conditions_type = (EUseConditionsType) json.units_cnds_type;
      this.job_group = json.job_group;
      this.jobs_conditions_type = (EUseConditionsType) json.jobs_cnds_type;
      this.sex = (ESex) json.sex;
      if (json.birth_id != null)
      {
        this.birth_id = new int[json.birth_id.Length];
        for (int index = 0; index < this.birth_id.Length; ++index)
          this.birth_id[index] = json.birth_id[index];
      }
      this.conditions_elements = new Dictionary<EElement, int>();
      this.conditions_elements.Add(EElement.Fire, json.el_fire);
      this.conditions_elements.Add(EElement.Water, json.el_watr);
      this.conditions_elements.Add(EElement.Wind, json.el_wind);
      this.conditions_elements.Add(EElement.Thunder, json.el_thdr);
      this.conditions_elements.Add(EElement.Shine, json.el_lit);
      this.conditions_elements.Add(EElement.Dark, json.el_drk);
      this.element_sum = json.el_fire + json.el_watr + json.el_wind + json.el_thdr + json.el_lit + json.el_drk;
      return true;
    }

    public bool IsMatchElement(EElement element)
    {
      if (this.element_sum <= 0)
        return true;
      return this.conditions_elements[element] > 0;
    }

    public bool IsMatchBirth(int unit_birth)
    {
      return this.birth_id == null || this.birth_id.Length <= 0 || Array.IndexOf<int>(this.birth_id, unit_birth) != -1;
    }

    public bool IsMatchUnitGroup(string unit_iname)
    {
      if (string.IsNullOrEmpty(this.unit_group))
        return true;
      UnitGroupParam unitGroup = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitGroup(this.unit_group);
      if (unitGroup == null)
        return true;
      bool flag = unitGroup.IsInGroup(unit_iname);
      if (this.units_conditions_type == EUseConditionsType.NotMatch)
        return !flag;
      return flag;
    }

    public bool IsMatchJobGroup(string job_iname)
    {
      if (string.IsNullOrEmpty(this.job_group))
        return true;
      JobGroupParam jobGroup = MonoSingleton<GameManager>.Instance.MasterParam.GetJobGroup(this.job_group);
      if (jobGroup == null)
        return true;
      bool flag = jobGroup.IsInGroup(job_iname);
      if (this.jobs_conditions_type == EUseConditionsType.NotMatch)
        return !flag;
      return flag;
    }

    public string GetConditionDescriptionEquip()
    {
      return this.GetConditionDescription() + LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_EQUIP");
    }

    public string GetConditionDescription()
    {
      List<string> stringList = new List<string>();
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      if (this.birth_id != null && this.birth_id.Length > 0)
      {
        stringList.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_BIRTH"));
        bool flag = false;
        for (int index = 0; index < this.birth_id.Length; ++index)
        {
          if (flag)
            stringList.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OR"));
          string birthplaceName = UnitParam.GetBirthplaceName(this.birth_id[index]);
          stringList.Add(birthplaceName);
          flag = true;
        }
        stringList.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
      }
      UnitGroupParam unitGroup = masterParam.GetUnitGroup(this.unit_group);
      if (unitGroup != null)
      {
        stringList.Add(unitGroup.GetName());
        stringList.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
      }
      List<EElement> eelementList = new List<EElement>((IEnumerable<EElement>) this.conditions_elements.Keys);
      if (this.element_sum > 0)
      {
        bool flag = false;
        for (int index = 0; index < eelementList.Count; ++index)
        {
          if (this.conditions_elements[eelementList[index]] == 1)
          {
            if (flag)
              stringList.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OR"));
            EElement eelement = eelementList[index];
            stringList.Add(LocalizedText.Get("sys.UNIT_ELEMENT_" + (object) eelement));
            stringList.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_ELEMENT"));
            flag = true;
          }
        }
        stringList.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
      }
      if (this.sex != ESex.Unknown)
      {
        stringList.Add(LocalizedText.Get("sys.SEX_" + (object) this.sex));
        stringList.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
      }
      JobGroupParam jobGroup = masterParam.GetJobGroup(this.job_group);
      if (jobGroup != null)
      {
        bool flag = false;
        for (int index = 0; index < jobGroup.jobs.Length; ++index)
        {
          if (flag)
            stringList.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OR"));
          string job = jobGroup.jobs[index];
          JobParam jobParam = masterParam.GetJobParam(job);
          stringList.Add(jobParam.name);
          flag = true;
        }
        stringList.Add(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
      }
      stringList.RemoveAt(stringList.Count - 1);
      return string.Join(string.Empty, stringList.ToArray());
    }

    public UnitGroupParam GetUnitGroupParam()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.GetUnitGroup(this.unit_group);
    }

    public JobGroupParam GetJobGroupParam()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.GetJobGroup(this.job_group);
    }
  }
}
