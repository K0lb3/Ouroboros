namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class SkillData
    {
        private SRPG.SkillParam mSkillParam;
        private OInt mRank;
        private OInt mRankCap;
        private OInt mCastSpeed;
        private OInt mEffectRate;
        private OInt mEffectValue;
        private OInt mEffectRange;
        private OInt mElementValue;
        private OInt mControlDamageRate;
        private OInt mControlDamageValue;
        private OInt mControlChargeTimeRate;
        private OInt mControlChargeTimeValue;
        private OInt mShieldTurn;
        private OInt mShieldValue;
        public BuffEffect mTargetBuffEffect;
        public CondEffect mTargetCondEffect;
        public BuffEffect mSelfBuffEffect;
        public CondEffect mSelfCondEffect;
        private OInt mUseRate;
        private SkillLockCondition mUseCondition;
        private bool mCheckCount;
        private OBool mIsCollabo;
        private string mReplaceSkillId;
        public string m_BaseSkillIname;
        private AbilityData m_OwnerAbility;
        private SkillDeriveParam m_SkillDeriveParam;

        public SkillData()
        {
            this.mRank = 1;
            this.mRankCap = 1;
            base..ctor();
            return;
        }

        public bool BuffSkill(ESkillTiming timing, EElement element, BaseStatus buff, BaseStatus buff_negative, BaseStatus buff_scale, BaseStatus debuff, BaseStatus debuff_negative, BaseStatus debuff_scale, RandXorshift rand, SkillEffectTargets buff_target, bool is_resume, List<BuffEffect.BuffValues> list)
        {
            BuffEffect effect;
            int num;
            int num2;
            if (this.Timing == timing)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            effect = this.GetBuffEffect(buff_target);
            if (effect != null)
            {
                goto Label_001F;
            }
            return 0;
        Label_001F:
            if (is_resume != null)
            {
                goto Label_006C;
            }
            num = effect.param.rate;
            if (num <= 0)
            {
                goto Label_006C;
            }
            if (num >= 100)
            {
                goto Label_006C;
            }
            DebugUtility.Assert((rand == null) == 0, "発動確率が設定されているスキルを正規タイミングで発動させたにも関わらず乱数生成器の設定がされていない");
            num2 = rand.Get() % 100;
            if (num2 <= num)
            {
                goto Label_006C;
            }
            return 0;
        Label_006C:
            if (list != null)
            {
                goto Label_00FA;
            }
            if (buff == null)
            {
                goto Label_0087;
            }
            this.InternalBuffSkill(effect, element, 0, 1, 0, 0, buff, null);
        Label_0087:
            if (buff_negative == null)
            {
                goto Label_009D;
            }
            this.InternalBuffSkill(effect, element, 0, 1, 1, 0, buff_negative, null);
        Label_009D:
            if (buff_scale == null)
            {
                goto Label_00B3;
            }
            this.InternalBuffSkill(effect, element, 0, 0, 0, 1, buff_scale, null);
        Label_00B3:
            if (debuff == null)
            {
                goto Label_00C9;
            }
            this.InternalBuffSkill(effect, element, 1, 1, 0, 0, debuff, null);
        Label_00C9:
            if (debuff_negative == null)
            {
                goto Label_00DF;
            }
            this.InternalBuffSkill(effect, element, 1, 1, 1, 0, debuff_negative, null);
        Label_00DF:
            if (debuff_scale == null)
            {
                goto Label_0136;
            }
            this.InternalBuffSkill(effect, element, 1, 0, 0, 1, debuff_scale, null);
            goto Label_0136;
        Label_00FA:
            this.InternalBuffSkill(effect, element, 0, 0, 0, 0, null, list);
            this.InternalBuffSkill(effect, element, 0, 0, 0, 1, null, list);
            this.InternalBuffSkill(effect, element, 1, 0, 0, 0, null, list);
            this.InternalBuffSkill(effect, element, 1, 0, 0, 1, null, list);
        Label_0136:
            return 1;
        }

        public int CalcBuffEffectValue(ParamTypes type, int src, SkillEffectTargets target)
        {
            BuffEffect effect;
            BuffEffect.BuffTarget target2;
            effect = this.GetBuffEffect(target);
            if (effect != null)
            {
                goto Label_0010;
            }
            return src;
        Label_0010:
            target2 = effect[type];
            if (target2 != null)
            {
                goto Label_0020;
            }
            return src;
        Label_0020:
            return SRPG.SkillParam.CalcSkillEffectValue(target2.calcType, target2.value, src);
        }

        public int CalcBuffEffectValue(ESkillTiming timing, ParamTypes type, int src, SkillEffectTargets target)
        {
            if (timing == this.Timing)
            {
                goto Label_000E;
            }
            return src;
        Label_000E:
            return this.CalcBuffEffectValue(type, src, target);
        }

        public bool CheckGridLineSkill()
        {
            SRPG.SkillParam param;
            param = this.SkillParam;
            if (param == null)
            {
                goto Label_0026;
            }
            if (param.line_type == null)
            {
                goto Label_0026;
            }
            if (this.RangeMax <= 1)
            {
                goto Label_0026;
            }
            return 1;
        Label_0026:
            return 0;
        }

        public bool CheckUnitSkillTarget()
        {
            ESkillTarget target;
            target = this.Target;
            if (target == null)
            {
                goto Label_0029;
            }
            if (target == 1)
            {
                goto Label_0029;
            }
            if (target == 2)
            {
                goto Label_0029;
            }
            if (target == 3)
            {
                goto Label_0029;
            }
            if (target != 4)
            {
                goto Label_002B;
            }
        Label_0029:
            return 1;
        Label_002B:
            return 0;
        }

        public SkillData CreateDeriveSkill(SkillDeriveParam skillDeriveParam)
        {
            SkillData data;
            data = new SkillData();
            data.Setup(skillDeriveParam.DeriveSkillIname, this.Rank, 1, null);
            data.m_OwnerAbility = this.m_OwnerAbility;
            data.m_BaseSkillIname = this.SkillID;
            data.m_SkillDeriveParam = skillDeriveParam;
            return data;
        }

        public BuffEffect GetBuffEffect(SkillEffectTargets target)
        {
            SkillEffectTargets targets;
            targets = target;
            if (targets == null)
            {
                goto Label_0014;
            }
            if (targets == 1)
            {
                goto Label_001B;
            }
            goto Label_0022;
        Label_0014:
            return this.mTargetBuffEffect;
        Label_001B:
            return this.mSelfBuffEffect;
        Label_0022:
            return null;
        }

        public int GetBuffEffectValue(ParamTypes type, SkillEffectTargets target)
        {
            BuffEffect effect;
            BuffEffect.BuffTarget target2;
            effect = this.GetBuffEffect(target);
            if (effect != null)
            {
                goto Label_0010;
            }
            return 0;
        Label_0010:
            target2 = effect[type];
            if (target2 != null)
            {
                goto Label_0020;
            }
            return 0;
        Label_0020:
            return target2.value;
        }

        private BuffMethodTypes GetBuffMethodType(BuffTypes buff, SkillParamCalcTypes calc)
        {
            if ((this.Timing != 1) && (calc == 1))
            {
                goto Label_0015;
            }
            return 0;
        Label_0015:
            return ((buff != null) ? 2 : 1);
        }

        public CondEffect GetCondEffect(SkillEffectTargets target)
        {
            SkillEffectTargets targets;
            targets = target;
            if (targets == null)
            {
                goto Label_0014;
            }
            if (targets == 1)
            {
                goto Label_001B;
            }
            goto Label_0022;
        Label_0014:
            return this.mTargetCondEffect;
        Label_001B:
            return this.mSelfCondEffect;
        Label_0022:
            return null;
        }

        public static void GetHomePassiveBuffStatus(SkillData skill, EElement element, ref BaseStatus status, ref BaseStatus scale_status)
        {
            GetHomePassiveBuffStatus(skill, element, status, status, status, status, scale_status);
            return;
        }

        public static void GetHomePassiveBuffStatus(SkillData skill, EElement element, ref BaseStatus status, ref BaseStatus negative_status, ref BaseStatus debuff_status, ref BaseStatus negative_debuff_status, ref BaseStatus scale_status)
        {
            BuffEffect effect;
            BuffEffect effect2;
            if (skill == null)
            {
                goto Label_0011;
            }
            if (skill.IsPassiveSkill() != null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            if (skill.Target == null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            if (skill.Condition == null)
            {
                goto Label_002A;
            }
            return;
        Label_002A:
            if (string.IsNullOrEmpty(skill.SkillParam.tokkou) != null)
            {
                goto Label_0040;
            }
            return;
        Label_0040:
            effect = skill.GetBuffEffect(0);
            if (effect == null)
            {
                goto Label_00BB;
            }
            if (effect.param == null)
            {
                goto Label_00BB;
            }
            if (effect.param.cond != null)
            {
                goto Label_00BB;
            }
            if (effect.param.mAppType != null)
            {
                goto Label_00BB;
            }
            if (effect.param.mEffRange != null)
            {
                goto Label_00BB;
            }
            if (effect.param.mIsUpBuff != null)
            {
                goto Label_00BB;
            }
            skill.BuffSkill(1, element, *(status), *(negative_status), *(scale_status), *(debuff_status), *(negative_debuff_status), *(scale_status), null, 0, 0, null);
        Label_00BB:
            effect2 = skill.GetBuffEffect(1);
            if (effect2 == null)
            {
                goto Label_0136;
            }
            if (effect2.param == null)
            {
                goto Label_0136;
            }
            if (effect2.param.cond != null)
            {
                goto Label_0136;
            }
            if (effect2.param.mAppType != null)
            {
                goto Label_0136;
            }
            if (effect2.param.mEffRange != null)
            {
                goto Label_0136;
            }
            if (effect2.param.mIsUpBuff != null)
            {
                goto Label_0136;
            }
            skill.BuffSkill(1, element, *(status), *(negative_status), *(scale_status), *(debuff_status), *(negative_debuff_status), *(scale_status), null, 1, 0, null);
        Label_0136:
            return;
        }

        public int GetHpCost(Unit self)
        {
            int num;
            int num2;
            num = this.SkillParam.hp_cost;
            num2 = num + (((num * self.CurrentStatus[0x17]) * 100) / 0x2710);
            return (((self.MaximumStatus.param.hp * num2) * 100) / 0x2710);
        }

        public static void GetPassiveBuffStatus(SkillData skill, BuffEffect[] add_buff_effects, EElement element, ref BaseStatus status, ref BaseStatus scale_status)
        {
            GetPassiveBuffStatus(skill, add_buff_effects, element, status, status, status, status, scale_status);
            return;
        }

        public static void GetPassiveBuffStatus(SkillData skill, BuffEffect[] add_buff_effects, EElement element, ref BaseStatus status, ref BaseStatus negative_status, ref BaseStatus debuff_status, ref BaseStatus negative_debuff_status, ref BaseStatus scale_status)
        {
            BuffEffect effect;
            BuffEffect effect2;
            if (skill == null)
            {
                goto Label_0011;
            }
            if (skill.IsPassiveSkill() != null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            effect = skill.GetBuffEffect(0);
            if (effect == null)
            {
                goto Label_007E;
            }
            if (effect.param == null)
            {
                goto Label_007E;
            }
            if (effect.param.mAppType != null)
            {
                goto Label_007E;
            }
            if (effect.param.mEffRange != null)
            {
                goto Label_007E;
            }
            if (effect.param.mIsUpBuff != null)
            {
                goto Label_007E;
            }
            skill.BuffSkill(1, element, *(status), *(negative_status), *(scale_status), *(debuff_status), *(negative_debuff_status), *(scale_status), null, 0, 0, null);
        Label_007E:
            effect2 = skill.GetBuffEffect(1);
            if (effect2 == null)
            {
                goto Label_00EA;
            }
            if (effect2.param == null)
            {
                goto Label_00EA;
            }
            if (effect2.param.mAppType != null)
            {
                goto Label_00EA;
            }
            if (effect2.param.mEffRange != null)
            {
                goto Label_00EA;
            }
            if (effect2.param.mIsUpBuff != null)
            {
                goto Label_00EA;
            }
            skill.BuffSkill(1, element, *(status), *(negative_status), *(scale_status), *(debuff_status), *(negative_debuff_status), *(scale_status), null, 1, 0, null);
        Label_00EA:
            return;
        }

        public int GetRankCap()
        {
            return this.mRankCap;
        }

        private unsafe void InternalBuffSkill(BuffEffect effect, EElement element, BuffTypes buffType, bool is_check_negative_value, bool is_negative_value_is_buff, SkillParamCalcTypes calcType, BaseStatus status, List<BuffEffect.BuffValues> list)
        {
            int num;
            BuffEffect.BuffTarget target;
            bool flag;
            BuffMethodTypes types;
            ParamTypes types2;
            int num2;
            bool flag2;
            int num3;
            BuffEffect.BuffValues values;
            BuffEffect.BuffValues values2;
            ParamTypes types3;
            num = 0;
            goto Label_0207;
        Label_0007:
            target = effect.targets[num];
            if (target != null)
            {
                goto Label_001F;
            }
            goto Label_0203;
        Label_001F:
            if (target.buffType == buffType)
            {
                goto Label_0030;
            }
            goto Label_0203;
        Label_0030:
            if (is_check_negative_value == null)
            {
                goto Label_004E;
            }
            if (BuffEffectParam.IsNegativeValueIsBuff(target.paramType) == is_negative_value_is_buff)
            {
                goto Label_004E;
            }
            goto Label_0203;
        Label_004E:
            if (target.calcType == calcType)
            {
                goto Label_0060;
            }
            goto Label_0203;
        Label_0060:
            if (element == null)
            {
                goto Label_0115;
            }
            flag = 0;
            types3 = target.paramType;
            switch ((types3 - 0x13))
            {
                case 0:
                    goto Label_00BC;

                case 1:
                    goto Label_00C9;

                case 2:
                    goto Label_00D6;

                case 3:
                    goto Label_00E3;

                case 4:
                    goto Label_00F0;

                case 5:
                    goto Label_00FD;
            }
            switch ((types3 - 0x8f))
            {
                case 0:
                    goto Label_00BC;

                case 1:
                    goto Label_00C9;

                case 2:
                    goto Label_00D6;

                case 3:
                    goto Label_00E3;

                case 4:
                    goto Label_00F0;

                case 5:
                    goto Label_00FD;
            }
            goto Label_010A;
        Label_00BC:
            flag = (element == 1) == 0;
            goto Label_010A;
        Label_00C9:
            flag = (element == 2) == 0;
            goto Label_010A;
        Label_00D6:
            flag = (element == 3) == 0;
            goto Label_010A;
        Label_00E3:
            flag = (element == 4) == 0;
            goto Label_010A;
        Label_00F0:
            flag = (element == 5) == 0;
            goto Label_010A;
        Label_00FD:
            flag = (element == 6) == 0;
        Label_010A:
            if (flag == null)
            {
                goto Label_0115;
            }
            goto Label_0203;
        Label_0115:
            types = this.GetBuffMethodType(target.buffType, calcType);
            types2 = target.paramType;
            num2 = target.value;
            if (effect.param.mIsUpBuff == null)
            {
                goto Label_0151;
            }
            num2 = 0;
        Label_0151:
            if (list != null)
            {
                goto Label_0169;
            }
            BuffEffect.SetBuffValues(types2, types, &status, num2);
            goto Label_0203;
        Label_0169:
            flag2 = 1;
            num3 = 0;
            goto Label_01C3;
        Label_0174:
            values = list[num3];
            if (&values.param_type != types2)
            {
                goto Label_01BD;
            }
            if (&values.method_type != types)
            {
                goto Label_01BD;
            }
            &values.value += num2;
            list[num3] = values;
            flag2 = 0;
            goto Label_01D1;
        Label_01BD:
            num3 += 1;
        Label_01C3:
            if (num3 < list.Count)
            {
                goto Label_0174;
            }
        Label_01D1:
            if (flag2 == null)
            {
                goto Label_0203;
            }
            values2 = new BuffEffect.BuffValues();
            &values2.param_type = types2;
            &values2.method_type = types;
            &values2.value = num2;
            list.Add(values2);
        Label_0203:
            num += 1;
        Label_0207:
            if (num < effect.targets.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public bool IsAdvantage()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsAdvantage());
        }

        public bool IsAllDamageReaction()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsAllDamageReaction());
        }

        public bool IsAllEffect()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsAllEffect());
        }

        public bool IsAreaSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsAreaSkill());
        }

        public bool IsBattleSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsBattleSkill());
        }

        public bool IsCastBreak()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsCastBreak());
        }

        public bool IsCastSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsCastSkill());
        }

        public bool IsChangeWeatherSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsChangeWeatherSkill());
        }

        public bool IsConditionSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsConditionSkill());
        }

        public bool IsCutin()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsCutin());
        }

        public bool IsDamagedSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsDamagedSkill());
        }

        public bool IsEnableChangeRange()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsEnableChangeRange());
        }

        public bool IsEnableHeightParamAdjust()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsEnableHeightParamAdjust());
        }

        public bool IsEnableHeightRangeBonus()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsEnableHeightRangeBonus());
        }

        public bool IsEnableUnitLockTarget()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsEnableUnitLockTarget());
        }

        public bool IsFixedDamage()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsFixedDamage());
        }

        public bool IsForceHit()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsForceHit());
        }

        public bool IsForceUnitLock()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsForceUnitLock());
        }

        public bool IsHealSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsHealSkill());
        }

        public bool IsIgnoreElement()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsIgnoreElement());
        }

        public bool IsItem()
        {
            return ((this.SkillParam == null) ? 0 : (this.SkillParam.type == 3));
        }

        public bool IsJewelAttack()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsJewelAttack());
        }

        public bool IsJudgeHp(Unit unit)
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsJudgeHp(unit));
        }

        public bool IsLongRangeSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsLongRangeSkill());
        }

        public bool IsMagicalAttack()
        {
            if (this.SkillParam == null)
            {
                goto Label_001E;
            }
            if (this.SkillParam.attack_type != 2)
            {
                goto Label_001E;
            }
            return 1;
        Label_001E:
            return 0;
        }

        public bool IsMapSkill()
        {
            return ((this.SkillParam == null) ? 1 : this.SkillParam.IsMapSkill());
        }

        public bool IsMhmDamage()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsMhmDamage());
        }

        public bool IsNormalAttack()
        {
            return ((this.SkillParam == null) ? 0 : (this.SkillParam.type == 0));
        }

        public bool IsPassiveSkill()
        {
            if (this.SkillParam != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (this.SkillType != 2)
            {
                goto Label_001B;
            }
            return 1;
        Label_001B:
            return ((this.SkillType != 3) ? 0 : (this.EffectType == 1));
        }

        public bool IsPhysicalAttack()
        {
            if (this.SkillParam == null)
            {
                goto Label_001E;
            }
            if (this.SkillParam.attack_type != 1)
            {
                goto Label_001E;
            }
            return 1;
        Label_001E:
            return 0;
        }

        public bool IsPierce()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsPierce());
        }

        public bool IsPrevApply()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsPrevApply());
        }

        public bool IsReactionDet(AttackDetailTypes atk_detail_type)
        {
            if (this.mSkillParam != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.mSkillParam.IsReactionDet(atk_detail_type);
        }

        public bool IsReactionSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsReactionSkill());
        }

        public bool IsSelfTargetSelect()
        {
            return this.SkillParam.IsSelfTargetSelect();
        }

        public bool IsSetBreakObjSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsSetBreakObjSkill());
        }

        public bool IsSubActuate()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsSubActuate());
        }

        public bool IsSuicide()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsSuicide());
        }

        public bool IsSupportSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsSupportSkill());
        }

        public bool IsTransformSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsTransformSkill());
        }

        public bool IsTrickSkill()
        {
            return ((this.SkillParam == null) ? 0 : this.SkillParam.IsTrickSkill());
        }

        public bool IsValid()
        {
            return ((this.mSkillParam == null) == 0);
        }

        private void Reset()
        {
            this.mSkillParam = null;
            this.mRank = 1;
            return;
        }

        public void SetOwnerAbility(AbilityData owner)
        {
            this.m_OwnerAbility = owner;
            return;
        }

        public void Setup(string iname, int rank, int rankcap, MasterParam master)
        {
            BuffEffectParam param;
            BuffEffectParam param2;
            CondEffectParam param3;
            CondEffectParam param4;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_0012;
            }
            this.Reset();
            return;
        Label_0012:
            if (master != null)
            {
                goto Label_0025;
            }
            master = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
        Label_0025:
            this.mSkillParam = master.GetSkillParam(iname);
            if (this.mSkillParam.lvcap != null)
            {
                goto Label_005F;
            }
            this.mRankCap = Math.Max(rankcap, 1);
            goto Label_0080;
        Label_005F:
            this.mRankCap = Math.Max(this.mSkillParam.lvcap, 1);
        Label_0080:
            this.mRank = Math.Min(rank, this.mRankCap);
            param = master.GetBuffEffectParam(this.SkillParam.target_buff_iname);
            this.mTargetBuffEffect = BuffEffect.CreateBuffEffect(param, this.mRank, this.mRankCap);
            param2 = master.GetBuffEffectParam(this.SkillParam.self_buff_iname);
            this.mSelfBuffEffect = BuffEffect.CreateBuffEffect(param2, this.mRank, this.mRankCap);
            param3 = master.GetCondEffectParam(this.SkillParam.target_cond_iname);
            this.mTargetCondEffect = CondEffect.CreateCondEffect(param3, this.mRank, this.mRankCap);
            param4 = master.GetCondEffectParam(this.SkillParam.self_cond_iname);
            this.mSelfCondEffect = CondEffect.CreateCondEffect(param4, this.mRank, this.mRankCap);
            this.UpdateParam();
            return;
        }

        public void UpdateParam()
        {
            int num;
            int num2;
            if (this.SkillParam == null)
            {
                goto Label_01FA;
            }
            num = this.Rank;
            num2 = this.GetRankCap();
            this.mCastSpeed = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.cast_speed);
            this.mEffectRate = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.effect_rate);
            this.mEffectValue = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.effect_value);
            this.mEffectRange = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.effect_range);
            this.mElementValue = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.element_value);
            this.mControlDamageRate = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.control_damage_rate);
            this.mControlDamageValue = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.control_damage_value);
            this.mControlChargeTimeRate = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.control_ct_rate);
            this.mControlChargeTimeValue = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.control_ct_value);
            this.mShieldTurn = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.shield_turn);
            this.mShieldValue = this.SkillParam.CalcCurrentRankValue(num, num2, this.SkillParam.shield_value);
            if (this.mTargetBuffEffect == null)
            {
                goto Label_01B2;
            }
            this.mTargetBuffEffect.UpdateCurrentValues(num, num2);
        Label_01B2:
            if (this.mTargetCondEffect == null)
            {
                goto Label_01CA;
            }
            this.mTargetCondEffect.UpdateCurrentValues(num, num2);
        Label_01CA:
            if (this.mSelfBuffEffect == null)
            {
                goto Label_01E2;
            }
            this.mSelfBuffEffect.UpdateCurrentValues(num, num2);
        Label_01E2:
            if (this.mSelfCondEffect == null)
            {
                goto Label_01FA;
            }
            this.mSelfCondEffect.UpdateCurrentValues(num, num2);
        Label_01FA:
            return;
        }

        public SRPG.SkillParam SkillParam
        {
            get
            {
                return this.mSkillParam;
            }
        }

        public string SkillID
        {
            get
            {
                return ((this.mSkillParam == null) ? null : this.mSkillParam.iname);
            }
        }

        public int Rank
        {
            get
            {
                return this.mRank;
            }
        }

        public string Name
        {
            get
            {
                return this.mSkillParam.name;
            }
        }

        public ESkillType SkillType
        {
            get
            {
                return this.mSkillParam.type;
            }
        }

        public ESkillTarget Target
        {
            get
            {
                return this.mSkillParam.target;
            }
        }

        public ESkillTiming Timing
        {
            get
            {
                return this.mSkillParam.timing;
            }
        }

        public ESkillCondition Condition
        {
            get
            {
                return this.mSkillParam.condition;
            }
        }

        public int Cost
        {
            get
            {
                return this.SkillParam.cost;
            }
        }

        public ELineType LineType
        {
            get
            {
                return this.mSkillParam.line_type;
            }
        }

        public int EnableAttackGridHeight
        {
            get
            {
                return this.mSkillParam.effect_height;
            }
        }

        public int RangeMin
        {
            get
            {
                return this.mSkillParam.range_min;
            }
        }

        public int RangeMax
        {
            get
            {
                return this.mSkillParam.range_max;
            }
        }

        public int Scope
        {
            get
            {
                return this.mSkillParam.scope;
            }
        }

        public int HpCostRate
        {
            get
            {
                return this.mSkillParam.hp_cost_rate;
            }
        }

        public ECastTypes CastType
        {
            get
            {
                return this.mSkillParam.cast_type;
            }
        }

        public OInt CastSpeed
        {
            get
            {
                return this.mCastSpeed;
            }
        }

        public SkillEffectTypes EffectType
        {
            get
            {
                return this.mSkillParam.effect_type;
            }
        }

        public OInt EffectRate
        {
            get
            {
                return this.mEffectRate;
            }
        }

        public OInt EffectValue
        {
            get
            {
                return this.mEffectValue;
            }
        }

        public OInt EffectRange
        {
            get
            {
                return this.mEffectRange;
            }
        }

        public OInt EffectHpMaxRate
        {
            get
            {
                return this.mSkillParam.effect_hprate;
            }
        }

        public OInt EffectGemsMaxRate
        {
            get
            {
                return this.mSkillParam.effect_mprate;
            }
        }

        public SkillParamCalcTypes EffectCalcType
        {
            get
            {
                return this.mSkillParam.effect_calc;
            }
        }

        public EElement ElementType
        {
            get
            {
                return this.mSkillParam.element_type;
            }
        }

        public OInt ElementValue
        {
            get
            {
                return this.mElementValue;
            }
        }

        public AttackTypes AttackType
        {
            get
            {
                return this.mSkillParam.attack_type;
            }
        }

        public AttackDetailTypes AttackDetailType
        {
            get
            {
                return this.mSkillParam.attack_detail;
            }
        }

        public int BackAttackDefenseDownRate
        {
            get
            {
                return this.mSkillParam.back_defrate;
            }
        }

        public int SideAttackDefenseDownRate
        {
            get
            {
                return this.mSkillParam.side_defrate;
            }
        }

        public DamageTypes ReactionDamageType
        {
            get
            {
                return this.mSkillParam.reaction_damage_type;
            }
        }

        public int DamageAbsorbRate
        {
            get
            {
                return this.mSkillParam.absorb_damage_rate;
            }
        }

        public OInt ControlDamageRate
        {
            get
            {
                return this.mControlDamageRate;
            }
        }

        public OInt ControlDamageValue
        {
            get
            {
                return this.mControlDamageValue;
            }
        }

        public SkillParamCalcTypes ControlDamageCalcType
        {
            get
            {
                return this.mSkillParam.control_damage_calc;
            }
        }

        public OInt ControlChargeTimeRate
        {
            get
            {
                return this.mControlChargeTimeRate;
            }
        }

        public OInt ControlChargeTimeValue
        {
            get
            {
                return this.mControlChargeTimeValue;
            }
        }

        public SkillParamCalcTypes ControlChargeTimeCalcType
        {
            get
            {
                return this.mSkillParam.control_ct_calc;
            }
        }

        public ShieldTypes ShieldType
        {
            get
            {
                return this.mSkillParam.shield_type;
            }
        }

        public DamageTypes ShieldDamageType
        {
            get
            {
                return this.mSkillParam.shield_damage_type;
            }
        }

        public OInt ShieldTurn
        {
            get
            {
                return this.mShieldTurn;
            }
        }

        public OInt ShieldValue
        {
            get
            {
                return this.mShieldValue;
            }
        }

        public OInt UseRate
        {
            get
            {
                return this.mUseRate;
            }
            set
            {
                this.mUseRate = value;
                return;
            }
        }

        public SkillLockCondition UseCondition
        {
            get
            {
                return this.mUseCondition;
            }
            set
            {
                if (value == null)
                {
                    goto Label_0028;
                }
                if (this.mUseCondition != null)
                {
                    goto Label_001C;
                }
                this.mUseCondition = new SkillLockCondition();
            Label_001C:
                value.CopyTo(this.mUseCondition);
            Label_0028:
                return;
            }
        }

        public bool CheckCount
        {
            get
            {
                return this.mCheckCount;
            }
            set
            {
                this.mCheckCount = value;
                return;
            }
        }

        public int DuplicateCount
        {
            get
            {
                return this.mSkillParam.DuplicateCount;
            }
        }

        public OBool IsCollabo
        {
            get
            {
                return this.mIsCollabo;
            }
            set
            {
                this.mIsCollabo = value;
                return;
            }
        }

        public string ReplaceSkillId
        {
            get
            {
                return this.mReplaceSkillId;
            }
            set
            {
                this.mReplaceSkillId = value;
                return;
            }
        }

        public eTeleportType TeleportType
        {
            get
            {
                return this.mSkillParam.TeleportType;
            }
        }

        public ESkillTarget TeleportTarget
        {
            get
            {
                return this.mSkillParam.TeleportTarget;
            }
        }

        public int TeleportHeight
        {
            get
            {
                return this.mSkillParam.TeleportHeight;
            }
        }

        public bool TeleportIsMove
        {
            get
            {
                return this.mSkillParam.TeleportIsMove;
            }
        }

        public OInt KnockBackRate
        {
            get
            {
                return this.mSkillParam.KnockBackRate;
            }
        }

        public OInt KnockBackVal
        {
            get
            {
                return this.mSkillParam.KnockBackVal;
            }
        }

        public eKnockBackDir KnockBackDir
        {
            get
            {
                return this.mSkillParam.KnockBackDir;
            }
        }

        public eKnockBackDs KnockBackDs
        {
            get
            {
                return this.mSkillParam.KnockBackDs;
            }
        }

        public int WeatherRate
        {
            get
            {
                return this.mSkillParam.WeatherRate;
            }
        }

        public string WeatherId
        {
            get
            {
                return this.mSkillParam.WeatherId;
            }
        }

        public int ElementSpcAtkRate
        {
            get
            {
                return this.mSkillParam.ElementSpcAtkRate;
            }
        }

        public int MaxDamageValue
        {
            get
            {
                return this.mSkillParam.MaxDamageValue;
            }
        }

        public string CutInConceptCardId
        {
            get
            {
                return this.mSkillParam.CutInConceptCardId;
            }
        }

        public int JumpSpcAtkRate
        {
            get
            {
                return this.mSkillParam.JumpSpcAtkRate;
            }
        }

        public AbilityData OwnerAbiliy
        {
            get
            {
                return this.m_OwnerAbility;
            }
        }

        public bool IsDerivedSkill
        {
            get
            {
                return (string.IsNullOrEmpty(this.m_BaseSkillIname) == 0);
            }
        }

        public SkillDeriveParam DeriveParam
        {
            get
            {
                return this.m_SkillDeriveParam;
            }
        }

        public bool IsTargetGridNoUnit
        {
            get
            {
                return ((this.SkillParam == null) ? 0 : this.SkillParam.IsTargetGridNoUnit);
            }
        }

        public bool IsTargetValidGrid
        {
            get
            {
                return ((this.SkillParam == null) ? 0 : this.SkillParam.IsTargetValidGrid);
            }
        }

        public bool IsSkillCountNoLimit
        {
            get
            {
                return ((this.SkillParam == null) ? 0 : this.SkillParam.IsSkillCountNoLimit);
            }
        }

        public bool IsTargetTeleport
        {
            get
            {
                return ((this.SkillParam == null) ? 0 : this.SkillParam.IsTargetTeleport);
            }
        }
    }
}

