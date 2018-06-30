namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class RankingQuestScheduleParam
    {
        public int id;
        public DateTime beginAt;
        public DateTime endAt;
        public DateTime reward_begin_at;
        public DateTime reward_end_at;
        public DateTime visible_begin_at;
        public DateTime visible_end_at;

        public RankingQuestScheduleParam()
        {
            base..ctor();
            return;
        }

        public static unsafe RankingQuestParam CompareOpenOrLatest(ref DateTime now, RankingQuestParam param1, RankingQuestParam param2)
        {
            long num;
            long num2;
            RankingQuestParam param;
            num = &param1.scheduleParam.endAt.Ticks - now.Ticks;
            num2 = &param2.scheduleParam.endAt.Ticks - now.Ticks;
            param = null;
            if (param1.scheduleParam.IsAvailablePeriod(*(now)) == null)
            {
                goto Label_0079;
            }
            if (param2.scheduleParam.IsAvailablePeriod(*(now)) == null)
            {
                goto Label_0072;
            }
            param = (num >= num2) ? param2 : param1;
            goto Label_0074;
        Label_0072:
            param = param1;
        Label_0074:
            goto Label_0126;
        Label_0079:
            if (param1.scheduleParam.IsAvailableVisiblePeriod(*(now)) == null)
            {
                goto Label_00DD;
            }
            if (param2.scheduleParam.IsAvailablePeriod(*(now)) == null)
            {
                goto Label_00AC;
            }
            param = param2;
            goto Label_00D8;
        Label_00AC:
            if (param2.scheduleParam.IsAvailableVisiblePeriod(*(now)) == null)
            {
                goto Label_00D6;
            }
            param = (num <= num2) ? param2 : param1;
            goto Label_00D8;
        Label_00D6:
            param = param1;
        Label_00D8:
            goto Label_0126;
        Label_00DD:
            if (param2.scheduleParam.IsAvailablePeriod(*(now)) == null)
            {
                goto Label_00FA;
            }
            param = param2;
            goto Label_0126;
        Label_00FA:
            if (param2.scheduleParam.IsAvailableVisiblePeriod(*(now)) == null)
            {
                goto Label_0117;
            }
            param = param2;
            goto Label_0126;
        Label_0117:
            param = (num <= num2) ? param2 : param1;
        Label_0126:
            return param;
        }

        public unsafe bool Deserialize(JSON_RankingQuestScheduleParam json)
        {
            this.id = json.id;
            this.beginAt = DateTime.MinValue;
            this.endAt = DateTime.MaxValue;
            if (string.IsNullOrEmpty(json.begin_at) != null)
            {
                goto Label_0044;
            }
            DateTime.TryParse(json.begin_at, &this.beginAt);
        Label_0044:
            if (string.IsNullOrEmpty(json.end_at) != null)
            {
                goto Label_0066;
            }
            DateTime.TryParse(json.end_at, &this.endAt);
        Label_0066:
            this.reward_begin_at = DateTime.MinValue;
            this.reward_end_at = DateTime.MaxValue;
            if (string.IsNullOrEmpty(json.reward_begin_at) != null)
            {
                goto Label_009E;
            }
            DateTime.TryParse(json.reward_begin_at, &this.reward_begin_at);
        Label_009E:
            if (string.IsNullOrEmpty(json.reward_end_at) != null)
            {
                goto Label_00C0;
            }
            DateTime.TryParse(json.reward_end_at, &this.reward_end_at);
        Label_00C0:
            this.visible_begin_at = DateTime.MinValue;
            this.visible_end_at = DateTime.MaxValue;
            if (string.IsNullOrEmpty(json.visible_begin_at) != null)
            {
                goto Label_00F8;
            }
            DateTime.TryParse(json.visible_begin_at, &this.visible_begin_at);
        Label_00F8:
            if (string.IsNullOrEmpty(json.visible_end_at) != null)
            {
                goto Label_011A;
            }
            DateTime.TryParse(json.visible_end_at, &this.visible_end_at);
        Label_011A:
            return 1;
        }

        public static unsafe List<RankingQuestParam> FilterDuplicateRankingQuestIDs(List<RankingQuestParam> list)
        {
            DateTime time;
            List<RankingQuestParam> list2;
            int num;
            RankingQuestParam param;
            <FilterDuplicateRankingQuestIDs>c__AnonStorey2EB storeyeb;
            <FilterDuplicateRankingQuestIDs>c__AnonStorey2EC storeyec;
            storeyeb = new <FilterDuplicateRankingQuestIDs>c__AnonStorey2EB();
            storeyeb.list = list;
            time = TimeManager.ServerTime;
            list2 = new List<RankingQuestParam>();
            storeyec = new <FilterDuplicateRankingQuestIDs>c__AnonStorey2EC();
            storeyec.<>f__ref$747 = storeyeb;
            storeyec.i = 0;
            goto Label_00B7;
        Label_0038:
            num = list2.FindIndex(new Predicate<RankingQuestParam>(storeyec.<>m__26B));
            param = (num == -1) ? null : list2[num];
            if (param == null)
            {
                goto Label_008E;
            }
            list2[num] = CompareOpenOrLatest(&time, param, storeyeb.list[storeyec.i]);
            goto Label_00A7;
        Label_008E:
            list2.Add(storeyeb.list[storeyec.i]);
        Label_00A7:
            storeyec.i += 1;
        Label_00B7:
            if (storeyec.i < storeyeb.list.Count)
            {
                goto Label_0038;
            }
            return list2;
        }

        public static RankingQuestScheduleParam FindByID(int id)
        {
            GameManager manager;
            <FindByID>c__AnonStorey2EA storeyea;
            storeyea = new <FindByID>c__AnonStorey2EA();
            storeyea.id = id;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0021;
            }
            return null;
        Label_0021:
            if (manager.RankingQuestScheduleParams != null)
            {
                goto Label_002E;
            }
            return null;
        Label_002E:
            return manager.RankingQuestScheduleParams.Find(new Predicate<RankingQuestScheduleParam>(storeyea.<>m__26A));
        }

        public static List<RankingQuestParam> FindRankingQuestParamBySchedule(RakingQuestScheduleGetFlags flag)
        {
            List<RankingQuestParam> list;
            GameManager manager;
            List<RankingQuestScheduleParam> list2;
            List<RankingQuestParam> list3;
            int num;
            int num2;
            list = new List<RankingQuestParam>();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_001A;
            }
            return list;
        Label_001A:
            if (manager.RankingQuestParams != null)
            {
                goto Label_0031;
            }
            DebugUtility.LogError("GameManager.Instance.RankingQuestParamsがnullです");
            return list;
        Label_0031:
            list2 = GetRankingQuestScheduleParam(flag);
            list3 = manager.RankingQuestParams;
            num = 0;
            goto Label_009A;
        Label_0047:
            num2 = 0;
            goto Label_0087;
        Label_004F:
            if (list3[num].schedule_id != list2[num2].id)
            {
                goto Label_0081;
            }
            list.Add(list3[num]);
            goto Label_0094;
        Label_0081:
            num2 += 1;
        Label_0087:
            if (num2 < list2.Count)
            {
                goto Label_004F;
            }
        Label_0094:
            num += 1;
        Label_009A:
            if (num < list3.Count)
            {
                goto Label_0047;
            }
            return list;
        }

        public static List<RankingQuestScheduleParam> GetRankingQuestScheduleParam(RakingQuestScheduleGetFlags flag)
        {
            List<RankingQuestScheduleParam> list;
            GameManager manager;
            <GetRankingQuestScheduleParam>c__AnonStorey2E9 storeye;
            <GetRankingQuestScheduleParam>c__AnonStorey2E8 storeye2;
            storeye = new <GetRankingQuestScheduleParam>c__AnonStorey2E9();
            storeye.flag = flag;
            list = new List<RankingQuestScheduleParam>();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0027;
            }
            return list;
        Label_0027:
            if (manager.RankingQuestScheduleParams != null)
            {
                goto Label_003E;
            }
            DebugUtility.LogError("GameManager.Instance.RankingQuestScheduleParamsがnullです");
            return list;
        Label_003E:
            if (manager.RankingQuestParams != null)
            {
                goto Label_0055;
            }
            DebugUtility.LogError("GameManager.Instance.RankingQuestParamsがnullです");
            return list;
        Label_0055:
            storeye.now = TimeManager.ServerTime;
            if (storeye.flag != null)
            {
                goto Label_007C;
            }
            list.AddRange(manager.RankingQuestScheduleParams);
            goto Label_00AD;
        Label_007C:
            storeye2 = new <GetRankingQuestScheduleParam>c__AnonStorey2E8();
            storeye2.<>f__ref$745 = storeye;
            storeye2.period = 0;
            list = Enumerable.ToList<RankingQuestScheduleParam>(Enumerable.Where<RankingQuestScheduleParam>(manager.RankingQuestScheduleParams, new Func<RankingQuestScheduleParam, bool>(storeye2.<>m__269)));
        Label_00AD:
            return list;
        }

        public bool IsAvailablePeriod(DateTime now)
        {
            if ((now < this.beginAt) != null)
            {
                goto Label_0022;
            }
            if ((this.endAt < now) == null)
            {
                goto Label_0024;
            }
        Label_0022:
            return 0;
        Label_0024:
            return 1;
        }

        public bool IsAvailableRewardPeriod(DateTime now)
        {
            if ((now < this.reward_begin_at) != null)
            {
                goto Label_0022;
            }
            if ((this.reward_end_at < now) == null)
            {
                goto Label_0024;
            }
        Label_0022:
            return 0;
        Label_0024:
            return 1;
        }

        public bool IsAvailableVisiblePeriod(DateTime now)
        {
            if ((now < this.visible_begin_at) != null)
            {
                goto Label_0022;
            }
            if ((this.visible_end_at < now) == null)
            {
                goto Label_0024;
            }
        Label_0022:
            return 0;
        Label_0024:
            return 1;
        }

        [CompilerGenerated]
        private sealed class <FilterDuplicateRankingQuestIDs>c__AnonStorey2EB
        {
            internal List<RankingQuestParam> list;

            public <FilterDuplicateRankingQuestIDs>c__AnonStorey2EB()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <FilterDuplicateRankingQuestIDs>c__AnonStorey2EC
        {
            internal int i;
            internal RankingQuestScheduleParam.<FilterDuplicateRankingQuestIDs>c__AnonStorey2EB <>f__ref$747;

            public <FilterDuplicateRankingQuestIDs>c__AnonStorey2EC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__26B(RankingQuestParam p)
            {
                return (p.iname == this.<>f__ref$747.list[this.i].iname);
            }
        }

        [CompilerGenerated]
        private sealed class <FindByID>c__AnonStorey2EA
        {
            internal int id;

            public <FindByID>c__AnonStorey2EA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__26A(RankingQuestScheduleParam param)
            {
                return (param.id == this.id);
            }
        }

        [CompilerGenerated]
        private sealed class <GetRankingQuestScheduleParam>c__AnonStorey2E8
        {
            internal bool period;
            internal RankingQuestScheduleParam.<GetRankingQuestScheduleParam>c__AnonStorey2E9 <>f__ref$745;

            public <GetRankingQuestScheduleParam>c__AnonStorey2E8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__269(RankingQuestScheduleParam param)
            {
                this.period = 0;
                if ((this.<>f__ref$745.flag & 1) == null)
                {
                    goto Label_0037;
                }
                this.period |= param.IsAvailablePeriod(this.<>f__ref$745.now);
            Label_0037:
                if ((this.<>f__ref$745.flag & 2) == null)
                {
                    goto Label_0067;
                }
                this.period |= param.IsAvailableRewardPeriod(this.<>f__ref$745.now);
            Label_0067:
                if ((this.<>f__ref$745.flag & 4) == null)
                {
                    goto Label_0097;
                }
                this.period |= param.IsAvailableVisiblePeriod(this.<>f__ref$745.now);
            Label_0097:
                return this.period;
            }
        }

        [CompilerGenerated]
        private sealed class <GetRankingQuestScheduleParam>c__AnonStorey2E9
        {
            internal RankingQuestScheduleParam.RakingQuestScheduleGetFlags flag;
            internal DateTime now;

            public <GetRankingQuestScheduleParam>c__AnonStorey2E9()
            {
                base..ctor();
                return;
            }
        }

        [Flags]
        public enum RakingQuestScheduleGetFlags
        {
            All = 0,
            Open = 1,
            Reward = 2,
            Visible = 4
        }
    }
}

