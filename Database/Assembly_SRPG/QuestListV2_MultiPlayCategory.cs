namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "イベントクエストを表示", 0, 1), Pin(2, "GPSクエストを含めて表示", 0, 1), Pin(0x65, "選択された", 1, 0x65), AddComponentMenu("Multi/クエストカテゴリー一覧"), Pin(0, "通常クエストを表示", 0, 0)]
    public class QuestListV2_MultiPlayCategory : MonoBehaviour, IFlowInterface
    {
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public ListItemEvents ItemTemplate;
        [Description("詳細画面として使用するゲームオブジェクト")]
        public GameObject DetailTemplate;
        private GameObject mDetailInfo;
        public UnityEngine.UI.ScrollRect ScrollRect;

        public QuestListV2_MultiPlayCategory()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0013;
            }
            this.Refresh(0, 0);
            goto Label_0036;
        Label_0013:
            if (pinID != 1)
            {
                goto Label_0027;
            }
            this.Refresh(1, 0);
            goto Label_0036;
        Label_0027:
            if (pinID != 2)
            {
                goto Label_0036;
            }
            this.Refresh(0, 1);
        Label_0036:
            return;
        }

        private void Awake()
        {
            GlobalVars.SelectedMultiPlayQuestIsEvent = 0;
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_003D;
            }
            if (this.ItemTemplate.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_003D;
            }
            this.ItemTemplate.get_gameObject().SetActive(0);
        Label_003D:
            if ((this.DetailTemplate != null) == null)
            {
                goto Label_006A;
            }
            if (this.DetailTemplate.get_activeInHierarchy() == null)
            {
                goto Label_006A;
            }
            this.DetailTemplate.SetActive(0);
        Label_006A:
            return;
        }

        private void OnCloseItemDetail(GameObject go)
        {
            if ((this.mDetailInfo != null) == null)
            {
                goto Label_0028;
            }
            Object.DestroyImmediate(this.mDetailInfo.get_gameObject());
            this.mDetailInfo = null;
        Label_0028:
            return;
        }

        private void OnOpenItemDetail(GameObject go)
        {
            QuestParam param;
            param = DataSource.FindDataOfClass<QuestParam>(go, null);
            if ((this.mDetailInfo == null) == null)
            {
                goto Label_003C;
            }
            if (param == null)
            {
                goto Label_003C;
            }
            this.mDetailInfo = Object.Instantiate<GameObject>(this.DetailTemplate);
            DataSource.Bind<QuestParam>(this.mDetailInfo, param);
        Label_003C:
            return;
        }

        private void OnSelectItem(GameObject go)
        {
            ChapterParam param;
            GameManager manager;
            QuestParam[] paramArray;
            long num;
            int num2;
            int num3;
            int num4;
            param = DataSource.FindDataOfClass<ChapterParam>(go, null);
            if (param == null)
            {
                goto Label_00E2;
            }
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            num = Network.GetServerTime();
            num2 = 0;
            num3 = 0;
            num4 = 0;
            goto Label_008A;
        Label_0034:
            if ((paramArray[num4].ChapterID == param.iname) == null)
            {
                goto Label_0084;
            }
            if (paramArray[num4].IsMulti == null)
            {
                goto Label_0084;
            }
            num2 += 1;
            if (paramArray[num4].IsJigen == null)
            {
                goto Label_0084;
            }
            if (paramArray[num4].IsDateUnlock(num) != null)
            {
                goto Label_0084;
            }
            num3 += 1;
        Label_0084:
            num4 += 1;
        Label_008A:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_0034;
            }
            if (num2 <= 0)
            {
                goto Label_00BB;
            }
            if (num2 != num3)
            {
                goto Label_00BB;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), null, null, 0, -1);
            return;
        Label_00BB:
            GlobalVars.SelectedMultiPlayArea = param.iname;
            DebugUtility.Log("Select Play Area:" + GlobalVars.SelectedMultiPlayArea);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
        Label_00E2:
            return;
        }

        private void Refresh(bool isEvent, DISPLAY_QUEST_TYPE type)
        {
            ListExtras extras;
            GlobalVars.SelectedMultiPlayQuestIsEvent = isEvent;
            this.RefreshItems(type);
            if ((this.ScrollRect != null) == null)
            {
                goto Label_0056;
            }
            extras = this.ScrollRect.GetComponent<ListExtras>();
            if ((extras != null) == null)
            {
                goto Label_0046;
            }
            extras.SetScrollPos(1f);
            goto Label_0056;
        Label_0046:
            this.ScrollRect.set_normalizedPosition(Vector2.get_one());
        Label_0056:
            return;
        }

        private void RefreshItems(DISPLAY_QUEST_TYPE dispMode)
        {
            Transform transform;
            DateTime time;
            int num;
            Transform transform2;
            List<ChapterParam> list;
            int num2;
            QuestParam param;
            int num3;
            ListItemEvents events;
            int num4;
            StringBuilder builder;
            ListItemEvents events2;
            Transform transform3;
            Transform transform4;
            <RefreshItems>c__AnonStorey387 storey;
            <RefreshItems>c__AnonStorey386 storey2;
            storey = new <RefreshItems>c__AnonStorey387();
            transform = base.get_transform();
            time = TimeManager.ServerTime;
            num = transform.get_childCount() - 1;
            goto Label_005A;
        Label_0022:
            transform2 = transform.GetChild(num);
            if ((transform2 == null) == null)
            {
                goto Label_003B;
            }
            goto Label_0056;
        Label_003B:
            if (transform2.get_gameObject().get_activeSelf() == null)
            {
                goto Label_0056;
            }
            Object.DestroyImmediate(transform2.get_gameObject());
        Label_0056:
            num -= 1;
        Label_005A:
            if (num >= 0)
            {
                goto Label_0022;
            }
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0073;
            }
            return;
        Label_0073:
            list = new List<ChapterParam>();
            num2 = 0;
            goto Label_01A1;
        Label_0082:
            storey2 = new <RefreshItems>c__AnonStorey386();
            param = MonoSingleton<GameManager>.Instance.Quests[num2];
            if (param == null)
            {
                goto Label_019B;
            }
            if (param.type == 1)
            {
                goto Label_00BD;
            }
            if (param.IsMultiAreaQuest != null)
            {
                goto Label_00BD;
            }
            goto Label_019B;
        Label_00BD:
            if (param.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent)
            {
                goto Label_00D3;
            }
            goto Label_019B;
        Label_00D3:
            if (param.IsMultiVersus == (GlobalVars.SelectedMultiPlayRoomType == 1))
            {
                goto Label_00EC;
            }
            goto Label_019B;
        Label_00EC:
            if (param.IsMultiAreaQuest == null)
            {
                goto Label_00FF;
            }
            if (dispMode != 1)
            {
                goto Label_019B;
            }
        Label_00FF:
            if (param.IsMultiAreaQuest == null)
            {
                goto Label_011C;
            }
            if (param.gps_enable != null)
            {
                goto Label_011C;
            }
            goto Label_019B;
        Label_011C:
            if (param.IsDateUnlock(-1L) != null)
            {
                goto Label_012F;
            }
            goto Label_019B;
        Label_012F:
            storey2.area = MonoSingleton<GameManager>.Instance.FindArea(param.ChapterID);
            if (storey2.area != null)
            {
                goto Label_0158;
            }
            goto Label_019B;
        Label_0158:
            if (storey2.area.IsAvailable(time) != null)
            {
                goto Label_016F;
            }
            goto Label_019B;
        Label_016F:
            if (list.Find(new Predicate<ChapterParam>(storey2.<>m__3D0)) == null)
            {
                goto Label_018D;
            }
            goto Label_019B;
        Label_018D:
            list.Add(storey2.area);
        Label_019B:
            num2 += 1;
        Label_01A1:
            if (num2 < ((int) MonoSingleton<GameManager>.Instance.Quests.Length))
            {
                goto Label_0082;
            }
            storey.indexList = new Dictionary<string, int>();
            num3 = 0;
            goto Label_01EA;
        Label_01C8:
            storey.indexList.Add(list[num3].iname, num3);
            num3 += 1;
        Label_01EA:
            if (num3 < list.Count)
            {
                goto Label_01C8;
            }
            list.Sort(new Comparison<ChapterParam>(storey.<>m__3D1));
            events = null;
            num4 = 0;
            goto Label_0354;
        Label_0217:
            events = null;
            if (string.IsNullOrEmpty(list[num4].prefabPath) != null)
            {
                goto Label_026A;
            }
            builder = GameUtility.GetStringBuilder();
            builder.Append("QuestChapters/");
            builder.Append(list[num4].prefabPath);
            events = AssetManager.Load<ListItemEvents>(builder.ToString());
        Label_026A:
            if ((events == null) == null)
            {
                goto Label_027F;
            }
            events = this.ItemTemplate;
        Label_027F:
            events2 = Object.Instantiate<ListItemEvents>(events);
            DataSource.Bind<ChapterParam>(events2.get_gameObject(), list[num4]);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            events2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            events2.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
            transform3 = events2.get_transform().FindChild("bg");
            if ((transform3 != null) == null)
            {
                goto Label_0333;
            }
            transform4 = transform3.FindChild("timer_base");
            if ((transform4 != null) == null)
            {
                goto Label_0333;
            }
            if (list[num4].end > 0L)
            {
                goto Label_0333;
            }
            transform4.get_gameObject().SetActive(0);
        Label_0333:
            events2.get_transform().SetParent(transform, 0);
            events2.get_gameObject().SetActive(1);
            num4 += 1;
        Label_0354:
            if (num4 < list.Count)
            {
                goto Label_0217;
            }
            return;
        }

        private void Start()
        {
        }

        [CompilerGenerated]
        private sealed class <RefreshItems>c__AnonStorey386
        {
            internal ChapterParam area;

            public <RefreshItems>c__AnonStorey386()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3D0(ChapterParam a)
            {
                return a.iname.Equals(this.area.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshItems>c__AnonStorey387
        {
            internal Dictionary<string, int> indexList;

            public <RefreshItems>c__AnonStorey387()
            {
                base..ctor();
                return;
            }

            internal int <>m__3D1(ChapterParam x, ChapterParam y)
            {
                bool flag;
                bool flag2;
                flag = x.IsMultiGpsQuest();
                flag2 = y.IsMultiGpsQuest();
                if (flag == null)
                {
                    goto Label_001C;
                }
                if (flag2 != null)
                {
                    goto Label_001C;
                }
                return -1;
            Label_001C:
                if (flag != null)
                {
                    goto Label_002A;
                }
                if (flag2 == null)
                {
                    goto Label_002A;
                }
                return 1;
            Label_002A:
                if (this.indexList.ContainsKey(x.iname) == null)
                {
                    goto Label_0056;
                }
                if (this.indexList.ContainsKey(y.iname) != null)
                {
                    goto Label_0058;
                }
            Label_0056:
                return 0;
            Label_0058:
                return (this.indexList[x.iname] - this.indexList[y.iname]);
            }
        }

        private enum DISPLAY_QUEST_TYPE
        {
            Normal,
            WithGps,
            Max
        }
    }
}

