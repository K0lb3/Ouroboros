namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(12, "アイテムが選択された", 1, 0x66), Pin(11, "閲覧可能なストーリーがない", 1, 0x65), Pin(10, "閲覧可能なストーリーがある", 1, 100), Pin(0, "再読み込み", 0, 0)]
    public class ReplayQuestChapterList : MonoBehaviour, IFlowInterface
    {
        private const int PIN_ID_REFRESH = 0;
        private const int PIN_ID_EXIST = 10;
        private const int PIN_ID_NOT_EXIST = 11;
        private const int PIN_ID_SELECT = 12;
        public ListItemEvents ItemTemplate;
        public GameObject ItemContainer;
        public bool Descending;
        public GameObject BackToSection;
        public GameObject BackToCategories;
        public UnityEngine.UI.ScrollRect ScrollRect;
        private QuestParam[] questParams;
        private string sectionName;
        private List<ListItemEvents> mItems;

        public ReplayQuestChapterList()
        {
            this.Descending = 1;
            this.mItems = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_000C;
            }
            this.Refresh();
        Label_000C:
            return;
        }

        private void OnItemSelect(GameObject go)
        {
            ChapterParam param;
            param = DataSource.FindDataOfClass<ChapterParam>(go, null);
            if (param == null)
            {
                goto Label_0026;
            }
            GlobalVars.ReplaySelectedChapter.Set(param.iname);
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
        Label_0026:
            return;
        }

        private unsafe void Refresh()
        {
            GameManager manager;
            List<ReplayChapterParam> list;
            QuestParam[] paramArray;
            long num;
            ChapterParam param;
            ChapterParam[] paramArray2;
            int num2;
            bool flag;
            bool flag2;
            QuestParam param2;
            QuestParam[] paramArray3;
            int num3;
            ReplayChapterParam param3;
            ReplayChapterParam param4;
            List<ReplayChapterParam>.Enumerator enumerator;
            ChapterParam param5;
            ListItemEvents events;
            StringBuilder builder;
            ListItemEvents events2;
            bool flag3;
            SectionParam param6;
            SectionParam[] paramArray4;
            int num4;
            if (this.sectionName != null)
            {
                goto Label_0020;
            }
            this.sectionName = GlobalVars.ReplaySelectedSection;
            goto Label_003B;
        Label_0020:
            if (this.sectionName.Equals(GlobalVars.ReplaySelectedSection) == null)
            {
                goto Label_003B;
            }
            return;
        Label_003B:
            this.sectionName = GlobalVars.ReplaySelectedSection;
            GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
            this.mItems.Clear();
            if ((this.ItemTemplate == null) != null)
            {
                goto Label_0083;
            }
            if ((this.ItemContainer == null) == null)
            {
                goto Label_0084;
            }
        Label_0083:
            return;
        Label_0084:
            manager = MonoSingleton<GameManager>.Instance;
            list = new List<ReplayChapterParam>();
            paramArray = manager.Player.AvailableQuests;
            num = Network.GetServerTime();
            paramArray2 = manager.Chapters;
            num2 = 0;
            goto Label_01FD;
        Label_00B2:
            param = paramArray2[num2];
            flag = 0;
            flag2 = 0;
            paramArray3 = paramArray;
            num3 = 0;
            goto Label_0195;
        Label_00CA:
            param2 = paramArray3[num3];
            if ((param2.ChapterID != param.iname) == null)
            {
                goto Label_00EE;
            }
            goto Label_018F;
        Label_00EE:
            if (param2.IsMulti == null)
            {
                goto Label_00FF;
            }
            goto Label_018F;
        Label_00FF:
            if (param2.type != 13)
            {
                goto Label_0112;
            }
            goto Label_018F;
        Label_0112:
            if (param2.IsReplayDateUnlock(num) != null)
            {
                goto Label_0124;
            }
            goto Label_018F;
        Label_0124:
            if (string.IsNullOrEmpty(param2.event_start) == null)
            {
                goto Label_014B;
            }
            if (string.IsNullOrEmpty(param2.event_clear) == null)
            {
                goto Label_014B;
            }
            goto Label_018F;
        Label_014B:
            if (param2.state == 1)
            {
                goto Label_016A;
            }
            if (param2.state == 2)
            {
                goto Label_016A;
            }
            goto Label_018F;
        Label_016A:
            if (param2.replayLimit == null)
            {
                goto Label_0187;
            }
            if (param2.end <= 0L)
            {
                goto Label_0187;
            }
            flag2 = 1;
        Label_0187:
            flag = 1;
            goto Label_01A0;
        Label_018F:
            num3 += 1;
        Label_0195:
            if (num3 < ((int) paramArray3.Length))
            {
                goto Label_00CA;
            }
        Label_01A0:
            if (flag == null)
            {
                goto Label_01F7;
            }
            if (string.IsNullOrEmpty(GlobalVars.ReplaySelectedSection) != null)
            {
                goto Label_01D6;
            }
            if ((param.section == GlobalVars.ReplaySelectedSection) == null)
            {
                goto Label_01F7;
            }
        Label_01D6:
            param3 = new ReplayChapterParam();
            param3.chapterParam = param;
            param3.replayLimit = flag2;
            list.Add(param3);
        Label_01F7:
            num2 += 1;
        Label_01FD:
            if (num2 < ((int) paramArray2.Length))
            {
                goto Label_00B2;
            }
            if (this.Descending == null)
            {
                goto Label_0219;
            }
            list.Reverse();
        Label_0219:
            enumerator = list.GetEnumerator();
        Label_0221:
            try
            {
                goto Label_0310;
            Label_0226:
                param4 = &enumerator.Current;
                param5 = param4.chapterParam;
                events = null;
                if (string.IsNullOrEmpty(param5.prefabPath) != null)
                {
                    goto Label_027D;
                }
                builder = GameUtility.GetStringBuilder();
                builder.Append("QuestChapters/");
                builder.Append(param5.prefabPath);
                events = AssetManager.Load<ListItemEvents>(builder.ToString());
            Label_027D:
                if ((events == null) == null)
                {
                    goto Label_0292;
                }
                events = this.ItemTemplate;
            Label_0292:
                events2 = Object.Instantiate<ListItemEvents>(events);
                DataSource.Bind<ChapterParam>(events2.get_gameObject(), param5);
                events2.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
                events2.get_gameObject().SetActive(1);
                events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
                if (param5.end <= 0L)
                {
                    goto Label_0303;
                }
                this.SetTimerActive(events2.get_transform(), param4.replayLimit);
            Label_0303:
                this.mItems.Add(events2);
            Label_0310:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0226;
                }
                goto Label_032E;
            }
            finally
            {
            Label_0321:
                ((List<ReplayChapterParam>.Enumerator) enumerator).Dispose();
            }
        Label_032E:
            if ((this.BackToCategories != null) == null)
            {
                goto Label_03C1;
            }
            if ((this.BackToSection != null) == null)
            {
                goto Label_03C1;
            }
            flag3 = 0;
            paramArray4 = manager.Sections;
            num4 = 0;
            goto Label_0399;
        Label_0363:
            param6 = paramArray4[num4];
            if ((param6.iname == GlobalVars.ReplaySelectedSection) == null)
            {
                goto Label_0393;
            }
            flag3 = param6.hidden;
            goto Label_03A4;
        Label_0393:
            num4 += 1;
        Label_0399:
            if (num4 < ((int) paramArray4.Length))
            {
                goto Label_0363;
            }
        Label_03A4:
            this.BackToCategories.SetActive(flag3);
            this.BackToSection.SetActive(flag3 == 0);
        Label_03C1:
            if ((this.ScrollRect != null) == null)
            {
                goto Label_03E2;
            }
            this.ScrollRect.set_normalizedPosition(Vector2.get_one());
        Label_03E2:
            if (this.mItems.Count <= 0)
            {
                goto Label_0400;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_0408;
        Label_0400:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
        Label_0408:
            return;
        }

        private void SetTimerActive(Transform tr, bool value)
        {
            QuestTimeLimit limit;
            Transform transform;
            Transform transform2;
            if ((tr == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            limit = tr.GetComponent<QuestTimeLimit>();
            if ((limit != null) == null)
            {
                goto Label_0085;
            }
            if ((limit.Body != null) == null)
            {
                goto Label_0042;
            }
            limit.Body.SetActive(value);
            goto Label_007E;
        Label_0042:
            transform = tr.FindChild("bg");
            if ((transform != null) == null)
            {
                goto Label_007E;
            }
            transform2 = transform.FindChild("timer_base");
            if ((transform2 != null) == null)
            {
                goto Label_007E;
            }
            transform2.get_gameObject().SetActive(value);
        Label_007E:
            limit.set_enabled(value);
        Label_0085:
            return;
        }

        private void Start()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.ItemTemplate.get_gameObject().SetActive(0);
        Label_0022:
            this.Refresh();
            return;
        }

        private class ReplayChapterParam
        {
            internal ChapterParam chapterParam;
            internal bool replayLimit;

            public ReplayChapterParam()
            {
                base..ctor();
                return;
            }
        }
    }
}

