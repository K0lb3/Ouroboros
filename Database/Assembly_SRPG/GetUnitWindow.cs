namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(10, "Unit Detail", 1, 10), Pin(100, "Refresh", 0, 100), Pin(2, "Unit Unlocked", 1, 2)]
    public class GetUnitWindow : SRPG_FixedList, IFlowInterface, ISortableList
    {
        public UnitSelectEvent OnUnitSelect;
        public Transform ItemLayoutParent;
        public GameObject ItemTemplate;
        public GameObject PieceTemplate;
        public Pulldown SortFilter;
        public GameObject UnitSortFilterButton;
        public GameObject AscendingIcon;
        public GameObject DescendingIcon;

        public GetUnitWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0019;
            }
            if (base.HasStarted == null)
            {
                goto Label_0019;
            }
            this.RefreshData();
        Label_0019:
            return;
        }

        protected override void Awake()
        {
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
            return;
        }

        protected override GameObject CreateItem()
        {
            return Object.Instantiate<GameObject>(this.PieceTemplate);
        }

        public static unsafe void FilterUnits(List<UnitData> units, List<int> sortValues, string[] filter)
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
            if (((1 << (data.Rarity & 0x1f)) & num) == null)
            {
                goto Label_01F0;
            }
            if (((1 << (data.Element & 0x1f)) & num2) == null)
            {
                goto Label_01F0;
            }
            if (data.CurrentJob.GetAttackSkill() == null)
            {
                goto Label_01F0;
            }
            if (((1 << (data.CurrentJob.GetAttackSkill().AttackDetailType & 0x1f)) & num3) == null)
            {
                goto Label_01F0;
            }
            if (string.IsNullOrEmpty(data.UnitParam.birth) != null)
            {
                goto Label_01F0;
            }
            if (list.Contains(&data.UnitParam.birth.ToString()) != null)
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

        public void OnHoldIcon(GameObject go)
        {
            string[] textArray1;
            UnitParam param;
            FlowNode_DownloadAssets assets;
            param = DataSource.FindDataOfClass<UnitParam>(go, null);
            if (param == null)
            {
                goto Label_0048;
            }
            GlobalVars.UnlockUnitID = param.iname;
            assets = base.GetComponent<FlowNode_DownloadAssets>();
            if ((assets != null) == null)
            {
                goto Label_0040;
            }
            textArray1 = new string[] { GlobalVars.UnlockUnitID };
            assets.DownloadUnits = textArray1;
        Label_0040:
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
        Label_0048:
            return;
        }

        protected override void OnItemSelect(GameObject go)
        {
            this.OnSelectPieceUnit(go);
            return;
        }

        private void OnSelectPieceUnit(GameObject go)
        {
            string[] textArray1;
            UnitParam param;
            FlowNode_DownloadAssets assets;
            param = DataSource.FindDataOfClass<UnitParam>(go, null);
            if (param == null)
            {
                goto Label_0047;
            }
            GlobalVars.UnlockUnitID = param.iname;
            assets = base.GetComponent<FlowNode_DownloadAssets>();
            if ((assets != null) == null)
            {
                goto Label_0040;
            }
            textArray1 = new string[] { GlobalVars.UnlockUnitID };
            assets.DownloadUnits = textArray1;
        Label_0040:
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
        Label_0047:
            return;
        }

        private void OnSelectUnit(GameObject go)
        {
            string[] textArray1;
            UnitData data;
            FlowNode_DownloadAssets assets;
            data = DataSource.FindDataOfClass<UnitData>(go, null);
            if (data == null)
            {
                goto Label_007D;
            }
            if (this.OnUnitSelect == null)
            {
                goto Label_002F;
            }
            this.OnUnitSelect(data.UniqueID);
            goto Label_007D;
        Label_002F:
            GlobalVars.SelectedUnitUniqueID.Set(data.UniqueID);
            GlobalVars.SelectedUnitJobIndex.Set(data.JobIndex);
            assets = base.GetComponent<FlowNode_DownloadAssets>();
            if ((assets != null) == null)
            {
                goto Label_0076;
            }
            textArray1 = new string[] { GlobalVars.UnlockUnitID };
            assets.DownloadUnits = textArray1;
        Label_0076:
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
        Label_007D:
            return;
        }

        private void OnSortModeChange(int index)
        {
            this.RefreshData();
            return;
        }

        public void RefreshData()
        {
            base.ClearItems();
            return;
        }

        public void RefreshPieceUnit(bool clear, UnitSelectListData UnitSelectListData)
        {
            GameManager manager;
            UnitParam[] paramArray;
            List<UnitParam> list;
            int num;
            UnitParam param;
            <RefreshPieceUnit>c__AnonStorey34B storeyb;
            if ((this.PieceTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnits();
            list = new List<UnitParam>(this.DataCount);
            num = 0;
            goto Label_007E;
        Label_0037:
            storeyb = new <RefreshPieceUnit>c__AnonStorey34B();
            storeyb.item = UnitSelectListData.items[num];
            param = Array.Find<UnitParam>(paramArray, new Predicate<UnitParam>(storeyb.<>m__346));
            if (param != null)
            {
                goto Label_0072;
            }
            goto Label_007A;
        Label_0072:
            list.Add(param);
        Label_007A:
            num += 1;
        Label_007E:
            if (num < UnitSelectListData.items.Count)
            {
                goto Label_0037;
            }
            this.SetData(list.ToArray(), typeof(UnitParam));
            return;
        }

        public void SetSortMethod(string method, bool ascending, string[] filters)
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
            this.RefreshData();
            return;
        }

        protected override unsafe void Start()
        {
            GameSettings settings;
            int num;
            string str;
            base.Start();
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            if ((this.SortFilter != null) == null)
            {
                goto Label_009D;
            }
            settings = GameSettings.Instance;
            num = 0;
            goto Label_0078;
        Label_0036:
            str = LocalizedText.Get("sys.SORT_" + ((GameUtility.UnitSortModes) &(settings.UnitSort_Modes[num]).Mode).ToString().ToUpper());
            this.SortFilter.AddItem(str, num);
            num += 1;
        Label_0078:
            if (num < ((int) settings.UnitSort_Modes.Length))
            {
                goto Label_0036;
            }
            this.SortFilter.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnSortModeChange);
        Label_009D:
            this.RefreshData();
            return;
        }

        protected override void Update()
        {
            base.Update();
            return;
        }

        public override RectTransform ListParent
        {
            get
            {
                return (((this.ItemLayoutParent != null) == null) ? null : this.ItemLayoutParent.GetComponent<RectTransform>());
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshPieceUnit>c__AnonStorey34B
        {
            internal UnitSelectListItemData item;

            public <RefreshPieceUnit>c__AnonStorey34B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__346(UnitParam p)
            {
                return (p.iname == this.item.iname);
            }
        }

        public delegate void UnitSelectEvent(long uniqueID);
    }
}

