namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class ConceptCardEquipList : UIBehaviour
    {
        private bool mIsInitialized;
        private bool mIsChangeRectSize;
        private bool mIsKeepCurrentPage;
        private RectTransform mRectTransform;
        private GridLayoutGroup mGrid;
        private List<ConceptCardData> mCardDatas;
        private List<ConceptCardIcon> mCardIcons;
        private int mFirstCardIndex;
        private int mCurrentPage;
        private int mLastPage;
        private ConceptCardData mSelectedConceptCardData;
        private ConceptCardData mReservedSelectConceptCardData;
        [SerializeField]
        private GameObject mNoEquipButtonObject;
        [SerializeField]
        private GameObject mCardObjectTemplate;
        [SerializeField]
        private RectTransform mCardObjectParent;
        [SerializeField]
        private Text mCardEmptyMessageText;

        public ConceptCardEquipList()
        {
            this.mCardDatas = new List<ConceptCardData>();
            this.mCardIcons = new List<ConceptCardIcon>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <LateUpdate>m__2C9(ConceptCardIcon card)
        {
            return ((((card != null) == null) || (card.ConceptCard == null)) ? 0 : (card.ConceptCard.UniqueID == this.mReservedSelectConceptCardData.UniqueID));
        }

        [CompilerGenerated]
        private bool <SelectedCardIconActive>m__2CB(ConceptCardIcon card)
        {
            return ((((card != null) == null) || (card.ConceptCard == null)) ? 0 : (card.ConceptCard.UniqueID == this.mSelectedConceptCardData.UniqueID));
        }

        private int CalcLastPage()
        {
            int num;
            int num2;
            int num3;
            num = this.CellCount;
            if (num != null)
            {
                goto Label_000F;
            }
            return 0;
        Label_000F:
            num2 = ((this.mCardDatas.Count % num) != null) ? 1 : 0;
            num3 = this.mCardDatas.Count / num;
            return Mathf.Max((num3 + num2) - 1, 0);
        }

        private void CreateIcon()
        {
            int num;
            GameObject obj2;
            ConceptCardIcon icon;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            num = this.CellCount;
            goto Label_0044;
        Label_000C:
            obj2 = Object.Instantiate<GameObject>(this.mCardObjectTemplate);
            obj2.get_transform().SetParent(this.mCardObjectParent, 0);
            obj2.SetActive(1);
            icon = obj2.GetComponent<ConceptCardIcon>();
            this.mCardIcons.Add(icon);
        Label_0044:
            if (this.mCardIcons.Count < num)
            {
                goto Label_000C;
            }
            if (this.mCurrentPage != null)
            {
                goto Label_006C;
            }
            num -= 1;
            num = Mathf.Max(0, num);
        Label_006C:
            if (this.mCardIcons.Count <= num)
            {
                goto Label_00E5;
            }
            num2 = this.mCardIcons.Count - num;
            num3 = 0;
            goto Label_00DD;
        Label_0093:
            num4 = (this.mCardIcons.Count - 1) - num3;
            if (this.mCardIcons.Count <= num4)
            {
                goto Label_00D7;
            }
            if (num4 < 0)
            {
                goto Label_00D7;
            }
            this.mCardIcons[num4].get_gameObject().SetActive(0);
        Label_00D7:
            num3 += 1;
        Label_00DD:
            if (num3 < num2)
            {
                goto Label_0093;
            }
        Label_00E5:
            num5 = this.GetActiveIconCount();
            if (num5 >= num)
            {
                goto Label_0167;
            }
            num6 = num - num5;
            num7 = 0;
            goto Label_0155;
        Label_0103:
            if (this.mCardIcons[num7].get_gameObject().get_activeSelf() == null)
            {
                goto Label_0124;
            }
            goto Label_014F;
        Label_0124:
            this.mCardIcons[num7].get_gameObject().SetActive(1);
            num6 -= 1;
            if (num6 > 0)
            {
                goto Label_014F;
            }
            goto Label_0167;
        Label_014F:
            num7 += 1;
        Label_0155:
            if (num7 < this.mCardIcons.Count)
            {
                goto Label_0103;
            }
        Label_0167:
            return;
        }

        private int GetActiveIconCount()
        {
            int num;
            int num2;
            num = 0;
            num2 = 0;
            goto Label_0031;
        Label_0009:
            if (this.mCardIcons[num2].get_gameObject().get_activeSelf() != null)
            {
                goto Label_0029;
            }
            goto Label_002D;
        Label_0029:
            num += 1;
        Label_002D:
            num2 += 1;
        Label_0031:
            if (num2 < this.mCardIcons.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        private int GetCardPage(ConceptCardData target)
        {
            int num;
            int num2;
            int num3;
            int num4;
            <GetCardPage>c__AnonStorey324 storey;
            storey = new <GetCardPage>c__AnonStorey324();
            storey.target = target;
            if (storey.target != null)
            {
                goto Label_001D;
            }
            return -1;
        Label_001D:
            num = this.mCardDatas.FindIndex(new Predicate<ConceptCardData>(storey.<>m__2CC));
            if (num > -1)
            {
                goto Label_003F;
            }
            return -1;
        Label_003F:
            num2 = -1;
            num3 = this.mCurrentPage;
            this.mCurrentPage = 0;
            num4 = 0;
            goto Label_0098;
        Label_0056:
            if (num >= (this.mFirstCardIndex + this.CellCount))
            {
                goto Label_0080;
            }
            num2 = Mathf.Min(this.mCurrentPage, this.LastPage);
            goto Label_00A4;
        Label_0080:
            this.mCurrentPage += 1;
            this.RefreshFirstIconIndex();
            num4 += 1;
        Label_0098:
            if (num4 <= this.LastPage)
            {
                goto Label_0056;
            }
        Label_00A4:
            this.mCurrentPage = num3;
            this.RefreshFirstIconIndex();
            return num2;
        }

        public void HideTemplateObject()
        {
            this.mCardObjectTemplate.SetActive(0);
            this.mNoEquipButtonObject.SetActive(0);
            return;
        }

        public void Init(List<ConceptCardData> card_datas, UnitData selected_unit, bool is_keep_page)
        {
            bool flag;
            List<ConceptCardData> list;
            UnitData data;
            ConceptCardListSortWindow.Type type;
            ConceptCardListSortWindow.Type type2;
            <Init>c__AnonStorey322 storey;
            <Init>c__AnonStorey323 storey2;
            storey = new <Init>c__AnonStorey322();
            storey.card_datas = card_datas;
            flag = this.IsIgnoreEquipedConceptCard;
            list = new List<ConceptCardData>();
            data = null;
            storey2 = new <Init>c__AnonStorey323();
            storey2.<>f__ref$802 = storey;
            storey2.i = 0;
            goto Label_00A8;
        Label_003B:
            data = MonoSingleton<GameManager>.Instance.Player.Units.Find(new Predicate<UnitData>(storey2.<>m__2CA));
            if (((flag == null) || (data == null)) || (data.UniqueID == selected_unit.UniqueID))
            {
                goto Label_007F;
            }
            goto Label_0098;
        Label_007F:
            list.Add(storey.card_datas[storey2.i]);
        Label_0098:
            storey2.i += 1;
        Label_00A8:
            if (storey2.i < storey.card_datas.Count)
            {
                goto Label_003B;
            }
            this.mIsChangeRectSize = 1;
            this.mIsKeepCurrentPage = is_keep_page;
            this.mCardEmptyMessageText.get_gameObject().SetActive((list.Count > 0) == 0);
            this.mCardDatas.Clear();
            this.mCardDatas.Add(null);
            this.mCardDatas.AddRange(list);
            type = ConceptCardListSortWindow.LoadDataType();
            type2 = ConceptCardListSortWindow.LoadDataOrderType();
            this.Sort(type, type2);
            this.mCurrentPage = (is_keep_page == null) ? 0 : this.mCurrentPage;
            this.mIsInitialized = 1;
            return;
        }

        private void LateUpdate()
        {
            int num;
            ConceptCardIcon icon;
            if (this.mIsInitialized != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mIsChangeRectSize == null)
            {
                goto Label_00BF;
            }
            this.mLastPage = this.CalcLastPage();
            num = -1;
            if (this.mIsKeepCurrentPage == null)
            {
                goto Label_004C;
            }
            this.mCurrentPage = Mathf.Min(this.mCurrentPage, this.LastPage);
            goto Label_0067;
        Label_004C:
            num = this.GetCardPage(this.mReservedSelectConceptCardData);
            if (num < 0)
            {
                goto Label_0067;
            }
            this.mCurrentPage = num;
        Label_0067:
            this.CreateIcon();
            this.RefreshNoEquipButtonObject();
            this.RefreshFirstIconIndex();
            this.RefreshIcon();
            this.mIsChangeRectSize = 0;
            if (num < 0)
            {
                goto Label_00BF;
            }
            icon = this.mCardIcons.Find(new Predicate<ConceptCardIcon>(this.<LateUpdate>m__2C9));
            if ((icon != null) == null)
            {
                goto Label_00B8;
            }
            this.SelectCardIcon(icon);
        Label_00B8:
            this.mReservedSelectConceptCardData = null;
        Label_00BF:
            return;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            this.mIsChangeRectSize = 1;
            return;
        }

        public void OpenSelectIconExistPage(ConceptCardData card)
        {
            if (card != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mReservedSelectConceptCardData = card;
            return;
        }

        public void PageBack()
        {
            if (this.mCurrentPage > 0)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            this.mCurrentPage -= 1;
            this.CreateIcon();
            this.RefreshNoEquipButtonObject();
            this.RefreshFirstIconIndex();
            this.RefreshIcon();
            this.ResetSelectCardAnimation();
            this.SelectedCardIconActive();
            return;
        }

        public void PageNext()
        {
            if (this.mCurrentPage < this.mLastPage)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mCurrentPage += 1;
            this.CreateIcon();
            this.RefreshNoEquipButtonObject();
            this.RefreshFirstIconIndex();
            this.RefreshIcon();
            this.ResetSelectCardAnimation();
            this.SelectedCardIconActive();
            return;
        }

        private void RefreshFirstIconIndex()
        {
            this.mFirstCardIndex = this.mCurrentPage * this.CellCount;
            if (this.mCurrentPage > 0)
            {
                goto Label_0026;
            }
            this.mFirstCardIndex = 1;
        Label_0026:
            return;
        }

        private void RefreshIcon()
        {
            int num;
            int num2;
            int num3;
            num = this.GetActiveIconCount();
            num2 = 0;
            goto Label_0076;
        Label_000E:
            num3 = this.mFirstCardIndex + num2;
            if (num3 < this.mCardDatas.Count)
            {
                goto Label_0055;
            }
            this.mCardIcons[num2].ResetIcon();
            this.mCardIcons[num2].get_gameObject().SetActive(0);
            goto Label_0072;
        Label_0055:
            this.mCardIcons[num2].Setup(this.mCardDatas[num3]);
        Label_0072:
            num2 += 1;
        Label_0076:
            if (num2 < num)
            {
                goto Label_000E;
            }
            return;
        }

        private void RefreshNoEquipButtonObject()
        {
            this.mNoEquipButtonObject.SetActive(this.mCurrentPage == 0);
            return;
        }

        private void ResetSelectCardAnimation()
        {
            int num;
            int num2;
            Animator animator;
            num = this.GetActiveIconCount();
            num2 = 0;
            goto Label_0041;
        Label_000E:
            animator = this.mCardIcons[num2].GetComponent<Animator>();
            if ((animator == null) == null)
            {
                goto Label_0031;
            }
            goto Label_003D;
        Label_0031:
            animator.SetInteger("st", 0);
        Label_003D:
            num2 += 1;
        Label_0041:
            if (num2 < num)
            {
                goto Label_000E;
            }
            return;
        }

        public void SelectCardIcon(ConceptCardIcon selected_icon)
        {
            Animator animator;
            this.ResetSelectCardAnimation();
            if ((selected_icon == null) == null)
            {
                goto Label_0025;
            }
            this.mSelectedConceptCardData = null;
            ConceptCardEquipWindow.Instance.SetSelectedCardIcon(null);
            return;
        Label_0025:
            this.mSelectedConceptCardData = selected_icon.ConceptCard;
            ConceptCardEquipWindow.Instance.SetSelectedCardIcon(selected_icon);
            animator = selected_icon.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_005B;
            }
            animator.SetInteger("st", 1);
        Label_005B:
            return;
        }

        private void SelectedCardIconActive()
        {
            ConceptCardIcon icon;
            if (this.mSelectedConceptCardData != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            icon = this.mCardIcons.Find(new Predicate<ConceptCardIcon>(this.<SelectedCardIconActive>m__2CB));
            if ((icon != null) == null)
            {
                goto Label_0037;
            }
            this.SelectCardIcon(icon);
        Label_0037:
            return;
        }

        public void Sort(ConceptCardListSortWindow.Type SortType, ConceptCardListSortWindow.Type SortOrderType)
        {
            bool flag;
            int num;
            flag = 0;
            num = 0;
            goto Label_0025;
        Label_0009:
            if (this.mCardDatas[num] == null)
            {
                goto Label_001F;
            }
            goto Label_0021;
        Label_001F:
            flag = 1;
        Label_0021:
            num += 1;
        Label_0025:
            if (num < this.mCardDatas.Count)
            {
                goto Label_0009;
            }
            ConceptCardListSortWindow.Sort(SortType, SortOrderType, this.mCardDatas);
            if (flag == null)
            {
                goto Label_0056;
            }
            this.mCardDatas.Insert(0, null);
        Label_0056:
            this.RefreshIcon();
            this.ResetSelectCardAnimation();
            this.SelectedCardIconActive();
            return;
        }

        public int CurrentPage
        {
            get
            {
                return this.mCurrentPage;
            }
        }

        public int LastPage
        {
            get
            {
                return this.mLastPage;
            }
        }

        private RectTransform RT
        {
            get
            {
                if ((this.mRectTransform == null) == null)
                {
                    goto Label_0022;
                }
                this.mRectTransform = (RectTransform) base.get_transform();
            Label_0022:
                return this.mRectTransform;
            }
        }

        private GridLayoutGroup Grid
        {
            get
            {
                if ((this.mGrid == null) == null)
                {
                    goto Label_001D;
                }
                this.mGrid = base.GetComponent<GridLayoutGroup>();
            Label_001D:
                return this.mGrid;
            }
        }

        public ConceptCardData SelectedConceptCardData
        {
            get
            {
                return this.mSelectedConceptCardData;
            }
        }

        public bool IsIgnoreEquipedConceptCard
        {
            get
            {
                return ((PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CONCEPTCARD_EXCLUDE_EQUIPED, 0) == 0) == 0);
            }
            set
            {
                int num;
                num = (value == null) ? 0 : 1;
                PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CONCEPTCARD_EXCLUDE_EQUIPED, num, 0);
                return;
            }
        }

        private int CellCount
        {
            get
            {
                int num;
                float num2;
                float num3;
                float num4;
                float num5;
                float num6;
                float num7;
                float num8;
                float num9;
                int num10;
                int num11;
                Vector2 vector;
                Vector2 vector2;
                Vector2 vector3;
                Vector2 vector4;
                Rect rect;
                Rect rect2;
                num = 0x40;
                if ((this.Grid == null) == null)
                {
                    goto Label_0020;
                }
                DebugUtility.LogError("ERROR!! : Dont Setup GridLayoutGroup");
                return 0;
            Label_0020:
                num2 = &this.Grid.get_cellSize().x;
                num3 = &this.Grid.get_cellSize().y;
                num4 = &this.Grid.get_spacing().x;
                num5 = &this.Grid.get_spacing().y;
                num6 = (float) this.Grid.get_padding().get_horizontal();
                num7 = (float) this.Grid.get_padding().get_vertical();
                num8 = (&this.RT.get_rect().get_width() - num6) + num4;
                num9 = (&this.RT.get_rect().get_height() - num7) + num5;
                num10 = Mathf.FloorToInt(num8 / (num2 + num4));
                num11 = Mathf.FloorToInt(num9 / (num3 + num5));
                return Mathf.Clamp(num10 * num11, 0, num);
            }
        }

        [CompilerGenerated]
        private sealed class <GetCardPage>c__AnonStorey324
        {
            internal ConceptCardData target;

            public <GetCardPage>c__AnonStorey324()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2CC(ConceptCardData data)
            {
                return ((data == null) ? 0 : (data.UniqueID == this.target.UniqueID));
            }
        }

        [CompilerGenerated]
        private sealed class <Init>c__AnonStorey322
        {
            internal List<ConceptCardData> card_datas;

            public <Init>c__AnonStorey322()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Init>c__AnonStorey323
        {
            internal int i;
            internal ConceptCardEquipList.<Init>c__AnonStorey322 <>f__ref$802;

            public <Init>c__AnonStorey323()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2CA(UnitData u)
            {
                return ((u.ConceptCard == null) ? 0 : (u.ConceptCard.UniqueID == this.<>f__ref$802.card_datas[this.i].UniqueID));
            }
        }
    }
}

