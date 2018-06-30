namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0, "初期化", 0, 1), Pin(13, "Movie", 1, 0x67), Pin(12, "Chara", 1, 0x66), Pin(11, "Event", 1, 0x65), Pin(10, "Story", 1, 100), Pin(50, "QuestList(Restore)", 1, 0x69), Pin(1, "更新", 0, 2), Pin(14, "閲覧可能なストーリーがない", 1, 0x68)]
    public class ReplayCategoryList : SRPG_ListBase, IFlowInterface
    {
        private const int PIN_ID_INIT = 0;
        private const int PIN_ID_REFRESH = 1;
        private const int PIN_ID_STORY = 10;
        private const int PIN_ID_EVENT = 11;
        private const int PIN_ID_CHARA = 12;
        private const int PIN_ID_MOVIE = 13;
        private const int PIN_ID_NOT_EXIST = 14;
        private const int PIN_ID_RESTORE = 50;
        public ListItemEvents Item_Story;
        public ListItemEvents Item_Event;
        public ListItemEvents Item_Character;
        public ListItemEvents Item_Movie;
        public CanvasGroup mCanvasGroup;
        private List<int> QuestCount;
        [CompilerGenerated]
        private static Action<ListItemEvents, bool> <>f__am$cache6;

        public ReplayCategoryList()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <Awake>m__3F0(ListItemEvents lie, bool value)
        {
            if ((lie != null) == null)
            {
                goto Label_0028;
            }
            if (lie.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0028;
            }
            lie.get_gameObject().SetActive(value);
        Label_0028:
            return;
        }

        public void Activated(int pinID)
        {
            QuestParam param;
            if (pinID != null)
            {
                goto Label_0051;
            }
            this.Initialize();
            this.Refresh();
            if (HomeWindow.GetRestorePoint() != 7)
            {
                goto Label_0046;
            }
            param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.ReplaySelectedQuestID);
            if (this.Resumable(param) == null)
            {
                goto Label_0046;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 50);
        Label_0046:
            HomeWindow.SetRestorePoint(0);
            goto Label_005E;
        Label_0051:
            if (pinID != 1)
            {
                goto Label_005E;
            }
            this.Refresh();
        Label_005E:
            return;
        }

        private void AddQuestCount(ReplayCategoryType type)
        {
            int num;
            Exception exception;
            List<int> list;
            int num2;
            num = type;
        Label_0002:
            try
            {
                num2 = list[num2];
                (list = this.QuestCount)[num2 = num] = num2 + 1;
                goto Label_0033;
            }
            catch (Exception exception1)
            {
            Label_0022:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0033;
            }
        Label_0033:
            return;
        }

        private void Awake()
        {
            Action<ListItemEvents, bool> action;
            if (<>f__am$cache6 != null)
            {
                goto Label_0018;
            }
            <>f__am$cache6 = new Action<ListItemEvents, bool>(ReplayCategoryList.<Awake>m__3F0);
        Label_0018:
            action = <>f__am$cache6;
            action(this.Item_Story, 0);
            action(this.Item_Event, 0);
            action(this.Item_Character, 0);
            action(this.Item_Movie, 0);
            return;
        }

        private int GetQuestCount(ReplayCategoryType type)
        {
            int num;
            Exception exception;
            int num2;
            num = type;
        Label_0002:
            try
            {
                num2 = this.QuestCount[num];
                goto Label_002C;
            }
            catch (Exception exception1)
            {
            Label_0019:
                exception = exception1;
                DebugUtility.LogException(exception);
                num2 = 0;
                goto Label_002C;
            }
        Label_002C:
            return num2;
        }

        private void Initialize()
        {
            int num;
            QuestParam[] paramArray;
            long num2;
            QuestParam param;
            QuestParam[] paramArray2;
            int num3;
            this.QuestCount = new List<int>(Enum.GetValues(typeof(ReplayCategoryType)).Length);
            num = 0;
            goto Label_0036;
        Label_0026:
            this.QuestCount.Add(0);
            num += 1;
        Label_0036:
            if (num < this.QuestCount.Capacity)
            {
                goto Label_0026;
            }
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            num2 = Network.GetServerTime();
            paramArray2 = paramArray;
            num3 = 0;
            goto Label_012E;
        Label_0068:
            param = paramArray2[num3];
            if (string.IsNullOrEmpty(param.ChapterID) == null)
            {
                goto Label_0083;
            }
            goto Label_0128;
        Label_0083:
            if (param.IsMulti == null)
            {
                goto Label_0093;
            }
            goto Label_0128;
        Label_0093:
            if (param.IsReplayDateUnlock(num2) != null)
            {
                goto Label_00A4;
            }
            goto Label_0128;
        Label_00A4:
            if (string.IsNullOrEmpty(param.event_start) == null)
            {
                goto Label_00C9;
            }
            if (string.IsNullOrEmpty(param.event_clear) == null)
            {
                goto Label_00C9;
            }
            goto Label_0128;
        Label_00C9:
            if (param.state == 1)
            {
                goto Label_00E6;
            }
            if (param.state == 2)
            {
                goto Label_00E6;
            }
            goto Label_0128;
        Label_00E6:
            if (param.type != null)
            {
                goto Label_00FD;
            }
            this.AddQuestCount(0);
            goto Label_0128;
        Label_00FD:
            if (param.type != 5)
            {
                goto Label_0115;
            }
            this.AddQuestCount(1);
            goto Label_0128;
        Label_0115:
            if (param.type != 6)
            {
                goto Label_0128;
            }
            this.AddQuestCount(2);
        Label_0128:
            num3 += 1;
        Label_012E:
            if (num3 < ((int) paramArray2.Length))
            {
                goto Label_0068;
            }
            return;
        }

        private void OnItemSelect(GameObject go)
        {
            int num;
            int num2;
            num = DataSource.FindDataOfClass<int>(go, 10);
            num2 = num;
            switch ((num2 - 10))
            {
                case 0:
                    goto Label_0029;

                case 1:
                    goto Label_006D;

                case 2:
                    goto Label_00B1;

                case 3:
                    goto Label_00F5;
            }
            goto Label_00FD;
        Label_0029:
            GlobalVars.ReplaySelectedSection.Set(string.Empty);
            GlobalVars.ReplaySelectedChapter.Set(string.Empty);
            if (this.GetQuestCount(0) <= 0)
            {
                goto Label_0060;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, num);
            goto Label_0068;
        Label_0060:
            FlowNode_GameObject.ActivateOutputLinks(this, 14);
        Label_0068:
            goto Label_00FE;
        Label_006D:
            GlobalVars.ReplaySelectedSection.Set("WD_DAILY");
            GlobalVars.ReplaySelectedChapter.Set(string.Empty);
            if (this.GetQuestCount(1) <= 0)
            {
                goto Label_00A4;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, num);
            goto Label_00AC;
        Label_00A4:
            FlowNode_GameObject.ActivateOutputLinks(this, 14);
        Label_00AC:
            goto Label_00FE;
        Label_00B1:
            GlobalVars.ReplaySelectedSection.Set("WD_CHARA");
            GlobalVars.ReplaySelectedChapter.Set(string.Empty);
            if (this.GetQuestCount(2) <= 0)
            {
                goto Label_00E8;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, num);
            goto Label_00F0;
        Label_00E8:
            FlowNode_GameObject.ActivateOutputLinks(this, 14);
        Label_00F0:
            goto Label_00FE;
        Label_00F5:
            FlowNode_GameObject.ActivateOutputLinks(this, num);
            return;
        Label_00FD:
            return;
        Label_00FE:
            return;
        }

        private void Refresh()
        {
            Action<ListItemEvents, int> action;
            <Refresh>c__AnonStorey39B storeyb;
            storeyb = new <Refresh>c__AnonStorey39B();
            storeyb.<>f__this = this;
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_002E;
            }
            this.mCanvasGroup.set_alpha(0f);
        Label_002E:
            base.ClearItems();
            storeyb._transform = base.get_transform();
            action = new Action<ListItemEvents, int>(storeyb.<>m__3F1);
            action(this.Item_Story, 10);
            action(this.Item_Event, 11);
            action(this.Item_Character, 12);
            action(this.Item_Movie, 13);
            return;
        }

        private bool Resumable(QuestParam quest)
        {
            if (quest != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (quest.IsMulti == null)
            {
                goto Label_0015;
            }
            return 0;
        Label_0015:
            if (quest.IsReplayDateUnlock(Network.GetServerTime()) != null)
            {
                goto Label_0027;
            }
            return 0;
        Label_0027:
            if (string.IsNullOrEmpty(quest.event_start) == null)
            {
                goto Label_0049;
            }
            if (string.IsNullOrEmpty(quest.event_clear) == null)
            {
                goto Label_0049;
            }
            return 0;
        Label_0049:
            if (quest.state == 1)
            {
                goto Label_0063;
            }
            if (quest.state == 2)
            {
                goto Label_0063;
            }
            return 0;
        Label_0063:
            return 1;
        }

        private void Update()
        {
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_004D;
            }
            if (this.mCanvasGroup.get_alpha() >= 1f)
            {
                goto Label_004D;
            }
            this.mCanvasGroup.set_alpha(Mathf.Clamp01(this.mCanvasGroup.get_alpha() + (Time.get_unscaledDeltaTime() * 3.333333f)));
        Label_004D:
            return;
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey39B
        {
            internal Transform _transform;
            internal ReplayCategoryList <>f__this;

            public <Refresh>c__AnonStorey39B()
            {
                base..ctor();
                return;
            }

            internal void <>m__3F1(ListItemEvents lie, int outPinID)
            {
                ListItemEvents events;
                if ((lie == null) == null)
                {
                    goto Label_000D;
                }
                return;
            Label_000D:
                events = Object.Instantiate<ListItemEvents>(lie);
                events.get_transform().SetParent(this._transform, 0);
                events.OnSelect = new ListItemEvents.ListItemEvent(this.<>f__this.OnItemSelect);
                events.get_gameObject().SetActive(1);
                DataSource.Bind<int>(events.get_gameObject(), outPinID);
                this.<>f__this.AddItem(events);
                return;
            }
        }

        private enum ReplayCategoryType
        {
            Story,
            Event,
            Character,
            Movie
        }
    }
}

