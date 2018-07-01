// Decompiled with JetBrains decompiler
// Type: SRPG.BuffEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SRPG
{
  public class BuffEffect
  {
    public BuffEffectParam param;
    public List<BuffEffect.BuffTarget> targets;

    public BuffEffect.BuffTarget this[ParamTypes type]
    {
      get
      {
        if (this.targets != null)
          return this.targets.Find((Predicate<BuffEffect.BuffTarget>) (p => p.paramType == type));
        return (BuffEffect.BuffTarget) null;
      }
    }

    public static BuffEffect CreateBuffEffect(BuffEffectParam param, int rank, int rankcap)
    {
      if (param == null || param.buffs == null || param.buffs.Length == 0)
        return (BuffEffect) null;
      BuffEffect buffEffect = new BuffEffect();
      buffEffect.param = param;
      buffEffect.targets = new List<BuffEffect.BuffTarget>(param.buffs.Length);
      buffEffect.UpdateCurrentValues(rank, rankcap);
      return buffEffect;
    }

    public void UpdateCurrentValues(int rank, int rankcap)
    {
      if (this.param == null || this.param.buffs == null || this.param.buffs.Length == 0)
      {
        this.Clear();
      }
      else
      {
        int length = this.param.buffs.Length;
        if (this.targets == null)
          this.targets = new List<BuffEffect.BuffTarget>(length);
        if (this.targets.Count > length)
          this.targets.RemoveRange(length, this.targets.Count - length);
        while (this.targets.Count < length)
          this.targets.Add(new BuffEffect.BuffTarget());
        for (int index = 0; index < length; ++index)
        {
          int valueIni = (int) this.param.buffs[index].value_ini;
          int valueMax = (int) this.param.buffs[index].value_max;
          int rankValue = this.GetRankValue(rank, rankcap, valueIni, valueMax);
          this.targets[index].value = (OInt) rankValue;
          this.targets[index].value_one = this.param.buffs[index].value_one;
          this.targets[index].calcType = this.param.buffs[index].calc;
          this.targets[index].paramType = this.param.buffs[index].type;
          this.targets[index].buffType = !BuffEffectParam.IsNegativeValueIsBuff(this.param.buffs[index].type) ? (rankValue >= 0 ? BuffTypes.Buff : BuffTypes.Debuff) : (rankValue <= 0 ? BuffTypes.Buff : BuffTypes.Debuff);
        }
      }
    }

    private int GetRankValue(int rank, int rankcap, int ini, int max)
    {
      int num1 = rankcap - 1;
      int num2 = rank - 1;
      if (ini == max || num2 < 1 || num1 < 1)
        return ini;
      if (num2 >= num1)
        return max;
      int num3 = (max - ini) * 100 / num1;
      return ini + num3 * num2 / 100;
    }

    private void Clear()
    {
      this.param = (BuffEffectParam) null;
      this.targets = (List<BuffEffect.BuffTarget>) null;
    }

    public bool CheckBuffCalcType(BuffTypes buff, SkillParamCalcTypes calc)
    {
      for (int index = 0; index < this.targets.Count; ++index)
      {
        if (buff == this.targets[index].buffType && calc == this.targets[index].calcType)
          return true;
      }
      return false;
    }

    public bool CheckBuffCalcType(BuffTypes buff, SkillParamCalcTypes calc, bool is_negative_value_is_buff)
    {
      for (int index = 0; index < this.targets.Count; ++index)
      {
        BuffEffect.BuffTarget target = this.targets[index];
        if (buff == target.buffType && calc == target.calcType && BuffEffectParam.IsNegativeValueIsBuff(target.paramType) == is_negative_value_is_buff)
          return true;
      }
      return false;
    }

    public bool CheckEnableBuffTarget(Unit target)
    {
      if (this.param == null)
        return false;
      bool flag = true;
      if (this.param.sex != ESex.Unknown)
        flag &= this.param.sex == target.UnitParam.sex;
      if (this.param.elem != 0)
      {
        int num = 1 << (int) (target.Element - 1 & (EElement) 31);
        flag &= (this.param.elem & num) == num;
      }
      if (!string.IsNullOrEmpty(this.param.job) && target.Job != null)
        flag &= this.param.job == target.Job.Param.origin;
      if (!string.IsNullOrEmpty(this.param.buki) && target.Job != null)
        flag &= this.param.job == target.Job.Param.buki;
      if (!string.IsNullOrEmpty(this.param.birth))
        flag &= this.param.birth == (string) target.UnitParam.birth;
      if (!string.IsNullOrEmpty(this.param.un_group))
      {
        UnitGroupParam unitGroup = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitGroup(this.param.un_group);
        if (unitGroup != null)
          flag &= unitGroup.IsInGroup(target.UnitParam.iname);
      }
      if (this.param.custom_targets != null && this.param.cond == ESkillCondition.CardSkill)
        flag &= this.CheckCustomTarget(target);
      return flag;
    }

    private bool CheckCustomTarget(Unit target)
    {
      foreach (string customTarget1 in this.param.custom_targets)
      {
        if (!string.IsNullOrEmpty(customTarget1))
        {
          CustomTargetParam customTarget2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetCustomTarget(customTarget1);
          if (customTarget2 != null)
          {
            if (customTarget2.units != null)
            {
              bool flag = false;
              foreach (string unit in customTarget2.units)
              {
                if (target.UnitParam.iname == unit)
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
                continue;
            }
            if (customTarget2.jobs != null)
            {
              if (target.Job != null)
              {
                bool flag = false;
                foreach (string job in customTarget2.jobs)
                {
                  if (target.Job.JobID == job)
                  {
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                  continue;
              }
              else
                continue;
            }
            if (customTarget2.unit_groups != null)
            {
              bool flag = false;
              foreach (string unitGroup1 in customTarget2.unit_groups)
              {
                UnitGroupParam unitGroup2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitGroup(unitGroup1);
                if (unitGroup2 == null)
                  Debug.LogWarning((object) "存在しないユニットグループ識別子が設定されている : CustomTarget");
                else if (unitGroup2.IsInGroup(target.UnitParam.iname))
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
                continue;
            }
            if (customTarget2.job_groups != null)
            {
              if (target.Job != null)
              {
                bool flag = false;
                foreach (string jobGroup1 in customTarget2.job_groups)
                {
                  JobGroupParam jobGroup2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetJobGroup(jobGroup1);
                  if (jobGroup2 == null)
                    Debug.LogWarning((object) "存在しないジョブグループ識別子が設定されている : CustomTarget");
                  else if (jobGroup2.IsInGroup(target.Job.JobID))
                  {
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                  continue;
              }
              else
                continue;
            }
            JobData[] jobs = target.UnitData.Jobs;
            if ((string.IsNullOrEmpty(customTarget2.first_job) || jobs != null && jobs.Length >= 1 && !(jobs[0].JobID != customTarget2.first_job)) && (string.IsNullOrEmpty(customTarget2.second_job) || jobs != null && jobs.Length >= 2 && !(jobs[1].JobID != customTarget2.second_job)) && ((string.IsNullOrEmpty(customTarget2.third_job) || jobs != null && jobs.Length >= 3 && !(jobs[2].JobID != customTarget2.third_job)) && ((customTarget2.sex == ESex.Unknown || customTarget2.sex == target.UnitParam.sex) && (customTarget2.birth_id == 0 || customTarget2.birth_id == target.UnitParam.birthID))))
            {
              if (customTarget2.element != 0)
              {
                int num = 1 << (int) (target.Element - 1 & (EElement) 31);
                if ((customTarget2.element & num) != num)
                  continue;
              }
              return true;
            }
          }
        }
      }
      return false;
    }

    private BuffMethodTypes GetBuffMethodType(BuffTypes buff, SkillParamCalcTypes calc)
    {
      if (calc != SkillParamCalcTypes.Scale)
        return BuffMethodTypes.Add;
      return buff == BuffTypes.Buff ? BuffMethodTypes.Highest : BuffMethodTypes.Lowest;
    }

    private static void SetBuffValue(BuffMethodTypes type, ref OInt param, int value)
    {
      switch (type)
      {
        case BuffMethodTypes.Add:
          param = (OInt) ((int) param + value);
          break;
        case BuffMethodTypes.Highest:
          if ((int) param >= value)
            break;
          param = (OInt) value;
          break;
        case BuffMethodTypes.Lowest:
          if ((int) param <= value)
            break;
          param = (OInt) value;
          break;
      }
    }

    private static void SetBuffValue(BuffMethodTypes type, ref OShort param, int value)
    {
      switch (type)
      {
        case BuffMethodTypes.Add:
          param = (OShort) ((int) param + value);
          break;
        case BuffMethodTypes.Highest:
          if ((int) param >= value)
            break;
          param = (OShort) value;
          break;
        case BuffMethodTypes.Lowest:
          if ((int) param <= value)
            break;
          param = (OShort) value;
          break;
      }
    }

    public static void SetBuffValues(ParamTypes param_type, BuffMethodTypes method_type, ref BaseStatus status, int value)
    {
      switch (param_type)
      {
        case ParamTypes.Hp:
          BuffEffect.SetBuffValue(method_type, ref status.param.values_hp, value);
          break;
        case ParamTypes.HpMax:
          BuffEffect.SetBuffValue(method_type, ref status.param.values_hp, value);
          break;
        case ParamTypes.Mp:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[0], value);
          break;
        case ParamTypes.MpIni:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[1], value);
          break;
        case ParamTypes.Atk:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[2], value);
          break;
        case ParamTypes.Def:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[3], value);
          break;
        case ParamTypes.Mag:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[4], value);
          break;
        case ParamTypes.Mnd:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[5], value);
          break;
        case ParamTypes.Rec:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[6], value);
          break;
        case ParamTypes.Dex:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[7], value);
          break;
        case ParamTypes.Spd:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[8], value);
          break;
        case ParamTypes.Cri:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[9], value);
          break;
        case ParamTypes.Luk:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[10], value);
          break;
        case ParamTypes.Mov:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[11], value);
          break;
        case ParamTypes.Jmp:
          BuffEffect.SetBuffValue(method_type, ref status.param.values[12], value);
          break;
        case ParamTypes.EffectRange:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[0], value);
          break;
        case ParamTypes.EffectScope:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[1], value);
          break;
        case ParamTypes.EffectHeight:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[2], value);
          break;
        case ParamTypes.Assist_Fire:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[1], value);
          break;
        case ParamTypes.Assist_Water:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[2], value);
          break;
        case ParamTypes.Assist_Wind:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[3], value);
          break;
        case ParamTypes.Assist_Thunder:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[4], value);
          break;
        case ParamTypes.Assist_Shine:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[5], value);
          break;
        case ParamTypes.Assist_Dark:
          BuffEffect.SetBuffValue(method_type, ref status.element_assist.values[6], value);
          break;
        case ParamTypes.Assist_Poison:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[0], value);
          break;
        case ParamTypes.Assist_Paralysed:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[1], value);
          break;
        case ParamTypes.Assist_Stun:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[2], value);
          break;
        case ParamTypes.Assist_Sleep:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[3], value);
          break;
        case ParamTypes.Assist_Charm:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[4], value);
          break;
        case ParamTypes.Assist_Stone:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[5], value);
          break;
        case ParamTypes.Assist_Blind:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[6], value);
          break;
        case ParamTypes.Assist_DisableSkill:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[7], value);
          break;
        case ParamTypes.Assist_DisableMove:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[8], value);
          break;
        case ParamTypes.Assist_DisableAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[9], value);
          break;
        case ParamTypes.Assist_Zombie:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[10], value);
          break;
        case ParamTypes.Assist_DeathSentence:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[11], value);
          break;
        case ParamTypes.Assist_Berserk:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[12], value);
          break;
        case ParamTypes.Assist_Knockback:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[13], value);
          break;
        case ParamTypes.Assist_ResistBuff:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[14], value);
          break;
        case ParamTypes.Assist_ResistDebuff:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[15], value);
          break;
        case ParamTypes.Assist_Stop:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[16], value);
          break;
        case ParamTypes.Assist_Fast:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[17], value);
          break;
        case ParamTypes.Assist_Slow:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[18], value);
          break;
        case ParamTypes.Assist_AutoHeal:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[19], value);
          break;
        case ParamTypes.Assist_Donsoku:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[20], value);
          break;
        case ParamTypes.Assist_Rage:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[21], value);
          break;
        case ParamTypes.Assist_GoodSleep:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[22], value);
          break;
        case ParamTypes.Assist_ConditionAll:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[0], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[1], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[2], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[3], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[4], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[5], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[6], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[7], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[8], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[9], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[11], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[12], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[16], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[17], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[18], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[20], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[21], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[24], value);
          break;
        case ParamTypes.Resist_Fire:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[1], value);
          break;
        case ParamTypes.Resist_Water:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[2], value);
          break;
        case ParamTypes.Resist_Wind:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[3], value);
          break;
        case ParamTypes.Resist_Thunder:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[4], value);
          break;
        case ParamTypes.Resist_Shine:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[5], value);
          break;
        case ParamTypes.Resist_Dark:
          BuffEffect.SetBuffValue(method_type, ref status.element_resist.values[6], value);
          break;
        case ParamTypes.Resist_Poison:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[0], value);
          break;
        case ParamTypes.Resist_Paralysed:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[1], value);
          break;
        case ParamTypes.Resist_Stun:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[2], value);
          break;
        case ParamTypes.Resist_Sleep:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[3], value);
          break;
        case ParamTypes.Resist_Charm:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[4], value);
          break;
        case ParamTypes.Resist_Stone:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[5], value);
          break;
        case ParamTypes.Resist_Blind:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[6], value);
          break;
        case ParamTypes.Resist_DisableSkill:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[7], value);
          break;
        case ParamTypes.Resist_DisableMove:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[8], value);
          break;
        case ParamTypes.Resist_DisableAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[9], value);
          break;
        case ParamTypes.Resist_Zombie:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[10], value);
          break;
        case ParamTypes.Resist_DeathSentence:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[11], value);
          break;
        case ParamTypes.Resist_Berserk:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[12], value);
          break;
        case ParamTypes.Resist_Knockback:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[13], value);
          break;
        case ParamTypes.Resist_ResistBuff:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[14], value);
          break;
        case ParamTypes.Resist_ResistDebuff:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[15], value);
          break;
        case ParamTypes.Resist_Stop:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[16], value);
          break;
        case ParamTypes.Resist_Fast:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[17], value);
          break;
        case ParamTypes.Resist_Slow:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[18], value);
          break;
        case ParamTypes.Resist_AutoHeal:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[19], value);
          break;
        case ParamTypes.Resist_Donsoku:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[20], value);
          break;
        case ParamTypes.Resist_Rage:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[21], value);
          break;
        case ParamTypes.Resist_GoodSleep:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[22], value);
          break;
        case ParamTypes.Resist_ConditionAll:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[0], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[1], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[2], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[3], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[4], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[5], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[6], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[7], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[8], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[9], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[11], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[12], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[16], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[18], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[20], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[21], value);
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[24], value);
          break;
        case ParamTypes.HitRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[3], value);
          break;
        case ParamTypes.AvoidRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[4], value);
          break;
        case ParamTypes.CriticalRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[5], value);
          break;
        case ParamTypes.GainJewel:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[13], value);
          break;
        case ParamTypes.UsedJewelRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[14], value);
          break;
        case ParamTypes.ActionCount:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[15], value);
          break;
        case ParamTypes.SlashAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[6], value);
          break;
        case ParamTypes.PierceAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[7], value);
          break;
        case ParamTypes.BlowAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[8], value);
          break;
        case ParamTypes.ShotAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[9], value);
          break;
        case ParamTypes.MagicAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[10], value);
          break;
        case ParamTypes.ReactionAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[11], value);
          break;
        case ParamTypes.JumpAttack:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[12], value);
          break;
        case ParamTypes.GutsRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[16], value);
          break;
        case ParamTypes.AutoJewel:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[17], value);
          break;
        case ParamTypes.ChargeTimeRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[18], value);
          break;
        case ParamTypes.CastTimeRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[19], value);
          break;
        case ParamTypes.BuffTurn:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[20], value);
          break;
        case ParamTypes.DebuffTurn:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[21], value);
          break;
        case ParamTypes.CombinationRange:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[22], value);
          break;
        case ParamTypes.HpCostRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[23], value);
          break;
        case ParamTypes.SkillUseCount:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[24], value);
          break;
        case ParamTypes.PoisonDamage:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[25], value);
          break;
        case ParamTypes.PoisonTurn:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[26], value);
          break;
        case ParamTypes.Assist_AutoJewel:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[23], value);
          break;
        case ParamTypes.Resist_AutoJewel:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[23], value);
          break;
        case ParamTypes.Assist_DisableHeal:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[24], value);
          break;
        case ParamTypes.Resist_DisableHeal:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[24], value);
          break;
        case ParamTypes.Resist_Slash:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[27], value);
          break;
        case ParamTypes.Resist_Pierce:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[28], value);
          break;
        case ParamTypes.Resist_Blow:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[29], value);
          break;
        case ParamTypes.Resist_Shot:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[30], value);
          break;
        case ParamTypes.Resist_Magic:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[31], value);
          break;
        case ParamTypes.Resist_Reaction:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[32], value);
          break;
        case ParamTypes.Resist_Jump:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[33], value);
          break;
        case ParamTypes.Avoid_Slash:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[34], value);
          break;
        case ParamTypes.Avoid_Pierce:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[35], value);
          break;
        case ParamTypes.Avoid_Blow:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[36], value);
          break;
        case ParamTypes.Avoid_Shot:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[37], value);
          break;
        case ParamTypes.Avoid_Magic:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[38], value);
          break;
        case ParamTypes.Avoid_Reaction:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[39], value);
          break;
        case ParamTypes.Avoid_Jump:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[40], value);
          break;
        case ParamTypes.GainJewelRate:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[41], value);
          break;
        case ParamTypes.UsedJewel:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[42], value);
          break;
        case ParamTypes.Assist_SingleAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[25], value);
          break;
        case ParamTypes.Assist_AreaAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[26], value);
          break;
        case ParamTypes.Resist_SingleAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[25], value);
          break;
        case ParamTypes.Resist_AreaAttack:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[26], value);
          break;
        case ParamTypes.Assist_DecCT:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[27], value);
          break;
        case ParamTypes.Assist_IncCT:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[28], value);
          break;
        case ParamTypes.Resist_DecCT:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[27], value);
          break;
        case ParamTypes.Resist_IncCT:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[28], value);
          break;
        case ParamTypes.Assist_ESA_Fire:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[29], value);
          break;
        case ParamTypes.Assist_ESA_Water:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[30], value);
          break;
        case ParamTypes.Assist_ESA_Wind:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[31], value);
          break;
        case ParamTypes.Assist_ESA_Thunder:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[32], value);
          break;
        case ParamTypes.Assist_ESA_Shine:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[33], value);
          break;
        case ParamTypes.Assist_ESA_Dark:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[34], value);
          break;
        case ParamTypes.Resist_ESA_Fire:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[29], value);
          break;
        case ParamTypes.Resist_ESA_Water:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[30], value);
          break;
        case ParamTypes.Resist_ESA_Wind:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[31], value);
          break;
        case ParamTypes.Resist_ESA_Thunder:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[32], value);
          break;
        case ParamTypes.Resist_ESA_Shine:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[33], value);
          break;
        case ParamTypes.Resist_ESA_Dark:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[34], value);
          break;
        case ParamTypes.UnitDefenseFire:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[43], value);
          break;
        case ParamTypes.UnitDefenseWater:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[44], value);
          break;
        case ParamTypes.UnitDefenseWind:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[45], value);
          break;
        case ParamTypes.UnitDefenseThunder:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[46], value);
          break;
        case ParamTypes.UnitDefenseShine:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[47], value);
          break;
        case ParamTypes.UnitDefenseDark:
          BuffEffect.SetBuffValue(method_type, ref status.bonus.values[48], value);
          break;
        case ParamTypes.Assist_MaxDamageHp:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[35], value);
          break;
        case ParamTypes.Assist_MaxDamageMp:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_assist.values[36], value);
          break;
        case ParamTypes.Resist_MaxDamageHp:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[35], value);
          break;
        case ParamTypes.Resist_MaxDamageMp:
          BuffEffect.SetBuffValue(method_type, ref status.enchant_resist.values[36], value);
          break;
      }
    }

    public void CalcBuffStatus(ref BaseStatus status, EElement element, BuffTypes buffType, bool is_check_negative_value, bool is_negative_value_is_buff, SkillParamCalcTypes calcType, int up_count = 0)
    {
      for (int index = 0; index < this.targets.Count; ++index)
      {
        BuffEffect.BuffTarget target = this.targets[index];
        if (target.buffType == buffType && (!is_check_negative_value || BuffEffectParam.IsNegativeValueIsBuff(target.paramType) == is_negative_value_is_buff) && target.calcType == calcType)
        {
          if (element != EElement.None)
          {
            bool flag = false;
            switch (target.paramType)
            {
              case ParamTypes.Assist_Fire:
              case ParamTypes.UnitDefenseFire:
                flag = element != EElement.Fire;
                break;
              case ParamTypes.Assist_Water:
              case ParamTypes.UnitDefenseWater:
                flag = element != EElement.Water;
                break;
              case ParamTypes.Assist_Wind:
              case ParamTypes.UnitDefenseWind:
                flag = element != EElement.Wind;
                break;
              case ParamTypes.Assist_Thunder:
              case ParamTypes.UnitDefenseThunder:
                flag = element != EElement.Thunder;
                break;
              case ParamTypes.Assist_Shine:
              case ParamTypes.UnitDefenseShine:
                flag = element != EElement.Shine;
                break;
              case ParamTypes.Assist_Dark:
              case ParamTypes.UnitDefenseDark:
                flag = element != EElement.Dark;
                break;
            }
            if (flag)
              continue;
          }
          BuffMethodTypes buffMethodType = this.GetBuffMethodType(target.buffType, calcType);
          ParamTypes paramType = target.paramType;
          int val1 = (int) target.value;
          if ((bool) this.param.mIsUpBuff)
          {
            val1 = (int) target.value_one * up_count;
            if (val1 > 0)
              val1 = Math.Min(val1, (int) target.value);
            else if (val1 < 0)
              val1 = Math.Max(val1, (int) target.value);
          }
          BuffEffect.SetBuffValues(paramType, buffMethodType, ref status, val1);
        }
      }
    }

    public void GetBaseStatus(ref BaseStatus total_add, ref BaseStatus total_scale)
    {
      if (total_add == null || total_scale == null)
        return;
      total_add.Clear();
      total_scale.Clear();
      BaseStatus status1 = new BaseStatus();
      BaseStatus status2 = new BaseStatus();
      BaseStatus status3 = new BaseStatus();
      BaseStatus status4 = new BaseStatus();
      BaseStatus status5 = new BaseStatus();
      BaseStatus status6 = new BaseStatus();
      this.CalcBuffStatus(ref status1, EElement.None, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add, 0);
      this.CalcBuffStatus(ref status2, EElement.None, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add, 0);
      this.CalcBuffStatus(ref status3, EElement.None, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale, 0);
      this.CalcBuffStatus(ref status4, EElement.None, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add, 0);
      this.CalcBuffStatus(ref status5, EElement.None, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add, 0);
      this.CalcBuffStatus(ref status6, EElement.None, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale, 0);
      total_add.Add(status1);
      total_add.Add(status2);
      total_add.Add(status4);
      total_add.Add(status5);
      total_scale.Add(status3);
      total_scale.Add(status6);
    }

    public class BuffTarget
    {
      public BuffTypes buffType;
      public SkillParamCalcTypes calcType;
      public ParamTypes paramType;
      public OInt value;
      public OInt value_one;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct BuffValues
    {
      public ParamTypes param_type { get; set; }

      public BuffMethodTypes method_type { get; set; }

      public int value { get; set; }
    }
  }
}
