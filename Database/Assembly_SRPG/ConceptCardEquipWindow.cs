namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(20, "前のページ", 0, 20), Pin(30, "念装アイコン選択", 0, 30), Pin(40, "OKボタン", 0, 40), Pin(50, "外すボタン", 0, 50), Pin(60, "真理念装データ取得完了", 0, 60), Pin(10, "次のページ", 0, 10), Pin(0x3fc, "ウィンドウ閉じる", 1, 0x3fc), Pin(0x406, "念装詳細設定完了", 1, 0x406), Pin(70, "ソート", 0, 70), Pin(80, "念装詳細設定", 0, 80), Pin(0x3e8, "装備変更", 1, 0x3e8), Pin(0x3f2, "真理念装データ取得開始", 1, 0x3f2)]
    public class ConceptCardEquipWindow : MonoBehaviour, IFlowInterface
    {
        public const int PIN_PAGE_NEXT = 10;
        public const int PIN_PAGE_BACK = 20;
        public const int PIN_CARD_SELECT_ICON = 30;
        public const int PIN_CLICK_OK = 40;
        public const int PIN_CLICK_RELEASE = 50;
        public const int PIN_END_REQUEST_CARD_DATAS = 60;
        public const int PIN_SORT_DATAS = 70;
        public const int PIN_SET_DETAIL_DATA = 80;
        public const int PIN_CHANGE_EQUIP = 0x3e8;
        public const int PIN_START_REQUEST_CARD_DATAS = 0x3f2;
        public const int PIN_CLOSE_WINDOW = 0x3fc;
        public const int PIN_SET_DETAIL_DATA_END = 0x406;
        private static ConceptCardEquipWindow _instance;
        [SerializeField]
        private ConceptCardEquipList mConceptCardEquipList;
        [SerializeField]
        private Text mCurrentPageText;
        [SerializeField]
        private Text mLastPageText;
        [SerializeField]
        private Button mNextPageButton;
        [SerializeField]
        private Button mPrevPageButton;
        [SerializeField]
        private Toggle mIgnoreEquipedConceptCardToggle;
        [SerializeField]
        private ConceptCardIcon mIcon;
        [SerializeField]
        private Text mName;
        [SerializeField]
        private Text mLvText;
        [SerializeField]
        private Text mLvMaxText;
        [SerializeField]
        private Text mExpText;
        [SerializeField]
        private Slider mExpSlider;
        [SerializeField]
        private StatusList mStatus;
        [SerializeField]
        private Text SortText;
        [SerializeField]
        private GameObject mParamViewObject;
        [SerializeField]
        private GameObject mEmptyParamViewObject;
        [SerializeField]
        private GameObject mTrustObject;
        [SerializeField]
        private Text mTrustText;
        private BaseStatus mBaseAdd;
        private BaseStatus mBaseMul;
        private BaseStatus mUnitAdd;
        private BaseStatus mUnitMul;
        private bool IsInitalized;
        private FlowNode_ReqConceptCardSet mFlownodeReqConceptCardSet;
        private ConceptCardData mSelectedCardData;
        private UnitData mSelectedUnit;
        public ConceptCardListSortWindow.Type SortType;
        public ConceptCardListSortWindow.Type SortOrderType;

        public ConceptCardEquipWindow()
        {
            this.mBaseAdd = new BaseStatus();
            this.mBaseMul = new BaseStatus();
            this.mUnitAdd = new BaseStatus();
            this.mUnitMul = new BaseStatus();
            base..ctor();
            return;
        }

        private void AcceptEquipConceptCard(GameObject obj)
        {
            this.mFlownodeReqConceptCardSet.SetEquipParam(this.mSelectedCardData.UniqueID, this.mSelectedUnit.UniqueID);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e8);
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 10)
            {
                goto Label_0047;
            }
            if (num == 20)
            {
                goto Label_0052;
            }
            if (num == 30)
            {
                goto Label_005D;
            }
            if (num == 40)
            {
                goto Label_0068;
            }
            if (num == 50)
            {
                goto Label_0073;
            }
            if (num == 60)
            {
                goto Label_007E;
            }
            if (num == 70)
            {
                goto Label_0089;
            }
            if (num == 80)
            {
                goto Label_00AB;
            }
            goto Label_00D6;
        Label_0047:
            this.PageNext();
            goto Label_00D6;
        Label_0052:
            this.PageBack();
            goto Label_00D6;
        Label_005D:
            this.SelectCardIcon();
            goto Label_00D6;
        Label_0068:
            this.ClickOKButton();
            goto Label_00D6;
        Label_0073:
            this.ClickReleaseButton();
            goto Label_00D6;
        Label_007E:
            this.EndRequestConceptCardDatas();
            goto Label_00D6;
        Label_0089:
            this.mConceptCardEquipList.Sort(this.SortType, this.SortOrderType);
            this.SetSortName();
            goto Label_00D6;
        Label_00AB:
            GlobalVars.SelectedConceptCardData.Set(this.mSelectedCardData);
            ConceptCardEquipDetail.SetSelectedUnitData(this.mSelectedUnit);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x406);
        Label_00D6:
            return;
        }

        private void Awake()
        {
            _instance = this;
            return;
        }

        private void CancelEquipConceptCard(GameObject obj)
        {
        }

        private void ClickOKButton()
        {
            object[] objArray1;
            bool flag;
            UnitData data;
            string str;
            flag = 0;
            if (this.mSelectedUnit.ConceptCard == null)
            {
                goto Label_0045;
            }
            if (this.mSelectedCardData == null)
            {
                goto Label_0045;
            }
            flag = this.mSelectedUnit.ConceptCard.UniqueID == this.mSelectedCardData.UniqueID;
        Label_0045:
            if (this.mSelectedCardData == null)
            {
                goto Label_0056;
            }
            if (flag == null)
            {
                goto Label_0062;
            }
        Label_0056:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3fc);
            return;
        Label_0062:
            data = this.mSelectedCardData.GetOwner();
            if (data == null)
            {
                goto Label_00CE;
            }
            if (data.UniqueID == this.mSelectedUnit.UniqueID)
            {
                goto Label_00CE;
            }
            objArray1 = new object[] { data.UnitParam.name };
            UIUtility.ConfirmBox(LocalizedText.Get("sys.CONCEPT_CARD_EQUIP_CONFIRM", objArray1), new UIUtility.DialogResultEvent(this.AcceptEquipConceptCard), new UIUtility.DialogResultEvent(this.CancelEquipConceptCard), null, 0, -1, null, null);
            return;
        Label_00CE:
            this.AcceptEquipConceptCard(base.get_gameObject());
            return;
        }

        private void ClickReleaseButton()
        {
            if (this.mSelectedUnit.ConceptCard == null)
            {
                goto Label_002C;
            }
            if (this.mSelectedUnit.ConceptCard.UniqueID > 0L)
            {
                goto Label_0038;
            }
        Label_002C:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3fc);
            return;
        Label_0038:
            this.mFlownodeReqConceptCardSet.SetReleaseParam(this.mSelectedUnit.ConceptCard.UniqueID);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e8);
            return;
        }

        private void EndRequestConceptCardDatas()
        {
            this.IsInitalized = 1;
            this.mConceptCardEquipList.Init(MonoSingleton<GameManager>.Instance.Player.ConceptCards, this.mSelectedUnit, 0);
            this.mConceptCardEquipList.OpenSelectIconExistPage(this.mSelectedUnit.ConceptCard);
            return;
        }

        public ConceptCardData GetSelectedCardData()
        {
            return this.mConceptCardEquipList.SelectedConceptCardData;
        }

        public void Init(UnitData unit)
        {
            string str;
            this.mSelectedUnit = unit;
            if ((this.mIgnoreEquipedConceptCardToggle != null) == null)
            {
                goto Label_002E;
            }
            this.mIgnoreEquipedConceptCardToggle.set_isOn(this.mConceptCardEquipList.IsIgnoreEquipedConceptCard);
        Label_002E:
            this.mFlownodeReqConceptCardSet = base.GetComponent<FlowNode_ReqConceptCardSet>();
            this.SetParam(this.mSelectedUnit.ConceptCard, this.mSelectedUnit, this.mSelectedUnit.JobIndex);
            this.SetActiveParamViewObject(this.mSelectedUnit.ConceptCard == null);
            str = "-";
            this.mLastPageText.set_text(str);
            this.mCurrentPageText.set_text(str);
            this.mConceptCardEquipList.HideTemplateObject();
            this.StartRequestConceptCardDatas();
            return;
        }

        public void OnChangeIgnoreEquipedCardToggle(bool isOn)
        {
            if (this.IsInitalized != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mConceptCardEquipList.IsIgnoreEquipedConceptCard = isOn;
            this.mConceptCardEquipList.Init(MonoSingleton<GameManager>.Instance.Player.ConceptCards, this.mSelectedUnit, 1);
            return;
        }

        private void OnDestroy()
        {
            _instance = null;
            return;
        }

        public void PageBack()
        {
            this.mConceptCardEquipList.PageBack();
            return;
        }

        public void PageNext()
        {
            this.mConceptCardEquipList.PageNext();
            return;
        }

        private unsafe void RefreshPageText()
        {
            bool flag;
            bool flag2;
            int num;
            int num2;
            num = this.mConceptCardEquipList.CurrentPage + 1;
            this.mCurrentPageText.set_text(&num.ToString());
            num2 = this.mConceptCardEquipList.LastPage + 1;
            this.mLastPageText.set_text(&num2.ToString());
            if (((this.mNextPageButton != null) == null) || ((this.mPrevPageButton != null) == null))
            {
                goto Label_00CA;
            }
            flag = (this.mConceptCardEquipList.LastPage <= 0) ? 0 : (this.mConceptCardEquipList.CurrentPage < this.mConceptCardEquipList.LastPage);
            flag2 = (this.mConceptCardEquipList.LastPage <= 0) ? 0 : (this.mConceptCardEquipList.CurrentPage > 0);
            this.mNextPageButton.set_interactable(flag);
            this.mPrevPageButton.set_interactable(flag2);
        Label_00CA:
            return;
        }

        private void SelectCardIcon()
        {
            SerializeValueList list;
            ConceptCardIcon icon;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_0087;
            }
            icon = list.GetComponent<ConceptCardIcon>("_self");
            this.mConceptCardEquipList.SelectCardIcon(icon);
            this.SetActiveParamViewObject(icon == null);
            this.mSelectedCardData = ((icon != null) == null) ? null : icon.ConceptCard;
            if (this.mSelectedCardData == null)
            {
                goto Label_0087;
            }
            if (this.mSelectedUnit == null)
            {
                goto Label_0087;
            }
            this.SetParam(this.mSelectedCardData, this.mSelectedUnit, this.mSelectedUnit.JobIndex);
        Label_0087:
            return;
        }

        private void SetActiveParamViewObject(bool is_empty)
        {
            this.mParamViewObject.SetActive(is_empty == 0);
            this.mEmptyParamViewObject.SetActive(is_empty);
            return;
        }

        private unsafe void SetParam(ConceptCardData card_data, UnitData unit, int job_index)
        {
            JobData data;
            int num;
            float num2;
            int num3;
            int num4;
            int num5;
            int num6;
            BaseStatus status;
            BaseStatus status2;
            List<ConceptCardEquipEffect> list;
            int num7;
            OInt num8;
            OInt num9;
            if (((card_data != null) && (unit != null)) && (((int) unit.Jobs.Length) > job_index))
            {
                goto Label_001B;
            }
            return;
        Label_001B:
            data = unit.Jobs[job_index];
            this.mName.set_text(card_data.Param.name);
            this.mLvText.set_text(&card_data.Lv.ToString());
            this.mLvMaxText.set_text(&card_data.CurrentLvCap.ToString());
            if ((this.mTrustObject != null) == null)
            {
                goto Label_009C;
            }
            this.mTrustObject.SetActive((card_data.GetReward() == null) ? 0 : 1);
        Label_009C:
            ConceptCardManager.SubstituteTrustFormat(card_data, this.mTrustText, card_data.Trust, 0);
            num = 0;
            num2 = 1f;
            if (card_data.Lv >= card_data.CurrentLvCap)
            {
                goto Label_0152;
            }
            num3 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp(card_data.Rarity, card_data.Lv);
            num4 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp(card_data.Rarity, card_data.Lv + 1);
            num5 = card_data.Exp - num3;
            num6 = num4 - num3;
            num2 = ((float) num5) / ((float) num6);
            num = num4 - card_data.Exp;
        Label_0152:
            this.mExpText.set_text(&num.ToString());
            this.mExpSlider.set_value(num2);
            if ((this.mIcon != null) == null)
            {
                goto Label_018D;
            }
            this.mIcon.Setup(card_data);
        Label_018D:
            this.mBaseAdd.Clear();
            this.mBaseMul.Clear();
            this.mUnitAdd.Clear();
            this.mUnitMul.Clear();
            status = new BaseStatus();
            status2 = new BaseStatus();
            list = card_data.GetEnableEquipEffects(unit, data);
            num7 = 0;
            goto Label_020B;
        Label_01D9:
            list[num7].GetStatus(&status, &status2);
            this.mBaseAdd.Add(status);
            this.mBaseMul.Add(status2);
            num7 += 1;
        Label_020B:
            if (num7 < list.Count)
            {
                goto Label_01D9;
            }
            this.mStatus.SetValues(this.mBaseAdd, this.mBaseMul, this.mUnitAdd, this.mUnitMul, 0);
            return;
        }

        public void SetSelectedCardIcon(ConceptCardIcon card_icon)
        {
            this.mSelectedCardData = ((card_icon != null) == null) ? null : card_icon.ConceptCard;
            return;
        }

        public void SetSortName()
        {
            if ((this.SortText != null) == null)
            {
                goto Label_002C;
            }
            this.SortText.set_text(LocalizedText.Get(ConceptCardListSortWindow.GetTypeString(this.SortType)));
        Label_002C:
            return;
        }

        private void Start()
        {
            this.SortType = ConceptCardListSortWindow.LoadDataType();
            this.SortOrderType = ConceptCardListSortWindow.LoadDataOrderType();
            this.SetSortName();
            return;
        }

        private void StartRequestConceptCardDatas()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3f2);
            return;
        }

        private void Update()
        {
            if (this.IsInitalized != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.RefreshPageText();
            return;
        }

        public static ConceptCardEquipWindow Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}

