// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(32, "クリア演出終了", FlowNode.PinTypes.Output, 32)]
  [FlowNode.Pin(11, "クリア演出開始", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(201, "ミッション不達成（敵が復活した）", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(200, "ミッション不達成", FlowNode.PinTypes.Output, 200)]
  public class TowerQuestResult : QuestResult
  {
    [SerializeField]
    [Description("クリア条件の星にトリガーを送る間隔 (秒数)")]
    private float TowerItem_TriggerInterval = 1f;
    [SerializeField]
    private float DeadAlpha = 0.5f;
    private List<GameObject> mTowerListItems = new List<GameObject>();
    private List<HpBarSlider> mHpBar = new List<HpBarSlider>();
    private List<TowerUnitIsDead> canvas_group = new List<TowerUnitIsDead>();
    private const int INPUT_START_EFFECT = 11;
    private const int OUTPUT_END_EFFECT = 32;
    private const int OUTPUT_MISSION_FAILURE = 200;
    private const int OUTPUT_MISSION_FAILURE_RESET = 201;
    [Description("塔報酬画面用入手アイテムのゲームオブジェクト")]
    [SerializeField]
    [HeaderBar("▼ヴェーダ用")]
    private GameObject TowerTreasureListItem;
    [SerializeField]
    private GameObject m_RewardPanel;
    [SerializeField]
    private TowerClear TowerClear;
    [SerializeField]
    private GameObject Window;
    [SerializeField]
    private string PreRewardAnimationTrigger;
    [SerializeField]
    private string PostRewardAnimationTrigger;
    [SerializeField]
    private float PreRewardAnimationDelay;
    [SerializeField]
    private float PostRewardAnimationDelay;
    private bool mContinueTowerItemAnimation;
    private bool mContinueTowerItem;
    private BattleCore.Record m_QuestRecord;
    private TowerQuestResult.eTowerResultFlags m_TowerResultFlags;

    private bool IsEnableMission
    {
      get
      {
        return this.ResultFlags_IsOn(TowerQuestResult.eTowerResultFlags.MissionComplete) || this.ResultFlags_IsOn(TowerQuestResult.eTowerResultFlags.MissionFailure);
      }
    }

    private bool IsUnlockNextFloor
    {
      get
      {
        return this.ResultFlags_IsOn(TowerQuestResult.eTowerResultFlags.MissionComplete) && this.ResultFlags_IsOn(TowerQuestResult.eTowerResultFlags.Win) || !this.IsEnableMission;
      }
    }

    private bool IsEnemyReset
    {
      get
      {
        return this.ResultFlags_IsOn(TowerQuestResult.eTowerResultFlags.MissionFailure) && this.ResultFlags_IsOn(TowerQuestResult.eTowerResultFlags.Win);
      }
    }

    public override void Activated(int pinID)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.Battle != null)
        this.m_QuestRecord = SceneBattle.Instance.Battle.GetQuestRecord();
      this.UpdateResultFlags();
      if (pinID == 11)
      {
        if (this.ResultFlags_IsOn(TowerQuestResult.eTowerResultFlags.ShowRank))
        {
          this.Window.SetActive(false);
          this.TowerClear.Refresh();
          ((Component) this.TowerClear).get_gameObject().SetActive(true);
        }
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 32);
      }
      else
        base.Activated(pinID);
    }

    private void UpdateResultFlags()
    {
      if (this.mCurrentQuest == null)
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      TowerParam tower = MonoSingleton<GameManager>.Instance.FindTower(MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mQuestName).tower_id);
      if (this.m_QuestRecord.result == BattleCore.QuestResult.Win)
        this.ResultFlags_SetOn(TowerQuestResult.eTowerResultFlags.Win);
      else
        this.ResultFlags_SetOn(TowerQuestResult.eTowerResultFlags.Lose);
      if (this.mCurrentQuest.HasMission())
      {
        bool flag = true;
        bool[] flagArray = new bool[this.m_QuestRecord.bonusCount];
        for (int index = 0; index < this.m_QuestRecord.bonusCount; ++index)
        {
          if ((this.m_QuestRecord.bonusFlags & 1 << index) != 0)
            flagArray[index] = true;
          if (this.mCurrentQuest.IsMissionClear(index))
            flagArray[index] = true;
          if (!flagArray[index])
          {
            flag = false;
            break;
          }
        }
        if (flag)
          this.ResultFlags_SetOn(TowerQuestResult.eTowerResultFlags.MissionComplete);
        else
          this.ResultFlags_SetOn(TowerQuestResult.eTowerResultFlags.MissionFailure);
      }
      if (towerResuponse.IsNotClear || towerResuponse.IsRoundClear || !tower.is_view_ranking)
        this.ResultFlags_Set(TowerQuestResult.eTowerResultFlags.ShowRank, false);
      else
        this.ResultFlags_Set(TowerQuestResult.eTowerResultFlags.ShowRank, true);
    }

    public void EndTowerClear()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 32);
    }

    private void SetTowerResult(Transform parent, GameObject ItemObject, List<ItemData> data = null)
    {
      if (this.mCurrentQuest.type != QuestTypes.Tower)
        return;
      List<TowerRewardItem> towerRewardItemList = new List<TowerRewardItem>((IEnumerable<TowerRewardItem>) MonoSingleton<GameManager>.Instance.FindTowerReward(MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID).reward_id).GetTowerRewardItem());
      if (data != null)
      {
        for (int index = 0; index < data.Count; ++index)
        {
          ItemData itemData = data[index];
          if (itemData != null)
            towerRewardItemList.Add(new TowerRewardItem()
            {
              iname = itemData.Param.iname,
              type = TowerRewardItem.RewardType.Item,
              num = data[index].Num,
              visible = true,
              is_new = data[index].IsNew
            });
        }
      }
      for (int index = 0; index < towerRewardItemList.Count; ++index)
      {
        TowerRewardItem item = towerRewardItemList[index];
        if (item.visible && item.type != TowerRewardItem.RewardType.Gold)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ItemObject);
          gameObject.get_transform().SetParent(parent, false);
          this.mTowerListItems.Add(gameObject);
          gameObject.get_transform().set_localScale(ItemObject.get_transform().get_localScale());
          DataSource.Bind<TowerRewardItem>(gameObject, towerRewardItemList[index]);
          gameObject.SetActive(true);
          foreach (GameParameter componentsInChild in (GameParameter[]) gameObject.GetComponentsInChildren<GameParameter>())
            componentsInChild.Index = index;
          TowerRewardUI componentInChildren1 = (TowerRewardUI) gameObject.GetComponentInChildren<TowerRewardUI>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
            componentInChildren1.Refresh();
          if (item.type == TowerRewardItem.RewardType.Artifact)
          {
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            ArtifactIcon componentInChildren2 = (ArtifactIcon) gameObject.GetComponentInChildren<ArtifactIcon>();
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null))
            {
              ((Behaviour) componentInChildren2).set_enabled(true);
              componentInChildren2.UpdateValue();
              if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (x => x.ArtifactParam.iname == item.iname)) == null)
              {
                item.is_new = true;
                break;
              }
              break;
            }
            break;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_NewItemBadge, (UnityEngine.Object) null) && item.is_new)
          {
            RectTransform transform = ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.Prefab_NewItemBadge)).get_transform() as RectTransform;
            ((Component) transform).get_gameObject().SetActive(true);
            transform.set_anchoredPosition(Vector2.get_zero());
            ((Transform) transform).SetParent(gameObject.get_transform(), false);
          }
        }
      }
      ItemObject.SetActive(false);
    }

    [DebuggerHidden]
    private IEnumerator RecvHealAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CRecvHealAnimation\u003Ec__Iterator140()
      {
        \u003C\u003Ef__this = this
      };
    }

    private bool IsHealEnd(List<TowerQuestResult.HealParanm> param)
    {
      for (int index = 0; index < param.Count; ++index)
      {
        if (param[index].hp_heal > param[index].hpGained)
          return false;
      }
      return true;
    }

    public override void CreateItemObject(List<QuestResult.DropItemData> items, Transform parent)
    {
      this.SetTowerResult(this.TowerTreasureListItem.get_transform().get_parent(), this.TowerTreasureListItem, (List<ItemData>) null);
    }

    public override void AddExpPlayer()
    {
      Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitList, (UnityEngine.Object) null) ? this.UnitListItem.get_transform().get_parent() : this.UnitList.get_transform();
      for (int index = 0; index < this.mUnits.Count; ++index)
      {
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.UnitListItem);
        gameObject.get_transform().SetParent(transform, false);
        this.canvas_group.Add((TowerUnitIsDead) gameObject.GetComponentInChildren<TowerUnitIsDead>());
        this.mUnitListItems.Add(gameObject);
        if (this.mCurrentQuest.type == QuestTypes.Tower)
          this.mHpBar.Add((HpBarSlider) gameObject.GetComponentInChildren<HpBarSlider>());
        DataSource.Bind<UnitData>(gameObject, this.mUnits[index]);
        gameObject.SetActive(true);
      }
      if (this.mCurrentQuest.type != QuestTypes.Tower)
        return;
      for (int i = 0; i < this.mHpBar.Count; ++i)
      {
        Unit unit = SceneBattle.Instance.Battle.Player.Find((Predicate<Unit>) (x => x.UnitData.UniqueID == this.mUnits[i].UniqueID));
        if (unit != null && MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit.UnitData.UniqueID) != null)
        {
          this.mHpBar[i].slider.set_maxValue((float) unit.TowerStartHP);
          this.mHpBar[i].slider.set_minValue(0.0f);
          this.mHpBar[i].slider.set_value((float) (int) unit.CurrentStatus.param.hp);
          TowerResuponse.PlayerUnit playerUnit = MonoSingleton<GameManager>.Instance.TowerResuponse.FindPlayerUnit(this.mUnits[i]);
          if (playerUnit != null && playerUnit.isDied)
            ((CanvasGroup) ((Component) this.mUnitListItems[i].get_transform().GetChild(0)).get_gameObject().GetComponent<CanvasGroup>()).set_alpha(this.DeadAlpha);
          GameParameter.UpdateAll(this.mUnitListItems[i]);
        }
      }
    }

    [DebuggerHidden]
    public override IEnumerator AddExp()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CAddExp\u003Ec__Iterator141()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void SetAnimationBool(string name, bool value)
    {
      if (string.IsNullOrEmpty(name) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MainAnimator, (UnityEngine.Object) null))
        return;
      this.MainAnimator.SetBool(name, value);
    }

    [DebuggerHidden]
    public override IEnumerator PlayAnimationAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CPlayAnimationAsync\u003Ec__Iterator142()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator StartTowerTreasureAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CStartTowerTreasureAnimation\u003Ec__Iterator143()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    protected IEnumerator TowerTreasureAnimation(List<GameObject> ListItems)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CTowerTreasureAnimation\u003Ec__Iterator144()
      {
        ListItems = ListItems,
        \u003C\u0024\u003EListItems = ListItems,
        \u003C\u003Ef__this = this
      };
    }

    public void OnTowerItemAnimationEnd()
    {
      this.mContinueTowerItemAnimation = true;
    }

    public void OnTowerItemEnd()
    {
      this.mContinueTowerItem = true;
    }

    private bool ResultFlags_IsOn(TowerQuestResult.eTowerResultFlags flags)
    {
      return (this.m_TowerResultFlags & flags) != (TowerQuestResult.eTowerResultFlags) 0;
    }

    private bool ResultFlags_IsOff(TowerQuestResult.eTowerResultFlags flags)
    {
      return (this.m_TowerResultFlags & flags) == (TowerQuestResult.eTowerResultFlags) 0;
    }

    private void ResultFlags_SetOn(TowerQuestResult.eTowerResultFlags flags)
    {
      this.m_TowerResultFlags |= flags;
    }

    private void ResultFlags_SetOff(TowerQuestResult.eTowerResultFlags flags)
    {
      this.m_TowerResultFlags &= ~flags;
    }

    private void ResultFlags_Set(TowerQuestResult.eTowerResultFlags flags, bool value)
    {
      if (value)
        this.ResultFlags_SetOn(flags);
      else
        this.ResultFlags_SetOff(flags);
    }

    [Flags]
    private enum eTowerResultFlags
    {
      Win = 1,
      Lose = 2,
      Clear = 4,
      ShowRank = 8,
      MissionFailure = 16, // 0x00000010
      MissionComplete = 32, // 0x00000020
    }

    private class HealParanm
    {
      public Unit unit;
      public int hp;
      public int hp_heal;
      public float hp_gainRate;
      public int hpGained;
      public float hpAccumulator;
      public HpBarSlider hp_bar;

      public HealParanm(Unit unit, TowerFloorParam FloorParam, HpBarSlider hp_bar, float GainRate, float GainTimeMax)
      {
        this.unit = unit;
        this.hp = unit.TowerStartHP;
        this.hp_heal = FloorParam.CalcHelaNum(this.hp);
        this.hp_gainRate = GainRate;
        if ((double) ((float) this.hp_heal / GainRate) > (double) GainTimeMax)
          this.hp_gainRate = (float) this.hp_heal / GainTimeMax;
        this.hp_bar = hp_bar;
      }

      public void Update()
      {
        this.hpAccumulator += this.hp_gainRate * Time.get_deltaTime();
        if ((double) this.hpAccumulator < 1.0)
          return;
        int num = Mathf.FloorToInt(this.hpAccumulator);
        this.hpAccumulator %= 1f;
        this.hpGained += num;
        if (this.hp_heal < this.hpGained)
        {
          num = Math.Max(num - (this.hpGained - this.hp_heal), 0);
          this.hpGained = this.hp_heal;
        }
        this.unit.Heal(num);
        this.hp_bar.slider.set_value((float) (int) this.unit.CurrentStatus.param.hp);
        this.hp_bar.color.ChangeValue((int) this.unit.CurrentStatus.param.hp, this.unit.TowerStartHP);
      }
    }
  }
}
