namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(150, "Open IAP Window", 1, 2), Pin(0x97, "Reset Cooldown", 1, 3), Pin(0x98, "Reset Tickets", 1, 4), Pin(100, "Refresh Enemy", 0, 0), Pin(110, "Refresh Party", 0, 0), Pin(0x65, "Player Selected", 1, 1)]
    public class ArenaWindow : MonoBehaviour, IFlowInterface
    {
        public const int PINID_REFRESH_ENEMYLIST = 100;
        public const int PINID_REFRESH_PARTY = 110;
        public const int PINID_PLAYER_SELECTED = 0x65;
        public const int PINID_OPEN_IAPWINDOW = 150;
        public const int PINID_RESET_COOLDOWN = 0x97;
        public const int PINID_RESET_TICKETS = 0x98;
        public GameObject PartyInfo;
        public GameObject DefensePartyInfo;
        public GameObject VsPartyInfo;
        public GameObject VsEnemyPartyInfo;
        public SRPG_ListBase EnemyPlayerList;
        public ListItemEvents EnemyPlayerItem;
        public GameObject EnemyPlayerDetail;
        public GameObject HistoryObject;
        public bool RefreshEnemyListOnStart;
        public bool RefreshPartyOnStart;
        public GameObject[] PartyUnitSlots;
        public GameObject PartyUnitLeader;
        public GameObject PartyUnitLeaderVS;
        public GameObject[] DefenseUnitSlots;
        public GameObject DefenseUnitLeader;
        public GameObject CooldownTimer;
        public Button CooldownResetButton;
        public GameObject BpHolder;
        public GameObject BattlePreWindow;
        public GameObject AttackDeckWindow;
        public GameObject AttackDeckWindowIcon;
        public GameObject DefenseDeckWindow;
        public GameObject DefenseDeckWindowIcon;
        public GameObject EnemyListWindow;
        public GameObject PlayerStatusWindow;
        public Button MatchingButton;
        public Button DeckNextButton;
        public Button DeckPrevButton;
        public Button MatchingCloseButton;
        public Button BattleBackButton;
        public Text LastBattleAtText;
        [Space(10f)]
        public GameObject GoMapInfo;
        public GameObject GoMapInfoThumbnail;
        public GameObject GoMapInfoEndAt;
        public Text TextMapInfoEndAt;
        private bool mIsUpdateMapInfoEndAt;
        private float mPassedTimeMapInfoEndAt;

        public ArenaWindow()
        {
            this.PartyUnitSlots = new GameObject[3];
            this.DefenseUnitSlots = new GameObject[3];
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
            this.RefreshEnemyList();
            goto Label_002D;
        Label_0022:
            this.RefreshParty();
        Label_002D:
            return;
        }

        private void ChangeDrawDeck(bool attack)
        {
            this.AttackDeckWindow.SetActive(attack);
            this.AttackDeckWindowIcon.SetActive(attack);
            this.DeckNextButton.get_gameObject().SetActive(attack);
            this.DefenseDeckWindow.SetActive(attack == 0);
            this.DefenseDeckWindowIcon.SetActive(attack == 0);
            this.DeckPrevButton.get_gameObject().SetActive(attack == 0);
            return;
        }

        private void ChangeDrawInformation(bool playerinfo)
        {
            this.PlayerStatusWindow.SetActive(playerinfo);
            this.EnemyListWindow.SetActive(playerinfo == 0);
            return;
        }

        public void OnBattleBackButtonClick()
        {
            this.BattlePreWindow.SetActive(0);
            return;
        }

        private void OnCooldownButtonClick()
        {
            GameManager manager;
            PlayerData data;
            if (MonoSingleton<GameManager>.Instance.Player.ChallengeArenaCoolDownSec <= 0L)
            {
                goto Label_002F;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.ARENA_WAIT_COOLDOWN"), null, null, 0, -1);
        Label_002F:
            return;
        }

        public void OnDeckNextButtonClick()
        {
            this.ChangeDrawDeck(0);
            return;
        }

        public void OnDeckPrevButtonClick()
        {
            this.ChangeDrawDeck(1);
            return;
        }

        private void OnDisable()
        {
            GameManager local1;
            if ((MonoSingleton<GameManager>.GetInstanceDirect() != null) == null)
            {
                goto Label_0036;
            }
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnDayChange = (GameManager.DayChangeEvent) Delegate.Remove(local1.OnDayChange, new GameManager.DayChangeEvent(this.RefreshBattleCountOnDayChange));
        Label_0036:
            return;
        }

        private void OnEnable()
        {
            GameManager local1;
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnDayChange = (GameManager.DayChangeEvent) Delegate.Combine(local1.OnDayChange, new GameManager.DayChangeEvent(this.RefreshBattleCountOnDayChange));
            return;
        }

        private void OnEnemyDetailSelect(GameObject go)
        {
            ArenaPlayer player;
            GameObject obj2;
            if ((this.EnemyPlayerDetail == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            player = DataSource.FindDataOfClass<ArenaPlayer>(go, null);
            if (player != null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            obj2 = Object.Instantiate<GameObject>(this.EnemyPlayerDetail);
            DataSource.Bind<ArenaPlayer>(obj2, player);
            obj2.GetComponent<ArenaPlayerInfo>().UpdateValue();
            return;
        }

        private void OnEnemySelect(GameObject go)
        {
            ArenaPlayer player;
            GameManager manager;
            PlayerData data;
            long num;
            player = DataSource.FindDataOfClass<ArenaPlayer>(go, null);
            if (player != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            if (AssetDownloader.isDone != null)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data.ChallengeArenaNum > 0)
            {
                goto Label_0049;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.ARENA_DAYLIMIT"), null, null, 0, -1);
            return;
        Label_0049:
            if (data.GetNextChallengeArenaCoolDownSec() <= 0L)
            {
                goto Label_005F;
            }
            this.OnCooldownButtonClick();
            return;
        Label_005F:
            GlobalVars.SelectedArenaPlayer.Set(player);
            if ((this.VsEnemyPartyInfo != null) == null)
            {
                goto Label_0092;
            }
            DataSource.Bind<ArenaPlayer>(this.VsEnemyPartyInfo, player);
            GameParameter.UpdateAll(this.VsEnemyPartyInfo);
        Label_0092:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            this.BattlePreWindow.SetActive(1);
            return;
        }

        public void OnHellpButtonClick(GameObject go)
        {
            this.BattlePreWindow.SetActive(0);
            return;
        }

        public void OnHistoryDisp()
        {
            if ((this.HistoryObject != null) == null)
            {
                goto Label_001D;
            }
            Object.Instantiate<GameObject>(this.HistoryObject);
        Label_001D:
            return;
        }

        public void OnMatchingButtonClick()
        {
            this.ChangeDrawInformation(0);
            return;
        }

        public void OnMatchingCloseButtonClick()
        {
            this.ChangeDrawInformation(1);
            return;
        }

        private void OnResetChallengeTickets(GameObject go)
        {
            GameManager manager;
            string str;
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.Coin >= manager.Player.GetChallengeArenaCost())
            {
                goto Label_004A;
            }
            UIUtility.ConfirmBox(LocalizedText.Get("sys.OUT_OF_COIN_CONFIRM_BUY_COIN"), new UIUtility.DialogResultEvent(this.OpenCoinShop), null, null, 0, -1, null, null);
            goto Label_0055;
        Label_004A:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x98);
        Label_0055:
            return;
        }

        private void OnResetCooldown(GameObject go)
        {
            GameManager manager;
            string str;
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.Coin >= manager.MasterParam.FixParam.ArenaResetCooldownCost)
            {
                goto Label_0054;
            }
            UIUtility.ConfirmBox(LocalizedText.Get("sys.OUT_OF_COIN_CONFIRM_BUY_COIN"), new UIUtility.DialogResultEvent(this.OpenCoinShop), null, null, 0, -1, null, null);
            goto Label_005F;
        Label_0054:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x97);
        Label_005F:
            return;
        }

        private void OpenCoinShop(GameObject go)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 150);
            return;
        }

        private void RefreshAttackParty()
        {
            PlayerData data;
            PartyData data2;
            int num;
            long num2;
            UnitData data3;
            JobData data4;
            UnitData data5;
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = data.FindPartyOfType(3);
            num = 0;
            goto Label_00CB;
        Label_001A:
            num2 = data2.GetUnitUniqueID(num);
            data3 = data.FindUnitDataByUniqueID(num2);
            if (data3 == null)
            {
                goto Label_0075;
            }
            if (data3.GetJobFor(3) == data3.CurrentJob)
            {
                goto Label_0075;
            }
            data5 = new UnitData();
            data5.TempFlags |= 1;
            data5.Setup(data3);
            data5.SetJob(3);
            data3 = data5;
        Label_0075:
            if (num != null)
            {
                goto Label_00AB;
            }
            DataSource.Bind<UnitData>(this.PartyUnitLeader, data3);
            DataSource.Bind<UnitData>(this.PartyUnitLeaderVS, data3);
            GameParameter.UpdateAll(this.PartyUnitLeader);
            GameParameter.UpdateAll(this.PartyUnitLeaderVS);
        Label_00AB:
            DataSource.Bind<UnitData>(this.PartyUnitSlots[num], data3);
            GameParameter.UpdateAll(this.PartyUnitSlots[num]);
            num += 1;
        Label_00CB:
            if (num < ((int) this.PartyUnitSlots.Length))
            {
                goto Label_001A;
            }
            if ((this.PartyInfo != null) == null)
            {
                goto Label_0101;
            }
            DataSource.Bind<PartyData>(this.PartyInfo, data2);
            GameParameter.UpdateAll(this.PartyInfo);
        Label_0101:
            if ((this.VsPartyInfo != null) == null)
            {
                goto Label_0129;
            }
            DataSource.Bind<PartyData>(this.VsPartyInfo, data2);
            GameParameter.UpdateAll(this.VsPartyInfo);
        Label_0129:
            return;
        }

        private unsafe void RefreshBattleCount()
        {
            int num;
            PlayerData data;
            int num2;
            int num3;
            int num4;
            if ((this.BpHolder == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaMax;
            num2 = MonoSingleton<GameManager>.Instance.Player.ChallengeArenaNum;
            if (num2 < num)
            {
                goto Label_0047;
            }
            num2 = num;
        Label_0047:
            num3 = 0;
            goto Label_008B;
        Label_004E:
            num4 = num3 + 1;
            this.BpHolder.get_transform().FindChild("bp" + &num4.ToString()).get_gameObject().SetActive(((num3 + 1) > num2) == 0);
            num3 += 1;
        Label_008B:
            if (num3 < num)
            {
                goto Label_004E;
            }
            return;
        }

        private unsafe void RefreshBattleCountOnDayChange()
        {
            int num;
            int num2;
            int num3;
            int num4;
            if ((this.BpHolder == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaMax;
            num2 = num;
            num3 = 0;
            goto Label_0071;
        Label_0035:
            num4 = num3 + 1;
            this.BpHolder.get_transform().FindChild("bp" + &num4.ToString()).get_gameObject().SetActive(((num3 + 1) > num2) == 0);
            num3 += 1;
        Label_0071:
            if (num3 < num)
            {
                goto Label_0035;
            }
            return;
        }

        private void RefreshCooldowns()
        {
            GameManager manager;
            PlayerData data;
            long num;
            bool flag;
            CanvasRenderer renderer;
            data = MonoSingleton<GameManager>.Instance.Player;
            data.UpdateChallengeArenaTimer();
            if ((this.CooldownTimer == null) == null)
            {
                goto Label_0025;
            }
            return;
        Label_0025:
            flag = (data.GetNextChallengeArenaCoolDownSec() <= 0L) ? 0 : (data.ChallengeArenaNum > 0);
            this.CooldownTimer.SetActive(flag);
            if (this.BpHolder == null)
            {
                goto Label_0092;
            }
            renderer = this.BpHolder.GetComponent<CanvasRenderer>();
            if (renderer == null)
            {
                goto Label_0092;
            }
            renderer.SetColor((flag == null) ? Color.get_white() : Color.get_gray());
        Label_0092:
            return;
        }

        private void RefreshDefenseParty()
        {
            PlayerData data;
            PartyData data2;
            int num;
            long num2;
            UnitData data3;
            JobData data4;
            UnitData data5;
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = data.FindPartyOfType(4);
            num = 0;
            goto Label_00B3;
        Label_001A:
            num2 = data2.GetUnitUniqueID(num);
            data3 = data.FindUnitDataByUniqueID(num2);
            if (data3 == null)
            {
                goto Label_0075;
            }
            if (data3.GetJobFor(4) == data3.CurrentJob)
            {
                goto Label_0075;
            }
            data5 = new UnitData();
            data5.TempFlags |= 1;
            data5.Setup(data3);
            data5.SetJob(4);
            data3 = data5;
        Label_0075:
            if (num != null)
            {
                goto Label_0093;
            }
            DataSource.Bind<UnitData>(this.DefenseUnitLeader, data3);
            GameParameter.UpdateAll(this.DefenseUnitLeader);
        Label_0093:
            DataSource.Bind<UnitData>(this.DefenseUnitSlots[num], data3);
            GameParameter.UpdateAll(this.DefenseUnitSlots[num]);
            num += 1;
        Label_00B3:
            if (num < ((int) this.PartyUnitSlots.Length))
            {
                goto Label_001A;
            }
            if ((this.DefensePartyInfo != null) == null)
            {
                goto Label_00E9;
            }
            DataSource.Bind<PartyData>(this.DefensePartyInfo, data2);
            GameParameter.UpdateAll(this.DefensePartyInfo);
        Label_00E9:
            return;
        }

        private void RefreshEnemyList()
        {
            ArenaPlayer[] playerArray;
            Transform transform;
            int num;
            ListItemEvents events;
            GameManager manager;
            PlayerData data;
            DataSource source;
            QuestParam param;
            if ((this.EnemyPlayerList == null) != null)
            {
                goto Label_0022;
            }
            if ((this.EnemyPlayerItem == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            this.EnemyPlayerList.ClearItems();
            playerArray = MonoSingleton<GameManager>.Instance.ArenaPlayers;
            transform = this.EnemyPlayerList.get_transform();
            num = 0;
            goto Label_00EB;
        Label_004C:
            events = Object.Instantiate<ListItemEvents>(this.EnemyPlayerItem);
            DataSource.Bind<ArenaPlayer>(events.get_gameObject(), playerArray[num]);
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnEnemySelect);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnEnemyDetailSelect);
            this.EnemyPlayerList.AddItem(events);
            events.get_transform().SetParent(transform, 0);
            events.get_gameObject().SetActive(1);
            AssetManager.PrepareAssets(AssetPath.UnitSkinImage(playerArray[num].Unit[0].UnitParam, playerArray[num].Unit[0].GetSelectedSkin(-1), playerArray[num].Unit[0].CurrentJobId));
            num += 1;
        Label_00EB:
            if (num < ((int) playerArray.Length))
            {
                goto Label_004C;
            }
            if (AssetDownloader.isDone != null)
            {
                goto Label_0107;
            }
            AssetDownloader.StartDownload(0, 1, 2);
        Label_0107:
            if (this.GoMapInfo == null)
            {
                goto Label_018C;
            }
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            if (manager == null)
            {
                goto Label_018C;
            }
            if (data == null)
            {
                goto Label_018C;
            }
            source = this.GoMapInfo.GetComponent<DataSource>();
            if (source == null)
            {
                goto Label_015A;
            }
            source.Clear();
        Label_015A:
            param = manager.FindQuest(GlobalVars.SelectedQuestID);
            DataSource.Bind<QuestParam>(this.GoMapInfo, param);
            GameParameter.UpdateAll(this.GoMapInfo);
            this.mIsUpdateMapInfoEndAt = this.RefreshMapInfoEndAt();
        Label_018C:
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
            if (this.GoMapInfoEndAt == null)
            {
                goto Label_008A;
            }
            this.GoMapInfoEndAt.SetActive(flag2);
        Label_008A:
            if (flag2 != null)
            {
                goto Label_00A4;
            }
            if (flag == null)
            {
                goto Label_00A2;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "REFRESH_ARENA_INFO");
        Label_00A2:
            return 0;
        Label_00A4:
            str = "sys.ARENA_TIMELIMIT_";
            str2 = string.Empty;
            if (&span.Days == null)
            {
                goto Label_00EB;
            }
            objArray1 = new object[] { (int) &span.Days };
            str2 = LocalizedText.Get(str + "D", objArray1);
            goto Label_0152;
        Label_00EB:
            if (&span.Hours == null)
            {
                goto Label_0124;
            }
            objArray2 = new object[] { (int) &span.Hours };
            str2 = LocalizedText.Get(str + "H", objArray2);
            goto Label_0152;
        Label_0124:
            objArray3 = new object[] { (int) Mathf.Max(&span.Minutes, 0) };
            str2 = LocalizedText.Get(str + "M", objArray3);
        Label_0152:
            if (this.TextMapInfoEndAt == null)
            {
                goto Label_0186;
            }
            if ((this.TextMapInfoEndAt.get_text() != str2) == null)
            {
                goto Label_0186;
            }
            this.TextMapInfoEndAt.set_text(str2);
        Label_0186:
            this.mPassedTimeMapInfoEndAt = 1f;
            return 1;
        }

        private void RefreshParty()
        {
            this.RefreshAttackParty();
            this.RefreshDefenseParty();
            return;
        }

        private void Start()
        {
            if ((this.EnemyPlayerItem != null) == null)
            {
                goto Label_0022;
            }
            this.EnemyPlayerItem.get_gameObject().SetActive(0);
        Label_0022:
            if (this.RefreshEnemyListOnStart == null)
            {
                goto Label_0033;
            }
            this.RefreshEnemyList();
        Label_0033:
            if (this.RefreshPartyOnStart == null)
            {
                goto Label_0044;
            }
            this.RefreshParty();
        Label_0044:
            if ((this.CooldownResetButton != null) == null)
            {
                goto Label_0071;
            }
            this.CooldownResetButton.get_onClick().AddListener(new UnityAction(this, this.OnCooldownButtonClick));
        Label_0071:
            this.BattlePreWindow.SetActive(0);
            this.ChangeDrawDeck(1);
            this.ChangeDrawInformation(1);
            this.RefreshBattleCount();
            if ((this.MatchingButton != null) == null)
            {
                goto Label_00BE;
            }
            this.MatchingButton.get_onClick().AddListener(new UnityAction(this, this.OnMatchingButtonClick));
        Label_00BE:
            if ((this.MatchingCloseButton != null) == null)
            {
                goto Label_00EB;
            }
            this.MatchingCloseButton.get_onClick().AddListener(new UnityAction(this, this.OnMatchingCloseButtonClick));
        Label_00EB:
            if ((this.DeckNextButton != null) == null)
            {
                goto Label_0118;
            }
            this.DeckNextButton.get_onClick().AddListener(new UnityAction(this, this.OnDeckNextButtonClick));
        Label_0118:
            if ((this.DeckPrevButton != null) == null)
            {
                goto Label_0145;
            }
            this.DeckPrevButton.get_onClick().AddListener(new UnityAction(this, this.OnDeckPrevButtonClick));
        Label_0145:
            if ((this.BattleBackButton != null) == null)
            {
                goto Label_0172;
            }
            this.BattleBackButton.get_onClick().AddListener(new UnityAction(this, this.OnBattleBackButtonClick));
        Label_0172:
            return;
        }

        private unsafe void Update()
        {
            PlayerData data;
            DateTime time;
            this.RefreshCooldowns();
            if (string.IsNullOrEmpty(this.LastBattleAtText.get_text()) == null)
            {
                goto Label_005B;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            if ((data.ArenaLastAt > GameUtility.UnixtimeToLocalTime(0L)) == null)
            {
                goto Label_005B;
            }
            this.LastBattleAtText.set_text(&data.ArenaLastAt.ToString("MM/dd HH:mm"));
        Label_005B:
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

