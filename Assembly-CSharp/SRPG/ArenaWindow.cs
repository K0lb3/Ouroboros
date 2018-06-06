// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Refresh Enemy", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(110, "Refresh Party", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Player Selected", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(151, "Reset Cooldown", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(150, "Open IAP Window", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(152, "Reset Tickets", FlowNode.PinTypes.Output, 4)]
  public class ArenaWindow : MonoBehaviour, IFlowInterface
  {
    public const int PINID_REFRESH_ENEMYLIST = 100;
    public const int PINID_REFRESH_PARTY = 110;
    public const int PINID_PLAYER_SELECTED = 101;
    public const int PINID_OPEN_IAPWINDOW = 150;
    public const int PINID_RESET_COOLDOWN = 151;
    public const int PINID_RESET_TICKETS = 152;
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

    public ArenaWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.EnemyPlayerItem, (Object) null))
        ((Component) this.EnemyPlayerItem).get_gameObject().SetActive(false);
      if (this.RefreshEnemyListOnStart)
        this.RefreshEnemyList();
      if (this.RefreshPartyOnStart)
        this.RefreshParty();
      if (Object.op_Inequality((Object) this.CooldownResetButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CooldownResetButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCooldownButtonClick)));
      }
      this.BattlePreWindow.SetActive(false);
      this.ChangeDrawDeck(true);
      this.ChangeDrawInformation(true);
      this.RefreshBattleCount();
      if (Object.op_Inequality((Object) this.MatchingButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MatchingButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnMatchingButtonClick)));
      }
      if (Object.op_Inequality((Object) this.MatchingCloseButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MatchingCloseButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnMatchingCloseButtonClick)));
      }
      if (Object.op_Inequality((Object) this.DeckNextButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DeckNextButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnDeckNextButtonClick)));
      }
      if (Object.op_Inequality((Object) this.DeckPrevButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DeckPrevButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnDeckPrevButtonClick)));
      }
      if (!Object.op_Inequality((Object) this.BattleBackButton, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.BattleBackButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnBattleBackButtonClick)));
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.RefreshEnemyList();
          break;
        case 110:
          this.RefreshParty();
          break;
      }
    }

    private void RefreshParty()
    {
      this.RefreshAttackParty();
      this.RefreshDefenseParty();
    }

    private void RefreshAttackParty()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyData partyOfType = player.FindPartyOfType(PlayerPartyTypes.Arena);
      for (int index = 0; index < this.PartyUnitSlots.Length; ++index)
      {
        long unitUniqueId = partyOfType.GetUnitUniqueID(index);
        UnitData unitData1 = player.FindUnitDataByUniqueID(unitUniqueId);
        if (unitData1 != null && unitData1.GetJobFor(PlayerPartyTypes.Arena) != unitData1.CurrentJob)
        {
          UnitData unitData2 = new UnitData();
          unitData2.TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData;
          unitData2.Setup(unitData1);
          unitData2.SetJob(PlayerPartyTypes.Arena);
          unitData1 = unitData2;
        }
        if (index == 0)
        {
          DataSource.Bind<UnitData>(this.PartyUnitLeader, unitData1);
          DataSource.Bind<UnitData>(this.PartyUnitLeaderVS, unitData1);
          GameParameter.UpdateAll(this.PartyUnitLeader);
          GameParameter.UpdateAll(this.PartyUnitLeaderVS);
        }
        DataSource.Bind<UnitData>(this.PartyUnitSlots[index], unitData1);
        GameParameter.UpdateAll(this.PartyUnitSlots[index]);
      }
      if (Object.op_Inequality((Object) this.PartyInfo, (Object) null))
      {
        DataSource.Bind<PartyData>(this.PartyInfo, partyOfType);
        GameParameter.UpdateAll(this.PartyInfo);
      }
      if (!Object.op_Inequality((Object) this.VsPartyInfo, (Object) null))
        return;
      DataSource.Bind<PartyData>(this.VsPartyInfo, partyOfType);
      GameParameter.UpdateAll(this.VsPartyInfo);
    }

    private void RefreshDefenseParty()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyData partyOfType = player.FindPartyOfType(PlayerPartyTypes.ArenaDef);
      for (int index = 0; index < this.PartyUnitSlots.Length; ++index)
      {
        long unitUniqueId = partyOfType.GetUnitUniqueID(index);
        UnitData unitData1 = player.FindUnitDataByUniqueID(unitUniqueId);
        if (unitData1 != null && unitData1.GetJobFor(PlayerPartyTypes.ArenaDef) != unitData1.CurrentJob)
        {
          UnitData unitData2 = new UnitData();
          unitData2.TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData;
          unitData2.Setup(unitData1);
          unitData2.SetJob(PlayerPartyTypes.ArenaDef);
          unitData1 = unitData2;
        }
        if (index == 0)
        {
          DataSource.Bind<UnitData>(this.DefenseUnitLeader, unitData1);
          GameParameter.UpdateAll(this.DefenseUnitLeader);
        }
        DataSource.Bind<UnitData>(this.DefenseUnitSlots[index], unitData1);
        GameParameter.UpdateAll(this.DefenseUnitSlots[index]);
      }
      if (!Object.op_Inequality((Object) this.DefensePartyInfo, (Object) null))
        return;
      DataSource.Bind<PartyData>(this.DefensePartyInfo, partyOfType);
      GameParameter.UpdateAll(this.DefensePartyInfo);
    }

    private void RefreshEnemyList()
    {
      if (Object.op_Equality((Object) this.EnemyPlayerList, (Object) null) || Object.op_Equality((Object) this.EnemyPlayerItem, (Object) null))
        return;
      this.EnemyPlayerList.ClearItems();
      ArenaPlayer[] arenaPlayers = MonoSingleton<GameManager>.Instance.ArenaPlayers;
      Transform transform = ((Component) this.EnemyPlayerList).get_transform();
      for (int index = 0; index < arenaPlayers.Length; ++index)
      {
        ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.EnemyPlayerItem);
        DataSource.Bind<ArenaPlayer>(((Component) listItemEvents).get_gameObject(), arenaPlayers[index]);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnEnemySelect);
        listItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnEnemyDetailSelect);
        this.EnemyPlayerList.AddItem(listItemEvents);
        ((Component) listItemEvents).get_transform().SetParent(transform, false);
        ((Component) listItemEvents).get_gameObject().SetActive(true);
        AssetManager.PrepareAssets(AssetPath.UnitSkinImage(arenaPlayers[index].Unit[0].UnitParam, arenaPlayers[index].Unit[0].GetSelectedSkin(-1), arenaPlayers[index].Unit[0].CurrentJobId));
      }
      if (AssetDownloader.isDone)
        return;
      AssetDownloader.StartDownload(false, true, ThreadPriority.Normal);
    }

    private void OnEnemySelect(GameObject go)
    {
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(go, (ArenaPlayer) null);
      if (dataOfClass == null || !AssetDownloader.isDone)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.ChallengeArenaNum <= 0)
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARENA_DAYLIMIT"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (player.GetNextChallengeArenaCoolDownSec() > 0L)
      {
        this.OnCooldownButtonClick();
      }
      else
      {
        GlobalVars.SelectedArenaPlayer.Set(dataOfClass);
        if (Object.op_Inequality((Object) this.VsEnemyPartyInfo, (Object) null))
        {
          DataSource.Bind<ArenaPlayer>(this.VsEnemyPartyInfo, dataOfClass);
          GameParameter.UpdateAll(this.VsEnemyPartyInfo);
        }
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        this.BattlePreWindow.SetActive(true);
      }
    }

    private void OnResetChallengeTickets(GameObject go)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.Player.Coin < instance.Player.GetChallengeArenaCost())
        UIUtility.ConfirmBox(LocalizedText.Get("sys.OUT_OF_COIN_CONFIRM_BUY_COIN"), new UIUtility.DialogResultEvent(this.OpenCoinShop), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 152);
    }

    private void OpenCoinShop(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 150);
    }

    private void OnEnemyDetailSelect(GameObject go)
    {
      if (Object.op_Equality((Object) this.EnemyPlayerDetail, (Object) null))
        return;
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(go, (ArenaPlayer) null);
      if (dataOfClass == null)
        return;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.EnemyPlayerDetail);
      DataSource.Bind<ArenaPlayer>(gameObject, dataOfClass);
      ((ArenaPlayerInfo) gameObject.GetComponent<ArenaPlayerInfo>()).UpdateValue();
    }

    private void OnCooldownButtonClick()
    {
      if (MonoSingleton<GameManager>.Instance.Player.ChallengeArenaCoolDownSec <= 0L)
        return;
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ARENA_WAIT_COOLDOWN"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
    }

    private void OnResetCooldown(GameObject go)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.Player.Coin < (int) instance.MasterParam.FixParam.ArenaResetCooldownCost)
        UIUtility.ConfirmBox(LocalizedText.Get("sys.OUT_OF_COIN_CONFIRM_BUY_COIN"), new UIUtility.DialogResultEvent(this.OpenCoinShop), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 151);
    }

    private void Update()
    {
      this.RefreshCooldowns();
      if (!string.IsNullOrEmpty(this.LastBattleAtText.get_text()))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (!(player.ArenaLastAt > GameUtility.UnixtimeToLocalTime(0L)))
        return;
      this.LastBattleAtText.set_text(player.ArenaLastAt.ToString(GameUtility.Localized_TimePattern_Short));
    }

    private void RefreshCooldowns()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      player.UpdateChallengeArenaTimer();
      if (Object.op_Equality((Object) this.CooldownTimer, (Object) null))
        return;
      this.CooldownTimer.SetActive(player.GetNextChallengeArenaCoolDownSec() > 0L && player.ChallengeArenaNum > 0);
    }

    private void ChangeDrawDeck(bool attack)
    {
      this.AttackDeckWindow.SetActive(attack);
      this.AttackDeckWindowIcon.SetActive(attack);
      ((Component) this.DeckNextButton).get_gameObject().SetActive(attack);
      this.DefenseDeckWindow.SetActive(!attack);
      this.DefenseDeckWindowIcon.SetActive(!attack);
      ((Component) this.DeckPrevButton).get_gameObject().SetActive(!attack);
    }

    private void ChangeDrawInformation(bool playerinfo)
    {
      this.PlayerStatusWindow.SetActive(playerinfo);
      this.EnemyListWindow.SetActive(!playerinfo);
    }

    private void RefreshBattleCount()
    {
      if (Object.op_Equality((Object) this.BpHolder, (Object) null))
        return;
      int challengeArenaMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaMax;
      int num = MonoSingleton<GameManager>.Instance.Player.ChallengeArenaNum;
      if (num >= challengeArenaMax)
        num = challengeArenaMax;
      for (int index = 0; index < challengeArenaMax; ++index)
        ((Component) this.BpHolder.get_transform().FindChild("bp" + (index + 1).ToString())).get_gameObject().SetActive(index + 1 <= num);
    }

    private void RefreshBattleCountOnDayChange()
    {
      if (Object.op_Equality((Object) this.BpHolder, (Object) null))
        return;
      int challengeArenaMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaMax;
      int num = challengeArenaMax;
      for (int index = 0; index < challengeArenaMax; ++index)
        ((Component) this.BpHolder.get_transform().FindChild("bp" + (index + 1).ToString())).get_gameObject().SetActive(index + 1 <= num);
    }

    private void OnEnable()
    {
      MonoSingleton<GameManager>.Instance.OnDayChange += new GameManager.DayChangeEvent(this.RefreshBattleCountOnDayChange);
    }

    private void OnDisable()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnDayChange -= new GameManager.DayChangeEvent(this.RefreshBattleCountOnDayChange);
    }

    public void OnMatchingButtonClick()
    {
      this.ChangeDrawInformation(false);
    }

    public void OnMatchingCloseButtonClick()
    {
      this.ChangeDrawInformation(true);
    }

    public void OnDeckNextButtonClick()
    {
      this.ChangeDrawDeck(false);
    }

    public void OnDeckPrevButtonClick()
    {
      this.ChangeDrawDeck(true);
    }

    public void OnBattleBackButtonClick()
    {
      this.BattlePreWindow.SetActive(false);
    }

    public void OnHellpButtonClick(GameObject go)
    {
      this.BattlePreWindow.SetActive(false);
    }

    public void OnHistoryDisp()
    {
      if (!Object.op_Inequality((Object) this.HistoryObject, (Object) null))
        return;
      Object.Instantiate<GameObject>((M0) this.HistoryObject);
    }
  }
}
