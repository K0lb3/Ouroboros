namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "選択クリア", 0, 1), Pin(10, "フィルタ適用", 0, 10), Pin(5, "バックアップ作成", 0, 5), Pin(4, "複数選択を戻す", 0, 4), Pin(2, "アイコン更新", 0, 2), Pin(3, "複数選択の登録", 0, 3)]
    public class ConceptCardList : MonoBehaviour, IFlowInterface
    {
        private const string SAVE_TOGGLE_SELECT_SAME_CARD_KEY = "TOGGLE_SAME_CARD";
        public const int PIN_CLEAR = 1;
        public const int PIN_REFRESH = 2;
        public const int PIN_REGIST_MAT = 3;
        public const int PIN_REVERT_MAT = 4;
        public const int PIN_BACKUP_MAT = 5;
        public const int PIN_FILTER = 10;
        [SerializeField]
        private int MAX_MULTI_SELECT;
        [SerializeField]
        private ListType mListType;
        [SerializeField]
        private ListIconCalc mListIconCalc;
        [SerializeField]
        private GameObject mCardObjectTemplate;
        [SerializeField]
        private RectTransform mCardObjectParent;
        [SerializeField]
        private GameObject EmptyMessage;
        [SerializeField]
        private Text PageIndex;
        [SerializeField]
        private Text PageIndexMax;
        [SerializeField]
        private Button ForwardButton;
        [SerializeField]
        private Button PrevButton;
        [SerializeField]
        private Text SortTypeText;
        [SerializeField]
        private ImageArray FilterBgImages;
        [SerializeField]
        private Text CurrSelectedNum;
        [SerializeField]
        private Text MaxSelectedNum;
        [SerializeField]
        private Text TextSellZeny;
        [SerializeField]
        private Text TextMixCost;
        [SerializeField]
        private Text TextExp;
        [SerializeField]
        private Text TextTrust;
        [SerializeField]
        private GameObject TextWarningObject;
        [SerializeField]
        private Button[] SelectedInteractableButton;
        [SerializeField]
        private Text CurrentConceptCardNum;
        [SerializeField]
        private Text MaxConceptCardNum;
        [SerializeField]
        private Toggle mIgnoreSelectSameConceptCardToggle;
        private ConceptCardManager mCCManager;
        private List<GameObject> mCardIcons;
        private MultiConceptCard mSortDataList;
        private MultiConceptCard mSelectedMaterials;
        private MultiConceptCard mBackupSelectedMaterials;
        private int mPage;
        private int mMaxPages;
        private int mPageSize;

        public ConceptCardList()
        {
            this.MAX_MULTI_SELECT = 10;
            this.mCardIcons = new List<GameObject>();
            this.mSortDataList = new MultiConceptCard();
            this.mSelectedMaterials = new MultiConceptCard();
            this.mBackupSelectedMaterials = new MultiConceptCard();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <Filter>m__2D7(ConceptCardData x)
        {
            return (x.FilterEnhance(this.CCManager.SelectedConceptCardData.Param.iname) == 0);
        }

        [CompilerGenerated]
        private bool <Filter>m__2D8(ConceptCardData x)
        {
            return (x.Filter(this.CCManager.FilterType) == 0);
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch ((num - 1))
            {
                case 0:
                    goto Label_0037;

                case 1:
                    goto Label_0042;

                case 2:
                    goto Label_0054;

                case 3:
                    goto Label_005F;

                case 4:
                    goto Label_006A;

                case 5:
                    goto Label_0087;

                case 6:
                    goto Label_0087;

                case 7:
                    goto Label_0087;

                case 8:
                    goto Label_0087;

                case 9:
                    goto Label_0075;
            }
            goto Label_0087;
        Label_0037:
            this.ClearSelected();
            goto Label_0087;
        Label_0042:
            this.RefreshIconList(0);
            this.RefreshConceptCardNum();
            goto Label_0087;
        Label_0054:
            this.RegistMultiSelect();
            goto Label_0087;
        Label_005F:
            this.IncorporateMultiSelect();
            goto Label_0087;
        Label_006A:
            this.BackupMultiSelect();
            goto Label_0087;
        Label_0075:
            this.RefreshIconList(1);
            this.RefreshSortFilterObjects();
        Label_0087:
            return;
        }

        private void BackupMultiSelect()
        {
            this.mBackupSelectedMaterials.Clone(this.CCManager.SelectedMaterials);
            return;
        }

        private void ClearSelected()
        {
            this.mSelectedMaterials.Clear();
            this.RefreshIconList(0);
            return;
        }

        private MultiConceptCard CurrMaterials()
        {
            List<ConceptCardData> list;
            list = new List<ConceptCardData>(this.PlayerConceptCards);
            if ((this.CCManager != null) == null)
            {
                goto Label_0040;
            }
            this.Filter(list);
            ConceptCardListSortWindow.Sort(this.CCManager.SortType, this.CCManager.SortOrderType, list);
        Label_0040:
            this.mSortDataList.SetArray(list.ToArray());
            return this.mSortDataList;
        }

        public void Filter(List<ConceptCardData> card_list)
        {
            if ((this.CCManager == null) != null)
            {
                goto Label_001C;
            }
            if (card_list.Count != null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            if ((this.mIgnoreSelectSameConceptCardToggle != null) == null)
            {
                goto Label_0062;
            }
            if (this.CCManager.SelectedConceptCardData == null)
            {
                goto Label_0062;
            }
            if (this.mIgnoreSelectSameConceptCardToggle.get_isOn() == null)
            {
                goto Label_0062;
            }
            card_list.RemoveAll(new Predicate<ConceptCardData>(this.<Filter>m__2D7));
            return;
        Label_0062:
            card_list.RemoveAll(new Predicate<ConceptCardData>(this.<Filter>m__2D8));
            return;
        }

        private MultiConceptCard GetIconList()
        {
            ListType type;
            if (this.mListType == 3)
            {
                goto Label_0013;
            }
            goto Label_001A;
        Label_0013:
            return this.mSelectedMaterials;
        Label_001A:
            return this.CurrMaterials();
        }

        private unsafe bool GetToggleSameSelect()
        {
            string str;
            bool flag;
            if (PlayerPrefsUtility.HasKey("TOGGLE_SAME_CARD") != null)
            {
                goto Label_0011;
            }
            return 0;
        Label_0011:
            str = PlayerPrefsUtility.GetString("TOGGLE_SAME_CARD", string.Empty);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            flag = 0;
            if (bool.TryParse(str, &flag) != null)
            {
                goto Label_003F;
            }
            return 0;
        Label_003F:
            return flag;
        }

        public void GotoNextPage()
        {
            if (this.mPage >= (this.mMaxPages - 1))
            {
                goto Label_0028;
            }
            this.mPage += 1;
            this.RefreshIconList(0);
        Label_0028:
            return;
        }

        public void GotoPreviousPage()
        {
            if (this.mPage <= 0)
            {
                goto Label_0021;
            }
            this.mPage -= 1;
            this.RefreshIconList(0);
        Label_0021:
            return;
        }

        private void IncorporateMultiSelect()
        {
            this.mSelectedMaterials.Clone(this.mBackupSelectedMaterials);
            return;
        }

        public void Init()
        {
            if ((this.mCardObjectTemplate == null) != null)
            {
                goto Label_0022;
            }
            if ((this.mCardObjectParent == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            if ((this.mIgnoreSelectSameConceptCardToggle != null) == null)
            {
                goto Label_005B;
            }
            this.mIgnoreSelectSameConceptCardToggle.set_isOn(this.GetToggleSameSelect());
            this.CCManager.ToggleSameSelectCard = this.mIgnoreSelectSameConceptCardToggle.get_isOn();
        Label_005B:
            this.mCardObjectTemplate.SetActive(0);
            this.RefreshIconList(0);
            this.RefreshConceptCardNum();
            this.RefreshSortFilterObjects();
            return;
        }

        private void InstantiateIcons()
        {
            GameObject obj2;
            Transform transform;
            ListItemEvents events;
            this.mPageSize = (this.mListIconCalc != null) ? this.MAX_MULTI_SELECT : this.CellCount;
            goto Label_008A;
        Label_0027:
            obj2 = Object.Instantiate<GameObject>(this.mCardObjectTemplate);
            obj2.get_transform().SetParent(this.mCardObjectParent, 0);
            this.mCardIcons.Add(obj2);
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_008A;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
        Label_008A:
            if (this.mCardIcons.Count < this.mPageSize)
            {
                goto Label_0027;
            }
            return;
        }

        public void OnChangeIgnoreSameCardToggle(bool isOn)
        {
            if ((this.mIgnoreSelectSameConceptCardToggle == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.CCManager.ToggleSameSelectCard = isOn;
            this.RefreshIconList(1);
            this.RefreshSortFilterObjects();
            return;
        }

        private void OnItemDetail(GameObject go)
        {
            ConceptCardIcon icon;
            ConceptCardData data;
            icon = go.GetComponent<ConceptCardIcon>();
            if ((icon == null) == null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            data = icon.ConceptCard;
            if (data != null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            this.CCManager.SelectedConceptCardData = data;
            return;
        }

        private void OnItemSelect(GameObject go)
        {
            ConceptCardIcon icon;
            ConceptCardData data;
            ConceptCardIconMultiSelect select;
            icon = go.GetComponent<ConceptCardIcon>();
            if ((icon == null) == null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            data = icon.ConceptCard;
            if (data != null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            if (this.mSelectedMaterials.IsSelected(data) != null)
            {
                goto Label_004A;
            }
            if (this.mSelectedMaterials.Count < this.MAX_MULTI_SELECT)
            {
                goto Label_004A;
            }
            return;
        Label_004A:
            this.mSelectedMaterials.Flip(data);
            select = go.GetComponent<ConceptCardIconMultiSelect>();
            this.RefreshSelectParam(select, this.mSelectedMaterials);
            this.RefreshIconList(0);
            return;
        }

        private unsafe void RefreshConceptCardNum()
        {
            int num;
            int num2;
            if ((this.CurrentConceptCardNum != null) == null)
            {
                goto Label_0033;
            }
            this.CurrentConceptCardNum.set_text(&MonoSingleton<GameManager>.Instance.Player.ConceptCardNum.ToString());
        Label_0033:
            if ((this.MaxConceptCardNum != null) == null)
            {
                goto Label_0066;
            }
            this.MaxConceptCardNum.set_text(&MonoSingleton<GameManager>.Instance.Player.ConceptCardCap.ToString());
        Label_0066:
            return;
        }

        private unsafe void RefreshEnableParam(ConceptCardIconMultiSelect drawicons, MultiConceptCard materials, bool AcceptableExp, bool AcceptableTrust, bool CanAwake)
        {
            bool flag;
            UnitData data;
            int num;
            int num2;
            int num3;
            MultiConceptCard card;
            bool flag2;
            <RefreshEnableParam>c__AnonStorey327 storey;
            storey = new <RefreshEnableParam>c__AnonStorey327();
            storey.drawicons = drawicons;
            if ((storey.drawicons == null) == null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            flag = 1;
            if (materials.IsSelected(storey.drawicons.ConceptCard) != null)
            {
                goto Label_00FE;
            }
            data = MonoSingleton<GameManager>.Instance.Player.Units.Find(new Predicate<UnitData>(storey.<>m__2D9));
            if (((materials.Count < this.MAX_MULTI_SELECT) && (storey.drawicons.ConceptCard.Favorite == null)) && (data == null))
            {
                goto Label_0091;
            }
            flag = 0;
            goto Label_00FE;
        Label_0091:
            if (this.CCManager.SelectedConceptCardData == null)
            {
                goto Label_00FE;
            }
            card = new MultiConceptCard();
            card.Add(storey.drawicons.ConceptCard);
            ConceptCardManager.CalcTotalExpTrust(this.CCManager.SelectedConceptCardData, card, &num, &num2, &num3);
        Label_00EE:
            if (((((0 < num) && (AcceptableExp != null)) || ((0 < num2) && (AcceptableTrust != null))) ? 1 : CanAwake) != null)
            {
                goto Label_00FE;
            }
            flag = 0;
        Label_00FE:
            if (this.mListType != 2)
            {
                goto Label_0139;
            }
            if (storey.drawicons.ConceptCard.Param.not_sale == null)
            {
                goto Label_0139;
            }
            storey.drawicons.SetNotSellFlag(1);
            flag = 0;
            goto Label_0146;
        Label_0139:
            storey.drawicons.SetNotSellFlag(0);
        Label_0146:
            storey.drawicons.RefreshEnableParam(flag);
            return;
        }

        private unsafe void RefreshIconList(bool filter)
        {
            MultiConceptCard card;
            List<long> list;
            long num;
            List<OLong>.Enumerator enumerator;
            long num2;
            List<long>.Enumerator enumerator2;
            bool flag;
            Button button;
            Button[] buttonArray;
            int num3;
            this.InstantiateIcons();
            if (this.mCardIcons.Count != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if ((this.mIgnoreSelectSameConceptCardToggle != null) == null)
            {
                goto Label_0044;
            }
            this.mIgnoreSelectSameConceptCardToggle.set_isOn(this.CCManager.ToggleSameSelectCard);
            this.SaveSameConceptCardToggle();
        Label_0044:
            this.mSelectedMaterials = this.CCManager.SelectedMaterials;
            card = this.GetIconList();
            if (this.mListType != 1)
            {
                goto Label_0079;
            }
            card.Remove(this.CCManager.SelectedConceptCardData);
        Label_0079:
            if (filter == null)
            {
                goto Label_0119;
            }
            list = new List<long>();
            enumerator = this.mSelectedMaterials.GetIDList().GetEnumerator();
        Label_0096:
            try
            {
                goto Label_00BB;
            Label_009B:
                num = &enumerator.Current;
                if (card.Contains(num) != null)
                {
                    goto Label_00BB;
                }
                list.Add(num);
            Label_00BB:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_009B;
                }
                goto Label_00D8;
            }
            finally
            {
            Label_00CC:
                ((List<OLong>.Enumerator) enumerator).Dispose();
            }
        Label_00D8:
            enumerator2 = list.GetEnumerator();
        Label_00E0:
            try
            {
                goto Label_00FB;
            Label_00E5:
                num2 = &enumerator2.Current;
                this.mSelectedMaterials.Remove(num2);
            Label_00FB:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00E5;
                }
                goto Label_0119;
            }
            finally
            {
            Label_010C:
                ((List<long>.Enumerator) enumerator2).Dispose();
            }
        Label_0119:
            if (this.mPageSize <= 0)
            {
                goto Label_0160;
            }
            this.mMaxPages = ((card.Count + this.mPageSize) - 1) / this.mPageSize;
            this.mPage = Mathf.Max(0, Mathf.Min(this.mPage, this.mMaxPages - 1));
        Label_0160:
            this.RefreshIcons(card, this.mSelectedMaterials);
            this.RefreshPage();
            this.RefreshParameter();
            if ((this.EmptyMessage != null) == null)
            {
                goto Label_01A1;
            }
            this.EmptyMessage.SetActive((0 < card.Count) == 0);
        Label_01A1:
            if (this.SelectedInteractableButton == null)
            {
                goto Label_01FF;
            }
            flag = 0 < this.mSelectedMaterials.Count;
            buttonArray = this.SelectedInteractableButton;
            num3 = 0;
            goto Label_01F4;
        Label_01CC:
            button = buttonArray[num3];
            if ((button == null) == null)
            {
                goto Label_01E5;
            }
            goto Label_01EE;
        Label_01E5:
            button.set_interactable(flag);
        Label_01EE:
            num3 += 1;
        Label_01F4:
            if (num3 < ((int) buttonArray.Length))
            {
                goto Label_01CC;
            }
        Label_01FF:
            return;
        }

        private unsafe void RefreshIcons(MultiConceptCard drawicons, MultiConceptCard materials)
        {
            ConceptCardManager manager;
            bool flag;
            bool flag2;
            bool flag3;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            ConceptCardIcon icon;
            ConceptCardIconMultiSelect select;
            bool flag4;
            if (drawicons != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            manager = this.CCManager;
            flag = 1;
            flag2 = 1;
            flag3 = 1;
            if ((this.mListType != 1) || (manager.SelectedConceptCardData == null))
            {
                goto Label_0091;
            }
            ConceptCardManager.CalcTotalExpTrust(manager.SelectedConceptCardData, materials, &num, &num2, &num3);
            flag = num < manager.SelectedConceptCardData.GetExpToLevelMax();
            flag2 = num2 < manager.SelectedConceptCardData.GetTrustToLevelMax();
            if (manager.SelectedConceptCardData.GetReward() != null)
            {
                goto Label_006F;
            }
            flag2 = 0;
        Label_006F:
            if (manager.SelectedConceptCardData.AwakeCount < manager.SelectedConceptCardData.AwakeCountCap)
            {
                goto Label_0091;
            }
            flag3 = 0;
        Label_0091:
            num4 = 0;
            goto Label_018B;
        Label_0099:
            num5 = (this.mPage * this.mPageSize) + num4;
            this.mCardIcons[num4].SetActive(1);
            icon = this.mCardIcons[num4].GetComponent<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_00EE;
            }
            icon.Setup(drawicons.GetItem(num5));
        Label_00EE:
            if ((0 > num5) || (num5 >= drawicons.Count))
            {
                goto Label_0185;
            }
            select = this.mCardIcons[num4].GetComponent<ConceptCardIconMultiSelect>();
            if ((select != null) == null)
            {
                goto Label_0185;
            }
            flag4 = 1;
            if ((this.mListType != 1) || (manager.SelectedConceptCardData == null))
            {
                goto Label_016F;
            }
            flag4 = (flag3 == null) ? 0 : (manager.SelectedConceptCardData.Param.iname == select.ConceptCard.Param.iname);
        Label_016F:
            this.RefreshSelectParam(select, materials);
            this.RefreshEnableParam(select, materials, flag, flag2, flag4);
        Label_0185:
            num4 += 1;
        Label_018B:
            if (num4 < this.mCardIcons.Count)
            {
                goto Label_0099;
            }
            return;
        }

        private unsafe void RefreshPage()
        {
            int num;
            if ((this.PageIndex != null) == null)
            {
                goto Label_0037;
            }
            this.PageIndex.set_text(&Mathf.Min(this.mPage + 1, this.mMaxPages).ToString());
        Label_0037:
            if ((this.PageIndexMax != null) == null)
            {
                goto Label_005E;
            }
            this.PageIndexMax.set_text(&this.mMaxPages.ToString());
        Label_005E:
            if ((this.ForwardButton != null) == null)
            {
                goto Label_008A;
            }
            this.ForwardButton.set_interactable(this.mPage < (this.mMaxPages - 1));
        Label_008A:
            if ((this.PrevButton != null) == null)
            {
                goto Label_00AF;
            }
            this.PrevButton.set_interactable(this.mPage > 0);
        Label_00AF:
            return;
        }

        private void RefreshParameter()
        {
            this.RefreshSelected();
            this.RefreshTextSellZeny();
            this.RefreshTextMixCost();
            this.RefreshTextExpAndTrust();
            this.RefreshTextWarning();
            return;
        }

        private unsafe void RefreshSelected()
        {
            int num;
            if ((this.CurrSelectedNum == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.CurrSelectedNum.set_text(&this.mSelectedMaterials.Count.ToString());
            if ((this.MaxSelectedNum == null) == null)
            {
                goto Label_0042;
            }
            return;
        Label_0042:
            this.MaxSelectedNum.set_text(&this.MAX_MULTI_SELECT.ToString());
            return;
        }

        private void RefreshSelectParam(ConceptCardIconMultiSelect drawicons, MultiConceptCard materials)
        {
            bool flag;
            if ((drawicons == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            flag = materials.IsSelected(drawicons.ConceptCard);
            drawicons.RefreshSelectParam(flag);
            return;
        }

        private void RefreshSortFilterObjects()
        {
            if ((this.SortTypeText != null) == null)
            {
                goto Label_0031;
            }
            this.SortTypeText.set_text(LocalizedText.Get(ConceptCardListSortWindow.GetTypeString(this.CCManager.SortType)));
        Label_0031:
            if ((this.FilterBgImages != null) == null)
            {
                goto Label_0066;
            }
            this.FilterBgImages.ImageIndex = (this.CCManager.FilterType != 0x1f) ? 1 : 0;
        Label_0066:
            return;
        }

        private unsafe void RefreshTextExpAndTrust()
        {
            int num;
            int num2;
            int num3;
            ConceptCardManager.CalcTotalExpTrust(this.CCManager.SelectedConceptCardData, this.mSelectedMaterials, &num, &num2, &num3);
            if ((this.TextExp != null) == null)
            {
                goto Label_003F;
            }
            this.TextExp.set_text(&num.ToString());
        Label_003F:
            if ((this.TextTrust != null) == null)
            {
                goto Label_0061;
            }
            this.TextTrust.set_text(ConceptCardManager.ParseTrustFormat(num2));
        Label_0061:
            return;
        }

        private unsafe void RefreshTextMixCost()
        {
            int num;
            if ((this.TextMixCost == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = 0;
            ConceptCardManager.GalcTotalMixZeny(this.mSelectedMaterials, &num);
            this.TextMixCost.set_text(&num.ToString());
            return;
        }

        private unsafe void RefreshTextSellZeny()
        {
            int num;
            if ((this.TextSellZeny == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = 0;
            ConceptCardManager.GalcTotalSellZeny(this.mSelectedMaterials, &num);
            this.TextSellZeny.set_text(&num.ToString());
            return;
        }

        private void RefreshTextWarning()
        {
            Text text;
            string str;
            if ((this.TextWarningObject == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.TextWarningObject.SetActive(0);
            text = this.TextWarningObject.GetComponentInChildren<Text>();
            if ((text == null) == null)
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            text.set_text(string.Empty);
            str = ConceptCardManager.GetWarningTextByMaterials(this.mSelectedMaterials);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_006C;
            }
            text.set_text(str);
            this.TextWarningObject.SetActive(1);
        Label_006C:
            return;
        }

        private void RegistMultiSelect()
        {
            this.CCManager.SelectedMaterials = this.mSelectedMaterials;
            return;
        }

        private unsafe void SaveSameConceptCardToggle()
        {
            bool flag;
            if (string.IsNullOrEmpty("TOGGLE_SAME_CARD") != null)
            {
                goto Label_0020;
            }
            if ((this.mIgnoreSelectSameConceptCardToggle == null) == null)
            {
                goto Label_0021;
            }
        Label_0020:
            return;
        Label_0021:
            PlayerPrefsUtility.SetString("TOGGLE_SAME_CARD", &this.mIgnoreSelectSameConceptCardToggle.get_isOn().ToString(), 1);
            return;
        }

        private ConceptCardManager CCManager
        {
            get
            {
                if ((this.mCCManager == null) == null)
                {
                    goto Label_0038;
                }
                this.mCCManager = base.GetComponentInParent<ConceptCardManager>();
                if ((this.mCCManager == null) == null)
                {
                    goto Label_0038;
                }
                DebugUtility.LogError("Not found ConceptCardManager.");
            Label_0038:
                return this.mCCManager;
            }
        }

        private List<ConceptCardData> PlayerConceptCards
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.Player.ConceptCards;
            }
        }

        public int CellCount
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
                GridLayoutGroup group;
                RectTransform transform;
                Rect rect;
                float num8;
                float num9;
                int num10;
                int num11;
                Vector2 vector;
                Vector2 vector2;
                Vector2 vector3;
                Vector2 vector4;
                num = 0x40;
                group = this.mCardObjectParent.GetComponent<GridLayoutGroup>();
                if ((group == null) == null)
                {
                    goto Label_0029;
                }
                DebugUtility.LogError("Not found GridLayoutGroup.");
                return 0;
            Label_0029:
                num2 = &group.get_cellSize().x;
                num3 = &group.get_cellSize().y;
                num4 = &group.get_spacing().x;
                num5 = &group.get_spacing().y;
                num6 = (float) group.get_padding().get_horizontal();
                num7 = (float) group.get_padding().get_vertical();
                transform = (RectTransform) this.mCardObjectParent.get_transform();
                rect = transform.get_rect();
                num8 = (&rect.get_width() - num6) + num4;
                num9 = (&rect.get_height() - num7) + num5;
                num10 = Mathf.FloorToInt(num8 / (num2 + num4));
                num11 = Mathf.FloorToInt(num9 / (num3 + num5));
                return Mathf.Clamp(num10 * num11, 0, num);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshEnableParam>c__AnonStorey327
        {
            internal ConceptCardIconMultiSelect drawicons;

            public <RefreshEnableParam>c__AnonStorey327()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2D9(UnitData u)
            {
                return ((u.ConceptCard == null) ? 0 : (u.ConceptCard.UniqueID == this.drawicons.ConceptCard.UniqueID));
            }
        }

        public enum ListIconCalc
        {
            AUTO,
            SEPCIFY
        }

        public enum ListType
        {
            NORMAL,
            ENHANCE,
            SELL,
            MANAGER
        }
    }
}

