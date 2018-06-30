namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class SRPG_FixedList : UIBehaviour
    {
        protected int mPage;
        protected int mMaxPages;
        protected int mPageSize;
        private GridLayoutGroup mGrid;
        private int mCellCountX;
        private int mCellCountY;
        private bool mStarted;
        private bool mShouldRefresh;
        private bool mCalculatedCellCounts;
        protected bool mInvokeSelChange;
        protected List<GameObject> mItems;
        protected bool mFocusSelection;
        protected object[] mData;
        protected Type mDataType;
        public SelectionChangeEvent OnSelectionChange;
        public Scrollbar PageScrollBar;
        public Text PageIndex;
        public int MaxSelection;
        public Text PageIndexMax;
        public Text NumSelection;
        public Button ForwardButton;
        public Button BackButton;
        public GameObject[] ExtraItems;
        protected List<object> mSelection;
        public int MaxCellCount;
        [CompilerGenerated]
        private static SelectionChangeEvent <>f__am$cache19;

        public SRPG_FixedList()
        {
            this.mItems = new List<GameObject>();
            if (<>f__am$cache19 != null)
            {
                goto Label_0024;
            }
            <>f__am$cache19 = new SelectionChangeEvent(SRPG_FixedList.<OnSelectionChange>m__15C);
        Label_0024:
            this.OnSelectionChange = <>f__am$cache19;
            this.MaxSelection = 8;
            this.ExtraItems = new GameObject[0];
            this.mSelection = new List<object>(4);
            this.MaxCellCount = 0x40;
            base..ctor();
            return;
        }

        protected void _OnItemSelect(GameObject go)
        {
            object obj2;
            if (this.mShouldRefresh == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.OnItemSelect(go);
            obj2 = DataSource.FindDataOfClass(go, this.mDataType, null);
            if (obj2 != null)
            {
                goto Label_0028;
            }
            return;
        Label_0028:
            if (this.MaxSelection <= 0)
            {
                goto Label_008A;
            }
            if (this.mSelection.Contains(obj2) == null)
            {
                goto Label_0057;
            }
            this.mSelection.Remove(obj2);
            goto Label_0079;
        Label_0057:
            if (this.mSelection.Count >= this.MaxSelection)
            {
                goto Label_0079;
            }
            this.mSelection.Add(obj2);
        Label_0079:
            this.UpdateSelection();
            this.TriggerSelectionChange();
            goto Label_00AD;
        Label_008A:
            this.mSelection.Clear();
            this.mSelection.Add(obj2);
            this.UpdateSelection();
            this.TriggerSelectionChange();
        Label_00AD:
            return;
        }

        [CompilerGenerated]
        private static void <OnSelectionChange>m__15C(SRPG_FixedList list)
        {
        }

        public virtual void BindData()
        {
            int num;
            int num2;
            num = 0;
            goto Label_00A5;
        Label_0007:
            num2 = ((this.mPage * this.mPageSize) + num) - ((int) this.ExtraItems.Length);
            if (0 > num2)
            {
                goto Label_008F;
            }
            if (num2 >= ((int) this.mData.Length))
            {
                goto Label_008F;
            }
            DataSource.Bind(this.mItems[num], this.mDataType, this.mData[num2]);
            this.OnUpdateItem(this.mItems[num], num2);
            this.mItems[num].SetActive(1);
            GameParameter.UpdateAll(this.mItems[num]);
            goto Label_00A1;
        Label_008F:
            this.mItems[num].SetActive(0);
        Label_00A1:
            num += 1;
        Label_00A5:
            if (num < this.mItems.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void CancelRefresh()
        {
            this.mShouldRefresh = 0;
            return;
        }

        public void ClearItems()
        {
            int num;
            num = 0;
            goto Label_0033;
        Label_0007:
            if ((this.mItems[num] != null) == null)
            {
                goto Label_002F;
            }
            Object.Destroy(this.mItems[num]);
        Label_002F:
            num += 1;
        Label_0033:
            if (num < this.mItems.Count)
            {
                goto Label_0007;
            }
            this.mItems.Clear();
            this.mSelection.Clear();
            return;
        }

        public void ClearSelection()
        {
            this.mSelection.Clear();
            this.UpdateSelection();
            this.TriggerSelectionChange();
            return;
        }

        protected virtual GameObject CreateItem()
        {
            return null;
        }

        protected virtual GameObject CreateItem(int index)
        {
            return null;
        }

        public virtual void GotoNextPage()
        {
            if (this.mPage >= (this.mMaxPages - 1))
            {
                goto Label_0027;
            }
            this.mPage += 1;
            this.Refresh();
        Label_0027:
            return;
        }

        public virtual void GotoPreviousPage()
        {
            if (this.mPage <= 0)
            {
                goto Label_0020;
            }
            this.mPage -= 1;
            this.Refresh();
        Label_0020:
            return;
        }

        protected virtual void LateUpdate()
        {
            if (this.mShouldRefresh == null)
            {
                goto Label_0018;
            }
            this.mShouldRefresh = 0;
            this.RefreshItems();
        Label_0018:
            return;
        }

        protected virtual void OnItemSelect(GameObject go)
        {
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            if (this.mCalculatedCellCounts == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mShouldRefresh = 1;
            return;
        }

        protected virtual void OnUpdateItem(GameObject go, int index)
        {
        }

        private unsafe void RecalculateCellCounts()
        {
            RectTransform transform;
            float num;
            float num2;
            Rect rect;
            Vector2 vector;
            Rect rect2;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            Vector2 vector5;
            Vector2 vector6;
            Vector2 vector7;
            Vector2 vector8;
            if ((this.mGrid == null) == null)
            {
                goto Label_0022;
            }
            this.mGrid = this.ListParent.GetComponent<GridLayoutGroup>();
        Label_0022:
            if ((this.mGrid == null) == null)
            {
                goto Label_0042;
            }
            this.mCellCountX = 0;
            this.mCellCountY = 0;
            return;
        Label_0042:
            transform = this.ListParent;
            num = &&transform.get_rect().get_size().x;
            num2 = &&transform.get_rect().get_size().y;
            this.mCellCountX = Mathf.Max(1, Mathf.FloorToInt((((num - ((float) this.mGrid.get_padding().get_horizontal())) + &this.mGrid.get_spacing().x) + 0.001f) / (&this.mGrid.get_cellSize().x + &this.mGrid.get_spacing().x)));
            this.mCellCountY = Mathf.Max(1, Mathf.FloorToInt((((num2 - ((float) this.mGrid.get_padding().get_vertical())) + &this.mGrid.get_spacing().y) + 0.001f) / (&this.mGrid.get_cellSize().y + &this.mGrid.get_spacing().y)));
            this.mCalculatedCellCounts = 1;
            return;
        }

        public void Refresh()
        {
            this.mShouldRefresh = 1;
            return;
        }

        protected virtual void RefreshItems()
        {
            Transform transform;
            GameObject obj2;
            Transform transform2;
            ListItemEvents events;
            int num;
            int num2;
            int num3;
            this.mPageSize = this.CellCount;
            transform = this.ListParent;
            goto Label_0076;
        Label_0018:
            obj2 = this.CreateItem();
            if ((obj2 == null) == null)
            {
                goto Label_0036;
            }
            DebugUtility.LogError("CreateItem returned NULL");
            return;
        Label_0036:
            obj2.get_transform().SetParent(transform, 0);
            this.mItems.Add(obj2);
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0076;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this._OnItemSelect);
        Label_0076:
            if (this.mItems.Count < this.mPageSize)
            {
                goto Label_0018;
            }
            if (this.mItems.Count != null)
            {
                goto Label_009D;
            }
            return;
        Label_009D:
            if (this.mPageSize <= 0)
            {
                goto Label_00E8;
            }
            this.mMaxPages = (((this.DataCount + ((int) this.ExtraItems.Length)) + this.mPageSize) - 1) / this.mPageSize;
            this.mPage = Mathf.Clamp(this.mPage, 0, this.mMaxPages - 1);
        Label_00E8:
            if (this.mFocusSelection == null)
            {
                goto Label_014F;
            }
            this.mFocusSelection = 0;
            if ((this.mSelection == null) || (this.mSelection.Count <= 0))
            {
                goto Label_014F;
            }
            num = Array.IndexOf<object>(this.mData, this.mSelection[0]) + ((int) this.ExtraItems.Length);
            if (num < 0)
            {
                goto Label_014F;
            }
            this.mPage = num / this.mPageSize;
        Label_014F:
            this.BindData();
            num2 = 0;
            goto Label_01AE;
        Label_015D:
            num3 = (this.mPage * this.mPageSize) + num2;
            if ((this.ExtraItems[num2] != null) == null)
            {
                goto Label_01A8;
            }
            this.ExtraItems[num2].SetActive((0 > num3) ? 0 : (num3 < ((int) this.ExtraItems.Length)));
        Label_01A8:
            num2 += 1;
        Label_01AE:
            if (num2 < ((int) this.ExtraItems.Length))
            {
                goto Label_015D;
            }
            this.UpdateSelection();
            this.UpdatePage();
            if (this.mInvokeSelChange == null)
            {
                goto Label_01E1;
            }
            this.mInvokeSelChange = 0;
            this.TriggerSelectionChange();
        Label_01E1:
            return;
        }

        public void RegisterNextButtonCallBack(UnityAction callBack)
        {
            if ((this.ForwardButton != null) == null)
            {
                goto Label_002D;
            }
            this.ForwardButton.get_onClick().AddListener(new UnityAction(callBack, this.Invoke));
        Label_002D:
            return;
        }

        public void RegisterPrevButtonCallBack(UnityAction callBack)
        {
            if ((this.BackButton != null) == null)
            {
                goto Label_002D;
            }
            this.BackButton.get_onClick().AddListener(new UnityAction(callBack, this.Invoke));
        Label_002D:
            return;
        }

        public virtual void SetData(object[] src, Type type)
        {
            this.mData = src;
            this.mDataType = type;
            this.Refresh();
            return;
        }

        public void SetPageIndex(int pIndex, bool isRefresh)
        {
            if (0 > pIndex)
            {
                goto Label_0028;
            }
            if (pIndex > (this.mMaxPages - 1))
            {
                goto Label_0028;
            }
            this.mPage = pIndex;
            if (isRefresh == null)
            {
                goto Label_0028;
            }
            this.Refresh();
        Label_0028:
            return;
        }

        public void SetSelection(object[] sel, bool invoke, bool focus)
        {
            int num;
            this.mFocusSelection = focus;
            this.Refresh();
            this.mSelection.Clear();
            num = 0;
            goto Label_004C;
        Label_001F:
            if ((sel[num] == null) || (this.mSelection.Contains(sel[num]) != null))
            {
                goto Label_0048;
            }
            this.mSelection.Add(sel[num]);
        Label_0048:
            num += 1;
        Label_004C:
            if (num < ((int) sel.Length))
            {
                goto Label_001F;
            }
            if (this.mStarted != null)
            {
                goto Label_007A;
            }
            this.mInvokeSelChange = (this.mInvokeSelChange != null) ? 1 : invoke;
            goto Label_008C;
        Label_007A:
            this.UpdateSelection();
            if (invoke == null)
            {
                goto Label_008C;
            }
            this.TriggerSelectionChange();
        Label_008C:
            return;
        }

        protected override void Start()
        {
            base.Start();
            this.mStarted = 1;
            return;
        }

        protected virtual void TriggerSelectionChange()
        {
            this.OnSelectionChange(this);
            return;
        }

        protected virtual void Update()
        {
            if (this.mShouldRefresh == null)
            {
                goto Label_0018;
            }
            this.mShouldRefresh = 0;
            this.RefreshItems();
        Label_0018:
            return;
        }

        public virtual unsafe void UpdatePage()
        {
            int num;
            if ((this.PageScrollBar != null) == null)
            {
                goto Label_007A;
            }
            if (this.mMaxPages < 2)
            {
                goto Label_005A;
            }
            this.PageScrollBar.set_size(1f / ((float) this.mMaxPages));
            this.PageScrollBar.set_value(((float) this.mPage) / (((float) this.mMaxPages) - 1f));
            goto Label_007A;
        Label_005A:
            this.PageScrollBar.set_size(1f);
            this.PageScrollBar.set_value(0f);
        Label_007A:
            if ((this.PageIndex != null) == null)
            {
                goto Label_00B1;
            }
            this.PageIndex.set_text(&Mathf.Min(this.mPage + 1, this.mMaxPages).ToString());
        Label_00B1:
            if ((this.PageIndexMax != null) == null)
            {
                goto Label_00D8;
            }
            this.PageIndexMax.set_text(&this.mMaxPages.ToString());
        Label_00D8:
            if ((this.ForwardButton != null) == null)
            {
                goto Label_0104;
            }
            this.ForwardButton.set_interactable(this.mPage < (this.mMaxPages - 1));
        Label_0104:
            if ((this.BackButton != null) == null)
            {
                goto Label_0129;
            }
            this.BackButton.set_interactable(this.mPage > 0);
        Label_0129:
            return;
        }

        public unsafe void UpdateSelection()
        {
            int num;
            if ((this.NumSelection != null) == null)
            {
                goto Label_002F;
            }
            this.NumSelection.set_text(&this.mSelection.Count.ToString());
        Label_002F:
            return;
        }

        public virtual RectTransform ListParent
        {
            get
            {
                return base.GetComponent<RectTransform>();
            }
        }

        public int CellCount
        {
            get
            {
                this.RecalculateCellCounts();
                return (this.mCellCountX * this.mCellCountY);
            }
        }

        public int Page
        {
            get
            {
                return this.mPage;
            }
            set
            {
                if (this.mPage != value)
                {
                    goto Label_000D;
                }
                return;
            Label_000D:
                this.mPage = value;
                this.Refresh();
                return;
            }
        }

        public int MaxPage
        {
            get
            {
                return this.mMaxPages;
            }
        }

        public object[] Selection
        {
            get
            {
                return this.mSelection.ToArray();
            }
        }

        protected object[] Data
        {
            get
            {
                return this.mData;
            }
        }

        protected virtual int DataCount
        {
            get
            {
                return ((this.mData == null) ? 0 : ((int) this.mData.Length));
            }
        }

        protected bool HasStarted
        {
            get
            {
                return this.mStarted;
            }
        }

        public delegate void SelectionChangeEvent(SRPG_FixedList list);
    }
}

