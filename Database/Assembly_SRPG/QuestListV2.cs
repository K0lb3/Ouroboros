namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "エリートクエストを表示", 0, 1), AddComponentMenu("UI/リスト/クエスト一覧"), Pin(0, "通常クエストを表示", 0, 0), Pin(2, "エクストラクエストを表示", 0, 2), Pin(10, "前回選択したクエストを表示", 0, 10), Pin(0x65, "シナリオクエストが選択された", 1, 0x65), Pin(0x66, "鍵クエストを開ける", 1, 0x66), Pin(0x67, "鍵クエストを閉じる", 1, 0x67), Pin(0x68, "上の階層に戻る", 1, 0x68)]
    public class QuestListV2 : SRPG_ListBase, IFlowInterface, IWebHelp
    {
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public GameObject ItemTemplate;
        public GameObject SpecialQuestItem;
        public GameObject ExtraQuestItem;
        [Description("シナリオクエスト用のアイテムとして使用するゲームオブジェクト")]
        public GameObject ScenarioQuestItem;
        [Description("シナリオクエスト用のアイテムとして使用するゲームオブジェクト")]
        public GameObject EliteQuestItem;
        [Description("挑戦回数を使い果たしたエリートクエストのアイテムとして使用するゲームオブジェクト")]
        public GameObject EliteQuestDisAbleItem;
        [Description("シナリオのエクストラクエスト用のアイテムとして使用するゲームオブジェクト")]
        public GameObject StoryExtraQuestItem;
        [Description("挑戦回数を使い果たしたシナリオのエクストラクエスト用のアイテムとして使用するゲームオブジェクト")]
        public GameObject StoryExtraQuestDisableItem;
        [Description("挑戦回数ありのイベントリストアイテムとして使用するゲームオブジェクト")]
        public GameObject EventTemplateLimit;
        [Description("詳細画面として使用するゲームオブジェクト")]
        public GameObject DetailTemplate;
        [Description("難易度選択ボタン (Normal)")]
        public GameObject BtnNormal;
        [Description("難易度選択ボタン (Elite)")]
        public GameObject BtnElite;
        [Description("難易度選択ボタン (Extra)")]
        public GameObject BtnExtra;
        [Description("ハードクエストのブックマーク表示用ボタン")]
        public GameObject BtnEliteBookmark;
        [Description("ユニットランキング表示ボタン")]
        public GameObject BtnUnitRanking;
        public Dictionary<string, GameObject> mListItemTemplates;
        private static Dictionary<int, float> mScrollPosCache;
        private GameObject mDetailInfo;
        public bool Descending;
        public bool RefreshOnStart;
        public bool ShowAllQuests;
        public UnityEngine.UI.ScrollRect ScrollRect;
        public GameObject AreaInfo;
        [FourCC]
        public int ListID;
        public GameObject ChapterTimer;
        public GameObject BackButton;
        private ChapterParam mCurrentChapter;
        private List<QuestParam> mQuests;
        private QuestDifficulties mDifficultyFilter;
        private int mSetScrollPos;
        private float mNewScrollPos;
        private bool mIsQuestsRefreshed;

        static QuestListV2()
        {
            mScrollPosCache = new Dictionary<int, float>();
            return;
        }

        public QuestListV2()
        {
            this.mListItemTemplates = new Dictionary<string, GameObject>();
            this.Descending = 1;
            this.RefreshOnStart = 1;
            this.ShowAllQuests = 1;
            this.mQuests = new List<QuestParam>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            bool flag;
            flag = 0;
            if (pinID != null)
            {
                goto Label_0015;
            }
            flag = this.Refresh(0);
            goto Label_0052;
        Label_0015:
            if (pinID != 1)
            {
                goto Label_0029;
            }
            flag = this.Refresh(1);
            goto Label_0052;
        Label_0029:
            if (pinID != 2)
            {
                goto Label_003D;
            }
            flag = this.Refresh(2);
            goto Label_0052;
        Label_003D:
            if (pinID != 10)
            {
                goto Label_0052;
            }
            flag = this.Refresh(this.PlayingDifficultiy());
        Label_0052:
            if (flag != null)
            {
                goto Label_0061;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
            return;
        Label_0061:
            return;
        }

        public bool ExistsQuest(QuestDifficulties difficulty)
        {
            <ExistsQuest>c__AnonStorey383 storey;
            storey = new <ExistsQuest>c__AnonStorey383();
            storey.difficulty = difficulty;
            return Enumerable.Any<QuestParam>(this.mQuests, new Func<QuestParam, bool>(storey.<>m__3CD));
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

        private bool HasEliteQuest(QuestParam q)
        {
            QuestParam[] paramArray;
            int num;
            paramArray = MonoSingleton<GameManager>.Instance.Quests;
            num = 0;
            goto Label_003F;
        Label_0012:
            if (paramArray[num].difficulty != 1)
            {
                goto Label_003B;
            }
            if (Array.IndexOf<string>(paramArray[num].cond_quests, q.iname) < 0)
            {
                goto Label_003B;
            }
            return 1;
        Label_003B:
            num += 1;
        Label_003F:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0012;
            }
            return 0;
        }

        protected override void LateUpdate()
        {
            if (this.mSetScrollPos <= 0)
            {
                goto Label_0036;
            }
            this.mSetScrollPos -= 1;
            if (this.mSetScrollPos != null)
            {
                goto Label_0036;
            }
            this.ScrollRect.set_verticalNormalizedPosition(this.mNewScrollPos);
        Label_0036:
            base.LateUpdate();
            return;
        }

        private GameObject LoadQuestListItem(QuestParam param)
        {
            GameObject obj2;
            if (string.IsNullOrEmpty(param.ItemLayout) == null)
            {
                goto Label_0012;
            }
            return null;
        Label_0012:
            if (this.mListItemTemplates.ContainsKey(param.ItemLayout) == null)
            {
                goto Label_003A;
            }
            return this.mListItemTemplates[param.ItemLayout];
        Label_003A:
            obj2 = AssetManager.Load<GameObject>("QuestListItems/" + param.ItemLayout);
            if ((obj2 != null) == null)
            {
                goto Label_006E;
            }
            this.mListItemTemplates.Add(param.ItemLayout, obj2);
        Label_006E:
            return obj2;
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

        private void OnCloseKeyQuest(GameObject go)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
            return;
        }

        protected override void OnDestroy()
        {
            GameManager manager;
            base.OnDestroy();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_003A;
            }
            manager.OnPlayerLvChange = (GameManager.PlayerLvChangeEvent) Delegate.Remove(manager.OnPlayerLvChange, new GameManager.PlayerLvChangeEvent(this.RefreshItems));
        Label_003A:
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

        private void OnSelectItem(GameObject go)
        {
            object[] objArray1;
            QuestParam param;
            string str;
            FlowNode_OnQuestSelect select;
            if ((this.ScrollRect != null) == null)
            {
                goto Label_0037;
            }
            if (this.ListID == null)
            {
                goto Label_0037;
            }
            mScrollPosCache[this.ListID] = this.ScrollRect.get_verticalNormalizedPosition();
        Label_0037:
            GlobalVars.SelectedRankingQuestParam = DataSource.FindDataOfClass<RankingQuestParam>(go, null);
            param = DataSource.FindDataOfClass<QuestParam>(go, null);
            if (param == null)
            {
                goto Label_0154;
            }
            if (param.IsKeyQuest == null)
            {
                goto Label_00CA;
            }
            if (param.IsKeyUnlock(-1L) != null)
            {
                goto Label_00CA;
            }
            if (param.Chapter == null)
            {
                goto Label_00A0;
            }
            if (param.Chapter.CheckHasKeyItem() == null)
            {
                goto Label_00A0;
            }
            if (param.IsDateUnlock(-1L) == null)
            {
                goto Label_00A0;
            }
            GlobalVars.KeyQuestTimeOver = 1;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
            return;
        Label_00A0:
            UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_AVAILABLE_CAUTION"), new UIUtility.DialogResultEvent(this.OnCloseKeyQuest), null, 1, -1);
            return;
        Label_00CA:
            GlobalVars.SelectedQuestID = param.iname;
            GlobalVars.LastQuestState.Set(param.state);
            if (param.IsScenario == null)
            {
                goto Label_00FD;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            goto Label_0154;
        Label_00FD:
            if (param.aplv <= MonoSingleton<GameManager>.Instance.Player.Lv)
            {
                goto Label_013C;
            }
            objArray1 = new object[] { (short) param.aplv };
            NotifyList.Push(LocalizedText.Get("sys.QUEST_AP_CONDITION", objArray1));
        Label_013C:
            select = Object.FindObjectOfType<FlowNode_OnQuestSelect>();
            if ((select != null) == null)
            {
                goto Label_0154;
            }
            select.Selected();
        Label_0154:
            return;
        }

        private QuestDifficulties PlayingDifficultiy()
        {
            QuestParam param;
            param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            return ((param == null) ? 0 : param.difficulty);
        }

        private bool Refresh(QuestDifficulties difficulty)
        {
            QuestStates states;
            if (GlobalVars.RankingQuestSelected == null)
            {
                goto Label_0046;
            }
            GlobalVars.RankingQuestSelected = 0;
            this.mIsQuestsRefreshed = 1;
            if (this.RefreshRankingQuests() != null)
            {
                goto Label_0024;
            }
            return 0;
        Label_0024:
            if ((this.BackButton != null) == null)
            {
                goto Label_0070;
            }
            this.BackButton.SetActive(0);
            goto Label_0070;
        Label_0046:
            if (this.RefreshQuests() != null)
            {
                goto Label_0053;
            }
            return 0;
        Label_0053:
            if ((this.BackButton != null) == null)
            {
                goto Label_0070;
            }
            this.BackButton.SetActive(1);
        Label_0070:
            if (difficulty != null)
            {
                goto Label_0085;
            }
            if (this.ExistsQuest(0) != null)
            {
                goto Label_0085;
            }
            difficulty = 1;
        Label_0085:
            if (difficulty != 1)
            {
                goto Label_009B;
            }
            if (this.ExistsQuest(1) != null)
            {
                goto Label_009B;
            }
            difficulty = 2;
        Label_009B:
            if (difficulty != 2)
            {
                goto Label_00B1;
            }
            if (this.ExistsQuest(2) != null)
            {
                goto Label_00B1;
            }
            difficulty = 0;
        Label_00B1:
            this.mDifficultyFilter = difficulty;
            this.RefreshItems();
            if ((this.ScrollRect != null) == null)
            {
                goto Label_0159;
            }
            this.ScrollRect.set_normalizedPosition(Vector2.get_one());
            if (HomeWindow.GetRestorePoint() != 1)
            {
                goto Label_0159;
            }
            if (string.IsNullOrEmpty(GlobalVars.LastPlayedQuest.Get()) != null)
            {
                goto Label_0159;
            }
            if (GlobalVars.LastQuestState != 2)
            {
                goto Label_0159;
            }
            if (mScrollPosCache.ContainsKey(this.ListID) == null)
            {
                goto Label_0159;
            }
            this.mSetScrollPos = 2;
            this.mNewScrollPos = mScrollPosCache[this.ListID];
            mScrollPosCache.Remove(this.ListID);
            HomeWindow.SetRestorePoint(0);
        Label_0159:
            this.RefreshButtons(difficulty);
            if (difficulty != null)
            {
                goto Label_01A0;
            }
            if ((this.BtnNormal != null) == null)
            {
                goto Label_0216;
            }
            if (this.ExistsQuest(1) != null)
            {
                goto Label_0216;
            }
            if (this.ExistsQuest(2) != null)
            {
                goto Label_0216;
            }
            this.BtnNormal.SetActive(0);
            goto Label_0216;
        Label_01A0:
            if (difficulty != 1)
            {
                goto Label_01E1;
            }
            if ((this.BtnElite != null) == null)
            {
                goto Label_0216;
            }
            if (this.ExistsQuest(2) != null)
            {
                goto Label_0216;
            }
            if (this.ExistsQuest(0) != null)
            {
                goto Label_0216;
            }
            this.BtnElite.SetActive(0);
            goto Label_0216;
        Label_01E1:
            if ((this.BtnExtra != null) == null)
            {
                goto Label_0216;
            }
            if (this.ExistsQuest(0) != null)
            {
                goto Label_0216;
            }
            if (this.ExistsQuest(1) != null)
            {
                goto Label_0216;
            }
            this.BtnExtra.SetActive(0);
        Label_0216:
            return 1;
        }

        private void RefreshButtons(QuestDifficulties difficulty)
        {
            QuestDifficulties difficulties;
            if ((null == this.BtnNormal) != null)
            {
                goto Label_0033;
            }
            if ((null == this.BtnElite) != null)
            {
                goto Label_0033;
            }
            if ((null == this.BtnExtra) == null)
            {
                goto Label_0034;
            }
        Label_0033:
            return;
        Label_0034:
            difficulties = difficulty;
            switch (difficulties)
            {
                case 0:
                    goto Label_004D;

                case 1:
                    goto Label_008E;

                case 2:
                    goto Label_00CF;
            }
            goto Label_0110;
        Label_004D:
            this.BtnNormal.SetActive(1);
            this.BtnUnitRanking.SetActive(1);
            this.BtnEliteBookmark.SetActive(1);
            this.BtnElite.SetActive(0);
            this.BtnExtra.SetActive(0);
            goto Label_0110;
        Label_008E:
            this.BtnUnitRanking.SetActive(1);
            this.BtnEliteBookmark.SetActive(1);
            this.BtnNormal.SetActive(0);
            this.BtnElite.SetActive(1);
            this.BtnExtra.SetActive(0);
            goto Label_0110;
        Label_00CF:
            this.BtnUnitRanking.SetActive(1);
            this.BtnEliteBookmark.SetActive(1);
            this.BtnNormal.SetActive(0);
            this.BtnElite.SetActive(0);
            this.BtnExtra.SetActive(1);
        Label_0110:
            return;
        }

        private void RefreshChapterTimer()
        {
            bool flag;
            ChapterParam param;
            KeyQuestTypes types;
            KeyQuestTypes types2;
            if ((this.ChapterTimer != null) == null)
            {
                goto Label_0091;
            }
            flag = 0;
            if (this.mCurrentChapter == null)
            {
                goto Label_007A;
            }
            param = this.mCurrentChapter;
            if (param == null)
            {
                goto Label_007A;
            }
            DataSource.Bind<ChapterParam>(this.ChapterTimer, param);
            types2 = param.GetKeyQuestType();
            if (types2 == 1)
            {
                goto Label_0053;
            }
            if (types2 == 2)
            {
                goto Label_0063;
            }
            goto Label_006A;
        Label_0053:
            flag = param.key_end > 0L;
            goto Label_007A;
        Label_0063:
            flag = 0;
            goto Label_007A;
        Label_006A:
            flag = param.end > 0L;
        Label_007A:
            this.ChapterTimer.SetActive(flag);
            GameParameter.UpdateAll(this.ChapterTimer);
        Label_0091:
            return;
        }

        private void RefreshItems()
        {
            Transform transform;
            QuestParam[] paramArray;
            int num;
            QuestParam param;
            GameObject obj2;
            GameObject obj3;
            RankingQuestParam param2;
            QuestCampaignData[] dataArray;
            ListItemEvents events;
            transform = base.get_transform();
            base.ClearItems();
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            if (((this.AreaInfo != null) == null) || (string.IsNullOrEmpty(GlobalVars.SelectedSection) != null))
            {
                goto Label_0063;
            }
            DataSource.Bind<ChapterParam>(this.AreaInfo, MonoSingleton<GameManager>.Instance.FindArea(GlobalVars.SelectedSection));
        Label_0063:
            paramArray = this.mQuests.ToArray();
            if (this.Descending == null)
            {
                goto Label_0080;
            }
            Array.Reverse(paramArray);
        Label_0080:
            num = 0;
            goto Label_0282;
        Label_0087:
            param = paramArray[num];
            if (param.difficulty == this.mDifficultyFilter)
            {
                goto Label_00A1;
            }
            goto Label_027E;
        Label_00A1:
            obj2 = null;
            if (string.IsNullOrEmpty(param.ItemLayout) != null)
            {
                goto Label_00BD;
            }
            obj2 = this.LoadQuestListItem(param);
        Label_00BD:
            if ((obj2 == null) == null)
            {
                goto Label_01A5;
            }
            if (param.difficulty != 1)
            {
                goto Label_00FB;
            }
            if (param.CheckEnableChallange() == null)
            {
                goto Label_00EE;
            }
            obj2 = this.EliteQuestItem;
            goto Label_00F6;
        Label_00EE:
            obj2 = this.EliteQuestDisAbleItem;
        Label_00F6:
            goto Label_01A5;
        Label_00FB:
            if (param.difficulty != 2)
            {
                goto Label_012C;
            }
            if (param.CheckEnableChallange() == null)
            {
                goto Label_011F;
            }
            obj2 = this.StoryExtraQuestItem;
            goto Label_0127;
        Label_011F:
            obj2 = this.StoryExtraQuestDisableItem;
        Label_0127:
            goto Label_01A5;
        Label_012C:
            if (param.IsScenario == null)
            {
                goto Label_0144;
            }
            obj2 = this.ScenarioQuestItem;
            goto Label_01A5;
        Label_0144:
            if (((this.SpecialQuestItem != null) == null) || (this.HasEliteQuest(param) == null))
            {
                goto Label_016E;
            }
            obj2 = this.SpecialQuestItem;
            goto Label_01A5;
        Label_016E:
            if (param.GetChallangeLimit() <= 0)
            {
                goto Label_0187;
            }
            obj2 = this.EventTemplateLimit;
            goto Label_01A5;
        Label_0187:
            obj2 = (param.IsExtra == null) ? this.ItemTemplate : this.ExtraQuestItem;
        Label_01A5:
            if ((obj2 == null) == null)
            {
                goto Label_01B7;
            }
            goto Label_027E;
        Label_01B7:
            obj3 = Object.Instantiate<GameObject>(obj2);
            obj3.set_hideFlags(0x34);
            DataSource.Bind<QuestParam>(obj3, param);
            param2 = MonoSingleton<GameManager>.Instance.FindAvailableRankingQuest(param.iname);
            DataSource.Bind<RankingQuestParam>(obj3, param2);
            DataSource.Bind<QuestParam>(obj3, param);
            dataArray = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(param);
            DataSource.Bind<QuestCampaignData[]>(obj3, (((int) dataArray.Length) != null) ? dataArray : null);
            events = obj3.GetComponent<ListItemEvents>();
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            events.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
            obj3.get_transform().SetParent(transform, 0);
            obj3.get_gameObject().SetActive(1);
            base.AddItem(events);
        Label_027E:
            num += 1;
        Label_0282:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0087;
            }
            return;
        }

        private bool RefreshQuests()
        {
            QuestParam[] paramArray;
            int num;
            QuestParam param;
            this.mCurrentChapter = MonoSingleton<GameManager>.Instance.FindArea(GlobalVars.SelectedChapter);
            this.mQuests.Clear();
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            num = 0;
            goto Label_00EE;
        Label_003C:
            param = paramArray[num];
            if (paramArray[num].IsMulti == null)
            {
                goto Label_0052;
            }
            goto Label_00EA;
        Label_0052:
            if (this.ShowAllQuests != null)
            {
                goto Label_0088;
            }
            if (this.mCurrentChapter == null)
            {
                goto Label_00EA;
            }
            if ((this.mCurrentChapter.iname != param.ChapterID) == null)
            {
                goto Label_0088;
            }
            goto Label_00EA;
        Label_0088:
            if (this.mCurrentChapter.IsGpsQuest() == null)
            {
                goto Label_00BA;
            }
            if (param.gps_enable == null)
            {
                goto Label_00EA;
            }
            if (param.type == 10)
            {
                goto Label_00CC;
            }
            goto Label_00EA;
            goto Label_00CC;
        Label_00BA:
            if (param.type != 10)
            {
                goto Label_00CC;
            }
            goto Label_00EA;
        Label_00CC:
            if (param.IsDateUnlock(-1L) != null)
            {
                goto Label_00DE;
            }
            goto Label_00EA;
        Label_00DE:
            this.mQuests.Add(param);
        Label_00EA:
            num += 1;
        Label_00EE:
            if (num < ((int) paramArray.Length))
            {
                goto Label_003C;
            }
            this.RefreshChapterTimer();
            return (this.mQuests.Count > 0);
        }

        private bool RefreshRankingQuests()
        {
            List<RankingQuestParam> list;
            List<QuestParam> list2;
            int num;
            int num2;
            QuestParam param;
            <RefreshRankingQuests>c__AnonStorey384 storey;
            list = MonoSingleton<GameManager>.Instance.AvailableRankingQuesstParams;
            list2 = new List<QuestParam>();
            num = 0;
            goto Label_0071;
        Label_0018:
            storey = new <RefreshRankingQuests>c__AnonStorey384();
            storey.quest = MonoSingleton<GameManager>.Instance.FindQuest(list[num].iname);
            if (storey.quest == null)
            {
                goto Label_006D;
            }
            if (list2.Find(new Predicate<QuestParam>(storey.<>m__3CE)) != null)
            {
                goto Label_006D;
            }
            list2.Add(storey.quest);
        Label_006D:
            num += 1;
        Label_0071:
            if (num < list.Count)
            {
                goto Label_0018;
            }
            this.mQuests.Clear();
            num2 = 0;
            goto Label_00F6;
        Label_008F:
            param = list2[num2];
            if (list2[num2].IsMulti == null)
            {
                goto Label_00AE;
            }
            goto Label_00F2;
        Label_00AE:
            if (param.type != 10)
            {
                goto Label_00C1;
            }
            goto Label_00F2;
        Label_00C1:
            if (param.IsDateUnlock(-1L) != null)
            {
                goto Label_00D4;
            }
            goto Label_00F2;
        Label_00D4:
            if (param.IsQuestCondition() != null)
            {
                goto Label_00E5;
            }
            goto Label_00F2;
        Label_00E5:
            this.mQuests.Add(param);
        Label_00F2:
            num2 += 1;
        Label_00F6:
            if (num2 < list2.Count)
            {
                goto Label_008F;
            }
            this.RefreshChapterTimer();
            return (this.mQuests.Count > 0);
        }

        protected override void Start()
        {
            GameManager local1;
            ChapterParam param;
            base.Start();
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0038;
            }
            if (this.ItemTemplate.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0038;
            }
            this.ItemTemplate.SetActive(0);
        Label_0038:
            if ((this.SpecialQuestItem != null) == null)
            {
                goto Label_006A;
            }
            if (this.SpecialQuestItem.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_006A;
            }
            this.SpecialQuestItem.SetActive(0);
        Label_006A:
            if ((this.ExtraQuestItem != null) == null)
            {
                goto Label_009C;
            }
            if (this.ExtraQuestItem.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_009C;
            }
            this.ExtraQuestItem.SetActive(0);
        Label_009C:
            if ((this.DetailTemplate != null) == null)
            {
                goto Label_00CE;
            }
            if (this.DetailTemplate.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_00CE;
            }
            this.DetailTemplate.SetActive(0);
        Label_00CE:
            if ((this.ScenarioQuestItem != null) == null)
            {
                goto Label_0100;
            }
            if (this.ScenarioQuestItem.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0100;
            }
            this.ScenarioQuestItem.SetActive(0);
        Label_0100:
            if ((this.EliteQuestItem != null) == null)
            {
                goto Label_0132;
            }
            if (this.EliteQuestItem.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0132;
            }
            this.EliteQuestItem.SetActive(0);
        Label_0132:
            if ((this.EliteQuestDisAbleItem != null) == null)
            {
                goto Label_0164;
            }
            if (this.EliteQuestDisAbleItem.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0164;
            }
            this.EliteQuestDisAbleItem.SetActive(0);
        Label_0164:
            if ((this.StoryExtraQuestItem != null) == null)
            {
                goto Label_0196;
            }
            if (this.StoryExtraQuestItem.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0196;
            }
            this.StoryExtraQuestItem.SetActive(0);
        Label_0196:
            if ((this.StoryExtraQuestDisableItem != null) == null)
            {
                goto Label_01C8;
            }
            if (this.StoryExtraQuestDisableItem.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_01C8;
            }
            this.StoryExtraQuestDisableItem.SetActive(0);
        Label_01C8:
            if (this.RefreshOnStart == null)
            {
                goto Label_025A;
            }
            if (this.mIsQuestsRefreshed != null)
            {
                goto Label_025A;
            }
            this.RefreshQuests();
            this.RefreshItems();
            param = this.mCurrentChapter;
            if (param == null)
            {
                goto Label_025A;
            }
            if (param.IsKeyQuest() == null)
            {
                goto Label_025A;
            }
            if (param.IsKeyUnlock(Network.GetServerTime()) != null)
            {
                goto Label_025A;
            }
            if (param.CheckHasKeyItem() == null)
            {
                goto Label_0231;
            }
            GlobalVars.KeyQuestTimeOver = 1;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
            goto Label_025A;
        Label_0231:
            UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_AVAILABLE_CAUTION"), new UIUtility.DialogResultEvent(this.OnCloseKeyQuest), null, 1, -1);
        Label_025A:
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnPlayerLvChange = (GameManager.PlayerLvChangeEvent) Delegate.Combine(local1.OnPlayerLvChange, new GameManager.PlayerLvChangeEvent(this.RefreshItems));
            return;
        }

        [CompilerGenerated]
        private sealed class <ExistsQuest>c__AnonStorey383
        {
            internal QuestDifficulties difficulty;

            public <ExistsQuest>c__AnonStorey383()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3CD(QuestParam q)
            {
                return (q.difficulty == this.difficulty);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshRankingQuests>c__AnonStorey384
        {
            internal QuestParam quest;

            public <RefreshRankingQuests>c__AnonStorey384()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3CE(QuestParam q)
            {
                return (q.iname == this.quest.iname);
            }
        }
    }
}

