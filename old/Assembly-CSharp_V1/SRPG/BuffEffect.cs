// Decompiled with JetBrains decompiler
// Type: SRPG.BuffEffect
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

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
          this.targets[index].calcType = this.param.buffs[index].calc;
          this.targets[index].paramType = this.param.buffs[index].type;
          ParamTypes type = this.param.buffs[index].type;
          switch (type)
          {
            case ParamTypes.ChargeTimeRate:
            case ParamTypes.CastTimeRate:
            case ParamTypes.HpCostRate:
              this.targets[index].buffType = rankValue <= 0 ? BuffTypes.Buff : BuffTypes.Debuff;
              break;
            default:
              if (type != ParamTypes.UsedJewelRate)
              {
                this.targets[index].buffType = rankValue >= 0 ? BuffTypes.Buff : BuffTypes.Debuff;
                break;
              }
              goto case ParamTypes.ChargeTimeRate;
          }
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
      if (!string.IsNullOrEmpty(this.param.job))
      {
        if (target.Job != null)
          flag &= this.param.job == target.Job.Param.origin;
        else
          flag &= this.param.job == (string) target.UnitParam.djob;
      }
      if (!string.IsNullOrEmpty(this.param.buki))
      {
        if (target.Job != null)
          flag &= this.param.job == target.Job.Param.buki;
        else
          flag &= this.param.buki == (string) target.UnitParam.dbuki;
      }
      if (!string.IsNullOrEmpty(this.param.birth))
        flag &= this.param.birth == (string) target.UnitParam.birth;
      return flag;
    }

    private void SetBuffValue(BuffTypes type, ref OInt param, int value)
    {
      switch (type)
      {
        case BuffTypes.Buff:
          if ((int) param >= value)
            break;
          param = (OInt) value;
          break;
        case BuffTypes.Debuff:
          if ((int) param <= value)
            break;
          param = (OInt) value;
          break;
      }
    }

    private void SetBuffValue(BuffTypes type, ref OShort param, int value)
    {
      switch (type)
      {
        case BuffTypes.Buff:
          if ((int) param >= value)
            break;
          param = (OShort) value;
          break;
        case BuffTypes.Debuff:
          if ((int) param <= value)
            break;
          param = (OShort) value;
          break;
      }
    }

    public void CalcBuffStatus(ref BaseStatus status, BuffTypes buffType, SkillParamCalcTypes calcType)
    {
      for (int index = 0; index < this.targets.Count; ++index)
      {
        BuffEffect.BuffTarget target = this.targets[index];
        if (target.buffType == buffType && target.calcType == calcType)
        {
          ParamTypes paramType = target.paramType;
          int num = (int) target.value;
          switch (paramType)
          {
            case ParamTypes.Hp:
              this.SetBuffValue(buffType, ref status.param.values_hp, num);
              continue;
            case ParamTypes.HpMax:
              this.SetBuffValue(buffType, ref status.param.values_hp, num);
              continue;
            case ParamTypes.Mp:
              this.SetBuffValue(buffType, ref status.param.values[0], num);
              continue;
            case ParamTypes.MpIni:
              this.SetBuffValue(buffType, ref status.param.values[1], num);
              continue;
            case ParamTypes.Atk:
              this.SetBuffValue(buffType, ref status.param.values[2], num);
              continue;
            case ParamTypes.Def:
              this.SetBuffValue(buffType, ref status.param.values[3], num);
              continue;
            case ParamTypes.Mag:
              this.SetBuffValue(buffType, ref status.param.values[4], num);
              continue;
            case ParamTypes.Mnd:
              this.SetBuffValue(buffType, ref status.param.values[5], num);
              continue;
            case ParamTypes.Rec:
              this.SetBuffValue(buffType, ref status.param.values[6], num);
              continue;
            case ParamTypes.Dex:
              this.SetBuffValue(buffType, ref status.param.values[7], num);
              continue;
            case ParamTypes.Spd:
              this.SetBuffValue(buffType, ref status.param.values[8], num);
              continue;
            case ParamTypes.Cri:
              this.SetBuffValue(buffType, ref status.param.values[9], num);
              continue;
            case ParamTypes.Luk:
              this.SetBuffValue(buffType, ref status.param.values[10], num);
              continue;
            case ParamTypes.Mov:
              this.SetBuffValue(buffType, ref status.param.values[11], num);
              continue;
            case ParamTypes.Jmp:
              this.SetBuffValue(buffType, ref status.param.values[12], num);
              continue;
            case ParamTypes.EffectRange:
              this.SetBuffValue(buffType, ref status.bonus.values[0], num);
              continue;
            case ParamTypes.EffectScope:
              this.SetBuffValue(buffType, ref status.bonus.values[1], num);
              continue;
            case ParamTypes.EffectHeight:
              this.SetBuffValue(buffType, ref status.bonus.values[2], num);
              continue;
            case ParamTypes.Assist_Fire:
              this.SetBuffValue(buffType, ref status.element_assist.values[1], num);
              continue;
            case ParamTypes.Assist_Water:
              this.SetBuffValue(buffType, ref status.element_assist.values[2], num);
              continue;
            case ParamTypes.Assist_Wind:
              this.SetBuffValue(buffType, ref status.element_assist.values[3], num);
              continue;
            case ParamTypes.Assist_Thunder:
              this.SetBuffValue(buffType, ref status.element_assist.values[4], num);
              continue;
            case ParamTypes.Assist_Shine:
              this.SetBuffValue(buffType, ref status.element_assist.values[5], num);
              continue;
            case ParamTypes.Assist_Dark:
              this.SetBuffValue(buffType, ref status.element_assist.values[6], num);
              continue;
            case ParamTypes.Assist_Poison:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[0], num);
              continue;
            case ParamTypes.Assist_Paralysed:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[1], num);
              continue;
            case ParamTypes.Assist_Stun:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[2], num);
              continue;
            case ParamTypes.Assist_Sleep:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[3], num);
              continue;
            case ParamTypes.Assist_Charm:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[4], num);
              continue;
            case ParamTypes.Assist_Stone:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[5], num);
              continue;
            case ParamTypes.Assist_Blind:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[6], num);
              continue;
            case ParamTypes.Assist_DisableSkill:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[7], num);
              continue;
            case ParamTypes.Assist_DisableMove:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[8], num);
              continue;
            case ParamTypes.Assist_DisableAttack:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[9], num);
              continue;
            case ParamTypes.Assist_Zombie:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[10], num);
              continue;
            case ParamTypes.Assist_DeathSentence:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[11], num);
              continue;
            case ParamTypes.Assist_Berserk:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[12], num);
              continue;
            case ParamTypes.Assist_Knockback:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[13], num);
              continue;
            case ParamTypes.Assist_ResistBuff:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[14], num);
              continue;
            case ParamTypes.Assist_ResistDebuff:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[15], num);
              continue;
            case ParamTypes.Assist_Stop:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[16], num);
              continue;
            case ParamTypes.Assist_Fast:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[17], num);
              continue;
            case ParamTypes.Assist_Slow:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[18], num);
              continue;
            case ParamTypes.Assist_AutoHeal:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[19], num);
              continue;
            case ParamTypes.Assist_Donsoku:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[20], num);
              continue;
            case ParamTypes.Assist_Rage:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[21], num);
              continue;
            case ParamTypes.Assist_GoodSleep:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[22], num);
              continue;
            case ParamTypes.Assist_ConditionAll:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[0], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[1], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[2], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[3], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[4], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[5], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[6], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[7], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[8], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[9], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[11], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[12], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[16], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[17], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[18], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[20], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[21], num);
              this.SetBuffValue(buffType, ref status.enchant_assist.values[24], num);
              continue;
            case ParamTypes.Resist_Fire:
              this.SetBuffValue(buffType, ref status.element_resist.values[1], num);
              continue;
            case ParamTypes.Resist_Water:
              this.SetBuffValue(buffType, ref status.element_resist.values[2], num);
              continue;
            case ParamTypes.Resist_Wind:
              this.SetBuffValue(buffType, ref status.element_resist.values[3], num);
              continue;
            case ParamTypes.Resist_Thunder:
              this.SetBuffValue(buffType, ref status.element_resist.values[4], num);
              continue;
            case ParamTypes.Resist_Shine:
              this.SetBuffValue(buffType, ref status.element_resist.values[5], num);
              continue;
            case ParamTypes.Resist_Dark:
              this.SetBuffValue(buffType, ref status.element_resist.values[6], num);
              continue;
            case ParamTypes.Resist_Poison:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[0], num);
              continue;
            case ParamTypes.Resist_Paralysed:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[1], num);
              continue;
            case ParamTypes.Resist_Stun:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[2], num);
              continue;
            case ParamTypes.Resist_Sleep:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[3], num);
              continue;
            case ParamTypes.Resist_Charm:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[4], num);
              continue;
            case ParamTypes.Resist_Stone:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[5], num);
              continue;
            case ParamTypes.Resist_Blind:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[6], num);
              continue;
            case ParamTypes.Resist_DisableSkill:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[7], num);
              continue;
            case ParamTypes.Resist_DisableMove:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[8], num);
              continue;
            case ParamTypes.Resist_DisableAttack:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[9], num);
              continue;
            case ParamTypes.Resist_Zombie:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[10], num);
              continue;
            case ParamTypes.Resist_DeathSentence:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[11], num);
              continue;
            case ParamTypes.Resist_Berserk:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[12], num);
              continue;
            case ParamTypes.Resist_Knockback:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[13], num);
              continue;
            case ParamTypes.Resist_ResistBuff:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[14], num);
              continue;
            case ParamTypes.Resist_ResistDebuff:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[15], num);
              continue;
            case ParamTypes.Resist_Stop:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[16], num);
              continue;
            case ParamTypes.Resist_Fast:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[17], num);
              continue;
            case ParamTypes.Resist_Slow:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[18], num);
              continue;
            case ParamTypes.Resist_AutoHeal:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[19], num);
              continue;
            case ParamTypes.Resist_Donsoku:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[20], num);
              continue;
            case ParamTypes.Resist_Rage:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[21], num);
              continue;
            case ParamTypes.Resist_GoodSleep:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[22], num);
              continue;
            case ParamTypes.Resist_ConditionAll:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[0], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[1], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[2], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[3], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[4], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[5], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[6], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[7], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[8], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[9], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[11], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[12], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[16], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[18], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[20], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[21], num);
              this.SetBuffValue(buffType, ref status.enchant_resist.values[24], num);
              continue;
            case ParamTypes.HitRate:
              this.SetBuffValue(buffType, ref status.bonus.values[3], num);
              continue;
            case ParamTypes.AvoidRate:
              this.SetBuffValue(buffType, ref status.bonus.values[4], num);
              continue;
            case ParamTypes.CriticalRate:
              this.SetBuffValue(buffType, ref status.bonus.values[5], num);
              continue;
            case ParamTypes.GainJewel:
              this.SetBuffValue(buffType, ref status.bonus.values[13], num);
              continue;
            case ParamTypes.UsedJewelRate:
              this.SetBuffValue(buffType, ref status.bonus.values[14], num);
              continue;
            case ParamTypes.ActionCount:
              this.SetBuffValue(buffType, ref status.bonus.values[15], num);
              continue;
            case ParamTypes.SlashAttack:
              this.SetBuffValue(buffType, ref status.bonus.values[6], num);
              continue;
            case ParamTypes.PierceAttack:
              this.SetBuffValue(buffType, ref status.bonus.values[7], num);
              continue;
            case ParamTypes.BlowAttack:
              this.SetBuffValue(buffType, ref status.bonus.values[8], num);
              continue;
            case ParamTypes.ShotAttack:
              this.SetBuffValue(buffType, ref status.bonus.values[9], num);
              continue;
            case ParamTypes.MagicAttack:
              this.SetBuffValue(buffType, ref status.bonus.values[10], num);
              continue;
            case ParamTypes.ReactionAttack:
              this.SetBuffValue(buffType, ref status.bonus.values[11], num);
              continue;
            case ParamTypes.JumpAttack:
              this.SetBuffValue(buffType, ref status.bonus.values[12], num);
              continue;
            case ParamTypes.GutsRate:
              this.SetBuffValue(buffType, ref status.bonus.values[16], num);
              continue;
            case ParamTypes.AutoJewel:
              this.SetBuffValue(buffType, ref status.bonus.values[17], num);
              continue;
            case ParamTypes.ChargeTimeRate:
              this.SetBuffValue(buffType, ref status.bonus.values[18], num);
              continue;
            case ParamTypes.CastTimeRate:
              this.SetBuffValue(buffType, ref status.bonus.values[19], num);
              continue;
            case ParamTypes.BuffTurn:
              this.SetBuffValue(buffType, ref status.bonus.values[20], num);
              continue;
            case ParamTypes.DebuffTurn:
              this.SetBuffValue(buffType, ref status.bonus.values[21], num);
              continue;
            case ParamTypes.CombinationRange:
              this.SetBuffValue(buffType, ref status.bonus.values[22], num);
              continue;
            case ParamTypes.HpCostRate:
              this.SetBuffValue(buffType, ref status.bonus.values[23], num);
              continue;
            case ParamTypes.SkillUseCount:
              this.SetBuffValue(buffType, ref status.bonus.values[24], num);
              continue;
            case ParamTypes.PoisonDamage:
              this.SetBuffValue(buffType, ref status.bonus.values[25], num);
              continue;
            case ParamTypes.PoisonTurn:
              this.SetBuffValue(buffType, ref status.bonus.values[26], num);
              continue;
            case ParamTypes.Assist_AutoJewel:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[23], num);
              continue;
            case ParamTypes.Resist_AutoJewel:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[23], num);
              continue;
            case ParamTypes.Assist_DisableHeal:
              this.SetBuffValue(buffType, ref status.enchant_assist.values[24], num);
              continue;
            case ParamTypes.Resist_DisableHeal:
              this.SetBuffValue(buffType, ref status.enchant_resist.values[24], num);
              continue;
            case ParamTypes.Resist_Slash:
              this.SetBuffValue(buffType, ref status.bonus.values[27], num);
              continue;
            case ParamTypes.Resist_Pierce:
              this.SetBuffValue(buffType, ref status.bonus.values[28], num);
              continue;
            case ParamTypes.Resist_Blow:
              this.SetBuffValue(buffType, ref status.bonus.values[29], num);
              continue;
            case ParamTypes.Resist_Shot:
              this.SetBuffValue(buffType, ref status.bonus.values[30], num);
              continue;
            case ParamTypes.Resist_Magic:
              this.SetBuffValue(buffType, ref status.bonus.values[31], num);
              continue;
            case ParamTypes.Resist_Reaction:
              this.SetBuffValue(buffType, ref status.bonus.values[32], num);
              continue;
            case ParamTypes.Resist_Jump:
              this.SetBuffValue(buffType, ref status.bonus.values[33], num);
              continue;
            case ParamTypes.Avoid_Slash:
              this.SetBuffValue(buffType, ref status.bonus.values[34], num);
              continue;
            case ParamTypes.Avoid_Pierce:
              this.SetBuffValue(buffType, ref status.bonus.values[35], num);
              continue;
            case ParamTypes.Avoid_Blow:
              this.SetBuffValue(buffType, ref status.bonus.values[36], num);
              continue;
            case ParamTypes.Avoid_Shot:
              this.SetBuffValue(buffType, ref status.bonus.values[37], num);
              continue;
            case ParamTypes.Avoid_Magic:
              this.SetBuffValue(buffType, ref status.bonus.values[38], num);
              continue;
            case ParamTypes.Avoid_Reaction:
              this.SetBuffValue(buffType, ref status.bonus.values[39], num);
              continue;
            case ParamTypes.Avoid_Jump:
              this.SetBuffValue(buffType, ref status.bonus.values[40], num);
              continue;
            default:
              continue;
          }
        }
      }
    }

    public class BuffTarget
    {
      public BuffTypes buffType;
      public SkillParamCalcTypes calcType;
      public ParamTypes paramType;
      public OInt value;
    }
  }
}
