// Decompiled with JetBrains decompiler
// Type: SRPG.CollaboSkillParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

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
      if (Object.op_Equality((Object) instanceDirect, (Object) null))
        return;
      using (List<CollaboSkillParam>.Enumerator enumerator1 = csp_lists.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          CollaboSkillParam current1 = enumerator1.Current;
          for (int index = 0; index < current1.mLearnSkillLists.Count; ++index)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            CollaboSkillParam.\u003CUpdateCollaboSkill\u003Ec__AnonStorey225 skillCAnonStorey225 = new CollaboSkillParam.\u003CUpdateCollaboSkill\u003Ec__AnonStorey225();
            // ISSUE: reference to a compiler-generated field
            skillCAnonStorey225.ls = current1.mLearnSkillLists[index];
            // ISSUE: reference to a compiler-generated field
            if (!string.IsNullOrEmpty(skillCAnonStorey225.ls.QuestIname))
            {
              AbilityParam abilityParam1 = instanceDirect.MasterParam.GetAbilityParam(current1.AbilityIname);
              if (abilityParam1 == null)
                DebugUtility.LogError(string.Format("CollaboSkillParam/Deserialize AbilityParam not found. ability_iname={0}", (object) current1.mAbilityIname));
              else if (index < abilityParam1.skills.Length)
              {
                // ISSUE: reference to a compiler-generated field
                skillCAnonStorey225.ls.SkillIname = abilityParam1.skills[index].iname;
                using (List<CollaboSkillParam>.Enumerator enumerator2 = csp_lists.GetEnumerator())
                {
                  while (enumerator2.MoveNext())
                  {
                    CollaboSkillParam current2 = enumerator2.Current;
                    if (!(current2.mUnitIname == current1.mUnitIname))
                    {
                      AbilityParam abilityParam2 = instanceDirect.MasterParam.GetAbilityParam(current2.AbilityIname);
                      // ISSUE: reference to a compiler-generated method
                      if (abilityParam2 != null && new List<LearningSkill>((IEnumerable<LearningSkill>) abilityParam2.skills).Find(new Predicate<LearningSkill>(skillCAnonStorey225.\u003C\u003Em__22A)) != null)
                      {
                        // ISSUE: reference to a compiler-generated field
                        skillCAnonStorey225.ls.PartnerUnitIname = current2.UnitIname;
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
      if (Object.op_Equality((Object) instanceDirect, (Object) null))
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
      if (Object.op_Equality((Object) instanceDirect, (Object) null))
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
      if (Object.op_Equality((Object) instanceDirect, (Object) null))
        return pairList;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CollaboSkillParam.\u003CGetPairLists\u003Ec__AnonStorey228 listsCAnonStorey228 = new CollaboSkillParam.\u003CGetPairLists\u003Ec__AnonStorey228();
      using (List<CollaboSkillParam>.Enumerator enumerator1 = instanceDirect.MasterParam.CollaboSkills.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          listsCAnonStorey228.csp = enumerator1.Current;
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          CollaboSkillParam.\u003CGetPairLists\u003Ec__AnonStorey229 listsCAnonStorey229 = new CollaboSkillParam.\u003CGetPairLists\u003Ec__AnonStorey229();
          // ISSUE: reference to a compiler-generated field
          listsCAnonStorey229.\u003C\u003Ef__ref\u0024552 = listsCAnonStorey228;
          // ISSUE: reference to a compiler-generated field
          using (List<CollaboSkillParam.LearnSkill>.Enumerator enumerator2 = listsCAnonStorey228.csp.mLearnSkillLists.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              // ISSUE: reference to a compiler-generated field
              listsCAnonStorey229.ls = enumerator2.Current;
              // ISSUE: reference to a compiler-generated method
              if (pairList.Find(new Predicate<CollaboSkillParam.Pair>(listsCAnonStorey229.\u003C\u003Em__22E)) == null)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                pairList.Add(new CollaboSkillParam.Pair(instanceDirect.MasterParam.GetUnitParam(listsCAnonStorey228.csp.mUnitIname), instanceDirect.MasterParam.GetUnitParam(listsCAnonStorey229.ls.PartnerUnitIname)));
              }
            }
          }
        }
      }
      return pairList;
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
