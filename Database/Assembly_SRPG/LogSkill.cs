namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class LogSkill : BattleLog
    {
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
        public Target self_effect;
        public List<Target> targets;
        public Reflection reflect;
        public Unit CauseOfReaction;
        public bool is_append;
        public bool is_gimmick;

        public LogSkill()
        {
            this.self_effect = new Target();
            this.targets = new List<Target>(BattleCore.MAX_UNITS);
            base..ctor();
            return;
        }

        public unsafe void CheckAliveTarget()
        {
            List<Target> list;
            Target target;
            List<Target>.Enumerator enumerator;
            list = new List<Target>(BattleCore.MAX_UNITS);
            enumerator = this.targets.GetEnumerator();
        Label_0017:
            try
            {
                goto Label_0040;
            Label_001C:
                target = &enumerator.Current;
                if (target.target.IsDead == null)
                {
                    goto Label_0039;
                }
                goto Label_0040;
            Label_0039:
                list.Add(target);
            Label_0040:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001C;
                }
                goto Label_005D;
            }
            finally
            {
            Label_0051:
                ((List<Target>.Enumerator) enumerator).Dispose();
            }
        Label_005D:
            if (this.targets.Count == list.Count)
            {
                goto Label_007A;
            }
            this.targets = list;
        Label_007A:
            return;
        }

        public Target FindTarget(Unit target)
        {
            int num;
            num = 0;
            goto Label_002F;
        Label_0007:
            if (this.targets[num].target != target)
            {
                goto Label_002B;
            }
            return this.targets[num];
        Label_002B:
            num += 1;
        Label_002F:
            if (num < this.targets.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public int GetGainJewel()
        {
            int num;
            int num2;
            Target target;
            num = 0;
            num2 = 0;
            goto Label_0047;
        Label_0009:
            target = this.targets[num2];
            if (target.gems != null)
            {
                goto Label_0026;
            }
            goto Label_0043;
        Label_0026:
            if (target.IsAvoid() == null)
            {
                goto Label_0036;
            }
            goto Label_0043;
        Label_0036:
            num = Math.Max(num, target.gems);
        Label_0043:
            num2 += 1;
        Label_0047:
            if (num2 < this.targets.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        public unsafe void GetTotalBuffEffect(out int buff_count, out int buff_value)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            num = 0;
            num2 = 0;
            num3 = 0;
            goto Label_0045;
        Label_000B:
            num4 = 0;
            num5 = 0;
            this.targets[num3].target.GetEnableBetterBuffEffect(this.self, this.skill, 0, &num4, &num5, 1);
            num += num4;
            num2 += num5;
            num3 += 1;
        Label_0045:
            if (num3 < this.targets.Count)
            {
                goto Label_000B;
            }
            if (num != null)
            {
                goto Label_007F;
            }
            if (num2 != null)
            {
                goto Label_007F;
            }
            this.self.GetEnableBetterBuffEffect(this.self, this.skill, 1, &num, &num2, 1);
        Label_007F:
            *((int*) buff_count) = num;
            *((int*) buff_value) = num2;
            return;
        }

        public int GetTotalCureConditionCount()
        {
            CondEffect effect;
            int num;
            int num2;
            int num3;
            EUnitCondition condition;
            effect = this.skill.GetCondEffect(0);
            if (effect == null)
            {
                goto Label_002E;
            }
            if (effect.param == null)
            {
                goto Label_002E;
            }
            if (effect.param.conditions != null)
            {
                goto Label_0030;
            }
        Label_002E:
            return 0;
        Label_0030:
            if (effect.param.type == 1)
            {
                goto Label_0043;
            }
            return 0;
        Label_0043:
            num = 0;
            num2 = 0;
            goto Label_00C0;
        Label_004C:
            num3 = 0;
            goto Label_00A9;
        Label_0053:
            condition = effect.param.conditions[num3];
            if (this.targets[num2].target.IsUnitCondition(condition) == null)
            {
                goto Label_00A5;
            }
            if (this.targets[num2].target.IsPassiveUnitCondition(condition) == null)
            {
                goto Label_00A1;
            }
            goto Label_00A5;
        Label_00A1:
            num += 1;
        Label_00A5:
            num3 += 1;
        Label_00A9:
            if (num3 < ((int) effect.param.conditions.Length))
            {
                goto Label_0053;
            }
            num2 += 1;
        Label_00C0:
            if (num2 < this.targets.Count)
            {
                goto Label_004C;
            }
            return num;
        }

        public int GetTotalDeathCount()
        {
            int num;
            int num2;
            int num3;
            num = 0;
            num2 = 0;
            goto Label_0053;
        Label_0009:
            if (this.targets[num2].target.CurrentStatus.param.hp <= this.targets[num2].GetTotalHpDamage())
            {
                goto Label_004B;
            }
            goto Label_004F;
        Label_004B:
            num += 1;
        Label_004F:
            num2 += 1;
        Label_0053:
            if (num2 < this.targets.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        public int GetTotalDisableConditionCount()
        {
            CondEffect effect;
            int num;
            int num2;
            Unit unit;
            int num3;
            EUnitCondition condition;
            effect = this.skill.GetCondEffect(0);
            if (effect == null)
            {
                goto Label_002E;
            }
            if (effect.param == null)
            {
                goto Label_002E;
            }
            if (effect.param.conditions != null)
            {
                goto Label_0030;
            }
        Label_002E:
            return 0;
        Label_0030:
            if (effect.param.type == 5)
            {
                goto Label_0043;
            }
            return 0;
        Label_0043:
            num = 0;
            num2 = 0;
            goto Label_00D9;
        Label_004C:
            unit = this.targets[num2].target;
            num3 = 0;
            goto Label_00C1;
        Label_0066:
            condition = effect.param.conditions[num3];
            if (condition != 0x4000L)
            {
                goto Label_0099;
            }
            if (unit.CheckActionSkillBuffAttachments(0) != null)
            {
                goto Label_00B7;
            }
            goto Label_00BB;
            goto Label_00B7;
        Label_0099:
            if (condition != 0x8000L)
            {
                goto Label_00B7;
            }
            if (unit.CheckActionSkillBuffAttachments(1) != null)
            {
                goto Label_00B7;
            }
            goto Label_00BB;
        Label_00B7:
            num += 1;
        Label_00BB:
            num3 += 1;
        Label_00C1:
            if (num3 < ((int) effect.param.conditions.Length))
            {
                goto Label_0066;
            }
            num2 += 1;
        Label_00D9:
            if (num2 < this.targets.Count)
            {
                goto Label_004C;
            }
            return num;
        }

        public int GetTotalFailConditionCount()
        {
            CondEffect effect;
            int num;
            int num2;
            int num3;
            int num4;
            Unit unit;
            EUnitCondition condition;
            int num5;
            effect = this.skill.GetCondEffect(0);
            if (effect == null)
            {
                goto Label_002E;
            }
            if (effect.param == null)
            {
                goto Label_002E;
            }
            if (effect.param.conditions != null)
            {
                goto Label_0030;
            }
        Label_002E:
            return 0;
        Label_0030:
            if (effect.param.type == 2)
            {
                goto Label_0065;
            }
            if (effect.param.type == 3)
            {
                goto Label_0065;
            }
            if (effect.param.type == 4)
            {
                goto Label_0065;
            }
            return 0;
        Label_0065:
            num = 0;
            num2 = 0;
            if (this.self.AI == null)
            {
                goto Label_008F;
            }
            num2 = this.self.AI.cond_border;
        Label_008F:
            if (num2 <= 0)
            {
                goto Label_00BA;
            }
            if (effect.rate <= 0)
            {
                goto Label_00BA;
            }
            if (effect.rate >= num2)
            {
                goto Label_00BA;
            }
            return 0;
        Label_00BA:
            num3 = 0;
            goto Label_0174;
        Label_00C1:
            num4 = 0;
            goto Label_015C;
        Label_00C9:
            unit = this.targets[num3].target;
            condition = effect.param.conditions[num4];
            if (unit.IsUnitCondition(condition) == null)
            {
                goto Label_00FF;
            }
            goto Label_0156;
        Label_00FF:
            if (unit.IsDisableUnitCondition(condition) == null)
            {
                goto Label_0112;
            }
            goto Label_0156;
        Label_0112:
            if (num2 <= 0)
            {
                goto Label_0152;
            }
            if (Math.Max(effect.value - unit.CurrentStatus.enchant_resist[condition], 0) >= num2)
            {
                goto Label_0152;
            }
            goto Label_0156;
        Label_0152:
            num += 1;
        Label_0156:
            num4 += 1;
        Label_015C:
            if (num4 < ((int) effect.param.conditions.Length))
            {
                goto Label_00C9;
            }
            num3 += 1;
        Label_0174:
            if (num3 < this.targets.Count)
            {
                goto Label_00C1;
            }
            return num;
        }

        public int GetTruthTotalHpDamage()
        {
            int num;
            int num2;
            int num3;
            num = 0;
            num2 = 0;
            goto Label_0053;
        Label_0009:
            num3 = Math.Max(Math.Min(this.targets[num2].GetTotalHpDamage(), this.targets[num2].target.CurrentStatus.param.hp), 0);
            num += num3;
            num2 += 1;
        Label_0053:
            if (num2 < this.targets.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        public int GetTruthTotalHpHeal()
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = 0;
            num2 = 0;
            goto Label_007B;
        Label_0009:
            num4 = Math.Max(Math.Min(this.targets[num2].GetTotalHpHeal(), this.targets[num2].target.MaximumStatus.param.hp - this.targets[num2].target.CurrentStatus.param.hp), 0);
            num += num4;
            num2 += 1;
        Label_007B:
            if (num2 < this.targets.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        public int GetTruthTotalHpHealCount()
        {
            int num;
            int num2;
            num = 0;
            num2 = 0;
            goto Label_0080;
        Label_0009:
            if (this.targets[num2].target.CurrentStatus.param.hp != this.targets[num2].target.MaximumStatus.param.hp)
            {
                goto Label_005D;
            }
            goto Label_007C;
        Label_005D:
            if (this.targets[num2].GetTotalHpHeal() != null)
            {
                goto Label_0078;
            }
            goto Label_007C;
        Label_0078:
            num += 1;
        Label_007C:
            num2 += 1;
        Label_0080:
            if (num2 < this.targets.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        public void Hit(Unit unit, Unit other, int hp_damage, int mp_damage, int ch_damage, int ca_damage, int hp_heal, int mp_heal, int ch_heal, int ca_heal, int dropgems, bool is_critical, bool is_avoid, bool is_combination, bool is_guts, int absorbed, bool is_pf_avoid, int critical_rate, int avoid_rate)
        {
            Target target;
            target = this.SetSkillTarget(unit, other);
            if (unit.IsUnitFlag(0x40) == null)
            {
                goto Label_0034;
            }
            if (this.skill.BackAttackDefenseDownRate == null)
            {
                goto Label_0034;
            }
            target.hitType |= 1;
        Label_0034:
            if (unit.IsUnitFlag(0x20) == null)
            {
                goto Label_005F;
            }
            if (this.skill.SideAttackDefenseDownRate == null)
            {
                goto Label_005F;
            }
            target.hitType |= 2;
        Label_005F:
            if (is_guts == null)
            {
                goto Label_0075;
            }
            target.hitType |= 0x20;
        Label_0075:
            if (is_combination == null)
            {
                goto Label_008B;
            }
            target.hitType |= 0x40;
        Label_008B:
            target.shieldDamage += absorbed;
            target.gems += dropgems;
            target.hits.Add(new BattleCore.HitData(hp_damage, mp_damage, ch_damage, ca_damage, hp_heal, mp_heal, ch_heal, ca_heal, is_critical, is_avoid, is_pf_avoid, critical_rate, avoid_rate));
            return;
        }

        public bool IsRenkei()
        {
            int num;
            num = this.targets.Count - 1;
            goto Label_0032;
        Label_0013:
            if ((this.targets[num].hitType & 0x40) == null)
            {
                goto Label_002E;
            }
            return 1;
        Label_002E:
            num -= 1;
        Label_0032:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return 0;
        }

        public void SetDefendEffect(Unit defender)
        {
            Target target;
            target = this.FindTarget(defender);
            if (target == null)
            {
                goto Label_0020;
            }
            target.hitType |= 0x80;
        Label_0020:
            return;
        }

        public Target SetSkillTarget(Unit unit, Unit other)
        {
            Target target;
            target = this.FindTarget(other);
            if (target != null)
            {
                goto Label_0027;
            }
            target = new Target();
            target.target = other;
            this.targets.Add(target);
        Label_0027:
            this.self = unit;
            return target;
        }

        public void ToSelfSkillEffect(int hp_damage, int mp_damage, int ch_damage, int ca_damage, int hp_heal, int mp_heal, int ch_heal, int ca_heal, int dropgems, bool is_critical, bool is_avoid, bool is_combination, bool is_guts)
        {
            this.self_effect.target = this.self;
            this.self_effect.hits.Add(new BattleCore.HitData(hp_damage, mp_damage, ch_damage, ca_damage, hp_heal, mp_heal, ch_heal, ca_heal, is_critical, is_avoid, 0, 0, 0));
            return;
        }

        [Flags]
        public enum EHitTypes
        {
            BackAttack = 1,
            SideAttack = 2,
            ItemSteal = 4,
            GoldSteal = 8,
            GemsSteal = 0x10,
            Guts = 0x20,
            Combination = 0x40,
            Defend = 0x80,
            CastBreak = 0x100,
            PerfectAvoid = 0x200
        }

        public class Reflection
        {
            public int damage;

            public Reflection()
            {
                base..ctor();
                return;
            }
        }

        public class Target
        {
            public Unit target;
            public List<BattleCore.HitData> hits;
            public LogSkill.EHitTypes hitType;
            public int gems;
            public SkillData defSkill;
            public int defSkillUseCount;
            public int shieldDamage;
            public bool isProcShield;
            public BuffBit buff;
            public BuffBit debuff;
            public EUnitCondition failCondition;
            public EUnitCondition cureCondition;
            public Unit guard;
            public bool is_force_reaction;
            public int element_effect_rate;
            public int element_effect_resist;
            public Grid KnockBackGrid;
            public int ChangeValueCT;
            public bool IsOldDying;
            public List<CondHit> CondHitLists;

            public Target()
            {
                this.hits = new List<BattleCore.HitData>();
                this.buff = new BuffBit();
                this.debuff = new BuffBit();
                this.CondHitLists = new List<CondHit>();
                base..ctor();
                return;
            }

            public int GetTotalAvoidRate()
            {
                int num;
                int num2;
                int num3;
                num = 0;
                num2 = 0;
                num3 = 0;
                goto Label_0027;
            Label_000B:
                num += this.hits[num3].avoid_rate;
                num2 += 1;
                num3 += 1;
            Label_0027:
                if (num3 < this.hits.Count)
                {
                    goto Label_000B;
                }
                if (num2 == null)
                {
                    goto Label_0042;
                }
                num /= num2;
            Label_0042:
                return num;
            }

            public int GetTotalCriticalRate()
            {
                int num;
                int num2;
                int num3;
                num = 0;
                num2 = 0;
                num3 = 0;
                goto Label_0027;
            Label_000B:
                num += this.hits[num3].critical_rate;
                num2 += 1;
                num3 += 1;
            Label_0027:
                if (num3 < this.hits.Count)
                {
                    goto Label_000B;
                }
                if (num2 == null)
                {
                    goto Label_0042;
                }
                num /= num2;
            Label_0042:
                return num;
            }

            public int GetTotalHpDamage()
            {
                int num;
                int num2;
                num = 0;
                num2 = 0;
                goto Label_0021;
            Label_0009:
                num += this.hits[num2].hp_damage;
                num2 += 1;
            Label_0021:
                if (num2 < this.hits.Count)
                {
                    goto Label_0009;
                }
                return num;
            }

            public int GetTotalHpHeal()
            {
                int num;
                int num2;
                num = 0;
                num2 = 0;
                goto Label_0021;
            Label_0009:
                num += this.hits[num2].hp_heal;
                num2 += 1;
            Label_0021:
                if (num2 < this.hits.Count)
                {
                    goto Label_0009;
                }
                return num;
            }

            public int GetTotalMpDamage()
            {
                int num;
                int num2;
                num = 0;
                num2 = 0;
                goto Label_0021;
            Label_0009:
                num += this.hits[num2].mp_damage;
                num2 += 1;
            Label_0021:
                if (num2 < this.hits.Count)
                {
                    goto Label_0009;
                }
                return num;
            }

            public int GetTotalMpHeal()
            {
                int num;
                int num2;
                num = 0;
                num2 = 0;
                goto Label_0021;
            Label_0009:
                num += this.hits[num2].mp_heal;
                num2 += 1;
            Label_0021:
                if (num2 < this.hits.Count)
                {
                    goto Label_0009;
                }
                return num;
            }

            public bool IsAvoid()
            {
                int num;
                if (this.hits.Count != null)
                {
                    goto Label_0012;
                }
                return 0;
            Label_0012:
                num = 0;
                goto Label_0035;
            Label_0019:
                if (this.hits[num].is_avoid != null)
                {
                    goto Label_0031;
                }
                return 0;
            Label_0031:
                num += 1;
            Label_0035:
                if (num < this.hits.Count)
                {
                    goto Label_0019;
                }
                return 1;
            }

            public bool IsAvoidJustOne()
            {
                int num;
                if (this.hits.Count != null)
                {
                    goto Label_0012;
                }
                return 0;
            Label_0012:
                num = 0;
                goto Label_0035;
            Label_0019:
                if (this.hits[num].is_avoid == null)
                {
                    goto Label_0031;
                }
                return 1;
            Label_0031:
                num += 1;
            Label_0035:
                if (num < this.hits.Count)
                {
                    goto Label_0019;
                }
                return 0;
            }

            public bool IsBuffEffect()
            {
                int num;
                int num2;
                num = 0;
                goto Label_001F;
            Label_0007:
                if (this.buff.bits[num] == null)
                {
                    goto Label_001B;
                }
                return 1;
            Label_001B:
                num += 1;
            Label_001F:
                if (num < ((int) this.buff.bits.Length))
                {
                    goto Label_0007;
                }
                num2 = 0;
                goto Label_0051;
            Label_0039:
                if (this.debuff.bits[num2] == null)
                {
                    goto Label_004D;
                }
                return 1;
            Label_004D:
                num2 += 1;
            Label_0051:
                if (num2 < ((int) this.debuff.bits.Length))
                {
                    goto Label_0039;
                }
                return 0;
            }

            public bool IsCombo()
            {
                return (this.hits.Count > 1);
            }

            public bool IsCritical()
            {
                int num;
                num = 0;
                goto Label_0023;
            Label_0007:
                if (this.hits[num].is_critical == null)
                {
                    goto Label_001F;
                }
                return 1;
            Label_001F:
                num += 1;
            Label_0023:
                if (num < this.hits.Count)
                {
                    goto Label_0007;
                }
                return 0;
            }

            public bool IsCureCondition()
            {
                return ((((int) this.cureCondition) == 0) == 0);
            }

            public bool IsDefend()
            {
                return (0 < (this.hitType & 0x80));
            }

            public bool IsFailCondition()
            {
                return ((((int) this.failCondition) == 0) == 0);
            }

            public bool IsNormalEffectElement()
            {
                return (this.element_effect_resist == 0);
            }

            public bool IsPerfectAvoid()
            {
                return (0 < (this.hitType & 0x200));
            }

            public bool IsResistEffectElement()
            {
                return (this.element_effect_resist > 0);
            }

            public bool IsWeakEffectElement()
            {
                return (this.element_effect_resist < 0);
            }

            public void SetDefend(bool flag)
            {
                if (flag == null)
                {
                    goto Label_001D;
                }
                this.hitType |= 0x80;
                goto Label_002F;
            Label_001D:
                this.hitType &= -129;
            Label_002F:
                return;
            }

            public void SetForceReaction(bool flag)
            {
                this.is_force_reaction = flag;
                return;
            }

            public void SetPerfectAvoid(bool flag)
            {
                if (flag == null)
                {
                    goto Label_001D;
                }
                this.hitType |= 0x200;
                goto Label_002F;
            Label_001D:
                this.hitType &= -513;
            Label_002F:
                return;
            }

            public class CondHit
            {
                public EUnitCondition Cond;
                public int Per;

                public CondHit(EUnitCondition cond, int per)
                {
                    base..ctor();
                    this.Cond = cond;
                    this.Per = per;
                    return;
                }
            }
        }
    }
}

