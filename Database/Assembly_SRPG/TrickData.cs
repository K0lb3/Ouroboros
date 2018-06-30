namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class TrickData
    {
        private SRPG.TrickParam mTrickParam;
        private SRPG.BuffEffect mBuffEffect;
        private SRPG.CondEffect mCondEffect;
        private OBool mValid;
        private Unit mCreateUnit;
        private OInt mRank;
        private OInt mRankCap;
        private OInt mGridX;
        private OInt mGridY;
        private OInt mRestActionCount;
        private OInt mCreateClock;
        private string mTag;
        private static List<TrickData> mTrickDataLists;
        private static Dictionary<TrickData, GameObject> mTrickMarkerLists;
        private EUnitDirection[] reverseDirection;

        static TrickData()
        {
            mTrickDataLists = new List<TrickData>();
            mTrickMarkerLists = new Dictionary<TrickData, GameObject>();
            return;
        }

        public TrickData()
        {
            EUnitDirection[] directionArray1;
            this.mValid = 0;
            this.mRank = 1;
            this.mRankCap = 1;
            directionArray1 = new EUnitDirection[5];
            directionArray1[0] = 2;
            directionArray1[1] = 3;
            directionArray1[3] = 1;
            this.reverseDirection = directionArray1;
            base..ctor();
            return;
        }

        private unsafe bool actionBuff(Unit target, EffectCheckTimings chk_timing, RandXorshift rand)
        {
            int num;
            int num2;
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
            if (this.mBuffEffect != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (target != null)
            {
                goto Label_0015;
            }
            return 0;
        Label_0015:
            if (this.mBuffEffect.CheckEnableBuffTarget(target) != null)
            {
                goto Label_0028;
            }
            return 0;
        Label_0028:
            if (rand == null)
            {
                goto Label_0066;
            }
            num = this.mBuffEffect.param.rate;
            if (0 >= num)
            {
                goto Label_0066;
            }
            if (num >= 100)
            {
                goto Label_0066;
            }
            num2 = rand.Get() % 100;
            if (num2 <= num)
            {
                goto Label_0066;
            }
            return 1;
        Label_0066:
            status = new BaseStatus();
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            status5 = new BaseStatus();
            status6 = new BaseStatus();
            this.mBuffEffect.CalcBuffStatus(&status, target.Element, 0, 1, 0, 0, 0);
            this.mBuffEffect.CalcBuffStatus(&status2, target.Element, 0, 1, 1, 0, 0);
            this.mBuffEffect.CalcBuffStatus(&status3, target.Element, 0, 0, 0, 1, 0);
            this.mBuffEffect.CalcBuffStatus(&status4, target.Element, 1, 1, 0, 0, 0);
            this.mBuffEffect.CalcBuffStatus(&status5, target.Element, 1, 1, 1, 0, 0);
            this.mBuffEffect.CalcBuffStatus(&status6, target.Element, 1, 0, 0, 1, 0);
            if (this.mBuffEffect.CheckBuffCalcType(0, 0, 0) == null)
            {
                goto Label_0149;
            }
            attachment = this.createBuffAttachment(target, 0, 0, 0, status, chk_timing);
            target.SetBuffAttachment(attachment, 0);
        Label_0149:
            if (this.mBuffEffect.CheckBuffCalcType(0, 0, 1) == null)
            {
                goto Label_0174;
            }
            attachment2 = this.createBuffAttachment(target, 0, 1, 0, status2, chk_timing);
            target.SetBuffAttachment(attachment2, 0);
        Label_0174:
            if (this.mBuffEffect.CheckBuffCalcType(0, 1) == null)
            {
                goto Label_019F;
            }
            attachment3 = this.createBuffAttachment(target, 0, 0, 1, status3, chk_timing);
            target.SetBuffAttachment(attachment3, 0);
        Label_019F:
            if (this.mBuffEffect.CheckBuffCalcType(1, 0, 0) == null)
            {
                goto Label_01CB;
            }
            attachment4 = this.createBuffAttachment(target, 1, 0, 0, status4, chk_timing);
            target.SetBuffAttachment(attachment4, 0);
        Label_01CB:
            if (this.mBuffEffect.CheckBuffCalcType(1, 0, 1) == null)
            {
                goto Label_01F7;
            }
            attachment5 = this.createBuffAttachment(target, 1, 1, 0, status5, chk_timing);
            target.SetBuffAttachment(attachment5, 0);
        Label_01F7:
            if (this.mBuffEffect.CheckBuffCalcType(1, 1) == null)
            {
                goto Label_0222;
            }
            attachment6 = this.createBuffAttachment(target, 1, 0, 1, status6, chk_timing);
            target.SetBuffAttachment(attachment6, 0);
        Label_0222:
            return 1;
        }

        private bool actionCond(Unit target, RandXorshift rand, LogMapTrick.TargetInfo log_mt_ti)
        {
            SRPG.CondEffect effect;
            ConditionEffectTypes types;
            int num;
            int num2;
            int num3;
            EUnitCondition condition;
            EnchantParam param;
            int num4;
            EUnitCondition condition2;
            EnchantParam param2;
            int num5;
            EUnitCondition condition3;
            int num6;
            EUnitCondition condition4;
            int num7;
            CondAttachment attachment;
            ConditionEffectTypes types2;
            effect = this.mCondEffect;
            if (rand == null)
            {
                goto Label_002E;
            }
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
            types = 0;
            if (effect.CheckEnableCondTarget(target) != null)
            {
                goto Label_0040;
            }
            return 1;
        Label_0040:
            if (effect.param.type == null)
            {
                goto Label_009A;
            }
            if (effect.param.conditions == null)
            {
                goto Label_009A;
            }
            num = effect.rate;
            if (0 >= num)
            {
                goto Label_008E;
            }
            if (num >= 100)
            {
                goto Label_008E;
            }
            num2 = rand.Get() % 100;
            if (num2 <= num)
            {
                goto Label_008E;
            }
            return 1;
        Label_008E:
            types = effect.param.type;
        Label_009A:
            types2 = types;
            switch ((types2 - 1))
            {
                case 0:
                    goto Label_00BF;

                case 1:
                    goto Label_0100;

                case 2:
                    goto Label_0221;

                case 3:
                    goto Label_0195;

                case 4:
                    goto Label_0264;
            }
            goto Label_02AD;
        Label_00BF:
            num3 = 0;
            goto Label_00E7;
        Label_00C7:
            condition = effect.param.conditions[num3];
            this.cureCond(target, condition, log_mt_ti);
            num3 += 1;
        Label_00E7:
            if (num3 < ((int) effect.param.conditions.Length))
            {
                goto Label_00C7;
            }
            goto Label_02AD;
        Label_0100:
            if (effect.value == null)
            {
                goto Label_02AD;
            }
            param = target.CurrentStatus.enchant_resist;
            num4 = 0;
            goto Label_017C;
        Label_0125:
            condition2 = effect.param.conditions[num4];
            if (target.IsDisableUnitCondition(condition2) != null)
            {
                goto Label_0176;
            }
            if (this.checkFailCond(target, effect.value, param[condition2], condition2, rand) == null)
            {
                goto Label_0176;
            }
            this.failCond(target, effect, types, condition2, log_mt_ti);
        Label_0176:
            num4 += 1;
        Label_017C:
            if (num4 < ((int) effect.param.conditions.Length))
            {
                goto Label_0125;
            }
            goto Label_02AD;
        Label_0195:
            if (effect.value == null)
            {
                goto Label_02AD;
            }
            param2 = target.CurrentStatus.enchant_resist;
            num5 = (int) (((ulong) rand.Get()) % ((long) ((int) effect.param.conditions.Length)));
            condition3 = effect.param.conditions[num5];
            if (target.IsDisableUnitCondition(condition3) != null)
            {
                goto Label_02AD;
            }
            if (this.checkFailCond(target, effect.value, param2[condition3], condition3, rand) == null)
            {
                goto Label_02AD;
            }
            this.failCond(target, effect, types, condition3, log_mt_ti);
            goto Label_02AD;
        Label_0221:
            num6 = 0;
            goto Label_024B;
        Label_0229:
            condition4 = effect.param.conditions[num6];
            this.failCond(target, effect, types, condition4, log_mt_ti);
            num6 += 1;
        Label_024B:
            if (num6 < ((int) effect.param.conditions.Length))
            {
                goto Label_0229;
            }
            goto Label_02AD;
        Label_0264:
            num7 = 0;
            goto Label_0294;
        Label_026C:
            attachment = this.createCondAttachment(target, effect, types, effect.param.conditions[num7]);
            target.SetCondAttachment(attachment);
            num7 += 1;
        Label_0294:
            if (num7 < ((int) effect.param.conditions.Length))
            {
                goto Label_026C;
            }
        Label_02AD:
            return 1;
        }

        private bool actionDamage(Unit target, LogMapTrick.TargetInfo log_mt_ti)
        {
            int num;
            num = this.calcDamage(target);
            if (num <= 0)
            {
                goto Label_002D;
            }
            target.Damage(num, 1);
            if (log_mt_ti == null)
            {
                goto Label_002B;
            }
            log_mt_ti.IsEffective = 1;
            log_mt_ti.Damage = num;
        Label_002B:
            return 1;
        Label_002D:
            return 0;
        }

        public static void ActionEffect(eTrickActionTiming action_timing, Unit target, int grid_x, int grid_y, RandXorshift rand, LogMapTrick log_mt)
        {
            TrickData data;
            List<Unit> list;
            eTrickActionTiming timing;
            if (target != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            data = SearchEffect(grid_x, grid_y);
            if (data != null)
            {
                goto Label_0016;
            }
            return;
        Label_0016:
            if (data.checkTarget(target, 0) != null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            if (target.IsJump == null)
            {
                goto Label_0030;
            }
            return;
        Label_0030:
            if (log_mt == null)
            {
                goto Label_004B;
            }
            log_mt.TrickData = data;
            log_mt.TargetInfoLists.Clear();
        Label_004B:
            list = new List<Unit>();
            if (data.checkTarget(target, 1) == null)
            {
                goto Label_0065;
            }
            list.Add(target);
        Label_0065:
            addTargetAreaEff(grid_x, grid_y, data, list);
            timing = action_timing;
            if (timing == 1)
            {
                goto Label_0083;
            }
            if (timing == 2)
            {
                goto Label_0092;
            }
            goto Label_00A9;
        Label_0083:
            data.actionEffectTurnStart(list, rand);
            goto Label_00A9;
        Label_0092:
            data.actionEffectTurnEnd(list, rand, log_mt);
            data.decActionCount();
        Label_00A9:
            return;
        }

        private unsafe bool actionEffectTurnEnd(List<Unit> target_lists, RandXorshift rand, LogMapTrick log_mt)
        {
            bool flag;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            LogMapTrick.TargetInfo info;
            eTrickDamageType type;
            if (target_lists != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            flag = 0;
            enumerator = target_lists.GetEnumerator();
        Label_0011:
            try
            {
                goto Label_009F;
            Label_0016:
                unit = &enumerator.Current;
                info = null;
                if (log_mt == null)
                {
                    goto Label_0033;
                }
                info = new LogMapTrick.TargetInfo();
                info.Target = unit;
            Label_0033:
                type = this.mTrickParam.DamageType;
                if (type == 1)
                {
                    goto Label_0065;
                }
                if (type == 2)
                {
                    goto Label_0055;
                }
                goto Label_0075;
            Label_0055:
                flag |= this.actionHeal(unit, info);
                goto Label_0075;
            Label_0065:
                flag |= this.actionDamage(unit, info);
            Label_0075:
                flag |= this.actionCond(unit, rand, info);
                flag |= this.actionKnockBack(unit, rand, info);
                if (log_mt == null)
                {
                    goto Label_009F;
                }
                log_mt.TargetInfoLists.Add(info);
            Label_009F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0016;
                }
                goto Label_00BC;
            }
            finally
            {
            Label_00B0:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_00BC:
            return flag;
        }

        private unsafe bool actionEffectTurnStart(List<Unit> target_lists, RandXorshift rand)
        {
            bool flag;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            if (target_lists != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            flag = 0;
            enumerator = target_lists.GetEnumerator();
        Label_0011:
            try
            {
                goto Label_002B;
            Label_0016:
                unit = &enumerator.Current;
                flag |= this.actionBuff(unit, 9, rand);
            Label_002B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0016;
                }
                goto Label_0048;
            }
            finally
            {
            Label_003C:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0048:
            return flag;
        }

        private bool actionHeal(Unit target, LogMapTrick.TargetInfo log_mt_ti)
        {
            int num;
            num = 0;
            if (target.IsUnitCondition(0x1000000L) != null)
            {
                goto Label_0023;
            }
            num = this.calcHeal(target);
            num = target.CalcParamRecover(num);
        Label_0023:
            if (num < 0)
            {
                goto Label_0047;
            }
            target.Heal(num);
            if (log_mt_ti == null)
            {
                goto Label_0045;
            }
            log_mt_ti.IsEffective = 1;
            log_mt_ti.Heal = num;
        Label_0045:
            return 1;
        Label_0047:
            return 0;
        }

        private bool actionKnockBack(Unit target, RandXorshift rand, LogMapTrick.TargetInfo log_mt_ti)
        {
            SceneBattle battle;
            BattleCore core;
            EUnitDirection direction;
            Grid grid;
            if (rand == null)
            {
                goto Label_0030;
            }
            if (this.mTrickParam.KnockBackRate == null)
            {
                goto Label_0030;
            }
            if (this.mTrickParam.KnockBackVal != null)
            {
                goto Label_0032;
            }
        Label_0030:
            return 0;
        Label_0032:
            battle = SceneBattle.Instance;
            if (battle != null)
            {
                goto Label_0045;
            }
            return 0;
        Label_0045:
            core = battle.Battle;
            if (core != null)
            {
                goto Label_0054;
            }
            return 0;
        Label_0054:
            if (this.checkKnockBack(target, rand) != null)
            {
                goto Label_0063;
            }
            return 1;
        Label_0063:
            direction = this.reverseDirection[target.Direction];
            grid = core.GetGridKnockBack(target, direction, this.mTrickParam.KnockBackVal, null, 0, 0);
            if (grid != null)
            {
                goto Label_0095;
            }
            return 1;
        Label_0095:
            if (log_mt_ti == null)
            {
                goto Label_00A9;
            }
            log_mt_ti.IsEffective = 1;
            log_mt_ti.KnockBackGrid = grid;
        Label_00A9:
            target.x = grid.x;
            target.y = grid.y;
            return 1;
        }

        public static unsafe void AddMarker()
        {
            SceneBattle battle;
            BattleCore core;
            TrickData data;
            List<TrickData>.Enumerator enumerator;
            Dictionary<TrickData, GameObject> dictionary;
            KeyValuePair<TrickData, GameObject> pair;
            Dictionary<TrickData, GameObject>.Enumerator enumerator2;
            TrickData data2;
            GameObject obj2;
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_0021;
            }
            if (battle.CurrentScene != null)
            {
                goto Label_0022;
            }
        Label_0021:
            return;
        Label_0022:
            if (battle.Battle != null)
            {
                goto Label_0030;
            }
            return;
        Label_0030:
            enumerator = mTrickDataLists.GetEnumerator();
        Label_003B:
            try
            {
                goto Label_014C;
            Label_0040:
                data = &enumerator.Current;
                if (data.mValid != null)
                {
                    goto Label_005D;
                }
                goto Label_014C;
            Label_005D:
                if (mTrickMarkerLists.ContainsKey(data) == null)
                {
                    goto Label_0072;
                }
                goto Label_014C;
            Label_0072:
                dictionary = new Dictionary<TrickData, GameObject>();
                enumerator2 = mTrickMarkerLists.GetEnumerator();
            Label_0085:
                try
                {
                    goto Label_010A;
                Label_008A:
                    pair = &enumerator2.Current;
                    data2 = &pair.Key;
                    if (data2.mGridX != data.mGridX)
                    {
                        goto Label_00FA;
                    }
                    if (data2.mGridY != data.mGridY)
                    {
                        goto Label_00FA;
                    }
                    obj2 = &pair.Value;
                    if (obj2 == null)
                    {
                        goto Label_010A;
                    }
                    Object.Destroy(obj2.get_gameObject());
                    goto Label_010A;
                Label_00FA:
                    dictionary.Add(data2, &pair.Value);
                Label_010A:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_008A;
                    }
                    goto Label_0128;
                }
                finally
                {
                Label_011B:
                    ((Dictionary<TrickData, GameObject>.Enumerator) enumerator2).Dispose();
                }
            Label_0128:
                if (mTrickMarkerLists.Count == dictionary.Count)
                {
                    goto Label_0145;
                }
                mTrickMarkerLists = dictionary;
            Label_0145:
                entryMarker(battle, data);
            Label_014C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0040;
                }
                goto Label_0169;
            }
            finally
            {
            Label_015D:
                ((List<TrickData>.Enumerator) enumerator).Dispose();
            }
        Label_0169:
            return;
        }

        public static unsafe void AddMarker(Transform parent, Dictionary<string, GameObject> trickObj, GameObject baseObj)
        {
            TrickData data;
            List<TrickData>.Enumerator enumerator;
            Dictionary<TrickData, GameObject> dictionary;
            KeyValuePair<TrickData, GameObject> pair;
            Dictionary<TrickData, GameObject>.Enumerator enumerator2;
            TrickData data2;
            GameObject obj2;
            enumerator = mTrickDataLists.GetEnumerator();
        Label_000B:
            try
            {
                goto Label_0119;
            Label_0010:
                data = &enumerator.Current;
                if (data.mValid != null)
                {
                    goto Label_002D;
                }
                goto Label_0119;
            Label_002D:
                if (mTrickMarkerLists.ContainsKey(data) == null)
                {
                    goto Label_0042;
                }
                goto Label_0119;
            Label_0042:
                dictionary = new Dictionary<TrickData, GameObject>();
                enumerator2 = mTrickMarkerLists.GetEnumerator();
            Label_0054:
                try
                {
                    goto Label_00D7;
                Label_0059:
                    pair = &enumerator2.Current;
                    data2 = &pair.Key;
                    if (data2.mGridX != data.mGridX)
                    {
                        goto Label_00C8;
                    }
                    if (data2.mGridY != data.mGridY)
                    {
                        goto Label_00C8;
                    }
                    obj2 = &pair.Value;
                    if (obj2 == null)
                    {
                        goto Label_00D7;
                    }
                    Object.Destroy(obj2.get_gameObject());
                    goto Label_00D7;
                Label_00C8:
                    dictionary.Add(data2, &pair.Value);
                Label_00D7:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_0059;
                    }
                    goto Label_00F5;
                }
                finally
                {
                Label_00E8:
                    ((Dictionary<TrickData, GameObject>.Enumerator) enumerator2).Dispose();
                }
            Label_00F5:
                if (mTrickMarkerLists.Count == dictionary.Count)
                {
                    goto Label_0110;
                }
                mTrickMarkerLists = dictionary;
            Label_0110:
                entryMarker(parent, data, trickObj, baseObj);
            Label_0119:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0010;
                }
                goto Label_0136;
            }
            finally
            {
            Label_012A:
                ((List<TrickData>.Enumerator) enumerator).Dispose();
            }
        Label_0136:
            return;
        }

        private static void addTargetAreaEff(int grid_x, int grid_y, TrickData trick_data, List<Unit> target_lists)
        {
            SRPG.TrickParam param;
            SceneBattle battle;
            BattleCore core;
            GridMap<bool> map;
            int num;
            int num2;
            Unit unit;
            if (trick_data == null)
            {
                goto Label_000C;
            }
            if (target_lists != null)
            {
                goto Label_000D;
            }
        Label_000C:
            return;
        Label_000D:
            param = trick_data.mTrickParam;
            if (param.IsAreaEff != null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            battle = SceneBattle.Instance;
            if (battle != null)
            {
                goto Label_0032;
            }
            return;
        Label_0032:
            core = battle.Battle;
            if (core != null)
            {
                goto Label_0040;
            }
            return;
        Label_0040:
            map = core.CreateScopeGridMap(grid_x, grid_y, param.EffShape, param.EffScope, param.EffHeight);
            if (map != null)
            {
                goto Label_006C;
            }
            return;
        Label_006C:
            num = 0;
            goto Label_00E9;
        Label_0074:
            num2 = 0;
            goto Label_00D6;
        Label_007C:
            if (map.get(num, num2) != null)
            {
                goto Label_0090;
            }
            goto Label_00D0;
        Label_0090:
            unit = core.FindUnitAtGrid(num, num2);
            if (unit == null)
            {
                goto Label_00D0;
            }
            if (target_lists.Contains(unit) == null)
            {
                goto Label_00B5;
            }
            goto Label_00D0;
        Label_00B5:
            if (trick_data.checkTarget(unit, 1) != null)
            {
                goto Label_00C8;
            }
            goto Label_00D0;
        Label_00C8:
            target_lists.Add(unit);
        Label_00D0:
            num2 += 1;
        Label_00D6:
            if (num2 < map.h)
            {
                goto Label_007C;
            }
            num += 1;
        Label_00E9:
            if (num < map.w)
            {
                goto Label_0074;
            }
            return;
        }

        public int calcDamage(Unit target)
        {
            int num;
            int num2;
            int num3;
            SkillParamCalcTypes types;
            num = 0;
            num2 = target.MaximumStatus.param.hp;
            if (this.mTrickParam.CalcType == 1)
            {
                goto Label_0030;
            }
            goto Label_004B;
        Label_0030:
            num = (num2 * this.mTrickParam.DamageVal) / 100;
            goto Label_0061;
        Label_004B:
            num = this.mTrickParam.DamageVal;
        Label_0061:
            if (num <= 0)
            {
                goto Label_0099;
            }
            num3 = 0;
            num3 += this.getRateDamageElement(target);
            num3 += this.getRateDamageAttackDetail(target);
            num3 += this.getRateDamageUnitDefense(target);
            return Math.Max(num - ((num * num3) / 100), 1);
        Label_0099:
            return 0;
        }

        public int calcHeal(Unit target)
        {
            int num;
            int num2;
            SkillParamCalcTypes types;
            num = 0;
            num2 = target.MaximumStatus.param.hp;
            if (this.mTrickParam.CalcType == 1)
            {
                goto Label_0030;
            }
            goto Label_004B;
        Label_0030:
            num = (num2 * this.mTrickParam.DamageVal) / 100;
            goto Label_0061;
        Label_004B:
            num = this.mTrickParam.DamageVal;
        Label_0061:
            return Math.Min(num, num2 - target.CurrentStatus.param.hp);
        }

        public static unsafe bool CheckClock(int now_clock)
        {
            List<TrickData> list;
            TrickData data;
            List<TrickData>.Enumerator enumerator;
            if (mTrickDataLists.Count != null)
            {
                goto Label_0011;
            }
            return 0;
        Label_0011:
            list = new List<TrickData>(mTrickDataLists.Count);
            enumerator = mTrickDataLists.GetEnumerator();
        Label_002C:
            try
            {
                goto Label_007C;
            Label_0031:
                data = &enumerator.Current;
                if (data.mTrickParam.ValidClock == null)
                {
                    goto Label_0075;
                }
                if ((data.mTrickParam.ValidClock + data.mCreateClock) >= now_clock)
                {
                    goto Label_0075;
                }
                goto Label_007C;
            Label_0075:
                list.Add(data);
            Label_007C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0031;
                }
                goto Label_0099;
            }
            finally
            {
            Label_008D:
                ((List<TrickData>.Enumerator) enumerator).Dispose();
            }
        Label_0099:
            if (mTrickDataLists.Count == list.Count)
            {
                goto Label_00B6;
            }
            mTrickDataLists = list;
            return 1;
        Label_00B6:
            return 0;
        }

        private bool checkFailCond(Unit target, int val, int resist, EUnitCondition condition, RandXorshift rand)
        {
            int num;
            int num2;
            if (rand == null)
            {
                goto Label_000E;
            }
            if (val > 0)
            {
                goto Label_0010;
            }
        Label_000E:
            return 0;
        Label_0010:
            num = val - resist;
            if (num <= 0)
            {
                goto Label_002B;
            }
            num2 = rand.Get() % 100;
            return (num > num2);
        Label_002B:
            return 0;
        }

        private bool checkKnockBack(Unit target, RandXorshift rand)
        {
            EnchantParam param;
            int num;
            int num2;
            if (target == null)
            {
                goto Label_000C;
            }
            if (rand != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            if (target.IsDisableUnitCondition(0x2000L) == null)
            {
                goto Label_0021;
            }
            return 0;
        Label_0021:
            param = target.CurrentStatus.enchant_resist;
            num = this.mTrickParam.KnockBackRate - param[13];
            if (num > 0)
            {
                goto Label_0055;
            }
            return 0;
        Label_0055:
            if (num >= 100)
            {
                goto Label_0070;
            }
            num2 = rand.Get() % 100;
            if (num2 < num)
            {
                goto Label_0070;
            }
            return 0;
        Label_0070:
            return 1;
        }

        public static bool CheckRemoveMarker(TrickData trick_data)
        {
            GameObject obj2;
            if (trick_data != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (mTrickDataLists.Contains(trick_data) == null)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            if (mTrickMarkerLists.ContainsKey(trick_data) == null)
            {
                goto Label_005A;
            }
            obj2 = mTrickMarkerLists[trick_data];
            if (obj2 == null)
            {
                goto Label_004C;
            }
            Object.Destroy(obj2.get_gameObject());
        Label_004C:
            mTrickMarkerLists.Remove(trick_data);
            return 1;
        Label_005A:
            return 0;
        }

        private bool checkTarget(Unit target, bool is_eff)
        {
            bool flag;
            ESkillTarget target2;
            EUnitSide side;
            EUnitSide side2;
            ESkillTarget target3;
            flag = 0;
            target2 = this.mTrickParam.Target;
            if (is_eff == null)
            {
                goto Label_0020;
            }
            target2 = this.mTrickParam.EffTarget;
        Label_0020:
            target3 = target2;
            switch (target3)
            {
                case 0:
                    goto Label_0043;

                case 1:
                    goto Label_005D;

                case 2:
                    goto Label_0085;

                case 3:
                    goto Label_00B0;

                case 4:
                    goto Label_00B7;
            }
            goto Label_00D4;
        Label_0043:
            if (this.mCreateUnit == null)
            {
                goto Label_00D4;
            }
            flag = target == this.mCreateUnit;
            goto Label_00D4;
        Label_005D:
            side = 1;
            if (this.mCreateUnit == null)
            {
                goto Label_0076;
            }
            side = this.mCreateUnit.Side;
        Label_0076:
            flag = target.Side == side;
            goto Label_00D4;
        Label_0085:
            side2 = 1;
            if (this.mCreateUnit == null)
            {
                goto Label_009E;
            }
            side2 = this.mCreateUnit.Side;
        Label_009E:
            flag = (target.Side == side2) == 0;
            goto Label_00D4;
        Label_00B0:
            flag = 1;
            goto Label_00D4;
        Label_00B7:
            if (this.mCreateUnit == null)
            {
                goto Label_00D4;
            }
            flag = (target == this.mCreateUnit) == 0;
        Label_00D4:
            return flag;
        }

        private bool checkTiming(EffectCheckTimings chk_timing)
        {
            bool flag;
            BuffEffectParam.Buff buff;
            BuffEffectParam.Buff[] buffArray;
            int num;
            ParamTypes types;
            EffectCheckTimings timings;
            if (this.mBuffEffect != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            flag = 0;
            buffArray = this.mBuffEffect.param.buffs;
            num = 0;
            goto Label_0055;
        Label_0027:
            buff = buffArray[num];
            types = buff.type;
            if (types == 14)
            {
                goto Label_004A;
            }
            if (types == 15)
            {
                goto Label_004A;
            }
            goto Label_0051;
        Label_004A:
            flag = 1;
        Label_0051:
            num += 1;
        Label_0055:
            if (num < ((int) buffArray.Length))
            {
                goto Label_0027;
            }
            timings = chk_timing;
            switch ((timings - 7))
            {
                case 0:
                    goto Label_007B;

                case 1:
                    goto Label_0082;

                case 2:
                    goto Label_0080;
            }
            goto Label_0082;
        Label_007B:
            return (flag == 0);
        Label_0080:
            return flag;
        Label_0082:
            return 0;
        }

        public static void ClearEffect()
        {
            mTrickDataLists.Clear();
            return;
        }

        private BuffAttachment createBuffAttachment(Unit target, BuffTypes buff_type, bool is_negative_value_is_buff, SkillParamCalcTypes calc_type, BaseStatus status, EffectCheckTimings chk_timing)
        {
            BuffAttachment attachment;
            if (this.mBuffEffect != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            attachment = new BuffAttachment(this.mBuffEffect.param);
            attachment.user = this.mCreateUnit;
            attachment.skill = null;
            attachment.skilltarget = 1;
            attachment.IsPassive = 0;
            attachment.CheckTarget = null;
            attachment.DuplicateCount = 0;
            attachment.CheckTiming = chk_timing;
            attachment.turn = 1;
            attachment.BuffType = buff_type;
            attachment.IsNegativeValueIsBuff = is_negative_value_is_buff;
            attachment.CalcType = calc_type;
            attachment.UseCondition = this.mBuffEffect.param.cond;
            status.CopyTo(attachment.status);
            return attachment;
        }

        private CondAttachment createCondAttachment(Unit target, SRPG.CondEffect effect, ConditionEffectTypes type, EUnitCondition condition)
        {
            CondAttachment attachment;
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
            attachment.IsPassive = 0;
            attachment.UseCondition = 0;
            attachment.CondType = type;
            attachment.Condition = condition;
            attachment.turn = effect.turn;
            attachment.CheckTiming = effect.param.chk_timing;
            attachment.CheckTarget = target;
            if (attachment.IsFailCondition() == null)
            {
                goto Label_009F;
            }
            attachment.IsCurse = effect.IsCurse;
        Label_009F:
            attachment.SetupLinkageBuff();
            return attachment;
        }

        private void cureCond(Unit target, EUnitCondition condition, LogMapTrick.TargetInfo log_mt_ti)
        {
            bool flag;
            flag = target.IsUnitCondition(condition);
            target.CureCondEffects(condition, 1, 0);
            if (log_mt_ti == null)
            {
                goto Label_003E;
            }
            if (flag == null)
            {
                goto Label_003E;
            }
            if (target.IsUnitCondition(condition) != null)
            {
                goto Label_003E;
            }
            log_mt_ti.IsEffective = 1;
            log_mt_ti.CureCondition |= condition;
        Label_003E:
            return;
        }

        private void decActionCount()
        {
            if (this.mTrickParam.ActionCount == null)
            {
                goto Label_003E;
            }
            this.mRestActionCount = OInt.op_Decrement(this.mRestActionCount);
            if (this.mRestActionCount > 0)
            {
                goto Label_003E;
            }
            RemoveEffect(this);
        Label_003E:
            return;
        }

        public static TrickData EntryEffect(string iname, int grid_x, int grid_y, string tag, Unit creator, int create_clock, int rank, int rankcap)
        {
            TrickData data;
            TrickData data2;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            data = new TrickData();
            data.setup(iname, grid_x, grid_y, tag, creator, create_clock, rank, rankcap);
            if (data.mTrickParam != null)
            {
                goto Label_0032;
            }
            return null;
        Label_0032:
            data2 = SearchEffect(grid_x, grid_y);
            if (data2 == null)
            {
                goto Label_0060;
            }
            if (data2.mTrickParam.IsNoOverWrite == null)
            {
                goto Label_0059;
            }
            data = null;
            return null;
        Label_0059:
            RemoveEffect(data2);
        Label_0060:
            mTrickDataLists.Add(data);
            return data;
        }

        private static void entryMarker(SceneBattle sb, TrickData td)
        {
            GameObject obj2;
            string str;
            Vector3 vector;
            GameObject obj3;
            if (sb == null)
            {
                goto Label_0011;
            }
            if (td != null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            if (td.IsVisualized() != null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            obj2 = sb.TrickMarker;
            str = td.mTrickParam.MarkerName;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_005A;
            }
            if (sb.TrickMarkerDics.ContainsKey(str) == null)
            {
                goto Label_005A;
            }
            obj2 = sb.TrickMarkerDics[str];
        Label_005A:
            if (obj2 != null)
            {
                goto Label_0066;
            }
            return;
        Label_0066:
            vector = sb.CalcGridCenter(td.mGridX, td.mGridY);
            obj3 = Object.Instantiate(obj2, vector, Quaternion.get_identity()) as GameObject;
            if (obj3 == null)
            {
                goto Label_00C3;
            }
            obj3.get_transform().SetParent(sb.CurrentScene.get_transform(), 0);
            mTrickMarkerLists.Add(td, obj3);
        Label_00C3:
            return;
        }

        private static unsafe void entryMarker(Transform parent, TrickData td, Dictionary<string, GameObject> dic, GameObject baseObj)
        {
            GameObject obj2;
            string str;
            Vector3 vector;
            GameObject obj3;
            if (td.IsVisualized() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            obj2 = baseObj;
            str = td.mTrickParam.MarkerName;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0039;
            }
            if (dic.ContainsKey(str) == null)
            {
                goto Label_0039;
            }
            obj2 = dic[str];
        Label_0039:
            if (obj2 != null)
            {
                goto Label_0045;
            }
            return;
        Label_0045:
            &vector..ctor(((float) td.mGridX) + 0.5f, GameUtility.CalcHeight(((float) td.mGridX) + 0.5f, ((float) td.mGridY) + 0.5f), ((float) td.mGridY) + 0.5f);
            obj3 = Object.Instantiate(obj2, vector, Quaternion.get_identity()) as GameObject;
            if (obj3 == null)
            {
                goto Label_00DB;
            }
            if ((parent != null) == null)
            {
                goto Label_00CF;
            }
            obj3.get_transform().SetParent(parent, 0);
        Label_00CF:
            mTrickMarkerLists.Add(td, obj3);
        Label_00DB:
            return;
        }

        private void failCond(Unit target, SRPG.CondEffect effect, ConditionEffectTypes effect_type, EUnitCondition condition, LogMapTrick.TargetInfo log_mt_ti)
        {
            SceneBattle battle;
            BattleCore core;
            LogFailCondition condition2;
            TacticsUnitController controller;
            CondAttachment attachment;
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_0057;
            }
            core = battle.Battle;
            if (core == null)
            {
                goto Label_0057;
            }
            condition2 = core.Log<LogFailCondition>();
            condition2.self = target;
            condition2.source = null;
            condition2.condition = condition;
            controller = battle.FindUnitController(target);
            if (controller == null)
            {
                goto Label_0057;
            }
            controller.LockUpdateBadStatus(condition, 0);
        Label_0057:
            attachment = this.createCondAttachment(target, effect, effect_type, condition);
            target.SetCondAttachment(attachment);
            if (log_mt_ti == null)
            {
                goto Label_0099;
            }
            if (target.IsUnitCondition(condition) == null)
            {
                goto Label_0099;
            }
            log_mt_ti.IsEffective = 1;
            log_mt_ti.FailCondition |= condition;
        Label_0099:
            return;
        }

        public static List<TrickData> GetEffectAll()
        {
            return mTrickDataLists;
        }

        private int getRateDamageAttackDetail(Unit target)
        {
            int num;
            AttackDetailTypes types;
            num = 0;
            switch ((this.mTrickParam.AttackDetail - 1))
            {
                case 0:
                    goto Label_0033;

                case 1:
                    goto Label_004D;

                case 2:
                    goto Label_0067;

                case 3:
                    goto Label_0081;

                case 4:
                    goto Label_009B;

                case 5:
                    goto Label_00B5;
            }
            goto Label_00CF;
        Label_0033:
            num += target.CurrentStatus[0x1b];
            goto Label_00CF;
        Label_004D:
            num += target.CurrentStatus[0x1c];
            goto Label_00CF;
        Label_0067:
            num += target.CurrentStatus[0x1d];
            goto Label_00CF;
        Label_0081:
            num += target.CurrentStatus[30];
            goto Label_00CF;
        Label_009B:
            num += target.CurrentStatus[0x1f];
            goto Label_00CF;
        Label_00B5:
            num += target.CurrentStatus[0x21];
        Label_00CF:
            return num;
        }

        private int getRateDamageElement(Unit target)
        {
            int num;
            ElementParam param;
            num = 0;
            if (this.mTrickParam.Elem == null)
            {
                goto Label_0037;
            }
            param = target.CurrentStatus.element_resist;
            num += param[this.mTrickParam.Elem];
        Label_0037:
            return num;
        }

        private int getRateDamageUnitDefense(Unit target)
        {
            int num;
            EElement element;
            num = 0;
            switch ((target.Element - 1))
            {
                case 0:
                    goto Label_002E;

                case 1:
                    goto Label_0048;

                case 2:
                    goto Label_0062;

                case 3:
                    goto Label_007C;

                case 4:
                    goto Label_0096;

                case 5:
                    goto Label_00B0;
            }
            goto Label_00CA;
        Label_002E:
            num += target.CurrentStatus[0x2b];
            goto Label_00CA;
        Label_0048:
            num += target.CurrentStatus[0x2c];
            goto Label_00CA;
        Label_0062:
            num += target.CurrentStatus[0x2d];
            goto Label_00CA;
        Label_007C:
            num += target.CurrentStatus[0x2e];
            goto Label_00CA;
        Label_0096:
            num += target.CurrentStatus[0x2f];
            goto Label_00CA;
        Label_00B0:
            num += target.CurrentStatus[0x30];
        Label_00CA:
            return num;
        }

        public bool IsVisualized()
        {
            if (this.mTrickParam.VisualType != null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            if (this.mTrickParam.VisualType != 1)
            {
                goto Label_0040;
            }
            if (this.mCreateUnit == null)
            {
                goto Label_003E;
            }
            if (this.mCreateUnit.Side == null)
            {
                goto Label_0040;
            }
        Label_003E:
            return 0;
        Label_0040:
            return 1;
        }

        public static void MomentBuff(Unit target, int grid_x, int grid_y, EffectCheckTimings chk_timing)
        {
            TrickData data;
            if (target != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            data = SearchEffect(grid_x, grid_y);
            if (data != null)
            {
                goto Label_0016;
            }
            return;
        Label_0016:
            if (data.checkTarget(target, 0) != null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            if (data.checkTiming(chk_timing) != null)
            {
                goto Label_0031;
            }
            return;
        Label_0031:
            data.actionBuff(target, chk_timing, null);
            return;
        }

        public static bool RemoveEffect(TrickData trick_data)
        {
            SceneBattle battle;
            BattleCore core;
            if (trick_data != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (mTrickDataLists.Contains(trick_data) != null)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_003F;
            }
            core = battle.Battle;
            if (core == null)
            {
                goto Label_003F;
            }
            core.GimmickEventTrickKillCount(trick_data);
        Label_003F:
            mTrickDataLists.Remove(trick_data);
            return 1;
        }

        public static unsafe void RemoveEffect(int grid_x, int grid_y)
        {
            List<TrickData> list;
            TrickData data;
            List<TrickData>.Enumerator enumerator;
            SceneBattle battle;
            BattleCore core;
            List<TrickData> list2;
            TrickData data2;
            List<TrickData>.Enumerator enumerator2;
            <RemoveEffect>c__AnonStorey258 storey;
            storey = new <RemoveEffect>c__AnonStorey258();
            storey.grid_x = grid_x;
            storey.grid_y = grid_y;
            list = mTrickDataLists.FindAll(new Predicate<TrickData>(storey.<>m__16F));
            if (list == null)
            {
                goto Label_0040;
            }
            if (list.Count != null)
            {
                goto Label_0041;
            }
        Label_0040:
            return;
        Label_0041:
            enumerator = list.GetEnumerator();
        Label_0048:
            try
            {
                goto Label_007D;
            Label_004D:
                data = &enumerator.Current;
                battle = SceneBattle.Instance;
                if (battle == null)
                {
                    goto Label_007D;
                }
                core = battle.Battle;
                if (core == null)
                {
                    goto Label_007D;
                }
                core.GimmickEventTrickKillCount(data);
            Label_007D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_004D;
                }
                goto Label_009A;
            }
            finally
            {
            Label_008E:
                ((List<TrickData>.Enumerator) enumerator).Dispose();
            }
        Label_009A:
            list2 = new List<TrickData>(mTrickDataLists.Count);
            enumerator2 = mTrickDataLists.GetEnumerator();
        Label_00B7:
            try
            {
                goto Label_00E0;
            Label_00BC:
                data2 = &enumerator2.Current;
                if (list.Contains(data2) == null)
                {
                    goto Label_00D7;
                }
                goto Label_00E0;
            Label_00D7:
                list2.Add(data2);
            Label_00E0:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00BC;
                }
                goto Label_00FE;
            }
            finally
            {
            Label_00F1:
                ((List<TrickData>.Enumerator) enumerator2).Dispose();
            }
        Label_00FE:
            mTrickDataLists = list2;
            return;
        }

        public static TrickData SearchEffect(int grid_x, int grid_y)
        {
            TrickData data;
            <SearchEffect>c__AnonStorey257 storey;
            storey = new <SearchEffect>c__AnonStorey257();
            storey.grid_x = grid_x;
            storey.grid_y = grid_y;
            return mTrickDataLists.Find(new Predicate<TrickData>(storey.<>m__16E));
        }

        private void setup(string iname, int grid_x, int grid_y, string tag, Unit creator, int create_clock, int rank, int rankcap)
        {
            GameManager manager;
            BuffEffectParam param;
            CondEffectParam param2;
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
            this.mTrickParam = manager.MasterParam.GetTrickParam(iname);
            if (this.mTrickParam != null)
            {
                goto Label_003C;
            }
            return;
        Label_003C:
            this.mRankCap = Math.Max(rankcap, 1);
            this.mRank = Math.Min(rank, this.mRankCap);
            param = manager.MasterParam.GetBuffEffectParam(this.mTrickParam.BuffId);
            this.mBuffEffect = SRPG.BuffEffect.CreateBuffEffect(param, this.mRank, this.mRankCap);
            param2 = manager.MasterParam.GetCondEffectParam(this.mTrickParam.CondId);
            this.mCondEffect = SRPG.CondEffect.CreateCondEffect(param2, this.mRank, this.mRankCap);
            this.mCreateUnit = creator;
            this.mGridX = grid_x;
            this.mGridY = grid_y;
            this.mTag = tag;
            this.mRestActionCount = this.mTrickParam.ActionCount;
            this.mCreateClock = create_clock;
            this.mValid = 1;
            return;
        }

        public static TrickData SuspendEffect(string iname, int grid_x, int grid_y, string tag, Unit creator, int create_clock, int rank, int rankcap, int rest_count)
        {
            TrickData data;
            data = EntryEffect(iname, grid_x, grid_y, tag, creator, create_clock, rank, rankcap);
            if (data == null)
            {
                goto Label_0025;
            }
            data.mRestActionCount = rest_count;
        Label_0025:
            return data;
        }

        public static unsafe void UpdateMarker()
        {
            Dictionary<TrickData, GameObject> dictionary;
            KeyValuePair<TrickData, GameObject> pair;
            Dictionary<TrickData, GameObject>.Enumerator enumerator;
            TrickData data;
            GameObject obj2;
            dictionary = new Dictionary<TrickData, GameObject>();
            enumerator = mTrickMarkerLists.GetEnumerator();
        Label_0011:
            try
            {
                goto Label_006A;
            Label_0016:
                pair = &enumerator.Current;
                data = &pair.Key;
                if (mTrickDataLists.Contains(data) != null)
                {
                    goto Label_005C;
                }
                obj2 = &pair.Value;
                if (obj2 == null)
                {
                    goto Label_006A;
                }
                Object.Destroy(obj2.get_gameObject());
                goto Label_006A;
            Label_005C:
                dictionary.Add(data, &pair.Value);
            Label_006A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0016;
                }
                goto Label_0087;
            }
            finally
            {
            Label_007B:
                ((Dictionary<TrickData, GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_0087:
            if (mTrickMarkerLists.Count == dictionary.Count)
            {
                goto Label_00A2;
            }
            mTrickMarkerLists = dictionary;
        Label_00A2:
            AddMarker();
            return;
        }

        public static unsafe void UpdateMarker(Transform parent, Dictionary<string, GameObject> trickObj, GameObject baseObj)
        {
            Dictionary<TrickData, GameObject> dictionary;
            KeyValuePair<TrickData, GameObject> pair;
            Dictionary<TrickData, GameObject>.Enumerator enumerator;
            TrickData data;
            GameObject obj2;
            dictionary = new Dictionary<TrickData, GameObject>();
            enumerator = mTrickMarkerLists.GetEnumerator();
        Label_0011:
            try
            {
                goto Label_006A;
            Label_0016:
                pair = &enumerator.Current;
                data = &pair.Key;
                if (mTrickDataLists.Contains(data) != null)
                {
                    goto Label_005C;
                }
                obj2 = &pair.Value;
                if (obj2 == null)
                {
                    goto Label_006A;
                }
                Object.Destroy(obj2.get_gameObject());
                goto Label_006A;
            Label_005C:
                dictionary.Add(data, &pair.Value);
            Label_006A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0016;
                }
                goto Label_0087;
            }
            finally
            {
            Label_007B:
                ((Dictionary<TrickData, GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_0087:
            if (mTrickMarkerLists.Count == dictionary.Count)
            {
                goto Label_00A2;
            }
            mTrickMarkerLists = dictionary;
        Label_00A2:
            AddMarker(parent, trickObj, baseObj);
            return;
        }

        public SRPG.TrickParam TrickParam
        {
            get
            {
                return this.mTrickParam;
            }
        }

        public SRPG.BuffEffect BuffEffect
        {
            get
            {
                return this.mBuffEffect;
            }
        }

        public SRPG.CondEffect CondEffect
        {
            get
            {
                return this.mCondEffect;
            }
        }

        public OBool Valid
        {
            get
            {
                return this.mValid;
            }
        }

        public Unit CreateUnit
        {
            get
            {
                return this.mCreateUnit;
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

        public OInt GridX
        {
            get
            {
                return this.mGridX;
            }
        }

        public OInt GridY
        {
            get
            {
                return this.mGridY;
            }
        }

        public OInt RestActionCount
        {
            get
            {
                return this.mRestActionCount;
            }
        }

        public OInt CreateClock
        {
            get
            {
                return this.mCreateClock;
            }
        }

        public string Tag
        {
            get
            {
                return this.mTag;
            }
        }

        [CompilerGenerated]
        private sealed class <RemoveEffect>c__AnonStorey258
        {
            internal int grid_x;
            internal int grid_y;

            public <RemoveEffect>c__AnonStorey258()
            {
                base..ctor();
                return;
            }

            internal bool <>m__16F(TrickData tdl)
            {
                return ((tdl.mGridX != this.grid_x) ? 0 : (tdl.mGridY == this.grid_y));
            }
        }

        [CompilerGenerated]
        private sealed class <SearchEffect>c__AnonStorey257
        {
            internal int grid_x;
            internal int grid_y;

            public <SearchEffect>c__AnonStorey257()
            {
                base..ctor();
                return;
            }

            internal bool <>m__16E(TrickData tdl)
            {
                return (((tdl.mValid == null) || (tdl.mGridX != this.grid_x)) ? 0 : (tdl.mGridY == this.grid_y));
            }
        }
    }
}

