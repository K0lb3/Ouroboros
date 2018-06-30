namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class WeatherData
    {
        private SRPG.WeatherParam mWeatherParam;
        private List<BuffEffect> mBuffEffectLists;
        private List<CondEffect> mCondEffectLists;
        private Unit mModifyUnit;
        private OInt mRank;
        private OInt mRankCap;
        private OInt mChangeClock;
        private static WeatherSetParam mCurrentWeatherSet;
        private static bool mIsAllowWeatherChange;
        private static WeatherData mCurrentWeatherData;
        private static bool mIsEntryConditionLog;
        private static bool mIsExecuteUpdate;

        public WeatherData()
        {
            this.mBuffEffectLists = new List<BuffEffect>();
            this.mCondEffectLists = new List<CondEffect>();
            this.mRank = 1;
            this.mRankCap = 1;
            base..ctor();
            return;
        }

        private unsafe bool attachBuffPassive(Unit target)
        {
            bool flag;
            BuffEffect effect;
            List<BuffEffect>.Enumerator enumerator;
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            BaseStatus status5;
            BaseStatus status6;
            BuffAttachment attachment;
            BuffAttachment attachment2;
            BuffAttachment attachment3;
            BuffAttachment attachment4;
            BuffAttachment attachment5;
            BuffAttachment attachment6;
            if (target != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            flag = 0;
            enumerator = this.mBuffEffectLists.GetEnumerator();
        Label_0016:
            try
            {
                goto Label_01D9;
            Label_001B:
                effect = &enumerator.Current;
                if (effect != null)
                {
                    goto Label_002E;
                }
                goto Label_01D9;
            Label_002E:
                if (effect.param.chk_timing == 1)
                {
                    goto Label_0044;
                }
                goto Label_01D9;
            Label_0044:
                if (effect.CheckEnableBuffTarget(target) != null)
                {
                    goto Label_0055;
                }
                goto Label_01D9;
            Label_0055:
                status = new BaseStatus();
                status2 = new BaseStatus();
                status3 = new BaseStatus();
                status4 = new BaseStatus();
                status5 = new BaseStatus();
                status6 = new BaseStatus();
                effect.CalcBuffStatus(&status, target.Element, 0, 1, 0, 0, 0);
                effect.CalcBuffStatus(&status2, target.Element, 0, 1, 1, 0, 0);
                effect.CalcBuffStatus(&status3, target.Element, 0, 0, 0, 1, 0);
                effect.CalcBuffStatus(&status4, target.Element, 1, 1, 0, 0, 0);
                effect.CalcBuffStatus(&status5, target.Element, 1, 1, 1, 0, 0);
                effect.CalcBuffStatus(&status6, target.Element, 1, 0, 0, 1, 0);
                if (effect.CheckBuffCalcType(0, 0, 0) == null)
                {
                    goto Label_0116;
                }
                attachment = this.createBuffAttachment(target, effect, 0, 0, 0, status);
                target.SetBuffAttachment(attachment, 0);
            Label_0116:
                if (effect.CheckBuffCalcType(0, 0, 1) == null)
                {
                    goto Label_013D;
                }
                attachment2 = this.createBuffAttachment(target, effect, 0, 1, 0, status2);
                target.SetBuffAttachment(attachment2, 0);
            Label_013D:
                if (effect.CheckBuffCalcType(0, 1) == null)
                {
                    goto Label_0163;
                }
                attachment3 = this.createBuffAttachment(target, effect, 0, 0, 1, status3);
                target.SetBuffAttachment(attachment3, 0);
            Label_0163:
                if (effect.CheckBuffCalcType(1, 0, 0) == null)
                {
                    goto Label_018A;
                }
                attachment4 = this.createBuffAttachment(target, effect, 1, 0, 0, status4);
                target.SetBuffAttachment(attachment4, 0);
            Label_018A:
                if (effect.CheckBuffCalcType(1, 0, 1) == null)
                {
                    goto Label_01B1;
                }
                attachment5 = this.createBuffAttachment(target, effect, 1, 1, 0, status5);
                target.SetBuffAttachment(attachment5, 0);
            Label_01B1:
                if (effect.CheckBuffCalcType(1, 1) == null)
                {
                    goto Label_01D7;
                }
                attachment6 = this.createBuffAttachment(target, effect, 1, 0, 1, status6);
                target.SetBuffAttachment(attachment6, 0);
            Label_01D7:
                flag = 1;
            Label_01D9:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001B;
                }
                goto Label_01F6;
            }
            finally
            {
            Label_01EA:
                ((List<BuffEffect>.Enumerator) enumerator).Dispose();
            }
        Label_01F6:
            return flag;
        }

        private unsafe bool attachCond(eCondAttachType cond_at_type, Unit target, RandXorshift rand)
        {
            bool flag;
            CondEffect effect;
            List<CondEffect>.Enumerator enumerator;
            ConditionEffectTypes types;
            int num;
            EUnitCondition condition;
            EnchantParam param;
            int num2;
            EUnitCondition condition2;
            EnchantParam param2;
            int num3;
            EUnitCondition condition3;
            int num4;
            EUnitCondition condition4;
            int num5;
            CondAttachment attachment;
            CondAttachment attachment2;
            ConditionEffectTypes types2;
            flag = 0;
            enumerator = this.mCondEffectLists.GetEnumerator();
        Label_000E:
            try
            {
                goto Label_02DE;
            Label_0013:
                effect = &enumerator.Current;
                if ((effect == null) || (effect.param == null))
                {
                    goto Label_02DE;
                }
                if (effect.param.conditions != null)
                {
                    goto Label_0041;
                }
                goto Label_02DE;
            Label_0041:
                if (cond_at_type != null)
                {
                    goto Label_0062;
                }
                if (effect.param.chk_timing == 1)
                {
                    goto Label_007F;
                }
                goto Label_02DE;
                goto Label_007F;
            Label_0062:
                if ((cond_at_type != 1) || (effect.param.chk_timing != 1))
                {
                    goto Label_007F;
                }
                goto Label_02DE;
            Label_007F:
                if (effect.CheckEnableCondTarget(target) != null)
                {
                    goto Label_0090;
                }
                goto Label_02DE;
            Label_0090:
                types = effect.param.type;
                types2 = types;
                switch ((types2 - 1))
                {
                    case 0:
                        goto Label_00C1;

                    case 1:
                        goto Label_0101;

                    case 2:
                        goto Label_022C;

                    case 3:
                        goto Label_0195;

                    case 4:
                        goto Label_026E;
                }
                goto Label_02DC;
            Label_00C1:
                num = 0;
                goto Label_00E8;
            Label_00C9:
                condition = effect.param.conditions[num];
                this.cureCond(target, condition);
                num += 1;
            Label_00E8:
                if (num < ((int) effect.param.conditions.Length))
                {
                    goto Label_00C9;
                }
                goto Label_02DC;
            Label_0101:
                if (effect.value == null)
                {
                    goto Label_02DC;
                }
                param = target.CurrentStatus.enchant_resist;
                num2 = 0;
                goto Label_017C;
            Label_0126:
                condition2 = effect.param.conditions[num2];
                if ((target.IsDisableUnitCondition(condition2) != null) || (this.checkFailCond(target, effect.value, param[condition2], condition2, rand) == null))
                {
                    goto Label_0176;
                }
                this.failCond(target, effect, types, condition2);
            Label_0176:
                num2 += 1;
            Label_017C:
                if (num2 < ((int) effect.param.conditions.Length))
                {
                    goto Label_0126;
                }
                goto Label_02DC;
            Label_0195:
                if (effect.value == null)
                {
                    goto Label_02DC;
                }
                param2 = target.CurrentStatus.enchant_resist;
                num3 = (rand == null) ? 0 : ((int) (((ulong) rand.Get()) % ((long) ((int) effect.param.conditions.Length))));
                condition3 = effect.param.conditions[num3];
                if (target.IsDisableUnitCondition(condition3) != null)
                {
                    goto Label_02DC;
                }
                if (this.checkFailCond(target, effect.value, param2[condition3], condition3, rand) == null)
                {
                    goto Label_02DC;
                }
                this.failCond(target, effect, types, condition3);
                goto Label_02DC;
            Label_022C:
                num4 = 0;
                goto Label_0255;
            Label_0234:
                condition4 = effect.param.conditions[num4];
                this.failCond(target, effect, types, condition4);
                num4 += 1;
            Label_0255:
                if (num4 < ((int) effect.param.conditions.Length))
                {
                    goto Label_0234;
                }
                goto Label_02DC;
            Label_026E:
                num5 = 0;
                goto Label_02C3;
            Label_0276:
                attachment = this.createCondAttachment(target, effect, types, effect.param.conditions[num5]);
                attachment2 = this.getSameCondAttachment(target, attachment);
                if (attachment2 == null)
                {
                    goto Label_02B4;
                }
                attachment2.turn = attachment.turn;
                goto Label_02BD;
            Label_02B4:
                target.SetCondAttachment(attachment);
            Label_02BD:
                num5 += 1;
            Label_02C3:
                if (num5 < ((int) effect.param.conditions.Length))
                {
                    goto Label_0276;
                }
            Label_02DC:
                flag = 1;
            Label_02DE:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0013;
                }
                goto Label_02FB;
            }
            finally
            {
            Label_02EF:
                ((List<CondEffect>.Enumerator) enumerator).Dispose();
            }
        Label_02FB:
            return flag;
        }

        public static unsafe WeatherData ChangeWeather(string iname, List<Unit> units, int now_clock, RandXorshift rand, Unit modify_unit, int rank, int rankcap)
        {
            WeatherData data;
            SceneBattle battle;
            BattleCore core;
            LogWeather weather;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (mCurrentWeatherData == null)
            {
                goto Label_0033;
            }
            if ((mCurrentWeatherData.WeatherParam.Iname == iname) == null)
            {
                goto Label_0033;
            }
            return null;
        Label_0033:
            data = new WeatherData();
            data.setup(iname, modify_unit, rank, rankcap);
            if (data.mWeatherParam != null)
            {
                goto Label_0053;
            }
            return null;
        Label_0053:
            if (mCurrentWeatherSet == null)
            {
                goto Label_0074;
            }
            data.mChangeClock = mCurrentWeatherSet.GetNextChangeClock(now_clock, rand);
        Label_0074:
            mCurrentWeatherData = data;
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_00AC;
            }
            core = battle.Battle;
            if (core == null)
            {
                goto Label_00AC;
            }
            weather = core.Log<LogWeather>();
            if (weather == null)
            {
                goto Label_00AC;
            }
            weather.WeatherData = data;
        Label_00AC:
            if (units == null)
            {
                goto Label_00FF;
            }
            enumerator = units.GetEnumerator();
        Label_00BA:
            try
            {
                goto Label_00E1;
            Label_00BF:
                unit = &enumerator.Current;
                if (unit.IsGimmick == null)
                {
                    goto Label_00D9;
                }
                goto Label_00E1;
            Label_00D9:
                data.updatePassive(unit);
            Label_00E1:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00BF;
                }
                goto Label_00FF;
            }
            finally
            {
            Label_00F2:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_00FF:
            return data;
        }

        private bool checkFailCond(Unit target, int val, int resist, EUnitCondition condition, RandXorshift rand)
        {
            int num;
            int num2;
            if (val > 0)
            {
                goto Label_0009;
            }
            return 0;
        Label_0009:
            num = val - resist;
            if (num <= 0)
            {
                goto Label_002D;
            }
            num2 = 0;
            if (rand == null)
            {
                goto Label_0028;
            }
            num2 = rand.Get() % 100;
        Label_0028:
            return (num > num2);
        Label_002D:
            return 0;
        }

        private BuffAttachment createBuffAttachment(Unit target, BuffEffect effect, BuffTypes buff_type, bool is_negative_value_is_buff, SkillParamCalcTypes calc_type, BaseStatus status)
        {
            BuffAttachment attachment;
            if (effect != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            attachment = new BuffAttachment(effect.param);
            attachment.user = this.mModifyUnit;
            attachment.skill = null;
            attachment.skilltarget = 1;
            attachment.IsPassive = 1;
            attachment.CheckTarget = null;
            attachment.DuplicateCount = 0;
            attachment.CheckTiming = effect.param.chk_timing;
            attachment.turn = effect.param.turn;
            attachment.BuffType = buff_type;
            attachment.IsNegativeValueIsBuff = is_negative_value_is_buff;
            attachment.CalcType = calc_type;
            attachment.UseCondition = 3;
            status.CopyTo(attachment.status);
            return attachment;
        }

        private CondAttachment createCondAttachment(Unit target, CondEffect effect, ConditionEffectTypes type, EUnitCondition condition)
        {
            CondAttachment attachment;
            int num;
            int num2;
            if (type != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            if (effect != null)
            {
                goto Label_0010;
            }
            return null;
        Label_0010:
            attachment = new CondAttachment(effect.param);
            attachment.user = null;
            attachment.skill = null;
            attachment.skilltarget = 1;
            attachment.CondId = effect.param.iname;
            attachment.IsPassive = effect.param.chk_timing == 1;
            attachment.UseCondition = 3;
            attachment.CondType = type;
            attachment.Condition = condition;
            num2 = effect.turn;
            if (num2 >= 2)
            {
                goto Label_0086;
            }
            num2 = 2;
        Label_0086:
            attachment.turn = num2;
            attachment.CheckTiming = effect.param.chk_timing;
            attachment.CheckTarget = target;
            if (attachment.IsFailCondition() == null)
            {
                goto Label_00C1;
            }
            attachment.IsCurse = effect.IsCurse;
        Label_00C1:
            attachment.SetupLinkageBuff();
            return attachment;
        }

        private void cureCond(Unit target, EUnitCondition condition)
        {
            target.CureCondEffects(condition, 1, 0);
            return;
        }

        private void detachPassive(Unit target)
        {
            int num;
            BuffAttachment attachment;
            int num2;
            CondAttachment attachment2;
            if (target != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            goto Label_0050;
        Label_000E:
            attachment = target.BuffAttachments[num];
            if (attachment.UseCondition != 3)
            {
                goto Label_004C;
            }
            if (attachment.IsPassive != null)
            {
                goto Label_003C;
            }
            goto Label_004C;
        Label_003C:
            target.BuffAttachments.RemoveAt(num--);
        Label_004C:
            num += 1;
        Label_0050:
            if (num < target.BuffAttachments.Count)
            {
                goto Label_000E;
            }
            num2 = 0;
            goto Label_00AA;
        Label_0068:
            attachment2 = target.CondAttachments[num2];
            if (attachment2.UseCondition != 3)
            {
                goto Label_00A6;
            }
            if (attachment2.IsPassive != null)
            {
                goto Label_0096;
            }
            goto Label_00A6;
        Label_0096:
            target.CondAttachments.RemoveAt(num2--);
        Label_00A6:
            num2 += 1;
        Label_00AA:
            if (num2 < target.CondAttachments.Count)
            {
                goto Label_0068;
            }
            return;
        }

        private void failCond(Unit target, CondEffect effect, ConditionEffectTypes effect_type, EUnitCondition condition)
        {
            CondAttachment attachment;
            CondAttachment attachment2;
            SceneBattle battle;
            BattleCore core;
            LogFailCondition condition2;
            TacticsUnitController controller;
            attachment = this.createCondAttachment(target, effect, effect_type, condition);
            attachment2 = this.getSameCondAttachment(target, attachment);
            if (attachment2 == null)
            {
                goto Label_0028;
            }
            attachment2.turn = attachment.turn;
            return;
        Label_0028:
            target.SetCondAttachment(attachment);
            if (mIsEntryConditionLog == null)
            {
                goto Label_0098;
            }
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_0098;
            }
            core = battle.Battle;
            if (core == null)
            {
                goto Label_0098;
            }
            condition2 = core.Log<LogFailCondition>();
            condition2.self = target;
            condition2.source = null;
            condition2.condition = condition;
            controller = battle.FindUnitController(target);
            if (controller == null)
            {
                goto Label_0098;
            }
            controller.LockUpdateBadStatus(condition, 0);
        Label_0098:
            return;
        }

        public int GetResistRate(int now_clock)
        {
            return 0;
        }

        private CondAttachment getSameCondAttachment(Unit target, CondAttachment new_cond)
        {
            int num;
            CondAttachment attachment;
            if (target == null)
            {
                goto Label_000C;
            }
            if (new_cond != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return null;
        Label_000E:
            num = 0;
            goto Label_0071;
        Label_0015:
            attachment = target.CondAttachments[num];
            if (attachment.UseCondition == 3)
            {
                goto Label_0033;
            }
            goto Label_006D;
        Label_0033:
            if (attachment.CondType != new_cond.CondType)
            {
                goto Label_006D;
            }
            if (attachment.Condition != new_cond.Condition)
            {
                goto Label_006D;
            }
            if (attachment.CheckTiming == new_cond.CheckTiming)
            {
                goto Label_006B;
            }
            goto Label_006D;
        Label_006B:
            return attachment;
        Label_006D:
            num += 1;
        Label_0071:
            if (num < target.CondAttachments.Count)
            {
                goto Label_0015;
            }
            return null;
        }

        public static void Initialize(WeatherSetParam weather_set, bool is_allow_weather_change)
        {
            mCurrentWeatherSet = weather_set;
            mIsAllowWeatherChange = is_allow_weather_change;
            mCurrentWeatherData = null;
            mIsEntryConditionLog = 1;
            IsExecuteUpdate = 1;
            return;
        }

        private unsafe void setup(string iname, Unit modify_unit, int rank, int rankcap)
        {
            GameManager manager;
            string str;
            List<string>.Enumerator enumerator;
            BuffEffectParam param;
            BuffEffect effect;
            string str2;
            List<string>.Enumerator enumerator2;
            CondEffectParam param2;
            CondEffect effect2;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if (manager != null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            this.mWeatherParam = manager.MasterParam.GetWeatherParam(iname);
            if (this.mWeatherParam != null)
            {
                goto Label_003C;
            }
            return;
        Label_003C:
            this.mRankCap = Math.Max(rankcap, 1);
            this.mRank = Math.Min(rank, this.mRankCap);
            this.mBuffEffectLists.Clear();
            enumerator = this.mWeatherParam.BuffIdLists.GetEnumerator();
        Label_0087:
            try
            {
                goto Label_00C5;
            Label_008C:
                str = &enumerator.Current;
                effect = BuffEffect.CreateBuffEffect(manager.MasterParam.GetBuffEffectParam(str), rank, rankcap);
                if (effect != null)
                {
                    goto Label_00B8;
                }
                goto Label_00C5;
            Label_00B8:
                this.mBuffEffectLists.Add(effect);
            Label_00C5:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_008C;
                }
                goto Label_00E2;
            }
            finally
            {
            Label_00D6:
                ((List<string>.Enumerator) enumerator).Dispose();
            }
        Label_00E2:
            this.mCondEffectLists.Clear();
            enumerator2 = this.mWeatherParam.CondIdLists.GetEnumerator();
        Label_00FF:
            try
            {
                goto Label_0141;
            Label_0104:
                str2 = &enumerator2.Current;
                effect2 = CondEffect.CreateCondEffect(manager.MasterParam.GetCondEffectParam(str2), rank, rankcap);
                if (effect2 != null)
                {
                    goto Label_0134;
                }
                goto Label_0141;
            Label_0134:
                this.mCondEffectLists.Add(effect2);
            Label_0141:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0104;
                }
                goto Label_015F;
            }
            finally
            {
            Label_0152:
                ((List<string>.Enumerator) enumerator2).Dispose();
            }
        Label_015F:
            this.mModifyUnit = modify_unit;
            return;
        }

        public static unsafe void SuspendWeather(string iname, List<Unit> units, Unit modify_unit, int rank, int rankcap, int change_clock)
        {
            WeatherData data;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            data = new WeatherData();
            data.setup(iname, modify_unit, rank, rankcap);
            if (data.mWeatherParam != null)
            {
                goto Label_0029;
            }
            return;
        Label_0029:
            data.mChangeClock = change_clock;
            if (units == null)
            {
                goto Label_0084;
            }
            enumerator = units.GetEnumerator();
        Label_0043:
            try
            {
                goto Label_0067;
            Label_0048:
                unit = &enumerator.Current;
                if (unit.IsGimmick == null)
                {
                    goto Label_0060;
                }
                goto Label_0067;
            Label_0060:
                data.updatePassive(unit);
            Label_0067:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0048;
                }
                goto Label_0084;
            }
            finally
            {
            Label_0078:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0084:
            mCurrentWeatherData = data;
            return;
        }

        private void updatePassive(Unit target)
        {
            if (target != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.detachPassive(target);
            this.attachBuffPassive(target);
            this.attachCond(0, target, null);
            target.CalcCurrentStatus(0, 0);
            return;
        }

        public static unsafe bool UpdateWeather(List<Unit> units, int now_clock, RandXorshift rand)
        {
            bool flag;
            WeatherSetParam param;
            string str;
            int num;
            WeatherData data;
            WeatherData data2;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            if (mIsExecuteUpdate != null)
            {
                goto Label_000C;
            }
            return 0;
        Label_000C:
            flag = 0;
            param = mCurrentWeatherSet;
            if (param == null)
            {
                goto Label_009E;
            }
            str = null;
            if (mCurrentWeatherData != null)
            {
                goto Label_0040;
            }
            str = param.GetStartWeather(rand);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_007C;
            }
            return 0;
            goto Label_007C;
        Label_0040:
            num = mCurrentWeatherData.mChangeClock;
            if (num == null)
            {
                goto Label_007C;
            }
            if (now_clock < num)
            {
                goto Label_007C;
            }
            mCurrentWeatherData.mChangeClock = param.GetNextChangeClock(now_clock, rand);
            str = param.GetChangeWeather(rand);
        Label_007C:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_009E;
            }
            if (ChangeWeather(str, units, now_clock, rand, null, 1, 1) == null)
            {
                goto Label_009E;
            }
            flag = 1;
        Label_009E:
            if (units == null)
            {
                goto Label_011E;
            }
            if (mCurrentWeatherData == null)
            {
                goto Label_011E;
            }
            data2 = mCurrentWeatherData;
            enumerator = units.GetEnumerator();
        Label_00BD:
            try
            {
                goto Label_0100;
            Label_00C2:
                unit = &enumerator.Current;
                if (unit.IsGimmick == null)
                {
                    goto Label_00DC;
                }
                goto Label_0100;
            Label_00DC:
                if (unit.IsEntry == null)
                {
                    goto Label_0100;
                }
                if (unit.IsSub != null)
                {
                    goto Label_0100;
                }
                data2.attachCond(1, unit, rand);
            Label_0100:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00C2;
                }
                goto Label_011E;
            }
            finally
            {
            Label_0111:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_011E:
            return flag;
        }

        public SRPG.WeatherParam WeatherParam
        {
            get
            {
                return this.mWeatherParam;
            }
        }

        public List<BuffEffect> BuffEffectLists
        {
            get
            {
                return this.mBuffEffectLists;
            }
        }

        public List<CondEffect> CondEffectLists
        {
            get
            {
                return this.mCondEffectLists;
            }
        }

        public Unit ModifyUnit
        {
            get
            {
                return this.mModifyUnit;
            }
        }

        public OInt Rank
        {
            get
            {
                return this.mRank;
            }
        }

        public OInt RankCap
        {
            get
            {
                return this.mRankCap;
            }
        }

        public OInt ChangeClock
        {
            get
            {
                return this.mChangeClock;
            }
        }

        public static WeatherSetParam CurrentWeatherSet
        {
            get
            {
                return mCurrentWeatherSet;
            }
        }

        public static bool IsAllowWeatherChange
        {
            get
            {
                return mIsAllowWeatherChange;
            }
        }

        public static WeatherData CurrentWeatherData
        {
            get
            {
                return mCurrentWeatherData;
            }
        }

        public static bool IsEntryConditionLog
        {
            get
            {
                return mIsEntryConditionLog;
            }
            set
            {
                mIsEntryConditionLog = value;
                return;
            }
        }

        public static bool IsExecuteUpdate
        {
            get
            {
                return mIsExecuteUpdate;
            }
            set
            {
                mIsExecuteUpdate = value;
                return;
            }
        }

        private enum eCondAttachType
        {
            PASSIVE,
            TURN
        }
    }
}

