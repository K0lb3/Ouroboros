namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TrophyParam
    {
        public int category_hash_code;
        public bool is_none_category_hash;
        public string iname;
        public string Name;
        public string Expr;
        public string Category;
        public TrophyCategoryParam CategoryParam;
        public SRPG.ChallengeCategoryParam ChallengeCategoryParam;
        public TrophyDispType DispType;
        public string[] RequiredTrophies;
        public TrophyObjective[] Objectives;
        public int Gold;
        public int Coin;
        public int Exp;
        public int Stamina;
        public RewardItem[] Items;
        public RewardItem[] Artifacts;
        public int Challenge;
        public string ParentTrophy;
        public int help;
        public RewardItem[] ConceptCards;

        public TrophyParam()
        {
            this.is_none_category_hash = 1;
            base..ctor();
            return;
        }

        private unsafe DateTime AddTimeSpan(DateTime times, TimeSpan span)
        {
            if (&times.Equals(DateTime.MaxValue) == null)
            {
                goto Label_0013;
            }
            return times;
        Label_0013:
            try
            {
                times = &times.Add(span);
                goto Label_002F;
            }
            catch (Exception)
            {
            Label_0022:
                times = DateTime.MaxValue;
                goto Label_002F;
            }
        Label_002F:
            return times;
        }

        public static bool CheckRequiredTrophies(GameManager gm, TrophyParam tp, bool is_end_check, bool is_beginner_check)
        {
            TrophyState state;
            bool flag;
            string[] strArray;
            int num;
            TrophyParam param;
            TrophyState state2;
            if (is_beginner_check == null)
            {
                goto Label_0046;
            }
            state = gm.Player.GetTrophyCounter(tp, 0);
            if (tp.IsBeginner == null)
            {
                goto Label_0046;
            }
            if (MonoSingleton<GameManager>.Instance.Player.IsBeginner() != null)
            {
                goto Label_0046;
            }
            if (state == null)
            {
                goto Label_0044;
            }
            if (state.IsCompleted != null)
            {
                goto Label_0046;
            }
        Label_0044:
            return 0;
        Label_0046:
            flag = 1;
            strArray = tp.RequiredTrophies;
            num = 0;
            goto Label_00B8;
        Label_0056:
            if (string.IsNullOrEmpty(strArray[num]) == null)
            {
                goto Label_0068;
            }
            goto Label_00B4;
        Label_0068:
            param = gm.MasterParam.GetTrophy(strArray[num]);
            if (param != null)
            {
                goto Label_0084;
            }
            goto Label_00B4;
        Label_0084:
            if (is_end_check == null)
            {
                goto Label_00B4;
            }
            state2 = gm.Player.GetTrophyCounter(param, 0);
            if (state2 == null)
            {
                goto Label_00AD;
            }
            if (state2.IsEnded != null)
            {
                goto Label_00B4;
            }
        Label_00AD:
            flag = 0;
            goto Label_00C1;
        Label_00B4:
            num += 1;
        Label_00B8:
            if (num < ((int) strArray.Length))
            {
                goto Label_0056;
            }
        Label_00C1:
            return flag;
        }

        public bool ContainsCondition(TrophyConditionTypes c)
        {
            int num;
            if (this.Objectives != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            num = 0;
            goto Label_002D;
        Label_0014:
            if (this.Objectives[num].type != c)
            {
                goto Label_0029;
            }
            return 1;
        Label_0029:
            num += 1;
        Label_002D:
            if (num < ((int) this.Objectives.Length))
            {
                goto Label_0014;
            }
            return 0;
        }

        public bool Deserialize(JSON_TrophyParam json)
        {
            int num;
            int num2;
            int num3;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (json.flg_quests != null)
            {
                goto Label_0024;
            }
            this.RequiredTrophies = new string[0];
            goto Label_0060;
        Label_0024:
            this.RequiredTrophies = new string[(int) json.flg_quests.Length];
            num = 0;
            goto Label_0052;
        Label_003E:
            this.RequiredTrophies[num] = json.flg_quests[num];
            num += 1;
        Label_0052:
            if (num < ((int) json.flg_quests.Length))
            {
                goto Label_003E;
            }
        Label_0060:
            this.Objectives = new TrophyObjective[1];
            num3 = 0;
            goto Label_00E9;
        Label_0073:
            this.Objectives[num3] = new TrophyObjective();
            this.Objectives[num3].Param = this;
            this.Objectives[num3].index = num3;
            this.Objectives[num3].type = json.type;
            this.Objectives[num3].ival = json.ival;
            if (json.sval == null)
            {
                goto Label_00E5;
            }
            this.Objectives[num3].sval = new List<string>(json.sval);
        Label_00E5:
            num3 += 1;
        Label_00E9:
            if (num3 < 1)
            {
                goto Label_0073;
            }
            this.iname = json.iname;
            this.Name = json.name;
            this.Expr = json.expr;
            this.Gold = json.reward_gold;
            this.Coin = json.reward_coin;
            this.Exp = json.reward_exp;
            this.Stamina = json.reward_stamina;
            this.ParentTrophy = json.parent_iname;
            this.help = json.help;
            if (string.IsNullOrEmpty(json.category) != null)
            {
                goto Label_0184;
            }
            this.category_hash_code = json.category.GetHashCode();
            this.is_none_category_hash = 0;
        Label_0184:
            this.Category = json.category;
            this.DispType = json.disp;
            this.Items = InitializeItems(json);
            this.Artifacts = InitializeArtifacts(json);
            this.ConceptCards = InitializeConceptCards(json);
            return 1;
        }

        private TimeSpan GetAvailableSpan()
        {
            return new TimeSpan(7, 0, 0, 0);
        }

        private TimeSpan GetGraceRewardSpan()
        {
            return new TimeSpan(14, 0, 0, 0);
        }

        public DateTime GetGraceRewardTime()
        {
            DateTime time;
            DateTime time2;
            DateTime time3;
            if (this.CategoryParam != null)
            {
                goto Label_0015;
            }
            return new DateTime();
        Label_0015:
            time = this.CategoryParam.end_at.DateTimes;
            if (this.IsBeginner == null)
            {
                goto Label_0055;
            }
            time2 = MonoSingleton<GameManager>.Instance.Player.GetBeginnerEndTime();
            time = ((time <= time2) == null) ? time2 : time;
        Label_0055:
            if (this.IsBeginner != null)
            {
                goto Label_006E;
            }
            time = this.CategoryParam.GetQuestTime(time, 0);
        Label_006E:
            return this.AddTimeSpan(time, this.GetGraceRewardSpan());
        }

        private static unsafe RewardItem[] InitializeArtifacts(JSON_TrophyParam json)
        {
            List<RewardItem> list;
            RewardItem item;
            RewardItem item2;
            RewardItem item3;
            list = new List<RewardItem>();
            if (string.IsNullOrEmpty(json.reward_artifact1_iname) != null)
            {
                goto Label_004B;
            }
            if (json.reward_artifact1_num <= 0)
            {
                goto Label_004B;
            }
            item = new RewardItem();
            &item.iname = json.reward_artifact1_iname;
            &item.Num = json.reward_artifact1_num;
            list.Add(item);
        Label_004B:
            if (string.IsNullOrEmpty(json.reward_artifact2_iname) != null)
            {
                goto Label_0090;
            }
            if (json.reward_artifact2_num <= 0)
            {
                goto Label_0090;
            }
            item2 = new RewardItem();
            &item2.iname = json.reward_artifact2_iname;
            &item2.Num = json.reward_artifact2_num;
            list.Add(item2);
        Label_0090:
            if (string.IsNullOrEmpty(json.reward_artifact3_iname) != null)
            {
                goto Label_00D5;
            }
            if (json.reward_artifact3_num <= 0)
            {
                goto Label_00D5;
            }
            item3 = new RewardItem();
            &item3.iname = json.reward_artifact3_iname;
            &item3.Num = json.reward_artifact3_num;
            list.Add(item3);
        Label_00D5:
            return list.ToArray();
        }

        private static RewardItem[] InitializeConceptCards(JSON_TrophyParam json)
        {
            Action<string, int> action;
            <InitializeConceptCards>c__AnonStorey2EF storeyef;
            storeyef = new <InitializeConceptCards>c__AnonStorey2EF();
            storeyef.result = new List<RewardItem>();
            action = new Action<string, int>(storeyef.<>m__277);
            action(json.reward_cc_1_iname, json.reward_cc_1_num);
            action(json.reward_cc_2_iname, json.reward_cc_2_num);
            return storeyef.result.ToArray();
        }

        private static unsafe RewardItem[] InitializeItems(JSON_TrophyParam json)
        {
            List<RewardItem> list;
            RewardItem item;
            RewardItem item2;
            RewardItem item3;
            list = new List<RewardItem>();
            if (string.IsNullOrEmpty(json.reward_item1_iname) != null)
            {
                goto Label_004B;
            }
            if (json.reward_item1_num <= 0)
            {
                goto Label_004B;
            }
            item = new RewardItem();
            &item.iname = json.reward_item1_iname;
            &item.Num = json.reward_item1_num;
            list.Add(item);
        Label_004B:
            if (string.IsNullOrEmpty(json.reward_item2_iname) != null)
            {
                goto Label_0090;
            }
            if (json.reward_item2_num <= 0)
            {
                goto Label_0090;
            }
            item2 = new RewardItem();
            &item2.iname = json.reward_item2_iname;
            &item2.Num = json.reward_item2_num;
            list.Add(item2);
        Label_0090:
            if (string.IsNullOrEmpty(json.reward_item3_iname) != null)
            {
                goto Label_00D5;
            }
            if (json.reward_item3_num <= 0)
            {
                goto Label_00D5;
            }
            item3 = new RewardItem();
            &item3.iname = json.reward_item3_iname;
            &item3.Num = json.reward_item3_num;
            list.Add(item3);
        Label_00D5:
            return list.ToArray();
        }

        public bool IsAvailablePeriod(DateTime now, bool is_grace)
        {
            if (this.IsChallengeMission == null)
            {
                goto Label_0018;
            }
            return this.ChallengeCategoryParam.IsAvailablePeriod(now);
        Label_0018:
            if (this.CategoryParam != null)
            {
                goto Label_0025;
            }
            return 0;
        Label_0025:
            return this.CategoryParam.IsAvailablePeriod(now, is_grace);
        }

        public bool IsInvisibleCard()
        {
            PlayerData data;
            TrophyState[] stateArray;
            bool flag;
            int num;
            int num2;
            stateArray = MonoSingleton<GameManager>.Instance.Player.TrophyStates;
            flag = 0;
            num = ((int) this.Objectives.Length) - 1;
            goto Label_008A;
        Label_0024:
            if (this.Objectives[num].type == 14)
            {
                goto Label_003A;
            }
            return 0;
        Label_003A:
            flag = 1;
            num2 = ((int) stateArray.Length) - 1;
            goto Label_007E;
        Label_0048:
            if ((stateArray[num2].iname != this.iname) == null)
            {
                goto Label_0066;
            }
            goto Label_0078;
        Label_0066:
            if (stateArray[num2].IsCompleted != null)
            {
                goto Label_0076;
            }
            return 1;
        Label_0076:
            return 0;
        Label_0078:
            num2 -= 1;
        Label_007E:
            if (num2 >= 0)
            {
                goto Label_0048;
            }
            num -= 1;
        Label_008A:
            if (num >= 0)
            {
                goto Label_0024;
            }
            return flag;
        }

        public unsafe bool IsInvisibleStamina()
        {
            int num;
            List<int> list;
            int num2;
            int num3;
            int num4;
            int num5;
            List<int>.Enumerator enumerator;
            DateTime time;
            bool flag;
            num = &TimeManager.ServerTime.Hour;
            list = MonoSingleton<WatchManager>.Instance.GetMealHours();
            num2 = ((int) this.Objectives.Length) - 1;
            goto Label_00D7;
        Label_002A:
            if (this.Objectives[num2].type == 13)
            {
                goto Label_0040;
            }
            return 0;
        Label_0040:
            num3 = int.Parse(this.Objectives[num2].sval_base.Substring(0, 2));
            num4 = int.Parse(this.Objectives[num2].sval_base.Substring(3, 2));
            if (num3 > num)
            {
                goto Label_0086;
            }
            if (num >= num4)
            {
                goto Label_0086;
            }
            return 0;
        Label_0086:
            enumerator = list.GetEnumerator();
        Label_008E:
            try
            {
                goto Label_00B5;
            Label_0093:
                num5 = &enumerator.Current;
                if (num3 > num5)
                {
                    goto Label_00B5;
                }
                if (num5 >= num4)
                {
                    goto Label_00B5;
                }
                flag = 0;
                goto Label_00E0;
            Label_00B5:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0093;
                }
                goto Label_00D3;
            }
            finally
            {
            Label_00C6:
                ((List<int>.Enumerator) enumerator).Dispose();
            }
        Label_00D3:
            num2 -= 1;
        Label_00D7:
            if (num2 >= 0)
            {
                goto Label_002A;
            }
            return 1;
        Label_00E0:
            return flag;
        }

        public bool IsInvisibleVip()
        {
            PlayerData data;
            int num;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = ((int) this.Objectives.Length) - 1;
            goto Label_0093;
        Label_001B:
            if (this.Objectives[num].type == 12)
            {
                goto Label_0031;
            }
            return 0;
        Label_0031:
            if ((this.Objectives[num].sval_base != "lv") == null)
            {
                goto Label_004F;
            }
            return 0;
        Label_004F:
            if (data.VipRank <= 0)
            {
                goto Label_007A;
            }
            if (data.VipRank == this.Objectives[num].ival)
            {
                goto Label_008F;
            }
            return 1;
            goto Label_008F;
        Label_007A:
            if (this.Objectives[num].ival == 1)
            {
                goto Label_008F;
            }
            return 1;
        Label_008F:
            num -= 1;
        Label_0093:
            if (num >= 0)
            {
                goto Label_001B;
            }
            return 0;
        }

        public bool IsPlanningToUse()
        {
            DateTime time;
            DateTime time2;
            DateTime time3;
            if (this.IsChallengeMission == null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            if (this.CategoryParam != null)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            time = TimeManager.ServerTime;
            time2 = this.CategoryParam.begin_at.DateTimes;
            time3 = this.CategoryParam.end_at.DateTimes;
            time2 = this.SubTimeSpan(time2, this.GetAvailableSpan());
            time3 = this.AddTimeSpan(time3, this.GetGraceRewardSpan() + this.GetAvailableSpan());
            if ((time < time2) != null)
            {
                goto Label_0081;
            }
            if ((time3 < time) == null)
            {
                goto Label_0083;
            }
        Label_0081:
            return 0;
        Label_0083:
            return 1;
        }

        public bool IsShowBadge(TrophyState state)
        {
            if (state == null)
            {
                goto Label_001C;
            }
            if (state.IsEnded != null)
            {
                goto Label_001C;
            }
            if (state.IsCompleted != null)
            {
                goto Label_001E;
            }
        Label_001C:
            return 0;
        Label_001E:
            if (this.IsBeginner == null)
            {
                goto Label_003F;
            }
            if (MonoSingleton<GameManager>.Instance.Player.IsBeginner() != null)
            {
                goto Label_003F;
            }
            return 0;
        Label_003F:
            if (this.IsInvisibleVip() == null)
            {
                goto Label_004C;
            }
            return 0;
        Label_004C:
            if (this.IsInvisibleCard() == null)
            {
                goto Label_0059;
            }
            return 0;
        Label_0059:
            if (this.IsInvisibleStamina() == null)
            {
                goto Label_0066;
            }
            return 0;
        Label_0066:
            if (this.IsChallengeMission == null)
            {
                goto Label_0073;
            }
            return 0;
        Label_0073:
            if (state.Param.DispType == 2)
            {
                goto Label_0095;
            }
            if (state.Param.DispType != 1)
            {
                goto Label_0097;
            }
        Label_0095:
            return 0;
        Label_0097:
            if (state.Param.RequiredTrophies == null)
            {
                goto Label_00C0;
            }
            if (CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, state.Param, 1, 1) != null)
            {
                goto Label_00C0;
            }
            return 0;
        Label_00C0:
            if (state.Param.IsAvailablePeriod(TimeManager.ServerTime, 1) != null)
            {
                goto Label_00D8;
            }
            return 0;
        Label_00D8:
            return 1;
        }

        private unsafe DateTime SubTimeSpan(DateTime times, TimeSpan span)
        {
            if (&times.Equals(DateTime.MinValue) == null)
            {
                goto Label_0013;
            }
            return times;
        Label_0013:
            try
            {
                times = &times.Subtract(span);
                goto Label_002F;
            }
            catch (Exception)
            {
            Label_0022:
                times = DateTime.MaxValue;
                goto Label_002F;
            }
        Label_002F:
            return times;
        }

        public bool IsBeginner
        {
            get
            {
                if (this.CategoryParam == null)
                {
                    goto Label_0017;
                }
                return this.CategoryParam.IsBeginner;
            Label_0017:
                return 0;
            }
        }

        public bool IsChallengeMission
        {
            get
            {
                return (this.Challenge == 1);
            }
        }

        public bool IsChallengeMissionRoot
        {
            get
            {
                return ((this.IsChallengeMission == null) ? 0 : string.IsNullOrEmpty(this.ParentTrophy));
            }
        }

        public bool IsDaily
        {
            get
            {
                if (this.CategoryParam == null)
                {
                    goto Label_0017;
                }
                return this.CategoryParam.IsDaily;
            Label_0017:
                return 0;
            }
        }

        [CompilerGenerated]
        private sealed class <InitializeConceptCards>c__AnonStorey2EF
        {
            internal List<TrophyParam.RewardItem> result;

            public <InitializeConceptCards>c__AnonStorey2EF()
            {
                base..ctor();
                return;
            }

            internal unsafe void <>m__277(string iname, int num)
            {
                TrophyParam.RewardItem item;
                if (string.IsNullOrEmpty(iname) != null)
                {
                    goto Label_0036;
                }
                if (num <= 0)
                {
                    goto Label_0036;
                }
                item = new TrophyParam.RewardItem();
                &item.iname = iname;
                &item.Num = num;
                this.result.Add(item);
            Label_0036:
                return;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RewardItem
        {
            public string iname;
            public int Num;
        }
    }
}

