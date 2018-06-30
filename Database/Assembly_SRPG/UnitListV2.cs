namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [AddComponentMenu("UI/リスト/ユニット"), Pin(2, "Unit Unlocked", 1, 2), Pin(100, "Refresh", 0, 100), Pin(1, "Unit Selected", 1, 1)]
    public class UnitListV2 : SRPG_FixedList, IFlowInterface, ISortableList
    {
        public ItemTypes ItemType;
        public UnitSelectEvent OnUnitSelect;
        public Transform ItemLayoutParent;
        public GameObject ItemTemplate;
        public GameObject PieceTemplate;
        public Pulldown SortFilter;
        public GameObject UnitSortFilterButton;
        public GameObject AscendingIcon;
        public GameObject DescendingIcon;
        public List<Toggle> UnitToggle;
        public bool IncludeShujinko;
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
        private ItemTypes mLastItemType;
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
        public static readonly string UNIT_SORT_PATH;
        public static readonly string UNIT_FILTER_PATH;
        private GameObject mSortWindow;
        private UnitPickerSort mUnitPickerSort;
        private GameObject mFilterWindow;
        private UnitPickerFilter mUnitPickerFilter;
        private ViewMode mSelectViewMode;
        public ItemSelectEvents OnItemSelectAction;
        public CommonEvents OnCloseEvent;
        public CommonEvents OnRemoveEvent;
        private List<UnitData> mUnitDatas;
        private PartyEditData mCurrentParty;
        private QuestParam mCurrentQuest;
        private bool mUseQuestInfo;
        private int mSelectedSlotIndex;
        public string MenuID;
        public Toggle SelectableToggle;
        public RectTransform[] ChosenUnitBadges;
        public RectTransform[] ChosenUnitBadgesTower;
        public RectTransform UnitHilit;
        public RectTransform UnitHilitTower;
        private List<GameObject> mExtraItemList;
        private bool mIsHeroOnly;
        public GameObject mBackToHome;
        private bool mIsSetRefresh;
        [CompilerGenerated]
        private static Func<UnitParam, int> <>f__am$cache41;
        [CompilerGenerated]
        private static Predicate<string> <>f__am$cache42;

        static UnitListV2()
        {
            UNIT_SORT_PATH = "UI/UnitPickerSort";
            UNIT_FILTER_PATH = "UI/UnitPickerFilter";
            return;
        }

        public UnitListV2()
        {
            this.IncludeShujinko = 1;
            this.mLastItemType = -1;
            this.mUnitDatas = new List<UnitData>();
            this.MenuID = "UNITLIST";
            this.ChosenUnitBadges = new RectTransform[0];
            this.ChosenUnitBadgesTower = new RectTransform[0];
            this.mExtraItemList = new List<GameObject>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <RefreshPieceUnit>m__419(UnitParam unit)
        {
            return unit.raremax;
        }

        [CompilerGenerated]
        private bool <Start>m__414(GameUtility.UnitSortModes p)
        {
            return (p == this.mUnitSortMode);
        }

        [CompilerGenerated]
        private static bool <UpdateFilterElement>m__41A(string flist)
        {
            return flist.Contains("ELEM:");
        }

        public void Activated(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0030;
            }
            if (base.HasStarted == null)
            {
                goto Label_0029;
            }
            if (this.mIsSetRefresh == null)
            {
                goto Label_0029;
            }
            this.RefreshData();
            goto Label_0030;
        Label_0029:
            this.mIsSetRefresh = 1;
        Label_0030:
            return;
        }

        protected override void Awake()
        {
            int num;
            <Awake>c__AnonStorey3A7 storeya;
            base.Awake();
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0033;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0033;
            }
            this.ItemTemplate.SetActive(0);
        Label_0033:
            if ((this.PieceTemplate != null) == null)
            {
                goto Label_0060;
            }
            if (this.PieceTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0060;
            }
            this.PieceTemplate.SetActive(0);
        Label_0060:
            if ((this.SelectTemplate != null) == null)
            {
                goto Label_008D;
            }
            if (this.SelectTemplate.get_activeInHierarchy() == null)
            {
                goto Label_008D;
            }
            this.SelectTemplate.SetActive(0);
        Label_008D:
            if ((this.RemoveTemplate != null) == null)
            {
                goto Label_00AF;
            }
            this.RemoveTemplate.get_gameObject().SetActive(0);
        Label_00AF:
            if ((this.SelectTowerTemplate != null) == null)
            {
                goto Label_00DC;
            }
            if (this.SelectTowerTemplate.get_activeInHierarchy() == null)
            {
                goto Label_00DC;
            }
            this.SelectTowerTemplate.SetActive(0);
        Label_00DC:
            if ((this.RemoveTowerTemplate != null) == null)
            {
                goto Label_00FE;
            }
            this.RemoveTowerTemplate.get_gameObject().SetActive(0);
        Label_00FE:
            if ((this.UnitHilit != null) == null)
            {
                goto Label_0120;
            }
            this.UnitHilit.get_gameObject().SetActive(0);
        Label_0120:
            if ((this.UnitHilitTower != null) == null)
            {
                goto Label_0142;
            }
            this.UnitHilitTower.get_gameObject().SetActive(0);
        Label_0142:
            if (this.UnitToggle == null)
            {
                goto Label_01BB;
            }
            num = 0;
            goto Label_01AA;
        Label_0154:
            storeya = new <Awake>c__AnonStorey3A7();
            storeya.<>f__this = this;
            if ((this.UnitToggle[num] == null) == null)
            {
                goto Label_017D;
            }
            goto Label_01A6;
        Label_017D:
            storeya.index = num;
            this.UnitToggle[num].onValueChanged.AddListener(new UnityAction<bool>(storeya, this.<>m__413));
        Label_01A6:
            num += 1;
        Label_01AA:
            if (num < this.UnitToggle.Count)
            {
                goto Label_0154;
            }
        Label_01BB:
            return;
        }

        protected override GameObject CreateItem()
        {
            if (this.ItemType != null)
            {
                goto Label_0017;
            }
            return Object.Instantiate<GameObject>(this.ItemTemplate);
        Label_0017:
            return Object.Instantiate<GameObject>(this.PieceTemplate);
        }

        protected override GameObject CreateItem(int index)
        {
            ViewMode mode;
            mode = index;
            if (mode != null)
            {
                goto Label_0014;
            }
            return Object.Instantiate<GameObject>(this.ItemTemplate);
        Label_0014:
            if (mode != 1)
            {
                goto Label_0027;
            }
            return Object.Instantiate<GameObject>(this.SelectTemplate);
        Label_0027:
            if (mode != 2)
            {
                goto Label_003A;
            }
            return Object.Instantiate<GameObject>(this.SelectTowerTemplate);
        Label_003A:
            return Object.Instantiate<GameObject>(this.PieceTemplate);
        }

        public static unsafe void FilterUnits(List<UnitData> units, List<int> sortValues, string[] filter)
        {
            int num;
            int num2;
            bool flag;
            int num3;
            List<string> list;
            string str;
            int num4;
            int num5;
            EElement element;
            AttackDetailTypes types;
            int num6;
            UnitData data;
            if (filter != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            num2 = 0;
            flag = 0;
            num3 = 0;
            list = new List<string>();
            str = null;
            num4 = 0;
            goto Label_0173;
        Label_0021:
            if (GetValue(filter[num4], "TAB:", &str) == null)
            {
                goto Label_004E;
            }
            if ((str == "Favorite") == null)
            {
                goto Label_016D;
            }
            flag = 1;
            goto Label_016D;
        Label_004E:
            if (GetValue(filter[num4], "RARE:", &str) == null)
            {
                goto Label_0083;
            }
            if (int.TryParse(str, &num5) == null)
            {
                goto Label_016D;
            }
            num |= 1 << ((num5 & 0x1f) & 0x1f);
            goto Label_016D;
        Label_0083:
            if (GetValue(filter[num4], "ELEM:", &str) == null)
            {
                goto Label_00E9;
            }
        Label_0098:
            try
            {
                element = (int) Enum.Parse(typeof(EElement), str, 1);
                num2 |= 1 << ((element & 0x1f) & 0x1f);
                goto Label_00E4;
            }
            catch
            {
            Label_00C3:
                if (GameUtility.IsDebugBuild == null)
                {
                    goto Label_00DF;
                }
                Debug.LogError("Unknown element type: " + str);
            Label_00DF:
                goto Label_00E4;
            }
        Label_00E4:
            goto Label_016D;
        Label_00E9:
            if (GetValue(filter[num4], "WEAPON:", &str) == null)
            {
                goto Label_014F;
            }
        Label_00FE:
            try
            {
                types = (int) Enum.Parse(typeof(AttackDetailTypes), str, 1);
                num3 |= 1 << ((types & 0x1f) & 0x1f);
                goto Label_014A;
            }
            catch
            {
            Label_0129:
                if (GameUtility.IsDebugBuild == null)
                {
                    goto Label_0145;
                }
                Debug.LogError("Unknown weapon type: " + str);
            Label_0145:
                goto Label_014A;
            }
        Label_014A:
            goto Label_016D;
        Label_014F:
            if (GetValue(filter[num4], "BIRTH:", &str) == null)
            {
                goto Label_016D;
            }
            list.Add(str);
        Label_016D:
            num4 += 1;
        Label_0173:
            if (num4 < ((int) filter.Length))
            {
                goto Label_0021;
            }
            num6 = units.Count - 1;
            goto Label_0250;
        Label_018C:
            data = units[num6];
            if (((1 << (data.Rarity & 0x1f)) & num) == null)
            {
                goto Label_0234;
            }
            if (flag == null)
            {
                goto Label_01BB;
            }
            if (data.IsFavorite == null)
            {
                goto Label_0234;
            }
        Label_01BB:
            if (((1 << (data.Element & 0x1f)) & num2) == null)
            {
                goto Label_0234;
            }
            if (data.CurrentJob.GetAttackSkill() == null)
            {
                goto Label_0234;
            }
            if (((1 << (data.CurrentJob.GetAttackSkill().AttackDetailType & 0x1f)) & num3) == null)
            {
                goto Label_0234;
            }
            if (string.IsNullOrEmpty(data.UnitParam.birth) != null)
            {
                goto Label_0234;
            }
            if (list.Contains(&data.UnitParam.birth.ToString()) != null)
            {
                goto Label_024A;
            }
        Label_0234:
            units.RemoveAt(num6);
            if (sortValues == null)
            {
                goto Label_024A;
            }
            sortValues.RemoveAt(num6);
        Label_024A:
            num6 -= 1;
        Label_0250:
            if (num6 >= 0)
            {
                goto Label_018C;
            }
            return;
        }

        public static unsafe void FilterUnitsRevert(List<UnitData> units, List<int> sortValues, string[] filter)
        {
            int num;
            int num2;
            int num3;
            List<string> list;
            string str;
            int num4;
            int num5;
            EElement element;
            AttackDetailTypes types;
            int num6;
            UnitData data;
            if (filter != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            num2 = 0;
            num3 = 0;
            list = new List<string>();
            str = null;
            num4 = 0;
            goto Label_0142;
        Label_001E:
            if (GetValue(filter[num4], "RARE:", &str) == null)
            {
                goto Label_0053;
            }
            if (int.TryParse(str, &num5) == null)
            {
                goto Label_013C;
            }
            num |= 1 << ((num5 & 0x1f) & 0x1f);
            goto Label_013C;
        Label_0053:
            if (GetValue(filter[num4], "ELEM:", &str) == null)
            {
                goto Label_00B9;
            }
        Label_0068:
            try
            {
                element = (int) Enum.Parse(typeof(EElement), str, 1);
                num2 |= 1 << ((element & 0x1f) & 0x1f);
                goto Label_00B4;
            }
            catch
            {
            Label_0093:
                if (GameUtility.IsDebugBuild == null)
                {
                    goto Label_00AF;
                }
                Debug.LogError("Unknown element type: " + str);
            Label_00AF:
                goto Label_00B4;
            }
        Label_00B4:
            goto Label_013C;
        Label_00B9:
            if (GetValue(filter[num4], "WEAPON:", &str) == null)
            {
                goto Label_011F;
            }
        Label_00CE:
            try
            {
                types = (int) Enum.Parse(typeof(AttackDetailTypes), str, 1);
                num3 |= 1 << ((types & 0x1f) & 0x1f);
                goto Label_011A;
            }
            catch
            {
            Label_00F9:
                if (GameUtility.IsDebugBuild == null)
                {
                    goto Label_0115;
                }
                Debug.LogError("Unknown weapon type: " + str);
            Label_0115:
                goto Label_011A;
            }
        Label_011A:
            goto Label_013C;
        Label_011F:
            if (GetValue(filter[num4], "BIRTH:", &str) == null)
            {
                goto Label_013C;
            }
            list.Add(str);
        Label_013C:
            num4 += 1;
        Label_0142:
            if (num4 < ((int) filter.Length))
            {
                goto Label_001E;
            }
            num6 = units.Count - 1;
            goto Label_020C;
        Label_015B:
            data = units[num6];
            if (((1 << (data.Rarity & 0x1f)) & num) != null)
            {
                goto Label_01F0;
            }
            if (((1 << (data.Element & 0x1f)) & num2) != null)
            {
                goto Label_01F0;
            }
            if (data.CurrentJob.GetAttackSkill() == null)
            {
                goto Label_01F0;
            }
            if (((1 << (data.CurrentJob.GetAttackSkill().AttackDetailType & 0x1f)) & num3) != null)
            {
                goto Label_01F0;
            }
            if (string.IsNullOrEmpty(data.UnitParam.birth) != null)
            {
                goto Label_01F0;
            }
            if (list.Contains(&data.UnitParam.birth.ToString()) == null)
            {
                goto Label_0206;
            }
        Label_01F0:
            units.RemoveAt(num6);
            if (sortValues == null)
            {
                goto Label_0206;
            }
            sortValues.RemoveAt(num6);
        Label_0206:
            num6 -= 1;
        Label_020C:
            if (num6 >= 0)
            {
                goto Label_015B;
            }
            return;
        }

        public string GetFilterInfo()
        {
            string[] strArray;
            strArray = UpdateFilterElement(this.mSelectTabIndex, this.mUnitFilter);
        Label_0021:
            return (((strArray != null) && (((int) strArray.Length) > 0)) ? string.Join("|", strArray) : string.Empty);
        }

        private int GetSortModeValue(UnitData unit, GameUtility.UnitSortModes mode)
        {
            int num;
            GameUtility.UnitSortModes modes;
            modes = mode;
            switch (modes)
            {
                case 0:
                    goto Label_01A3;

                case 1:
                    goto Label_01A3;

                case 2:
                    goto Label_0197;

                case 3:
                    goto Label_0181;

                case 4:
                    goto Label_0045;

                case 5:
                    goto Label_005B;

                case 6:
                    goto Label_0071;

                case 7:
                    goto Label_0087;

                case 8:
                    goto Label_01A3;

                case 9:
                    goto Label_009D;

                case 10:
                    goto Label_00B3;

                case 11:
                    goto Label_0173;

                case 12:
                    goto Label_017A;

                case 13:
                    goto Label_01A3;
            }
            goto Label_01A3;
        Label_0045:
            return unit.Status.param.atk;
        Label_005B:
            return unit.Status.param.def;
        Label_0071:
            return unit.Status.param.mag;
        Label_0087:
            return unit.Status.param.mnd;
        Label_009D:
            return unit.Status.param.spd;
        Label_00B3:
            num = unit.Status.param.atk + unit.Status.param.def;
            num += unit.Status.param.mag;
            num += unit.Status.param.mnd;
            num += unit.Status.param.spd;
            num += unit.Status.param.dex;
            num += unit.Status.param.cri;
            num += unit.Status.param.luk;
            return num;
        Label_0173:
            return unit.AwakeLv;
        Label_017A:
            return unit.GetCombination();
        Label_0181:
            return unit.Status.param.hp;
        Label_0197:
            return unit.CurrentJob.Rank;
        Label_01A3:
            return unit.Lv;
        }

        private static bool GetValue(string str, string key, ref string value)
        {
            if (str.StartsWith(key) == null)
            {
                goto Label_001C;
            }
            *(value) = str.Substring(key.Length);
            return 1;
        Label_001C:
            return 0;
        }

        private bool IsSetRemoveObject(int slot)
        {
            if (slot != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if ((slot <= 0) || (slot >= ((int) this.mCurrentParty.Units.Length)))
            {
                goto Label_003C;
            }
            return ((this.mCurrentParty.Units[slot] != null) ? 1 : 0);
        Label_003C:
            return 1;
        }

        protected override void LateUpdate()
        {
        }

        private void MoveToOrigin(GameObject go)
        {
            RectTransform transform;
            transform = go.GetComponent<RectTransform>();
            if ((transform != null) == null)
            {
                goto Label_001E;
            }
            transform.set_anchoredPosition(Vector2.get_zero());
        Label_001E:
            return;
        }

        private void OnChangedToggle(int index)
        {
            string[] strArray;
            strArray = null;
            if ((this.mUnitPickerFilter != null) == null)
            {
                goto Label_001F;
            }
            strArray = this.mUnitPickerFilter.GetFiltersAll();
        Label_001F:
            this.mUnitFilter = UpdateFilterElement(index, strArray);
            this.mSelectTabIndex = index;
            base.Page = 0;
            this.RefreshData();
            return;
        }

        private void OnCloseButton(SRPG_Button button)
        {
            if (this.OnCloseEvent == null)
            {
                goto Label_0017;
            }
            this.OnCloseEvent(button);
        Label_0017:
            return;
        }

        protected override void OnDestroy()
        {
            if ((this.mSortWindow != null) == null)
            {
                goto Label_0023;
            }
            Object.Destroy(this.mSortWindow);
            this.mSortWindow = null;
        Label_0023:
            if ((this.mFilterWindow != null) == null)
            {
                goto Label_0046;
            }
            Object.Destroy(this.mFilterWindow);
            this.mFilterWindow = null;
        Label_0046:
            base.OnDestroy();
            return;
        }

        protected override void OnItemSelect(GameObject go)
        {
            if (this.mSelectViewMode != null)
            {
                goto Label_0017;
            }
            this.OnSelectUnit(go);
            goto Label_006E;
        Label_0017:
            if (this.mSelectViewMode != 1)
            {
                goto Label_003F;
            }
            if (this.OnItemSelectAction == null)
            {
                goto Label_006E;
            }
            this.OnItemSelectAction(go);
            goto Label_006E;
        Label_003F:
            if (this.mSelectViewMode != 2)
            {
                goto Label_0067;
            }
            if (this.OnItemSelectAction == null)
            {
                goto Label_006E;
            }
            this.OnItemSelectAction(go);
            goto Label_006E;
        Label_0067:
            this.OnSelectPieceUnit(go);
        Label_006E:
            return;
        }

        public void OnRemoveUnitSelect(SRPG_Button button)
        {
            if (this.OnRemoveEvent == null)
            {
                goto Label_0022;
            }
            this.OnRemoveEvent(button);
            base.GetComponent<WindowController>().Close();
        Label_0022:
            return;
        }

        private void OnSelectPieceUnit(GameObject go)
        {
            UnitParam param;
            param = DataSource.FindDataOfClass<UnitParam>(go, null);
            if (param == null)
            {
                goto Label_0020;
            }
            GlobalVars.UnlockUnitID = param.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
        Label_0020:
            return;
        }

        private void OnSelectUnit(GameObject go)
        {
            UnitData data;
            data = DataSource.FindDataOfClass<UnitData>(go, null);
            if (data == null)
            {
                goto Label_0062;
            }
            this.mSelectUnitUniqueID = data.UniqueID;
            if (this.OnUnitSelect == null)
            {
                goto Label_003B;
            }
            this.OnUnitSelect(data.UniqueID);
            goto Label_0062;
        Label_003B:
            GlobalVars.SelectedUnitUniqueID.Set(data.UniqueID);
            GlobalVars.SelectedUnitJobIndex.Set(data.JobIndex);
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
        Label_0062:
            return;
        }

        private unsafe void OnSortModeChange(int index)
        {
            GameSettings settings;
            settings = GameSettings.Instance;
            this.mUnitSortMode = &(settings.UnitSort_Modes[index]).Mode;
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.UNITLIST_UNIT_SORT_MODE, this.mUnitSortMode, 0);
            this.RefreshData();
            return;
        }

        private void OnUnitFilterOpen()
        {
            if ((this.mUnitPickerFilter != null) == null)
            {
                goto Label_001C;
            }
            this.mUnitPickerFilter.Open();
        Label_001C:
            return;
        }

        private void OnUnitSortOpen()
        {
            if ((this.mUnitPickerSort != null) == null)
            {
                goto Label_001C;
            }
            this.mUnitPickerSort.Open();
        Label_001C:
            return;
        }

        protected override void OnUpdateItem(GameObject go, int index)
        {
            UnitData data;
            Selectable[] selectableArray;
            bool flag;
            UnitListItemEvents events;
            UnitIcon icon;
            if (this.ItemType != null)
            {
                goto Label_00E1;
            }
            data = base.Data[index] as UnitData;
            if (this.IsEnhanceEquipment == null)
            {
                goto Label_0053;
            }
            selectableArray = go.GetComponentsInChildren<Selectable>(1);
            if (((int) selectableArray.Length) <= 0)
            {
                goto Label_0053;
            }
            flag = data.CheckEnableEnhanceEquipment();
            if (flag == selectableArray[0].get_interactable())
            {
                goto Label_0053;
            }
            selectableArray[0].set_interactable(flag);
        Label_0053:
            events = go.GetComponent<UnitListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_006C;
            }
            events.Refresh();
        Label_006C:
            icon = go.GetComponent<UnitIcon>();
            if ((icon != null) == null)
            {
                goto Label_00E1;
            }
            if (this.mSortValues == null)
            {
                goto Label_00DA;
            }
            if (this.mSelectViewMode == 1)
            {
                goto Label_00A4;
            }
            if (this.mSelectViewMode != 2)
            {
                goto Label_00BF;
            }
        Label_00A4:
            icon.SetSortValue(this.mUnitSortMode, this.mSortValues[index], 0);
            goto Label_00D5;
        Label_00BF:
            icon.SetSortValue(this.mUnitSortMode, this.mSortValues[index], 1);
        Label_00D5:
            goto Label_00E1;
        Label_00DA:
            icon.ClearSortValue();
        Label_00E1:
            return;
        }

        private void OnValueChanged(bool value)
        {
            this.RefreshData();
            return;
        }

        public void RefreshData()
        {
            bool flag;
            ItemTypes types;
            flag = 0;
            if (this.mLastItemType == this.ItemType)
            {
                goto Label_0027;
            }
            this.mLastItemType = this.ItemType;
            base.ClearItems();
            flag = 1;
        Label_0027:
            types = this.ItemType;
            if (types == null)
            {
                goto Label_0040;
            }
            if (types == 1)
            {
                goto Label_004C;
            }
            goto Label_0058;
        Label_0040:
            this.RefreshUnit(flag);
            goto Label_0058;
        Label_004C:
            this.RefreshPieceUnit(flag);
        Label_0058:
            return;
        }

        protected override unsafe void RefreshItems()
        {
            Transform transform;
            GameObject obj2;
            Transform transform2;
            ListItemEvents events;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            UnitData data;
            int num8;
            RectTransform transform3;
            RectTransform transform4;
            RectTransform transform5;
            int num9;
            int num10;
            CanvasGroup group;
            bool flag;
            string str;
            int num11;
            int num12;
            if ((this.mSelectViewMode != null) && (this.mSelectViewMode != 3))
            {
                goto Label_0022;
            }
            base.RefreshItems();
            goto Label_07C5;
        Label_0022:
            base.mPageSize = base.CellCount;
            transform = this.ListParent;
            goto Label_0094;
        Label_003A:
            obj2 = this.CreateItem(this.mSelectViewMode);
            if ((obj2 == null) == null)
            {
                goto Label_0054;
            }
            return;
        Label_0054:
            obj2.get_transform().SetParent(transform, 0);
            base.mItems.Add(obj2);
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0094;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this._OnItemSelect);
        Label_0094:
            if (base.mItems.Count < base.mPageSize)
            {
                goto Label_003A;
            }
            if (base.mItems.Count != null)
            {
                goto Label_00BB;
            }
            return;
        Label_00BB:
            num = 0;
            goto Label_00E1;
        Label_00C3:
            base.mItems[num].get_gameObject().SetActive(0);
            num += 1;
        Label_00E1:
            if (num < base.mItems.Count)
            {
                goto Label_00C3;
            }
            if (base.mPageSize <= 0)
            {
                goto Label_0141;
            }
            base.mMaxPages = (((this.DataCount + this.mExtraItemList.Count) + base.mPageSize) - 1) / base.mPageSize;
            base.mPage = Mathf.Clamp(base.mPage, 0, base.mMaxPages - 1);
        Label_0141:
            if (base.mFocusSelection == null)
            {
                goto Label_01AB;
            }
            base.mFocusSelection = 0;
            if ((base.mSelection == null) || (base.mSelection.Count <= 0))
            {
                goto Label_01AB;
            }
            num2 = Array.IndexOf<object>(base.mData, base.mSelection[0]) + this.mExtraItemList.Count;
            if (num2 < 0)
            {
                goto Label_01AB;
            }
            base.mPage = num2 / base.mPageSize;
        Label_01AB:
            num3 = 0;
            goto Label_01E2;
        Label_01B3:
            this.ChosenUnitBadges[num3].get_gameObject().SetActive(0);
            this.ChosenUnitBadges[num3].SetParent(this.ListParent, 0);
            num3 += 1;
        Label_01E2:
            if (num3 < ((int) this.ChosenUnitBadges.Length))
            {
                goto Label_01B3;
            }
            num4 = 0;
            goto Label_0228;
        Label_01F9:
            this.ChosenUnitBadgesTower[num4].get_gameObject().SetActive(0);
            this.ChosenUnitBadgesTower[num4].SetParent(this.ListParent, 0);
            num4 += 1;
        Label_0228:
            if (num4 < ((int) this.ChosenUnitBadgesTower.Length))
            {
                goto Label_01F9;
            }
            if ((this.UnitHilit != null) == null)
            {
                goto Label_026B;
            }
            this.UnitHilit.get_gameObject().SetActive(0);
            this.UnitHilit.SetParent(this.ListParent, 0);
        Label_026B:
            if ((this.UnitHilitTower != null) == null)
            {
                goto Label_029F;
            }
            this.UnitHilitTower.get_gameObject().SetActive(0);
            this.UnitHilitTower.SetParent(this.ListParent, 0);
        Label_029F:
            num5 = 0;
            goto Label_0719;
        Label_02A7:
            num6 = ((base.mPage * base.mPageSize) + num5) - this.mExtraItemList.Count;
            if ((0 > num6) || (num6 >= ((int) base.mData.Length)))
            {
                goto Label_0700;
            }
            num7 = -1;
            if (this.mUnitDatas.Count <= num6)
            {
                goto Label_030B;
            }
            num7 = this.mCurrentParty.IndexOf(this.mUnitDatas[num6]);
        Label_030B:
            DataSource.Bind(base.mItems[num5], base.mDataType, base.mData[num6]);
            this.OnUpdateItem(base.mItems[num5], num6);
            base.mItems[num5].SetActive(1);
            GameParameter.UpdateAll(base.mItems[num5]);
            data = DataSource.FindDataOfClass<UnitData>(base.mItems[num5], null);
            if (data != null)
            {
                goto Label_0387;
            }
            goto Label_0713;
        Label_0387:
            num8 = 0;
            goto Label_0658;
        Label_038F:
            if (this.mCurrentParty.Units[num8] != null)
            {
                goto Label_03A7;
            }
            goto Label_0652;
        Label_03A7:
            if (this.mCurrentParty.Units[num8].UniqueID == data.UniqueID)
            {
                goto Label_03CB;
            }
            goto Label_0652;
        Label_03CB:
            transform3 = base.mItems[num5].GetComponent<RectTransform>();
            if (num8 != this.mSelectedSlotIndex)
            {
                goto Label_0482;
            }
            transform4 = null;
            if (this.mSelectViewMode != 1)
            {
                goto Label_0408;
            }
            transform4 = this.UnitHilit;
            goto Label_041C;
        Label_0408:
            if (this.mSelectViewMode != 2)
            {
                goto Label_041C;
            }
            transform4 = this.UnitHilitTower;
        Label_041C:
            if ((transform4 != null) == null)
            {
                goto Label_0482;
            }
            transform4.SetParent(transform3, 0);
            transform4.set_anchorMin(new Vector2(0f, 1f));
            transform4.set_anchorMax(new Vector2(0f, 1f));
            transform4.set_anchoredPosition(new Vector2(100f, 0f));
            transform4.get_gameObject().SetActive(1);
        Label_0482:
            transform5 = null;
            if (this.mSelectViewMode != 1)
            {
                goto Label_0548;
            }
            if ((num8 < 0) || (num8 >= ((int) this.ChosenUnitBadges.Length)))
            {
                goto Label_0652;
            }
            if ((this.ChosenUnitBadges[num8] == null) == null)
            {
                goto Label_04C1;
            }
            goto Label_0652;
        Label_04C1:
            if (num8 < this.mCurrentParty.PartyData.SUBMEMBER_START)
            {
                goto Label_0538;
            }
            num9 = 4;
            num10 = num9 + (num8 - this.mCurrentParty.PartyData.SUBMEMBER_START);
            if ((num10 >= 0) || (num10 < ((int) this.ChosenUnitBadges.Length)))
            {
                goto Label_050F;
            }
            goto Label_0652;
        Label_050F:
            if ((this.ChosenUnitBadges[num10] == null) == null)
            {
                goto Label_0528;
            }
            goto Label_0652;
        Label_0528:
            transform5 = this.ChosenUnitBadges[num10];
            goto Label_0543;
        Label_0538:
            transform5 = this.ChosenUnitBadges[num8];
        Label_0543:
            goto Label_058F;
        Label_0548:
            if (this.mSelectViewMode != 2)
            {
                goto Label_058F;
            }
            if ((num8 < 0) || (num8 >= ((int) this.ChosenUnitBadgesTower.Length)))
            {
                goto Label_0652;
            }
            if ((this.ChosenUnitBadgesTower[num8] == null) == null)
            {
                goto Label_0584;
            }
            goto Label_0652;
        Label_0584:
            transform5 = this.ChosenUnitBadgesTower[num8];
        Label_058F:
            if ((transform5 == null) == null)
            {
                goto Label_05A1;
            }
            goto Label_0652;
        Label_05A1:
            if ((transform5.get_parent() != transform3) == null)
            {
                goto Label_060D;
            }
            transform5.SetParent(transform3, 0);
            transform5.set_anchorMin(new Vector2(0f, 1f));
            transform5.set_anchorMax(new Vector2(0f, 1f));
            transform5.set_anchoredPosition(new Vector2(0f, 20f));
            transform5.get_gameObject().SetActive(1);
        Label_060D:
            if (num8 != null)
            {
                goto Label_0652;
            }
            transform5.get_gameObject().SetActive(((base.mPage > 0) || (this.mIsHeroOnly != null)) ? 0 : ((this.mSelectViewMode == 1) ? 1 : (this.mSelectViewMode == 2)));
        Label_0652:
            num8 += 1;
        Label_0658:
            if (num8 < ((int) this.mCurrentParty.Units.Length))
            {
                goto Label_038F;
            }
            if (this.mCurrentQuest == null)
            {
                goto Label_0713;
            }
            group = base.mItems[num5].GetComponent<CanvasGroup>();
            if (((group != null) == null) || (data == null))
            {
                goto Label_0713;
            }
            flag = (this.mCurrentQuest.IsUnitAllowed(data) != null) ? 1 : ((num7 < 0) == 0);
            str = null;
            flag &= this.mCurrentQuest.IsEntryQuestCondition(data, &str);
            group.set_alpha((flag == null) ? 0.5f : 1f);
            group.set_interactable(flag);
            goto Label_0713;
        Label_0700:
            base.mItems[num5].SetActive(0);
        Label_0713:
            num5 += 1;
        Label_0719:
            if (num5 < base.mItems.Count)
            {
                goto Label_02A7;
            }
            num11 = 0;
            goto Label_078F;
        Label_0733:
            num12 = (base.mPage * base.mPageSize) + num11;
            if ((this.mExtraItemList[num11] != null) == null)
            {
                goto Label_0789;
            }
            this.mExtraItemList[num11].SetActive((0 > num12) ? 0 : (num12 < this.mExtraItemList.Count));
        Label_0789:
            num11 += 1;
        Label_078F:
            if (num11 < this.mExtraItemList.Count)
            {
                goto Label_0733;
            }
            base.UpdateSelection();
            this.UpdatePage();
            if (base.mInvokeSelChange == null)
            {
                goto Label_07C5;
            }
            base.mInvokeSelChange = 0;
            this.TriggerSelectionChange();
        Label_07C5:
            return;
        }

        private void RefreshPieceUnit(bool clear)
        {
            GameManager manager;
            List<ItemParam> list;
            UnitParam[] paramArray;
            PlayerData data;
            List<UnitParam> list2;
            int num;
            UnitParam param;
            UnitParam[] paramArray2;
            <RefreshPieceUnit>c__AnonStorey3AA storeyaa;
            if ((this.PieceTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            manager = MonoSingleton<GameManager>.Instance;
            list = manager.MasterParam.Items;
            paramArray = manager.MasterParam.GetAllUnits();
            data = manager.Player;
            list2 = new List<UnitParam>(this.DataCount);
            num = 0;
            goto Label_00F0;
        Label_004C:
            storeyaa = new <RefreshPieceUnit>c__AnonStorey3AA();
            storeyaa.item = list[num];
            if (storeyaa.item.type == 1)
            {
                goto Label_0079;
            }
            goto Label_00EA;
        Label_0079:
            param = Array.Find<UnitParam>(paramArray, new Predicate<UnitParam>(storeyaa.<>m__418));
            if (param == null)
            {
                goto Label_00EA;
            }
            if (param.IsSummon() != null)
            {
                goto Label_00A6;
            }
            goto Label_00EA;
        Label_00A6:
            if (list2.Contains(param) != null)
            {
                goto Label_00EA;
            }
            if (data.FindUnitDataByUnitID(param.iname) == null)
            {
                goto Label_00CB;
            }
            goto Label_00EA;
        Label_00CB:
            if (param.CheckAvailable(TimeManager.ServerTime) != null)
            {
                goto Label_00E1;
            }
            goto Label_00EA;
        Label_00E1:
            list2.Add(param);
        Label_00EA:
            num += 1;
        Label_00F0:
            if (num < list.Count)
            {
                goto Label_004C;
            }
            if (<>f__am$cache41 != null)
            {
                goto Label_0117;
            }
            <>f__am$cache41 = new Func<UnitParam, int>(UnitListV2.<RefreshPieceUnit>m__419);
        Label_0117:
            paramArray2 = Enumerable.ToArray<UnitParam>(Enumerable.OrderByDescending<UnitParam, int>(list2, <>f__am$cache41));
            if ((this.NoMatchingUnit != null) == null)
            {
                goto Label_0145;
            }
            this.NoMatchingUnit.SetActive(0);
        Label_0145:
            this.mSortValues = null;
            this.SetData(paramArray2, typeof(UnitParam));
            return;
        }

        protected virtual unsafe void RefreshUnit(bool clear)
        {
            List<UnitData> list;
            bool flag;
            List<UnitData> list2;
            int num;
            UnitData data;
            List<UnitData> list3;
            int num2;
            List<int> list4;
            string str;
            int num3;
            int num4;
            List<UnitData> list5;
            List<UnitData> list6;
            List<int> list7;
            bool flag2;
            int num5;
            int num6;
            List<int> list8;
            int num7;
            List<int> list9;
            List<UnitData> list10;
            List<int> list11;
            int num8;
            string str2;
            <RefreshUnit>c__AnonStorey3A8 storeya;
            <RefreshUnit>c__AnonStorey3A9 storeya2;
            if ((this.mSelectViewMode != null) && (this.mSelectViewMode != 3))
            {
                goto Label_024E;
            }
            flag = this.mReverse;
            if ((this.mUnitDatas == null) || (this.mUnitDatas.Count <= 0))
            {
                goto Label_004B;
            }
            list = new List<UnitData>(this.mUnitDatas);
            goto Label_005B;
        Label_004B:
            list = MonoSingleton<GameManager>.Instance.Player.Units;
        Label_005B:
            list2 = new List<UnitData>();
            num = 0;
            goto Label_009E;
        Label_0068:
            data = list[num];
            if ((this.IncludeShujinko != null) || (data.UnitParam.IsHero() == null))
            {
                goto Label_0092;
            }
            goto Label_009A;
        Label_0092:
            list2.Add(data);
        Label_009A:
            num += 1;
        Label_009E:
            if (num < list.Count)
            {
                goto Label_0068;
            }
            list3 = new List<UnitData>(list2);
            if (this.mUnitSortMode == null)
            {
                goto Label_00DC;
            }
            GameUtility.SortUnits(list3, this.mUnitSortMode, this.mReverse, &this.mSortValues, 1);
            goto Label_00E3;
        Label_00DC:
            this.mSortValues = null;
        Label_00E3:
            num2 = list3.Count;
            list4 = null;
            if ((this.mSortValues == null) || (((int) this.mSortValues.Length) <= 0))
            {
                goto Label_0115;
            }
            list4 = new List<int>(this.mSortValues);
        Label_0115:
            FilterUnits(list3, list4, this.mUnitFilter);
            if (list4 == null)
            {
                goto Label_0138;
            }
            this.mSortValues = list4.ToArray();
        Label_0138:
            if ((this.NoMatchingUnit != null) == null)
            {
                goto Label_016C;
            }
            this.NoMatchingUnit.SetActive((num2 <= 0) ? 0 : ((list3.Count > 0) == 0));
        Label_016C:
            if (flag == null)
            {
                goto Label_018F;
            }
            list3.Reverse();
            if (this.mSortValues == null)
            {
                goto Label_018F;
            }
            Array.Reverse(this.mSortValues);
        Label_018F:
            str = FlowNode_Variable.Get("LAST_SELECTED_UNITID");
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0232;
            }
            storeya = new <RefreshUnit>c__AnonStorey3A8();
            storeya.lastselected_uniqueId = long.Parse(str);
            if (((storeya.lastselected_uniqueId <= 0L) || (this.mSelectUnitUniqueID <= 0L)) || (storeya.lastselected_uniqueId == this.mSelectUnitUniqueID))
            {
                goto Label_021B;
            }
            num3 = list3.FindIndex(new Predicate<UnitData>(storeya.<>m__415));
            if (num3 < 0)
            {
                goto Label_021B;
            }
            num4 = num3 / base.CellCount;
            base.SetPageIndex(num4, 0);
        Label_021B:
            FlowNode_Variable.Set("LAST_SELECTED_UNITID", string.Empty);
            this.mSelectUnitUniqueID = -1L;
        Label_0232:
            this.SetData(list3.ToArray(), typeof(UnitData));
            goto Label_0667;
        Label_024E:
            list5 = new List<UnitData>(this.mUnitDatas);
            list6 = new List<UnitData>();
            list7 = new List<int>();
            flag2 = this.mReverse;
            if (this.mUnitSortMode == null)
            {
                goto Label_0297;
            }
            GameUtility.SortUnits(list5, this.mUnitSortMode, flag2, &this.mSortValues, 1);
            goto Label_029E;
        Label_0297:
            this.mSortValues = null;
        Label_029E:
            num5 = 0;
            num6 = this.mCurrentParty.PartyData.MAINMEMBER_START;
            goto Label_02D7;
        Label_02B8:
            if (this.mCurrentParty.Units[num6] == null)
            {
                goto Label_02D1;
            }
            num5 += 1;
        Label_02D1:
            num6 += 1;
        Label_02D7:
            if (num6 <= this.mCurrentParty.PartyData.MAINMEMBER_END)
            {
                goto Label_02B8;
            }
            list8 = ((this.mSortValues == null) || (((int) this.mSortValues.Length) <= 0)) ? null : new List<int>(this.mSortValues);
            storeya2 = new <RefreshUnit>c__AnonStorey3A9();
            storeya2.<>f__this = this;
            storeya2.i = 0;
            goto Label_046B;
        Label_0336:
            if (this.mCurrentParty.Units[storeya2.i] != null)
            {
                goto Label_0353;
            }
            goto Label_045B;
        Label_0353:
            if (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex)
            {
                goto Label_03B9;
            }
            if (this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END)
            {
                goto Label_03B9;
            }
            if (storeya2.i != null)
            {
                goto Label_03B9;
            }
            if (num5 > 1)
            {
                goto Label_03B9;
            }
            if (this.mCurrentParty.Units[this.mSelectedSlotIndex] != null)
            {
                goto Label_03B9;
            }
            goto Label_045B;
        Label_03B9:
            list6.Add(list5.Find(new Predicate<UnitData>(storeya2.<>m__416)));
            if (this.mUnitSortMode == null)
            {
                goto Label_041D;
            }
            if (this.mCurrentParty.Units[storeya2.i] == null)
            {
                goto Label_041D;
            }
            list7.Add(this.GetSortModeValue(this.mCurrentParty.Units[storeya2.i], this.mUnitSortMode));
        Label_041D:
            num7 = list5.FindIndex(new Predicate<UnitData>(storeya2.<>m__417));
            if (num7 < 0)
            {
                goto Label_045B;
            }
            if (list8 == null)
            {
                goto Label_044B;
            }
            list8.RemoveAt(num7);
        Label_044B:
            if (list5 == null)
            {
                goto Label_045B;
            }
            list5.RemoveAt(num7);
        Label_045B:
            storeya2.i += 1;
        Label_046B:
            if (storeya2.i < this.mCurrentParty.PartyData.MAX_UNIT)
            {
                goto Label_0336;
            }
            if (this.mSortValues == null)
            {
                goto Label_04A6;
            }
            if (list8 == null)
            {
                goto Label_04A6;
            }
            this.mSortValues = list8.ToArray();
        Label_04A6:
            list9 = null;
            if (this.mSortValues == null)
            {
                goto Label_04CF;
            }
            if (((int) this.mSortValues.Length) <= 0)
            {
                goto Label_04CF;
            }
            list9 = new List<int>(this.mSortValues);
        Label_04CF:
            FilterUnits(list5, list9, this.mUnitFilter);
            if (list9 == null)
            {
                goto Label_04F2;
            }
            this.mSortValues = list9.ToArray();
        Label_04F2:
            if (flag2 == null)
            {
                goto Label_0516;
            }
            list5.Reverse();
            if (this.mSortValues == null)
            {
                goto Label_0516;
            }
            Array.Reverse(this.mSortValues);
        Label_0516:
            list10 = new List<UnitData>();
            if (this.SelectableToggle.get_isOn() == null)
            {
                goto Label_05DE;
            }
            if (this.UseQuestInfo == null)
            {
                goto Label_05FF;
            }
            list11 = new List<int>();
            num8 = 0;
            goto Label_05AE;
        Label_0547:
            str2 = string.Empty;
            if (this.mCurrentQuest.IsEntryQuestCondition(list5[num8], &str2) != null)
            {
                goto Label_056E;
            }
            goto Label_05A8;
        Label_056E:
            list10.Add(list5[num8]);
            if (this.mSortValues == null)
            {
                goto Label_05A8;
            }
            if (((int) this.mSortValues.Length) <= num8)
            {
                goto Label_05A8;
            }
            list11.Add(this.mSortValues[num8]);
        Label_05A8:
            num8 += 1;
        Label_05AE:
            if (num8 < list5.Count)
            {
                goto Label_0547;
            }
            if (list11 == null)
            {
                goto Label_05FF;
            }
            if (list11.Count <= 0)
            {
                goto Label_05FF;
            }
            list7.AddRange(list11);
            goto Label_05FF;
        Label_05DE:
            list10.AddRange(list5);
            if (this.mSortValues == null)
            {
                goto Label_05FF;
            }
            list7.AddRange(this.mSortValues);
        Label_05FF:
            list6.AddRange(list10);
            if (this.mSortValues == null)
            {
                goto Label_0627;
            }
            if (list7 == null)
            {
                goto Label_0627;
            }
            this.mSortValues = list7.ToArray();
        Label_0627:
            if ((this.NoMatchingUnit != null) == null)
            {
                goto Label_0650;
            }
            this.NoMatchingUnit.SetActive((list6.Count > 0) == 0);
        Label_0650:
            this.SetData(list6.ToArray(), typeof(UnitData));
        Label_0667:
            return;
        }

        private void RefreshViewPieceList()
        {
            this.UpdateViewMode(3);
            if ((this.CloseButton != null) == null)
            {
                goto Label_004A;
            }
            if (this.CloseButton.get_onClick().GetPersistentEventCount() > 0)
            {
                goto Label_004A;
            }
            this.CloseButton.get_onClick().AddListener(new UnityAction(this, this.RefreshViewUnitList));
        Label_004A:
            base.Page = 0;
            this.RefreshData();
            return;
        }

        private void RefreshViewSelectList()
        {
            this.UpdateViewMode(1);
            return;
        }

        private void RefreshViewUnitList()
        {
            this.UpdateViewMode(0);
            this.OnChangedToggle(this.mSelectTabIndex);
            return;
        }

        public void SetPartyData(PartyEditData party)
        {
            this.mCurrentParty = party;
            if (this.mCurrentParty != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            return;
        }

        public void SetQuestData(QuestParam quest)
        {
            this.mCurrentQuest = quest;
            return;
        }

        public void SetRefresh()
        {
            if (base.HasStarted == null)
            {
                goto Label_0017;
            }
            this.RefreshData();
            this.RefreshItems();
        Label_0017:
            return;
        }

        public void SetSortMethod(string method, bool ascending, string[] filters)
        {
            this.SetSortMethod(method, ascending, filters, 0);
            return;
        }

        public void SetSortMethod(string method, bool ascending, string[] filters, bool is_ignore_element)
        {
            GameUtility.UnitSortModes modes;
            modes = 0;
        Label_0002:
            try
            {
                if (string.IsNullOrEmpty(method) != null)
                {
                    goto Label_0024;
                }
                modes = (int) Enum.Parse(typeof(GameUtility.UnitSortModes), method, 1);
            Label_0024:
                goto Label_0049;
            }
            catch (Exception)
            {
            Label_0029:
                if (GameUtility.IsDebugBuild == null)
                {
                    goto Label_0044;
                }
                DebugUtility.LogError("Unknown sort mode: " + method);
            Label_0044:
                goto Label_0049;
            }
        Label_0049:
            if ((this.AscendingIcon != null) == null)
            {
                goto Label_0066;
            }
            this.AscendingIcon.SetActive(ascending);
        Label_0066:
            if ((this.DescendingIcon != null) == null)
            {
                goto Label_0086;
            }
            this.DescendingIcon.SetActive(ascending == 0);
        Label_0086:
            if (modes != null)
            {
                goto Label_0092;
            }
            ascending = ascending == 0;
        Label_0092:
            if ((this.SortModeCaption != null) == null)
            {
                goto Label_00CD;
            }
            this.SortModeCaption.set_text(LocalizedText.Get("sys.SORT_" + ((GameUtility.UnitSortModes) modes).ToString().ToUpper()));
        Label_00CD:
            this.mReverse = ascending;
            this.mUnitSortMode = modes;
            if (is_ignore_element == null)
            {
                goto Label_0117;
            }
            if (filters != null)
            {
                goto Label_0100;
            }
            if (this.mSelectTabIndex == null)
            {
                goto Label_0100;
            }
            filters = this.mUnitPickerFilter.GetFiltersAll();
        Label_0100:
            this.mUnitFilter = UpdateFilterElement(this.mSelectTabIndex, filters);
            goto Label_011E;
        Label_0117:
            this.mUnitFilter = filters;
        Label_011E:
            this.RefreshData();
            return;
        }

        public void SetUnitList(UnitData[] units)
        {
            this.mUnitDatas = new List<UnitData>(units);
            if (this.mUnitDatas == null)
            {
                goto Label_0028;
            }
            if (this.mUnitDatas.Count > 0)
            {
                goto Label_0029;
            }
        Label_0028:
            return;
        Label_0029:
            return;
        }

        private void SetupFilter(string[] filters)
        {
            bool flag;
            flag = this.mReverse;
            if ((this.mUnitPickerSort != null) == null)
            {
                goto Label_0024;
            }
            flag = this.mUnitPickerSort.IsAscending;
        Label_0024:
            this.SetSortMethod(((GameUtility.UnitSortModes) this.mUnitSortMode).ToString(), flag, filters, 1);
            return;
        }

        private void SetupSortMethod(string method, bool ascending)
        {
            this.SetSortMethod(method, ascending, this.mUnitFilter, 1);
            return;
        }

        public void SetViewMode(ViewMode value)
        {
            this.mSelectViewMode = value;
            return;
        }

        protected override unsafe void Start()
        {
            char[] chArray2;
            char[] chArray1;
            GameObject obj2;
            GameObject obj3;
            UnitPickerSort sort;
            GameObject obj4;
            GameObject obj5;
            UnitPickerFilter filter;
            int num;
            int num2;
            GameUtility.UnitSortModes modes;
            string str;
            bool flag;
            string str2;
            string[] strArray;
            string str3;
            string[] strArray2;
            string[] strArray3;
            GameSettings settings;
            int num3;
            string str4;
            bool flag2;
            bool flag3;
            base.Start();
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            if ((this.PieceButton != null) == null)
            {
                goto Label_0045;
            }
            this.PieceButton.get_onClick().AddListener(new UnityAction(this, this.RefreshViewPieceList));
        Label_0045:
            if ((this.SortButton != null) == null)
            {
                goto Label_0072;
            }
            this.SortButton.get_onClick().AddListener(new UnityAction(this, this.OnUnitSortOpen));
        Label_0072:
            if ((this.FilterButton != null) == null)
            {
                goto Label_009F;
            }
            this.FilterButton.get_onClick().AddListener(new UnityAction(this, this.OnUnitFilterOpen));
        Label_009F:
            if ((this.CloseButton != null) == null)
            {
                goto Label_00C7;
            }
            this.CloseButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnCloseButton));
        Label_00C7:
            if ((this.RemoveTemplate != null) == null)
            {
                goto Label_00EF;
            }
            this.RemoveTemplate.AddListener(new SRPG_Button.ButtonClickEvent(this.OnRemoveUnitSelect));
        Label_00EF:
            if ((this.RemoveTowerTemplate != null) == null)
            {
                goto Label_0117;
            }
            this.RemoveTowerTemplate.AddListener(new SRPG_Button.ButtonClickEvent(this.OnRemoveUnitSelect));
        Label_0117:
            if (string.IsNullOrEmpty(UNIT_SORT_PATH) != null)
            {
                goto Label_01A0;
            }
            obj2 = AssetManager.Load<GameObject>(UNIT_SORT_PATH);
            if ((obj2 != null) == null)
            {
                goto Label_01A0;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 != null) == null)
            {
                goto Label_01A0;
            }
            this.mSortWindow = obj3;
            sort = obj3.GetComponent<UnitPickerSort>();
            if ((sort != null) == null)
            {
                goto Label_01A0;
            }
            sort.OnAccept = new UnitPickerSort.SortEvent(this.SetupSortMethod);
            sort.MenuID = this.MenuID;
            this.mUnitPickerSort = sort;
            if ((this.m_SortChanger != null) == null)
            {
                goto Label_01A0;
            }
        Label_01A0:
            if (string.IsNullOrEmpty(UNIT_FILTER_PATH) != null)
            {
                goto Label_0232;
            }
            obj4 = AssetManager.Load<GameObject>(UNIT_FILTER_PATH);
            if ((obj4 != null) == null)
            {
                goto Label_0232;
            }
            obj5 = Object.Instantiate<GameObject>(obj4);
            if ((obj5 != null) == null)
            {
                goto Label_0232;
            }
            this.mFilterWindow = obj5;
            filter = obj5.GetComponent<UnitPickerFilter>();
            if ((filter != null) == null)
            {
                goto Label_0232;
            }
            filter.OnAccept = new UnitPickerFilter.FilterEvent(this.SetupFilter);
            filter.MenuID = this.MenuID;
            this.mUnitPickerFilter = filter;
            if ((this.m_FilterChanger != null) == null)
            {
                goto Label_0232;
            }
        Label_0232:
            if ((this.ChosenUnitBadges == null) || (((int) this.ChosenUnitBadges.Length) <= 0))
            {
                goto Label_02A9;
            }
            num = 0;
            goto Label_029A;
        Label_0253:
            if ((this.ChosenUnitBadges[num] == null) == null)
            {
                goto Label_026C;
            }
            goto Label_0294;
        Label_026C:
            this.MoveToOrigin(this.ChosenUnitBadges[num].get_gameObject());
            this.ChosenUnitBadges[num].get_gameObject().SetActive(0);
        Label_0294:
            num += 1;
        Label_029A:
            if (num < ((int) this.ChosenUnitBadges.Length))
            {
                goto Label_0253;
            }
        Label_02A9:
            if ((this.ChosenUnitBadgesTower == null) || (((int) this.ChosenUnitBadgesTower.Length) <= 0))
            {
                goto Label_0320;
            }
            num2 = 0;
            goto Label_0311;
        Label_02CA:
            if ((this.ChosenUnitBadgesTower[num2] == null) == null)
            {
                goto Label_02E3;
            }
            goto Label_030B;
        Label_02E3:
            this.MoveToOrigin(this.ChosenUnitBadgesTower[num2].get_gameObject());
            this.ChosenUnitBadgesTower[num2].get_gameObject().SetActive(0);
        Label_030B:
            num2 += 1;
        Label_0311:
            if (num2 < ((int) this.ChosenUnitBadgesTower.Length))
            {
                goto Label_02CA;
            }
        Label_0320:
            if ((this.SelectableToggle != null) == null)
            {
                goto Label_035E;
            }
            this.SelectableToggle.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnValueChanged));
            this.SelectableToggle.get_gameObject().SetActive(0);
        Label_035E:
            modes = 0;
            if (PlayerPrefsUtility.HasKey(this.MenuID) == null)
            {
                goto Label_03C4;
            }
            str = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        Label_0383:
            try
            {
                if (string.IsNullOrEmpty(str) != null)
                {
                    goto Label_03A8;
                }
                modes = (int) Enum.Parse(typeof(GameUtility.UnitSortModes), str, 1);
            Label_03A8:
                goto Label_03C4;
            }
            catch (Exception)
            {
            Label_03AD:
                DebugUtility.LogError("Unknown sort mode:" + str);
                goto Label_03C4;
            }
        Label_03C4:
            this.mUnitSortMode = modes;
            flag = 0;
            if (PlayerPrefsUtility.HasKey(this.MenuID + "#") == null)
            {
                goto Label_0415;
            }
            flag = (PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) != null) ? 1 : 0;
            this.mReverse = flag;
        Label_0415:
            if (this.mUnitSortMode != null)
            {
                goto Label_042B;
            }
            this.mReverse = flag == 0;
        Label_042B:
            if ((this.mUnitPickerSort != null) == null)
            {
                goto Label_045E;
            }
            this.mUnitPickerSort.SetUp(((GameUtility.UnitSortModes) this.mUnitSortMode).ToString().ToUpper(), flag);
        Label_045E:
            str2 = this.MenuID + "&";
            strArray = null;
            if (PlayerPrefsUtility.HasKey(str2) == null)
            {
                goto Label_051E;
            }
            str3 = PlayerPrefsUtility.GetString(str2, string.Empty);
            if (str3 == null)
            {
                goto Label_051E;
            }
            if (str3.IndexOf(0x7c) == -1)
            {
                goto Label_050A;
            }
            chArray1 = new char[] { 0x7c };
            strArray2 = str3.Split(chArray1);
            this.mUnitPickerFilter.SetFilters(strArray2, (strArray2 != null) ? 1 : 0);
            this.mUnitPickerFilter.SaveState();
            strArray3 = this.mUnitPickerFilter.GetFilters(0);
            str3 = (strArray3 == null) ? string.Empty : string.Join("&", strArray3);
        Label_050A:
            chArray2 = new char[] { 0x26 };
            strArray = str3.Split(chArray2);
        Label_051E:
            if ((this.mUnitPickerFilter != null) == null)
            {
                goto Label_055F;
            }
            this.mUnitPickerFilter.SetFilters(strArray, 1);
            this.mUnitPickerFilter.SaveState();
            this.mUnitFilter = UpdateFilterElement(0, this.mUnitPickerFilter.GetFiltersAll());
        Label_055F:
            if ((this.SortFilter != null) == null)
            {
                goto Label_0616;
            }
            settings = GameSettings.Instance;
            num3 = 0;
            goto Label_05C8;
        Label_057F:
            str4 = LocalizedText.Get("sys.SORT_" + ((GameUtility.UnitSortModes) &(settings.UnitSort_Modes[num3]).Mode).ToString().ToUpper());
            this.SortFilter.AddItem(str4, num3);
            num3 += 1;
        Label_05C8:
            if (num3 < ((int) settings.UnitSort_Modes.Length))
            {
                goto Label_057F;
            }
            this.SortFilter.Selection = Math.Max(Array.FindIndex<GameUtility.UnitSortModes>(GameUtility.UnitSortMenuItems, new Predicate<GameUtility.UnitSortModes>(this.<Start>m__414)), 0);
            this.SortFilter.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnSortModeChange);
        Label_0616:
            if ((this.UnitBadge != null) == null)
            {
                goto Label_0649;
            }
            flag2 = MonoSingleton<GameManager>.Instance.CheckBadges(1);
            this.UnitBadge.SetActive(flag2);
            this.mPrevUnitBadgeState = flag2;
        Label_0649:
            if ((this.UnitUnlockBadge != null) == null)
            {
                goto Label_067C;
            }
            flag3 = MonoSingleton<GameManager>.Instance.CheckBadges(2);
            this.UnitUnlockBadge.SetActive(flag3);
            this.mPrevUnlockBadgeState = flag3;
        Label_067C:
            this.mSelectTabIndex = 0;
            this.UpdateViewMode(this.mSelectViewMode);
            this.RefreshData();
            return;
        }

        protected override void Update()
        {
            bool flag;
            bool flag2;
            bool flag3;
            base.Update();
            if ((this.UnitBadge != null) != null)
            {
                goto Label_0028;
            }
            if ((this.UnitUnlockBadge != null) == null)
            {
                goto Label_00D2;
            }
        Label_0028:
            flag = 0;
            if (MonoSingleton<GameManager>.Instance.CheckBusyBadges(1) != null)
            {
                goto Label_00C6;
            }
            if (MonoSingleton<GameManager>.Instance.CheckBusyBadges(2) != null)
            {
                goto Label_00C6;
            }
            if ((this.UnitBadge != null) == null)
            {
                goto Label_0088;
            }
            flag2 = MonoSingleton<GameManager>.Instance.CheckBadges(1);
            this.UnitBadge.SetActive(flag2);
            if (this.mPrevUnitBadgeState == flag2)
            {
                goto Label_0081;
            }
            flag = 1;
        Label_0081:
            this.mPrevUnitBadgeState = flag2;
        Label_0088:
            if ((this.UnitUnlockBadge != null) == null)
            {
                goto Label_00C6;
            }
            flag3 = MonoSingleton<GameManager>.Instance.CheckBadges(2);
            this.UnitUnlockBadge.SetActive(flag3);
            if (this.mPrevUnlockBadgeState == flag3)
            {
                goto Label_00BF;
            }
            flag = 1;
        Label_00BF:
            this.mPrevUnlockBadgeState = flag3;
        Label_00C6:
            if (flag == null)
            {
                goto Label_00D2;
            }
            this.RefreshData();
        Label_00D2:
            return;
        }

        public static string[] UpdateFilterElement(int index, string[] filter)
        {
            string[] textArray1;
            string[] strArray;
            List<string> list;
            int num;
            if (filter != null)
            {
                goto Label_0008;
            }
            return filter;
        Label_0008:
            textArray1 = new string[] { "ELEM:Fire", "ELEM:Water", "ELEM:Wind", "ELEM:Thunder", "ELEM:Shine", "ELEM:Dark" };
            strArray = textArray1;
            list = new List<string>(0);
            if (filter == null)
            {
                goto Label_005C;
            }
            if (((int) filter.Length) <= 0)
            {
                goto Label_005C;
            }
            list.AddRange(filter);
        Label_005C:
            if (<>f__am$cache42 != null)
            {
                goto Label_0075;
            }
            <>f__am$cache42 = new Predicate<string>(UnitListV2.<UpdateFilterElement>m__41A);
        Label_0075:
            list.RemoveAll(<>f__am$cache42);
            if (index != 7)
            {
                goto Label_00B6;
            }
            list.Add("TAB:Favorite");
            num = 0;
            goto Label_00A6;
        Label_0099:
            list.Add(strArray[num]);
            num += 1;
        Label_00A6:
            if (num < ((int) strArray.Length))
            {
                goto Label_0099;
            }
            return list.ToArray();
        Label_00B6:
            if (index == null)
            {
                goto Label_00C3;
            }
            if (index != 1)
            {
                goto Label_00CC;
            }
        Label_00C3:
            list.Add(strArray[0]);
        Label_00CC:
            if (index == null)
            {
                goto Label_00D9;
            }
            if (index != 2)
            {
                goto Label_00E2;
            }
        Label_00D9:
            list.Add(strArray[1]);
        Label_00E2:
            if (index == null)
            {
                goto Label_00EF;
            }
            if (index != 3)
            {
                goto Label_00F8;
            }
        Label_00EF:
            list.Add(strArray[2]);
        Label_00F8:
            if (index == null)
            {
                goto Label_0105;
            }
            if (index != 4)
            {
                goto Label_010E;
            }
        Label_0105:
            list.Add(strArray[3]);
        Label_010E:
            if (index == null)
            {
                goto Label_011B;
            }
            if (index != 5)
            {
                goto Label_0124;
            }
        Label_011B:
            list.Add(strArray[4]);
        Label_0124:
            if (index == null)
            {
                goto Label_0131;
            }
            if (index != 6)
            {
                goto Label_013A;
            }
        Label_0131:
            list.Add(strArray[5]);
        Label_013A:
            return list.ToArray();
        }

        public void UpdateViewMode()
        {
            this.UpdateViewMode(this.mSelectViewMode);
            return;
        }

        public void UpdateViewMode(ViewMode mode)
        {
            GameObject obj2;
            if ((this.TabParentObject != null) == null)
            {
                goto Label_0023;
            }
            this.TabParentObject.SetActive((mode == 3) == 0);
        Label_0023:
            if ((this.TitleObject != null) == null)
            {
                goto Label_0043;
            }
            this.TitleObject.SetActive(mode == 3);
        Label_0043:
            if ((this.CloseButton != null) == null)
            {
                goto Label_006B;
            }
            this.CloseButton.get_gameObject().SetActive((mode == 0) == 0);
        Label_006B:
            if ((this.SelectableToggle != null) == null)
            {
                goto Label_009A;
            }
            this.SelectableToggle.get_gameObject().SetActive((mode == 1) ? 1 : (mode == 2));
        Label_009A:
            if ((this.SortButton != null) == null)
            {
                goto Label_00C2;
            }
            this.SortButton.get_gameObject().SetActive((mode == 3) == 0);
        Label_00C2:
            if ((this.FilterButton != null) == null)
            {
                goto Label_00EA;
            }
            this.FilterButton.get_gameObject().SetActive((mode == 3) == 0);
        Label_00EA:
            if ((this.PieceButton != null) == null)
            {
                goto Label_010F;
            }
            this.PieceButton.get_gameObject().SetActive(mode == 0);
        Label_010F:
            if ((this.HelpButton != null) == null)
            {
                goto Label_012F;
            }
            this.HelpButton.SetActive(mode == 0);
        Label_012F:
            obj2 = null;
            if (mode != 1)
            {
                goto Label_0144;
            }
            obj2 = this.RemoveTemplate.get_gameObject();
        Label_0144:
            if (mode != 2)
            {
                goto Label_0157;
            }
            obj2 = this.RemoveTowerTemplate.get_gameObject();
        Label_0157:
            if (((this.mSelectedSlotIndex == null) || (this.mCurrentParty.Units[this.mSelectedSlotIndex] == null)) && ((this.mIsHeroOnly == null) || (this.mCurrentParty.Units[this.mSelectedSlotIndex] == null)))
            {
                goto Label_01B7;
            }
            this.mExtraItemList.Clear();
            this.mExtraItemList.Add(obj2);
            goto Label_01F0;
        Label_01B7:
            if ((this.mExtraItemList == null) || (this.mExtraItemList.Count != 1))
            {
                goto Label_01E5;
            }
            this.mExtraItemList[0].SetActive(0);
        Label_01E5:
            this.mExtraItemList.Clear();
        Label_01F0:
            if ((this.mBackToHome != null) == null)
            {
                goto Label_0219;
            }
            this.mBackToHome.SetActive((mode == null) ? 1 : (mode == 3));
        Label_0219:
            this.ItemType = (mode == 3) ? 1 : 0;
            this.mSelectViewMode = mode;
            return;
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
                return;
            }
        }

        public int SelectedSlotIndex
        {
            set
            {
                this.mSelectedSlotIndex = value;
                return;
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
                return;
            }
        }

        public override RectTransform ListParent
        {
            get
            {
                return (((this.ItemLayoutParent != null) == null) ? null : this.ItemLayoutParent.GetComponent<RectTransform>());
            }
        }

        [CompilerGenerated]
        private sealed class <Awake>c__AnonStorey3A7
        {
            internal int index;
            internal UnitListV2 <>f__this;

            public <Awake>c__AnonStorey3A7()
            {
                base..ctor();
                return;
            }

            internal void <>m__413(bool value)
            {
                if (value == null)
                {
                    goto Label_0017;
                }
                this.<>f__this.OnChangedToggle(this.index);
            Label_0017:
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshPieceUnit>c__AnonStorey3AA
        {
            internal ItemParam item;

            public <RefreshPieceUnit>c__AnonStorey3AA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__418(UnitParam p)
            {
                return (p.piece == this.item.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshUnit>c__AnonStorey3A8
        {
            internal long lastselected_uniqueId;

            public <RefreshUnit>c__AnonStorey3A8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__415(UnitData p)
            {
                return (p.UniqueID == this.lastselected_uniqueId);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshUnit>c__AnonStorey3A9
        {
            internal int i;
            internal UnitListV2 <>f__this;

            public <RefreshUnit>c__AnonStorey3A9()
            {
                base..ctor();
                return;
            }

            internal bool <>m__416(UnitData v)
            {
                return (v.UniqueID == this.<>f__this.mCurrentParty.Units[this.i].UniqueID);
            }

            internal bool <>m__417(UnitData v)
            {
                return (v.UniqueID == this.<>f__this.mCurrentParty.Units[this.i].UniqueID);
            }
        }

        public delegate void CommonEvents(SRPG_Button button);

        public delegate void ItemSelectEvents(GameObject go);

        public enum ItemTypes
        {
            PlayerUnits,
            PieceUnits
        }

        public enum PartyBadge
        {
            Main0,
            Main1,
            Main2,
            Main3,
            Sub0,
            Sub1,
            Max
        }

        public delegate void UnitSelectEvent(long uniqueID);

        public enum ViewMode
        {
            UNITLIST,
            SELECTLIST,
            TOWERSELECTLIST,
            PIECELIST
        }
    }
}

