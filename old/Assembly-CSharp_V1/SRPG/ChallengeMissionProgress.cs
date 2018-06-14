// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionProgress
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1004, "ゴールド購入画面へ移動", FlowNode.PinTypes.Output, 1002)]
  [FlowNode.Pin(1005, "マルチプレイへ移動", FlowNode.PinTypes.Output, 1005)]
  [FlowNode.Pin(1002, "アビリティ強化画面へ移動", FlowNode.PinTypes.Output, 1003)]
  [FlowNode.Pin(0, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "パネル表示", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "ヘルプ表示", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(100, "キャンセル", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1000, "ガチャへ移動", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "クエスト選択へ移動", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1026, "対戦へ移動", FlowNode.PinTypes.Output, 1026)]
  [FlowNode.Pin(1025, "塔へ移動", FlowNode.PinTypes.Output, 1025)]
  [FlowNode.Pin(1024, "武具進化画面へ移動", FlowNode.PinTypes.Output, 1024)]
  [FlowNode.Pin(1023, "武具強化画面へ移動", FlowNode.PinTypes.Output, 1023)]
  [FlowNode.Pin(1022, "武具錬成画面へ移動", FlowNode.PinTypes.Output, 1022)]
  [FlowNode.Pin(1003, "装備強化画面へ移動", FlowNode.PinTypes.Output, 1004)]
  [FlowNode.Pin(1021, "武具の店へ移動", FlowNode.PinTypes.Output, 1021)]
  [FlowNode.Pin(1020, "魂の交換所へ移動", FlowNode.PinTypes.Output, 1020)]
  [FlowNode.Pin(1019, "マルチ交換所へ移動", FlowNode.PinTypes.Output, 1019)]
  [FlowNode.Pin(1018, "闘技場交換所へ移動", FlowNode.PinTypes.Output, 1018)]
  [FlowNode.Pin(1017, "ツアーの店へ移動", FlowNode.PinTypes.Output, 1017)]
  [FlowNode.Pin(1016, "ソウルショップへ移動", FlowNode.PinTypes.Output, 1016)]
  [FlowNode.Pin(1015, "ルイザの店へ移動", FlowNode.PinTypes.Output, 1015)]
  [FlowNode.Pin(1014, "マリアの店へ移動", FlowNode.PinTypes.Output, 1014)]
  [FlowNode.Pin(1013, "アンナの店へ移動", FlowNode.PinTypes.Output, 1013)]
  [FlowNode.Pin(1012, "ユニット選択へ移動", FlowNode.PinTypes.Output, 1012)]
  [FlowNode.Pin(1011, "ユニットジョブ画面へ移動", FlowNode.PinTypes.Output, 1011)]
  [FlowNode.Pin(1010, "ユニットアビリティセット画面へ移動", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1009, "ユニット強化画面へ移動", FlowNode.PinTypes.Output, 1009)]
  [FlowNode.Pin(1008, "アリーナへ移動", FlowNode.PinTypes.Output, 1008)]
  [FlowNode.Pin(1007, "イベントクエスト選択へ移動", FlowNode.PinTypes.Output, 1007)]
  [FlowNode.Pin(1006, "エリートクエスト選択へ移動", FlowNode.PinTypes.Output, 1006)]
  public class ChallengeMissionProgress : MonoBehaviour, IFlowInterface
  {
    public const int PIN_OPEN = 0;
    public const int PIN_CLOSE = 1;
    public const int PIN_REFRESH = 2;
    public const int PIN_SHOW_PANEL = 10;
    public const int PIN_SHOW_HELP = 11;
    public const int PIN_CANCEL = 100;
    public const int PIN_GOTO_GACHA = 1000;
    public const int PIN_GOTO_QUEST = 1001;
    public const int PIN_GOTO_ABILITY = 1002;
    public const int PIN_GOTO_SOUBI = 1003;
    public const int PIN_GOTO_BUYGOLD = 1004;
    public const int PIN_GOTO_MULTI = 1005;
    public const int PIN_GOTO_ELITE = 1006;
    public const int PIN_GOTO_EVENT = 1007;
    public const int PIN_GOTO_ARENA = 1008;
    public const int PIN_GOTO_UNIT_STR = 1009;
    public const int PIN_GOTO_ABILITYSET = 1010;
    public const int PIN_GOTO_UNIT_JOB = 1011;
    public const int PIN_GOTO_UNIT = 1012;
    public const int PIN_GOTO_SHOP_NORMAL = 1013;
    public const int PIN_GOTO_SHOP_TABI = 1014;
    public const int PIN_GOTO_SHOP_KIMAGRE = 1015;
    public const int PIN_GOTO_SHOP_MONOZUKI = 1016;
    public const int PIN_GOTO_SHOP_TOUR = 1017;
    public const int PIN_GOTO_SHOP_ARENA = 1018;
    public const int PIN_GOTO_SHOP_MULTI = 1019;
    public const int PIN_GOTO_SHOP_KAKERA = 1020;
    public const int PIN_GOTO_SHOP_ARTIFACT = 1021;
    public const int PIN_GOTO_ATF_TRANS = 1022;
    public const int PIN_GOTO_ATF_STRTH = 1023;
    public const int PIN_GOTO_ATF_EVOLT = 1024;
    public const int PIN_GOTO_TOWER = 1025;
    public const int PIN_GOTO_VERSUS = 1026;
    public Button ButtonHelp;
    public Button ButtonDetail;
    public Button ButtonTry;
    public Button ButtonReward;
    public RawImage ImageItem;
    public RawImage ImageExp;
    public RawImage ImageGold;
    public RawImage ImageAp;
    public Text TextReward;
    private readonly string EVENT_CHALLENGE_ICON_SHOW;
    private readonly string EVENT_CHALLENGE_ICON_HIDE;
    private bool mShowingOverlay;

    public ChallengeMissionProgress()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh(false);
          break;
        case 1:
          if (!((Component) this).get_gameObject().get_activeSelf())
            break;
          ((Component) this).get_gameObject().SetActive(false);
          break;
        case 2:
          this.Refresh(true);
          break;
      }
    }

    private void OnEnable()
    {
      GlobalEvent.Invoke(this.EVENT_CHALLENGE_ICON_HIDE, (object) this);
    }

    private void OnDisable()
    {
      GlobalEvent.Invoke(this.EVENT_CHALLENGE_ICON_SHOW, (object) this);
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ButtonTry, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonTry.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickTry)));
      }
      if (Object.op_Inequality((Object) this.ButtonDetail, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonDetail.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickDetail)));
      }
      if (Object.op_Inequality((Object) this.ButtonHelp, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonHelp.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickHelp)));
      }
      if (!Object.op_Inequality((Object) this.ButtonReward, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ButtonReward.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickDetail)));
    }

    private void OnDestroy()
    {
      if (Object.op_Inequality((Object) this.ButtonReward, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonReward.get_onClick()).RemoveListener(new UnityAction((object) this, __methodptr(OnClickDetail)));
      }
      if (Object.op_Inequality((Object) this.ButtonHelp, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonHelp.get_onClick()).RemoveListener(new UnityAction((object) this, __methodptr(OnClickHelp)));
      }
      if (Object.op_Inequality((Object) this.ButtonDetail, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonDetail.get_onClick()).RemoveListener(new UnityAction((object) this, __methodptr(OnClickDetail)));
      }
      if (!Object.op_Inequality((Object) this.ButtonTry, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ButtonTry.get_onClick()).RemoveListener(new UnityAction((object) this, __methodptr(OnClickTry)));
    }

    private void Update()
    {
      if (this.mShowingOverlay || !Input.GetMouseButtonDown(0) || (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L)
        return;
      EventSystem current = EventSystem.get_current();
      List<RaycastResult> raycastResultList = new List<RaycastResult>();
      PointerEventData pointerEventData = new PointerEventData(current);
      bool flag = true;
      pointerEventData.set_position(new Vector2(285f, 376f));
      current.RaycastAll(pointerEventData, raycastResultList);
      if (raycastResultList.Count != 0)
      {
        RaycastResult raycastResult = raycastResultList[0];
        // ISSUE: explicit reference operation
        if (this.IsIncludeObject(((RaycastResult) @raycastResult).get_gameObject()))
          flag = false;
      }
      if (flag)
        return;
      raycastResultList.Clear();
      pointerEventData.set_position(Vector2.op_Implicit(Input.get_mousePosition()));
      current.RaycastAll(pointerEventData, raycastResultList);
      if (raycastResultList.Count == 0)
        return;
      RaycastResult raycastResult1 = raycastResultList[0];
      // ISSUE: explicit reference operation
      if (this.IsIncludeObject(((RaycastResult) @raycastResult1).get_gameObject()))
        return;
      ((Component) this).get_gameObject().SetActive(false);
    }

    private bool IsIncludeObject(GameObject value)
    {
      for (Transform transform = value.get_transform(); Object.op_Inequality((Object) transform, (Object) null); transform = transform.get_parent())
      {
        if (Object.op_Equality((Object) ((Component) this).get_gameObject(), (Object) ((Component) transform).get_gameObject()))
          return true;
      }
      return false;
    }

    private void Refresh(bool fromRefresh)
    {
      this.mShowingOverlay = false;
      bool flag1 = false;
      TrophyState trophyState = (TrophyState) null;
      TrophyParam currentRootTrophy = ChallengeMission.GetCurrentRootTrophy();
      if (currentRootTrophy == null)
        return;
      foreach (TrophyParam currentTrophy in ChallengeMission.GetCurrentTrophies(currentRootTrophy))
      {
        TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(currentTrophy);
        if (!trophyCounter.IsEnded)
        {
          if (!trophyCounter.IsCompleted)
            ;
          DataSource.Bind<TrophyParam>(((Component) this).get_gameObject(), currentTrophy);
          DataSource.Bind<TrophyState>(((Component) this).get_gameObject(), trophyCounter);
          trophyState = trophyCounter;
          flag1 = true;
          this.RefreshRewardIcon(currentTrophy);
          break;
        }
      }
      if (!flag1)
      {
        this.RefreshRewardIcon(currentRootTrophy);
        DataSource.Bind<TrophyParam>(((Component) this).get_gameObject(), currentRootTrophy);
        TrophyState trophyCounter = ChallengeMission.GetTrophyCounter(currentRootTrophy);
        if (trophyCounter != null)
          DataSource.Bind<TrophyState>(((Component) this).get_gameObject(), trophyCounter);
      }
      if (!((Component) this).get_gameObject().get_activeSelf())
        ((Component) this).get_gameObject().SetActive(true);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      bool flag2 = !flag1;
      if (trophyState != null)
        flag2 = trophyState.IsCompleted;
      if (!Object.op_Inequality((Object) this.ButtonTry, (Object) null) || !Object.op_Inequality((Object) this.ButtonDetail, (Object) null) || (!Object.op_Inequality((Object) this.ButtonReward, (Object) null) || !Object.op_Inequality((Object) this.ButtonHelp, (Object) null)))
        return;
      ((Component) this.ButtonTry).get_gameObject().SetActive(!flag2);
      ((Component) this.ButtonDetail).get_gameObject().SetActive(!flag2);
      ((Component) this.ButtonReward).get_gameObject().SetActive(flag2);
      ((Component) this.ButtonHelp).get_gameObject().SetActive(!flag2);
    }

    private void RefreshRewardIcon(TrophyParam trophy)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      string str = string.Empty;
      ItemParam data = (ItemParam) null;
      string format = "@{0}";
      if (trophy.Gold != 0)
      {
        flag3 = true;
        str = string.Format(format, (object) trophy.Gold);
      }
      else if (trophy.Exp != 0)
      {
        flag2 = true;
        str = string.Format(format, (object) trophy.Exp);
      }
      else if (trophy.Coin != 0)
      {
        flag1 = true;
        data = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
        str = string.Format(format, (object) trophy.Coin);
      }
      else if (trophy.Stamina != 0)
      {
        flag4 = true;
        str = string.Format(format, (object) trophy.Stamina);
      }
      else if (trophy.Items != null && trophy.Items.Length > 0)
      {
        flag1 = true;
        data = MonoSingleton<GameManager>.Instance.GetItemParam(trophy.Items[0].iname);
        if (data != null)
          str = string.Format(format, (object) trophy.Items[0].Num);
      }
      if (Object.op_Inequality((Object) this.ImageItem, (Object) null))
        ((Component) this.ImageItem).get_gameObject().SetActive(flag1);
      if (Object.op_Inequality((Object) this.ImageExp, (Object) null))
        ((Component) this.ImageExp).get_gameObject().SetActive(flag2);
      if (Object.op_Inequality((Object) this.ImageGold, (Object) null))
        ((Component) this.ImageGold).get_gameObject().SetActive(flag3);
      if (Object.op_Inequality((Object) this.ImageAp, (Object) null))
        ((Component) this.ImageAp).get_gameObject().SetActive(flag4);
      if (Object.op_Inequality((Object) this.TextReward, (Object) null))
        this.TextReward.set_text(str);
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), data);
    }

    private void OnClickDetail()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      this.mShowingOverlay = true;
    }

    private void OnClickHelp()
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).get_gameObject(), (TrophyParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.help == 0)
      {
        string str = LocalizedText.Get("sys.CHALLENGE_HELP_" + dataOfClass.Objectives[0].type.ToString().ToUpper());
        FlowNode_Variable.Set(HelpWindow.VAR_NAME_MENU_ID, str);
      }
      else
        FlowNode_Variable.Set(HelpWindow.VAR_NAME_MENU_ID, dataOfClass.help.ToString());
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      this.mShowingOverlay = true;
    }

    private void OnClickTry()
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).get_gameObject(), (TrophyParam) null);
      if (dataOfClass == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        QuestParam questParam = new QuestParam();
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        switch (dataOfClass.Objectives[0].type)
        {
          case TrophyConditionTypes.winquest:
          case TrophyConditionTypes.winquestsoldier:
          case TrophyConditionTypes.losequest:
            QuestTypes quest_type1 = QuestTypes.Story;
            if (!questParam.TransSectionGotoQuest(dataOfClass.Objectives[0].sval, out quest_type1, new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
              break;
            QuestTypes questTypes = quest_type1;
            switch (questTypes)
            {
              case QuestTypes.Event:
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
                return;
              case QuestTypes.Tower:
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 1025);
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
          case TrophyConditionTypes.killenemy:
          case TrophyConditionTypes.getitem:
          case TrophyConditionTypes.playerlv:
          case TrophyConditionTypes.vip:
          case TrophyConditionTypes.stamina:
          case TrophyConditionTypes.card:
          case TrophyConditionTypes.review:
          case TrophyConditionTypes.followtwitter:
          case TrophyConditionTypes.fggid:
          case TrophyConditionTypes.unitlevel:
          case TrophyConditionTypes.evolutionnum:
          case TrophyConditionTypes.joblevel:
          case TrophyConditionTypes.logincount:
          case TrophyConditionTypes.fblogin:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
            break;
          case TrophyConditionTypes.winelite:
          case TrophyConditionTypes.loseelite:
            if (!questParam.TransSectionGotoElite(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
              break;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
            break;
          case TrophyConditionTypes.winevent:
          case TrophyConditionTypes.loseevent:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
            break;
          case TrophyConditionTypes.gacha:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
            break;
          case TrophyConditionTypes.multiplay:
          case TrophyConditionTypes.winmulti:
          case TrophyConditionTypes.winmultimore:
          case TrophyConditionTypes.winmultiless:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1005);
            break;
          case TrophyConditionTypes.ability:
          case TrophyConditionTypes.makeabilitylevel:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1002);
            break;
          case TrophyConditionTypes.soubi:
            if (player.CheckUnlock(UnlockTargets.EnhanceEquip))
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1003);
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EnhanceEquip);
            break;
          case TrophyConditionTypes.buygold:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1004);
            break;
          case TrophyConditionTypes.arena:
          case TrophyConditionTypes.winarena:
          case TrophyConditionTypes.losearena:
            if (player.CheckUnlock(UnlockTargets.Arena))
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1008);
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Arena);
            break;
          case TrophyConditionTypes.upunitlevel:
          case TrophyConditionTypes.makeunitlevel:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1009);
            break;
          case TrophyConditionTypes.unitequip:
          case TrophyConditionTypes.upjoblevel:
          case TrophyConditionTypes.makejoblevel:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1011);
            break;
          case TrophyConditionTypes.limitbreak:
          case TrophyConditionTypes.evoltiontimes:
          case TrophyConditionTypes.changejob:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1012);
            break;
          case TrophyConditionTypes.changeability:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
            break;
          case TrophyConditionTypes.buyatshop:
            this.GotoShop(dataOfClass);
            break;
          case TrophyConditionTypes.artifacttransmute:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1022);
            break;
          case TrophyConditionTypes.artifactstrength:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1023);
            break;
          case TrophyConditionTypes.artifactevolution:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1024);
            break;
          case TrophyConditionTypes.wintower:
          case TrophyConditionTypes.losetower:
            QuestTypes quest_type2 = QuestTypes.Story;
            if (!questParam.TransSectionGotoTower(dataOfClass.Objectives[0].sval, out quest_type2))
              break;
            if (quest_type2 == QuestTypes.Event)
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
              break;
            }
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1025);
            break;
          case TrophyConditionTypes.vs:
          case TrophyConditionTypes.vswin:
            if (player.CheckUnlock(UnlockTargets.MultiVS))
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1026);
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiVS);
            break;
          default:
            DebugUtility.Log(string.Format("未知の Trophy 条件 / {0}", (object) dataOfClass.Objectives[0].type));
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
            break;
        }
      }
    }

    private void GotoShop(TrophyParam trophy)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      EShopType type = EShopType.Normal;
      if (!string.IsNullOrEmpty(trophy.Objectives[0].sval))
      {
        char[] chArray = new char[1]{ ',' };
        string[] strArray = trophy.Objectives[0].sval.Split(chArray);
        type = !string.IsNullOrEmpty(strArray[0]) ? (EShopType) MonoSingleton<GameManager>.Instance.MasterParam.GetShopType(strArray[0]) : EShopType.Normal;
      }
      if (type >= EShopType.Normal && player.CheckUnlockShopType(type))
      {
        switch (type)
        {
          case EShopType.Normal:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1013);
            break;
          case EShopType.Tabi:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1014);
            break;
          case EShopType.Kimagure:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1015);
            break;
          case EShopType.Monozuki:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1016);
            break;
          case EShopType.Tour:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1017);
            break;
          case EShopType.Arena:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1018);
            break;
          case EShopType.Multi:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1019);
            break;
          case EShopType.AwakePiece:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
            break;
          case EShopType.Artifact:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1021);
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

    private void MsgBoxJumpToQuest(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
    }
  }
}
