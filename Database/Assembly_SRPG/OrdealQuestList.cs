namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "チーム情報更新", 0, 0), Pin(0x44c, "チーム編成開始", 1, 0x44c), Pin(1, "クエスト開始要求", 0, 1), Pin(0x3e8, "クエスト開始", 1, 0x3e8)]
    public class OrdealQuestList : MonoBehaviour, IFlowInterface, IWebHelp
    {
        [SerializeField]
        private GameObject ItemContainer;
        [SerializeField]
        private Text QuestTypeText;
        [SerializeField]
        private GameObject ChapterScrollRect;
        [SerializeField]
        private GameObject DetailTemplate;
        [Space(10f), SerializeField]
        private GameObject TeamPanelContainer;
        [SerializeField]
        private OrdealTeamPanel TeamPanelTemplate;
        [SerializeField]
        private Button StartButton;
        [SerializeField]
        private ListItemEvents MissionButton;
        [SerializeField]
        private Image BossImage;
        [SerializeField]
        private Text BossText;
        private List<ListItemEvents> mItems;
        private GameObject mDetailInfo;
        private ChapterParam mCurrentChapter;
        private QuestParam mCurrentQuest;
        private List<GameObject> mTeamPanels;

        public OrdealQuestList()
        {
            this.mItems = new List<ListItemEvents>();
            this.mTeamPanels = new List<GameObject>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <StartQuest>m__379()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e8);
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == null)
            {
                goto Label_0014;
            }
            if (num == 1)
            {
                goto Label_001B;
            }
            goto Label_0022;
        Label_0014:
            this.LoadTeam();
            return;
        Label_001B:
            this.StartQuest();
            return;
        Label_0022:
            return;
        }

        private void Awake()
        {
            if ((this.TeamPanelTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.TeamPanelTemplate.get_gameObject().SetActive(0);
        Label_0022:
            GlobalVars.OrdealParties = new List<PartyEditData>();
            GlobalVars.OrdealSupports = new List<SupportData>();
            this.Refresh();
            this.RefreshQuestTypeText();
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

        private void CheckPlayableTeams(QuestParam quest, List<PartyEditData> teams, List<SupportData> supports)
        {
            if ((this.StartButton != null) == null)
            {
                goto Label_0025;
            }
            this.StartButton.set_interactable(PartyUtility.ValidateOrdealTeams(quest, teams, supports, 1));
        Label_0025:
            return;
        }

        private List<ChapterParam> GetAvailableChapters(ChapterParam[] allChapters, QuestParam[] questsAvailable, long currentTime, out ChapterParam currentChapter)
        {
            List<ChapterParam> list;
            ChapterParam param;
            ChapterParam[] paramArray;
            int num;
            int num2;
            ChapterParam param2;
            list = new List<ChapterParam>();
            *(currentChapter) = null;
            paramArray = allChapters;
            num = 0;
            goto Label_0048;
        Label_0013:
            param = paramArray[num];
            if (param.IsOrdealQuest() == null)
            {
                goto Label_0044;
            }
            list.Add(param);
            if (param.quests[0].state == 2)
            {
                goto Label_0044;
            }
            *(currentChapter) = param;
        Label_0044:
            num += 1;
        Label_0048:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0013;
            }
            if (*(currentChapter) != null)
            {
                goto Label_006F;
            }
            if (list.Count <= 0)
            {
                goto Label_006F;
            }
            *(currentChapter) = list[0];
        Label_006F:
            num2 = list.Count - 1;
            goto Label_00AB;
        Label_007E:
            param2 = list[num2];
            if (this.ChapterContainsPlayableQuest(param2, allChapters, questsAvailable, currentTime) == null)
            {
                goto Label_009D;
            }
            goto Label_00A5;
        Label_009D:
            list.RemoveAt(num2);
        Label_00A5:
            num2 -= 1;
        Label_00AB:
            if (num2 >= 0)
            {
                goto Label_007E;
            }
            return list;
        }

        public bool GetHelpURL(out string url, out string title)
        {
            if (this.mCurrentChapter == null)
            {
                goto Label_003C;
            }
            if (string.IsNullOrEmpty(this.mCurrentChapter.helpURL) != null)
            {
                goto Label_003C;
            }
            *(title) = this.mCurrentChapter.name;
            *(url) = this.mCurrentChapter.helpURL;
            return 1;
        Label_003C:
            *(title) = null;
            *(url) = null;
            return 0;
        }

        private void LoadBossData(QuestParam quest)
        {
            SpriteSheet sheet;
            sheet = AssetManager.Load<SpriteSheet>("OrdealQuestList/OrdealQuestList_Images");
            if ((sheet != null) == null)
            {
                goto Label_002E;
            }
            this.BossImage.set_sprite(sheet.GetSprite(quest.iname));
        Label_002E:
            this.BossText.set_text(LocalizedText.Get("sys.ORDEAL_QUEST_BOSS_MESSAGE_" + quest.iname));
            return;
        }

        private unsafe void LoadTeam()
        {
            List<PartyEditData> list;
            List<SupportData> list2;
            int num;
            OrdealTeamPanel panel;
            UnitData data;
            UnitData[] dataArray;
            int num2;
            SupportData data2;
            int num3;
            <LoadTeam>c__AnonStorey368 storey;
            GameUtility.DestroyGameObjects(this.mTeamPanels);
            this.mTeamPanels.Clear();
            GlobalVars.OrdealParties = this.LoadTeamFromPlayerPrefs();
            list = GlobalVars.OrdealParties;
            list2 = GlobalVars.OrdealSupports;
            num = 0;
            goto Label_0162;
        Label_0034:
            storey = new <LoadTeam>c__AnonStorey368();
            storey.<>f__this = this;
            panel = Object.Instantiate<GameObject>(this.TeamPanelTemplate.get_gameObject()).GetComponent<OrdealTeamPanel>();
            panel.get_gameObject().SetActive(1);
            dataArray = list[num].Units;
            num2 = 0;
            goto Label_0097;
        Label_007B:
            data = dataArray[num2];
            if (data == null)
            {
                goto Label_0091;
            }
            panel.Add(data);
        Label_0091:
            num2 += 1;
        Label_0097:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_007B;
            }
            storey.index = num;
            panel.Button.get_onClick().AddListener(new UnityAction(storey, this.<>m__37A));
            panel.TeamName.set_text(list[num].Name);
            data2 = null;
            if (list2 == null)
            {
                goto Label_0104;
            }
            if (num >= list2.Count)
            {
                goto Label_0104;
            }
            data2 = list2[num];
            panel.SetSupport(data2);
        Label_0104:
            num3 = PartyUtility.CalcTotalAttack(list[num], MonoSingleton<GameManager>.Instance.Player.Units, data2, null);
            panel.TotalAtack.set_text(&num3.ToString());
            this.mTeamPanels.Add(panel.get_gameObject());
            panel.get_transform().SetParent(this.TeamPanelContainer.get_transform(), 0);
            num += 1;
        Label_0162:
            if (num < list.Count)
            {
                goto Label_0034;
            }
            this.CheckPlayableTeams(this.mCurrentQuest, list, list2);
            return;
        }

        private unsafe List<PartyEditData> LoadTeamFromPlayerPrefs()
        {
            int num;
            int num2;
            List<PartyEditData> list;
            num = SRPG_Extensions.GetMaxTeamCount(10);
            list = PartyUtility.LoadTeamPresets(10, &num2, 0);
            if (list != null)
            {
                goto Label_001F;
            }
            list = new List<PartyEditData>();
        Label_001F:
            this.ValidateTeam(this.mCurrentQuest, list, num);
            return list;
        }

        private void OnClickTeamPanel(int index)
        {
            GlobalVars.SelectedTeamIndex = index;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x44c);
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
            if (param.quests == null)
            {
                goto Label_0123;
            }
            if (param.quests.Count <= 0)
            {
                goto Label_0123;
            }
            this.mCurrentQuest = param.quests[0];
            GlobalVars.SelectedQuestID = this.mCurrentQuest.iname;
            DataSource.Bind<QuestParam>(base.get_gameObject(), this.mCurrentQuest);
            this.ResetMissionButton();
            this.LoadBossData(this.mCurrentQuest);
            this.LoadTeam();
        Label_0123:
            return;
        }

        private void OnOpenItemDetail(GameObject go)
        {
            QuestParam param;
            QuestCampaignData[] dataArray;
            param = DataSource.FindDataOfClass<QuestParam>(go, null);
            if (((this.mDetailInfo == null) == null) || (param == null))
            {
                goto Label_006E;
            }
            this.mDetailInfo = Object.Instantiate<GameObject>(this.DetailTemplate);
            DataSource.Bind<QuestParam>(this.mDetailInfo, param);
            dataArray = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(param);
            DataSource.Bind<QuestCampaignData[]>(this.mDetailInfo, (((int) dataArray.Length) != null) ? dataArray : null);
            this.mDetailInfo.SetActive(1);
        Label_006E:
            return;
        }

        private unsafe void Refresh()
        {
            GameManager manager;
            ChapterParam[] paramArray;
            QuestParam[] paramArray2;
            long num;
            ChapterParam param;
            List<ChapterParam> list;
            int num2;
            ChapterParam param2;
            StringBuilder builder;
            ListItemEvents events;
            ListItemEvents events2;
            ButtonEvent event2;
            ButtonEvent[] eventArray;
            int num3;
            KeyQuestBanner banner;
            int num4;
            ListItemEvents events3;
            GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
            this.mItems.Clear();
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.Chapters;
            paramArray2 = manager.Player.AvailableQuests;
            num = Network.GetServerTime();
            list = this.GetAvailableChapters(paramArray, paramArray2, num, &param);
            this.mCurrentChapter = param;
            num2 = 0;
            goto Label_01CE;
        Label_0052:
            param2 = list[num2];
            if (string.IsNullOrEmpty(param2.prefabPath) == null)
            {
                goto Label_0073;
            }
            goto Label_01C8;
        Label_0073:
            builder = GameUtility.GetStringBuilder();
            builder.Append("QuestChapters/");
            builder.Append(param2.prefabPath);
            events = AssetManager.Load<ListItemEvents>(builder.ToString());
            if ((events == null) == null)
            {
                goto Label_00B6;
            }
            goto Label_01C8;
        Label_00B6:
            events2 = Object.Instantiate<ListItemEvents>(events);
            eventArray = events2.GetComponentsInChildren<ButtonEvent>(1);
            num3 = 0;
            goto Label_00EB;
        Label_00D1:
            event2 = eventArray[num3];
            event2.syncEvent = this.ChapterScrollRect;
            num3 += 1;
        Label_00EB:
            if (num3 < ((int) eventArray.Length))
            {
                goto Label_00D1;
            }
            DataSource.Bind<ChapterParam>(events2.get_gameObject(), param2);
            if (param2.quests == null)
            {
                goto Label_013B;
            }
            if (param2.quests.Count <= 0)
            {
                goto Label_013B;
            }
            DataSource.Bind<QuestParam>(events2.get_gameObject(), param2.quests[0]);
        Label_013B:
            banner = events2.get_gameObject().GetComponent<KeyQuestBanner>();
            if ((banner != null) == null)
            {
                goto Label_015D;
            }
            banner.UpdateValue();
        Label_015D:
            events2.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
            events2.get_gameObject().SetActive(1);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            events2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            events2.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
            this.mItems.Add(events2);
        Label_01C8:
            num2 += 1;
        Label_01CE:
            if (num2 < list.Count)
            {
                goto Label_0052;
            }
            num4 = 0;
            goto Label_0224;
        Label_01E4:
            events3 = this.mItems[num4];
            if ((events3 != null) == null)
            {
                goto Label_021E;
            }
            this.mItems[num4].get_gameObject().get_transform().SetSiblingIndex(num4);
        Label_021E:
            num4 += 1;
        Label_0224:
            if (num4 < this.mItems.Count)
            {
                goto Label_01E4;
            }
            this.ResetScroll();
            return;
        }

        private void RefreshQuestTypeText()
        {
            if ((this.QuestTypeText != null) == null)
            {
                goto Label_0026;
            }
            this.QuestTypeText.set_text(LocalizedText.Get("sys.QUESTTYPE_ORDEAL"));
        Label_0026:
            return;
        }

        private void ResetMissionButton()
        {
            if ((this.MissionButton != null) == null)
            {
                goto Label_003E;
            }
            this.MissionButton.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            DataSource.Bind<QuestParam>(this.MissionButton.get_gameObject(), this.mCurrentQuest);
        Label_003E:
            return;
        }

        private void ResetScroll()
        {
            ScrollRect[] rectArray;
            if ((this.ItemContainer != null) == null)
            {
                goto Label_0034;
            }
            rectArray = this.ItemContainer.GetComponentsInParent<ScrollRect>(1);
            if (((int) rectArray.Length) <= 0)
            {
                goto Label_0034;
            }
            rectArray[0].set_verticalNormalizedPosition(1f);
        Label_0034:
            return;
        }

        private void StartQuest()
        {
            List<PartyEditData> list;
            List<SupportData> list2;
            bool flag;
            bool flag2;
            list = GlobalVars.OrdealParties;
            list2 = GlobalVars.OrdealSupports;
            if (PartyUtility.ValidateOrdealTeams(this.mCurrentQuest, list, list2, 0) == null)
            {
                goto Label_0046;
            }
            if (PartyUtility.CheckWarningForOrdealTeams(list, new Action(this.<StartQuest>m__379)) == null)
            {
                goto Label_003B;
            }
            return;
        Label_003B:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e8);
        Label_0046:
            return;
        }

        private void ValidateTeam(QuestParam quest, List<PartyEditData> teams, int maxTeamCount)
        {
            bool flag;
            int num;
            PartyData data;
            flag = 0;
            if (teams.Count <= maxTeamCount)
            {
                goto Label_0023;
            }
            teams = Enumerable.ToList<PartyEditData>(Enumerable.Take<PartyEditData>(teams, maxTeamCount));
            flag = 1;
            goto Label_0062;
        Label_0023:
            if (teams.Count >= maxTeamCount)
            {
                goto Label_0062;
            }
            num = teams.Count;
            goto Label_0059;
        Label_003B:
            data = new PartyData(9);
            teams.Add(new PartyEditData(PartyUtility.CreateOrdealPartyNameFromIndex(num), data));
            num += 1;
        Label_0059:
            if (num < maxTeamCount)
            {
                goto Label_003B;
            }
            flag = 1;
        Label_0062:
            if ((flag | (PartyUtility.ResetToDefaultTeamIfNeededForOrdealQuest(quest, teams) == 0)) == null)
            {
                goto Label_007F;
            }
            PartyUtility.SaveTeamPresets(10, 0, teams, 0);
        Label_007F:
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadTeam>c__AnonStorey368
        {
            internal int index;
            internal OrdealQuestList <>f__this;

            public <LoadTeam>c__AnonStorey368()
            {
                base..ctor();
                return;
            }

            internal void <>m__37A()
            {
                this.<>f__this.OnClickTeamPanel(this.index);
                return;
            }
        }
    }
}

