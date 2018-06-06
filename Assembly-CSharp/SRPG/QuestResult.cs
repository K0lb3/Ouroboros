// Decompiled with JetBrains decompiler
// Type: SRPG.QuestResult
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(40, "ランクアップ演出表示", FlowNode.PinTypes.Output, 40)]
  [FlowNode.Pin(31, "演出終了", FlowNode.PinTypes.Output, 31)]
  [FlowNode.Pin(10, "演出開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(41, "ランクアップ演出終了", FlowNode.PinTypes.Input, 41)]
  public class QuestResult : MonoBehaviour, IFlowInterface
  {
    [Description("確認用に使用するユニットのID。ユニットID@ジョブIDで指定する。")]
    public string[] DebugUnitIDs;
    public bool[] DebugObjectiveFlags;
    public string DebugMasterAbilityID;
    [Description("ユニットアイコンを梱包する親ゲームオブジェクト")]
    public GameObject UnitList;
    [Description("ユニットアイコンのゲームオブジェクト")]
    public GameObject UnitListItem;
    [Description("ユニット獲得経験値のゲームオブジェクト")]
    public GameObject UnitExpText;
    [Description("入手アイテムのリストになる親ゲームオブジェクト")]
    public GameObject TreasureList;
    [Description("入手アイテムのゲームオブジェクト")]
    public GameObject TreasureListItem;
    [Description("クリア条件の星")]
    public GameObject[] ObjectiveStars;
    [Description("クリア条件の星を白星に切り替えるトリガーの名前")]
    public string Star_TurnOnTrigger;
    [Description("クリア条件の星が白星にならなかった場合のトリガーの名前")]
    public string Star_KeepOffTrigger;
    [Description("クリア条件の星が白星に既になってる場合のトリガーの名前")]
    public string Star_ClearTrigger;
    [Description("クリア条件の星にトリガーを送る間隔 (秒数)")]
    public float Star_TriggerInterval;
    [Description("クリア条件の星で黒星を無視する")]
    public bool Star_SkipDarkStar;
    [Description("入手アイテムを可視状態に切り替えるトリガー")]
    public string Treasure_TurnOnTrigger;
    [Description("入手アイテムを可視状態に切り替える間隔 (秒数)")]
    public float Treasure_TriggerInterval;
    public GameObject Prefab_NewItemBadge;
    public GameObject Prefab_MasterAbilityPopup;
    public GameObject Prefab_UnitDataUnlockPopup;
    public Text TextConsumeAp;
    public Color TextConsumeApColor;
    [Description("ユニットのレベルアップ時に使用するトリガー。ユニットのゲームオブジェクトにアタッチされたAnimatorへ送られます。")]
    public string Unit_LevelUpTrigger;
    [Description("一秒あたりの経験値の増加量")]
    public float ExpGainRate;
    [Description("経験値増加アニメーションの最長時間。経験値がExpGainRateの速度で増加する時、これで設定した時間を超える時に加算速度を上げる。")]
    public float ExpGainTimeMax;
    protected List<GameObject> mUnitListItems;
    private List<GameObject> mTreasureListItems;
    public string PreStarAnimationTrigger;
    public string PostStarAnimationTrigger;
    public float PreStarAnimationDelay;
    public float PostStarAnimationDelay;
    public string PreExpAnimationTrigger;
    public string PostExpAnimationTrigger;
    public float PreExpAnimationDelay;
    public float PostExpAnimationDelay;
    public string PreItemAnimationTrigger;
    public string PostItemAnimationTrigger;
    public float PreItemAnimationDelay;
    public float PostItemAnimationDelay;
    protected QuestParam mCurrentQuest;
    private GameObject mMasterAbilityPopup;
    private QuestResultData mResultData;
    protected string mQuestName;
    public GameObject RetryButton;
    public Button StarKakuninButton;
    protected List<UnitData> mUnits;
    private bool mExpAnimationEnd;
    private bool mContinueStarAnimation;
    public bool UseUnitGetEffect;
    public bool NewEffectUse;
    public int[] AcquiredUnitExp;
    [Description("アリーナ：勝ち表示するゲームオブジェクト")]
    public GameObject GoArenaResultWin;
    [Description("アリーナ：負けを表示するゲームオブジェクト")]
    public GameObject GoArenaResultLose;
    public Animator MainAnimator;

    public QuestResult()
    {
      base.\u002Ector();
    }

    private void OnStarKakuninButtonClick()
    {
      this.mContinueStarAnimation = true;
    }

    private void OnDestroy()
    {
      GameUtility.DestroyGameObject(this.mMasterAbilityPopup);
      this.mMasterAbilityPopup = (GameObject) null;
    }

    private void Start()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (Object.op_Inequality((Object) this.UnitListItem, (Object) null))
        this.UnitListItem.SetActive(false);
      if (Object.op_Inequality((Object) this.TreasureListItem, (Object) null))
        this.TreasureListItem.SetActive(false);
      if (Object.op_Inequality((Object) this.Prefab_NewItemBadge, (Object) null) && this.Prefab_NewItemBadge.get_gameObject().get_activeInHierarchy())
        this.Prefab_NewItemBadge.SetActive(false);
      SceneBattle instance = SceneBattle.Instance;
      GameUtility.DestroyGameObjects(this.mUnitListItems);
      GameUtility.DestroyGameObjects(this.mTreasureListItems);
      if (Object.op_Inequality((Object) instance, (Object) null) && instance.ResultData != null)
      {
        this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(instance.Battle.QuestID);
        DataSource.Bind<QuestParam>(((Component) this).get_gameObject(), this.mCurrentQuest);
        if (Object.op_Inequality((Object) this.RetryButton, (Object) null))
        {
          this.RetryButton.SetActive((long) new TimeSpan(TimeManager.ServerTime.Ticks).Days <= (long) new TimeSpan(player.LoginDate.Ticks).Days && this.mCurrentQuest.type != QuestTypes.Tutorial && !this.mCurrentQuest.IsCharacterQuest());
          if (this.mCurrentQuest.GetChallangeCount() >= this.mCurrentQuest.GetChallangeLimit() && this.mCurrentQuest.GetChallangeLimit() > 0)
            ((Selectable) this.RetryButton.GetComponent<Button>()).set_interactable(false);
        }
        this.mResultData = instance.ResultData;
        this.mQuestName = this.mCurrentQuest.iname;
        if (instance.IsPlayingArenaQuest)
        {
          this.mResultData.Record.playerexp = (OInt) GlobalVars.ResultArenaBattleResponse.got_pexp;
          this.mResultData.Record.gold = (OInt) GlobalVars.ResultArenaBattleResponse.got_gold;
          this.mResultData.Record.unitexp = (OInt) GlobalVars.ResultArenaBattleResponse.got_uexp;
          if (Object.op_Implicit((Object) this.GoArenaResultWin))
            this.GoArenaResultWin.SetActive(this.mResultData.Record.result == BattleCore.QuestResult.Win);
          if (Object.op_Implicit((Object) this.GoArenaResultLose))
            this.GoArenaResultLose.SetActive(this.mResultData.Record.result != BattleCore.QuestResult.Win);
        }
        for (int index = 0; index < instance.Battle.Units.Count; ++index)
        {
          Unit unit = instance.Battle.Units[index];
          if ((!instance.Battle.IsMultiPlay || unit.OwnerPlayerIndex == instance.Battle.MyPlayerIndex) && player.FindUnitDataByUniqueID(unit.UnitData.UniqueID) != null)
          {
            UnitData unitData = new UnitData();
            unitData.Setup(unit.UnitData);
            this.mUnits.Add(unitData);
          }
        }
        if (!string.IsNullOrEmpty(this.Star_ClearTrigger))
        {
          for (int index = 0; index < this.ObjectiveStars.Length; ++index)
          {
            if (((int) this.mCurrentQuest.clear_missions & 1 << index) != 0)
              GameUtility.SetAnimatorTrigger(this.ObjectiveStars[index], this.Star_ClearTrigger);
          }
        }
        if (instance.IsArenaRankupInfo())
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 40);
      }
      DataSource.Bind<BattleCore.Record>(((Component) this).get_gameObject(), this.mResultData.Record);
      if (this.mResultData != null)
      {
        if (Object.op_Inequality((Object) this.TreasureListItem, (Object) null))
        {
          Transform parent = !Object.op_Inequality((Object) this.TreasureList, (Object) null) ? this.TreasureListItem.get_transform().get_parent() : this.TreasureList.get_transform();
          List<ItemData> items = new List<ItemData>();
          for (int index1 = 0; index1 < this.mResultData.Record.items.Count; ++index1)
          {
            bool flag = false;
            for (int index2 = 0; index2 < items.Count; ++index2)
            {
              if (items[index2].Param == this.mResultData.Record.items[index1])
              {
                items[index2].Gain(1);
                flag = true;
                break;
              }
            }
            if (!flag)
            {
              ItemData itemDataByItemParam = player.FindItemDataByItemParam(this.mResultData.Record.items[index1]);
              ItemData itemData = new ItemData();
              itemData.Setup(0L, this.mResultData.Record.items[index1].iname, 1);
              if (this.mResultData.Record.items[index1].type != EItemType.Unit)
              {
                itemData.IsNew = !player.ItemEntryExists(this.mResultData.Record.items[index1].iname) || (itemDataByItemParam == null || itemDataByItemParam.IsNew);
              }
              else
              {
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: reference to a compiler-generated method
                itemData.IsNew = this.mResultData.GetUnits.Params.FindIndex(new Predicate<UnitGetParam.Set>(new QuestResult.\u003CStart\u003Ec__AnonStorey268()
                {
                  iid = this.mResultData.Record.items[index1].iname
                }.\u003C\u003Em__2CB)) != -1;
              }
              items.Add(itemData);
            }
          }
          this.CreateItemObject(items, parent);
        }
        this.ApplyQuestCampaignParams(instance.Battle.QuestCampaignIds);
        if (Object.op_Inequality((Object) this.UnitListItem, (Object) null))
          this.AddExpPlayer();
        GlobalVars.PlayerExpOld.Set(this.mResultData.StartExp);
        GlobalVars.PlayerExpNew.Set(this.mResultData.StartExp + (int) this.mResultData.Record.playerexp);
        GlobalVars.PlayerLevelChanged.Set(player.Lv != PlayerData.CalcLevelFromExp(this.mResultData.StartExp));
        if ((int) this.mResultData.Record.gold > 0)
          AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.Zeni, AnalyticsManager.CurrencySubType.FREE, (long) (int) this.mResultData.Record.gold, "Quests", (Dictionary<string, object>) null);
      }
      if (!Object.op_Inequality((Object) this.StarKakuninButton, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.StarKakuninButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnStarKakuninButtonClick)));
      this.mContinueStarAnimation = false;
    }

    public virtual void CreateItemObject(List<ItemData> items, Transform parent)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        GameObject root = (GameObject) Object.Instantiate<GameObject>((M0) this.TreasureListItem);
        root.get_transform().SetParent(parent, false);
        this.mTreasureListItems.Add(root);
        DataSource.Bind<ItemData>(root, items[index]);
        root.SetActive(true);
        GameParameter.UpdateAll(root);
        if (Object.op_Inequality((Object) this.Prefab_NewItemBadge, (Object) null) && items[index].IsNew)
        {
          RectTransform transform = ((GameObject) Object.Instantiate<GameObject>((M0) this.Prefab_NewItemBadge)).get_transform() as RectTransform;
          ((Component) transform).get_gameObject().SetActive(true);
          transform.set_anchoredPosition(Vector2.get_zero());
          ((Transform) transform).SetParent(root.get_transform(), false);
        }
        ItemData itemData = items[index];
        if (itemData.ItemType == EItemType.Ticket)
          AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.SummonTicket, AnalyticsManager.CurrencySubType.FREE, (long) itemData.Num, "Quests", new Dictionary<string, object>()
          {
            {
              "ticket_id",
              (object) itemData.ItemID
            }
          });
        else
          AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.Item, AnalyticsManager.CurrencySubType.FREE, (long) itemData.Num, "Quests", new Dictionary<string, object>()
          {
            {
              "item_id",
              (object) itemData.ItemID
            }
          });
      }
    }

    public virtual void AddExpPlayer()
    {
      if (Object.op_Inequality((Object) this.UnitExpText, (Object) null))
        this.UnitExpText.AddComponent<QuestResult.CampaignPartyExp>();
      Transform transform = !Object.op_Inequality((Object) this.UnitList, (Object) null) ? this.UnitListItem.get_transform().get_parent() : this.UnitList.get_transform();
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.UnitListItem);
        gameObject.get_transform().SetParent(transform, false);
        QuestResult.CampaignPartyExp componentInChildren = (QuestResult.CampaignPartyExp) gameObject.GetComponentInChildren<QuestResult.CampaignPartyExp>();
        if (Object.op_Inequality((Object) componentInChildren, (Object) null))
          componentInChildren.Exp = this.AcquiredUnitExp[index];
        this.mUnitListItems.Add(gameObject);
        DataSource.Bind<UnitData>(gameObject, this.mUnits[index]);
        gameObject.SetActive(true);
      }
    }

    public virtual void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.StartCoroutine(this.PlayAnimationAsync());
    }

    public void TriggerAnimation(string trigger)
    {
      if (string.IsNullOrEmpty(trigger) || !Object.op_Inequality((Object) this.MainAnimator, (Object) null))
        return;
      this.MainAnimator.SetTrigger(trigger);
    }

    [DebuggerHidden]
    public virtual IEnumerator PlayAnimationAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CPlayAnimationAsync\u003Ec__IteratorC9() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    public virtual IEnumerator AddExp()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CAddExp\u003Ec__IteratorCA() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    protected IEnumerator RecvExpAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CRecvExpAnimation\u003Ec__IteratorCB() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    public virtual IEnumerator StartTreasureAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CStartTreasureAnimation\u003Ec__IteratorCC() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    protected virtual IEnumerator TreasureAnimation(List<GameObject> ListItems)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CTreasureAnimation\u003Ec__IteratorCD() { ListItems = ListItems, \u003C\u0024\u003EListItems = ListItems, \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator ObjectiveAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CObjectiveAnimation\u003Ec__IteratorCE() { \u003C\u003Ef__this = this };
    }

    private void ApplyQuestCampaignParams(string[] campaignIds)
    {
      this.AcquiredUnitExp = new int[this.mUnits.Count];
      if (campaignIds != null)
      {
        QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCampaigns(campaignIds);
        List<UnitData> mUnits = this.mUnits;
        float[] numArray = new float[mUnits.Count];
        float num1 = 1f;
        for (int index = 0; index < numArray.Length; ++index)
          numArray[index] = 1f;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        QuestResult.\u003CApplyQuestCampaignParams\u003Ec__AnonStorey269 paramsCAnonStorey269 = new QuestResult.\u003CApplyQuestCampaignParams\u003Ec__AnonStorey269();
        foreach (QuestCampaignData questCampaignData in questCampaigns)
        {
          // ISSUE: reference to a compiler-generated field
          paramsCAnonStorey269.data = questCampaignData;
          // ISSUE: reference to a compiler-generated field
          if (paramsCAnonStorey269.data.type == QuestCampaignValueTypes.ExpUnit)
          {
            // ISSUE: reference to a compiler-generated field
            if (string.IsNullOrEmpty(paramsCAnonStorey269.data.unit))
            {
              // ISSUE: reference to a compiler-generated field
              num1 = paramsCAnonStorey269.data.GetRate();
            }
            else
            {
              // ISSUE: reference to a compiler-generated method
              int index = mUnits.FindIndex(new Predicate<UnitData>(paramsCAnonStorey269.\u003C\u003Em__2CC));
              if (index >= 0)
              {
                // ISSUE: reference to a compiler-generated field
                numArray[index] = paramsCAnonStorey269.data.GetRate();
              }
            }
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (paramsCAnonStorey269.data.type == QuestCampaignValueTypes.ExpPlayer)
            {
              // ISSUE: reference to a compiler-generated field
              this.mResultData.Record.playerexp = (OInt) Mathf.RoundToInt((float) (int) this.mResultData.Record.playerexp * paramsCAnonStorey269.data.GetRate());
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (paramsCAnonStorey269.data.type == QuestCampaignValueTypes.Ap && Object.op_Inequality((Object) this.TextConsumeAp, (Object) null))
                ((Graphic) this.TextConsumeAp).set_color(this.TextConsumeApColor);
            }
          }
        }
        int unitexp = (int) this.mResultData.Record.unitexp;
        for (int index = 0; index < numArray.Length; ++index)
        {
          float num2 = 1f;
          if ((double) num1 != 1.0 && (double) numArray[index] != 1.0)
            num2 = num1 + numArray[index];
          else if ((double) num1 != 1.0)
            num2 = num1;
          else if ((double) numArray[index] != 1.0)
            num2 = numArray[index];
          this.AcquiredUnitExp[index] = Mathf.RoundToInt((float) unitexp * num2);
        }
      }
      else
      {
        for (int index = 0; index < this.AcquiredUnitExp.Length; ++index)
          this.AcquiredUnitExp[index] = (int) this.mResultData.Record.unitexp;
      }
    }

    private class CampaignPartyExp : MonoBehaviour
    {
      public int Exp;

      public CampaignPartyExp()
      {
        base.\u002Ector();
      }

      private void Start()
      {
        Text component = (Text) ((Component) this).get_gameObject().GetComponent<Text>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.set_text(this.Exp.ToString());
      }
    }
  }
}
