// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
  public class TowerQuestResult : QuestResult
  {
    [Description("クリア条件の星にトリガーを送る間隔 (秒数)")]
    public float TowerItem_TriggerInterval = 1f;
    protected List<GameObject> mTowerListItems = new List<GameObject>();
    private List<HpBarSlider> mHpBar = new List<HpBarSlider>();
    public float DeadAlpha = 0.5f;
    private List<TowerUnitIsDead> canvas_group = new List<TowerUnitIsDead>();
    [Description("塔報酬画面用入手アイテムのゲームオブジェクト")]
    public GameObject TowerTreasureListItem;
    [Description("塔リザルト画面入手アイテムのゲームオブジェクト")]
    public GameObject TowerItemObject;
    private bool mContinueTowerItemAnimation;
    private bool mContinueTowerItem;
    public TowerClear TowerClear;
    public GameObject Window;

    public override void Activated(int pinID)
    {
      base.Activated(pinID);
      if (pinID != 11)
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      TowerParam tower = MonoSingleton<GameManager>.Instance.FindTower(MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mQuestName).tower_id);
      if (towerResuponse.clear == 0 || towerResuponse.clear == 1 && towerResuponse.arrived_num == 0 || !tower.is_view_ranking)
      {
        this.EndTowerClear();
      }
      else
      {
        this.Window.SetActive(false);
        this.TowerClear.Refresh();
        ((Component) this.TowerClear).get_gameObject().SetActive(true);
      }
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
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        TowerQuestResult.\u003CSetTowerResult\u003Ec__AnonStorey380 resultCAnonStorey380 = new TowerQuestResult.\u003CSetTowerResult\u003Ec__AnonStorey380();
        // ISSUE: reference to a compiler-generated field
        resultCAnonStorey380.item = towerRewardItemList[index];
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (resultCAnonStorey380.item.visible && resultCAnonStorey380.item.type != TowerRewardItem.RewardType.Gold)
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
          // ISSUE: reference to a compiler-generated field
          if (resultCAnonStorey380.item.type == TowerRewardItem.RewardType.Artifact)
          {
            // ISSUE: reference to a compiler-generated field
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(resultCAnonStorey380.item.iname);
            DataSource.Bind<ArtifactParam>(gameObject, artifactParam);
            ArtifactIcon componentInChildren2 = (ArtifactIcon) gameObject.GetComponentInChildren<ArtifactIcon>();
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null))
            {
              ((Behaviour) componentInChildren2).set_enabled(true);
              componentInChildren2.UpdateValue();
              // ISSUE: reference to a compiler-generated method
              if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find(new Predicate<ArtifactData>(resultCAnonStorey380.\u003C\u003Em__426)) == null)
              {
                // ISSUE: reference to a compiler-generated field
                resultCAnonStorey380.item.is_new = true;
                break;
              }
              break;
            }
            break;
          }
          // ISSUE: reference to a compiler-generated field
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_NewItemBadge, (UnityEngine.Object) null) && resultCAnonStorey380.item.is_new)
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
      return (IEnumerator) new TowerQuestResult.\u003CRecvHealAnimation\u003Ec__Iterator118() { \u003C\u003Ef__this = this };
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      TowerQuestResult.\u003CAddExpPlayer\u003Ec__AnonStorey381 playerCAnonStorey381 = new TowerQuestResult.\u003CAddExpPlayer\u003Ec__AnonStorey381();
      // ISSUE: reference to a compiler-generated field
      playerCAnonStorey381.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (playerCAnonStorey381.i = 0; playerCAnonStorey381.i < this.mHpBar.Count; ++playerCAnonStorey381.i)
      {
        // ISSUE: reference to a compiler-generated method
        Unit unit = SceneBattle.Instance.Battle.Player.Find(new Predicate<Unit>(playerCAnonStorey381.\u003C\u003Em__427));
        if (unit != null && MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit.UnitData.UniqueID) != null)
        {
          // ISSUE: reference to a compiler-generated field
          this.mHpBar[playerCAnonStorey381.i].slider.set_maxValue((float) unit.TowerStartHP);
          // ISSUE: reference to a compiler-generated field
          this.mHpBar[playerCAnonStorey381.i].slider.set_minValue(0.0f);
          // ISSUE: reference to a compiler-generated field
          this.mHpBar[playerCAnonStorey381.i].slider.set_value((float) (int) unit.CurrentStatus.param.hp);
          // ISSUE: reference to a compiler-generated field
          TowerResuponse.PlayerUnit playerUnit = MonoSingleton<GameManager>.Instance.TowerResuponse.FindPlayerUnit(this.mUnits[playerCAnonStorey381.i]);
          if (playerUnit != null && playerUnit.isDied)
          {
            // ISSUE: reference to a compiler-generated field
            ((CanvasGroup) ((Component) this.mUnitListItems[playerCAnonStorey381.i].get_transform().GetChild(0)).get_gameObject().GetComponent<CanvasGroup>()).set_alpha(this.DeadAlpha);
          }
          // ISSUE: reference to a compiler-generated field
          GameParameter.UpdateAll(this.mUnitListItems[playerCAnonStorey381.i]);
        }
      }
    }

    [DebuggerHidden]
    public override IEnumerator AddExp()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CAddExp\u003Ec__Iterator119() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    public override IEnumerator PlayAnimationAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CPlayAnimationAsync\u003Ec__Iterator11A() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    public IEnumerator StartTowerTreasureAnimation()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CStartTowerTreasureAnimation\u003Ec__Iterator11B() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    protected IEnumerator TowerTreasureAnimation(List<GameObject> ListItems)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerQuestResult.\u003CTowerTreasureAnimation\u003Ec__Iterator11C() { ListItems = ListItems, \u003C\u0024\u003EListItems = ListItems, \u003C\u003Ef__this = this };
    }

    public void OnTowerItemAnimationEnd()
    {
      this.mContinueTowerItemAnimation = true;
    }

    public void OnTowerItemEnd()
    {
      this.mContinueTowerItem = true;
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
