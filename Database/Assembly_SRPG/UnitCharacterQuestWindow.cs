namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(10, "リスト切り替え", 0, 10), Pin(100, "クエストが選択された", 1, 100)]
    public class UnitCharacterQuestWindow : MonoBehaviour, IFlowInterface
    {
        public UnitData CurrentUnit;
        public Transform QuestList;
        public GameObject StoryQuestItemTemplate;
        public GameObject StoryQuestDisableItemTemplate;
        public GameObject PieceQuestItemTemplate;
        public GameObject PieceQuestDisableItemTemplate;
        public GameObject QuestDetailTemplate;
        public string DisableFlagName;
        public GameObject CharacterImage;
        private List<QuestParam> mQuestList;
        private List<GameObject> mStoryQuestListItems;
        private List<GameObject> mPieceQuestListItems;
        private GameObject mQuestDetail;
        public string PieceQuestWorldId;
        public Image ListToggleButton;
        public Sprite StoryListSprite;
        public Sprite PieceListSprite;
        private bool mIsStoryList;
        private bool mListRefreshing;
        private bool mIsRestore;

        public UnitCharacterQuestWindow()
        {
            this.DisableFlagName = "is_disable";
            this.mQuestList = new List<QuestParam>();
            this.mStoryQuestListItems = new List<GameObject>();
            this.mPieceQuestListItems = new List<GameObject>();
            this.mIsStoryList = 1;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_000E;
            }
            this.OnToggleButton();
        Label_000E:
            return;
        }

        private void CreatePieceQuest()
        {
            GameManager manager;
            List<QuestParam> list;
            QuestParam[] paramArray;
            int num;
            int num2;
            GameObject obj2;
            ListItemEvents events;
            if ((this.PieceQuestItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            manager = MonoSingleton<GameManager>.Instance;
            list = new List<QuestParam>();
            paramArray = manager.Quests;
            num = 0;
            goto Label_0092;
        Label_002C:
            if (string.IsNullOrEmpty(paramArray[num].world) != null)
            {
                goto Label_008E;
            }
            if ((paramArray[num].world == this.PieceQuestWorldId) == null)
            {
                goto Label_008E;
            }
            if (string.IsNullOrEmpty(paramArray[num].ChapterID) != null)
            {
                goto Label_008E;
            }
            if ((paramArray[num].ChapterID == this.CurrentUnit.UnitID) == null)
            {
                goto Label_008E;
            }
            list.Add(paramArray[num]);
        Label_008E:
            num += 1;
        Label_0092:
            if (num < ((int) paramArray.Length))
            {
                goto Label_002C;
            }
            if (list.Count <= 1)
            {
                goto Label_0155;
            }
            num2 = 0;
            goto Label_0148;
        Label_00AF:
            obj2 = Object.Instantiate<GameObject>(this.PieceQuestItemTemplate);
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.QuestList, 0);
            DataSource.Bind<QuestParam>(obj2, list[num2]);
            DataSource.Bind<UnitData>(obj2, this.CurrentUnit);
            events = obj2.GetComponent<ListItemEvents>();
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            events.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
            this.mPieceQuestListItems.Add(obj2);
            num2 += 1;
        Label_0148:
            if (num2 < list.Count)
            {
                goto Label_00AF;
            }
        Label_0155:
            return;
        }

        private void CreateStoryList()
        {
            UnitData.CharacterQuestParam[] paramArray;
            QuestParam[] paramArray2;
            bool flag;
            bool flag2;
            bool flag3;
            bool flag4;
            bool flag5;
            GameObject obj2;
            Button button;
            ListItemEvents events;
            <CreateStoryList>c__AnonStorey3B9 storeyb;
            this.mQuestList.Clear();
            this.mQuestList = this.CurrentUnit.FindCondQuests();
            paramArray = this.CurrentUnit.GetCharaEpisodeList();
            paramArray2 = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            storeyb = new <CreateStoryList>c__AnonStorey3B9();
            storeyb.<>f__this = this;
            storeyb.i = 0;
            goto Label_0215;
        Label_0054:
            flag = this.mQuestList[storeyb.i].IsDateUnlock(-1L);
            flag2 = (Array.Find<QuestParam>(paramArray2, new Predicate<QuestParam>(storeyb.<>m__43B)) == null) == 0;
            flag3 = this.mQuestList[storeyb.i].state == 2;
            flag4 = ((paramArray[storeyb.i] == null) || (paramArray[storeyb.i].IsAvailable == null)) ? 0 : this.CurrentUnit.IsChQuestParentUnlocked(this.mQuestList[storeyb.i]);
            flag5 = ((flag == null) || (flag2 == null)) ? 0 : (flag3 == 0);
            obj2 = null;
            if (flag4 != null)
            {
                goto Label_010E;
            }
            if (flag3 == null)
            {
                goto Label_0144;
            }
        Label_010E:
            obj2 = Object.Instantiate<GameObject>(this.StoryQuestItemTemplate);
            button = obj2.GetComponent<Button>();
            if ((button == null) == null)
            {
                goto Label_0136;
            }
            goto Label_0205;
        Label_0136:
            button.set_interactable(flag5);
            goto Label_0151;
        Label_0144:
            obj2 = Object.Instantiate<GameObject>(this.StoryQuestDisableItemTemplate);
        Label_0151:
            if ((obj2 == null) == null)
            {
                goto Label_0163;
            }
            goto Label_0205;
        Label_0163:
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.QuestList, 0);
            DataSource.Bind<QuestParam>(obj2, this.mQuestList[storeyb.i]);
            DataSource.Bind<UnitData>(obj2, this.CurrentUnit);
            DataSource.Bind<UnitParam>(obj2, this.CurrentUnit.UnitParam);
            events = obj2.GetComponent<ListItemEvents>();
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            events.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
            this.mStoryQuestListItems.Add(obj2);
        Label_0205:
            storeyb.i += 1;
        Label_0215:
            if (storeyb.i < this.mQuestList.Count)
            {
                goto Label_0054;
            }
            return;
        }

        private void OnCloseItemDetail(GameObject go)
        {
            if ((this.mQuestDetail != null) == null)
            {
                goto Label_0028;
            }
            Object.DestroyImmediate(this.mQuestDetail.get_gameObject());
            this.mQuestDetail = null;
        Label_0028:
            return;
        }

        private void OnOpenItemDetail(GameObject go)
        {
            QuestParam param;
            param = DataSource.FindDataOfClass<QuestParam>(go, null);
            if ((this.mQuestDetail == null) == null)
            {
                goto Label_0059;
            }
            if (param == null)
            {
                goto Label_0059;
            }
            this.mQuestDetail = Object.Instantiate<GameObject>(this.QuestDetailTemplate);
            DataSource.Bind<QuestParam>(this.mQuestDetail, param);
            DataSource.Bind<UnitData>(this.mQuestDetail, this.CurrentUnit);
            this.mQuestDetail.SetActive(1);
        Label_0059:
            return;
        }

        private void OnQuestSelect(GameObject button)
        {
            List<GameObject> list;
            int num;
            bool flag;
            QuestParam[] paramArray;
            bool flag2;
            <OnQuestSelect>c__AnonStorey3BA storeyba;
            storeyba = new <OnQuestSelect>c__AnonStorey3BA();
            list = (this.mIsStoryList == null) ? this.mPieceQuestListItems : this.mStoryQuestListItems;
            num = list.IndexOf(button.get_gameObject());
            storeyba.quest = DataSource.FindDataOfClass<QuestParam>(list[num], null);
            if (storeyba.quest == null)
            {
                goto Label_00DD;
            }
            if (storeyba.quest.IsDateUnlock(-1L) != null)
            {
                goto Label_007C;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), null, null, 0, -1);
            return;
        Label_007C:
            if (((Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(storeyba.<>m__43C)) == null) == 0) != null)
            {
                goto Label_00C4;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), null, null, 0, -1);
            return;
        Label_00C4:
            GlobalVars.SelectedQuestID = storeyba.quest.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_00DD:
            return;
        }

        private void OnToggleButton()
        {
            if (this.mListRefreshing == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mIsStoryList = this.mIsStoryList == 0;
            this.UpdateToggleButton();
            this.RefreshQuestList();
            return;
        }

        private void RefreshQuestList()
        {
            int num;
            int num2;
            UnitData data;
            if (this.mListRefreshing == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.StoryQuestItemTemplate == null) != null)
            {
                goto Label_003F;
            }
            if ((this.StoryQuestDisableItemTemplate == null) != null)
            {
                goto Label_003F;
            }
            if ((this.QuestList == null) == null)
            {
                goto Label_0040;
            }
        Label_003F:
            return;
        Label_0040:
            this.mListRefreshing = 1;
            if (this.mStoryQuestListItems.Count > 0)
            {
                goto Label_005E;
            }
            this.CreateStoryList();
        Label_005E:
            if (this.mPieceQuestListItems.Count > 0)
            {
                goto Label_0075;
            }
            this.CreatePieceQuest();
        Label_0075:
            num = 0;
            goto Label_0097;
        Label_007C:
            this.mStoryQuestListItems[num].SetActive(this.mIsStoryList);
            num += 1;
        Label_0097:
            if (num < this.mStoryQuestListItems.Count)
            {
                goto Label_007C;
            }
            num2 = 0;
            goto Label_00CD;
        Label_00AF:
            this.mPieceQuestListItems[num2].SetActive(this.mIsStoryList == 0);
            num2 += 1;
        Label_00CD:
            if (num2 < this.mPieceQuestListItems.Count)
            {
                goto Label_00AF;
            }
            data = new UnitData();
            data.Setup(this.CurrentUnit);
            data.SetJobSkinAll(null);
            DataSource.Bind<UnitData>(this.CharacterImage, data);
            this.mListRefreshing = 0;
            return;
        }

        private void Start()
        {
            QuestParam param;
            <Start>c__AnonStorey3B8 storeyb;
            if ((this.StoryQuestItemTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.StoryQuestItemTemplate.get_gameObject().SetActive(0);
        Label_0022:
            if (this.IsRestore == null)
            {
                goto Label_00AA;
            }
            storeyb = new <Start>c__AnonStorey3B8();
            storeyb.lastQuestName = GlobalVars.LastPlayedQuest.Get();
            if (storeyb.lastQuestName == null)
            {
                goto Label_00AA;
            }
            if (string.IsNullOrEmpty(storeyb.lastQuestName) != null)
            {
                goto Label_00AA;
            }
            param = Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Quests, new Predicate<QuestParam>(storeyb.<>m__43A));
            if (param == null)
            {
                goto Label_00AA;
            }
            if (string.IsNullOrEmpty(param.ChapterID) != null)
            {
                goto Label_00AA;
            }
            this.mIsStoryList = (param.world == this.PieceQuestWorldId) == 0;
        Label_00AA:
            this.UpdateToggleButton();
            this.RefreshQuestList();
            return;
        }

        private void UpdateToggleButton()
        {
            if ((this.ListToggleButton != null) == null)
            {
                goto Label_0038;
            }
            this.ListToggleButton.set_sprite((this.mIsStoryList == null) ? this.PieceListSprite : this.StoryListSprite);
        Label_0038:
            return;
        }

        public bool IsRestore
        {
            get
            {
                return this.mIsRestore;
            }
            set
            {
                this.mIsRestore = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateStoryList>c__AnonStorey3B9
        {
            internal int i;
            internal UnitCharacterQuestWindow <>f__this;

            public <CreateStoryList>c__AnonStorey3B9()
            {
                base..ctor();
                return;
            }

            internal bool <>m__43B(QuestParam p)
            {
                return (p == this.<>f__this.mQuestList[this.i]);
            }
        }

        [CompilerGenerated]
        private sealed class <OnQuestSelect>c__AnonStorey3BA
        {
            internal QuestParam quest;

            public <OnQuestSelect>c__AnonStorey3BA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__43C(QuestParam p)
            {
                return (p == this.quest);
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey3B8
        {
            internal string lastQuestName;

            public <Start>c__AnonStorey3B8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__43A(QuestParam p)
            {
                return (p.iname == this.lastQuestName);
            }
        }
    }
}

