namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1), Pin(0x65, "売却数の選択", 1, 0x65), Pin(100, "売却", 1, 100), Pin(2, "Open", 0, 1)]
    public class ShopSellWindow : SRPG_FixedList, IFlowInterface
    {
        private const int SELL_ITEM_MAX = 10;
        public RectTransform ItemLayoutParent;
        public GameObject ItemTemplate;
        public Toggle ToggleShowAll;
        public Toggle ToggleShowUsed;
        public Toggle ToggleShowEquip;
        public Toggle ToggleShowUnitPierce;
        public Toggle ToggleShowItemPierce;
        public Toggle ToggleShowMaterial;
        public Button BtnSort;
        public Button BtnCleared;
        public Button BtnSell;
        public Text TxtSort;
        public string Msg_NoSelection;
        private List<GameObject> mSellItemGameObjects;
        private List<SellItem> mSellItemListSelected;
        private List<SellItem> mSellItemList;
        public SellListConfig ListConfig;
        private FilterTypes mFilterType;
        private static List<EItemType>[] FilterItemTypes;
        private static string[] SortTypeTexts;
        private int[] mSortValues;
        private int filteredCnt;
        private SortTypes sortType;
        [CompilerGenerated]
        private static Comparison<ItemData> <>f__am$cache17;
        [CompilerGenerated]
        private static Comparison<ItemData> <>f__am$cache18;

        static ShopSellWindow()
        {
            string[] textArray1;
            List<EItemType>[] listArray1;
            List<EItemType> list;
            listArray1 = new List<EItemType>[6];
            list = new List<EItemType>();
            list.Add(5);
            list.Add(6);
            list.Add(7);
            list.Add(8);
            list.Add(13);
            list.Add(9);
            list.Add(10);
            list.Add(0);
            list.Add(20);
            listArray1[1] = list;
            list = new List<EItemType>();
            list.Add(3);
            listArray1[2] = list;
            list = new List<EItemType>();
            list.Add(2);
            list.Add(11);
            list.Add(14);
            listArray1[3] = list;
            list = new List<EItemType>();
            list.Add(4);
            listArray1[4] = list;
            list = new List<EItemType>();
            list.Add(1);
            listArray1[5] = list;
            FilterItemTypes = listArray1;
            textArray1 = new string[] { "sys.SORT_INDEX", "sys.SORT_RARITY" };
            SortTypeTexts = textArray1;
            return;
        }

        public ShopSellWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <getCurrentItem>m__40F(ItemData src, ItemData dsc)
        {
            return (src.No - dsc.No);
        }

        [CompilerGenerated]
        private static int <getCurrentItem>m__410(ItemData src, ItemData dsc)
        {
            if (dsc.Rarity != src.Rarity)
            {
                goto Label_001F;
            }
            return (src.No - dsc.No);
        Label_001F:
            return (dsc.Rarity - src.Rarity);
        }

        [CompilerGenerated]
        private void <Start>m__408(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mFilterType = 0;
            base.Refresh();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__409(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mFilterType = 1;
            base.Refresh();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__40A(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mFilterType = 2;
            base.Refresh();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__40B(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mFilterType = 5;
            base.Refresh();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__40C(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mFilterType = 3;
            base.Refresh();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__40D(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mFilterType = 4;
            base.Refresh();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            base.Refresh();
        Label_000D:
            if (pinID != 2)
            {
                goto Label_001A;
            }
            this.OnCleared();
        Label_001A:
            return;
        }

        private void adjustPages()
        {
            if (base.mPageSize <= 0)
            {
                goto Label_0040;
            }
            base.mMaxPages = ((this.DataCount + base.mPageSize) - 1) / base.mPageSize;
            if (base.mMaxPages >= 1)
            {
                goto Label_0047;
            }
            base.mMaxPages = 1;
            goto Label_0047;
        Label_0040:
            base.mMaxPages = 1;
        Label_0047:
            base.mPage = Mathf.Clamp(base.mPage, 0, base.mMaxPages - 1);
            return;
        }

        protected override void Awake()
        {
        }

        private GameObject createDisplayObject()
        {
            GameObject obj2;
            Transform transform;
            ListItemEvents events;
            obj2 = this.CreateItem();
            obj2.get_transform().SetParent(this.ListParent, 0);
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0040;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Label_0040:
            return obj2;
        }

        private unsafe void createDisplaySellItem(List<ItemData> list)
        {
            List<SellItem> list2;
            ItemData data;
            List<ItemData>.Enumerator enumerator;
            GameObject obj2;
            Transform transform;
            ListItemEvents events;
            SellItem item;
            list2 = new List<SellItem>();
            enumerator = list.GetEnumerator();
        Label_000D:
            try
            {
                goto Label_00B1;
            Label_0012:
                data = &enumerator.Current;
                obj2 = this.CreateItem();
                if ((obj2 == null) == null)
                {
                    goto Label_003C;
                }
                DebugUtility.LogError("CreateItem returned NULL");
                goto Label_00D5;
            Label_003C:
                obj2.get_transform().SetParent(this.ListParent, 0);
                events = obj2.GetComponent<ListItemEvents>();
                if ((events != null) == null)
                {
                    goto Label_007A;
                }
                events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
            Label_007A:
                if (data.ItemType != 12)
                {
                    goto Label_008C;
                }
                goto Label_00B1;
            Label_008C:
                this.mSellItemGameObjects.Add(obj2);
                item = this.CreateOrSearchSellItem(data);
                list2.Add(item);
                DataSource.Bind<SellItem>(obj2, item);
            Label_00B1:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0012;
                }
                goto Label_00CE;
            }
            finally
            {
            Label_00C2:
                ((List<ItemData>.Enumerator) enumerator).Dispose();
            }
        Label_00CE:
            this.mSellItemList = list2;
        Label_00D5:
            return;
        }

        protected override GameObject CreateItem()
        {
            return Object.Instantiate<GameObject>(this.ItemTemplate);
        }

        private SellItem CreateOrSearchSellItem(ItemData item)
        {
            SellItem item2;
            item2 = this.SearchFromSelectedItem(item);
            if (item2 != null)
            {
                goto Label_002E;
            }
            item2 = new SellItem();
            item2.item = item;
            item2.num = 0;
            item2.index = -1;
            goto Label_0048;
        Label_002E:
            Debug.Log("Exist SellItem num=" + ((int) item2.num));
        Label_0048:
            return item2;
        }

        private void firstSetupDisplayItem()
        {
            int num;
            GameObject obj2;
            num = 0;
            goto Label_001E;
        Label_0007:
            obj2 = this.createDisplayObject();
            this.mSellItemGameObjects.Add(obj2);
            num += 1;
        Label_001E:
            if (num < base.mPageSize)
            {
                goto Label_0007;
            }
            return;
        }

        private unsafe List<ItemData> getCurrentItem()
        {
            List<ItemData> list;
            List<EItemType> list2;
            ItemData data;
            List<ItemData>.Enumerator enumerator;
            ItemData data2;
            List<ItemData>.Enumerator enumerator2;
            EItemType type;
            List<EItemType>.Enumerator enumerator3;
            int num;
            SortTypes types;
            list = new List<ItemData>();
            list2 = FilterItemTypes[this.mFilterType];
            if (this.ListConfig.MaxGentotuOnly == null)
            {
                goto Label_0097;
            }
            enumerator = MonoSingleton<GameManager>.Instance.Player.Items.GetEnumerator();
        Label_0038:
            try
            {
                goto Label_0075;
            Label_003D:
                data = &enumerator.Current;
                if (data.Num > 0)
                {
                    goto Label_0056;
                }
                goto Label_0075;
            Label_0056:
                if (data.ItemType != 1)
                {
                    goto Label_0075;
                }
                if (this.isSellNGUnit(data) != null)
                {
                    goto Label_0075;
                }
                list.Add(data);
            Label_0075:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_003D;
                }
                goto Label_0092;
            }
            finally
            {
            Label_0086:
                ((List<ItemData>.Enumerator) enumerator).Dispose();
            }
        Label_0092:
            goto Label_017A;
        Label_0097:
            enumerator2 = MonoSingleton<GameManager>.Instance.Player.Items.GetEnumerator();
        Label_00AD:
            try
            {
                goto Label_015C;
            Label_00B2:
                data2 = &enumerator2.Current;
                if (data2.Num <= 0)
                {
                    goto Label_015C;
                }
                if (data2.ItemType == 12)
                {
                    goto Label_015C;
                }
                if (data2.ItemType == 0x11)
                {
                    goto Label_015C;
                }
                if (data2.Param.is_valuables == null)
                {
                    goto Label_00FA;
                }
                goto Label_015C;
            Label_00FA:
                if (list2 == null)
                {
                    goto Label_0154;
                }
                enumerator3 = list2.GetEnumerator();
            Label_0108:
                try
                {
                    goto Label_0131;
                Label_010D:
                    type = &enumerator3.Current;
                    if (data2.ItemType != type)
                    {
                        goto Label_0131;
                    }
                    list.Add(data2);
                    goto Label_013D;
                Label_0131:
                    if (&enumerator3.MoveNext() != null)
                    {
                        goto Label_010D;
                    }
                Label_013D:
                    goto Label_014F;
                }
                finally
                {
                Label_0142:
                    ((List<EItemType>.Enumerator) enumerator3).Dispose();
                }
            Label_014F:
                goto Label_015C;
            Label_0154:
                list.Add(data2);
            Label_015C:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00B2;
                }
                goto Label_017A;
            }
            finally
            {
            Label_016D:
                ((List<ItemData>.Enumerator) enumerator2).Dispose();
            }
        Label_017A:
            this.filteredCnt = list.Count;
            types = this.sortType;
            if (types == null)
            {
                goto Label_01A2;
            }
            if (types == 1)
            {
                goto Label_01CA;
            }
            goto Label_01F2;
        Label_01A2:
            if (<>f__am$cache17 != null)
            {
                goto Label_01BB;
            }
            <>f__am$cache17 = new Comparison<ItemData>(ShopSellWindow.<getCurrentItem>m__40F);
        Label_01BB:
            list.Sort(<>f__am$cache17);
            goto Label_0211;
        Label_01CA:
            if (<>f__am$cache18 != null)
            {
                goto Label_01E3;
            }
            <>f__am$cache18 = new Comparison<ItemData>(ShopSellWindow.<getCurrentItem>m__410);
        Label_01E3:
            list.Sort(<>f__am$cache18);
            goto Label_0211;
        Label_01F2:
            Debug.Log("invalid sortType:" + ((SortTypes) this.sortType));
        Label_0211:
            this.adjustPages();
            num = this.filteredCnt - (base.mPage * base.mPageSize);
            if (num <= base.mPageSize)
            {
                goto Label_0242;
            }
            num = base.mPageSize;
        Label_0242:
            if (num <= 0)
            {
                goto Label_0265;
            }
            list = list.GetRange(base.mPage * base.mPageSize, num);
            goto Label_027B;
        Label_0265:
            Debug.Log("invalid get:" + ((int) num));
        Label_027B:
            return list;
        }

        private bool isSellNGUnit(ItemData item)
        {
            PlayerData data;
            List<UnitData> list;
            UnitData data2;
            <isSellNGUnit>c__AnonStorey3A4 storeya;
            storeya = new <isSellNGUnit>c__AnonStorey3A4();
            list = MonoSingleton<GameManager>.Instance.Player.Units;
            storeya.uParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParamForPiece(item.ItemID, 0);
            if (storeya.uParam != null)
            {
                goto Label_0041;
            }
            return 1;
        Label_0041:
            data2 = list.Find(new Predicate<UnitData>(storeya.<>m__40E));
            if (data2 == null)
            {
                goto Label_0095;
            }
            if (data2.GetRarityCap() > data2.Rarity)
            {
                goto Label_0095;
            }
            if (data2.AwakeLv >= MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(data2.GetRarityCap()).UnitAwakeLvCap)
            {
                goto Label_0097;
            }
        Label_0095:
            return 1;
        Label_0097:
            return 0;
        }

        private void OnCleared()
        {
            int num;
            if (this.mSellItemList != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_003B;
        Label_0013:
            this.mSellItemList[num].index = -1;
            this.mSellItemList[num].num = 0;
            num += 1;
        Label_003B:
            if (num < this.mSellItemList.Count)
            {
                goto Label_0013;
            }
            this.mSellItemListSelected.Clear();
            base.Refresh();
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        protected override void OnItemSelect(GameObject go)
        {
            this.OnSelect(go);
            return;
        }

        private void OnSelect(GameObject go)
        {
            SellItem item;
            ItemData data;
            item = DataSource.FindDataOfClass<SellItem>(go, null);
            if (item == null)
            {
                goto Label_0029;
            }
            if (item.item == null)
            {
                goto Label_0029;
            }
            if (item.item.Num != null)
            {
                goto Label_0034;
            }
        Label_0029:
            Debug.Log("invalid state");
            return;
        Label_0034:
            GlobalVars.SelectSellItem = item;
            GlobalVars.SellItemList = this.mSellItemListSelected;
            data = item.item;
            if (data.Num <= 1)
            {
                goto Label_0089;
            }
            if (this.mSellItemListSelected.Contains(item) != null)
            {
                goto Label_007C;
            }
            if (this.mSellItemListSelected.Count != 10)
            {
                goto Label_007C;
            }
            return;
        Label_007C:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            goto Label_00F6;
        Label_0089:
            if (this.mSellItemListSelected.Remove(item) == null)
            {
                goto Label_00BA;
            }
            item.index = -1;
            item.num = 0;
            this.UpdateSellIndex();
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        Label_00BA:
            if (this.mSellItemListSelected.Count != 10)
            {
                goto Label_00CD;
            }
            return;
        Label_00CD:
            item.num = data.Num;
            this.mSellItemListSelected.Add(item);
            this.UpdateSellIndex();
            GameParameter.UpdateAll(base.get_gameObject());
        Label_00F6:
            return;
        }

        private void OnSell()
        {
            if (this.mSellItemListSelected.Count != null)
            {
                goto Label_0042;
            }
            if (string.IsNullOrEmpty(this.Msg_NoSelection) == null)
            {
                goto Label_002B;
            }
            this.Msg_NoSelection = "sys.CONFIRM_SELL_ITEM_NOT_SELECT";
        Label_002B:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get(this.Msg_NoSelection), null, null, 0, -1);
            return;
        Label_0042:
            GlobalVars.SellItemList = this.mSellItemListSelected;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void OnSort()
        {
            int num;
            num = this.sortType + 1;
            num = num % ((int) Enum.GetNames(typeof(SortTypes)).Length);
            this.sortType = num;
            base.Refresh();
            return;
        }

        protected override void RefreshItems()
        {
            List<ItemData> list;
            bool flag;
            list = this.getCurrentItem();
            this.UpdateDispalyItem(list);
            if ((this.TxtSort != null) == null)
            {
                goto Label_003B;
            }
            this.TxtSort.set_text(LocalizedText.Get(SortTypeTexts[this.sortType]));
        Label_003B:
            if ((this.ListConfig.EmptyTemplate != null) == null)
            {
                goto Label_007F;
            }
            flag = list.Count == 0;
            this.ListConfig.EmptyTemplate.SetActive((this.ListConfig.ShowEmpty == null) ? 0 : flag);
        Label_007F:
            this.UpdateSellIndex();
            DataSource.Bind<List<SellItem>>(base.get_gameObject(), this.mSellItemListSelected);
            GlobalVars.SelectSellItem = null;
            GlobalVars.SellItemList = null;
            GameParameter.UpdateAll(base.get_gameObject());
            this.UpdatePage();
            return;
        }

        private SellItem SearchFromSelectedItem(ItemData item)
        {
            int num;
            num = 0;
            goto Label_002F;
        Label_0007:
            if (this.mSellItemListSelected[num].item != item)
            {
                goto Label_002B;
            }
            return this.mSellItemListSelected[num];
        Label_002B:
            num += 1;
        Label_002F:
            if (num < this.mSellItemListSelected.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        protected override unsafe void Start()
        {
            List<ItemData> list;
            Vector3 vector;
            Vector3 vector2;
            base.Start();
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0034;
            }
            this.ItemTemplate.get_transform().SetSiblingIndex(0);
            this.ItemTemplate.SetActive(0);
        Label_0034:
            if ((this.ToggleShowAll != null) == null)
            {
                goto Label_0061;
            }
            this.ToggleShowAll.onValueChanged.AddListener(new UnityAction<bool>(this, this.<Start>m__408));
        Label_0061:
            if ((this.ToggleShowUsed != null) == null)
            {
                goto Label_008E;
            }
            this.ToggleShowUsed.onValueChanged.AddListener(new UnityAction<bool>(this, this.<Start>m__409));
        Label_008E:
            if ((this.ToggleShowEquip != null) == null)
            {
                goto Label_00BB;
            }
            this.ToggleShowEquip.onValueChanged.AddListener(new UnityAction<bool>(this, this.<Start>m__40A));
        Label_00BB:
            if ((this.ToggleShowUnitPierce != null) == null)
            {
                goto Label_00E8;
            }
            this.ToggleShowUnitPierce.onValueChanged.AddListener(new UnityAction<bool>(this, this.<Start>m__40B));
        Label_00E8:
            if ((this.ToggleShowItemPierce != null) == null)
            {
                goto Label_0115;
            }
            this.ToggleShowItemPierce.onValueChanged.AddListener(new UnityAction<bool>(this, this.<Start>m__40C));
        Label_0115:
            if ((this.ToggleShowMaterial != null) == null)
            {
                goto Label_0142;
            }
            this.ToggleShowMaterial.onValueChanged.AddListener(new UnityAction<bool>(this, this.<Start>m__40D));
        Label_0142:
            if ((this.BtnSort != null) == null)
            {
                goto Label_016F;
            }
            this.BtnSort.get_onClick().AddListener(new UnityAction(this, this.OnSort));
        Label_016F:
            if ((this.BtnCleared != null) == null)
            {
                goto Label_019C;
            }
            this.BtnCleared.get_onClick().AddListener(new UnityAction(this, this.OnCleared));
        Label_019C:
            if ((this.BtnSell != null) == null)
            {
                goto Label_01C9;
            }
            this.BtnSell.get_onClick().AddListener(new UnityAction(this, this.OnSell));
        Label_01C9:
            base.mPageSize = base.CellCount;
            list = this.getCurrentItem();
            this.mSellItemList = new List<SellItem>();
            this.mSellItemListSelected = new List<SellItem>(10);
            this.mSellItemGameObjects = new List<GameObject>(list.Count);
            this.firstSetupDisplayItem();
            this.sortType = 0;
            this.mFilterType = 0;
            this.ItemLayoutParent.get_transform().set_position(new Vector3((float) (Screen.get_width() / 2), &this.ItemLayoutParent.get_transform().get_position().y, &this.ItemLayoutParent.get_transform().get_position().z));
            return;
        }

        protected override void Update()
        {
            base.Update();
            return;
        }

        private void UpdateDispalyItem(List<ItemData> list)
        {
            List<SellItem> list2;
            int num;
            GameObject obj2;
            ItemData data;
            SellItem item;
            list2 = new List<SellItem>();
            num = 0;
            goto Label_0077;
        Label_000D:
            obj2 = this.mSellItemGameObjects[num];
            obj2.SetActive(1);
            if (num >= list.Count)
            {
                goto Label_0063;
            }
            data = list[num];
            item = this.CreateOrSearchSellItem(data);
            list2.Add(item);
            DataSource.Bind<SellItem>(obj2, item);
            obj2.get_transform().set_localScale(Vector3.get_one());
            goto Label_0073;
        Label_0063:
            obj2.get_transform().set_localScale(Vector3.get_zero());
        Label_0073:
            num += 1;
        Label_0077:
            if (num < this.mSellItemGameObjects.Count)
            {
                goto Label_000D;
            }
            this.mSellItemList = list2;
            return;
        }

        public override unsafe void UpdatePage()
        {
            int num;
            if ((base.PageScrollBar != null) == null)
            {
                goto Label_007A;
            }
            if (base.mMaxPages < 2)
            {
                goto Label_005A;
            }
            base.PageScrollBar.set_size(1f / ((float) base.mMaxPages));
            base.PageScrollBar.set_value(((float) base.mPage) / (((float) base.mMaxPages) - 1f));
            goto Label_007A;
        Label_005A:
            base.PageScrollBar.set_size(1f);
            base.PageScrollBar.set_value(0f);
        Label_007A:
            if ((base.PageIndex != null) == null)
            {
                goto Label_00B1;
            }
            base.PageIndex.set_text(&Mathf.Min(base.mPage + 1, base.mMaxPages).ToString());
        Label_00B1:
            if ((base.PageIndexMax != null) == null)
            {
                goto Label_00D8;
            }
            base.PageIndexMax.set_text(&this.mMaxPages.ToString());
        Label_00D8:
            if ((base.ForwardButton != null) == null)
            {
                goto Label_0104;
            }
            base.ForwardButton.set_interactable(base.mPage < (base.mMaxPages - 1));
        Label_0104:
            if ((base.BackButton != null) == null)
            {
                goto Label_0129;
            }
            base.BackButton.set_interactable(base.mPage > 0);
        Label_0129:
            return;
        }

        private void UpdateSellIndex()
        {
            int num;
            num = 0;
            goto Label_001D;
        Label_0007:
            this.mSellItemListSelected[num].index = num;
            num += 1;
        Label_001D:
            if (num < this.mSellItemListSelected.Count)
            {
                goto Label_0007;
            }
            return;
        }

        protected override int DataCount
        {
            get
            {
                return this.filteredCnt;
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
        private sealed class <isSellNGUnit>c__AnonStorey3A4
        {
            internal UnitParam uParam;

            public <isSellNGUnit>c__AnonStorey3A4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__40E(UnitData u)
            {
                return (u.UnitParam.iname == this.uParam.iname);
            }
        }

        [Serializable]
        public enum FilterTypes
        {
            All,
            Used,
            Equip,
            ItemPiece,
            Material,
            UnitPiece
        }

        [Serializable]
        public class SellListConfig
        {
            public bool MaxGentotuOnly;
            public bool ShowEmpty;
            public GameObject EmptyTemplate;

            public SellListConfig()
            {
                base..ctor();
                return;
            }
        }

        private enum SortTypes
        {
            Index,
            Rarity
        }
    }
}

