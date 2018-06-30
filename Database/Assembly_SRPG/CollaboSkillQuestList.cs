namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(10, "リスト切り替え", 0, 10), Pin(100, "クエストが選択された", 1, 100)]
    public class CollaboSkillQuestList : MonoBehaviour, IFlowInterface
    {
        public UnitData CurrentUnit1;
        public UnitData CurrentUnit2;
        public Transform QuestList;
        public GameObject StoryQuestItemTemplate;
        public GameObject StoryQuestDisableItemTemplate;
        public GameObject QuestDetailTemplate;
        public string DisableFlagName;
        public GameObject CharacterImage1;
        public GameObject CharacterImage2;
        private List<GameObject> mStoryQuestListItems;
        private GameObject mQuestDetail;
        public Image ListToggleButton;
        public Sprite StoryListSprite;
        private bool mIsStoryList;
        private bool mListRefreshing;
        private bool mIsRestore;

        public CollaboSkillQuestList()
        {
            this.DisableFlagName = "is_disable";
            this.mStoryQuestListItems = new List<GameObject>();
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

        private void CreateStoryList()
        {
            GameManager manager;
            List<QuestParam> list;
            QuestParam[] paramArray;
            int num;
            bool flag;
            bool flag2;
            bool flag3;
            bool flag4;
            GameObject obj2;
            Button button;
            CharacterQuestListItem item;
            ListItemEvents events;
            <CreateStoryList>c__AnonStorey31F storeyf;
            if ((MonoSingleton<GameManager>.GetInstanceDirect() == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            list = GetCollaboSkillQuests(this.CurrentUnit1, this.CurrentUnit2);
            if (list != null)
            {
                goto Label_005B;
            }
            DebugUtility.LogError(string.Format("連携スキルクエストが見つかりません:{0} \x00d7 {1}", this.CurrentUnit1.UnitParam.iname, this.CurrentUnit2.UnitParam.iname));
            return;
        Label_005B:
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            num = 0;
            goto Label_01C9;
        Label_0072:
            storeyf = new <CreateStoryList>c__AnonStorey31F();
            storeyf.questParam = list[num];
            flag = storeyf.questParam.IsDateUnlock(-1L);
            flag2 = (Array.Find<QuestParam>(paramArray, new Predicate<QuestParam>(storeyf.<>m__2C5)) == null) == 0;
            flag3 = storeyf.questParam.state == 2;
            flag4 = ((flag == null) || (flag2 == null)) ? 0 : (flag3 == 0);
            obj2 = null;
            if (flag2 != null)
            {
                goto Label_00EC;
            }
            if (flag3 == null)
            {
                goto Label_0110;
            }
        Label_00EC:
            obj2 = Object.Instantiate<GameObject>(this.StoryQuestItemTemplate);
            obj2.GetComponent<Button>().set_interactable(flag4);
            goto Label_011D;
        Label_0110:
            obj2 = Object.Instantiate<GameObject>(this.StoryQuestDisableItemTemplate);
        Label_011D:
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.QuestList, 0);
            DataSource.Bind<QuestParam>(obj2, storeyf.questParam);
            item = obj2.GetComponent<CharacterQuestListItem>();
            if ((item != null) == null)
            {
                goto Label_0176;
            }
            item.SetUp(this.CurrentUnit1, this.CurrentUnit2, storeyf.questParam);
        Label_0176:
            events = obj2.GetComponent<ListItemEvents>();
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            events.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
            this.mStoryQuestListItems.Add(obj2);
            num += 1;
        Label_01C9:
            if (num < list.Count)
            {
                goto Label_0072;
            }
            return;
        }

        public static QuestParam GetCollaboSkillQuest(UnitData unitData1, UnitData unitData2)
        {
            GameManager manager;
            CollaboSkillParam param;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0014;
            }
            return null;
        Label_0014:
            return GetLearnSkillQuest(manager.MasterParam.GetCollaboSkillData(unitData1.UnitParam.iname), unitData2);
        }

        public static List<QuestParam> GetCollaboSkillQuests(UnitData unitData1, UnitData unitData2)
        {
            List<QuestParam> list;
            GameManager manager;
            QuestParam param;
            QuestParam[] paramArray;
            int num;
            list = new List<QuestParam>();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_001A;
            }
            return list;
        Label_001A:
            param = GetCollaboSkillQuest(unitData1, unitData2);
            if (param == null)
            {
                goto Label_006A;
            }
            paramArray = manager.Quests;
            num = 0;
            goto Label_0060;
        Label_0037:
            if ((paramArray[num].ChapterID == param.ChapterID) == null)
            {
                goto Label_005A;
            }
            list.Add(paramArray[num]);
        Label_005A:
            num += 1;
        Label_0060:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0037;
            }
        Label_006A:
            return list;
        }

        private static QuestParam GetLearnSkillQuest(CollaboSkillParam csp, UnitData partner)
        {
            GameManager manager;
            CollaboSkillParam.LearnSkill skill;
            <GetLearnSkillQuest>c__AnonStorey31E storeye;
            storeye = new <GetLearnSkillQuest>c__AnonStorey31E();
            storeye.partner = partner;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0021;
            }
            return null;
        Label_0021:
            if (csp == null)
            {
                goto Label_0032;
            }
            if (storeye.partner != null)
            {
                goto Label_0034;
            }
        Label_0032:
            return null;
        Label_0034:
            skill = csp.LearnSkillLists.Find(new Predicate<CollaboSkillParam.LearnSkill>(storeye.<>m__2C4));
            if (skill != null)
            {
                goto Label_005E;
            }
            DebugUtility.LogError("learnSkill がnull");
            return null;
        Label_005E:
            return manager.FindQuest(skill.QuestIname);
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
            DataSource.Bind<UnitData>(this.mQuestDetail, this.CurrentUnit1);
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
            <OnQuestSelect>c__AnonStorey320 storey;
            storey = new <OnQuestSelect>c__AnonStorey320();
            list = this.mStoryQuestListItems;
            num = list.IndexOf(button.get_gameObject());
            storey.quest = DataSource.FindDataOfClass<QuestParam>(list[num], null);
            if (storey.quest == null)
            {
                goto Label_00C7;
            }
            if (storey.quest.IsDateUnlock(-1L) != null)
            {
                goto Label_0066;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), null, null, 0, -1);
            return;
        Label_0066:
            if (((Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(storey.<>m__2C6)) == null) == 0) != null)
            {
                goto Label_00AE;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), null, null, 0, -1);
            return;
        Label_00AE:
            GlobalVars.SelectedQuestID = storey.quest.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_00C7:
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
            num = 0;
            goto Label_0080;
        Label_0065:
            this.mStoryQuestListItems[num].SetActive(this.mIsStoryList);
            num += 1;
        Label_0080:
            if (num < this.mStoryQuestListItems.Count)
            {
                goto Label_0065;
            }
            DataSource.Bind<UnitData>(this.CharacterImage1, this.CurrentUnit1);
            DataSource.Bind<UnitData>(this.CharacterImage2, this.CurrentUnit2);
            this.mListRefreshing = 0;
            return;
        }

        protected virtual void Start()
        {
            if ((this.StoryQuestItemTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.StoryQuestItemTemplate.get_gameObject().SetActive(0);
        Label_0022:
            this.UpdateToggleButton();
            this.RefreshQuestList();
            return;
        }

        private void UpdateToggleButton()
        {
            if ((this.ListToggleButton != null) == null)
            {
                goto Label_0022;
            }
            this.ListToggleButton.set_sprite(this.StoryListSprite);
        Label_0022:
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
        private sealed class <CreateStoryList>c__AnonStorey31F
        {
            internal QuestParam questParam;

            public <CreateStoryList>c__AnonStorey31F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2C5(QuestParam p)
            {
                return (p == this.questParam);
            }
        }

        [CompilerGenerated]
        private sealed class <GetLearnSkillQuest>c__AnonStorey31E
        {
            internal UnitData partner;

            public <GetLearnSkillQuest>c__AnonStorey31E()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2C4(CollaboSkillParam.LearnSkill ls)
            {
                return (ls.PartnerUnitIname == this.partner.UnitParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <OnQuestSelect>c__AnonStorey320
        {
            internal QuestParam quest;

            public <OnQuestSelect>c__AnonStorey320()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2C6(QuestParam p)
            {
                return (p == this.quest);
            }
        }
    }
}

