// Decompiled with JetBrains decompiler
// Type: SRPG.SkillAbilityDeriveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  public class SkillAbilityDeriveData
  {
    public HashSet<SkillAbilityDeriveParam> m_AdditionalSkillAbilityDeriveParam = new HashSet<SkillAbilityDeriveParam>();
    public SkillAbilityDeriveParam m_SkillAbilityDeriveParam;
    public List<SkillDeriveData> m_SkillDeriveData;
    public List<AbilityDeriveData> m_AbilityDeriveData;

    public int MasterIndex
    {
      get
      {
        return this.m_SkillAbilityDeriveParam.m_OriginIndex;
      }
    }

    public bool CheckContainsTriggerIname(ESkillAbilityDeriveConds triggerType, string triggerIname)
    {
      return this.m_SkillAbilityDeriveParam.CheckContainsTriggerIname(triggerType, triggerIname);
    }

    public bool CheckContainsTriggerInames(SkillAbilityDeriveTriggerParam[] searchKeyTriggerParam)
    {
      return this.m_SkillAbilityDeriveParam.CheckContainsTriggerInames(searchKeyTriggerParam);
    }

    public void Setup(SkillAbilityDeriveParam skillAbilityDeriveParam, List<SkillAbilityDeriveParam> additionalSkillAbilityDeriveParams)
    {
      this.m_SkillDeriveData = new List<SkillDeriveData>();
      this.m_AbilityDeriveData = new List<AbilityDeriveData>();
      this.m_SkillAbilityDeriveParam = skillAbilityDeriveParam;
      IEnumerable<SkillDeriveData> collection1 = this.m_SkillAbilityDeriveParam.SkillDeriveParams.Select<SkillDeriveParam, SkillDeriveData>((Func<SkillDeriveParam, SkillDeriveData>) (param => new SkillDeriveData(param)));
      IEnumerable<AbilityDeriveData> collection2 = this.m_SkillAbilityDeriveParam.AbilityDeriveParams.Select<AbilityDeriveParam, AbilityDeriveData>((Func<AbilityDeriveParam, AbilityDeriveData>) (param => new AbilityDeriveData(param)));
      this.m_SkillDeriveData.AddRange(collection1);
      this.m_AbilityDeriveData.AddRange(collection2);
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      using (List<SkillDeriveData>.Enumerator enumerator = this.m_SkillDeriveData.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillDeriveData current = enumerator.Current;
          if (!dictionary.ContainsKey(current.Param.BaseSkillIname))
            dictionary.Add(current.Param.BaseSkillIname, this.MasterIndex);
        }
      }
      using (List<AbilityDeriveData>.Enumerator enumerator = this.m_AbilityDeriveData.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          AbilityDeriveData current = enumerator.Current;
          if (!dictionary.ContainsKey(current.Param.BaseAbilityIname))
            dictionary.Add(current.Param.BaseAbilityIname, this.MasterIndex);
        }
      }
      using (List<SkillAbilityDeriveParam>.Enumerator enumerator1 = additionalSkillAbilityDeriveParams.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          SkillAbilityDeriveParam current1 = enumerator1.Current;
          if (!this.m_AdditionalSkillAbilityDeriveParam.Contains(current1))
            this.m_AdditionalSkillAbilityDeriveParam.Add(current1);
          List<SkillDeriveParam> skillDeriveParams = current1.SkillDeriveParams;
          List<AbilityDeriveParam> abilityDeriveParams = current1.AbilityDeriveParams;
          using (List<SkillDeriveParam>.Enumerator enumerator2 = skillDeriveParams.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              SkillDeriveParam current2 = enumerator2.Current;
              int num = -1;
              if (dictionary.TryGetValue(current2.BaseSkillIname, out num))
              {
                num = Mathf.Min(current2.MasterIndex, num);
                dictionary[current2.BaseSkillIname] = num;
              }
              else
                dictionary.Add(current2.BaseSkillIname, current2.MasterIndex);
              this.m_SkillDeriveData.Add(new SkillDeriveData(current2)
              {
                IsAdd = true
              });
            }
          }
          using (List<AbilityDeriveParam>.Enumerator enumerator2 = abilityDeriveParams.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              AbilityDeriveParam current2 = enumerator2.Current;
              int num = -1;
              if (dictionary.TryGetValue(current2.BaseAbilityIname, out num))
              {
                num = Mathf.Min(current2.MasterIndex, num);
                dictionary[current2.BaseAbilityIname] = num;
              }
              else
                dictionary.Add(current2.BaseAbilityIname, current2.MasterIndex);
              this.m_AbilityDeriveData.Add(new AbilityDeriveData(current2)
              {
                IsAdd = true
              });
            }
          }
        }
      }
      using (List<AbilityDeriveData>.Enumerator enumerator = this.m_AbilityDeriveData.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          AbilityDeriveData current = enumerator.Current;
          int num = -1;
          if (dictionary.TryGetValue(current.Param.BaseAbilityIname, out num))
            current.IsDisable = current.Param.MasterIndex > num;
        }
      }
      using (List<SkillDeriveData>.Enumerator enumerator = this.m_SkillDeriveData.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillDeriveData current = enumerator.Current;
          int num = -1;
          if (dictionary.TryGetValue(current.Param.BaseSkillIname, out num))
            current.IsDisable = current.Param.MasterIndex > num;
        }
      }
    }

    public List<SkillDeriveParam> GetAvailableSkillDeriveParams()
    {
      return this.m_SkillDeriveData.Where<SkillDeriveData>((Func<SkillDeriveData, bool>) (deriveData => !deriveData.IsDisable)).Select<SkillDeriveData, SkillDeriveParam>((Func<SkillDeriveData, SkillDeriveParam>) (deriveData => deriveData.Param)).ToList<SkillDeriveParam>();
    }

    public List<AbilityDeriveParam> GetAvailableAbilityDeriveParams()
    {
      return this.m_AbilityDeriveData.Where<AbilityDeriveData>((Func<AbilityDeriveData, bool>) (deriveData => !deriveData.IsDisable)).Select<AbilityDeriveData, AbilityDeriveParam>((Func<AbilityDeriveData, AbilityDeriveParam>) (deriveData => deriveData.Param)).ToList<AbilityDeriveParam>();
    }
  }
}
