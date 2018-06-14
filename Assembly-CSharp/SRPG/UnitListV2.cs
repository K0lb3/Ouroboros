// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListV2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(999, "TutorialShowUnit", FlowNode.PinTypes.Input, 100)]
  [AddComponentMenu("UI/リスト/ユニット")]
  [FlowNode.Pin(100, "Refresh", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(1, "Unit Selected", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Unit Unlocked", FlowNode.PinTypes.Output, 2)]
  public class UnitListV2 : SRPG_FixedList, IFlowInterface, ISortableList
  {
    public static readonly string UNIT_SORT_PATH = "UI/UnitPickerSort";
    public static readonly string UNIT_FILTER_PATH = "UI/UnitPickerFilter";
    public bool IncludeShujinko = true;
    private UnitListV2.ItemTypes mLastItemType = ~UnitListV2.ItemTypes.PlayerUnits;
    private List<UnitData> mUnitDatas = new List<UnitData>();
    public string MenuID = "UNITLIST";
    public RectTransform[] ChosenUnitBadges = new RectTransform[0];
    public RectTransform[] ChosenUnitBadgesTower = new RectTransform[0];
    private List<GameObject> mExtraItemList = new List<GameObject>();
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
    private int mSelectTabIndex;
    public GameObject TabParentObject;
    public SRPG_Button CloseButton;
    public GameObject TitleObject;
    public SRPG_Button SortButton;
    public SRPG_Button FilterButton;
    public SRPG_Button PieceButton;
    public GameObject HelpButton;
    public GameObject SelectTemplate;
    public SRPG_Button RemoveTemplate;
    public GameObject SelectTowerTemplate;
    public SRPG_Button RemoveTowerTemplate;
    public UnitPickerButtonChanger m_SortChanger;
    public UnitPickerButtonChanger m_FilterChanger;
    private GameObject mSortWindow;
    private UnitPickerSort mUnitPickerSort;
    private GameObject mFilterWindow;
    private UnitPickerFilter mUnitPickerFilter;
    private UnitListV2.ViewMode mSelectViewMode;
    public UnitListV2.ItemSelectEvents OnItemSelectAction;
    public UnitListV2.CommonEvents OnCloseEvent;
    public UnitListV2.CommonEvents OnRemoveEvent;
    private PartyEditData mCurrentParty;
    private QuestParam mCurrentQuest;
    private bool mUseQuestInfo;
    private int mSelectedSlotIndex;
    public Toggle SelectableToggle;
    public RectTransform UnitHilit;
    public RectTransform UnitHilitTower;
    private bool mIsHeroOnly;
    public GameObject mBackToHome;
    private bool mIsSetRefresh;

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

    public bool UseQuestInfo
    {
      get
      {
        return this.mUseQuestInfo;
      }
      set
      {
        this.mUseQuestInfo = value;
      }
    }

    public int SelectedSlotIndex
    {
      set
      {
        this.mSelectedSlotIndex = value;
      }
    }

    public bool IsHeroOnly
    {
      get
      {
        return this.mIsHeroOnly;
      }
      set
      {
        this.mIsHeroOnly = value;
      }
    }

    protected virtual void Awake()
    {
      base.Awake();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PieceTemplate, (UnityEngine.Object) null) && this.PieceTemplate.get_activeInHierarchy())
        this.PieceTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectTemplate, (UnityEngine.Object) null) && this.SelectTemplate.get_activeInHierarchy())
        this.SelectTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemoveTemplate, (UnityEngine.Object) null))
        ((Component) this.RemoveTemplate).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectTowerTemplate, (UnityEngine.Object) null) && this.SelectTowerTemplate.get_activeInHierarchy())
        this.SelectTowerTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemoveTowerTemplate, (UnityEngine.Object) null))
        ((Component) this.RemoveTowerTemplate).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitHilit, (UnityEngine.Object) null))
        ((Component) this.UnitHilit).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitHilitTower, (UnityEngine.Object) null))
        ((Component) this.UnitHilitTower).get_gameObject().SetActive(false);
      if (this.UnitToggle == null)
        return;
      for (int index = 0; index < this.UnitToggle.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitListV2.\u003CAwake\u003Ec__AnonStorey23D awakeCAnonStorey23D = new UnitListV2.\u003CAwake\u003Ec__AnonStorey23D();
        // ISSUE: reference to a compiler-generated field
        awakeCAnonStorey23D.\u003C\u003Ef__this = this;
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.UnitToggle[index], (UnityEngine.Object) null))
        {
          // ISSUE: reference to a compiler-generated field
          awakeCAnonStorey23D.index = index;
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.UnitToggle[index].onValueChanged).AddListener(new UnityAction<bool>((object) awakeCAnonStorey23D, __methodptr(\u003C\u003Em__1B4)));
        }
      }
    }

    public override RectTransform ListParent
    {
      get
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemLayoutParent, (UnityEngine.Object) null))
          return (RectTransform) ((Component) this.ItemLayoutParent).GetComponent<RectTransform>();
        return (RectTransform) null;
      }
    }

    protected override void Start()
    {
      base.Start();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PieceButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.PieceButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(RefreshViewPieceList)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SortButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.SortButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnUnitSortOpen)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FilterButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.FilterButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnUnitFilterOpen)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseButton, (UnityEngine.Object) null))
        this.CloseButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnCloseButton));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemoveTemplate, (UnityEngine.Object) null))
        this.RemoveTemplate.AddListener(new SRPG_Button.ButtonClickEvent(this.OnRemoveUnitSelect));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemoveTowerTemplate, (UnityEngine.Object) null))
        this.RemoveTowerTemplate.AddListener(new SRPG_Button.ButtonClickEvent(this.OnRemoveUnitSelect));
      if (!string.IsNullOrEmpty(UnitListV2.UNIT_SORT_PATH))
      {
        GameObject gameObject1 = AssetManager.Load<GameObject>(UnitListV2.UNIT_SORT_PATH);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        {
          GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject1);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
          {
            this.mSortWindow = gameObject2;
            UnitPickerSort component = (UnitPickerSort) gameObject2.GetComponent<UnitPickerSort>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              component.OnAccept = new UnitPickerSort.SortEvent(this.SetupSortMethod);
              component.MenuID = this.MenuID;
              this.mUnitPickerSort = component;
              if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SortChanger, (UnityEngine.Object) null))
                ;
            }
          }
        }
      }
      if (!string.IsNullOrEmpty(UnitListV2.UNIT_FILTER_PATH))
      {
        GameObject gameObject1 = AssetManager.Load<GameObject>(UnitListV2.UNIT_FILTER_PATH);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        {
          GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject1);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
          {
            this.mFilterWindow = gameObject2;
            UnitPickerFilter component = (UnitPickerFilter) gameObject2.GetComponent<UnitPickerFilter>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              component.OnAccept = new UnitPickerFilter.FilterEvent(this.SetupFilter);
              component.MenuID = this.MenuID;
              this.mUnitPickerFilter = component;
              if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_FilterChanger, (UnityEngine.Object) null))
                ;
            }
          }
        }
      }
      if (this.ChosenUnitBadges != null && this.ChosenUnitBadges.Length > 0)
      {
        for (int index = 0; index < this.ChosenUnitBadges.Length; ++index)
        {
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.ChosenUnitBadges[index], (UnityEngine.Object) null))
          {
            this.MoveToOrigin(((Component) this.ChosenUnitBadges[index]).get_gameObject());
            ((Component) this.ChosenUnitBadges[index]).get_gameObject().SetActive(false);
          }
        }
      }
      if (this.ChosenUnitBadgesTower != null && this.ChosenUnitBadgesTower.Length > 0)
      {
        for (int index = 0; index < this.ChosenUnitBadgesTower.Length; ++index)
        {
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.ChosenUnitBadgesTower[index], (UnityEngine.Object) null))
          {
            this.MoveToOrigin(((Component) this.ChosenUnitBadgesTower[index]).get_gameObject());
            ((Component) this.ChosenUnitBadgesTower[index]).get_gameObject().SetActive(false);
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectableToggle, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.SelectableToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnValueChanged)));
        ((Component) this.SelectableToggle).get_gameObject().SetActive(false);
      }
      GameUtility.UnitSortModes unitSortModes = GameUtility.UnitSortModes.Time;
      if (PlayerPrefsUtility.HasKey(this.MenuID))
      {
        string str = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        try
        {
          if (!string.IsNullOrEmpty(str))
            unitSortModes = (GameUtility.UnitSortModes) Enum.Parse(typeof (GameUtility.UnitSortModes), str, true);
        }
        catch (Exception ex)
        {
          DebugUtility.LogError("Unknown sort mode:" + str);
        }
      }
      this.mUnitSortMode = unitSortModes;
      bool ascending = false;
      if (PlayerPrefsUtility.HasKey(this.MenuID + "#"))
      {
        ascending = PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) != 0;
        this.mReverse = ascending;
      }
      if (this.mUnitSortMode == GameUtility.UnitSortModes.Time)
        this.mReverse = !ascending;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitPickerSort, (UnityEngine.Object) null))
        this.mUnitPickerSort.SetUp(this.mUnitSortMode.ToString().ToUpper(), ascending);
      string key = this.MenuID + "&";
      string[] filters1 = (string[]) null;
      if (PlayerPrefsUtility.HasKey(key))
      {
        string str = PlayerPrefsUtility.GetString(key, string.Empty);
        if (str != null)
        {
          if (str.IndexOf('|') != -1)
          {
            string[] filters2 = str.Split('|');
            this.mUnitPickerFilter.SetFilters(filters2, filters2 != null);
            this.mUnitPickerFilter.SaveState();
            string[] filters3 = this.mUnitPickerFilter.GetFilters(false);
            str = filters3 == null ? string.Empty : string.Join("&", filters3);
          }
          filters1 = str.Split('&');
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitPickerFilter, (UnityEngine.Object) null))
      {
        this.mUnitPickerFilter.SetFilters(filters1, true);
        this.mUnitPickerFilter.SaveState();
        this.mUnitFilter = UnitListV2.UpdateFilterElement(0, this.mUnitPickerFilter.GetFiltersAll());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SortFilter, (UnityEngine.Object) null))
      {
        GameSettings instance = GameSettings.Instance;
        for (int index = 0; index < instance.UnitSort_Modes.Length; ++index)
          this.SortFilter.AddItem(LocalizedText.Get("sys.SORT_" + instance.UnitSort_Modes[index].Mode.ToString().ToUpper()), index);
        this.SortFilter.Selection = Math.Max(Array.FindIndex<GameUtility.UnitSortModes>(GameUtility.UnitSortMenuItems, (Predicate<GameUtility.UnitSortModes>) (p => p == this.mUnitSortMode)), 0);
        this.SortFilter.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnSortModeChange);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitBadge, (UnityEngine.Object) null))
      {
        bool flag = MonoSingleton<GameManager>.Instance.CheckBadges(GameManager.BadgeTypes.Unit);
        this.UnitBadge.SetActive(flag);
        this.mPrevUnitBadgeState = flag;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitUnlockBadge, (UnityEngine.Object) null))
      {
        bool flag = MonoSingleton<GameManager>.Instance.CheckBadges(GameManager.BadgeTypes.UnitUnlock);
        this.UnitUnlockBadge.SetActive(flag);
        this.mPrevUnlockBadgeState = flag;
      }
      this.mSelectTabIndex = 0;
      this.UpdateViewMode(this.mSelectViewMode);
      this.RefreshData();
    }

    protected override void Update()
    {
      base.Update();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitBadge, (UnityEngine.Object) null) && !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitUnlockBadge, (UnityEngine.Object) null))
        return;
      bool flag1 = false;
      if (!MonoSingleton<GameManager>.Instance.CheckBusyBadges(GameManager.BadgeTypes.Unit) && !MonoSingleton<GameManager>.Instance.CheckBusyBadges(GameManager.BadgeTypes.UnitUnlock))
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitBadge, (UnityEngine.Object) null))
        {
          bool flag2 = MonoSingleton<GameManager>.Instance.CheckBadges(GameManager.BadgeTypes.Unit);
          this.UnitBadge.SetActive(flag2);
          if (this.mPrevUnitBadgeState != flag2)
            flag1 = true;
          this.mPrevUnitBadgeState = flag2;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitUnlockBadge, (UnityEngine.Object) null))
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

    protected virtual void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSortWindow, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mSortWindow);
        this.mSortWindow = (GameObject) null;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mFilterWindow, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mFilterWindow);
        this.mFilterWindow = (GameObject) null;
      }
      base.OnDestroy();
    }

    public void Activated(int pinID)
    {
      this.SGActivated(pinID);
      if (pinID != 100)
        return;
      if (this.HasStarted && this.mIsSetRefresh)
        this.RefreshData();
      else
        this.mIsSetRefresh = true;
    }

    private void OnSortModeChange(int index)
    {
      this.mUnitSortMode = GameSettings.Instance.UnitSort_Modes[index].Mode;
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.UNITLIST_UNIT_SORT_MODE, (int) this.mUnitSortMode, false);
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
        return (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate);
      return (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.PieceTemplate);
    }

    protected override GameObject CreateItem(int index)
    {
      switch ((UnitListV2.ViewMode) index)
      {
        case UnitListV2.ViewMode.UNITLIST:
          return (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate);
        case UnitListV2.ViewMode.SELECTLIST:
          return (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.SelectTemplate);
        case UnitListV2.ViewMode.TOWERSELECTLIST:
          return (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.SelectTowerTemplate);
        default:
          return (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.PieceTemplate);
      }
    }

    protected virtual void RefreshUnit(bool clear)
    {
      if (this.mSelectViewMode == UnitListV2.ViewMode.UNITLIST || this.mSelectViewMode == UnitListV2.ViewMode.PIECELIST)
      {
        bool mReverse = this.mReverse;
        List<UnitData> unitDataList1 = this.mUnitDatas == null || this.mUnitDatas.Count <= 0 ? MonoSingleton<GameManager>.Instance.Player.Units : new List<UnitData>((IEnumerable<UnitData>) this.mUnitDatas);
        List<UnitData> unitDataList2 = new List<UnitData>();
        for (int index = 0; index < unitDataList1.Count; ++index)
        {
          UnitData unitData = unitDataList1[index];
          if (this.IncludeShujinko || (int) unitData.UnitParam.hero == 0)
            unitDataList2.Add(unitData);
        }
        List<UnitData> units = new List<UnitData>((IEnumerable<UnitData>) unitDataList2);
        if (this.mUnitSortMode != GameUtility.UnitSortModes.Time)
          GameUtility.SortUnits(units, this.mUnitSortMode, this.mReverse, out this.mSortValues, true);
        else
          this.mSortValues = (int[]) null;
        int count = units.Count;
        List<int> sortValues = (List<int>) null;
        if (this.mSortValues != null && this.mSortValues.Length > 0)
          sortValues = new List<int>((IEnumerable<int>) this.mSortValues);
        UnitListV2.FilterUnits(units, sortValues, this.mUnitFilter);
        if (sortValues != null)
          this.mSortValues = sortValues.ToArray();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NoMatchingUnit, (UnityEngine.Object) null))
          this.NoMatchingUnit.SetActive(count > 0 && units.Count <= 0);
        if (mReverse)
        {
          units.Reverse();
          if (this.mSortValues != null)
            Array.Reverse((Array) this.mSortValues);
        }
        string s = FlowNode_Variable.Get("LAST_SELECTED_UNITID");
        if (!string.IsNullOrEmpty(s))
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          UnitListV2.\u003CRefreshUnit\u003Ec__AnonStorey23E unitCAnonStorey23E = new UnitListV2.\u003CRefreshUnit\u003Ec__AnonStorey23E();
          // ISSUE: reference to a compiler-generated field
          unitCAnonStorey23E.lastselected_uniqueId = long.Parse(s);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (unitCAnonStorey23E.lastselected_uniqueId > 0L && this.mSelectUnitUniqueID > 0L && unitCAnonStorey23E.lastselected_uniqueId != this.mSelectUnitUniqueID)
          {
            // ISSUE: reference to a compiler-generated method
            int index = units.FindIndex(new Predicate<UnitData>(unitCAnonStorey23E.\u003C\u003Em__1B6));
            if (index >= 0)
              this.SetPageIndex(index / this.CellCount, false);
          }
          FlowNode_Variable.Set("LAST_SELECTED_UNITID", string.Empty);
          this.mSelectUnitUniqueID = -1L;
        }
        this.SetData((object[]) units.ToArray(), typeof (UnitData));
      }
      else
      {
        List<UnitData> units = new List<UnitData>((IEnumerable<UnitData>) this.mUnitDatas);
        List<UnitData> unitDataList1 = new List<UnitData>();
        List<int> intList1 = new List<int>();
        bool mReverse = this.mReverse;
        if (this.mUnitSortMode != GameUtility.UnitSortModes.Time)
          GameUtility.SortUnits(units, this.mUnitSortMode, mReverse, out this.mSortValues, true);
        else
          this.mSortValues = (int[]) null;
        int num = 0;
        for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.mCurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
        {
          if (this.mCurrentParty.Units[mainmemberStart] != null)
            ++num;
        }
        List<int> intList2 = this.mSortValues == null || this.mSortValues.Length <= 0 ? (List<int>) null : new List<int>((IEnumerable<int>) this.mSortValues);
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitListV2.\u003CRefreshUnit\u003Ec__AnonStorey23F unitCAnonStorey23F = new UnitListV2.\u003CRefreshUnit\u003Ec__AnonStorey23F();
        // ISSUE: reference to a compiler-generated field
        unitCAnonStorey23F.\u003C\u003Ef__this = this;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        for (unitCAnonStorey23F.i = 0; unitCAnonStorey23F.i < this.mCurrentParty.PartyData.MAX_UNIT; ++unitCAnonStorey23F.i)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (this.mCurrentParty.Units[unitCAnonStorey23F.i] != null && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (unitCAnonStorey23F.i != 0 || num > 1) || this.mCurrentParty.Units[this.mSelectedSlotIndex] != null))
          {
            // ISSUE: reference to a compiler-generated method
            unitDataList1.Add(units.Find(new Predicate<UnitData>(unitCAnonStorey23F.\u003C\u003Em__1B7)));
            // ISSUE: reference to a compiler-generated field
            if (this.mUnitSortMode != GameUtility.UnitSortModes.Time && this.mCurrentParty.Units[unitCAnonStorey23F.i] != null)
            {
              // ISSUE: reference to a compiler-generated field
              intList1.Add(this.GetSortModeValue(this.mCurrentParty.Units[unitCAnonStorey23F.i], this.mUnitSortMode));
            }
            // ISSUE: reference to a compiler-generated method
            int index = units.FindIndex(new Predicate<UnitData>(unitCAnonStorey23F.\u003C\u003Em__1B8));
            if (index >= 0)
            {
              if (intList2 != null)
                intList2.RemoveAt(index);
              if (units != null)
                units.RemoveAt(index);
            }
          }
        }
        if (this.mSortValues != null && intList2 != null)
          this.mSortValues = intList2.ToArray();
        List<int> sortValues = (List<int>) null;
        if (this.mSortValues != null && this.mSortValues.Length > 0)
          sortValues = new List<int>((IEnumerable<int>) this.mSortValues);
        UnitListV2.FilterUnits(units, sortValues, this.mUnitFilter);
        if (sortValues != null)
          this.mSortValues = sortValues.ToArray();
        if (mReverse)
        {
          units.Reverse();
          if (this.mSortValues != null)
            Array.Reverse((Array) this.mSortValues);
        }
        List<UnitData> unitDataList2 = new List<UnitData>();
        if (this.SelectableToggle.get_isOn())
        {
          if (this.UseQuestInfo)
          {
            List<int> intList3 = new List<int>();
            for (int index = 0; index < units.Count; ++index)
            {
              string empty = string.Empty;
              if (this.mCurrentQuest.IsEntryQuestCondition(units[index], ref empty))
              {
                unitDataList2.Add(units[index]);
                if (this.mSortValues != null && this.mSortValues.Length > index)
                  intList3.Add(this.mSortValues[index]);
              }
            }
            if (intList3 != null && intList3.Count > 0)
              intList1.AddRange((IEnumerable<int>) intList3);
          }
        }
        else
        {
          unitDataList2.AddRange((IEnumerable<UnitData>) units);
          if (this.mSortValues != null)
            intList1.AddRange((IEnumerable<int>) this.mSortValues);
        }
        unitDataList1.AddRange((IEnumerable<UnitData>) unitDataList2);
        if (this.mSortValues != null && intList1 != null)
          this.mSortValues = intList1.ToArray();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NoMatchingUnit, (UnityEngine.Object) null))
          this.NoMatchingUnit.SetActive(unitDataList1.Count <= 0);
        this.SetData((object[]) unitDataList1.ToArray(), typeof (UnitData));
      }
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
      bool flag = false;
      int num3 = 0;
      List<string> stringList = new List<string>();
      string s = (string) null;
      for (int index = 0; index < filter.Length; ++index)
      {
        if (UnitListV2.GetValue(filter[index], "TAB:", ref s))
        {
          if (s == "Favorite")
            flag = true;
        }
        else if (UnitListV2.GetValue(filter[index], "RARE:", ref s))
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
        if ((1 << unit.Rarity & num1) == 0 || flag && !unit.IsFavorite || ((1 << (int) (unit.Element & (EElement) 31) & num2) == 0 || unit.CurrentJob.GetAttackSkill() == null || ((1 << (int) (unit.CurrentJob.GetAttackSkill().AttackDetailType & (AttackDetailTypes) 31) & num3) == 0 || string.IsNullOrEmpty((string) unit.UnitParam.birth))) || !stringList.Contains(unit.UnitParam.birth.ToString()))
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        component1.Refresh();
      UnitIcon component2 = (UnitIcon) go.GetComponent<UnitIcon>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        return;
      if (this.mSortValues != null)
      {
        if (this.mSelectViewMode == UnitListV2.ViewMode.SELECTLIST || this.mSelectViewMode == UnitListV2.ViewMode.TOWERSELECTLIST)
          component2.SetSortValue(this.mUnitSortMode, this.mSortValues[index], false);
        else
          component2.SetSortValue(this.mUnitSortMode, this.mSortValues[index], true);
      }
      else
        component2.ClearSortValue();
    }

    private void RefreshPieceUnit(bool clear)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.PieceTemplate, (UnityEngine.Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<ItemParam> items = instance.MasterParam.Items;
      UnitParam[] allUnits = instance.MasterParam.GetAllUnits();
      PlayerData player = instance.Player;
      List<UnitParam> source = new List<UnitParam>(this.DataCount);
      for (int index = 0; index < items.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitListV2.\u003CRefreshPieceUnit\u003Ec__AnonStorey240 unitCAnonStorey240 = new UnitListV2.\u003CRefreshPieceUnit\u003Ec__AnonStorey240();
        // ISSUE: reference to a compiler-generated field
        unitCAnonStorey240.item = items[index];
        // ISSUE: reference to a compiler-generated field
        if (unitCAnonStorey240.item.type == EItemType.UnitPiece)
        {
          // ISSUE: reference to a compiler-generated method
          UnitParam unitParam = Array.Find<UnitParam>(allUnits, new Predicate<UnitParam>(unitCAnonStorey240.\u003C\u003Em__1B9));
          if (unitParam != null && unitParam.summon && (!source.Contains(unitParam) && player.FindUnitDataByUnitID(unitParam.iname) == null) && unitParam.CheckAvailable(TimeManager.ServerTime))
            source.Add(unitParam);
        }
      }
      UnitParam[] array = source.OrderByDescending<UnitParam, int>((Func<UnitParam, int>) (unit => (int) unit.raremax)).ToArray<UnitParam>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NoMatchingUnit, (UnityEngine.Object) null))
        this.NoMatchingUnit.SetActive(false);
      this.mSortValues = (int[]) null;
      this.SetData((object[]) array, typeof (UnitParam));
    }

    protected override void OnItemSelect(GameObject go)
    {
      if (this.mSelectViewMode == UnitListV2.ViewMode.UNITLIST)
        this.OnSelectUnit(go);
      else if (this.mSelectViewMode == UnitListV2.ViewMode.SELECTLIST)
      {
        if (this.OnItemSelectAction == null)
          return;
        this.OnItemSelectAction(go);
      }
      else if (this.mSelectViewMode == UnitListV2.ViewMode.TOWERSELECTLIST)
      {
        if (this.OnItemSelectAction == null)
          return;
        this.OnItemSelectAction(go);
      }
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
      string[] filter = (string[]) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitPickerFilter, (UnityEngine.Object) null))
        filter = this.mUnitPickerFilter.GetFiltersAll();
      this.mUnitFilter = UnitListV2.UpdateFilterElement(index, filter);
      this.mSelectTabIndex = index;
      this.Page = 0;
      this.RefreshData();
    }

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      this.SetSortMethod(method, ascending, filters, false);
    }

    public void SetSortMethod(string method, bool ascending, string[] filters, bool is_ignore_element)
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AscendingIcon, (UnityEngine.Object) null))
        this.AscendingIcon.SetActive(ascending);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescendingIcon, (UnityEngine.Object) null))
        this.DescendingIcon.SetActive(!ascending);
      if (unitSortModes == GameUtility.UnitSortModes.Time)
        ascending = !ascending;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SortModeCaption, (UnityEngine.Object) null))
        this.SortModeCaption.set_text(LocalizedText.Get("sys.SORT_" + unitSortModes.ToString().ToUpper()));
      this.mReverse = ascending;
      this.mUnitSortMode = unitSortModes;
      if (is_ignore_element)
      {
        if (filters == null && this.mSelectTabIndex != 0)
          filters = this.mUnitPickerFilter.GetFiltersAll();
        this.mUnitFilter = UnitListV2.UpdateFilterElement(this.mSelectTabIndex, filters);
      }
      else
        this.mUnitFilter = filters;
      this.RefreshData();
    }

    public static string[] UpdateFilterElement(int index, string[] filter)
    {
      if (filter == null)
        return filter;
      string[] strArray = new string[6]{ "ELEM:Fire", "ELEM:Water", "ELEM:Wind", "ELEM:Thunder", "ELEM:Shine", "ELEM:Dark" };
      List<string> stringList = new List<string>(0);
      if (filter != null && filter.Length > 0)
        stringList.AddRange((IEnumerable<string>) filter);
      stringList.RemoveAll((Predicate<string>) (flist => flist.Contains("ELEM:")));
      if (index == 7)
      {
        stringList.Add("TAB:Favorite");
        for (int index1 = 0; index1 < strArray.Length; ++index1)
          stringList.Add(strArray[index1]);
        return stringList.ToArray();
      }
      if (index == 0 || index == 1)
        stringList.Add(strArray[0]);
      if (index == 0 || index == 2)
        stringList.Add(strArray[1]);
      if (index == 0 || index == 3)
        stringList.Add(strArray[2]);
      if (index == 0 || index == 4)
        stringList.Add(strArray[3]);
      if (index == 0 || index == 5)
        stringList.Add(strArray[4]);
      if (index == 0 || index == 6)
        stringList.Add(strArray[5]);
      return stringList.ToArray();
    }

    public void SetViewMode(UnitListV2.ViewMode value)
    {
      this.mSelectViewMode = value;
    }

    public void UpdateViewMode()
    {
      this.UpdateViewMode(this.mSelectViewMode);
    }

    public void UpdateViewMode(UnitListV2.ViewMode mode)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TabParentObject, (UnityEngine.Object) null))
        this.TabParentObject.SetActive(mode != UnitListV2.ViewMode.PIECELIST);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleObject, (UnityEngine.Object) null))
        this.TitleObject.SetActive(mode == UnitListV2.ViewMode.PIECELIST);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseButton, (UnityEngine.Object) null))
        ((Component) this.CloseButton).get_gameObject().SetActive(mode != UnitListV2.ViewMode.UNITLIST);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectableToggle, (UnityEngine.Object) null))
        ((Component) this.SelectableToggle).get_gameObject().SetActive(mode == UnitListV2.ViewMode.SELECTLIST || mode == UnitListV2.ViewMode.TOWERSELECTLIST);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SortButton, (UnityEngine.Object) null))
        ((Component) this.SortButton).get_gameObject().SetActive(mode != UnitListV2.ViewMode.PIECELIST);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FilterButton, (UnityEngine.Object) null))
        ((Component) this.FilterButton).get_gameObject().SetActive(mode != UnitListV2.ViewMode.PIECELIST);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PieceButton, (UnityEngine.Object) null))
        ((Component) this.PieceButton).get_gameObject().SetActive(mode == UnitListV2.ViewMode.UNITLIST);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HelpButton, (UnityEngine.Object) null))
        this.HelpButton.SetActive(mode == UnitListV2.ViewMode.UNITLIST);
      GameObject gameObject = (GameObject) null;
      if (mode == UnitListV2.ViewMode.SELECTLIST)
        gameObject = ((Component) this.RemoveTemplate).get_gameObject();
      if (mode == UnitListV2.ViewMode.TOWERSELECTLIST)
        gameObject = ((Component) this.RemoveTowerTemplate).get_gameObject();
      if (this.mSelectedSlotIndex != 0 && this.mCurrentParty.Units[this.mSelectedSlotIndex] != null || this.mIsHeroOnly && this.mCurrentParty.Units[this.mSelectedSlotIndex] != null)
      {
        this.mExtraItemList.Clear();
        this.mExtraItemList.Add(gameObject);
      }
      else
      {
        if (this.mExtraItemList != null && this.mExtraItemList.Count == 1)
          this.mExtraItemList[0].SetActive(false);
        this.mExtraItemList.Clear();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBackToHome, (UnityEngine.Object) null))
        this.mBackToHome.SetActive(mode == UnitListV2.ViewMode.UNITLIST || mode == UnitListV2.ViewMode.PIECELIST);
      this.ItemType = mode == UnitListV2.ViewMode.PIECELIST ? UnitListV2.ItemTypes.PieceUnits : UnitListV2.ItemTypes.PlayerUnits;
      this.mSelectViewMode = mode;
    }

    private void RefreshViewPieceList()
    {
      this.UpdateViewMode(UnitListV2.ViewMode.PIECELIST);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseButton, (UnityEngine.Object) null) && ((UnityEventBase) this.CloseButton.get_onClick()).GetPersistentEventCount() <= 0)
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CloseButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(RefreshViewUnitList)));
      }
      this.Page = 0;
      this.RefreshData();
    }

    private void RefreshViewUnitList()
    {
      this.UpdateViewMode(UnitListV2.ViewMode.UNITLIST);
      this.OnChangedToggle(this.mSelectTabIndex);
    }

    private void RefreshViewSelectList()
    {
      this.UpdateViewMode(UnitListV2.ViewMode.SELECTLIST);
    }

    private void OnCloseButton(SRPG_Button button)
    {
      if (this.OnCloseEvent == null)
        return;
      this.OnCloseEvent(button);
    }

    private void OnUnitSortOpen()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitPickerSort, (UnityEngine.Object) null))
        return;
      this.mUnitPickerSort.Open();
    }

    private void OnUnitFilterOpen()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitPickerFilter, (UnityEngine.Object) null))
        return;
      this.mUnitPickerFilter.Open();
    }

    private void SetupSortMethod(string method, bool ascending)
    {
      this.SetSortMethod(method, ascending, this.mUnitFilter, true);
    }

    private void SetupFilter(string[] filters)
    {
      bool ascending = this.mReverse;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitPickerSort, (UnityEngine.Object) null))
        ascending = this.mUnitPickerSort.IsAscending;
      this.SetSortMethod(this.mUnitSortMode.ToString(), ascending, filters, true);
    }

    protected override void RefreshItems()
    {
      if (this.mSelectViewMode == UnitListV2.ViewMode.UNITLIST || this.mSelectViewMode == UnitListV2.ViewMode.PIECELIST)
      {
        base.RefreshItems();
      }
      else
      {
        this.mPageSize = this.CellCount;
        Transform listParent = (Transform) this.ListParent;
        while (this.mItems.Count < this.mPageSize)
        {
          GameObject gameObject = this.CreateItem((int) this.mSelectViewMode);
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
            return;
          gameObject.get_transform().SetParent(listParent, false);
          this.mItems.Add(gameObject);
          ListItemEvents component = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.OnSelect = new ListItemEvents.ListItemEvent(((SRPG_FixedList) this)._OnItemSelect);
        }
        if (this.mItems.Count == 0)
          return;
        for (int index = 0; index < this.mItems.Count; ++index)
          this.mItems[index].get_gameObject().SetActive(false);
        if (this.mPageSize > 0)
        {
          this.mMaxPages = (this.DataCount + this.mExtraItemList.Count + this.mPageSize - 1) / this.mPageSize;
          this.mPage = Mathf.Clamp(this.mPage, 0, this.mMaxPages - 1);
        }
        if (this.mFocusSelection)
        {
          this.mFocusSelection = false;
          if (this.mSelection != null && this.mSelection.Count > 0)
          {
            int num = Array.IndexOf<object>(this.mData, this.mSelection[0]) + this.mExtraItemList.Count;
            if (num >= 0)
              this.mPage = num / this.mPageSize;
          }
        }
        for (int index = 0; index < this.ChosenUnitBadges.Length; ++index)
        {
          ((Component) this.ChosenUnitBadges[index]).get_gameObject().SetActive(false);
          ((Transform) this.ChosenUnitBadges[index]).SetParent((Transform) this.ListParent, false);
        }
        for (int index = 0; index < this.ChosenUnitBadgesTower.Length; ++index)
        {
          ((Component) this.ChosenUnitBadgesTower[index]).get_gameObject().SetActive(false);
          ((Transform) this.ChosenUnitBadgesTower[index]).SetParent((Transform) this.ListParent, false);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitHilit, (UnityEngine.Object) null))
        {
          ((Component) this.UnitHilit).get_gameObject().SetActive(false);
          ((Transform) this.UnitHilit).SetParent((Transform) this.ListParent, false);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitHilitTower, (UnityEngine.Object) null))
        {
          ((Component) this.UnitHilitTower).get_gameObject().SetActive(false);
          ((Transform) this.UnitHilitTower).SetParent((Transform) this.ListParent, false);
        }
        for (int index1 = 0; index1 < this.mItems.Count; ++index1)
        {
          int index2 = this.mPage * this.mPageSize + index1 - this.mExtraItemList.Count;
          if (0 <= index2 && index2 < this.mData.Length)
          {
            int num = -1;
            if (this.mUnitDatas.Count > index2)
              num = this.mCurrentParty.IndexOf(this.mUnitDatas[index2]);
            DataSource.Bind(this.mItems[index1], this.mDataType, this.mData[index2]);
            this.OnUpdateItem(this.mItems[index1], index2);
            this.mItems[index1].SetActive(true);
            GameParameter.UpdateAll(this.mItems[index1]);
            UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.mItems[index1], (UnitData) null);
            if (dataOfClass != null)
            {
              for (int index3 = 0; index3 < this.mCurrentParty.Units.Length; ++index3)
              {
                if (this.mCurrentParty.Units[index3] != null && this.mCurrentParty.Units[index3].UniqueID == dataOfClass.UniqueID)
                {
                  RectTransform component = (RectTransform) this.mItems[index1].GetComponent<RectTransform>();
                  if (index3 == this.mSelectedSlotIndex)
                  {
                    RectTransform rectTransform = (RectTransform) null;
                    if (this.mSelectViewMode == UnitListV2.ViewMode.SELECTLIST)
                      rectTransform = this.UnitHilit;
                    else if (this.mSelectViewMode == UnitListV2.ViewMode.TOWERSELECTLIST)
                      rectTransform = this.UnitHilitTower;
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) rectTransform, (UnityEngine.Object) null))
                    {
                      ((Transform) rectTransform).SetParent((Transform) component, false);
                      rectTransform.set_anchorMin(new Vector2(0.0f, 1f));
                      rectTransform.set_anchorMax(new Vector2(0.0f, 1f));
                      rectTransform.set_anchoredPosition(new Vector2(100f, 0.0f));
                      ((Component) rectTransform).get_gameObject().SetActive(true);
                    }
                  }
                  RectTransform rectTransform1 = (RectTransform) null;
                  if (this.mSelectViewMode == UnitListV2.ViewMode.SELECTLIST)
                  {
                    if (index3 >= 0 && index3 < this.ChosenUnitBadges.Length && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.ChosenUnitBadges[index3], (UnityEngine.Object) null))
                    {
                      if (index3 >= this.mCurrentParty.PartyData.SUBMEMBER_START)
                      {
                        int index4 = 4 + (index3 - this.mCurrentParty.PartyData.SUBMEMBER_START);
                        if ((index4 >= 0 || index4 < this.ChosenUnitBadges.Length) && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.ChosenUnitBadges[index4], (UnityEngine.Object) null))
                          rectTransform1 = this.ChosenUnitBadges[index4];
                        else
                          continue;
                      }
                      else
                        rectTransform1 = this.ChosenUnitBadges[index3];
                    }
                    else
                      continue;
                  }
                  else if (this.mSelectViewMode == UnitListV2.ViewMode.TOWERSELECTLIST)
                  {
                    if (index3 >= 0 && index3 < this.ChosenUnitBadgesTower.Length && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.ChosenUnitBadgesTower[index3], (UnityEngine.Object) null))
                      rectTransform1 = this.ChosenUnitBadgesTower[index3];
                    else
                      continue;
                  }
                  if (!UnityEngine.Object.op_Equality((UnityEngine.Object) rectTransform1, (UnityEngine.Object) null))
                  {
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Transform) rectTransform1).get_parent(), (UnityEngine.Object) component))
                    {
                      ((Transform) rectTransform1).SetParent((Transform) component, false);
                      rectTransform1.set_anchorMin(new Vector2(0.0f, 1f));
                      rectTransform1.set_anchorMax(new Vector2(0.0f, 1f));
                      rectTransform1.set_anchoredPosition(new Vector2(0.0f, 20f));
                      ((Component) rectTransform1).get_gameObject().SetActive(true);
                    }
                    if (index3 == 0)
                      ((Component) rectTransform1).get_gameObject().SetActive(this.mPage <= 0 && !this.mIsHeroOnly && (this.mSelectViewMode == UnitListV2.ViewMode.SELECTLIST || this.mSelectViewMode == UnitListV2.ViewMode.TOWERSELECTLIST));
                  }
                }
              }
              if (this.mCurrentQuest != null)
              {
                CanvasGroup component = (CanvasGroup) this.mItems[index1].GetComponent<CanvasGroup>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && dataOfClass != null)
                {
                  bool flag1 = this.mCurrentQuest.IsUnitAllowed(dataOfClass) || num >= 0;
                  string error = (string) null;
                  bool flag2 = flag1 & this.mCurrentQuest.IsEntryQuestCondition(dataOfClass, ref error);
                  component.set_alpha(!flag2 ? 0.5f : 1f);
                  component.set_interactable(flag2);
                }
              }
            }
          }
          else
            this.mItems[index1].SetActive(false);
        }
        for (int index = 0; index < this.mExtraItemList.Count; ++index)
        {
          int num = this.mPage * this.mPageSize + index;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mExtraItemList[index], (UnityEngine.Object) null))
            this.mExtraItemList[index].SetActive(0 <= num && num < this.mExtraItemList.Count);
        }
        this.UpdateSelection();
        this.UpdatePage();
        if (!this.mInvokeSelChange)
          return;
        this.mInvokeSelChange = false;
        this.TriggerSelectionChange();
      }
    }

    private bool IsSetRemoveObject(int slot)
    {
      return slot != 0 && (slot <= 0 || slot >= this.mCurrentParty.Units.Length || this.mCurrentParty.Units[slot] != null);
    }

    public void SetUnitList(UnitData[] units)
    {
      this.mUnitDatas = new List<UnitData>((IEnumerable<UnitData>) units);
      if (this.mUnitDatas == null || this.mUnitDatas.Count > 0)
        ;
    }

    public void SetPartyData(PartyEditData party)
    {
      this.mCurrentParty = party;
      if (this.mCurrentParty == null)
        ;
    }

    public void SetQuestData(QuestParam quest)
    {
      this.mCurrentQuest = quest;
    }

    public void OnRemoveUnitSelect(SRPG_Button button)
    {
      if (this.OnRemoveEvent == null)
        return;
      this.OnRemoveEvent(button);
      ((WindowController) ((Component) this).GetComponent<WindowController>()).Close();
    }

    private void OnValueChanged(bool value)
    {
      this.RefreshData();
    }

    private void MoveToOrigin(GameObject go)
    {
      RectTransform component = (RectTransform) go.GetComponent<RectTransform>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.set_anchoredPosition(Vector2.get_zero());
    }

    public void SetRefresh()
    {
      if (!this.HasStarted)
        return;
      this.RefreshData();
      this.RefreshItems();
    }

    private int GetSortModeValue(UnitData unit, GameUtility.UnitSortModes mode)
    {
      switch (mode)
      {
        case GameUtility.UnitSortModes.JobRank:
          return unit.CurrentJob.Rank;
        case GameUtility.UnitSortModes.HP:
          return (int) unit.Status.param.hp;
        case GameUtility.UnitSortModes.Atk:
          return (int) unit.Status.param.atk;
        case GameUtility.UnitSortModes.Def:
          return (int) unit.Status.param.def;
        case GameUtility.UnitSortModes.Mag:
          return (int) unit.Status.param.mag;
        case GameUtility.UnitSortModes.Mnd:
          return (int) unit.Status.param.mnd;
        case GameUtility.UnitSortModes.Spd:
          return (int) unit.Status.param.spd;
        case GameUtility.UnitSortModes.Total:
          return (int) unit.Status.param.atk + (int) unit.Status.param.def + (int) unit.Status.param.mag + (int) unit.Status.param.mnd + (int) unit.Status.param.spd + (int) unit.Status.param.dex + (int) unit.Status.param.cri + (int) unit.Status.param.luk;
        case GameUtility.UnitSortModes.Awake:
          return unit.AwakeLv;
        case GameUtility.UnitSortModes.Combination:
          return unit.GetCombination();
        default:
          return unit.Lv;
      }
    }

    public string GetFilterInfo()
    {
      string[] strArray = UnitListV2.UpdateFilterElement(this.mSelectTabIndex, this.mUnitFilter);
      if (strArray == null || strArray.Length <= 0)
        return string.Empty;
      return string.Join("|", strArray);
    }

    public enum ItemTypes
    {
      PlayerUnits,
      PieceUnits,
    }

    public enum PartyBadge
    {
      Main0,
      Main1,
      Main2,
      Main3,
      Sub0,
      Sub1,
      Max,
    }

    public enum ViewMode
    {
      UNITLIST,
      SELECTLIST,
      TOWERSELECTLIST,
      PIECELIST,
    }

    public delegate void UnitSelectEvent(long uniqueID);

    public delegate void ItemSelectEvents(GameObject go);

    public delegate void CommonEvents(SRPG_Button button);
  }
}
