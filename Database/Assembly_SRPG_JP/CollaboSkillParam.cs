// Decompiled with JetBrains decompiler
// Type: SRPG.CollaboSkillParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class CollaboSkillParam
  {
    private List<CollaboSkillParam.LearnSkill> mLearnSkillLists = new List<CollaboSkillParam.LearnSkill>();
    private string mIname;
    private string mUnitIname;
    private string mAbilityIname;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public string UnitIname
    {
      get
      {
        return this.mUnitIname;
      }
    }

    public string AbilityIname
    {
      get
      {
        return this.mAbilityIname;
      }
    }

    public List<CollaboSkillParam.LearnSkill> LearnSkillLists
    {
      get
      {
        return this.mLearnSkillLists;
      }
    }

    public void Deserialize(JSON_CollaboSkillParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mUnitIname = json.uname;
      this.mAbilityIname = json.abid;
      this.mLearnSkillLists.Clear();
      if (json.lqs == null)
        return;
      foreach (string lq in json.lqs)
        this.mLearnSkillLists.Add(new CollaboSkillParam.LearnSkill(lq));
    }

    public static void UpdateCollaboSkill(List<CollaboSkillParam> csp_lists)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return;
      using (List<CollaboSkillParam>.Enumerator enumerator1 = csp_lists.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          CollaboSkillParam current1 = enumerator1.Current;
          for (int index = 0; index < current1.mLearnSkillLists.Count; ++index)
          {
            CollaboSkillParam.LearnSkill ls = current1.mLearnSkillLists[index];
            if (!string.IsNullOrEmpty(ls.QuestIname))
            {
              AbilityParam abilityParam1 = instanceDirect.MasterParam.GetAbilityParam(current1.AbilityIname);
              if (abilityParam1 == null)
                DebugUtility.LogError(string.Format("CollaboSkillParam/Deserialize AbilityParam not found. ability_iname={0}", (object) current1.mAbilityIname));
              else if (index < abilityParam1.skills.Length)
              {
                ls.SkillIname = abilityParam1.skills[index].iname;
                using (List<CollaboSkillParam>.Enumerator enumerator2 = csp_lists.GetEnumerator())
                {
                  while (enumerator2.MoveNext())
                  {
                    CollaboSkillParam current2 = enumerator2.Current;
                    if (!(current2.mUnitIname == current1.mUnitIname))
                    {
                      AbilityParam abilityParam2 = instanceDirect.MasterParam.GetAbilityParam(current2.AbilityIname);
                      if (abilityParam2 != null && new List<LearningSkill>((IEnumerable<LearningSkill>) abilityParam2.skills).Find((Predicate<LearningSkill>) (flgs => flgs.iname == ls.SkillIname)) != null)
                      {
                        ls.PartnerUnitIname = current2.UnitIname;
                        break;
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    public static string GetPartnerIname(string unit_iname, string skill_iname)
    {
      if (string.IsNullOrEmpty(unit_iname) || string.IsNullOrEmpty(skill_iname))
        return (string) null;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return (string) null;
      CollaboSkillParam collaboSkillParam = instanceDirect.MasterParam.CollaboSkills.Find((Predicate<CollaboSkillParam>) (fcs => fcs.UnitIname == unit_iname));
      if (collaboSkillParam == null)
      {
        DebugUtility.LogError(string.Format("CollaboSkillParam/GetPartnerIname CollaboSkillParam not found. unit_iname={0}", (object) unit_iname));
        return (string) null;
      }
      CollaboSkillParam.LearnSkill learnSkill = collaboSkillParam.mLearnSkillLists.Find((Predicate<CollaboSkillParam.LearnSkill>) (fls => fls.SkillIname == skill_iname));
      if (learnSkill != null)
        return learnSkill.PartnerUnitIname;
      DebugUtility.LogError(string.Format("CollaboSkillParam/GetPartnerIname LearnSkill not found. skill_iname={0}", (object) skill_iname));
      return (string) null;
    }

    public static List<string> GetLearnSkill(string quest_iname, string unit_iname)
    {
      List<string> stringList = new List<string>();
      if (string.IsNullOrEmpty(quest_iname) || string.IsNullOrEmpty(unit_iname))
        return stringList;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return stringList;
      CollaboSkillParam collaboSkillParam = instanceDirect.MasterParam.CollaboSkills.Find((Predicate<CollaboSkillParam>) (fcs => fcs.UnitIname == unit_iname));
      if (collaboSkillParam == null)
        return stringList;
      using (List<CollaboSkillParam.LearnSkill>.Enumerator enumerator = collaboSkillParam.mLearnSkillLists.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          CollaboSkillParam.LearnSkill current = enumerator.Current;
          if (!(current.QuestIname != quest_iname))
            stringList.Add(current.SkillIname);
        }
      }
      return stringList;
    }

    public static List<CollaboSkillParam.Pair> GetPairLists()
    {
      List<CollaboSkillParam.Pair> pairList = new List<CollaboSkillParam.Pair>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return pairList;
      using (List<CollaboSkillParam>.Enumerator enumerator1 = instanceDirect.MasterParam.CollaboSkills.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          CollaboSkillParam csp = enumerator1.Current;
          using (List<CollaboSkillParam.LearnSkill>.Enumerator enumerator2 = csp.mLearnSkillLists.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              CollaboSkillParam.LearnSkill ls = enumerator2.Current;
              if (pairList.Find((Predicate<CollaboSkillParam.Pair>) (tp =>
              {
                if (tp.UnitParam1.iname == csp.mUnitIname && tp.UnitParam2.iname == ls.PartnerUnitIname)
                  return true;
                if (tp.UnitParam1.iname == ls.PartnerUnitIname)
                  return tp.UnitParam2.iname == csp.mUnitIname;
                return false;
              })) == null)
                pairList.Add(new CollaboSkillParam.Pair(instanceDirect.MasterParam.GetUnitParam(csp.mUnitIname), instanceDirect.MasterParam.GetUnitParam(ls.PartnerUnitIname)));
            }
          }
        }
      }
      return pairList;
    }

    public static CollaboSkillParam.Pair IsLearnQuest(string quest_id)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return (CollaboSkillParam.Pair) null;
      List<string> stringList = new List<string>();
      using (List<CollaboSkillParam>.Enumerator enumerator1 = instanceDirect.MasterParam.CollaboSkills.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          using (List<CollaboSkillParam.LearnSkill>.Enumerator enumerator2 = enumerator1.Current.mLearnSkillLists.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              CollaboSkillParam.LearnSkill current = enumerator2.Current;
              if (current.QuestIname == quest_id)
                stringList.Add(current.PartnerUnitIname);
            }
          }
        }
      }
      if (stringList.Count == 2)
        return new CollaboSkillParam.Pair(instanceDirect.MasterParam.GetUnitParam(stringList[1]), instanceDirect.MasterParam.GetUnitParam(stringList[0]));
      return (CollaboSkillParam.Pair) null;
    }

    public class LearnSkill
    {
      public string QuestIname;
      public string SkillIname;
      public string PartnerUnitIname;

      public LearnSkill(string q_iname)
      {
        this.QuestIname = q_iname;
      }
    }

    public class Pair
    {
      public UnitParam UnitParam1;
      public UnitParam UnitParam2;

      public Pair(UnitParam u_param1, UnitParam u_param2)
      {
        this.UnitParam1 = u_param1;
        this.UnitParam2 = u_param2;
      }
    }
  }
}
