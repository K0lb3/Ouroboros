namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "Refresh Map", 0, 0), Pin(110, "Refresh Party", 0, 1)]
    public class RankMatchWindow : MonoBehaviour, IFlowInterface
    {
        public const int PINID_REFRESH_MAP = 100;
        public const int PINID_REFRESH_PARTY = 110;
        private const float UPDATE_WAIT_TIME = 1f;
        private float mWaitTime;
        private long mEndTime;
        [SerializeField]
        private GameObject PartyInfo;
        [SerializeField]
        private GameObject[] PartyUnitSlots;
        [SerializeField]
        private GameObject PartyUnitLeader;
        [SerializeField]
        private Text SeasonDateText;
        [SerializeField]
        private Text SeasonTimeText;
        [SerializeField, Space(10f)]
        private GameObject GoMapInfo;
        [SerializeField]
        private Text TextMapInfoSchedule;
        [SerializeField]
        private Text NextOpenDate;
        [SerializeField]
        private Text NextOpenTime;
        [SerializeField]
        private Text RemainTime;
        [SerializeField]
        private Text StreakWin;
        [SerializeField]
        private GameObject AwardItem;
        private bool mIsUpdateMapInfoEndAt;
        private float mPassedTimeMapInfoEndAt;

        public RankMatchWindow()
        {
            this.mWaitTime = 1f;
            this.PartyUnitSlots = new GameObject[5];
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 100)
            {
                goto Label_0017;
            }
            if (num == 110)
            {
                goto Label_0022;
            }
            goto Label_002D;
        Label_0017:
            this.RefreshMap();
            goto Label_002D;
        Label_0022:
            this.RefreshParty();
        Label_002D:
            return;
        }

        private unsafe void CountDown()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            DateTime time;
            DateTime time2;
            TimeSpan span;
            if (this.mEndTime > 0L)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            time = TimeManager.FromUnixTime(this.mEndTime);
            time2 = TimeManager.ServerTime;
            if ((time2 > time) == null)
            {
                goto Label_002D;
            }
            return;
        Label_002D:
            span = time - time2;
            if ((this.RemainTime != null) == null)
            {
                goto Label_00FE;
            }
            if (&span.TotalDays < 1.0)
            {
                goto Label_008A;
            }
            objArray1 = new object[] { (int) &span.Days };
            this.RemainTime.set_text(LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_DAY", objArray1));
            goto Label_00FE;
        Label_008A:
            if (&span.TotalHours < 1.0)
            {
                goto Label_00CE;
            }
            objArray2 = new object[] { (int) &span.Hours };
            this.RemainTime.set_text(LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_HOUR", objArray2));
            goto Label_00FE;
        Label_00CE:
            objArray3 = new object[] { (int) Mathf.Max(&span.Minutes, 0) };
            this.RemainTime.set_text(LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_MINUTE", objArray3));
        Label_00FE:
            this.mWaitTime = 1f;
            return;
        }

        private unsafe void RefreshMap()
        {
            GameManager manager;
            PlayerData data;
            DataSource source;
            QuestParam param;
            List<VersusEnableTimeScheduleParam> list;
            List<VersusEnableTimeScheduleParam> list2;
            int num;
            VersusEnableTimeScheduleParam param2;
            List<VersusEnableTimeScheduleParam>.Enumerator enumerator;
            DateTime time;
            List<DateTime>.Enumerator enumerator2;
            int num2;
            bool flag;
            int num3;
            VersusEnableTimeScheduleParam param3;
            List<VersusEnableTimeScheduleParam>.Enumerator enumerator3;
            DateTime time2;
            TimeSpan span;
            DateTime time3;
            int num4;
            int num5;
            VersusEnableTimeScheduleParam param4;
            VersusEnableTimeScheduleParam param5;
            List<VersusEnableTimeScheduleParam>.Enumerator enumerator4;
            DateTime time4;
            int num6;
            DateTime time5;
            int num7;
            VersusEnableTimeScheduleParam param6;
            List<VersusEnableTimeScheduleParam>.Enumerator enumerator5;
            DateTime time6;
            List<DateTime>.Enumerator enumerator6;
            int num8;
            DateTime time7;
            TimeSpan span2;
            DateTime time8;
            DateTime time9;
            DateTime time10;
            DateTime time11;
            DateTime time12;
            DateTime time13;
            DateTime time14;
            DateTime time15;
            DateTime time16;
            if (this.GoMapInfo == null)
            {
                goto Label_04BF;
            }
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            if (manager == null)
            {
                goto Label_007A;
            }
            if (data == null)
            {
                goto Label_007A;
            }
            source = this.GoMapInfo.GetComponent<DataSource>();
            if (source == null)
            {
                goto Label_004B;
            }
            source.Clear();
        Label_004B:
            param = manager.FindQuest(GlobalVars.SelectedQuestID);
            DataSource.Bind<QuestParam>(this.GoMapInfo, param);
            GameParameter.UpdateAll(this.GoMapInfo);
            this.mIsUpdateMapInfoEndAt = this.RefreshMapInfoEndAt();
        Label_007A:
            list = manager.GetVersusRankMapSchedule(manager.RankMatchScheduleId);
            if (list == null)
            {
                goto Label_04BF;
            }
            list2 = new List<VersusEnableTimeScheduleParam>();
            num = ((&TimeManager.ServerTime.Year * 0x2710) + (&TimeManager.ServerTime.Month * 100)) + &TimeManager.ServerTime.Day;
            enumerator = list.GetEnumerator();
        Label_00D6:
            try
            {
                goto Label_0182;
            Label_00DB:
                param2 = &enumerator.Current;
                if (param2.AddDateList == null)
                {
                    goto Label_0101;
                }
                if (param2.AddDateList.Count != null)
                {
                    goto Label_010F;
                }
            Label_0101:
                list2.Add(param2);
                goto Label_0182;
            Label_010F:
                enumerator2 = param2.AddDateList.GetEnumerator();
            Label_011D:
                try
                {
                    goto Label_0164;
                Label_0122:
                    time = &enumerator2.Current;
                    num2 = ((&time.Year * 0x2710) + (&time.Month * 100)) + &time.Day;
                    if (num != num2)
                    {
                        goto Label_0164;
                    }
                    list2.Add(param2);
                Label_0164:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_0122;
                    }
                    goto Label_0182;
                }
                finally
                {
                Label_0175:
                    ((List<DateTime>.Enumerator) enumerator2).Dispose();
                }
            Label_0182:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00DB;
                }
                goto Label_01A0;
            }
            finally
            {
            Label_0193:
                ((List<VersusEnableTimeScheduleParam>.Enumerator) enumerator).Dispose();
            }
        Label_01A0:
            flag = 0;
            num3 = (&TimeManager.ServerTime.Hour * 100) + &TimeManager.ServerTime.Minute;
            enumerator3 = list2.GetEnumerator();
        Label_01CE:
            try
            {
                goto Label_028F;
            Label_01D3:
                param3 = &enumerator3.Current;
                time2 = DateTime.Parse(&TimeManager.ServerTime.ToShortDateString() + " " + param3.Begin + ":00");
                span = TimeSpan.Parse(param3.Open);
                time3 = time2 + span;
                num4 = (&time2.Hour * 100) + &time2.Minute;
                num5 = (&time3.Hour * 100) + &time3.Minute;
                if (num4 > num3)
                {
                    goto Label_028F;
                }
                if (num3 >= num5)
                {
                    goto Label_028F;
                }
                this.TextMapInfoSchedule.set_text(&time2.ToString("HH:mm") + "-" + &time3.ToString("HH:mm"));
                flag = 1;
                goto Label_029B;
            Label_028F:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_01D3;
                }
            Label_029B:
                goto Label_02AD;
            }
            finally
            {
            Label_02A0:
                ((List<VersusEnableTimeScheduleParam>.Enumerator) enumerator3).Dispose();
            }
        Label_02AD:
            if (flag != null)
            {
                goto Label_04BF;
            }
            param4 = null;
            enumerator4 = list2.GetEnumerator();
        Label_02C0:
            try
            {
                goto Label_031F;
            Label_02C5:
                param5 = &enumerator4.Current;
                time4 = DateTime.Parse(&TimeManager.ServerTime.ToShortDateString() + " " + param5.Begin + ":00");
                num6 = (&time4.Hour * 100) + &time4.Minute;
                if (num6 <= num3)
                {
                    goto Label_031F;
                }
                param4 = param5;
                goto Label_032B;
            Label_031F:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_02C5;
                }
            Label_032B:
                goto Label_033D;
            }
            finally
            {
            Label_0330:
                ((List<VersusEnableTimeScheduleParam>.Enumerator) enumerator4).Dispose();
            }
        Label_033D:
            if (param4 != null)
            {
                goto Label_0447;
            }
            time5 = TimeManager.ServerTime;
            &time5.AddDays(1.0);
            num7 = ((&time5.Year * 0x2710) + (&time5.Month * 100)) + &time5.Day;
            enumerator5 = list.GetEnumerator();
        Label_0387:
            try
            {
                goto Label_0429;
            Label_038C:
                param6 = &enumerator5.Current;
                if (param6.AddDateList == null)
                {
                    goto Label_03B2;
                }
                if (param6.AddDateList.Count != null)
                {
                    goto Label_03BB;
                }
            Label_03B2:
                param4 = param6;
                goto Label_0435;
            Label_03BB:
                enumerator6 = param6.AddDateList.GetEnumerator();
            Label_03C9:
                try
                {
                    goto Label_040B;
                Label_03CE:
                    time6 = &enumerator6.Current;
                    num8 = ((&time6.Year * 0x2710) + (&time6.Month * 100)) + &time6.Day;
                    if (num7 != num8)
                    {
                        goto Label_040B;
                    }
                    param4 = param6;
                    goto Label_0417;
                Label_040B:
                    if (&enumerator6.MoveNext() != null)
                    {
                        goto Label_03CE;
                    }
                Label_0417:
                    goto Label_0429;
                }
                finally
                {
                Label_041C:
                    ((List<DateTime>.Enumerator) enumerator6).Dispose();
                }
            Label_0429:
                if (&enumerator5.MoveNext() != null)
                {
                    goto Label_038C;
                }
            Label_0435:
                goto Label_0447;
            }
            finally
            {
            Label_043A:
                ((List<VersusEnableTimeScheduleParam>.Enumerator) enumerator5).Dispose();
            }
        Label_0447:
            if (param4 == null)
            {
                goto Label_04BF;
            }
            time7 = DateTime.Parse(&TimeManager.ServerTime.ToShortDateString() + " " + param4.Begin + ":00");
            span2 = TimeSpan.Parse(param4.Open);
            time8 = time7 + span2;
            this.TextMapInfoSchedule.set_text(&time7.ToString("HH:mm") + "-" + &time8.ToString("HH:mm"));
        Label_04BF:
            return;
        }

        private unsafe bool RefreshMapInfoEndAt()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            GameManager manager;
            PlayerData data;
            bool flag;
            DateTime time;
            TimeSpan span;
            bool flag2;
            string str;
            string str2;
            manager = MonoSingleton<GameManager>.Instance;
            if (manager != null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            data = manager.Player;
            if (data != null)
            {
                goto Label_0022;
            }
            return 0;
        Label_0022:
            flag = 0;
            time = TimeManager.ServerTime;
            span = data.ArenaEndAt - time;
            flag2 = data.ArenaEndAt > GameUtility.UnixtimeToLocalTime(0L);
            if (flag2 == null)
            {
                goto Label_006D;
            }
            if (&span.TotalSeconds >= 0.0)
            {
                goto Label_006D;
            }
            flag2 = 0;
            flag = 1;
        Label_006D:
            if (flag2 != null)
            {
                goto Label_0087;
            }
            if (flag == null)
            {
                goto Label_0085;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "REFRESH_ARENA_INFO");
        Label_0085:
            return 0;
        Label_0087:
            str = "sys.ARENA_TIMELIMIT_";
            str2 = string.Empty;
            if (&span.Days == null)
            {
                goto Label_00CE;
            }
            objArray1 = new object[] { (int) &span.Days };
            str2 = LocalizedText.Get(str + "D", objArray1);
            goto Label_0135;
        Label_00CE:
            if (&span.Hours == null)
            {
                goto Label_0107;
            }
            objArray2 = new object[] { (int) &span.Hours };
            str2 = LocalizedText.Get(str + "H", objArray2);
            goto Label_0135;
        Label_0107:
            objArray3 = new object[] { (int) Mathf.Max(&span.Minutes, 0) };
            str2 = LocalizedText.Get(str + "M", objArray3);
        Label_0135:
            if (this.TextMapInfoSchedule == null)
            {
                goto Label_0169;
            }
            if ((this.TextMapInfoSchedule.get_text() != str2) == null)
            {
                goto Label_0169;
            }
            this.TextMapInfoSchedule.set_text(str2);
        Label_0169:
            this.mPassedTimeMapInfoEndAt = 1f;
            return 1;
        }

        private unsafe void RefreshParty()
        {
            int num;
            List<PartyEditData> list;
            PartyEditData data;
            VersusRankParam param;
            int num2;
            UnitData data2;
            JobData data3;
            UnitData data4;
            PlayerData data5;
            DateTime time;
            DateTime time2;
            MultiPlayVersusEdit edit;
            int num3;
            DateTime time3;
            DateTime time4;
            data = PartyUtility.LoadTeamPresets(10, &num, 0)[num];
            param = MonoSingleton<GameManager>.Instance.GetVersusRankParam(MonoSingleton<GameManager>.Instance.RankMatchScheduleId);
            if (param != null)
            {
                goto Label_002F;
            }
            return;
        Label_002F:
            num2 = 0;
            goto Label_00FD;
        Label_0037:
            if ((num2 + 1) > ((int) data.Units.Length))
            {
                goto Label_00F7;
            }
            if (data.Units[num2] != null)
            {
                goto Label_005B;
            }
            goto Label_00F7;
        Label_005B:
            data2 = data.Units[num2];
            if (data2.GetJobFor(10) == data2.CurrentJob)
            {
                goto Label_00AB;
            }
            data4 = new UnitData();
            data4.TempFlags |= 1;
            data4.Setup(data2);
            data4.SetJob(10);
            data2 = data4;
        Label_00AB:
            data2.TempFlags |= 2;
            if (num2 != null)
            {
                goto Label_00D9;
            }
            DataSource.Bind<UnitData>(this.PartyUnitLeader, data2);
            GameParameter.UpdateAll(this.PartyUnitLeader);
        Label_00D9:
            DataSource.Bind<UnitData>(this.PartyUnitSlots[num2], data2);
            GameParameter.UpdateAll(this.PartyUnitSlots[num2]);
        Label_00F7:
            num2 += 1;
        Label_00FD:
            if (num2 >= ((int) this.PartyUnitSlots.Length))
            {
                goto Label_011E;
            }
            if (num2 < data.PartyData.VSWAITMEMBER_START)
            {
                goto Label_0037;
            }
        Label_011E:
            if ((this.PartyInfo != null) == null)
            {
                goto Label_014B;
            }
            DataSource.Bind<PartyData>(this.PartyInfo, data.PartyData);
            GameParameter.UpdateAll(this.PartyInfo);
        Label_014B:
            DataSource.Bind<PlayerPartyTypes>(base.get_gameObject(), 10);
            data5 = MonoSingleton<GameManager>.Instance.Player;
            if ((this.AwardItem != null) == null)
            {
                goto Label_0182;
            }
            DataSource.Bind<PlayerData>(this.AwardItem, data5);
        Label_0182:
            if ((this.StreakWin != null) == null)
            {
                goto Label_01DB;
            }
            if (data5.RankMatchStreakWin <= 1)
            {
                goto Label_01C0;
            }
            this.StreakWin.set_text(&data5.RankMatchStreakWin.ToString());
            goto Label_01DB;
        Label_01C0:
            this.StreakWin.get_transform().get_parent().get_gameObject().SetActive(0);
        Label_01DB:
            if ((this.NextOpenDate != null) == null)
            {
                goto Label_024A;
            }
            if (MonoSingleton<GameManager>.Instance.RankMatchNextTime != null)
            {
                goto Label_0211;
            }
            this.NextOpenDate.get_gameObject().SetActive(0);
            goto Label_024A;
        Label_0211:
            time = TimeManager.FromUnixTime(MonoSingleton<GameManager>.Instance.RankMatchNextTime);
            this.NextOpenDate.get_gameObject().SetActive(1);
            this.NextOpenDate.set_text(&time.ToString("MM/dd"));
        Label_024A:
            if ((this.NextOpenTime != null) == null)
            {
                goto Label_02A7;
            }
            if (MonoSingleton<GameManager>.Instance.RankMatchNextTime != null)
            {
                goto Label_027F;
            }
            this.NextOpenTime.set_text("--");
            goto Label_02A7;
        Label_027F:
            time2 = TimeManager.FromUnixTime(MonoSingleton<GameManager>.Instance.RankMatchNextTime);
            this.NextOpenTime.set_text(&time2.ToString("HH:mm"));
        Label_02A7:
            if ((this.SeasonDateText != null) == null)
            {
                goto Label_02D7;
            }
            this.SeasonDateText.set_text(&param.EndAt.ToString("MM/dd"));
        Label_02D7:
            if ((this.SeasonTimeText != null) == null)
            {
                goto Label_0307;
            }
            this.SeasonTimeText.set_text(&param.EndAt.ToString("HH:mm"));
        Label_0307:
            edit = base.GetComponent<MultiPlayVersusEdit>();
            if ((edit != null) == null)
            {
                goto Label_0323;
            }
            edit.Set();
        Label_0323:
            return;
        }

        private void Start()
        {
            this.RefreshParty();
            this.mEndTime = MonoSingleton<GameManager>.Instance.RankMatchExpiredTime;
            this.CountDown();
            return;
        }

        private void Update()
        {
            this.mWaitTime -= Time.get_deltaTime();
            if (this.mWaitTime >= 0f)
            {
                goto Label_0028;
            }
            this.CountDown();
        Label_0028:
            this.UpdateMapInfoEndAt();
            return;
        }

        private void UpdateMapInfoEndAt()
        {
            if (this.mIsUpdateMapInfoEndAt != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mPassedTimeMapInfoEndAt <= 0f)
            {
                goto Label_003F;
            }
            this.mPassedTimeMapInfoEndAt -= Time.get_fixedDeltaTime();
            if (this.mPassedTimeMapInfoEndAt <= 0f)
            {
                goto Label_003F;
            }
            return;
        Label_003F:
            this.mIsUpdateMapInfoEndAt = this.RefreshMapInfoEndAt();
            return;
        }
    }
}

