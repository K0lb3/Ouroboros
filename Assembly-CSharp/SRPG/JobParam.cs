// Decompiled with JetBrains decompiler
// Type: SRPG.JobParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class JobParam
  {
    public static readonly int MAX_JOB_RANK = 11;
    public string[] atkskill = new string[7];
    public JobRankParam[] ranks = new JobRankParam[JobParam.MAX_JOB_RANK + 1];
    private string localizedNameID;
    public string iname;
    public string name;
    public string expr;
    public string model;
    public string modelp;
    public string pet;
    public string buki;
    public string origin;
    public JobTypes type;
    public RoleTypes role;
    public OInt mov;
    public OInt jmp;
    public string wepmdl;
    public string artifact;
    public string ai;
    public string master;
    public string fixed_ability;

    protected void localizeFields(string language)
    {
      this.init();
      this.name = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedNameID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.iname, "NAME");
    }

    public void Deserialize(string language, JSON_JobParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

    public bool Deserialize(JSON_JobParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.model = json.mdl;
      this.modelp = json.mdlp;
      this.pet = json.pet;
      this.buki = json.buki;
      this.origin = json.origin;
      this.type = (JobTypes) json.type;
      this.role = (RoleTypes) json.role;
      this.wepmdl = json.wepmdl;
      this.mov = (OInt) json.jmov;
      this.jmp = (OInt) json.jjmp;
      this.atkskill[0] = string.IsNullOrEmpty(json.atkskl) ? string.Empty : json.atkskl;
      this.atkskill[1] = string.IsNullOrEmpty(json.atkfi) ? string.Empty : json.atkfi;
      this.atkskill[2] = string.IsNullOrEmpty(json.atkwa) ? string.Empty : json.atkwa;
      this.atkskill[3] = string.IsNullOrEmpty(json.atkwi) ? string.Empty : json.atkwi;
      this.atkskill[4] = string.IsNullOrEmpty(json.atkth) ? string.Empty : json.atkth;
      this.atkskill[5] = string.IsNullOrEmpty(json.atksh) ? string.Empty : json.atksh;
      this.atkskill[6] = string.IsNullOrEmpty(json.atkda) ? string.Empty : json.atkda;
      this.fixed_ability = json.fixabl;
      this.artifact = json.artifact;
      this.ai = json.ai;
      this.master = json.master;
      Array.Clear((Array) this.ranks, 0, this.ranks.Length);
      if (json.ranks != null)
      {
        for (int index = 0; index < json.ranks.Length; ++index)
        {
          this.ranks[index] = new JobRankParam();
          if (!this.ranks[index].Deserialize(json.ranks[index]))
            return false;
        }
      }
      return true;
    }

    public void UpdateJobRankTransfarStatus(MasterParam master)
    {
      BaseStatus baseStatus = new BaseStatus();
      for (int index1 = 0; index1 < this.ranks.Length; ++index1)
      {
        if (this.ranks[index1] != null)
        {
          this.ranks[index1].TransfarStatus.Clear();
          baseStatus.CopyTo(this.ranks[index1].TransfarStatus);
          if (this.ranks[index1].equips != null || index1 != this.ranks.Length)
          {
            for (int index2 = 0; index2 < this.ranks[index1].equips.Length; ++index2)
            {
              if (!string.IsNullOrEmpty(this.ranks[index1].equips[index2]))
              {
                ItemParam itemParam = master.GetItemParam(this.ranks[index1].equips[index2]);
                if (itemParam != null && !string.IsNullOrEmpty((string) itemParam.skill))
                {
                  SkillData skillData = new SkillData();
                  skillData.Setup((string) itemParam.skill, 1, 1, master);
                  skillData.BuffSkill(ESkillTiming.Passive, baseStatus, (BaseStatus) null, baseStatus, (BaseStatus) null, (RandXorshift) null, SkillEffectTargets.Target);
                }
              }
            }
          }
        }
      }
    }

    public int GetJobChangeCost(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return 0;
      return this.ranks[lv].JobChangeCost;
    }

    public string[] GetJobChangeItems(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return (string[]) null;
      return this.ranks[lv].JobChangeItems;
    }

    public int[] GetJobChangeItemNums(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return (int[]) null;
      return this.ranks[lv].JobChangeItemNums;
    }

    public int GetRankupCost(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return 0;
      return this.ranks[lv].cost;
    }

    public string[] GetRankupItems(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return (string[]) null;
      return this.ranks[lv].equips;
    }

    public string GetRankupItemID(int lv, int index)
    {
      string[] rankupItems = this.GetRankupItems(lv);
      if (rankupItems != null && 0 <= index && index < rankupItems.Length)
        return rankupItems[index];
      return (string) null;
    }

    public OString[] GetLearningAbilitys(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return (OString[]) null;
      return this.ranks[lv].learnings;
    }

    public int GetJobRankAvoidRate(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return 0;
      return (int) this.ranks[lv].avoid;
    }

    public int GetJobRankInitJewelRate(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return 0;
      return (int) this.ranks[lv].inimp;
    }

    public StatusParam GetJobRankStatus(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return (StatusParam) null;
      return this.ranks[lv].status;
    }

    public BaseStatus GetJobTransfarStatus(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return (BaseStatus) null;
      return this.ranks[lv].TransfarStatus;
    }

    public static int GetJobRankCap(int unitRarity)
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(unitRarity);
      if (rarityParam != null)
        return (int) rarityParam.UnitJobLvCap;
      return 1;
    }

    public int FindRankOfAbility(string abilityID)
    {
      if (this.ranks != null)
      {
        for (int index = 0; index < this.ranks.Length; ++index)
        {
          if (this.ranks[index].learnings != null && Array.FindIndex<OString>(this.ranks[index].learnings, (Predicate<OString>) (p => (string) p == abilityID)) != -1)
            return index;
        }
      }
      return -1;
    }
  }
}
