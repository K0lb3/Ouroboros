// Decompiled with JetBrains decompiler
// Type: SRPG.QuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(41, "ランクアップ演出終了", FlowNode.PinTypes.Input, 41)]
  [FlowNode.Pin(100, "演出スキップ", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(40, "ランクアップ演出表示", FlowNode.PinTypes.Output, 40)]
  [FlowNode.Pin(10, "演出開始", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(31, "演出終了", FlowNode.PinTypes.Output, 31)]
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
    [Description("入手ユニットのゲームオブジェクト")]
    public GameObject TreasureListUnit;
    [Description("入手武具のゲームオブジェクト")]
    public GameObject TreasureListArtifact;
    [Description("バトルコインのゲームオブジェクト")]
    public GameObject TreasureListBattleCoin;
    [Description("入手真理念装のゲームオブジェクト")]
    public GameObject TreasureListConceptCard;
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
    private bool mUseLarge;
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
    protected QuestResultData mResultData;
    protected string mQuestName;
    public GameObject RetryButton;
    public Button StarKakuninButton;
    public SRPG_Button TeamUploadButton;
    protected List<UnitData> mUnits;
    [Description("スキップボタン")]
    public Button ResultSkipButton;
    [Description("経験値増加アニメーションスキップの倍速設定")]
    public float ResultSkipSpeedMul;
    private bool mResultSkipElement;
    private bool mExpAnimationEnd;
    protected bool mContinueStarAnimation;
    public bool UseUnitGetEffect;
    public bool NewEffectUse;
    public int[] AcquiredUnitExp;
    [Description("アリーナ：勝ち表示するゲームオブジェクト")]
    public GameObject GoArenaResultWin;
    [Description("アリーナ：負けを表示するゲームオブジェクト")]
    public GameObject GoArenaResultLose;
    public BattleResultMissionDetail MissionDetailSmall;
    public BattleResultMissionDetail MissionDetailLarge;
    private List<GameObject> mObjectiveStars;
    protected BattleResultMissionDetail mMissionDetail;
    protected List<int> mMultiTowerUnitsId;
    public GameObject[] MultiTowerPlayerObj;
    public RectTransform[] MultiTowerPlayerTransform;
    public Texture2D GoldTex;
    public Sprite GoldFrame;
    public Animator MainAnimator;

    public QuestResult()
    {
      base.\u002Ector();
    }

    protected void SetExpAnimationEnd()
    {
      this.mExpAnimationEnd = true;
    }

    public void OnStarKakuninButtonClick()
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
      GlobalVars.PartyUploadFinished = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitListItem, (UnityEngine.Object) null))
        this.UnitListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListItem, (UnityEngine.Object) null))
        this.TreasureListItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListUnit, (UnityEngine.Object) null))
        this.TreasureListUnit.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListArtifact, (UnityEngine.Object) null))
        this.TreasureListArtifact.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListConceptCard, (UnityEngine.Object) null))
        this.TreasureListConceptCard.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_NewItemBadge, (UnityEngine.Object) null) && this.Prefab_NewItemBadge.get_gameObject().get_activeInHierarchy())
        this.Prefab_NewItemBadge.SetActive(false);
      SceneBattle instance = SceneBattle.Instance;
      GameUtility.DestroyGameObjects(this.mUnitListItems);
      GameUtility.DestroyGameObjects(this.mTreasureListItems);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.ResultData != null)
      {
        this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(instance.Battle.QuestID);
        DataSource.Bind<QuestParam>(((Component) this).get_gameObject(), this.mCurrentQuest);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RetryButton, (UnityEngine.Object) null))
          this.RetryButton.SetActive((long) new TimeSpan(TimeManager.ServerTime.Ticks).Days <= (long) new TimeSpan(player.LoginDate.Ticks).Days && this.mCurrentQuest.type != QuestTypes.Tutorial && !this.mCurrentQuest.IsCharacterQuest());
        if (this.mCurrentQuest.type == QuestTypes.Tutorial && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamUploadButton, (UnityEngine.Object) null))
          ((Selectable) this.TeamUploadButton).set_interactable(false);
        this.mResultData = instance.ResultData;
        this.mQuestName = this.mCurrentQuest.iname;
        if (instance.IsPlayingArenaQuest)
        {
          this.mResultData.Record.playerexp = (OInt) GlobalVars.ResultArenaBattleResponse.got_pexp;
          this.mResultData.Record.gold = (OInt) GlobalVars.ResultArenaBattleResponse.got_gold;
          this.mResultData.Record.unitexp = (OInt) GlobalVars.ResultArenaBattleResponse.got_uexp;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoArenaResultWin))
            this.GoArenaResultWin.SetActive(this.mResultData.Record.result == BattleCore.QuestResult.Win);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoArenaResultLose))
            this.GoArenaResultLose.SetActive(this.mResultData.Record.result != BattleCore.QuestResult.Win);
          if (instance.IsArenaRankupInfo())
            MonoSingleton<GameManager>.Instance.Player.UpdateArenaRankTrophyStates(GlobalVars.ResultArenaBattleResponse.new_rank, GlobalVars.ResultArenaBattleResponse.new_rank);
          else
            MonoSingleton<GameManager>.Instance.Player.UpdateArenaRankTrophyStates(GlobalVars.ResultArenaBattleResponse.new_rank, MonoSingleton<GameManager>.Instance.Player.ArenaRankBest);
        }
        bool isMultiTower = instance.Battle.IsMultiTower;
        bool isMultiPlay = instance.Battle.IsMultiPlay;
        for (int index = 0; index < instance.Battle.Units.Count; ++index)
        {
          Unit unit = instance.Battle.Units[index];
          if ((isMultiTower || !isMultiPlay || unit.OwnerPlayerIndex == instance.Battle.MyPlayerIndex) && (player.FindUnitDataByUniqueID(unit.UnitData.UniqueID) != null || isMultiTower && unit.Side == EUnitSide.Player))
          {
            UnitData unitData = new UnitData();
            unitData.Setup(unit.UnitData);
            this.mUnits.Add(unitData);
            this.mMultiTowerUnitsId.Add(unit.OwnerPlayerIndex);
          }
        }
        if (instance.IsArenaRankupInfo())
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 40);
      }
      DataSource.Bind<BattleCore.Record>(((Component) this).get_gameObject(), this.mResultData.Record);
      if (this.mResultData != null)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureListItem, (UnityEngine.Object) null))
        {
          Transform parent = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TreasureList, (UnityEngine.Object) null) ? this.TreasureListItem.get_transform().get_parent() : this.TreasureList.get_transform();
          List<QuestResult.DropItemData> items = new List<QuestResult.DropItemData>();
          for (int index1 = 0; index1 < this.mResultData.Record.items.Count; ++index1)
          {
            bool flag = false;
            for (int index2 = 0; index2 < items.Count; ++index2)
            {
              if (items[index2].mIsSecret == this.mResultData.Record.items[index1].mIsSecret)
              {
                if (items[index2].IsItem)
                {
                  if (items[index2].itemParam == this.mResultData.Record.items[index1].itemParam)
                  {
                    items[index2].Gain(1);
                    flag = true;
                    break;
                  }
                }
                else if (items[index2].IsConceptCard && items[index2].conceptCardParam == this.mResultData.Record.items[index1].conceptCardParam)
                {
                  items[index2].Gain(1);
                  flag = true;
                  break;
                }
              }
            }
            if (!flag)
            {
              QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
              if (this.mResultData.Record.items[index1].IsItem)
              {
                dropItemData.SetupDropItemData(EBattleRewardType.Item, 0L, this.mResultData.Record.items[index1].itemParam.iname, 1);
                if (this.mResultData.Record.items[index1].itemParam.type != EItemType.Unit)
                {
                  ItemData itemDataByItemParam = player.FindItemDataByItemParam(this.mResultData.Record.items[index1].itemParam);
                  dropItemData.IsNew = !player.ItemEntryExists(this.mResultData.Record.items[index1].itemParam.iname) || (itemDataByItemParam == null || itemDataByItemParam.IsNew);
                }
                else
                {
                  string iid = this.mResultData.Record.items[index1].itemParam.iname;
                  if (player.Units.Find((Predicate<UnitData>) (p => p.UnitParam.iname == iid)) == null)
                    dropItemData.IsNew = true;
                }
              }
              else if (this.mResultData.Record.items[index1].IsConceptCard)
                dropItemData.SetupDropItemData(EBattleRewardType.ConceptCard, 0L, this.mResultData.Record.items[index1].conceptCardParam.iname, 1);
              dropItemData.mIsSecret = this.mResultData.Record.items[index1].mIsSecret;
              items.Add(dropItemData);
            }
          }
          if (this.mCurrentQuest != null && this.mCurrentQuest.IsVersus)
          {
            VersusCoinParam coinParam = MonoSingleton<GameManager>.Instance.GetVersusCoinParam(this.mCurrentQuest.iname);
            if (coinParam != null)
            {
              QuestResult.DropItemData dropItemData1 = items.Find((Predicate<QuestResult.DropItemData>) (x => x.Param.iname == coinParam.coin_iname));
              if (dropItemData1 != null)
                dropItemData1.Gain((int) this.mResultData.Record.pvpcoin);
              else if ((int) this.mResultData.Record.pvpcoin > 0)
              {
                QuestResult.DropItemData dropItemData2 = new QuestResult.DropItemData();
                dropItemData2.Setup(0L, coinParam.coin_iname, (int) this.mResultData.Record.pvpcoin);
                dropItemData2.mIsSecret = false;
                dropItemData2.IsNew = !player.ItemEntryExists(coinParam.coin_iname);
                items.Add(dropItemData2);
              }
            }
          }
          this.CreateItemObject(items, parent);
          this.CreateArtifactObjects(parent);
          this.CreateGoldObjects(parent);
        }
        this.ApplyQuestCampaignParams(instance.Battle.QuestCampaignIds);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitListItem, (UnityEngine.Object) null))
        {
          if (instance.Battle.IsMultiTower)
            this.AddExpPlayerMultiTower();
          else
            this.AddExpPlayer();
        }
        GlobalVars.PlayerExpOld.Set(this.mResultData.StartExp);
        GlobalVars.PlayerExpNew.Set(this.mResultData.StartExp + (int) this.mResultData.Record.playerexp);
        GlobalVars.PlayerLevelChanged.Set(player.Lv != PlayerData.CalcLevelFromExp(this.mResultData.StartExp));
        this.RefreshQuestMissionReward();
        if (!string.IsNullOrEmpty(this.Star_ClearTrigger))
        {
          for (int index = 0; index < this.mObjectiveStars.Count; ++index)
          {
            if ((this.mCurrentQuest.clear_missions & 1 << index) != 0)
              GameUtility.SetAnimatorTrigger(this.mObjectiveStars[index], this.Star_ClearTrigger);
          }
        }
        player.OnGoldChange((int) this.mResultData.Record.gold);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StarKakuninButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.StarKakuninButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnStarKakuninButtonClick)));
        this.mContinueStarAnimation = false;
      }
      GlobalVars.CreateAutoMultiTower = false;
      GlobalVars.InvtationSameUser = false;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultSkipButton, (UnityEngine.Object) null))
        return;
      ((Component) this.ResultSkipButton).get_gameObject().SetActive(false);
    }

    public virtual void CreateItemObject(List<QuestResult.DropItemData> items, Transform parent)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        GameObject root = (GameObject) null;
        if (items[index].IsConceptCard)
        {
          root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.TreasureListConceptCard);
          root.get_transform().SetParent(parent, false);
          this.mTreasureListItems.Add(root);
          DataSource.Bind<QuestResult.DropItemData>(root, items[index]);
          if (items[index].mIsSecret)
          {
            ItemIcon component = (ItemIcon) root.GetComponent<DropItemIcon>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.IsSecret = true;
          }
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
        else if (items[index].IsItem)
        {
          root = (GameObject) UnityEngine.Object.Instantiate<GameObject>(items[index].ItemType != EItemType.Unit ? (M0) this.TreasureListItem : (M0) this.TreasureListUnit);
          root.get_transform().SetParent(parent, false);
          this.mTreasureListItems.Add(root);
          DataSource.Bind<ItemData>(root, (ItemData) items[index]);
          if (items[index].mIsSecret)
          {
            ItemIcon component = (ItemIcon) root.GetComponent<ItemIcon>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.IsSecret = true;
          }
          root.SetActive(true);
          GameParameter.UpdateAll(root);
        }
        else
          DebugUtility.LogError(string.Format("[コードの追加が必要] DropItemData.mBattleRewardType(={0})は不明な列挙です", (object) items[index].BattleRewardType));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_NewItemBadge, (UnityEngine.Object) null) && items[index].IsNew)
        {
          RectTransform transform = ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.Prefab_NewItemBadge)).get_transform() as RectTransform;
          ((Component) transform).get_gameObject().SetActive(true);
          transform.set_anchoredPosition(Vector2.get_zero());
          ((Transform) transform).SetParent(root.get_transform(), false);
        }
      }
    }

    private void CreateArtifactObjects(Transform parent)
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts;
      OrderedDictionary orderedDictionary = new OrderedDictionary();
      using (List<ArtifactParam>.Enumerator enumerator = this.mResultData.Record.artifacts.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ArtifactParam current = enumerator.Current;
          if (orderedDictionary.Contains((object) current.iname))
          {
            int num = (int) orderedDictionary[(object) current.iname];
            orderedDictionary[(object) current.iname] = (object) (num + 1);
          }
          else
            orderedDictionary.Add((object) current.iname, (object) 1);
        }
      }
      foreach (DictionaryEntry dictionaryEntry in orderedDictionary)
      {
        DictionaryEntry artiAndNum = dictionaryEntry;
        GameObject root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.TreasureListArtifact);
        root.get_transform().SetParent(parent, false);
        ArtifactParam data = artifacts.FirstOrDefault<ArtifactParam>((Func<ArtifactParam, bool>) (arti => arti.iname == (string) artiAndNum.Key));
        this.mTreasureListItems.Add(root);
        DataSource.Bind<ArtifactParam>(root, data);
        DataSource.Bind<int>(root, (int) artiAndNum.Value);
        root.SetActive(true);
        GameParameter.UpdateAll(root);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_NewItemBadge, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.Player.GetArtifactNumByRarity(data.iname, data.rareini) <= 0)
        {
          RectTransform transform = ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.Prefab_NewItemBadge)).get_transform() as RectTransform;
          ((Component) transform).get_gameObject().SetActive(true);
          transform.set_anchoredPosition(Vector2.get_zero());
          ((Transform) transform).SetParent(root.get_transform(), false);
        }
      }
    }

    private void CreateGoldObjects(Transform parent)
    {
      if (this.mCurrentQuest != null && !this.mCurrentQuest.IsVersus)
        return;
      if ((int) this.mResultData.Record.gold <= 0)
        return;
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.TreasureListItem);
      gameObject.get_transform().SetParent(parent, false);
      this.mTreasureListItems.Add(gameObject);
      gameObject.SetActive(true);
      Transform child1 = gameObject.get_transform().FindChild("BODY/frame");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child1, (UnityEngine.Object) null))
      {
        Image_Transparent component = (Image_Transparent) ((Component) child1).GetComponent<Image_Transparent>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GoldFrame, (UnityEngine.Object) null))
          component.set_sprite(this.GoldFrame);
      }
      Transform child2 = gameObject.get_transform().FindChild("BODY/itemicon");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child2, (UnityEngine.Object) null))
      {
        RawImage_Transparent component = (RawImage_Transparent) ((Component) child2).GetComponent<RawImage_Transparent>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GoldTex, (UnityEngine.Object) null))
          component.set_texture((Texture) this.GoldTex);
      }
      Transform child3 = gameObject.get_transform().FindChild("BODY/amount/Text_amount");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) child3, (UnityEngine.Object) null))
        return;
      BitmapText component1 = (BitmapText) ((Component) child3).GetComponent<BitmapText>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      component1.text = this.mResultData.Record.gold.ToString();
    }

    public virtual void AddExpPlayer()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitExpText, (UnityEngine.Object) null))
        this.UnitExpText.AddComponent<QuestResult.CampaignPartyExp>();
      Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitList, (UnityEngine.Object) null) ? this.UnitListItem.get_transform().get_parent() : this.UnitList.get_transform();
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.UnitListItem);
        gameObject.get_transform().SetParent(transform, false);
        QuestResult.CampaignPartyExp componentInChildren = (QuestResult.CampaignPartyExp) gameObject.GetComponentInChildren<QuestResult.CampaignPartyExp>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          componentInChildren.Exp = this.AcquiredUnitExp[index];
        ConceptCardIcon component = (ConceptCardIcon) gameObject.GetComponent<ConceptCardIcon>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Setup(this.mUnits[index].ConceptCard);
        this.mUnitListItems.Add(gameObject);
        DataSource.Bind<UnitData>(gameObject, this.mUnits[index]);
        gameObject.SetActive(true);
      }
    }

    public void AddExpPlayerMultiTower()
    {
      if (this.MultiTowerPlayerObj == null || this.MultiTowerPlayerTransform == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitExpText, (UnityEngine.Object) null))
        this.UnitExpText.AddComponent<QuestResult.CampaignPartyExp>();
      List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
      int length = SceneBattle.Instance.MultiPlayerCount + 1;
      for (int index = 0; index < this.MultiTowerPlayerObj.Length; ++index)
      {
        if (length > index)
          DataSource.Bind<JSON_MyPhotonPlayerParam>(this.MultiTowerPlayerObj[index], myPlayersStarted[index]);
        else
          this.MultiTowerPlayerObj[index].SetActive(false);
      }
      List<UnitData>[] unitDataListArray = new List<UnitData>[length];
      for (int index = 0; index < unitDataListArray.Length; ++index)
        unitDataListArray[index] = new List<UnitData>();
      for (int index = 0; index < this.mUnits.Count; ++index)
        unitDataListArray[this.mMultiTowerUnitsId[index] - 1].Add(this.mUnits[index]);
      for (int index1 = 0; index1 < length; ++index1)
      {
        Transform transform = (Transform) this.MultiTowerPlayerTransform[index1];
        for (int index2 = 0; index2 < unitDataListArray[index1].Count; ++index2)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.UnitListItem);
          gameObject.get_transform().SetParent(transform, false);
          QuestResult.CampaignPartyExp componentInChildren = (QuestResult.CampaignPartyExp) gameObject.GetComponentInChildren<QuestResult.CampaignPartyExp>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
            componentInChildren.Exp = this.AcquiredUnitExp[index2];
          this.mUnitListItems.Add(gameObject);
          DataSource.Bind<UnitData>(gameObject, unitDataListArray[index1][index2]);
          gameObject.SetActive(true);
        }
      }
    }

    public virtual void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.StartCoroutine(this.PlayAnimationAsync());
          break;
        case 100:
          this.mResultSkipElement = true;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ResultSkipButton, (UnityEngine.Object) null))
            break;
          ((Component) this.ResultSkipButton).get_gameObject().SetActive(false);
          break;
      }
    }

    public void TriggerAnimation(string trigger)
    {
      if (string.IsNullOrEmpty(trigger) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MainAnimator, (UnityEngine.Object) null))
        return;
      this.MainAnimator.SetTrigger(trigger);
    }

    [DebuggerHidden]
    public virtual IEnumerator PlayAnimationAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CPlayAnimationAsync\u003Ec__Iterator12F()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    public virtual IEnumerator AddExp()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CAddExp\u003Ec__Iterator130()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    protected IEnumerator RecvExpAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CRecvExpAnimation\u003Ec__Iterator131()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    protected IEnumerator RecvTrustAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CRecvTrustAnimation\u003Ec__Iterator132()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    public virtual IEnumerator StartTreasureAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CStartTreasureAnimation\u003Ec__Iterator133()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    protected virtual IEnumerator TreasureAnimation(List<GameObject> ListItems)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CTreasureAnimation\u003Ec__Iterator134()
      {
        ListItems = ListItems,
        \u003C\u0024\u003EListItems = ListItems,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    protected IEnumerator ObjectiveAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestResult.\u003CObjectiveAnimation\u003Ec__Iterator135()
      {
        \u003C\u003Ef__this = this
      };
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
        foreach (QuestCampaignData questCampaignData in questCampaigns)
        {
          QuestCampaignData data = questCampaignData;
          if (data.type == QuestCampaignValueTypes.ExpUnit)
          {
            if (string.IsNullOrEmpty(data.unit))
            {
              num1 = data.GetRate();
            }
            else
            {
              int index = mUnits.FindIndex((Predicate<UnitData>) (value => value.UnitParam.iname == data.unit));
              if (index >= 0)
                numArray[index] = data.GetRate();
            }
          }
          else if (data.type == QuestCampaignValueTypes.ExpPlayer)
            this.mResultData.Record.playerexp = (OInt) Mathf.RoundToInt((float) (int) this.mResultData.Record.playerexp * data.GetRate());
          else if (data.type == QuestCampaignValueTypes.Ap && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextConsumeAp, (UnityEngine.Object) null))
            ((Graphic) this.TextConsumeAp).set_color(this.TextConsumeApColor);
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

    private void RefreshQuestMissionReward()
    {
      if (this.mCurrentQuest == null)
        return;
      this.mUseLarge = this.mCurrentQuest.bonusObjective != null && this.mCurrentQuest.bonusObjective.Length > 3;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MissionDetailLarge, (UnityEngine.Object) null))
        ((Component) this.MissionDetailLarge).get_gameObject().SetActive(this.mUseLarge);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MissionDetailSmall, (UnityEngine.Object) null))
        ((Component) this.MissionDetailSmall).get_gameObject().SetActive(!this.mUseLarge);
      BattleResultMissionDetail resultMissionDetail = !this.mUseLarge ? this.MissionDetailSmall : this.MissionDetailLarge;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) resultMissionDetail, (UnityEngine.Object) null))
        return;
      this.mMissionDetail = (BattleResultMissionDetail) ((Component) resultMissionDetail).GetComponent<BattleResultMissionDetail>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMissionDetail, (UnityEngine.Object) null))
        return;
      this.mObjectiveStars = this.mMissionDetail.GetObjectiveStars();
    }

    public void OnClickMultiTowerRetry()
    {
      GlobalVars.CreateAutoMultiTower = true;
      UIUtility.ConfirmBoxTitle((string) null, LocalizedText.Get("sys.MULTI_TOWER_SAMEUSER"), (UIUtility.DialogResultEvent) (g =>
      {
        FlowNode_Variable.Set("MultiPlayPasscode", "1");
        GlobalVars.InvtationSameUser = true;
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FINISH_RESULT");
      }), (UIUtility.DialogResultEvent) (g =>
      {
        FlowNode_Variable.Set("MultiPlayPasscode", "0");
        GlobalVars.InvtationSameUser = false;
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "FINISH_RESULT");
      }), (GameObject) null, false, -1, (string) null, (string) null);
    }

    public void OnClickMultiTowerNextRetry()
    {
      ++GlobalVars.SelectedMultiTowerFloor;
      this.OnClickMultiTowerRetry();
    }

    public void OnPartyUploadFinished()
    {
      if (!GlobalVars.PartyUploadFinished || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamUploadButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.TeamUploadButton).set_interactable(false);
    }

    public class DropItemData : ItemData
    {
      private EBattleRewardType mBattleRewardType = EBattleRewardType.Item;
      public bool mIsSecret;
      private ConceptCardData mConceptCardData;

      public EBattleRewardType BattleRewardType
      {
        get
        {
          return this.mBattleRewardType;
        }
      }

      public bool IsItem
      {
        get
        {
          return this.mBattleRewardType == EBattleRewardType.Item;
        }
      }

      public bool IsConceptCard
      {
        get
        {
          return this.mBattleRewardType == EBattleRewardType.ConceptCard;
        }
      }

      public ItemParam itemParam
      {
        get
        {
          return this.Param;
        }
      }

      public ItemData itemData
      {
        get
        {
          return (ItemData) this;
        }
      }

      public ConceptCardParam conceptCardParam
      {
        get
        {
          if (this.mConceptCardData != null)
            return this.mConceptCardData.Param;
          return (ConceptCardParam) null;
        }
      }

      public ConceptCardData conceptCardData
      {
        get
        {
          return this.mConceptCardData;
        }
      }

      public void SetupDropItemData(EBattleRewardType rewardType, long iid, string iname, int num)
      {
        this.mBattleRewardType = rewardType;
        if (rewardType == EBattleRewardType.Item)
        {
          this.Setup(iid, iname, num);
        }
        else
        {
          if (rewardType != EBattleRewardType.ConceptCard)
            return;
          this.SetupConceptCard(iname, num);
        }
      }

      private void SetupConceptCard(string iname, int num)
      {
        this.mBattleRewardType = EBattleRewardType.ConceptCard;
        this.mConceptCardData = ConceptCardData.CreateConceptCardDataForDisplay(iname);
        this.mNum = num;
      }
    }

    public class TrustAnimWork
    {
      public UnitData beforeUnit;
      public UnitData afterUnit;
      public ConceptCardIconBattleResult cardIcon;

      public TrustAnimWork(UnitData before, UnitData after, ConceptCardIconBattleResult card)
      {
        this.beforeUnit = before;
        this.afterUnit = after;
        this.cardIcon = card;
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
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.set_text(this.Exp.ToString());
      }
    }
  }
}
