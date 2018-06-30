namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "ワールドマップへ戻す", 0, 0), Pin(2, "ロケーションのハイライトを戻す", 0, 1), Pin(3, "セクション決定", 0, 3), Pin(100, "アイテムが選択された", 1, 100), Pin(0, "再読み込み", 0, 40), Pin(200, "塔が選択された", 1, 200), Pin(40, "セクション選択に戻す", 1, 40), Pin(50, "再読み込み完了", 1, 50), Pin(4, "階層を上げる", 0, 4)]
    public class QuestChapterList : MonoBehaviour, IFlowInterface
    {
        public ListItemEvents ItemTemplate;
        public GameObject ItemContainer;
        public string WorldMapControllerID;
        public bool Descending;
        public bool RefreshOnStart;
        public GameObject BackButton;
        public Vector2 DefaultScrollPos;
        private List<ListItemEvents> mItems;

        public QuestChapterList()
        {
            this.Descending = 1;
            this.RefreshOnStart = 1;
            this.DefaultScrollPos = new Vector2(0f, 1f);
            this.mItems = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            WorldMapController controller;
            ChapterParam param;
            if (pinID != null)
            {
                goto Label_0011;
            }
            this.Refresh();
            goto Label_010B;
        Label_0011:
            if (pinID != 1)
            {
                goto Label_003C;
            }
            controller = WorldMapController.FindInstance(this.WorldMapControllerID);
            if ((controller != null) == null)
            {
                goto Label_010B;
            }
            controller.GotoArea(null);
            goto Label_010B;
        Label_003C:
            if (pinID != 2)
            {
                goto Label_0048;
            }
            goto Label_010B;
        Label_0048:
            if (pinID != 3)
            {
                goto Label_0065;
            }
            GlobalVars.SelectedChapter.Set(null);
            this.Refresh();
            goto Label_010B;
        Label_0065:
            if (pinID != 4)
            {
                goto Label_010B;
            }
            param = MonoSingleton<GameManager>.Instance.FindArea(GlobalVars.SelectedChapter);
            if (param == null)
            {
                goto Label_00BE;
            }
            if (param.parent == null)
            {
                goto Label_00AC;
            }
            GlobalVars.SelectedChapter.Set(param.parent.iname);
            goto Label_00B7;
        Label_00AC:
            GlobalVars.SelectedChapter.Set(null);
        Label_00B7:
            this.Refresh();
            return;
        Label_00BE:
            this.ResetScroll();
            if (string.IsNullOrEmpty(GlobalVars.SelectedSection) != null)
            {
                goto Label_0103;
            }
            if (this.IsSectionHidden(GlobalVars.SelectedSection) == null)
            {
                goto Label_0103;
            }
            GlobalVars.SelectedChapter.Set(null);
            this.Refresh();
            goto Label_010B;
        Label_0103:
            FlowNode_GameObject.ActivateOutputLinks(this, 40);
        Label_010B:
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

        private void OnBackClick()
        {
        }

        private void OnItemSelect(GameObject go)
        {
            ChapterParam param;
            GameManager manager;
            QuestParam[] paramArray;
            long num;
            int num2;
            int num3;
            int num4;
            param = DataSource.FindDataOfClass<ChapterParam>(go, null);
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
            goto Label_008B;
        Label_0035:
            if ((paramArray[num4].ChapterID == param.iname) == null)
            {
                goto Label_0085;
            }
            if (paramArray[num4].IsMulti != null)
            {
                goto Label_0085;
            }
            num2 += 1;
            if (paramArray[num4].IsJigen == null)
            {
                goto Label_0085;
            }
            if (paramArray[num4].IsDateUnlock(num) != null)
            {
                goto Label_0085;
            }
            num3 += 1;
        Label_0085:
            num4 += 1;
        Label_008B:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_0035;
            }
            if (num2 <= 0)
            {
                goto Label_00BC;
            }
            if (num2 != num3)
            {
                goto Label_00BC;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), null, null, 0, -1);
            return;
        Label_00BC:
            GlobalVars.SelectedChapter.Set(param.iname);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
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
            goto Label_0043;
        Label_001A:
            if (paramArray[num].parent != param)
            {
                goto Label_003F;
            }
            GlobalVars.SelectedChapter.Set(param.iname);
            this.Refresh();
            return;
        Label_003F:
            num += 1;
        Label_0043:
            if (num < ((int) paramArray.Length))
            {
                goto Label_001A;
            }
            this.OnItemSelect(go);
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

        private void Refresh()
        {
            GameManager manager;
            ChapterParam[] paramArray;
            List<ChapterParam> list;
            QuestParam[] paramArray2;
            long num;
            ChapterParam param;
            int num2;
            int num3;
            int num4;
            int num5;
            ChapterParam param2;
            List<TowerParam> list2;
            TowerParam param3;
            TowerParam[] paramArray3;
            int num6;
            bool flag;
            int num7;
            int num8;
            TowerParam param4;
            ListItemEvents events;
            StringBuilder builder;
            QuestParam param5;
            ListItemEvents events2;
            int num9;
            ChapterParam param6;
            ListItemEvents events3;
            StringBuilder builder2;
            ListItemEvents events4;
            bool flag2;
            GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
            if ((this.ItemTemplate == null) != null)
            {
                goto Label_002D;
            }
            if ((this.ItemContainer == null) == null)
            {
                goto Label_002E;
            }
        Label_002D:
            return;
        Label_002E:
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.Chapters;
            list = new List<ChapterParam>(paramArray);
            paramArray2 = manager.Player.AvailableQuests;
            num = Network.GetServerTime();
            param = null;
            num2 = list.Count - 1;
            goto Label_0096;
        Label_0067:
            if ((GlobalVars.SelectedSection != list[num2].section) == null)
            {
                goto Label_0090;
            }
            list.RemoveAt(num2);
        Label_0090:
            num2 -= 1;
        Label_0096:
            if (num2 >= 0)
            {
                goto Label_0067;
            }
            if (string.IsNullOrEmpty(GlobalVars.SelectedChapter) != null)
            {
                goto Label_0126;
            }
            param = manager.FindArea(GlobalVars.SelectedChapter);
            num3 = list.Count - 1;
            goto Label_0119;
        Label_00D3:
            if (list[num3].parent == null)
            {
                goto Label_010B;
            }
            if ((list[num3].parent.iname != GlobalVars.SelectedChapter) == null)
            {
                goto Label_0113;
            }
        Label_010B:
            list.RemoveAt(num3);
        Label_0113:
            num3 -= 1;
        Label_0119:
            if (num3 >= 0)
            {
                goto Label_00D3;
            }
            goto Label_015D;
        Label_0126:
            num4 = list.Count - 1;
            goto Label_0155;
        Label_0135:
            if (list[num4].parent == null)
            {
                goto Label_014F;
            }
            list.RemoveAt(num4);
        Label_014F:
            num4 -= 1;
        Label_0155:
            if (num4 >= 0)
            {
                goto Label_0135;
            }
        Label_015D:
            num5 = list.Count - 1;
            goto Label_019A;
        Label_016C:
            param2 = list[num5];
            if (this.ChapterContainsPlayableQuest(param2, paramArray, paramArray2, num) == null)
            {
                goto Label_018C;
            }
            goto Label_0194;
        Label_018C:
            list.RemoveAt(num5);
        Label_0194:
            num5 -= 1;
        Label_019A:
            if (num5 >= 0)
            {
                goto Label_016C;
            }
            list2 = new List<TowerParam>();
            paramArray3 = manager.Towers;
            num6 = 0;
            goto Label_024A;
        Label_01B9:
            param3 = paramArray3[num6];
            flag = 0;
            num7 = 0;
            goto Label_01FD;
        Label_01CB:
            if (paramArray2[num7].type == 7)
            {
                goto Label_01DF;
            }
            goto Label_01F7;
        Label_01DF:
            if (paramArray2[num7].IsDateUnlock(num) == null)
            {
                goto Label_01F7;
            }
            flag = 1;
            goto Label_0207;
        Label_01F7:
            num7 += 1;
        Label_01FD:
            if (num7 < ((int) paramArray2.Length))
            {
                goto Label_01CB;
            }
        Label_0207:
            if (flag == null)
            {
                goto Label_0244;
            }
            if (string.IsNullOrEmpty(GlobalVars.SelectedSection) != null)
            {
                goto Label_023B;
            }
            if (("WD_DAILY" == GlobalVars.SelectedSection) == null)
            {
                goto Label_0244;
            }
        Label_023B:
            list2.Add(param3);
        Label_0244:
            num6 += 1;
        Label_024A:
            if (num6 < ((int) paramArray3.Length))
            {
                goto Label_01B9;
            }
            if (this.Descending == null)
            {
                goto Label_0266;
            }
            list.Reverse();
        Label_0266:
            num8 = 0;
            goto Label_0356;
        Label_026E:
            param4 = list2[num8];
            events = null;
            if (string.IsNullOrEmpty(param4.prefabPath) != null)
            {
                goto Label_02BE;
            }
            builder = GameUtility.GetStringBuilder();
            builder.Append("QuestChapters/");
            builder.Append(param4.prefabPath);
            events = AssetManager.Load<ListItemEvents>(builder.ToString());
        Label_02BE:
            if ((events == null) == null)
            {
                goto Label_02D3;
            }
            events = this.ItemTemplate;
        Label_02D3:
            param5 = MonoSingleton<GameManager>.Instance.FindQuest(param4.iname);
            events2 = Object.Instantiate<ListItemEvents>(events);
            DataSource.Bind<TowerParam>(events2.get_gameObject(), param4);
            DataSource.Bind<QuestParam>(events2.get_gameObject(), param5);
            events2.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
            events2.get_gameObject().SetActive(1);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnTowerSelect);
            this.mItems.Add(events2);
            num8 += 1;
        Label_0356:
            if (num8 < list2.Count)
            {
                goto Label_026E;
            }
            num9 = 0;
            goto Label_0432;
        Label_036C:
            param6 = list[num9];
            events3 = null;
            if (string.IsNullOrEmpty(param6.prefabPath) != null)
            {
                goto Label_03BB;
            }
            builder2 = GameUtility.GetStringBuilder();
            builder2.Append("QuestChapters/");
            builder2.Append(param6.prefabPath);
            events3 = AssetManager.Load<ListItemEvents>(builder2.ToString());
        Label_03BB:
            if ((events3 == null) == null)
            {
                goto Label_03D0;
            }
            events3 = this.ItemTemplate;
        Label_03D0:
            events4 = Object.Instantiate<ListItemEvents>(events3);
            DataSource.Bind<ChapterParam>(events4.get_gameObject(), param6);
            events4.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
            events4.get_gameObject().SetActive(1);
            events4.OnSelect = new ListItemEvents.ListItemEvent(this.OnNodeSelect);
            this.mItems.Add(events4);
            num9 += 1;
        Label_0432:
            if (num9 < list.Count)
            {
                goto Label_036C;
            }
            if ((this.BackButton != null) == null)
            {
                goto Label_049E;
            }
            if (param == null)
            {
                goto Label_0468;
            }
            this.BackButton.SetActive(1);
            goto Label_049E;
        Label_0468:
            if (string.IsNullOrEmpty(GlobalVars.SelectedSection) != null)
            {
                goto Label_049E;
            }
            flag2 = this.IsSectionHidden(GlobalVars.SelectedSection);
            this.BackButton.SetActive(flag2 == 0);
        Label_049E:
            FlowNode_GameObject.ActivateOutputLinks(this, 50);
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
            rectArray[0].set_normalizedPosition(this.DefaultScrollPos);
        Label_0035:
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
            if (this.RefreshOnStart == null)
            {
                goto Label_0033;
            }
            this.Refresh();
        Label_0033:
            return;
        }

        private WorldMapController mWorldMap
        {
            get
            {
                GameObject obj2;
                obj2 = GameObjectID.FindGameObject(this.WorldMapControllerID);
                if ((obj2 != null) == null)
                {
                    goto Label_001F;
                }
                return obj2.GetComponent<WorldMapController>();
            Label_001F:
                return null;
            }
        }
    }
}

