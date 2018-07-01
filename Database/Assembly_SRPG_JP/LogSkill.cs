// Decompiled with JetBrains decompiler
// Type: SRPG.LogSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class LogSkill : BattleLog
  {
    public LogSkill.Target self_effect = new LogSkill.Target();
    public List<LogSkill.Target> targets = new List<LogSkill.Target>(BattleCore.MAX_UNITS);
    public Unit self;
    public int hp_cost;
    public int rad;
    public int height;
    public long buff;
    public long debuff;
    public SkillData skill;
    public IntVector2 pos;
    public Grid landing;
    public Grid TeleportGrid;
    public LogSkill.Reflection reflect;
    public Unit CauseOfReaction;
    public bool is_append;
    public bool is_gimmick;

    public LogSkill.Target FindTarget(Unit target)
    {
      for (int index = 0; index < this.targets.Count; ++index)
      {
        if (this.targets[index].target == target)
          return this.targets[index];
      }
      return (LogSkill.Target) null;
    }

    public LogSkill.Target SetSkillTarget(Unit unit, Unit other)
    {
      LogSkill.Target target = this.FindTarget(other);
      if (target == null)
      {
        target = new LogSkill.Target();
        target.target = other;
        this.targets.Add(target);
      }
      this.self = unit;
      return target;
    }

    public void CheckAliveTarget()
    {
      List<LogSkill.Target> targetList = new List<LogSkill.Target>(BattleCore.MAX_UNITS);
      using (List<LogSkill.Target>.Enumerator enumerator = this.targets.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          LogSkill.Target current = enumerator.Current;
          if (!current.target.IsDead)
            targetList.Add(current);
        }
      }
      if (this.targets.Count == targetList.Count)
        return;
      this.targets = targetList;
    }

    public void Hit(Unit unit, Unit other, int hp_damage, int mp_damage, int ch_damage, int ca_damage, int hp_heal, int mp_heal, int ch_heal, int ca_heal, int dropgems, bool is_critical, bool is_avoid, bool is_combination, bool is_guts, int absorbed = 0, bool is_pf_avoid = false, int critical_rate = 0, int avoid_rate = 0)
    {
      LogSkill.Target target = this.SetSkillTarget(unit, other);
      if (unit.IsUnitFlag(EUnitFlag.BackAttack) && this.skill.BackAttackDefenseDownRate != 0)
        target.hitType |= LogSkill.EHitTypes.BackAttack;
      if (unit.IsUnitFlag(EUnitFlag.SideAttack) && this.skill.SideAttackDefenseDownRate != 0)
        target.hitType |= LogSkill.EHitTypes.SideAttack;
      if (is_guts)
        target.hitType |= LogSkill.EHitTypes.Guts;
      if (is_combination)
        target.hitType |= LogSkill.EHitTypes.Combination;
      target.shieldDamage += absorbed;
      target.gems += dropgems;
      target.hits.Add(new BattleCore.HitData(hp_damage, mp_damage, ch_damage, ca_damage, hp_heal, mp_heal, ch_heal, ca_heal, is_critical, is_avoid, is_pf_avoid, critical_rate, avoid_rate));
    }

    public void ToSelfSkillEffect(int hp_damage, int mp_damage, int ch_damage, int ca_damage, int hp_heal, int mp_heal, int ch_heal, int ca_heal, int dropgems, bool is_critical, bool is_avoid, bool is_combination, bool is_guts)
    {
      this.self_effect.target = this.self;
      this.self_effect.hits.Add(new BattleCore.HitData(hp_damage, mp_damage, ch_damage, ca_damage, hp_heal, mp_heal, ch_heal, ca_heal, is_critical, is_avoid, false, 0, 0));
    }

    public void SetDefendEffect(Unit defender)
    {
      LogSkill.Target target = this.FindTarget(defender);
      if (target == null)
        return;
      target.hitType |= LogSkill.EHitTypes.Defend;
    }

    public int GetGainJewel()
    {
      int val1 = 0;
      for (int index = 0; index < this.targets.Count; ++index)
      {
        LogSkill.Target target = this.targets[index];
        if (target.gems != 0 && !target.IsAvoid())
          val1 = Math.Max(val1, target.gems);
      }
      return val1;
    }

    public bool IsRenkei()
    {
      for (int index = this.targets.Count - 1; index >= 0; --index)
      {
        if ((this.targets[index].hitType & LogSkill.EHitTypes.Combination) != (LogSkill.EHitTypes) 0)
          return true;
      }
      return false;
    }

    public int GetTruthTotalHpHeal()
    {
      int num1 = 0;
      for (int index = 0; index < this.targets.Count; ++index)
      {
        int num2 = Math.Max(Math.Min(this.targets[index].GetTotalHpHeal(), (int) this.targets[index].target.MaximumStatus.param.hp - (int) this.targets[index].target.CurrentStatus.param.hp), 0);
        num1 += num2;
      }
      return num1;
    }

    public int GetTruthTotalHpHealCount()
    {
      int num = 0;
      for (int index = 0; index < this.targets.Count; ++index)
      {
        if ((int) this.targets[index].target.CurrentStatus.param.hp != (int) this.targets[index].target.MaximumStatus.param.hp && this.targets[index].GetTotalHpHeal() != 0)
          ++num;
      }
      return num;
    }

    public int GetTruthTotalHpDamage()
    {
      int num1 = 0;
      for (int index = 0; index < this.targets.Count; ++index)
      {
        int num2 = Math.Max(Math.Min(this.targets[index].GetTotalHpDamage(), (int) this.targets[index].target.CurrentStatus.param.hp), 0);
        num1 += num2;
      }
      return num1;
    }

    public int GetTotalDeathCount()
    {
      int num = 0;
      for (int index = 0; index < this.targets.Count; ++index)
      {
        if ((int) this.targets[index].target.CurrentStatus.param.hp <= this.targets[index].GetTotalHpDamage())
          ++num;
      }
      return num;
    }

    public int GetTotalCureConditionCount()
    {
      CondEffect condEffect = this.skill.GetCondEffect(SkillEffectTargets.Target);
      if (condEffect == null || condEffect.param == null || (condEffect.param.conditions == null || condEffect.param.type != ConditionEffectTypes.CureCondition))
        return 0;
      int num = 0;
      for (int index1 = 0; index1 < this.targets.Count; ++index1)
      {
        for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
        {
          EUnitCondition condition = condEffect.param.conditions[index2];
          if (this.targets[index1].target.IsUnitCondition(condition) && !this.targets[index1].target.IsPassiveUnitCondition(condition))
            ++num;
        }
      }
      return num;
    }

    public int GetTotalFailConditionCount()
    {
      CondEffect condEffect = this.skill.GetCondEffect(SkillEffectTargets.Target);
      if (condEffect == null || condEffect.param == null || condEffect.param.conditions == null || condEffect.param.type != ConditionEffectTypes.FailCondition && condEffect.param.type != ConditionEffectTypes.ForcedFailCondition && condEffect.param.type != ConditionEffectTypes.RandomFailCondition)
        return 0;
      int num1 = 0;
      int num2 = 0;
      if (this.self.AI != null)
        num2 = (int) this.self.AI.cond_border;
      if (num2 > 0 && (int) condEffect.rate > 0 && (int) condEffect.rate < num2)
        return 0;
      for (int index1 = 0; index1 < this.targets.Count; ++index1)
      {
        for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
        {
          Unit target = this.targets[index1].target;
          EUnitCondition condition = condEffect.param.conditions[index2];
          if (!target.IsUnitCondition(condition) && !target.IsDisableUnitCondition(condition) && (num2 <= 0 || Math.Max((int) condEffect.value - (int) target.CurrentStatus.enchant_resist[condition], 0) >= num2))
            ++num1;
        }
      }
      return num1;
    }

    public int GetTotalDisableConditionCount()
    {
      CondEffect condEffect = this.skill.GetCondEffect(SkillEffectTargets.Target);
      if (condEffect == null || condEffect.param == null || (condEffect.param.conditions == null || condEffect.param.type != ConditionEffectTypes.DisableCondition))
        return 0;
      int num = 0;
      for (int index1 = 0; index1 < this.targets.Count; ++index1)
      {
        Unit target = this.targets[index1].target;
        for (int index2 = 0; index2 < condEffect.param.conditions.Length; ++index2)
        {
          switch (condEffect.param.conditions[index2])
          {
            case EUnitCondition.DisableBuff:
              if (target.CheckActionSkillBuffAttachments(BuffTypes.Buff))
                goto default;
              else
                break;
            case EUnitCondition.DisableDebuff:
              if (target.CheckActionSkillBuffAttachments(BuffTypes.Debuff))
                goto default;
              else
                break;
            default:
              ++num;
              break;
          }
        }
      }
      return num;
    }

    public void GetTotalBuffEffect(out int buff_count, out int buff_value)
    {
      int buff_count1 = 0;
      int buff_value1 = 0;
      for (int index = 0; index < this.targets.Count; ++index)
      {
        int buff_count2 = 0;
        int buff_value2 = 0;
        this.targets[index].target.GetEnableBetterBuffEffect(this.self, this.skill, SkillEffectTargets.Target, out buff_count2, out buff_value2, true);
        buff_count1 += buff_count2;
        buff_value1 += buff_value2;
      }
      if (buff_count1 == 0 && buff_value1 == 0)
        this.self.GetEnableBetterBuffEffect(this.self, this.skill, SkillEffectTargets.Self, out buff_count1, out buff_value1, true);
      buff_count = buff_count1;
      buff_value = buff_value1;
    }

    [Flags]
    public enum EHitTypes
    {
      BackAttack = 1,
      SideAttack = 2,
      ItemSteal = 4,
      GoldSteal = 8,
      GemsSteal = 16, // 0x00000010
      Guts = 32, // 0x00000020
      Combination = 64, // 0x00000040
      Defend = 128, // 0x00000080
      CastBreak = 256, // 0x00000100
      PerfectAvoid = 512, // 0x00000200
    }

    public class Target
    {
      public List<BattleCore.HitData> hits = new List<BattleCore.HitData>();
      public BuffBit buff = new BuffBit();
      public BuffBit debuff = new BuffBit();
      public List<LogSkill.Target.CondHit> CondHitLists = new List<LogSkill.Target.CondHit>();
      public Unit target;
      public LogSkill.EHitTypes hitType;
      public int gems;
      public SkillData defSkill;
      public int defSkillUseCount;
      public int shieldDamage;
      public bool isProcShield;
      public EUnitCondition failCondition;
      public EUnitCondition cureCondition;
      public Unit guard;
      public bool is_force_reaction;
      public int element_effect_rate;
      public int element_effect_resist;
      public Grid KnockBackGrid;
      public int ChangeValueCT;
      public bool IsOldDying;

      public int GetTotalHpDamage()
      {
        int num = 0;
        for (int index = 0; index < this.hits.Count; ++index)
          num += this.hits[index].hp_damage;
        return num;
      }

      public int GetTotalHpHeal()
      {
        int num = 0;
        for (int index = 0; index < this.hits.Count; ++index)
          num += this.hits[index].hp_heal;
        return num;
      }

      public int GetTotalMpDamage()
      {
        int num = 0;
        for (int index = 0; index < this.hits.Count; ++index)
          num += this.hits[index].mp_damage;
        return num;
      }

      public int GetTotalMpHeal()
      {
        int num = 0;
        for (int index = 0; index < this.hits.Count; ++index)
          num += this.hits[index].mp_heal;
        return num;
      }

      public int GetTotalCriticalRate()
      {
        int num1 = 0;
        int num2 = 0;
        for (int index = 0; index < this.hits.Count; ++index)
        {
          num1 += this.hits[index].critical_rate;
          ++num2;
        }
        if (num2 != 0)
          num1 /= num2;
        return num1;
      }

      public int GetTotalAvoidRate()
      {
        int num1 = 0;
        int num2 = 0;
        for (int index = 0; index < this.hits.Count; ++index)
        {
          num1 += this.hits[index].avoid_rate;
          ++num2;
        }
        if (num2 != 0)
          num1 /= num2;
        return num1;
      }

      public bool IsCritical()
      {
        for (int index = 0; index < this.hits.Count; ++index)
        {
          if (this.hits[index].is_critical)
            return true;
        }
        return false;
      }

      public bool IsAvoid()
      {
        if (this.hits.Count == 0)
          return false;
        for (int index = 0; index < this.hits.Count; ++index)
        {
          if (!this.hits[index].is_avoid)
            return false;
        }
        return true;
      }

      public bool IsCombo()
      {
        return this.hits.Count > 1;
      }

      public bool IsAvoidJustOne()
      {
        if (this.hits.Count == 0)
          return false;
        for (int index = 0; index < this.hits.Count; ++index)
        {
          if (this.hits[index].is_avoid)
            return true;
        }
        return false;
      }

      public void SetDefend(bool flag)
      {
        if (flag)
          this.hitType |= LogSkill.EHitTypes.Defend;
        else
          this.hitType &= ~LogSkill.EHitTypes.Defend;
      }

      public bool IsDefend()
      {
        return (LogSkill.EHitTypes) 0 < (this.hitType & LogSkill.EHitTypes.Defend);
      }

      public void SetPerfectAvoid(bool flag)
      {
        if (flag)
          this.hitType |= LogSkill.EHitTypes.PerfectAvoid;
        else
          this.hitType &= ~LogSkill.EHitTypes.PerfectAvoid;
      }

      public bool IsPerfectAvoid()
      {
        return (LogSkill.EHitTypes) 0 < (this.hitType & LogSkill.EHitTypes.PerfectAvoid);
      }

      public void SetForceReaction(bool flag)
      {
        this.is_force_reaction = flag;
      }

      public bool IsBuffEffect()
      {
        for (int index = 0; index < this.buff.bits.Length; ++index)
        {
          if (this.buff.bits[index] != 0)
            return true;
        }
        for (int index = 0; index < this.debuff.bits.Length; ++index)
        {
          if (this.debuff.bits[index] != 0)
            return true;
        }
        return false;
      }

      public bool IsFailCondition()
      {
        return (int) this.failCondition != 0;
      }

      public bool IsCureCondition()
      {
        return (int) this.cureCondition != 0;
      }

      public bool IsWeakEffectElement()
      {
        return this.element_effect_resist < 0;
      }

      public bool IsResistEffectElement()
      {
        return this.element_effect_resist > 0;
      }

      public bool IsNormalEffectElement()
      {
        return this.element_effect_resist == 0;
      }

      public class CondHit
      {
        public EUnitCondition Cond;
        public int Per;

        public CondHit(EUnitCondition cond, int per = 0)
        {
          this.Cond = cond;
          this.Per = per;
        }
      }
    }

    public class Reflection
    {
      public int damage;
    }
  }
}
