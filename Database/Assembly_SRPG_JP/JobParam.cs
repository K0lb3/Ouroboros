// Decompiled with JetBrains decompiler
// Type: SRPG.JobParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class JobParam
  {
    public static readonly int MAX_JOB_RANK = 11;
    public string[] atkskill = new string[7];
    public StatusParam status = new StatusParam();
    public OInt avoid = (OInt) 0;
    public OInt inimp = (OInt) 0;
    public JobRankParam[] ranks = new JobRankParam[JobParam.MAX_JOB_RANK + 1];
    public string iname;
    public string name;
    public string expr;
    public string model;
    public string ac2d;
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
    public string MapEffectAbility;
    public bool IsMapEffectRevReso;
    public string DescCharacteristic;
    public string DescOther;
    public string unit_image;

    public bool Deserialize(JSON_JobParam json, MasterParam master_param)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.model = json.mdl;
      this.ac2d = json.ac2d;
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
      this.MapEffectAbility = json.me_abl;
      this.IsMapEffectRevReso = json.is_me_rr != 0;
      this.DescCharacteristic = json.desc_ch;
      this.DescOther = json.desc_ot;
      this.status.hp = (OInt) json.hp;
      this.status.mp = (OShort) json.mp;
      this.status.atk = (OShort) json.atk;
      this.status.def = (OShort) json.def;
      this.status.mag = (OShort) json.mag;
      this.status.mnd = (OShort) json.mnd;
      this.status.dex = (OShort) json.dex;
      this.status.spd = (OShort) json.spd;
      this.status.cri = (OShort) json.cri;
      this.status.luk = (OShort) json.luk;
      this.avoid = (OInt) json.avoid;
      this.inimp = (OInt) json.inimp;
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
      if (master_param != null)
        this.CreateBuffList(master_param);
      this.unit_image = json.unit_image;
      return true;
    }

    private void CreateBuffList(MasterParam master_param)
    {
      for (int index1 = 0; index1 < this.ranks.Length; ++index1)
      {
        if (this.ranks[index1] != null)
        {
          List<BuffEffect.BuffValues> list = new List<BuffEffect.BuffValues>();
          if (this.ranks[index1].equips != null || index1 != this.ranks.Length)
          {
            for (int index2 = 0; index2 < this.ranks[index1].equips.Length; ++index2)
            {
              if (!string.IsNullOrEmpty(this.ranks[index1].equips[index2]))
              {
                ItemParam itemParam = master_param.GetItemParam(this.ranks[index1].equips[index2]);
                if (itemParam != null && !string.IsNullOrEmpty(itemParam.skill))
                {
                  SkillData skillData = new SkillData();
                  skillData.Setup(itemParam.skill, 1, 1, master_param);
                  skillData.BuffSkill(ESkillTiming.Passive, EElement.None, (BaseStatus) null, (BaseStatus) null, (BaseStatus) null, (BaseStatus) null, (BaseStatus) null, (BaseStatus) null, (RandXorshift) null, SkillEffectTargets.Target, false, list);
                }
              }
            }
            if (list.Count > 0)
            {
              this.ranks[index1].buff_list = new BuffEffect.BuffValues[list.Count];
              for (int index2 = 0; index2 < list.Count; ++index2)
                this.ranks[index1].buff_list[index2] = list[index2];
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
      return (int) this.avoid;
    }

    public int GetJobRankInitJewelRate(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return 0;
      return (int) this.inimp;
    }

    public StatusParam GetJobRankStatus(int lv)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return (StatusParam) null;
      return this.status;
    }

    public BaseStatus GetJobTransfarStatus(int lv, EElement element)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return (BaseStatus) null;
      BaseStatus status = new BaseStatus();
      for (int index = 0; index < lv; ++index)
      {
        if (this.ranks[index].buff_list != null)
        {
          foreach (BuffEffect.BuffValues buff in this.ranks[index].buff_list)
          {
            bool flag = false;
            switch (buff.param_type)
            {
              case ParamTypes.UnitDefenseFire:
                flag = element != EElement.Fire;
                break;
              case ParamTypes.UnitDefenseWater:
                flag = element != EElement.Water;
                break;
              case ParamTypes.UnitDefenseWind:
                flag = element != EElement.Wind;
                break;
              case ParamTypes.UnitDefenseThunder:
                flag = element != EElement.Thunder;
                break;
              case ParamTypes.UnitDefenseShine:
                flag = element != EElement.Shine;
                break;
              case ParamTypes.UnitDefenseDark:
                flag = element != EElement.Dark;
                break;
            }
            if (!flag)
              BuffEffect.SetBuffValues(buff.param_type, buff.method_type, ref status, buff.value);
          }
        }
      }
      return status;
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
