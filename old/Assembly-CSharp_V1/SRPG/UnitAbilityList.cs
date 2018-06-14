// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilityList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "アビリティ詳細画面の表示", FlowNode.PinTypes.Output, 100)]
  public class UnitAbilityList : MonoBehaviour, IGameParameter, IFlowInterface
  {
    public UnitAbilityList.RefreshTypes RefreshOnStart;
    private UnitAbilityList.RefreshTypes mLastDisplayMode;
    private EAbilitySlot mLastDisplaySlot;
    public bool AutoRefresh;
    public UnitAbilityList.AbilityEvent OnAbilitySelect;
    public UnitAbilityList.AbilityEvent OnAbilityRankUp;
    public UnitAbilityList.AbilityEvent OnRankUpBtnPress;
    public UnitAbilityList.AbilityEvent OnAbilityRankUpExec;
    public UnitAbilityList.AbilitySlotEvent OnSlotSelect;
    public UnitData Unit;
    [Description("アビリティ詳細を表示するアンカー位置 (0.0 - 1.0)")]
    public Vector2 TooltipAnchorPos;
    public GameObject AbilityTooltip;
    [Description("通常状態のアイテムとして使用する雛形")]
    public UnitAbilityListItemEvents Item_Normal;
    [Description("ロック状態のアイテムとして使用する雛形")]
    public UnitAbilityListItemEvents Item_Locked;
    [Description("使用不可スロットの雛形")]
    public UnitAbilityListItemEvents Item_NoSlot;
    [Description("アビリティが割り当てられていないスロットの雛形")]
    public UnitAbilityListItemEvents Item_Empty;
    [Description("スロットが違うアイテムの雛形")]
    public UnitAbilityListItemEvents Item_SlotMismatch;
    [Description("変更できないアビリティのスロットの雛形")]
    public UnitAbilityListItemEvents Item_Fixed;
    private List<UnitAbilityListItemEvents> mItems;
    [Description("固定アビリティを表示する")]
    public bool ShowFixedAbilities;
    [Description("マスターアビリティを表示する")]
    public bool ShowMasterAbilities;
    public bool ShowLockedJobAbilities;
    public ScrollRect ScrollParent;
    private float mDecelerationRate;

    public UnitAbilityList()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.Item_Normal, (Object) null))
        ((Component) this.Item_Normal).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.Item_Locked, (Object) null))
        ((Component) this.Item_Locked).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.Item_NoSlot, (Object) null))
        ((Component) this.Item_NoSlot).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.Item_Empty, (Object) null))
        ((Component) this.Item_Empty).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.Item_SlotMismatch, (Object) null))
        ((Component) this.Item_SlotMismatch).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.Item_Fixed, (Object) null))
        ((Component) this.Item_Fixed).get_gameObject().SetActive(false);
      if (this.RefreshOnStart == UnitAbilityList.RefreshTypes.DisplayAll)
      {
        this.DisplayAll();
      }
      else
      {
        if (this.RefreshOnStart != UnitAbilityList.RefreshTypes.DisplaySlots)
          return;
        this.DisplaySlots();
      }
    }

    public void Activated(int pinID)
    {
    }

    public void DisplayAll()
    {
      this.DisplaySlotType(~EAbilitySlot.Action, false);
    }

    public void DisplaySlots()
    {
      this.mLastDisplayMode = UnitAbilityList.RefreshTypes.DisplaySlots;
      for (int index = 0; index < this.mItems.Count; ++index)
        Object.Destroy((Object) ((Component) this.mItems[index]).get_gameObject());
      this.mItems.Clear();
      if (this.Unit == null)
        this.Unit = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (this.Unit == null)
        return;
      AbilityData[] equipAbilitys = this.Unit.CreateEquipAbilitys();
      Transform transform = ((Component) this).get_transform();
      for (int index = 0; index < this.Unit.CurrentJob.AbilitySlots.Length; ++index)
      {
        AbilityData data1 = equipAbilitys[index];
        bool flag = false;
        UnitAbilityListItemEvents abilityListItemEvents1;
        if (data1 != null && data1.Param != null)
        {
          flag = data1.Param.is_fixed;
          UnitAbilityListItemEvents abilityListItemEvents2 = !flag ? this.Item_Normal : this.Item_Fixed;
          if (Object.op_Equality((Object) abilityListItemEvents2, (Object) null))
            abilityListItemEvents2 = this.Item_Normal;
          abilityListItemEvents1 = (UnitAbilityListItemEvents) Object.Instantiate<UnitAbilityListItemEvents>((M0) abilityListItemEvents2);
          DataSource.Bind<AbilityData>(((Component) abilityListItemEvents1).get_gameObject(), data1);
          string key = this.Unit.SearchAbilityReplacementSkill(data1.Param.iname);
          if (!string.IsNullOrEmpty(key))
            DataSource.Bind<AbilityParam>(((Component) abilityListItemEvents1).get_gameObject(), MonoSingleton<GameManager>.Instance.GetAbilityParam(key));
          abilityListItemEvents1.IsEnableSkillChange = true;
          JobData job;
          int rank;
          if (this.GetAbilitySource(data1.AbilityID, out job, out rank))
            DataSource.Bind<AbilityUnlockInfo>(((Component) abilityListItemEvents1).get_gameObject(), new AbilityUnlockInfo()
            {
              JobName = job.Name,
              Rank = rank
            });
          abilityListItemEvents1.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
          abilityListItemEvents1.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
          abilityListItemEvents1.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
          abilityListItemEvents1.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
        }
        else
        {
          AbilityParam data2 = new AbilityParam();
          data2.slot = JobData.ABILITY_SLOT_TYPES[index];
          abilityListItemEvents1 = (UnitAbilityListItemEvents) Object.Instantiate<UnitAbilityListItemEvents>((M0) this.Item_Empty);
          DataSource.Bind<AbilityParam>(((Component) abilityListItemEvents1).get_gameObject(), data2);
        }
        ((Component) abilityListItemEvents1).get_transform().SetParent(transform, false);
        ((Component) abilityListItemEvents1).get_gameObject().SetActive(true);
        if (!flag)
          abilityListItemEvents1.OnSelect = new ListItemEvents.ListItemEvent(this._OnSlotSelect);
        this.mItems.Add(abilityListItemEvents1);
      }
    }

    private void _OnSlotSelect(GameObject go)
    {
      for (int slotIndex = 0; slotIndex < this.mItems.Count; ++slotIndex)
      {
        if (Object.op_Equality((Object) ((Component) this.mItems[slotIndex]).get_gameObject(), (Object) go))
        {
          if (this.OnSlotSelect == null)
            break;
          this.OnSlotSelect(slotIndex);
          break;
        }
      }
    }

    private GameObject GetItemRoot(GameObject go)
    {
      return go;
    }

    private void _OnAbilitySelect(GameObject go)
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(go, (AbilityData) null);
      go = this.GetItemRoot(go);
      if (this.OnAbilitySelect == null)
        return;
      this.OnAbilitySelect(dataOfClass, go);
    }

    private void _OnAbilityRankUp(GameObject go)
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(go, (AbilityData) null);
      go = this.GetItemRoot(go);
      if (this.OnAbilityRankUp != null)
        this.OnAbilityRankUp(dataOfClass, go);
      GameParameter.UpdateAll(go);
    }

    private void _OnAbilityDetail(GameObject go)
    {
      GlobalVars.SelectedAbilityID.Set(string.Empty);
      GlobalVars.SelectedAbilityUniqueID.Set(0L);
      AbilityData dataOfClass1 = DataSource.FindDataOfClass<AbilityData>(go, (AbilityData) null);
      if (dataOfClass1 != null)
      {
        GlobalVars.SelectedAbilityID.Set(dataOfClass1.Param.iname);
        GlobalVars.SelectedAbilityUniqueID.Set(dataOfClass1.UniqueID);
      }
      else
      {
        AbilityParam dataOfClass2 = DataSource.FindDataOfClass<AbilityParam>(go, (AbilityParam) null);
        GlobalVars.SelectedAbilityID.Set(dataOfClass2 == null ? (string) null : dataOfClass2.iname);
      }
      if (string.IsNullOrEmpty((string) GlobalVars.SelectedAbilityID))
        return;
      EventSystem current = EventSystem.get_current();
      if (Object.op_Inequality((Object) current, (Object) null))
      {
        ((Behaviour) current).set_enabled(false);
        ((Behaviour) current).set_enabled(true);
      }
      GameSettings instance = GameSettings.Instance;
      if (!string.IsNullOrEmpty(instance.Dialog_AbilityDetail))
      {
        GameObject gameObject = AssetManager.Load<GameObject>(instance.Dialog_AbilityDetail);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
          Object.Instantiate<GameObject>((M0) gameObject);
      }
      bool flag = false;
      if (Object.op_Implicit((Object) go))
      {
        ListItemEvents component = (ListItemEvents) go.GetComponent<ListItemEvents>();
        if (Object.op_Implicit((Object) component))
          flag = component.IsEnableSkillChange;
      }
      AbilityDetailWindow.IsEnableSkillChange = flag;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void _OnRankUpBtnPress(GameObject go)
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(go, (AbilityData) null);
      go = this.GetItemRoot(go);
      if (this.OnRankUpBtnPress != null)
        this.OnRankUpBtnPress(dataOfClass, go);
      GameParameter.UpdateAll(go);
    }

    private void _OnRankUpBtnUp(GameObject go)
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(go, (AbilityData) null);
      go = this.GetItemRoot(go);
      if (this.OnAbilityRankUpExec != null)
        this.OnAbilityRankUpExec(dataOfClass, go);
      GameParameter.UpdateAll(go);
    }

    private bool GetAbilitySource(string abilityID, out JobData job, out int rank)
    {
      for (int index = 0; index < this.Unit.Jobs.Length; ++index)
      {
        rank = this.Unit.Jobs[index].Param.FindRankOfAbility(abilityID);
        if (rank != -1)
        {
          job = this.Unit.Jobs[index];
          return true;
        }
      }
      job = (JobData) null;
      rank = -1;
      return false;
    }

    public void DisplaySlotType(EAbilitySlot slotType, bool hideEquipped = false)
    {
      this.mLastDisplayMode = UnitAbilityList.RefreshTypes.DisplayAll;
      this.mLastDisplaySlot = slotType;
      for (int index = 0; index < this.mItems.Count; ++index)
        Object.Destroy((Object) ((Component) this.mItems[index]).get_gameObject());
      this.mItems.Clear();
      if (this.Unit == null)
        this.Unit = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (this.Unit == null)
        return;
      List<AbilityData> learnedAbilities = this.Unit.GetAllLearnedAbilities();
      Transform transform1 = ((Component) this).get_transform();
      bool flag1 = false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if ((instance.Player.TutorialFlags & 1L) == 0L && instance.GetNextTutorialStep() == "ShowAbilityTab")
      {
        instance.CompleteTutorialStep();
        if (instance.GetNextTutorialStep() == "ShowAbilityLvUp")
          flag1 = true;
      }
      if (Object.op_Inequality((Object) this.Item_Normal, (Object) null))
      {
        for (int index1 = 0; index1 < learnedAbilities.Count; ++index1)
        {
          AbilityData data = learnedAbilities[index1];
          if ((slotType == ~EAbilitySlot.Action || slotType == data.SlotType) && (this.ShowFixedAbilities || !data.Param.is_fixed) && (this.ShowMasterAbilities || !((string) this.Unit.UnitParam.ability == data.AbilityID) && !this.Unit.IsQuestClearUnlocked(data.Param.iname, QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility)))
          {
            if (hideEquipped)
            {
              bool flag2 = false;
              for (int index2 = 0; index2 < this.Unit.CurrentJob.AbilitySlots.Length; ++index2)
              {
                if (this.Unit.CurrentJob.AbilitySlots[index2] == data.UniqueID)
                {
                  flag2 = true;
                  break;
                }
              }
              if (flag2)
                continue;
            }
            UnitAbilityListItemEvents abilityListItemEvents = (UnitAbilityListItemEvents) Object.Instantiate<UnitAbilityListItemEvents>((M0) this.Item_Normal);
            this.mItems.Add(abilityListItemEvents);
            DataSource.Bind<AbilityData>(((Component) abilityListItemEvents).get_gameObject(), data);
            abilityListItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
            abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
            ((Component) abilityListItemEvents).get_transform().SetParent(transform1, false);
            ((Component) abilityListItemEvents).get_gameObject().SetActive(true);
            JobData job;
            int rank;
            if (this.GetAbilitySource(data.AbilityID, out job, out rank))
              DataSource.Bind<AbilityUnlockInfo>(((Component) abilityListItemEvents).get_gameObject(), new AbilityUnlockInfo()
              {
                JobName = job.Name,
                Rank = rank
              });
            if (flag1 && index1 == 0)
            {
              SGHighlightObject.Instance().highlightedObject = ((Component) abilityListItemEvents.RankupButton).get_gameObject();
              SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_1.023", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.TopLeft, true, false, false);
            }
          }
        }
      }
      if (slotType != ~EAbilitySlot.Action && Object.op_Inequality((Object) this.Item_SlotMismatch, (Object) null))
      {
        for (int index = 0; index < learnedAbilities.Count; ++index)
        {
          AbilityData data = learnedAbilities[index];
          if (slotType != data.SlotType && (this.ShowFixedAbilities || !data.Param.is_fixed) && (this.ShowMasterAbilities || !((string) this.Unit.UnitParam.ability == data.AbilityID) && !this.Unit.IsQuestClearUnlocked(data.Param.iname, QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility)))
          {
            UnitAbilityListItemEvents abilityListItemEvents = (UnitAbilityListItemEvents) Object.Instantiate<UnitAbilityListItemEvents>((M0) this.Item_SlotMismatch);
            this.mItems.Add(abilityListItemEvents);
            DataSource.Bind<AbilityData>(((Component) abilityListItemEvents).get_gameObject(), data);
            abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
            ((Component) abilityListItemEvents).get_transform().SetParent(transform1, false);
            ((Component) abilityListItemEvents).get_gameObject().SetActive(true);
            JobData job;
            int rank;
            if (this.GetAbilitySource(data.AbilityID, out job, out rank))
              DataSource.Bind<AbilityUnlockInfo>(((Component) abilityListItemEvents).get_gameObject(), new AbilityUnlockInfo()
              {
                JobName = job.Name,
                Rank = rank
              });
          }
        }
      }
      if (Object.op_Inequality((Object) this.Item_Locked, (Object) null))
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        for (int index1 = 0; index1 < this.Unit.Jobs.Length; ++index1)
        {
          if (this.ShowLockedJobAbilities || this.Unit.Jobs[index1].Rank > 0)
          {
            RarityParam rarityParam = instanceDirect.GetRarityParam((int) this.Unit.UnitParam.raremax);
            for (int lv = this.Unit.Jobs[index1].Rank + 1; lv < JobParam.MAX_JOB_RANK; ++lv)
            {
              OString[] learningAbilitys = this.Unit.Jobs[index1].Param.GetLearningAbilitys(lv);
              if (learningAbilitys != null && (int) rarityParam.UnitJobLvCap >= lv)
              {
                for (int index2 = 0; index2 < learningAbilitys.Length; ++index2)
                {
                  string key = (string) learningAbilitys[index2];
                  if (!string.IsNullOrEmpty(key))
                  {
                    AbilityParam abilityParam = instanceDirect.GetAbilityParam(key);
                    if (this.ShowFixedAbilities || !abilityParam.is_fixed)
                    {
                      UnitAbilityListItemEvents abilityListItemEvents = (UnitAbilityListItemEvents) Object.Instantiate<UnitAbilityListItemEvents>((M0) this.Item_Locked);
                      this.mItems.Add(abilityListItemEvents);
                      DataSource.Bind<AbilityParam>(((Component) abilityListItemEvents).get_gameObject(), abilityParam);
                      abilityListItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
                      abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
                      abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
                      abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
                      abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
                      ((Component) abilityListItemEvents).get_transform().SetParent(transform1, false);
                      ((Component) abilityListItemEvents).get_gameObject().SetActive(true);
                      DataSource.Bind<AbilityUnlockInfo>(((Component) abilityListItemEvents).get_gameObject(), new AbilityUnlockInfo()
                      {
                        JobName = this.Unit.Jobs[index1].Name,
                        Rank = lv
                      });
                    }
                  }
                }
              }
            }
          }
        }
        if (this.ShowMasterAbilities && !string.IsNullOrEmpty((string) this.Unit.UnitParam.ability) && this.Unit.LearnAbilitys.Find((Predicate<AbilityData>) (p => p.AbilityID == (string) this.Unit.UnitParam.ability)) == null)
        {
          AbilityParam abilityParam = instanceDirect.GetAbilityParam((string) this.Unit.UnitParam.ability);
          UnitAbilityListItemEvents abilityListItemEvents = (UnitAbilityListItemEvents) Object.Instantiate<UnitAbilityListItemEvents>((M0) this.Item_Locked);
          this.mItems.Add(abilityListItemEvents);
          DataSource.Bind<AbilityParam>(((Component) abilityListItemEvents).get_gameObject(), abilityParam);
          abilityListItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
          abilityListItemEvents.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
          abilityListItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
          abilityListItemEvents.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
          abilityListItemEvents.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
          ((Component) abilityListItemEvents).get_transform().SetParent(transform1, false);
          ((Component) abilityListItemEvents).get_gameObject().SetActive(true);
        }
      }
      if (Object.op_Inequality((Object) this.ScrollParent, (Object) null))
      {
        this.mDecelerationRate = this.ScrollParent.get_decelerationRate();
        this.ScrollParent.set_decelerationRate(0.0f);
      }
      RectTransform transform2 = ((Component) this).get_transform() as RectTransform;
      transform2.set_anchoredPosition(new Vector2((float) transform2.get_anchoredPosition().x, 0.0f));
      this.StartCoroutine(this.RefreshScrollRect());
    }

    [DebuggerHidden]
    private IEnumerator RefreshScrollRect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitAbilityList.\u003CRefreshScrollRect\u003Ec__IteratorD9() { \u003C\u003Ef__this = this };
    }

    public void UpdateItem(AbilityData ability)
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (DataSource.FindDataOfClass<AbilityData>(((Component) this.mItems[index]).get_gameObject(), (AbilityData) null) == ability)
          GameParameter.UpdateAll(((Component) this.mItems[index]).get_gameObject());
      }
    }

    public bool IsEmpty
    {
      get
      {
        return this.mItems.Count == 0;
      }
    }

    public void UpdateValue()
    {
      if (!this.AutoRefresh)
        return;
      if (this.mLastDisplayMode == UnitAbilityList.RefreshTypes.DisplayAll)
      {
        this.DisplaySlotType(this.mLastDisplaySlot, false);
      }
      else
      {
        if (this.mLastDisplayMode != UnitAbilityList.RefreshTypes.DisplaySlots)
          return;
        this.DisplaySlots();
      }
    }

    public enum RefreshTypes
    {
      None,
      DisplayAll,
      DisplaySlots,
    }

    public delegate void AbilityEvent(AbilityData ability, GameObject itemGO);

    public delegate void AbilitySlotEvent(int slotIndex);
  }
}
