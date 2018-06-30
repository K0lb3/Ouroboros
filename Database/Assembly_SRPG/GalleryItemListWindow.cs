namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(4, "ソート設定変更ダイアログ表示要求", 0, 4), Pin(5, "ソート設定変更完了", 0, 5), Pin(6, "フィルタ設定変更ダイアログ表示要求", 0, 6), Pin(7, "フィルタ設定変更完了", 0, 7), Pin(100, "ソート設定変更ダイアログ表示", 1, 100), Pin(0x65, "フィルター設定変更ダイアログ表示", 1, 0x65), Pin(0, "Open", 0, 0), Pin(1, "NextPage", 0, 1), Pin(2, "PrevPage", 0, 2), Pin(3, "TabChange", 0, 3)]
    public class GalleryItemListWindow : MonoBehaviour, IFlowInterface
    {
        private const int OPEN = 0;
        private const int NEXT_PAGE = 1;
        private const int PREV_PAGE = 2;
        private const int TAB_CHANGE = 3;
        private const int REQ_SORT_SETTING = 4;
        private const int UPDATED_SORT_SETTING = 5;
        private const int REQ_FILTER_SETTING = 6;
        private const int UPDATED_FILTER_SETTING = 7;
        private const int OUTPUT_SORT_SETTING = 100;
        private const int OUTPUT_FILTER_SETTING = 0x65;
        private static readonly int[] DefaultFilter;
        [SerializeField]
        private GridLayoutGroup ItemGrid;
        [SerializeField]
        private Text CurrentPage;
        [SerializeField]
        private Text TotalPage;
        [SerializeField]
        private GameObject ItemTemplate;
        [SerializeField]
        private Button NextButton;
        [SerializeField]
        private Button PrevButton;
        [SerializeField]
        private Button FilterButton;
        [SerializeField]
        private Sprite FilterButtonAllOnImage;
        [SerializeField]
        private Sprite FilterButtonNotAllImage;
        [SerializeField]
        private Text SortButtonTitle;
        private int mCellCount;
        private int mCurrentPage;
        private int mTotalPage;
        private int mTotalItemNum;
        private SortType mSortType;
        private bool mIsRarityAscending;
        private bool mIsNameAscending;
        private int[] mRareFilters;
        private EItemTabType mCurrentTabType;
        private Settings mSettings;
        private List<ItemParam> mAllItems;
        private List<ItemParam> mItems;
        private List<GameObject> mItemObjects;
        private Dictionary<EItemTabType, int> mRecentPages;
        private Dictionary<EItemTabType, int> mLoadCompletedPage;
        private HashSet<string> mItemAvailable;
        private bool mInitialized;
        [CompilerGenerated]
        private static Func<ItemParam, int> <>f__am$cache1C;
        [CompilerGenerated]
        private static Func<ItemParam, int> <>f__am$cache1D;
        [CompilerGenerated]
        private static Func<ItemParam, string> <>f__am$cache1E;
        [CompilerGenerated]
        private static Func<ItemParam, string> <>f__am$cache1F;
        [CompilerGenerated]
        private static Func<int, int> <>f__am$cache20;
        [CompilerGenerated]
        private static Func<int, int> <>f__am$cache21;

        static GalleryItemListWindow()
        {
            DefaultFilter = new int[] { 0, 1, 2, 3, 4 };
            return;
        }

        public GalleryItemListWindow()
        {
            this.mIsRarityAscending = 1;
            this.mIsNameAscending = 1;
            this.mRareFilters = new int[0];
            this.mItemObjects = new List<GameObject>();
            this.mRecentPages = new Dictionary<EItemTabType, int>();
            this.mLoadCompletedPage = new Dictionary<EItemTabType, int>();
            this.mItemAvailable = new HashSet<string>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <GetAvailableItems>m__33F(ItemParam item)
        {
            return item.rare;
        }

        [CompilerGenerated]
        private static int <GetAvailableItems>m__340(ItemParam item)
        {
            return item.rare;
        }

        [CompilerGenerated]
        private static string <GetAvailableItems>m__341(ItemParam item)
        {
            return item.name;
        }

        [CompilerGenerated]
        private static string <GetAvailableItems>m__342(ItemParam item)
        {
            return item.name;
        }

        [CompilerGenerated]
        private static int <IsSameFilter>m__343(int x)
        {
            return x;
        }

        [CompilerGenerated]
        private static int <IsSameFilter>m__344(int x)
        {
            return x;
        }

        public void Activated(int pinID)
        {
            SerializeValueList list;
            int num;
            int num2;
            if (this.mInitialized != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num2 = pinID;
            switch ((num2 - 1))
            {
                case 0:
                    goto Label_0037;

                case 1:
                    goto Label_003E;

                case 2:
                    goto Label_0045;

                case 3:
                    goto Label_007E;

                case 4:
                    goto Label_0087;

                case 5:
                    goto Label_0129;

                case 6:
                    goto Label_0132;
            }
            goto Label_0186;
        Label_0037:
            this.NextPage();
            return;
        Label_003E:
            this.PrevPage();
            return;
        Label_0045:
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            num = list.GetInt("tabtype");
            if (Enum.IsDefined(typeof(EItemTabType), (int) num) == null)
            {
                goto Label_007D;
            }
            this.ChangeTab(num);
        Label_007D:
            return;
        Label_007E:
            this.SaveSettingAndOutputPin(100);
            return;
        Label_0087:
            if (this.mSortType != this.mSettings.sortType)
            {
                goto Label_00C9;
            }
            if (this.mIsRarityAscending != this.mSettings.isRarityAscending)
            {
                goto Label_00C9;
            }
            if (this.mIsNameAscending == this.mSettings.isNameAscending)
            {
                goto Label_0128;
            }
        Label_00C9:
            this.SaveSetting(this.mSettings);
            this.mSortType = this.mSettings.sortType;
            this.mIsRarityAscending = this.mSettings.isRarityAscending;
            this.mIsNameAscending = this.mSettings.isNameAscending;
            this.mCurrentPage = 0;
            this.ChangeSortButtonText();
            this.ClearAllChache();
            this.RefreshNewPage(this.mCurrentTabType, 0);
        Label_0128:
            return;
        Label_0129:
            this.SaveSettingAndOutputPin(0x65);
            return;
        Label_0132:
            if (this.IsSameFilter(this.mRareFilters, this.mSettings.rareFilters) == null)
            {
                goto Label_014F;
            }
            return;
        Label_014F:
            this.SaveSetting(this.mSettings);
            this.mRareFilters = this.mSettings.rareFilters;
            this.ChangeFilterButtonSprite();
            this.ClearAllChache();
            this.RefreshNewPage(this.mCurrentTabType, 0);
            return;
        Label_0186:
            return;
        }

        private void ChangeEnabledArrowButtons(int index, int max)
        {
            if ((this.NextButton != null) == null)
            {
                goto Label_0022;
            }
            this.NextButton.set_interactable(index < (max - 1));
        Label_0022:
            if ((this.PrevButton != null) == null)
            {
                goto Label_0042;
            }
            this.PrevButton.set_interactable(index > 0);
        Label_0042:
            return;
        }

        private void ChangeFilterButtonSprite()
        {
            Sprite sprite;
            if (this.IsSameFilter(this.mRareFilters, DefaultFilter) == null)
            {
                goto Label_0022;
            }
            sprite = this.FilterButtonAllOnImage;
            goto Label_0029;
        Label_0022:
            sprite = this.FilterButtonNotAllImage;
        Label_0029:
            this.FilterButton.get_image().set_sprite(sprite);
            return;
        }

        private void ChangeSortButtonText()
        {
            string str;
            if ((this.SortButtonTitle != null) == null)
            {
                goto Label_003D;
            }
            str = (this.mSortType != null) ? "sys.SORT_NAME" : "sys.SORT_RARITY";
            this.SortButtonTitle.set_text(LocalizedText.Get(str));
        Label_003D:
            return;
        }

        private void ChangeTab(EItemTabType tabtype)
        {
            if (this.mCurrentTabType != tabtype)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            this.RefreshNewPage(tabtype);
            return;
        }

        private void ClearAllChache()
        {
            this.mRecentPages.Clear();
            this.mLoadCompletedPage.Clear();
            this.mItemAvailable.Clear();
            return;
        }

        private List<ItemParam> GetAvailableItems(EItemTabType tabtype)
        {
            List<ItemParam> list;
            IEnumerable<ItemParam> enumerable;
            <GetAvailableItems>c__AnonStorey349 storey;
            storey = new <GetAvailableItems>c__AnonStorey349();
            storey.tabtype = tabtype;
            storey.<>f__this = this;
            enumerable = Enumerable.Where<ItemParam>(MonoSingleton<GameManager>.Instance.MasterParam.Items, new Func<ItemParam, bool>(storey.<>m__33E));
            if (this.mSortType != null)
            {
                goto Label_009F;
            }
            if (this.mIsRarityAscending == null)
            {
                goto Label_0076;
            }
            if (<>f__am$cache1C != null)
            {
                goto Label_0066;
            }
            <>f__am$cache1C = new Func<ItemParam, int>(GalleryItemListWindow.<GetAvailableItems>m__33F);
        Label_0066:
            enumerable = Enumerable.OrderBy<ItemParam, int>(enumerable, <>f__am$cache1C);
            goto Label_009A;
        Label_0076:
            if (<>f__am$cache1D != null)
            {
                goto Label_008F;
            }
            <>f__am$cache1D = new Func<ItemParam, int>(GalleryItemListWindow.<GetAvailableItems>m__340);
        Label_008F:
            enumerable = Enumerable.OrderByDescending<ItemParam, int>(enumerable, <>f__am$cache1D);
        Label_009A:
            goto Label_0101;
        Label_009F:
            if (this.mIsNameAscending == null)
            {
                goto Label_00D8;
            }
            if (<>f__am$cache1E != null)
            {
                goto Label_00C3;
            }
            <>f__am$cache1E = new Func<ItemParam, string>(GalleryItemListWindow.<GetAvailableItems>m__341);
        Label_00C3:
            enumerable = Enumerable.ToList<ItemParam>(Enumerable.OrderBy<ItemParam, string>(enumerable, <>f__am$cache1E));
            goto Label_0101;
        Label_00D8:
            if (<>f__am$cache1F != null)
            {
                goto Label_00F1;
            }
            <>f__am$cache1F = new Func<ItemParam, string>(GalleryItemListWindow.<GetAvailableItems>m__342);
        Label_00F1:
            enumerable = Enumerable.ToList<ItemParam>(Enumerable.OrderByDescending<ItemParam, string>(enumerable, <>f__am$cache1F));
        Label_0101:
            return Enumerable.ToList<ItemParam>(enumerable);
        }

        private unsafe int GetCellCount()
        {
            float num;
            float num2;
            float num3;
            float num4;
            float num5;
            float num6;
            Rect rect;
            float num7;
            float num8;
            int num9;
            int num10;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            num = &this.ItemGrid.get_cellSize().x;
            num2 = &this.ItemGrid.get_cellSize().y;
            num3 = &this.ItemGrid.get_spacing().x;
            num4 = &this.ItemGrid.get_spacing().y;
            num5 = (float) this.ItemGrid.get_padding().get_horizontal();
            num6 = (float) this.ItemGrid.get_padding().get_vertical();
            rect = ((RectTransform) this.ItemGrid.get_transform()).get_rect();
            num7 = (&rect.get_width() - num5) + num3;
            num8 = (&rect.get_height() - num6) + num4;
            num9 = Mathf.FloorToInt(num7 / (num + num3));
            num10 = Mathf.FloorToInt(num8 / (num2 + num4));
            return (num9 * num10);
        }

        private bool IsSameFilter(int[] fil1, int[] fil2)
        {
            IOrderedEnumerable<int> enumerable;
            IOrderedEnumerable<int> enumerable2;
            if (fil1 != fil2)
            {
                goto Label_0009;
            }
            return 1;
        Label_0009:
            if (<>f__am$cache20 != null)
            {
                goto Label_0022;
            }
            <>f__am$cache20 = new Func<int, int>(GalleryItemListWindow.<IsSameFilter>m__343);
        Label_0022:
            enumerable = Enumerable.OrderBy<int, int>(fil1, <>f__am$cache20);
            if (<>f__am$cache21 != null)
            {
                goto Label_0046;
            }
            <>f__am$cache21 = new Func<int, int>(GalleryItemListWindow.<IsSameFilter>m__344);
        Label_0046:
            enumerable2 = Enumerable.OrderBy<int, int>(fil2, <>f__am$cache21);
            if (Enumerable.SequenceEqual<int>(enumerable, enumerable2) == null)
            {
                goto Label_005F;
            }
            return 1;
        Label_005F:
            return 0;
        }

        private Settings LoadSetting()
        {
            string str;
            Settings settings;
            string str2;
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.GALLERY_SETTING) == null)
            {
                goto Label_0031;
            }
            str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.GALLERY_SETTING, string.Empty);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0031;
            }
            return JsonUtility.FromJson<Settings>(str);
        Label_0031:
            settings = new Settings();
            settings.sortType = 0;
            settings.isRarityAscending = 1;
            settings.isNameAscending = 1;
            settings.rareFilters = DefaultFilter;
            str2 = JsonUtility.ToJson(settings);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.GALLERY_SETTING, str2, 1);
            return settings;
        }

        private void NextPage()
        {
            if ((this.mCurrentPage + 1) >= this.mTotalPage)
            {
                goto Label_0027;
            }
            this.mCurrentPage += 1;
            this.RequestItems();
        Label_0027:
            return;
        }

        private void PrevPage()
        {
            if ((this.mCurrentPage - 1) < 0)
            {
                goto Label_0022;
            }
            this.mCurrentPage -= 1;
            this.RequestItems();
        Label_0022:
            return;
        }

        private unsafe void RefreshNewPage(EItemTabType tabtype)
        {
            int num;
            this.mCurrentTabType = tabtype;
            if (this.mRecentPages.TryGetValue(tabtype, &num) == null)
            {
                goto Label_0026;
            }
            this.mCurrentPage = num;
            goto Label_002D;
        Label_0026:
            this.mCurrentPage = 0;
        Label_002D:
            this.mAllItems = this.GetAvailableItems(this.mCurrentTabType);
            this.mTotalPage = Mathf.CeilToInt(((float) this.mAllItems.Count) / ((float) this.mCellCount));
            this.RequestItems();
            return;
        }

        private void RefreshNewPage(EItemTabType tabtype, int page)
        {
            this.mCurrentTabType = tabtype;
            this.mCurrentPage = page;
            this.mAllItems = this.GetAvailableItems(this.mCurrentTabType);
            this.mTotalPage = Mathf.CeilToInt(((float) this.mAllItems.Count) / ((float) this.mCellCount));
            this.RequestItems();
            return;
        }

        private unsafe void RefreshPage(HashSet<string> availables)
        {
            ItemParam param;
            List<ItemParam>.Enumerator enumerator;
            GameObject obj2;
            GalleryItem item;
            int num;
            int num2;
            int num3;
            GameUtility.DestroyGameObjects(this.mItemObjects);
            this.mItemObjects.Clear();
            if (availables != null)
            {
                goto Label_0067;
            }
            this.mCurrentPage = 0;
            this.mTotalPage = 0;
            num = 0;
            this.TotalPage.set_text(&num.ToString());
            num2 = 0;
            this.CurrentPage.set_text(&num2.ToString());
            this.ChangeEnabledArrowButtons(this.mCurrentPage, this.mTotalPage);
            return;
        Label_0067:
            this.TotalPage.set_text(&this.mTotalPage.ToString());
            this.CurrentPage.set_text(&Math.Min(this.mCurrentPage + 1, this.mTotalPage).ToString());
            enumerator = this.mItems.GetEnumerator();
        Label_00B0:
            try
            {
                goto Label_011F;
            Label_00B5:
                param = &enumerator.Current;
                obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
                item = obj2.GetComponent<GalleryItem>();
                if ((item != null) == null)
                {
                    goto Label_00EE;
                }
                item.SetAvailable(availables.Contains(param.iname));
            Label_00EE:
                DataSource.Bind<ItemParam>(obj2, param);
                obj2.get_transform().SetParent(this.ItemGrid.get_transform(), 0);
                obj2.SetActive(1);
                this.mItemObjects.Add(obj2);
            Label_011F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00B5;
                }
                goto Label_013C;
            }
            finally
            {
            Label_0130:
                ((List<ItemParam>.Enumerator) enumerator).Dispose();
            }
        Label_013C:
            this.ChangeEnabledArrowButtons(this.mCurrentPage, this.mTotalPage);
            return;
        }

        private unsafe void RequestItems()
        {
            int num;
            this.mRecentPages[this.mCurrentTabType] = this.mCurrentPage;
            this.mItems = Enumerable.ToList<ItemParam>(Enumerable.Take<ItemParam>(Enumerable.Skip<ItemParam>(this.mAllItems, this.mCurrentPage * this.mCellCount), this.mCellCount));
            if (this.mItems.Count > 0)
            {
                goto Label_005D;
            }
            this.RefreshPage(null);
        Label_005D:
            if (this.mLoadCompletedPage.TryGetValue(this.mCurrentTabType, &num) == null)
            {
                goto Label_008E;
            }
            if (this.mCurrentPage > num)
            {
                goto Label_008E;
            }
            this.RefreshPage(this.mItemAvailable);
            return;
        Label_008E:
            Network.RequestAPI(new ReqGalleryItem(this.mItems, new SRPG.Network.ResponseCallback(this.ResponseCallback)), 0);
            return;
        }

        private unsafe void ResponseCallback(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_Body> response;
            string str;
            string[] strArray;
            int num;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_001C;
            }
            FlowNode_Network.Retry();
            return;
        Label_001C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_Body>>(&www.text);
            if (response.body == null)
            {
                goto Label_0075;
            }
            if (response.body.items == null)
            {
                goto Label_0075;
            }
            strArray = response.body.items;
            num = 0;
            goto Label_006C;
        Label_0057:
            str = strArray[num];
            this.mItemAvailable.Add(str);
            num += 1;
        Label_006C:
            if (num < ((int) strArray.Length))
            {
                goto Label_0057;
            }
        Label_0075:
            this.RefreshPage(this.mItemAvailable);
            this.mLoadCompletedPage[this.mCurrentTabType] = this.mCurrentPage;
            this.mInitialized = 1;
            Network.RemoveAPI();
            return;
        }

        private void SaveSetting(Settings settings)
        {
            string str;
            str = JsonUtility.ToJson(settings);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.GALLERY_SETTING, str, 1);
            return;
        }

        private void SaveSettingAndOutputPin(int pinID)
        {
            SerializeValueList list;
            list = new SerializeValueList();
            list.SetObject("settings", this.mSettings);
            FlowNode_ButtonEvent.currentValue = list;
            FlowNode_GameObject.ActivateOutputLinks(this, pinID);
            return;
        }

        private void Start()
        {
            this.mCellCount = this.GetCellCount();
            this.mCurrentTabType = 1;
            this.mSettings = this.LoadSetting();
            this.mSortType = this.mSettings.sortType;
            this.mIsRarityAscending = this.mSettings.isRarityAscending;
            this.mIsNameAscending = this.mSettings.isNameAscending;
            this.mRareFilters = this.mSettings.rareFilters;
            this.ChangeFilterButtonSprite();
            this.ChangeSortButtonText();
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_008C;
            }
            this.ItemTemplate.SetActive(0);
        Label_008C:
            this.RefreshNewPage(this.mCurrentTabType, 0);
            return;
        }

        [CompilerGenerated]
        private sealed class <GetAvailableItems>c__AnonStorey349
        {
            internal EItemTabType tabtype;
            internal GalleryItemListWindow <>f__this;

            public <GetAvailableItems>c__AnonStorey349()
            {
                base..ctor();
                return;
            }

            internal bool <>m__33E(ItemParam item)
            {
                <GetAvailableItems>c__AnonStorey34A storeya;
                storeya = new <GetAvailableItems>c__AnonStorey34A();
                storeya.<>f__ref$841 = this;
                storeya.item = item;
                return ((storeya.item.tabtype != this.tabtype) ? 0 : Enumerable.Any<int>(this.<>f__this.mRareFilters, new Func<int, bool>(storeya.<>m__345)));
            }

            private sealed class <GetAvailableItems>c__AnonStorey34A
            {
                internal ItemParam item;
                internal GalleryItemListWindow.<GetAvailableItems>c__AnonStorey349 <>f__ref$841;

                public <GetAvailableItems>c__AnonStorey34A()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__345(int rare)
                {
                    return (rare == this.item.rare);
                }
            }
        }

        public class JSON_Body
        {
            public string[] items;

            public JSON_Body()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Settings
        {
            public GalleryItemListWindow.SortType sortType;
            public bool isRarityAscending;
            public bool isNameAscending;
            public int[] rareFilters;

            public Settings()
            {
                base..ctor();
                return;
            }
        }

        public enum SortType
        {
            Rarity,
            Name
        }
    }
}

