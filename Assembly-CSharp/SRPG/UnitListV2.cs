// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListV2
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(999, "TutorialShowUnit", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(1, "Unit Selected", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(100, "Refresh", FlowNode.PinTypes.Input, 100)]
  [AddComponentMenu("UI/リスト/ユニット")]
  [FlowNode.Pin(2, "Unit Unlocked", FlowNode.PinTypes.Output, 2)]
  public class UnitListV2 : SRPG_FixedList, IFlowInterface, ISortableList
  {
    public bool IncludeShujinko = true;
    private UnitListV2.ItemTypes mLastItemType = ~UnitListV2.ItemTypes.PlayerUnits;
    public UnitListV2.ItemTypes ItemType;
    public UnitListV2.UnitSelectEvent OnUnitSelect;
    public Transform ItemLayoutParent;
    public GameObject ItemTemplate;
    public GameObject PieceTemplate;
    public Pulldown SortFilter;
    public GameObject UnitSortFilterButton;
    public GameObject AscendingIcon;
    public GameObject DescendingIcon;
    public List<Toggle> UnitToggle;
    public bool IsSorting;
    public bool IsEnhanceEquipment;
    public GameObject UnitBadge;
    public GameObject UnitUnlockBadge;
    private bool mPrevUnitBadgeState;
    private bool mPrevUnlockBadgeState;
    protected GameUtility.UnitSortModes mUnitSortMode;
    protected string[] mUnitFilter;
    protected bool mReverse;
    public GameObject NoMatchingUnit;
    public GameObject SortMode;
    public Text SortModeCaption;
    protected int[] mSortValues;
    private long mSelectUnitUniqueID;

    private void SGActivated(int pinID)
    {
      if (pinID != 999 || !this.HasStarted)
        return;
      using (List<GameObject>.Enumerator enumerator = this.mItems.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          if (current.GetActive() && ((DataSource) current.GetComponent<DataSource>()).FindDataOfClass<UnitData>(new UnitData()).UnitID == "UN_V2_LOGI")
          {
            SGHighlightObject.Instance().highlightedObject = current.get_gameObject();
            SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_1.017", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.BottomLeft, true, false, false);
          }
        }
      }
    }

    protected virtual void Awake()
    {
      base.Awake();
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.PieceTemplate, (Object) null) && this.PieceTemplate.get_activeInHierarchy())
        this.PieceTemplate.SetActive(false);
      if (this.UnitToggle == null)
        return;
      for (int index = 0; index < this.UnitToggle.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitListV2.\u003CAwake\u003Ec__AnonStorey1AD awakeCAnonStorey1Ad = new UnitListV2.\u003CAwake\u003Ec__AnonStorey1AD();
        // ISSUE: reference to a compiler-generated field
        awakeCAnonStorey1Ad.\u003C\u003Ef__this = this;
        if (!Object.op_Equality((Object) this.UnitToggle[index], (Object) null))
        {
          // ISSUE: reference to a compiler-generated field
          awakeCAnonStorey1Ad.index = index;
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.UnitToggle[index].onValueChanged).AddListener(new UnityAction<bool>((object) awakeCAnonStorey1Ad, __methodptr(\u003C\u003Em__14A)));
        }
      }
    }

    public override RectTransform ListParent
    {
      get
      {
        if (Object.op_Inequality((Object) this.ItemLayoutParent, (Object) null))
          return (RectTransform) ((Component) this.ItemLayoutParent).GetComponent<RectTransform>();
        return (RectTransform) null;
      }
    }

    protected override void Start()
    {
      base.Start();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      this.mUnitSortMode = !PlayerPrefs.HasKey("UnitSortMode") ? GameUtility.UnitSortModes.Time : (GameUtility.UnitSortModes) PlayerPrefs.GetInt("UnitSortMode");
      if (Object.op_Inequality((Object) this.SortFilter, (Object) null))
      {
        GameSettings instance = GameSettings.Instance;
        for (int index = 0; index < instance.UnitSort_Modes.Length; ++index)
          this.SortFilter.AddItem(LocalizedText.Get("sys.SORT_" + instance.UnitSort_Modes[index].Mode.ToString().ToUpper()), index);
        this.SortFilter.Selection = Math.Max(Array.FindIndex<GameUtility.UnitSortModes>(GameUtility.UnitSortMenuItems, (Predicate<GameUtility.UnitSortModes>) (p => p == this.mUnitSortMode)), 0);
        this.SortFilter.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnSortModeChange);
      }
      if (Object.op_Inequality((Object) this.UnitBadge, (Object) null))
      {
        bool flag = MonoSingleton<GameManager>.Instance.CheckBadges(GameManager.BadgeTypes.Unit);
        this.UnitBadge.SetActive(flag);
        this.mPrevUnitBadgeState = flag;
      }
      if (Object.op_Inequality((Object) this.UnitUnlockBadge, (Object) null))
      {
        bool flag = MonoSingleton<GameManager>.Instance.CheckBadges(GameManager.BadgeTypes.UnitUnlock);
        this.UnitUnlockBadge.SetActive(flag);
        this.mPrevUnlockBadgeState = flag;
      }
      this.RefreshData();
    }

    protected override void Update()
    {
      base.Update();
      if (!Object.op_Inequality((Object) this.UnitBadge, (Object) null) && !Object.op_Inequality((Object) this.UnitUnlockBadge, (Object) null))
        return;
      bool flag1 = false;
      if (!MonoSingleton<GameManager>.Instance.CheckBusyBadges(GameManager.BadgeTypes.Unit) && !MonoSingleton<GameManager>.Instance.CheckBusyBadges(GameManager.BadgeTypes.UnitUnlock))
      {
        if (Object.op_Inequality((Object) this.UnitBadge, (Object) null))
        {
          bool flag2 = MonoSingleton<GameManager>.Instance.CheckBadges(GameManager.BadgeTypes.Unit);
          this.UnitBadge.SetActive(flag2);
          if (this.mPrevUnitBadgeState != flag2)
            flag1 = true;
          this.mPrevUnitBadgeState = flag2;
        }
        if (Object.op_Inequality((Object) this.UnitUnlockBadge, (Object) null))
        {
          bool flag2 = MonoSingleton<GameManager>.Instance.CheckBadges(GameManager.BadgeTypes.UnitUnlock);
          this.UnitUnlockBadge.SetActive(flag2);
          if (this.mPrevUnlockBadgeState != flag2)
            flag1 = true;
          this.mPrevUnlockBadgeState = flag2;
        }
      }
      if (!flag1)
        return;
      this.RefreshData();
    }

    public void Activated(int pinID)
    {
      this.SGActivated(pinID);
      if (pinID != 100 || !this.HasStarted)
        return;
      this.RefreshData();
    }

    private void OnSortModeChange(int index)
    {
      this.mUnitSortMode = GameSettings.Instance.UnitSort_Modes[index].Mode;
      PlayerPrefs.SetInt("UnitSortMode", (int) this.mUnitSortMode);
      this.RefreshData();
    }

    public void RefreshData()
    {
      bool clear = false;
      if (this.mLastItemType != this.ItemType)
      {
        this.mLastItemType = this.ItemType;
        this.ClearItems();
        clear = true;
      }
      switch (this.ItemType)
      {
        case UnitListV2.ItemTypes.PlayerUnits:
          this.RefreshUnit(clear);
          break;
        case UnitListV2.ItemTypes.PieceUnits:
          this.RefreshPieceUnit(clear);
          break;
      }
    }

    protected override GameObject CreateItem()
    {
      if (this.ItemType == UnitListV2.ItemTypes.PlayerUnits)
        return (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
      return (GameObject) Object.Instantiate<GameObject>((M0) this.PieceTemplate);
    }

    protected virtual void RefreshUnit(bool clear)
    {
      List<UnitData> units1 = MonoSingleton<GameManager>.Instance.Player.Units;
      List<UnitData> unitDataList = new List<UnitData>();
      for (int index = 0; index < units1.Count; ++index)
      {
        UnitData unitData = units1[index];
        if (this.IncludeShujinko || (int) unitData.UnitParam.hero == 0)
          unitDataList.Add(unitData);
      }
      List<UnitData> units2 = new List<UnitData>((IEnumerable<UnitData>) unitDataList);
      if (this.mUnitSortMode != GameUtility.UnitSortModes.Time)
        GameUtility.SortUnits(units2, this.mUnitSortMode, false, out this.mSortValues, true);
      else
        this.mSortValues = (int[]) null;
      int count = units2.Count;
      List<int> sortValues = (List<int>) null;
      if (this.mSortValues != null && this.mSortValues.Length > 0)
        sortValues = new List<int>((IEnumerable<int>) this.mSortValues);
      UnitListV2.FilterUnits(units2, sortValues, this.mUnitFilter);
      if (sortValues != null)
        this.mSortValues = sortValues.ToArray();
      if (Object.op_Inequality((Object) this.NoMatchingUnit, (Object) null))
        this.NoMatchingUnit.SetActive(count > 0 && units2.Count <= 0);
      if (this.mReverse)
      {
        units2.Reverse();
        if (this.mSortValues != null)
          Array.Reverse((Array) this.mSortValues);
      }
      string s = FlowNode_Variable.Get("LAST_SELECTED_UNITID");
      if (!string.IsNullOrEmpty(s))
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitListV2.\u003CRefreshUnit\u003Ec__AnonStorey1AE unitCAnonStorey1Ae = new UnitListV2.\u003CRefreshUnit\u003Ec__AnonStorey1AE();
        // ISSUE: reference to a compiler-generated field
        unitCAnonStorey1Ae.lastselected_uniqueId = long.Parse(s);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (unitCAnonStorey1Ae.lastselected_uniqueId > 0L && this.mSelectUnitUniqueID > 0L && unitCAnonStorey1Ae.lastselected_uniqueId != this.mSelectUnitUniqueID)
        {
          // ISSUE: reference to a compiler-generated method
          int index = units2.FindIndex(new Predicate<UnitData>(unitCAnonStorey1Ae.\u003C\u003Em__14C));
          if (index >= 0)
            this.SetPageIndex(index / this.CellCount, false);
        }
        FlowNode_Variable.Set("LAST_SELECTED_UNITID", string.Empty);
        this.mSelectUnitUniqueID = -1L;
      }
      this.SetData((object[]) units2.ToArray(), typeof (UnitData));
    }

    private static bool GetValue(string str, string key, ref string value)
    {
      if (!str.StartsWith(key))
        return false;
      value = str.Substring(key.Length);
      return true;
    }

    protected override void LateUpdate()
    {
    }

    public static void FilterUnits(List<UnitData> units, List<int> sortValues, string[] filter)
    {
      if (filter == null)
        return;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      List<string> stringList = new List<string>();
      string s = (string) null;
      for (int index = 0; index < filter.Length; ++index)
      {
        if (UnitListV2.GetValue(filter[index], "RARE:", ref s))
        {
          int result;
          if (int.TryParse(s, out result))
            num1 |= 1 << result;
        }
        else if (UnitListV2.GetValue(filter[index], "ELEM:", ref s))
        {
          try
          {
            EElement eelement = (EElement) Enum.Parse(typeof (EElement), s, true);
            num2 |= 1 << (int) (eelement & (EElement) 31 & (EElement) 31);
          }
          catch
          {
            if (GameUtility.IsDebugBuild)
              Debug.LogError((object) ("Unknown element type: " + s));
          }
        }
        else if (UnitListV2.GetValue(filter[index], "WEAPON:", ref s))
        {
          try
          {
            AttackDetailTypes attackDetailTypes = (AttackDetailTypes) Enum.Parse(typeof (AttackDetailTypes), s, true);
            num3 |= 1 << (int) (attackDetailTypes & (AttackDetailTypes) 31 & (AttackDetailTypes) 31);
          }
          catch
          {
            if (GameUtility.IsDebugBuild)
              Debug.LogError((object) ("Unknown weapon type: " + s));
          }
        }
        else if (UnitListV2.GetValue(filter[index], "BIRTH:", ref s))
          stringList.Add(s);
      }
      for (int index = units.Count - 1; index >= 0; --index)
      {
        UnitData unit = units[index];
        if ((1 << unit.Rarity & num1) == 0 || (1 << (int) (unit.Element & (EElement) 31) & num2) == 0 || (unit.CurrentJob.GetAttackSkill() == null || (1 << (int) (unit.CurrentJob.GetAttackSkill().AttackDetailType & (AttackDetailTypes) 31) & num3) == 0) || (string.IsNullOrEmpty((string) unit.UnitParam.birth) || !stringList.Contains(unit.UnitParam.birth.ToString())))
        {
          units.RemoveAt(index);
          if (sortValues != null)
            sortValues.RemoveAt(index);
        }
      }
    }

    public static void FilterUnitsRevert(List<UnitData> units, List<int> sortValues, string[] filter)
    {
      if (filter == null)
        return;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      List<string> stringList = new List<string>();
      string s = (string) null;
      for (int index = 0; index < filter.Length; ++index)
      {
        if (UnitListV2.GetValue(filter[index], "RARE:", ref s))
        {
          int result;
          if (int.TryParse(s, out result))
            num1 |= 1 << result;
        }
        else if (UnitListV2.GetValue(filter[index], "ELEM:", ref s))
        {
          try
          {
            EElement eelement = (EElement) Enum.Parse(typeof (EElement), s, true);
            num2 |= 1 << (int) (eelement & (EElement) 31 & (EElement) 31);
          }
          catch
          {
            if (GameUtility.IsDebugBuild)
              Debug.LogError((object) ("Unknown element type: " + s));
          }
        }
        else if (UnitListV2.GetValue(filter[index], "WEAPON:", ref s))
        {
          try
          {
            AttackDetailTypes attackDetailTypes = (AttackDetailTypes) Enum.Parse(typeof (AttackDetailTypes), s, true);
            num3 |= 1 << (int) (attackDetailTypes & (AttackDetailTypes) 31 & (AttackDetailTypes) 31);
          }
          catch
          {
            if (GameUtility.IsDebugBuild)
              Debug.LogError((object) ("Unknown weapon type: " + s));
          }
        }
        else if (UnitListV2.GetValue(filter[index], "BIRTH:", ref s))
          stringList.Add(s);
      }
      for (int index = units.Count - 1; index >= 0; --index)
      {
        UnitData unit = units[index];
        if ((1 << unit.Rarity & num1) != 0 || (1 << (int) (unit.Element & (EElement) 31) & num2) != 0 || (unit.CurrentJob.GetAttackSkill() == null || (1 << (int) (unit.CurrentJob.GetAttackSkill().AttackDetailType & (AttackDetailTypes) 31) & num3) != 0) || (string.IsNullOrEmpty((string) unit.UnitParam.birth) || stringList.Contains(unit.UnitParam.birth.ToString())))
        {
          units.RemoveAt(index);
          if (sortValues != null)
            sortValues.RemoveAt(index);
        }
      }
    }

    protected override void OnUpdateItem(GameObject go, int index)
    {
      if (this.ItemType != UnitListV2.ItemTypes.PlayerUnits)
        return;
      UnitData unitData = this.Data[index] as UnitData;
      if (this.IsEnhanceEquipment)
      {
        Selectable[] componentsInChildren = (Selectable[]) go.GetComponentsInChildren<Selectable>(true);
        if (componentsInChildren.Length > 0)
        {
          bool flag = unitData.CheckEnableEnhanceEquipment();
          if (flag != componentsInChildren[0].get_interactable())
            componentsInChildren[0].set_interactable(flag);
        }
      }
      UnitListItemEvents component1 = (UnitListItemEvents) go.GetComponent<UnitListItemEvents>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        component1.Refresh();
      UnitIcon component2 = (UnitIcon) go.GetComponent<UnitIcon>();
      if (!Object.op_Inequality((Object) component2, (Object) null))
        return;
      if (this.mSortValues != null)
        component2.SetSortValue(this.mUnitSortMode, this.mSortValues[index]);
      else
        component2.ClearSortValue();
    }

    private void RefreshPieceUnit(bool clear)
    {
      if (Object.op_Equality((Object) this.PieceTemplate, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<ItemParam> items = instance.MasterParam.Items;
      UnitParam[] allUnits = instance.MasterParam.GetAllUnits();
      PlayerData player = instance.Player;
      List<UnitParam> unitParamList = new List<UnitParam>(this.DataCount);
      for (int index = 0; index < items.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitListV2.\u003CRefreshPieceUnit\u003Ec__AnonStorey1AF unitCAnonStorey1Af = new UnitListV2.\u003CRefreshPieceUnit\u003Ec__AnonStorey1AF();
        // ISSUE: reference to a compiler-generated field
        unitCAnonStorey1Af.item = items[index];
        // ISSUE: reference to a compiler-generated field
        if (unitCAnonStorey1Af.item.type == EItemType.UnitPiece)
        {
          // ISSUE: reference to a compiler-generated method
          UnitParam unitParam = Array.Find<UnitParam>(allUnits, new Predicate<UnitParam>(unitCAnonStorey1Af.\u003C\u003Em__14D));
          if (unitParam != null && unitParam.summon && (!unitParamList.Contains(unitParam) && player.FindUnitDataByUnitID(unitParam.iname) == null) && unitParam.CheckAvailable(TimeManager.ServerTime))
            unitParamList.Add(unitParam);
        }
      }
      UnitParam[] array = unitParamList.ToArray();
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < array.Length; ++index2)
        {
          if ((int) array[index1].raremax < (int) array[index2].raremax)
          {
            UnitParam unitParam = array[index1];
            array[index1] = array[index2];
            array[index2] = unitParam;
          }
        }
      }
      if (Object.op_Inequality((Object) this.NoMatchingUnit, (Object) null))
        this.NoMatchingUnit.SetActive(false);
      this.mSortValues = (int[]) null;
      this.SetData((object[]) array, typeof (UnitParam));
    }

    protected override void OnItemSelect(GameObject go)
    {
      if (this.ItemType == UnitListV2.ItemTypes.PlayerUnits)
        this.OnSelectUnit(go);
      else
        this.OnSelectPieceUnit(go);
    }

    private void OnSelectUnit(GameObject go)
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(go, (UnitData) null);
      if (dataOfClass == null)
        return;
      this.mSelectUnitUniqueID = dataOfClass.UniqueID;
      if (this.OnUnitSelect != null)
      {
        this.OnUnitSelect(dataOfClass.UniqueID);
      }
      else
      {
        GlobalVars.SelectedUnitUniqueID.Set(dataOfClass.UniqueID);
        GlobalVars.SelectedUnitJobIndex.Set(dataOfClass.JobIndex);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      }
    }

    private void OnSelectPieceUnit(GameObject go)
    {
      UnitParam dataOfClass = DataSource.FindDataOfClass<UnitParam>(go, (UnitParam) null);
      if (dataOfClass == null)
        return;
      GlobalVars.UnlockUnitID = dataOfClass.iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }

    private void OnChangedToggle(int index)
    {
      this.ItemType = index != 0 ? UnitListV2.ItemTypes.PieceUnits : UnitListV2.ItemTypes.PlayerUnits;
      if (Object.op_Inequality((Object) this.SortFilter, (Object) null))
        ((Component) this.SortFilter).get_gameObject().SetActive(this.ItemType == UnitListV2.ItemTypes.PlayerUnits);
      if (Object.op_Inequality((Object) this.UnitSortFilterButton, (Object) null))
        this.UnitSortFilterButton.SetActive(this.ItemType == UnitListV2.ItemTypes.PlayerUnits);
      if (Object.op_Inequality((Object) this.SortMode, (Object) null))
        this.SortMode.get_gameObject().SetActive(this.ItemType == UnitListV2.ItemTypes.PlayerUnits);
      this.Page = 0;
      this.RefreshData();
    }

    public void UpdateItem(UnitData unit)
    {
    }

    public void UpdateEquipment()
    {
    }

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      GameUtility.UnitSortModes unitSortModes = GameUtility.UnitSortModes.Time;
      try
      {
        if (!string.IsNullOrEmpty(method))
          unitSortModes = (GameUtility.UnitSortModes) Enum.Parse(typeof (GameUtility.UnitSortModes), method, true);
      }
      catch (Exception ex)
      {
        if (GameUtility.IsDebugBuild)
          DebugUtility.LogError("Unknown sort mode: " + method);
      }
      if (Object.op_Inequality((Object) this.AscendingIcon, (Object) null))
        this.AscendingIcon.SetActive(ascending);
      if (Object.op_Inequality((Object) this.DescendingIcon, (Object) null))
        this.DescendingIcon.SetActive(!ascending);
      if (unitSortModes == GameUtility.UnitSortModes.Time)
        ascending = !ascending;
      if (Object.op_Inequality((Object) this.SortModeCaption, (Object) null))
        this.SortModeCaption.set_text(LocalizedText.Get("sys.SORT_" + unitSortModes.ToString().ToUpper()));
      this.mReverse = ascending;
      this.mUnitSortMode = unitSortModes;
      this.mUnitFilter = filters;
      this.RefreshData();
    }

    public enum ItemTypes
    {
      PlayerUnits,
      PieceUnits,
    }

    public delegate void UnitSelectEvent(long uniqueID);
  }
}
