namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(2, "リスト階層を戻る", 0, 1), Pin(1, "リスト要素を選択", 0, 0), Pin(0, "再読み込み", 0, 40), Pin(100, "クエストが選択された", 1, 100), Pin(50, "再読み込み完了", 1, 50), Pin(0x65, "クエストのアンロック", 1, 0x65), Pin(200, "塔が選択された", 1, 200), Pin(0xc9, "マルチ塔が選択された", 1, 0xc9), Pin(6, "塔チャプター切り替え", 0, 5), Pin(5, "GPSチャプター切り替え", 0, 4), Pin(4, "鍵チャプター切り替え", 0, 3), Pin(7, "更新", 0, 6), Pin(8, "ランキングクエストへチャプター切り替え", 0, 7), Pin(9, "UIの更新", 0, 8), Pin(3, "通常チャプター切り替え", 0, 2)]
    public class EventQuestList : MonoBehaviour, IFlowInterface
    {
        public GameObject ItemTemplate;
        public GameObject ItemContainer;
        public bool Descending;
        public bool RefreshOnStart;
        public RectTransform SwitchParent;
        public Button EventQuestButton;
        public Button KeyQuestButton;
        public Button TowerQuestButton;
        public GameObject KeyQuestOpenEffect;
        public GameObject QuestTypeTextFrame;
        public Text QuestTypeText;
        public GameObject TabTriple;
        public GameObject TabDouble;
        public Toggle[] TripleTabPages;
        public Toggle[] DoubleTabPages;
        public GameObject BackButton;
        public GameObject Caution;
        public Text CautionText;
        public Vector2 DefaultScrollPosition;
        public GameObject[] DisabledInBeginnerQuest;
        private List<ListItemEvents> mItems;
        private int mTabIndex;
        private GameObject mCurrentTab;
        private Toggle[] mCurrentTabPages;
        private EventQuestTypes mEventType;
        private bool mForceChangeTab;
        [CompilerGenerated]
        private static Comparison<ListItemEvents> <>f__am$cache1A;

        public EventQuestList()
        {
            this.Descending = 1;
            this.RefreshOnStart = 1;
            this.TripleTabPages = new Toggle[0];
            this.DoubleTabPages = new Toggle[0];
            this.DefaultScrollPosition = new Vector2(0f, 1f);
            this.DisabledInBeginnerQuest = new GameObject[0];
            this.mItems = new List<ListItemEvents>();
            this.mCurrentTabPages = new Toggle[0];
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static unsafe int <Refresh>m__301(ListItemEvents p1, ListItemEvents p2)
        {
            ChapterParam param;
            ChapterParam param2;
            int num;
            int num2;
            param = ((p1 != null) == null) ? null : p1.Chapter;
            param2 = ((p2 != null) == null) ? null : p2.Chapter;
            num = (param == null) ? 1 : ((param.IsGpsQuest() == null) ? 1 : 0);
            num2 = (param2 == null) ? 1 : ((param2.IsGpsQuest() == null) ? 1 : 0);
            return &num.CompareTo(num2);
        }

        public void Activated(int pinID)
        {
            EventQuestTypes types;
            GlobalVars.EventQuestListType type;
            GlobalVars.RankingQuestSelected = 0;
            if (pinID != null)
            {
                goto Label_0019;
            }
            this.Refresh(this.mEventType);
            return;
        Label_0019:
            if (pinID != 1)
            {
                goto Label_002D;
            }
            this.Refresh(this.mEventType);
            return;
        Label_002D:
            if (pinID != 2)
            {
                goto Label_0047;
            }
            this.RestoreHierarchy();
            this.Refresh(this.mEventType);
            return;
        Label_0047:
            if (pinID != 3)
            {
                goto Label_006E;
            }
            this.RestoreHierarchyRoot();
            this.mEventType = 0;
            GlobalVars.ReqEventPageListType = 0;
            this.Refresh(this.mEventType);
            return;
        Label_006E:
            if (pinID != 4)
            {
                goto Label_0095;
            }
            this.RestoreHierarchyRoot();
            this.mEventType = 1;
            GlobalVars.ReqEventPageListType = 1;
            this.Refresh(this.mEventType);
            return;
        Label_0095:
            if (pinID != 5)
            {
                goto Label_00B7;
            }
            this.mEventType = 2;
            this.mForceChangeTab = 1;
            this.Refresh(this.mEventType);
            return;
        Label_00B7:
            if (pinID != 6)
            {
                goto Label_00DE;
            }
            this.RestoreHierarchyRoot();
            this.mEventType = 3;
            GlobalVars.ReqEventPageListType = 2;
            this.Refresh(this.mEventType);
            return;
        Label_00DE:
            if (pinID != 7)
            {
                goto Label_0149;
            }
            this.RestoreHierarchyRoot();
            switch (GlobalVars.ReqEventPageListType)
            {
                case 0:
                    goto Label_010C;

                case 1:
                    goto Label_0118;

                case 2:
                    goto Label_0124;

                case 3:
                    goto Label_0130;
            }
            goto Label_013C;
        Label_010C:
            this.mEventType = 0;
            goto Label_013C;
        Label_0118:
            this.mEventType = 1;
            goto Label_013C;
        Label_0124:
            this.mEventType = 3;
            goto Label_013C;
        Label_0130:
            this.mEventType = 4;
        Label_013C:
            this.Refresh(this.mEventType);
            return;
        Label_0149:
            if (pinID != 8)
            {
                goto Label_0172;
            }
            GlobalVars.RankingQuestSelected = 1;
            this.mEventType = 4;
            this.RefreshQuestTypeText(this.mEventType);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        Label_0172:
            if (pinID != 9)
            {
                goto Label_01AD;
            }
            if (string.IsNullOrEmpty(GlobalVars.SelectedChapter) != null)
            {
                goto Label_01AD;
            }
            types = this.GetQuestTypeFromSelectedChapter(GlobalVars.SelectedChapter);
            this.RefreshSwitchButton(types);
            this.RefreshBeginnerObjects(types);
        Label_01AD:
            return;
        }

        private void Awake()
        {
            ChapterParam[] paramArray;
            int num;
            ChapterParam[] paramArray2;
            int num2;
            SubQuestTypes types;
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.ItemTemplate.SetActive(0);
        Label_001D:
            if ((this.Caution != null) == null)
            {
                goto Label_003A;
            }
            this.Caution.SetActive(0);
        Label_003A:
            if ((this.QuestTypeTextFrame != null) == null)
            {
                goto Label_0057;
            }
            this.QuestTypeTextFrame.SetActive(0);
        Label_0057:
            if ((this.TabTriple != null) == null)
            {
                goto Label_0074;
            }
            this.TabTriple.SetActive(0);
        Label_0074:
            if ((this.TabDouble != null) == null)
            {
                goto Label_0091;
            }
            this.TabDouble.SetActive(0);
        Label_0091:
            if (string.IsNullOrEmpty(GlobalVars.SelectedChapter) != null)
            {
                goto Label_00C0;
            }
            this.mEventType = this.GetQuestTypeFromSelectedChapter(GlobalVars.SelectedChapter);
            goto Label_01A8;
        Label_00C0:
            if (GlobalVars.ReqEventPageListType != 1)
            {
                goto Label_0109;
            }
            paramArray = MonoSingleton<GameManager>.Instance.Chapters;
            if (paramArray == null)
            {
                goto Label_0109;
            }
            num = 0;
            goto Label_0100;
        Label_00E3:
            if (paramArray[num].IsKeyQuest() == null)
            {
                goto Label_00FC;
            }
            this.mEventType = 1;
            goto Label_0109;
        Label_00FC:
            num += 1;
        Label_0100:
            if (num < ((int) paramArray.Length))
            {
                goto Label_00E3;
            }
        Label_0109:
            if (GlobalVars.ReqEventPageListType != 4)
            {
                goto Label_0152;
            }
            paramArray2 = MonoSingleton<GameManager>.Instance.Chapters;
            if (paramArray2 == null)
            {
                goto Label_0152;
            }
            num2 = 0;
            goto Label_0149;
        Label_012C:
            if (paramArray2[num2].IsBeginnerQuest() == null)
            {
                goto Label_0145;
            }
            this.mEventType = 5;
            goto Label_0152;
        Label_0145:
            num2 += 1;
        Label_0149:
            if (num2 < ((int) paramArray2.Length))
            {
                goto Label_012C;
            }
        Label_0152:
            if (GlobalVars.ReqEventPageListType != 2)
            {
                goto Label_016F;
            }
            if (this.IsOpendTower() == null)
            {
                goto Label_016F;
            }
            this.mEventType = 3;
        Label_016F:
            if (GlobalVars.ReqEventPageListType != 3)
            {
                goto Label_01A8;
            }
            if (MonoSingleton<GameManager>.Instance.AvailableRankingQuesstParams.Count <= 0)
            {
                goto Label_01A8;
            }
            GlobalVars.RankingQuestSelected = 1;
            this.mEventType = 4;
            this.RefreshQuestTypeText(this.mEventType);
        Label_01A8:
            types = this.GetHighestPrioritySubType();
            this.InitializeTab(types);
            this.mTabIndex = types;
            if (this.RefreshOnStart == null)
            {
                goto Label_01D7;
            }
            this.Refresh(this.mEventType);
        Label_01D7:
            this.RefreshQuestTypeText(this.mEventType);
            return;
        }

        private bool ChapterContainsPlayableQuest(ChapterParam chapter, ChapterParam[] allChapters, QuestParam[] availableQuests, long currentTime)
        {
            bool flag;
            int num;
            int num2;
            flag = 0;
            num = 0;
            goto Label_0031;
        Label_0009:
            if (allChapters[num].parent != chapter)
            {
                goto Label_002D;
            }
            if (this.ChapterContainsPlayableQuest(allChapters[num], allChapters, availableQuests, currentTime) == null)
            {
                goto Label_002B;
            }
            return 1;
        Label_002B:
            flag = 1;
        Label_002D:
            num += 1;
        Label_0031:
            if (num < ((int) allChapters.Length))
            {
                goto Label_0009;
            }
            if (flag != null)
            {
                goto Label_008A;
            }
            num2 = 0;
            goto Label_0081;
        Label_0047:
            if ((availableQuests[num2].ChapterID == chapter.iname) == null)
            {
                goto Label_007D;
            }
            if (availableQuests[num2].IsMulti != null)
            {
                goto Label_007D;
            }
            if (availableQuests[num2].IsDateUnlock(currentTime) == null)
            {
                goto Label_007D;
            }
            return 1;
        Label_007D:
            num2 += 1;
        Label_0081:
            if (num2 < ((int) availableQuests.Length))
            {
                goto Label_0047;
            }
        Label_008A:
            return 0;
        }

        private void DisableTab()
        {
            if ((this.mCurrentTab != null) == null)
            {
                goto Label_001D;
            }
            this.mCurrentTab.SetActive(0);
        Label_001D:
            return;
        }

        private void EnableTab()
        {
            if ((this.mCurrentTab != null) == null)
            {
                goto Label_001D;
            }
            this.mCurrentTab.SetActive(1);
        Label_001D:
            return;
        }

        private List<ChapterParam> GetAvailableChapters(EventQuestTypes type, ChapterParam[] allChapters, QuestParam[] questsAvailable, string selectedSection, string selectedChapter, long currentTime, out ChapterParam currentChapter)
        {
            GameManager manager;
            List<ChapterParam> list;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            ChapterParam param;
            int num9;
            ChapterParam param2;
            EventQuestTypes types;
            manager = MonoSingleton<GameManager>.Instance;
            list = new List<ChapterParam>(allChapters);
            *(currentChapter) = null;
            num = list.Count - 1;
            goto Label_0042;
        Label_001F:
            if ((selectedSection != list[num].section) == null)
            {
                goto Label_003E;
            }
            list.RemoveAt(num);
        Label_003E:
            num -= 1;
        Label_0042:
            if (num >= 0)
            {
                goto Label_001F;
            }
            if (string.IsNullOrEmpty(selectedChapter) != null)
            {
                goto Label_00B3;
            }
            *(currentChapter) = manager.FindArea(selectedChapter);
            num2 = list.Count - 1;
            goto Label_00A7;
        Label_006E:
            if (list[num2].parent == null)
            {
                goto Label_009C;
            }
            if ((list[num2].parent.iname != selectedChapter) == null)
            {
                goto Label_00A3;
            }
        Label_009C:
            list.RemoveAt(num2);
        Label_00A3:
            num2 -= 1;
        Label_00A7:
            if (num2 >= 0)
            {
                goto Label_006E;
            }
            goto Label_00EA;
        Label_00B3:
            num3 = list.Count - 1;
            goto Label_00E2;
        Label_00C2:
            if (list[num3].parent == null)
            {
                goto Label_00DC;
            }
            list.RemoveAt(num3);
        Label_00DC:
            num3 -= 1;
        Label_00E2:
            if (num3 >= 0)
            {
                goto Label_00C2;
            }
        Label_00EA:
            types = type;
            switch (types)
            {
                case 0:
                    goto Label_0115;

                case 1:
                    goto Label_018A;

                case 2:
                    goto Label_01C9;

                case 3:
                    goto Label_0269;

                case 4:
                    goto Label_02B3;

                case 5:
                    goto Label_0274;

                case 6:
                    goto Label_01C9;
            }
            goto Label_02B3;
        Label_0115:
            num4 = 0;
            goto Label_0178;
        Label_011D:
            if (list[num4].IsKeyQuest() != null)
            {
                goto Label_0165;
            }
            if (list[num4].IsTowerQuest() != null)
            {
                goto Label_0165;
            }
            if (list[num4].IsBeginnerQuest() != null)
            {
                goto Label_0165;
            }
            if (list[num4].IsGpsQuest() == null)
            {
                goto Label_0172;
            }
        Label_0165:
            list.RemoveAt(num4--);
        Label_0172:
            num4 += 1;
        Label_0178:
            if (num4 < list.Count)
            {
                goto Label_011D;
            }
            goto Label_02B3;
        Label_018A:
            num5 = 0;
            goto Label_01B7;
        Label_0192:
            if (list[num5].IsKeyQuest() != null)
            {
                goto Label_01B1;
            }
            list.RemoveAt(num5--);
        Label_01B1:
            num5 += 1;
        Label_01B7:
            if (num5 < list.Count)
            {
                goto Label_0192;
            }
            goto Label_02B3;
        Label_01C9:
            num6 = 0;
            goto Label_0257;
        Label_01D1:
            if (list[num6].IsKeyQuest() != null)
            {
                goto Label_0207;
            }
            if (list[num6].IsTowerQuest() != null)
            {
                goto Label_0207;
            }
            if (list[num6].IsBeginnerQuest() == null)
            {
                goto Label_0219;
            }
        Label_0207:
            list.RemoveAt(num6--);
            goto Label_0251;
        Label_0219:
            if (list[num6].IsGpsQuest() == null)
            {
                goto Label_0251;
            }
            if (list[num6].HasGpsQuest() != null)
            {
                goto Label_0251;
            }
            if (type != 2)
            {
                goto Label_0251;
            }
            list.RemoveAt(num6--);
        Label_0251:
            num6 += 1;
        Label_0257:
            if (num6 < list.Count)
            {
                goto Label_01D1;
            }
            goto Label_02B3;
        Label_0269:
            list.Clear();
            goto Label_02B3;
        Label_0274:
            num7 = 0;
            goto Label_02A1;
        Label_027C:
            if (list[num7].IsBeginnerQuest() != null)
            {
                goto Label_029B;
            }
            list.RemoveAt(num7--);
        Label_029B:
            num7 += 1;
        Label_02A1:
            if (num7 < list.Count)
            {
                goto Label_027C;
            }
        Label_02B3:
            num8 = list.Count - 1;
            goto Label_02F0;
        Label_02C2:
            param = list[num8];
            if (this.ChapterContainsPlayableQuest(param, allChapters, questsAvailable, currentTime) == null)
            {
                goto Label_02E2;
            }
            goto Label_02EA;
        Label_02E2:
            list.RemoveAt(num8);
        Label_02EA:
            num8 -= 1;
        Label_02F0:
            if (num8 >= 0)
            {
                goto Label_02C2;
            }
            num9 = 0;
            goto Label_0331;
        Label_0300:
            param2 = list[num9];
            if (param2 == null)
            {
                goto Label_031D;
            }
            if (param2.IsGpsQuest() == null)
            {
                goto Label_032B;
            }
        Label_031D:
            list.RemoveAt(num9);
            num9 -= 1;
        Label_032B:
            num9 += 1;
        Label_0331:
            if (num9 < list.Count)
            {
                goto Label_0300;
            }
            return list;
        }

        private unsafe SubQuestTypes GetHighestPrioritySubType()
        {
            ChapterParam[] paramArray;
            QuestParam[] paramArray2;
            long num;
            ChapterParam param;
            List<ChapterParam> list;
            ChapterParam param2;
            List<ChapterParam>.Enumerator enumerator;
            SubQuestTypes types;
            paramArray = MonoSingleton<GameManager>.Instance.Chapters;
            paramArray2 = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            num = Network.GetServerTime();
            enumerator = this.GetAvailableChapters(6, paramArray, paramArray2, GlobalVars.SelectedSection, null, num, &param).GetEnumerator();
        Label_0043:
            try
            {
                goto Label_0076;
            Label_0048:
                param2 = &enumerator.Current;
                if (param2.GetSubQuestType() != 2)
                {
                    goto Label_0076;
                }
                if (this.ChapterContainsPlayableQuest(param2, paramArray, paramArray2, num) == null)
                {
                    goto Label_0076;
                }
                types = 2;
                goto Label_0096;
            Label_0076:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0048;
                }
                goto Label_0094;
            }
            finally
            {
            Label_0087:
                ((List<ChapterParam>.Enumerator) enumerator).Dispose();
            }
        Label_0094:
            return 1;
        Label_0096:
            return types;
        }

        private EventQuestTypes GetQuestTypeFromSelectedChapter(string chapterName)
        {
            ChapterParam param;
            param = MonoSingleton<GameManager>.Instance.FindArea(chapterName);
            if (param == null)
            {
                goto Label_003B;
            }
            if (param.IsGpsQuest() == null)
            {
                goto Label_001F;
            }
            return 2;
        Label_001F:
            if (param.IsKeyQuest() == null)
            {
                goto Label_002C;
            }
            return 1;
        Label_002C:
            if (param.IsBeginnerQuest() == null)
            {
                goto Label_0039;
            }
            return 5;
        Label_0039:
            return 0;
        Label_003B:
            return 0;
        }

        private void InitializeTab(SubQuestTypes subtype)
        {
            int num;
            int num2;
            <InitializeTab>c__AnonStorey330 storey;
            <InitializeTab>c__AnonStorey331 storey2;
            if (subtype != 2)
            {
                goto Label_00A9;
            }
            if ((this.TabTriple != null) == null)
            {
                goto Label_0024;
            }
            this.TabTriple.SetActive(1);
        Label_0024:
            if ((this.TabDouble != null) == null)
            {
                goto Label_0041;
            }
            this.TabDouble.SetActive(0);
        Label_0041:
            this.mCurrentTab = this.TabTriple;
            this.mCurrentTabPages = this.TripleTabPages;
            num = 0;
            goto Label_0096;
        Label_0060:
            storey = new <InitializeTab>c__AnonStorey330();
            storey.<>f__this = this;
            storey.index = num;
            this.TripleTabPages[num].onValueChanged.AddListener(new UnityAction<bool>(storey, this.<>m__302));
            num += 1;
        Label_0096:
            if (num < ((int) this.TripleTabPages.Length))
            {
                goto Label_0060;
            }
            goto Label_0146;
        Label_00A9:
            if ((this.TabTriple != null) == null)
            {
                goto Label_00C6;
            }
            this.TabTriple.SetActive(0);
        Label_00C6:
            if ((this.TabDouble != null) == null)
            {
                goto Label_00E3;
            }
            this.TabDouble.SetActive(1);
        Label_00E3:
            this.mCurrentTab = this.TabDouble;
            this.mCurrentTabPages = this.DoubleTabPages;
            num2 = 0;
            goto Label_0138;
        Label_0102:
            storey2 = new <InitializeTab>c__AnonStorey331();
            storey2.<>f__this = this;
            storey2.index = num2;
            this.DoubleTabPages[num2].onValueChanged.AddListener(new UnityAction<bool>(storey2, this.<>m__303));
            num2 += 1;
        Label_0138:
            if (num2 < ((int) this.DoubleTabPages.Length))
            {
                goto Label_0102;
            }
        Label_0146:
            return;
        }

        private bool IsChapterChildOf(ChapterParam child, ChapterParam parent)
        {
            goto Label_0016;
        Label_0005:
            if (child != parent)
            {
                goto Label_000E;
            }
            return 1;
        Label_000E:
            child = child.parent;
        Label_0016:
            if (child != null)
            {
                goto Label_0005;
            }
            return 0;
        }

        public bool IsOpendTower()
        {
            GameManager manager;
            QuestParam[] paramArray;
            long num;
            int num2;
            TowerParam param;
            int num3;
            int num4;
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.Player.AvailableQuests;
            num = Network.GetServerTime();
            num2 = 0;
            goto Label_0089;
        Label_001F:
            param = manager.Towers[num2];
            num3 = 0;
            goto Label_007B;
        Label_0031:
            if (paramArray[num3].type == 7)
            {
                goto Label_0045;
            }
            goto Label_0075;
        Label_0045:
            if ((paramArray[num3].iname != param.iname) == null)
            {
                goto Label_0064;
            }
            goto Label_0075;
        Label_0064:
            if (paramArray[num3].IsDateUnlock(num) == null)
            {
                goto Label_0075;
            }
            return 1;
        Label_0075:
            num3 += 1;
        Label_007B:
            if (num3 < ((int) paramArray.Length))
            {
                goto Label_0031;
            }
            num2 += 1;
        Label_0089:
            if (num2 < ((int) manager.Towers.Length))
            {
                goto Label_001F;
            }
            num4 = 0;
            goto Label_00C9;
        Label_009F:
            if (paramArray[num4].IsMultiTower != null)
            {
                goto Label_00B2;
            }
            goto Label_00C3;
        Label_00B2:
            if (paramArray[num4].IsDateUnlock(num) == null)
            {
                goto Label_00C3;
            }
            return 1;
        Label_00C3:
            num4 += 1;
        Label_00C9:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_009F;
            }
            return 0;
        }

        private bool IsOpenRankingQuest()
        {
            return 0;
        }

        private bool IsSectionHidden(string iname)
        {
            SectionParam[] paramArray;
            int num;
            paramArray = MonoSingleton<GameManager>.Instance.Sections;
            num = 0;
            goto Label_003B;
        Label_0012:
            if ((paramArray[num].iname == GlobalVars.SelectedSection) == null)
            {
                goto Label_0037;
            }
            return paramArray[num].hidden;
        Label_0037:
            num += 1;
        Label_003B:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0012;
            }
            return 0;
        }

        private void OnItemSelect(GameObject go)
        {
            ChapterParam param;
            long num;
            GameManager manager;
            QuestParam[] paramArray;
            long num2;
            int num3;
            int num4;
            int num5;
            param = DataSource.FindDataOfClass<ChapterParam>(go, null);
            if (param != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            if (param.IsKeyQuest() == null)
            {
                goto Label_006D;
            }
            num = Network.GetServerTime();
            if (param.IsKeyUnlock(num) != null)
            {
                goto Label_006D;
            }
            if (param.IsDateUnlock(num) != null)
            {
                goto Label_004E;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), null, null, 0, -1);
            return;
        Label_004E:
            GlobalVars.SelectedChapter.Set(param.iname);
            GlobalVars.KeyQuestTimeOver = 0;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        Label_006D:
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            num2 = Network.GetServerTime();
            num3 = 0;
            num4 = 0;
            num5 = 0;
            goto Label_00EB;
        Label_0094:
            if ((paramArray[num5].ChapterID == param.iname) == null)
            {
                goto Label_00E5;
            }
            if (paramArray[num5].IsMulti != null)
            {
                goto Label_00E5;
            }
            num3 += 1;
            if (paramArray[num5].IsJigen == null)
            {
                goto Label_00E5;
            }
            if (paramArray[num5].IsDateUnlock(num2) != null)
            {
                goto Label_00E5;
            }
            num4 += 1;
        Label_00E5:
            num5 += 1;
        Label_00EB:
            if (num5 < ((int) paramArray.Length))
            {
                goto Label_0094;
            }
            if (num3 <= 0)
            {
                goto Label_011C;
            }
            if (num3 != num4)
            {
                goto Label_011C;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), null, null, 0, -1);
            return;
        Label_011C:
            GlobalVars.SelectedChapter.Set(param.iname);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void OnMultiTowerSelect(GameObject go)
        {
            QuestParam param;
            long num;
            param = DataSource.FindDataOfClass<QuestParam>(go, null);
            if (param != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            num = Network.GetServerTime();
            if (param.IsJigen == null)
            {
                goto Label_0042;
            }
            if (param.IsDateUnlock(num) != null)
            {
                goto Label_0042;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), null, null, 0, -1);
            return;
        Label_0042:
            GlobalVars.SelectedMultiTowerID = param.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 0xc9);
            return;
        }

        private void OnNodeSelect(GameObject go)
        {
            ChapterParam param;
            ChapterParam[] paramArray;
            int num;
            param = DataSource.FindDataOfClass<ChapterParam>(go, null);
            paramArray = MonoSingleton<GameManager>.Instance.Chapters;
            num = 0;
            goto Label_0074;
        Label_001A:
            if (paramArray[num].parent != param)
            {
                goto Label_0070;
            }
            if (param.IsGpsQuest() == null)
            {
                goto Label_0053;
            }
            GlobalVars.SelectedChapter.Set(param.iname);
            GlobalEvent.Invoke("GPS_MODE", this);
            goto Label_006F;
        Label_0053:
            GlobalVars.SelectedChapter.Set(param.iname);
            this.Refresh(this.mEventType);
        Label_006F:
            return;
        Label_0070:
            num += 1;
        Label_0074:
            if (num < ((int) paramArray.Length))
            {
                goto Label_001A;
            }
            this.OnItemSelect(go);
            return;
        }

        private void OnToggleValueChanged(int index)
        {
            if (Enum.IsDefined(typeof(SubQuestTypes), (SubQuestTypes) ((byte) index)) != null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            if (index != this.mTabIndex)
            {
                goto Label_0029;
            }
            return;
        Label_0029:
            this.mTabIndex = index;
            this.SetToggleIsOn();
            GlobalVars.SelectedChapter.Set(null);
            this.Refresh(this.mEventType);
            return;
        }

        private void OnTowerSelect(GameObject go)
        {
            TowerParam param;
            GameManager manager;
            QuestParam[] paramArray;
            long num;
            int num2;
            int num3;
            int num4;
            param = DataSource.FindDataOfClass<TowerParam>(go, null);
            if (param != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            num = Network.GetServerTime();
            num2 = 0;
            num3 = 0;
            num4 = 0;
            goto Label_00A9;
        Label_0035:
            if (paramArray[num4].type == 7)
            {
                goto Label_0049;
            }
            goto Label_00A3;
        Label_0049:
            if ((paramArray[num4].ChapterID != param.iname) == null)
            {
                goto Label_0067;
            }
            goto Label_00A3;
        Label_0067:
            if (paramArray[num4].IsMulti == null)
            {
                goto Label_007A;
            }
            goto Label_00A3;
        Label_007A:
            num2 += 1;
            if (paramArray[num4].IsJigen == null)
            {
                goto Label_00A3;
            }
            if (paramArray[num4].IsDateUnlock(num) != null)
            {
                goto Label_00A3;
            }
            num3 += 1;
        Label_00A3:
            num4 += 1;
        Label_00A9:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_0035;
            }
            if (num2 <= 0)
            {
                goto Label_00DA;
            }
            if (num2 != num3)
            {
                goto Label_00DA;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), null, null, 0, -1);
            return;
        Label_00DA:
            GlobalVars.SelectedTowerID = param.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 200);
            return;
        }

        private unsafe void Refresh(EventQuestTypes type)
        {
            GameManager manager;
            ChapterParam[] paramArray;
            QuestParam[] paramArray2;
            long num;
            ChapterParam param;
            List<ChapterParam> list;
            int num2;
            int num3;
            SubQuestTypes types;
            SubQuestTypes types2;
            int num4;
            List<TowerParam> list2;
            int num5;
            TowerParam param2;
            int num6;
            int num7;
            TowerParam param3;
            ListItemEvents events;
            StringBuilder builder;
            QuestParam param4;
            ListItemEvents events2;
            int num8;
            ListItemEvents events3;
            ChapterParam param5;
            StringBuilder builder2;
            ListItemEvents events4;
            int num9;
            ChapterParam param6;
            ListItemEvents events5;
            StringBuilder builder3;
            ListItemEvents events6;
            KeyQuestBanner banner;
            Comparison<ListItemEvents> comparison;
            int num10;
            ListItemEvents events7;
            bool flag;
            GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
            this.mItems.Clear();
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.Chapters;
            paramArray2 = manager.Player.AvailableQuests;
            num = Network.GetServerTime();
            param = null;
            list = this.GetAvailableChapters(type, paramArray, paramArray2, GlobalVars.SelectedSection, GlobalVars.SelectedChapter, num, &param);
            if ((type != 2) || (this.mForceChangeTab == null))
            {
                goto Label_00DE;
            }
            num2 = 0;
            goto Label_00C9;
        Label_0074:
            if (list[num2].IsGpsQuest() == null)
            {
                goto Label_00C3;
            }
            num3 = list[num2].GetSubQuestType();
            if (num3 == this.mTabIndex)
            {
                goto Label_00C3;
            }
            this.RemoveTabs();
            this.OnToggleValueChanged(num3);
            types = this.GetHighestPrioritySubType();
            this.InitializeTab(types);
            return;
        Label_00C3:
            num2 += 1;
        Label_00C9:
            if (num2 < list.Count)
            {
                goto Label_0074;
            }
            this.mForceChangeTab = 0;
        Label_00DE:
            if (type != 4)
            {
                goto Label_00EC;
            }
            list.Clear();
        Label_00EC:
            if ((type != null) && (type != 2))
            {
                goto Label_0141;
            }
            types2 = (byte) this.mTabIndex;
            num4 = 0;
            goto Label_0133;
        Label_010A:
            if (list[num4].GetSubQuestType() == types2)
            {
                goto Label_012D;
            }
            list.RemoveAt(num4--);
        Label_012D:
            num4 += 1;
        Label_0133:
            if (num4 < list.Count)
            {
                goto Label_010A;
            }
        Label_0141:
            if (this.Descending == null)
            {
                goto Label_0153;
            }
            list.Reverse();
        Label_0153:
            if ((type != 3) || (string.IsNullOrEmpty(GlobalVars.SelectedChapter.Get()) == null))
            {
                goto Label_044D;
            }
            list2 = new List<TowerParam>();
            num5 = 0;
            goto Label_01F6;
        Label_017D:
            param2 = manager.Towers[num5];
            num6 = 0;
            goto Label_01E6;
        Label_0190:
            if (paramArray2[num6].type == 7)
            {
                goto Label_01A4;
            }
            goto Label_01E0;
        Label_01A4:
            if ((paramArray2[num6].iname != param2.iname) == null)
            {
                goto Label_01C3;
            }
            goto Label_01E0;
        Label_01C3:
            if (paramArray2[num6].IsDateUnlock(num) == null)
            {
                goto Label_01E0;
            }
            list2.Add(param2);
            goto Label_01F0;
        Label_01E0:
            num6 += 1;
        Label_01E6:
            if (num6 < ((int) paramArray2.Length))
            {
                goto Label_0190;
            }
        Label_01F0:
            num5 += 1;
        Label_01F6:
            if (num5 < ((int) manager.Towers.Length))
            {
                goto Label_017D;
            }
            num7 = 0;
            goto Label_0313;
        Label_020D:
            param3 = list2[num7];
            events = ((this.ItemTemplate != null) == null) ? null : this.ItemTemplate.GetComponent<ListItemEvents>();
            if (string.IsNullOrEmpty(param3.prefabPath) != null)
            {
                goto Label_027E;
            }
            builder = GameUtility.GetStringBuilder();
            builder.Append("QuestChapters/");
            builder.Append(param3.prefabPath);
            events = AssetManager.Load<ListItemEvents>(builder.ToString());
        Label_027E:
            if ((events == null) == null)
            {
                goto Label_0290;
            }
            goto Label_030D;
        Label_0290:
            param4 = MonoSingleton<GameManager>.Instance.FindQuest(param3.iname);
            events2 = Object.Instantiate<ListItemEvents>(events);
            DataSource.Bind<TowerParam>(events2.get_gameObject(), param3);
            DataSource.Bind<QuestParam>(events2.get_gameObject(), param4);
            events2.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
            events2.get_gameObject().SetActive(1);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnTowerSelect);
            this.mItems.Add(events2);
        Label_030D:
            num7 += 1;
        Label_0313:
            if (num7 < list2.Count)
            {
                goto Label_020D;
            }
            num8 = 0;
            goto Label_0443;
        Label_0329:
            if (paramArray2[num8].IsMultiTower != null)
            {
                goto Label_033C;
            }
            goto Label_043D;
        Label_033C:
            if (paramArray2[num8].IsDateUnlock(num) != null)
            {
                goto Label_0350;
            }
            goto Label_043D;
        Label_0350:
            events3 = ((this.ItemTemplate != null) == null) ? null : this.ItemTemplate.GetComponent<ListItemEvents>();
            param5 = paramArray2[num8].Chapter;
            if (param5 != null)
            {
                goto Label_038B;
            }
            goto Label_043D;
        Label_038B:
            if (string.IsNullOrEmpty(param5.prefabPath) != null)
            {
                goto Label_03CD;
            }
            builder2 = GameUtility.GetStringBuilder();
            builder2.Append("QuestChapters/");
            builder2.Append(param5.prefabPath);
            events3 = AssetManager.Load<ListItemEvents>(builder2.ToString());
        Label_03CD:
            if ((events3 == null) == null)
            {
                goto Label_03DF;
            }
            goto Label_043D;
        Label_03DF:
            events4 = Object.Instantiate<ListItemEvents>(events3);
            DataSource.Bind<QuestParam>(events4.get_gameObject(), paramArray2[num8]);
            events4.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
            events4.get_gameObject().SetActive(1);
            events4.OnSelect = new ListItemEvents.ListItemEvent(this.OnMultiTowerSelect);
            this.mItems.Add(events4);
        Label_043D:
            num8 += 1;
        Label_0443:
            if (num8 < ((int) paramArray2.Length))
            {
                goto Label_0329;
            }
        Label_044D:
            num9 = 0;
            goto Label_0594;
        Label_0455:
            param6 = list[num9];
            events5 = ((this.ItemTemplate != null) == null) ? null : this.ItemTemplate.GetComponent<ListItemEvents>();
            if (string.IsNullOrEmpty(param6.prefabPath) != null)
            {
                goto Label_04C6;
            }
            builder3 = GameUtility.GetStringBuilder();
            builder3.Append("QuestChapters/");
            builder3.Append(param6.prefabPath);
            events5 = AssetManager.Load<ListItemEvents>(builder3.ToString());
        Label_04C6:
            if ((events5 == null) == null)
            {
                goto Label_04D8;
            }
            goto Label_058E;
        Label_04D8:
            events6 = Object.Instantiate<ListItemEvents>(events5);
            DataSource.Bind<ChapterParam>(events6.get_gameObject(), param6);
            DataSource.Bind<KeyItem>(events6.get_gameObject(), ((param6 == null) || (param6.keys.Count <= 0)) ? null : param6.keys[0]);
            banner = events6.get_gameObject().GetComponent<KeyQuestBanner>();
            if ((banner != null) == null)
            {
                goto Label_0549;
            }
            banner.UpdateValue();
        Label_0549:
            events6.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
            events6.get_gameObject().SetActive(1);
            events6.OnSelect = new ListItemEvents.ListItemEvent(this.OnNodeSelect);
            this.mItems.Add(events6);
        Label_058E:
            num9 += 1;
        Label_0594:
            if (num9 < list.Count)
            {
                goto Label_0455;
            }
            if (<>f__am$cache1A != null)
            {
                goto Label_05BA;
            }
            <>f__am$cache1A = new Comparison<ListItemEvents>(EventQuestList.<Refresh>m__301);
        Label_05BA:
            comparison = <>f__am$cache1A;
            this.StableSort<ListItemEvents>(this.mItems, comparison);
            num10 = 0;
            goto Label_0617;
        Label_05D7:
            events7 = this.mItems[num10];
            if ((events7 != null) == null)
            {
                goto Label_0611;
            }
            this.mItems[num10].get_gameObject().get_transform().SetSiblingIndex(num10);
        Label_0611:
            num10 += 1;
        Label_0617:
            if (num10 < this.mItems.Count)
            {
                goto Label_05D7;
            }
            if ((this.BackButton != null) == null)
            {
                goto Label_0688;
            }
            if (param == null)
            {
                goto Label_0652;
            }
            this.BackButton.SetActive(1);
            goto Label_0688;
        Label_0652:
            if (string.IsNullOrEmpty(GlobalVars.SelectedSection) != null)
            {
                goto Label_0688;
            }
            flag = this.IsSectionHidden(GlobalVars.SelectedSection);
            this.BackButton.SetActive(flag == 0);
        Label_0688:
            if ((this.Caution != null) == null)
            {
                goto Label_06F0;
            }
            this.Caution.SetActive((list.Count != null) ? 0 : ((type == 3) == 0));
            if (type != 4)
            {
                goto Label_06DB;
            }
            this.CautionText.set_text(LocalizedText.Get("sys.RANKING_QUEST_NO_CHALLENGEABLE"));
            goto Label_06F0;
        Label_06DB:
            this.CautionText.set_text(LocalizedText.Get("sys.KEYQUEST_CAUTION_NOTQUEST"));
        Label_06F0:
            this.RefreshSwitchButton(type);
            this.RefreshBeginnerObjects(type);
            if (type == null)
            {
                goto Label_070B;
            }
            if (type != 2)
            {
                goto Label_0739;
            }
        Label_070B:
            this.EnableTab();
            if ((this.QuestTypeTextFrame != null) == null)
            {
                goto Label_072E;
            }
            this.QuestTypeTextFrame.SetActive(0);
        Label_072E:
            this.SetToggleIsOn();
            goto Label_0763;
        Label_0739:
            this.DisableTab();
            if ((this.QuestTypeTextFrame != null) == null)
            {
                goto Label_075C;
            }
            this.QuestTypeTextFrame.SetActive(1);
        Label_075C:
            this.RefreshQuestTypeText(type);
        Label_0763:
            this.ResetScroll();
            FlowNode_GameObject.ActivateOutputLinks(this, 50);
            return;
        }

        private void RefreshBeginnerObjects(EventQuestTypes type)
        {
            GameObject obj2;
            GameObject[] objArray;
            int num;
            if (type != 5)
            {
                goto Label_0046;
            }
            if (this.DisabledInBeginnerQuest == null)
            {
                goto Label_0046;
            }
            if (((int) this.DisabledInBeginnerQuest.Length) <= 0)
            {
                goto Label_0046;
            }
            objArray = this.DisabledInBeginnerQuest;
            num = 0;
            goto Label_003D;
        Label_002E:
            obj2 = objArray[num];
            obj2.SetActive(0);
            num += 1;
        Label_003D:
            if (num < ((int) objArray.Length))
            {
                goto Label_002E;
            }
        Label_0046:
            return;
        }

        private bool RefreshQuestButtonState(Button _btn, bool _active, bool _interactable)
        {
            LevelLock @lock;
            if ((_btn == null) == null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            _btn.get_gameObject().SetActive(_active);
            @lock = _btn.get_gameObject().GetComponent<LevelLock>();
            if ((@lock != null) == null)
            {
                goto Label_004F;
            }
            @lock.UpdateLockState();
            if (_btn.get_interactable() == null)
            {
                goto Label_0056;
            }
            _btn.set_interactable(_interactable);
            goto Label_0056;
        Label_004F:
            _btn.set_interactable(_interactable);
        Label_0056:
            return _btn.get_interactable();
        }

        private void RefreshQuestTypeText(EventQuestTypes type)
        {
            EventQuestTypes types;
            if ((this.QuestTypeText != null) == null)
            {
                goto Label_00D2;
            }
            types = type;
            switch (types)
            {
                case 0:
                    goto Label_0036;

                case 1:
                    goto Label_0050;

                case 2:
                    goto Label_006A;

                case 3:
                    goto Label_0084;

                case 4:
                    goto Label_009E;

                case 5:
                    goto Label_00B8;
            }
            goto Label_00D2;
        Label_0036:
            this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_EVENT"));
            goto Label_00D2;
        Label_0050:
            this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_KEY"));
            goto Label_00D2;
        Label_006A:
            this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_AREA"));
            goto Label_00D2;
        Label_0084:
            this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_TOWER"));
            goto Label_00D2;
        Label_009E:
            this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_RANKING"));
            goto Label_00D2;
        Label_00B8:
            this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_BEGINNER"));
        Label_00D2:
            return;
        }

        private void RefreshSwitchButton(EventQuestTypes type)
        {
            ChapterParam[] paramArray;
            bool flag;
            bool flag2;
            bool flag3;
            long num;
            int num2;
            EventQuestTypes types;
            paramArray = MonoSingleton<GameManager>.Instance.Chapters;
            flag = 0;
            flag2 = 0;
            flag3 = 0;
            if (paramArray == null)
            {
                goto Label_006A;
            }
            num = Network.GetServerTime();
            num2 = 0;
            goto Label_0060;
        Label_0026:
            if (paramArray[num2].IsKeyQuest() == null)
            {
                goto Label_005A;
            }
            flag = 1;
            if (paramArray[num2].IsDateUnlock(num) == null)
            {
                goto Label_0048;
            }
            flag3 = 1;
        Label_0048:
            if (paramArray[num2].IsKeyUnlock(num) == null)
            {
                goto Label_005A;
            }
            flag2 = 1;
        Label_005A:
            num2 += 1;
        Label_0060:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0026;
            }
        Label_006A:
            types = type;
            switch (types)
            {
                case 0:
                    goto Label_008D;

                case 1:
                    goto Label_0101;

                case 2:
                    goto Label_0267;

                case 3:
                    goto Label_0175;

                case 4:
                    goto Label_01EE;
            }
            goto Label_0267;
        Label_008D:
            if ((this.EventQuestButton != null) == null)
            {
                goto Label_00B7;
            }
            this.RefreshQuestButtonState(this.EventQuestButton, 0, this.EventQuestButton.get_interactable());
        Label_00B7:
            if ((this.KeyQuestButton != null) == null)
            {
                goto Label_00D7;
            }
            this.RefreshQuestButtonState(this.KeyQuestButton, 1, flag3);
        Label_00D7:
            if ((this.TowerQuestButton != null) == null)
            {
                goto Label_0267;
            }
            this.RefreshQuestButtonState(this.TowerQuestButton, 1, this.IsOpendTower());
            goto Label_0267;
        Label_0101:
            if ((this.EventQuestButton != null) == null)
            {
                goto Label_012B;
            }
            this.RefreshQuestButtonState(this.EventQuestButton, 1, this.EventQuestButton.get_interactable());
        Label_012B:
            if ((this.KeyQuestButton != null) == null)
            {
                goto Label_014B;
            }
            this.RefreshQuestButtonState(this.KeyQuestButton, 0, flag);
        Label_014B:
            if ((this.TowerQuestButton != null) == null)
            {
                goto Label_0267;
            }
            this.RefreshQuestButtonState(this.TowerQuestButton, 1, this.IsOpendTower());
            goto Label_0267;
        Label_0175:
            if ((this.EventQuestButton != null) == null)
            {
                goto Label_019F;
            }
            this.RefreshQuestButtonState(this.EventQuestButton, 1, this.EventQuestButton.get_interactable());
        Label_019F:
            if ((this.KeyQuestButton != null) == null)
            {
                goto Label_01BF;
            }
            this.RefreshQuestButtonState(this.KeyQuestButton, 1, flag3);
        Label_01BF:
            if ((this.TowerQuestButton != null) == null)
            {
                goto Label_0267;
            }
            this.RefreshQuestButtonState(this.TowerQuestButton, 0, this.TowerQuestButton.get_interactable());
            goto Label_0267;
        Label_01EE:
            if ((this.EventQuestButton != null) == null)
            {
                goto Label_0218;
            }
            this.RefreshQuestButtonState(this.EventQuestButton, 1, this.EventQuestButton.get_interactable());
        Label_0218:
            if ((this.KeyQuestButton != null) == null)
            {
                goto Label_0238;
            }
            this.RefreshQuestButtonState(this.KeyQuestButton, 1, flag3);
        Label_0238:
            if ((this.TowerQuestButton != null) == null)
            {
                goto Label_0267;
            }
            this.RefreshQuestButtonState(this.TowerQuestButton, 0, this.TowerQuestButton.get_interactable());
        Label_0267:
            if ((this.KeyQuestOpenEffect != null) == null)
            {
                goto Label_0284;
            }
            this.KeyQuestOpenEffect.SetActive(flag2);
        Label_0284:
            return;
        }

        private void RemoveTabs()
        {
            int num;
            int num2;
            if (this.TripleTabPages == null)
            {
                goto Label_0036;
            }
            num = 0;
            goto Label_0028;
        Label_0012:
            this.TripleTabPages[num].onValueChanged.RemoveAllListeners();
            num += 1;
        Label_0028:
            if (num < ((int) this.TripleTabPages.Length))
            {
                goto Label_0012;
            }
        Label_0036:
            if (this.DoubleTabPages == null)
            {
                goto Label_006C;
            }
            num2 = 0;
            goto Label_005E;
        Label_0048:
            this.DoubleTabPages[num2].onValueChanged.RemoveAllListeners();
            num2 += 1;
        Label_005E:
            if (num2 < ((int) this.DoubleTabPages.Length))
            {
                goto Label_0048;
            }
        Label_006C:
            return;
        }

        private void ResetScroll()
        {
            ScrollRect[] rectArray;
            if ((this.ItemContainer != null) == null)
            {
                goto Label_0035;
            }
            rectArray = this.ItemContainer.GetComponentsInParent<ScrollRect>(1);
            if (((int) rectArray.Length) <= 0)
            {
                goto Label_0035;
            }
            rectArray[0].set_normalizedPosition(this.DefaultScrollPosition);
        Label_0035:
            return;
        }

        private void RestoreHierarchy()
        {
            ChapterParam param;
            param = MonoSingleton<GameManager>.Instance.FindArea(GlobalVars.SelectedChapter);
            if (param == null)
            {
                goto Label_00FA;
            }
            this.mTabIndex = param.GetSubQuestType();
            if (param.IsGpsQuest() == null)
            {
                goto Label_0094;
            }
            if (param.parent == null)
            {
                goto Label_0071;
            }
            if (param.parent.HasGpsQuest() == null)
            {
                goto Label_0059;
            }
            this.mEventType = 2;
            goto Label_006C;
        Label_0059:
            this.mEventType = 0;
            GlobalVars.SelectedChapter.Set(null);
            return;
        Label_006C:
            goto Label_008F;
        Label_0071:
            if (param.HasGpsQuest() == null)
            {
                goto Label_0088;
            }
            this.mEventType = 2;
            goto Label_008F;
        Label_0088:
            this.mEventType = 0;
        Label_008F:
            goto Label_00C9;
        Label_0094:
            if (param.IsKeyQuest() == null)
            {
                goto Label_00AB;
            }
            this.mEventType = 1;
            goto Label_00C9;
        Label_00AB:
            if (param.IsBeginnerQuest() == null)
            {
                goto Label_00C2;
            }
            this.mEventType = 5;
            goto Label_00C9;
        Label_00C2:
            this.mEventType = 0;
        Label_00C9:
            if (param.parent == null)
            {
                goto Label_00EE;
            }
            GlobalVars.SelectedChapter.Set(param.parent.iname);
            goto Label_00F9;
        Label_00EE:
            GlobalVars.SelectedChapter.Set(null);
        Label_00F9:
            return;
        Label_00FA:
            if (string.IsNullOrEmpty(GlobalVars.SelectedSection) != null)
            {
                goto Label_012E;
            }
            if (this.IsSectionHidden(GlobalVars.SelectedSection) == null)
            {
                goto Label_012E;
            }
            GlobalVars.SelectedChapter.Set(null);
        Label_012E:
            return;
        }

        private void RestoreHierarchyRoot()
        {
            GlobalVars.SelectedChapter.Set(null);
            return;
        }

        private void SetToggleIsOn()
        {
            int num;
            num = 0;
            goto Label_003E;
        Label_0007:
            if ((this.mCurrentTabPages[num] != null) == null)
            {
                goto Label_003A;
            }
            this.mCurrentTabPages[num].set_isOn((num != this.mTabIndex) ? 0 : 1);
        Label_003A:
            num += 1;
        Label_003E:
            if (num < ((int) this.mCurrentTabPages.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe void StableSort<T>(List<T> list, Comparison<T> comparison)
        {
            List<KeyValuePair<int, T>> list2;
            int num;
            int num2;
            <StableSort>c__AnonStorey332<T> storey;
            KeyValuePair<int, T> pair;
            storey = new <StableSort>c__AnonStorey332<T>();
            storey.comparison = comparison;
            list2 = new List<KeyValuePair<int, T>>(list.Count);
            num = 0;
            goto Label_0037;
        Label_0020:
            list2.Add(new KeyValuePair<int, T>(num, list[num]));
            num += 1;
        Label_0037:
            if (num < list.Count)
            {
                goto Label_0020;
            }
            list2.Sort(new Comparison<KeyValuePair<int, T>>(storey.<>m__304));
            num2 = 0;
            goto Label_0077;
        Label_005C:
            pair = list2[num2];
            list[num2] = &pair.Value;
            num2 += 1;
        Label_0077:
            if (num2 < list.Count)
            {
                goto Label_005C;
            }
            return;
        }

        [CompilerGenerated]
        private sealed class <InitializeTab>c__AnonStorey330
        {
            internal int index;
            internal EventQuestList <>f__this;

            public <InitializeTab>c__AnonStorey330()
            {
                base..ctor();
                return;
            }

            internal void <>m__302(bool isOn)
            {
                this.<>f__this.OnToggleValueChanged(this.index);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <InitializeTab>c__AnonStorey331
        {
            internal int index;
            internal EventQuestList <>f__this;

            public <InitializeTab>c__AnonStorey331()
            {
                base..ctor();
                return;
            }

            internal void <>m__303(bool isOn)
            {
                this.<>f__this.OnToggleValueChanged(this.index);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <StableSort>c__AnonStorey332<T>
        {
            internal Comparison<T> comparison;

            public <StableSort>c__AnonStorey332()
            {
                base..ctor();
                return;
            }

            internal unsafe int <>m__304(KeyValuePair<int, T> x, KeyValuePair<int, T> y)
            {
                int num;
                int num2;
                num = this.comparison(&x.Value, &y.Value);
                if (num != null)
                {
                    goto Label_0037;
                }
                num = &&x.Key.CompareTo(&y.Key);
            Label_0037:
                return num;
            }
        }

        public enum EventQuestTypes
        {
            Normal,
            Key,
            Gps,
            Tower,
            Ranking,
            Beginner,
            NormalAndGps
        }
    }
}

