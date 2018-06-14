// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1029, "レビューへ移動", FlowNode.PinTypes.Output, 1029)]
  [FlowNode.Pin(1028, "イベントショップへ移動", FlowNode.PinTypes.Output, 1028)]
  [FlowNode.Pin(1027, "限定ショップへ移動", FlowNode.PinTypes.Output, 1027)]
  [FlowNode.Pin(1013, "アンナの店へ移動", FlowNode.PinTypes.Output, 1013)]
  [FlowNode.Pin(1026, "塔クエスト選択へ移動", FlowNode.PinTypes.Output, 1026)]
  [FlowNode.Pin(1025, "ユニット選択へ移動", FlowNode.PinTypes.Output, 1025)]
  [FlowNode.Pin(1021, "武具の店へ移動", FlowNode.PinTypes.Output, 1021)]
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1001, "クエスト選択へ移動", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1004, "ゴールド購入画面へ移動", FlowNode.PinTypes.Output, 1002)]
  [FlowNode.Pin(1003, "装備強化画面へ移動", FlowNode.PinTypes.Output, 1004)]
  [FlowNode.Pin(1005, "マルチプレイへ移動", FlowNode.PinTypes.Output, 1005)]
  [FlowNode.Pin(1007, "イベントクエスト選択へ移動", FlowNode.PinTypes.Output, 1007)]
  [FlowNode.Pin(1008, "アリーナへ移動", FlowNode.PinTypes.Output, 1008)]
  [FlowNode.Pin(1009, "FgGID画面へ移動", FlowNode.PinTypes.Output, 1009)]
  [FlowNode.Pin(1000, "ガチャへ移動", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(100, "報酬を受け取り", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1020, "魂の交換所へ移動", FlowNode.PinTypes.Output, 1020)]
  [FlowNode.Pin(1040, "SG: FBLogin", FlowNode.PinTypes.Output, 1040)]
  [FlowNode.Pin(1030, "対戦へ移動", FlowNode.PinTypes.Output, 1030)]
  public class TrophyList : SRPG_ListBase, IFlowInterface
  {
    public const int PIN_GOTO_GACHA = 1000;
    public const int PIN_GOTO_QUEST = 1001;
    public const int PIN_GOTO_SOUBI = 1003;
    public const int PIN_GOTO_BUYGOLD = 1004;
    public const int PIN_GOTO_MULTI = 1005;
    public const int PIN_GOTO_EVENT = 1007;
    public const int PIN_GOTO_ARENA = 1008;
    public const int PIN_GOTO_FGGID = 1009;
    public const int PIN_GOTO_SHOP_NORMAL = 1013;
    public const int PIN_GOTO_SHOP_KAKERA = 1020;
    public const int PIN_GOTO_SHOP_ARTIFACT = 1021;
    public const int PIN_GOTO_UNIT = 1025;
    public const int PIN_GOTO_TOWER = 1026;
    public const int PIN_GOTO_SHOP_LIMITE = 1027;
    public const int PIN_GOTO_SHOP_EVENT = 1028;
    public const int PIN_GOTO_REVIEW = 1029;
    public const int PIN_GOTO_VERSUS = 1030;
    public const int PIN_GOTO_FBLOGIN = 1040;
    private const int CREATE_ENDED_PLATE_COUNT = 50;
    public TrophyList.TrophyTypes TrophyType;
    public TrophyCategorys TrophyCategory;
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Completed;
    public ListItemEvents Item_Ended;
    public GameObject DetailWindow;
    public ListItemEvents Item_Review;
    public ListItemEvents Item_FollowTwitter;
    public bool RefreshOnStart;
    private bool mStarted;
    private CanvasGroup mCanvasGroup;
    private QuestTypes g_quest_type;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.Item_Normal, (Object) null) && ((Component) this.Item_Normal).get_gameObject().get_activeInHierarchy())
        ((Component) this.Item_Normal).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.Item_Review, (Object) null) && ((Component) this.Item_Review).get_gameObject().get_activeInHierarchy())
        ((Component) this.Item_Review).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.Item_FollowTwitter, (Object) null) && ((Component) this.Item_FollowTwitter).get_gameObject().get_activeInHierarchy())
        ((Component) this.Item_FollowTwitter).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.Item_Completed, (Object) null) && ((Component) this.Item_Completed).get_gameObject().get_activeInHierarchy())
        ((Component) this.Item_Completed).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.Item_Ended, (Object) null) && ((Component) this.Item_Ended).get_gameObject().get_activeInHierarchy())
        ((Component) this.Item_Ended).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.DetailWindow, (Object) null) && this.DetailWindow.get_activeInHierarchy())
        this.DetailWindow.SetActive(false);
      this.mCanvasGroup = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
      if (!Object.op_Equality((Object) this.mCanvasGroup, (Object) null))
        return;
      this.mCanvasGroup = (CanvasGroup) ((Component) this).get_gameObject().AddComponent<CanvasGroup>();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!Object.op_Inequality((Object) HomeWindow.Current, (Object) null))
        return;
      HomeWindow.Current.UnlockContents();
    }

    protected override void Start()
    {
      base.Start();
      this.mStarted = true;
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    private void OnTrophyReset()
    {
      this.Refresh();
    }

    private void OnEnable()
    {
      this.Refresh();
      MonoSingleton<GameManager>.Instance.OnDayChange += new GameManager.DayChangeEvent(this.OnTrophyReset);
    }

    private void OnDisable()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnDayChange -= new GameManager.DayChangeEvent(this.OnTrophyReset);
    }

    private void Update()
    {
      if (!Object.op_Inequality((Object) this.mCanvasGroup, (Object) null) || (double) this.mCanvasGroup.get_alpha() >= 1.0)
        return;
      this.mCanvasGroup.set_alpha(Mathf.Clamp01(this.mCanvasGroup.get_alpha() + Time.get_unscaledDeltaTime() * 3.333333f));
    }

    private void Refresh()
    {
      if (!this.mStarted)
        return;
      if (Object.op_Inequality((Object) this.mCanvasGroup, (Object) null))
        this.mCanvasGroup.set_alpha(0.0f);
      this.ClearItems();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      TrophyParam[] trophies = instance.Trophies;
      PlayerData player = instance.Player;
      if (trophies == null || trophies.Length <= 0)
        return;
      if (this.TrophyType == TrophyList.TrophyTypes.Daily)
        MonoSingleton<GameManager>.Instance.Player.DailyAllCompleteCheck();
      int[] numArray1 = new int[trophies.Length];
      for (int index = 0; index < trophies.Length; ++index)
        numArray1[index] = index;
      TrophyState[] trophyStateArray = new TrophyState[trophies.Length];
      for (int index = 0; index < trophies.Length; ++index)
        trophyStateArray[index] = !trophies[index].IsChallengeMission ? player.GetTrophyCounter(trophies[index]) : (TrophyState) null;
      int num = -1;
      if (this.TrophyType == TrophyList.TrophyTypes.All)
      {
        num = 50;
        ulong[] numArray2 = new ulong[trophies.Length];
        for (int index = 0; index < trophies.Length; ++index)
        {
          DateTime dateTime = trophyStateArray[index] == null ? DateTime.MinValue : trophyStateArray[index].RewardedAt;
          numArray2[index] = (ulong) ((long) ((ulong) dateTime.Year % 100UL) * 10000000000L + (long) ((ulong) dateTime.Month % 100UL) * 100000000L + (long) ((ulong) dateTime.Day % 100UL) * 1000000L + (long) ((ulong) dateTime.Hour % 100UL) * 10000L + (long) ((ulong) dateTime.Minute % 100UL) * 100L) + (ulong) dateTime.Second % 100UL;
        }
        Dictionary<int, ulong> dictionary = new Dictionary<int, ulong>();
        dictionary.Keys.CopyTo(numArray1, 0);
        dictionary.Values.CopyTo(numArray2, 0);
        Array.Sort<ulong, int>(numArray2, numArray1);
        Array.Reverse((Array) numArray1);
      }
      for (int index1 = 0; index1 < trophies.Length && num != 0; ++index1)
      {
        int index2 = numArray1[index1];
        TrophyState st = trophyStateArray[index2];
        if (st != null && st.IsCompleted && (trophies[index2].DispType != TrophyDispType.Award && this.MakeTrophyPlate(trophies[index2], st, true)) && 0 < num)
          --num;
      }
      for (int index1 = 0; index1 < trophies.Length && num != 0; ++index1)
      {
        int index2 = numArray1[index1];
        TrophyState st = trophyStateArray[index2];
        if (st != null && !st.IsCompleted && (trophies[index2].DispType != TrophyDispType.Award && trophies[index2].DispType != TrophyDispType.Hide) && (this.MakeTrophyPlate(trophies[index2], st, false) && 0 < num))
          --num;
      }
      GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.TROPHY_BADGE);
    }

    private bool MakeTrophyPlate(TrophyParam trophy, TrophyState st, bool is_achievement)
    {
      Transform transform = ((Component) this).get_transform();
      if (this.TrophyType == TrophyList.TrophyTypes.Daily)
      {
        if (trophy.Days != 1)
          return false;
      }
      else if (this.TrophyType == TrophyList.TrophyTypes.Normal && (trophy.Days != 0 || this.TrophyCategory != trophy.Category))
        return false;
      if (trophy.IsInvisibleVip() || trophy.IsInvisibleCard() || trophy.IsInvisibleStamina() || (trophy.RequiredTrophies != null && !TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, trophy, true) || !trophy.IsAvailablePeriod(TimeManager.ServerTime, is_achievement)))
        return false;
      ListItemEvents listItemEvents1 = !st.IsEnded ? (!Object.op_Inequality((Object) this.Item_FollowTwitter, (Object) null) || !trophy.ContainsCondition(TrophyConditionTypes.followtwitter) ? (!trophy.iname.Contains("DAILY_GLAPVIDEO") ? (!st.IsCompleted ? this.Item_Normal : this.Item_Completed) : this.Item_Normal) : this.Item_FollowTwitter) : this.Item_Ended;
      if (Object.op_Equality((Object) listItemEvents1, (Object) null) || trophy.iname.Substring(0, 7) == "REVIEW_" && Network.Host.Contains("eval.alchemist.gu3.jp"))
        return false;
      ListItemEvents listItemEvents2 = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
      DataSource.Bind<TrophyParam>(((Component) listItemEvents2).get_gameObject(), trophy);
      ((Component) listItemEvents2).get_transform().SetParent(transform, false);
      listItemEvents2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
      listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
      SRPG_Button componentInChildren = (SRPG_Button) ((Component) listItemEvents2).GetComponentInChildren<SRPG_Button>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null) && !st.IsEnded)
      {
        if (Object.op_Inequality((Object) this.Item_FollowTwitter, (Object) null) && trophy.ContainsCondition(TrophyConditionTypes.followtwitter))
          this.FollowBtnSetting(componentInChildren);
        else if (trophy.iname.Contains("DAILY_GLAPVIDEO"))
          this.GlobalVideoAdsBtnSetting(componentInChildren);
        else if (st.IsCompleted)
          this.AchievementBtnSetting(componentInChildren);
        else
          this.ChallengeBtnSetting(componentInChildren, trophy);
      }
      RewardData data = new RewardData(trophy);
      DataSource.Bind<RewardData>(((Component) listItemEvents2).get_gameObject(), data);
      this.AddItem(listItemEvents2);
      ((Component) listItemEvents2).get_gameObject().SetActive(true);
      return true;
    }

    private void OnItemSelect(GameObject go)
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(go, (TrophyParam) null);
      if (dataOfClass == null)
        return;
      TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(dataOfClass);
      if (!trophyCounter.IsEnded && trophyCounter.IsCompleted)
      {
        if (dataOfClass.IsInvisibleStamina() || !dataOfClass.IsAvailablePeriod(TimeManager.ServerTime, true))
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.TROPHY_OUTDATED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          this.Refresh();
        }
        else
        {
          GlobalVars.SelectedTrophy.Set(dataOfClass.iname);
          RewardData rewardData = new RewardData(dataOfClass);
          GlobalVars.LastReward.Set(rewardData);
          GlobalVars.UnitGetReward = new UnitGetParam(rewardData.Items.ToArray());
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
        }
      }
      else
      {
        QuestParam questParam = new QuestParam();
        switch (dataOfClass.Objectives[0].type)
        {
          case TrophyConditionTypes.winquest:
          case TrophyConditionTypes.playerlv:
          case TrophyConditionTypes.winquestsoldier:
          case TrophyConditionTypes.losequest:
            QuestTypes quest_type1 = QuestTypes.Story;
            if (!questParam.TransSectionGotoQuest(dataOfClass.Objectives[0].sval, out quest_type1, new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            {
              this.g_quest_type = quest_type1;
              break;
            }
            QuestTypes questTypes = quest_type1;
            switch (questTypes)
            {
              case QuestTypes.Event:
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
                return;
              case QuestTypes.Tower:
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 1026);
                return;
              default:
                if (questTypes == QuestTypes.Multi)
                {
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 1005);
                  return;
                }
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
                return;
            }
          case TrophyConditionTypes.winelite:
          case TrophyConditionTypes.loseelite:
            if (!questParam.TransSectionGotoElite(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            {
              this.g_quest_type = QuestTypes.Story;
              break;
            }
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
            break;
          case TrophyConditionTypes.winevent:
          case TrophyConditionTypes.loseevent:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
            break;
          case TrophyConditionTypes.gacha:
          case TrophyConditionTypes.collectunits:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
            break;
          case TrophyConditionTypes.multiplay:
          case TrophyConditionTypes.winmulti:
          case TrophyConditionTypes.winmultimore:
          case TrophyConditionTypes.winmultiless:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1005);
            break;
          case TrophyConditionTypes.ability:
          case TrophyConditionTypes.changeability:
          case TrophyConditionTypes.makeabilitylevel:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1025);
            break;
          case TrophyConditionTypes.soubi:
            this.GotoEquip();
            break;
          case TrophyConditionTypes.buygold:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1004);
            break;
          case TrophyConditionTypes.arena:
          case TrophyConditionTypes.winarena:
          case TrophyConditionTypes.losearena:
            this.GotoArena();
            break;
          case TrophyConditionTypes.review:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1029);
            break;
          case TrophyConditionTypes.fggid:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1009);
            break;
          case TrophyConditionTypes.unitlevel:
          case TrophyConditionTypes.evolutionnum:
          case TrophyConditionTypes.joblevel:
          case TrophyConditionTypes.upunitlevel:
          case TrophyConditionTypes.makeunitlevel:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1025);
            break;
          case TrophyConditionTypes.unitequip:
          case TrophyConditionTypes.upjoblevel:
          case TrophyConditionTypes.makejoblevel:
          case TrophyConditionTypes.totaljoblv11:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1025);
            break;
          case TrophyConditionTypes.limitbreak:
          case TrophyConditionTypes.evoltiontimes:
          case TrophyConditionTypes.changejob:
          case TrophyConditionTypes.totalunitlvs:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1025);
            break;
          case TrophyConditionTypes.buyatshop:
            this.GotoShop(dataOfClass);
            break;
          case TrophyConditionTypes.artifacttransmute:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1021);
            break;
          case TrophyConditionTypes.artifactstrength:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1021);
            break;
          case TrophyConditionTypes.artifactevolution:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1021);
            break;
          case TrophyConditionTypes.wintower:
          case TrophyConditionTypes.losetower:
            QuestTypes quest_type2 = QuestTypes.Tower;
            if (!questParam.TransSectionGotoTower(dataOfClass.Objectives[0].sval, out quest_type2))
              break;
            if (quest_type2 == QuestTypes.Event)
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
              break;
            }
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1026);
            break;
          case TrophyConditionTypes.vs:
          case TrophyConditionTypes.vswin:
            this.GotoVersus();
            break;
          case TrophyConditionTypes.fblogin:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1040);
            break;
        }
      }
    }

    private void OnItemDetail(GameObject go)
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(go, (TrophyParam) null);
      if (!Object.op_Inequality((Object) this.DetailWindow, (Object) null) || dataOfClass == null)
        return;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.DetailWindow);
      DataSource.Bind<TrophyParam>(gameObject, dataOfClass);
      RewardData data = new RewardData(dataOfClass);
      DataSource.Bind<RewardData>(gameObject, data);
      gameObject.SetActive(true);
    }

    private void GotoArena()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.Arena))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1008);
      else
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Arena);
    }

    private void GotoVersus()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.MultiVS))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1030);
      else
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiVS);
    }

    private void GotoEquip()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.CheckUnlock(UnlockTargets.EnhanceEquip))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1003);
      else
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EnhanceEquip);
    }

    private void GotoShop(TrophyParam param)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      EShopType type;
      if (string.IsNullOrEmpty(param.Objectives[0].sval))
      {
        type = EShopType.Normal;
      }
      else
      {
        char[] chArray = new char[1]{ ',' };
        string[] strArray = param.Objectives[0].sval.Split(chArray);
        type = !string.IsNullOrEmpty(strArray[0]) ? (EShopType) MonoSingleton<GameManager>.Instance.MasterParam.GetShopType(strArray[0]) : EShopType.Normal;
      }
      if (type >= EShopType.Normal && player.CheckUnlockShopType(type))
      {
        switch (type)
        {
          case EShopType.Normal:
          case EShopType.Tabi:
          case EShopType.Kimagure:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1013);
            break;
          case EShopType.Arena:
            this.GotoArena();
            break;
          case EShopType.Multi:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1005);
            break;
          case EShopType.AwakePiece:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
            break;
          case EShopType.Artifact:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1021);
            break;
          case EShopType.Event:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1028);
            break;
          case EShopType.Limited:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1027);
            break;
        }
      }
      if (type < EShopType.Normal)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.CheckUnlockShopType(type))
        return;
      try
      {
        UnlockTargets unlockTargets = type.ToUnlockTargets();
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, unlockTargets);
      }
      catch (Exception ex)
      {
      }
    }

    private void ChallengeBtnSetting(SRPG_Button btn, TrophyParam trophy)
    {
      bool flag = true;
      switch (trophy.Objectives[0].type)
      {
        case TrophyConditionTypes.killenemy:
        case TrophyConditionTypes.getitem:
        case TrophyConditionTypes.vip:
        case TrophyConditionTypes.stamina:
        case TrophyConditionTypes.card:
        case TrophyConditionTypes.logincount:
        case TrophyConditionTypes.childrencomp:
        case TrophyConditionTypes.dailyall:
          flag = false;
          break;
      }
      if (trophy.DispType == TrophyDispType.HideChallenge)
        flag = false;
      ((Component) btn).get_gameObject().SetActive(flag);
      if (!flag)
      {
        VerticalLayoutGroup component = (VerticalLayoutGroup) ((Component) ((Component) btn).get_transform().get_parent()).GetComponent<VerticalLayoutGroup>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        ((LayoutGroup) component).set_childAlignment((TextAnchor) 7);
      }
      else
      {
        Text componentInChildren = (Text) ((Component) ((Component) btn).get_transform()).GetComponentInChildren<Text>();
        if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
          return;
        componentInChildren.set_text(LocalizedText.Get("sys.TROPHY_BTN_GO"));
      }
    }

    private void AchievementBtnSetting(SRPG_Button btn)
    {
      Text componentInChildren = (Text) ((Component) ((Component) btn).get_transform()).GetComponentInChildren<Text>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.set_text(LocalizedText.Get("sys.TROPHY_BTN_CLEAR"));
    }

    private void GlobalVideoAdsBtnSetting(SRPG_Button btn)
    {
      Text componentInChildren = (Text) ((Component) ((Component) btn).get_transform()).GetComponentInChildren<Text>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.set_text(LocalizedText.Get("sys.TROPHY_BTN_GO"));
    }

    private void FollowBtnSetting(SRPG_Button btn)
    {
      Text componentInChildren = (Text) ((Component) ((Component) btn).get_transform()).GetComponentInChildren<Text>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      componentInChildren.set_text(LocalizedText.Get("sys.TROPHY_BTN_FOLLOWTWITTER"));
    }

    private void MsgBoxJumpToQuest(GameObject go)
    {
      QuestTypes gQuestType = this.g_quest_type;
      switch (gQuestType)
      {
        case QuestTypes.Event:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
          break;
        case QuestTypes.Tower:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1026);
          break;
        default:
          if (gQuestType == QuestTypes.Multi)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1005);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
          break;
      }
    }

    public enum TrophyTypes
    {
      Normal,
      Daily,
      All,
    }
  }
}
