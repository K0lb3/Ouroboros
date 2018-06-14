// Decompiled with JetBrains decompiler
// Type: SRPG.BuffEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
              if (type != ParamTypes.UsedJewelRate && type != ParamTypes.UsedJewel)
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

    private BuffMethodTypes GetBuffMethodType(BuffTypes buff, SkillParamCalcTypes calc)
    {
      if (calc != SkillParamCalcTypes.Scale)
        return BuffMethodTypes.Add;
      return buff == BuffTypes.Buff ? BuffMethodTypes.Highest : BuffMethodTypes.Lowest;
    }

    private void SetBuffValue(BuffMethodTypes type, ref OInt param, int value)
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

    private void SetBuffValue(BuffMethodTypes type, ref OShort param, int value)
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

    public void SetBuffValues(ParamTypes param_type, BuffMethodTypes method_type, ref BaseStatus status, int value)
    {
      switch (param_type)
      {
        case ParamTypes.Hp:
          this.SetBuffValue(method_type, ref status.param.values_hp, value);
          break;
        case ParamTypes.HpMax:
          this.SetBuffValue(method_type, ref status.param.values_hp, value);
          break;
        case ParamTypes.Mp:
          this.SetBuffValue(method_type, ref status.param.values[0], value);
          break;
        case ParamTypes.MpIni:
          this.SetBuffValue(method_type, ref status.param.values[1], value);
          break;
        case ParamTypes.Atk:
          this.SetBuffValue(method_type, ref status.param.values[2], value);
          break;
        case ParamTypes.Def:
          this.SetBuffValue(method_type, ref status.param.values[3], value);
          break;
        case ParamTypes.Mag:
          this.SetBuffValue(method_type, ref status.param.values[4], value);
          break;
        case ParamTypes.Mnd:
          this.SetBuffValue(method_type, ref status.param.values[5], value);
          break;
        case ParamTypes.Rec:
          this.SetBuffValue(method_type, ref status.param.values[6], value);
          break;
        case ParamTypes.Dex:
          this.SetBuffValue(method_type, ref status.param.values[7], value);
          break;
        case ParamTypes.Spd:
          this.SetBuffValue(method_type, ref status.param.values[8], value);
          break;
        case ParamTypes.Cri:
          this.SetBuffValue(method_type, ref status.param.values[9], value);
          break;
        case ParamTypes.Luk:
          this.SetBuffValue(method_type, ref status.param.values[10], value);
          break;
        case ParamTypes.Mov:
          this.SetBuffValue(method_type, ref status.param.values[11], value);
          break;
        case ParamTypes.Jmp:
          this.SetBuffValue(method_type, ref status.param.values[12], value);
          break;
        case ParamTypes.EffectRange:
          this.SetBuffValue(method_type, ref status.bonus.values[0], value);
          break;
        case ParamTypes.EffectScope:
          this.SetBuffValue(method_type, ref status.bonus.values[1], value);
          break;
        case ParamTypes.EffectHeight:
          this.SetBuffValue(method_type, ref status.bonus.values[2], value);
          break;
        case ParamTypes.Assist_Fire:
          this.SetBuffValue(method_type, ref status.element_assist.values[1], value);
          break;
        case ParamTypes.Assist_Water:
          this.SetBuffValue(method_type, ref status.element_assist.values[2], value);
          break;
        case ParamTypes.Assist_Wind:
          this.SetBuffValue(method_type, ref status.element_assist.values[3], value);
          break;
        case ParamTypes.Assist_Thunder:
          this.SetBuffValue(method_type, ref status.element_assist.values[4], value);
          break;
        case ParamTypes.Assist_Shine:
          this.SetBuffValue(method_type, ref status.element_assist.values[5], value);
          break;
        case ParamTypes.Assist_Dark:
          this.SetBuffValue(method_type, ref status.element_assist.values[6], value);
          break;
        case ParamTypes.Assist_Poison:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[0], value);
          break;
        case ParamTypes.Assist_Paralysed:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[1], value);
          break;
        case ParamTypes.Assist_Stun:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[2], value);
          break;
        case ParamTypes.Assist_Sleep:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[3], value);
          break;
        case ParamTypes.Assist_Charm:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[4], value);
          break;
        case ParamTypes.Assist_Stone:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[5], value);
          break;
        case ParamTypes.Assist_Blind:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[6], value);
          break;
        case ParamTypes.Assist_DisableSkill:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[7], value);
          break;
        case ParamTypes.Assist_DisableMove:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[8], value);
          break;
        case ParamTypes.Assist_DisableAttack:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[9], value);
          break;
        case ParamTypes.Assist_Zombie:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[10], value);
          break;
        case ParamTypes.Assist_DeathSentence:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[11], value);
          break;
        case ParamTypes.Assist_Berserk:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[12], value);
          break;
        case ParamTypes.Assist_Knockback:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[13], value);
          break;
        case ParamTypes.Assist_ResistBuff:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[14], value);
          break;
        case ParamTypes.Assist_ResistDebuff:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[15], value);
          break;
        case ParamTypes.Assist_Stop:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[16], value);
          break;
        case ParamTypes.Assist_Fast:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[17], value);
          break;
        case ParamTypes.Assist_Slow:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[18], value);
          break;
        case ParamTypes.Assist_AutoHeal:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[19], value);
          break;
        case ParamTypes.Assist_Donsoku:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[20], value);
          break;
        case ParamTypes.Assist_Rage:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[21], value);
          break;
        case ParamTypes.Assist_GoodSleep:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[22], value);
          break;
        case ParamTypes.Assist_ConditionAll:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[0], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[1], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[2], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[3], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[4], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[5], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[6], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[7], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[8], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[9], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[11], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[12], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[16], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[17], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[18], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[20], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[21], value);
          this.SetBuffValue(method_type, ref status.enchant_assist.values[24], value);
          break;
        case ParamTypes.Resist_Fire:
          this.SetBuffValue(method_type, ref status.element_resist.values[1], value);
          break;
        case ParamTypes.Resist_Water:
          this.SetBuffValue(method_type, ref status.element_resist.values[2], value);
          break;
        case ParamTypes.Resist_Wind:
          this.SetBuffValue(method_type, ref status.element_resist.values[3], value);
          break;
        case ParamTypes.Resist_Thunder:
          this.SetBuffValue(method_type, ref status.element_resist.values[4], value);
          break;
        case ParamTypes.Resist_Shine:
          this.SetBuffValue(method_type, ref status.element_resist.values[5], value);
          break;
        case ParamTypes.Resist_Dark:
          this.SetBuffValue(method_type, ref status.element_resist.values[6], value);
          break;
        case ParamTypes.Resist_Poison:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[0], value);
          break;
        case ParamTypes.Resist_Paralysed:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[1], value);
          break;
        case ParamTypes.Resist_Stun:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[2], value);
          break;
        case ParamTypes.Resist_Sleep:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[3], value);
          break;
        case ParamTypes.Resist_Charm:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[4], value);
          break;
        case ParamTypes.Resist_Stone:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[5], value);
          break;
        case ParamTypes.Resist_Blind:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[6], value);
          break;
        case ParamTypes.Resist_DisableSkill:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[7], value);
          break;
        case ParamTypes.Resist_DisableMove:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[8], value);
          break;
        case ParamTypes.Resist_DisableAttack:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[9], value);
          break;
        case ParamTypes.Resist_Zombie:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[10], value);
          break;
        case ParamTypes.Resist_DeathSentence:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[11], value);
          break;
        case ParamTypes.Resist_Berserk:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[12], value);
          break;
        case ParamTypes.Resist_Knockback:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[13], value);
          break;
        case ParamTypes.Resist_ResistBuff:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[14], value);
          break;
        case ParamTypes.Resist_ResistDebuff:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[15], value);
          break;
        case ParamTypes.Resist_Stop:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[16], value);
          break;
        case ParamTypes.Resist_Fast:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[17], value);
          break;
        case ParamTypes.Resist_Slow:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[18], value);
          break;
        case ParamTypes.Resist_AutoHeal:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[19], value);
          break;
        case ParamTypes.Resist_Donsoku:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[20], value);
          break;
        case ParamTypes.Resist_Rage:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[21], value);
          break;
        case ParamTypes.Resist_GoodSleep:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[22], value);
          break;
        case ParamTypes.Resist_ConditionAll:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[0], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[1], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[2], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[3], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[4], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[5], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[6], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[7], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[8], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[9], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[11], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[12], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[16], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[18], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[20], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[21], value);
          this.SetBuffValue(method_type, ref status.enchant_resist.values[24], value);
          break;
        case ParamTypes.HitRate:
          this.SetBuffValue(method_type, ref status.bonus.values[3], value);
          break;
        case ParamTypes.AvoidRate:
          this.SetBuffValue(method_type, ref status.bonus.values[4], value);
          break;
        case ParamTypes.CriticalRate:
          this.SetBuffValue(method_type, ref status.bonus.values[5], value);
          break;
        case ParamTypes.GainJewel:
          this.SetBuffValue(method_type, ref status.bonus.values[13], value);
          break;
        case ParamTypes.UsedJewelRate:
          this.SetBuffValue(method_type, ref status.bonus.values[14], value);
          break;
        case ParamTypes.ActionCount:
          this.SetBuffValue(method_type, ref status.bonus.values[15], value);
          break;
        case ParamTypes.SlashAttack:
          this.SetBuffValue(method_type, ref status.bonus.values[6], value);
          break;
        case ParamTypes.PierceAttack:
          this.SetBuffValue(method_type, ref status.bonus.values[7], value);
          break;
        case ParamTypes.BlowAttack:
          this.SetBuffValue(method_type, ref status.bonus.values[8], value);
          break;
        case ParamTypes.ShotAttack:
          this.SetBuffValue(method_type, ref status.bonus.values[9], value);
          break;
        case ParamTypes.MagicAttack:
          this.SetBuffValue(method_type, ref status.bonus.values[10], value);
          break;
        case ParamTypes.ReactionAttack:
          this.SetBuffValue(method_type, ref status.bonus.values[11], value);
          break;
        case ParamTypes.JumpAttack:
          this.SetBuffValue(method_type, ref status.bonus.values[12], value);
          break;
        case ParamTypes.GutsRate:
          this.SetBuffValue(method_type, ref status.bonus.values[16], value);
          break;
        case ParamTypes.AutoJewel:
          this.SetBuffValue(method_type, ref status.bonus.values[17], value);
          break;
        case ParamTypes.ChargeTimeRate:
          this.SetBuffValue(method_type, ref status.bonus.values[18], value);
          break;
        case ParamTypes.CastTimeRate:
          this.SetBuffValue(method_type, ref status.bonus.values[19], value);
          break;
        case ParamTypes.BuffTurn:
          this.SetBuffValue(method_type, ref status.bonus.values[20], value);
          break;
        case ParamTypes.DebuffTurn:
          this.SetBuffValue(method_type, ref status.bonus.values[21], value);
          break;
        case ParamTypes.CombinationRange:
          this.SetBuffValue(method_type, ref status.bonus.values[22], value);
          break;
        case ParamTypes.HpCostRate:
          this.SetBuffValue(method_type, ref status.bonus.values[23], value);
          break;
        case ParamTypes.SkillUseCount:
          this.SetBuffValue(method_type, ref status.bonus.values[24], value);
          break;
        case ParamTypes.PoisonDamage:
          this.SetBuffValue(method_type, ref status.bonus.values[25], value);
          break;
        case ParamTypes.PoisonTurn:
          this.SetBuffValue(method_type, ref status.bonus.values[26], value);
          break;
        case ParamTypes.Assist_AutoJewel:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[23], value);
          break;
        case ParamTypes.Resist_AutoJewel:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[23], value);
          break;
        case ParamTypes.Assist_DisableHeal:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[24], value);
          break;
        case ParamTypes.Resist_DisableHeal:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[24], value);
          break;
        case ParamTypes.Resist_Slash:
          this.SetBuffValue(method_type, ref status.bonus.values[27], value);
          break;
        case ParamTypes.Resist_Pierce:
          this.SetBuffValue(method_type, ref status.bonus.values[28], value);
          break;
        case ParamTypes.Resist_Blow:
          this.SetBuffValue(method_type, ref status.bonus.values[29], value);
          break;
        case ParamTypes.Resist_Shot:
          this.SetBuffValue(method_type, ref status.bonus.values[30], value);
          break;
        case ParamTypes.Resist_Magic:
          this.SetBuffValue(method_type, ref status.bonus.values[31], value);
          break;
        case ParamTypes.Resist_Reaction:
          this.SetBuffValue(method_type, ref status.bonus.values[32], value);
          break;
        case ParamTypes.Resist_Jump:
          this.SetBuffValue(method_type, ref status.bonus.values[33], value);
          break;
        case ParamTypes.Avoid_Slash:
          this.SetBuffValue(method_type, ref status.bonus.values[34], value);
          break;
        case ParamTypes.Avoid_Pierce:
          this.SetBuffValue(method_type, ref status.bonus.values[35], value);
          break;
        case ParamTypes.Avoid_Blow:
          this.SetBuffValue(method_type, ref status.bonus.values[36], value);
          break;
        case ParamTypes.Avoid_Shot:
          this.SetBuffValue(method_type, ref status.bonus.values[37], value);
          break;
        case ParamTypes.Avoid_Magic:
          this.SetBuffValue(method_type, ref status.bonus.values[38], value);
          break;
        case ParamTypes.Avoid_Reaction:
          this.SetBuffValue(method_type, ref status.bonus.values[39], value);
          break;
        case ParamTypes.Avoid_Jump:
          this.SetBuffValue(method_type, ref status.bonus.values[40], value);
          break;
        case ParamTypes.GainJewelRate:
          this.SetBuffValue(method_type, ref status.bonus.values[41], value);
          break;
        case ParamTypes.UsedJewel:
          this.SetBuffValue(method_type, ref status.bonus.values[42], value);
          break;
        case ParamTypes.Assist_SingleAttack:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[25], value);
          break;
        case ParamTypes.Assist_AreaAttack:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[26], value);
          break;
        case ParamTypes.Resist_SingleAttack:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[25], value);
          break;
        case ParamTypes.Resist_AreaAttack:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[26], value);
          break;
        case ParamTypes.Assist_DecCT:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[27], value);
          break;
        case ParamTypes.Assist_IncCT:
          this.SetBuffValue(method_type, ref status.enchant_assist.values[28], value);
          break;
        case ParamTypes.Resist_DecCT:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[27], value);
          break;
        case ParamTypes.Resist_IncCT:
          this.SetBuffValue(method_type, ref status.enchant_resist.values[28], value);
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
          BuffMethodTypes buffMethodType = this.GetBuffMethodType(target.buffType, calcType);
          ParamTypes paramType = target.paramType;
          int num = (int) target.value;
          this.SetBuffValues(paramType, buffMethodType, ref status, num);
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
