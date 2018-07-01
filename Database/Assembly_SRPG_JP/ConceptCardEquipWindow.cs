// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEquipWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(20, "前のページ", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(30, "念装アイコン選択", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(40, "OKボタン", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(50, "外すボタン", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(60, "真理念装データ取得完了", FlowNode.PinTypes.Input, 60)]
  [FlowNode.Pin(10, "次のページ", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1020, "ウィンドウ閉じる", FlowNode.PinTypes.Output, 1020)]
  [FlowNode.Pin(1030, "念装詳細設定完了", FlowNode.PinTypes.Output, 1030)]
  [FlowNode.Pin(70, "ソート", FlowNode.PinTypes.Input, 70)]
  [FlowNode.Pin(80, "念装詳細設定", FlowNode.PinTypes.Input, 80)]
  [FlowNode.Pin(1000, "装備変更", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1010, "真理念装データ取得開始", FlowNode.PinTypes.Output, 1010)]
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
    public const int PIN_CHANGE_EQUIP = 1000;
    public const int PIN_START_REQUEST_CARD_DATAS = 1010;
    public const int PIN_CLOSE_WINDOW = 1020;
    public const int PIN_SET_DETAIL_DATA_END = 1030;
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
      base.\u002Ector();
    }

    public static ConceptCardEquipWindow Instance
    {
      get
      {
        return ConceptCardEquipWindow._instance;
      }
    }

    private void Awake()
    {
      ConceptCardEquipWindow._instance = this;
    }

    private void Start()
    {
      this.SortType = ConceptCardListSortWindow.LoadDataType();
      this.SortOrderType = ConceptCardListSortWindow.LoadDataOrderType();
      this.SetSortName();
    }

    private void OnDestroy()
    {
      ConceptCardEquipWindow._instance = (ConceptCardEquipWindow) null;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.PageNext();
          break;
        case 20:
          this.PageBack();
          break;
        case 30:
          this.SelectCardIcon();
          break;
        case 40:
          this.ClickOKButton();
          break;
        case 50:
          this.ClickReleaseButton();
          break;
        case 60:
          this.EndRequestConceptCardDatas();
          break;
        case 70:
          this.mConceptCardEquipList.Sort(this.SortType, this.SortOrderType);
          this.SetSortName();
          break;
        case 80:
          GlobalVars.SelectedConceptCardData.Set(this.mSelectedCardData);
          ConceptCardEquipDetail.SetSelectedUnitData(this.mSelectedUnit);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1030);
          break;
      }
    }

    private void Update()
    {
      if (!this.IsInitalized)
        return;
      this.RefreshPageText();
    }

    public void Init(UnitData unit)
    {
      this.mSelectedUnit = unit;
      if (Object.op_Inequality((Object) this.mIgnoreEquipedConceptCardToggle, (Object) null))
        this.mIgnoreEquipedConceptCardToggle.set_isOn(this.mConceptCardEquipList.IsIgnoreEquipedConceptCard);
      this.mFlownodeReqConceptCardSet = (FlowNode_ReqConceptCardSet) ((Component) this).GetComponent<FlowNode_ReqConceptCardSet>();
      this.SetParam(this.mSelectedUnit.ConceptCard, this.mSelectedUnit, this.mSelectedUnit.JobIndex);
      this.SetActiveParamViewObject(this.mSelectedUnit.ConceptCard == null);
      Text mCurrentPageText = this.mCurrentPageText;
      string str1 = "-";
      this.mLastPageText.set_text(str1);
      string str2 = str1;
      mCurrentPageText.set_text(str2);
      this.mConceptCardEquipList.HideTemplateObject();
      this.StartRequestConceptCardDatas();
    }

    public void PageNext()
    {
      this.mConceptCardEquipList.PageNext();
    }

    public void PageBack()
    {
      this.mConceptCardEquipList.PageBack();
    }

    private void RefreshPageText()
    {
      this.mCurrentPageText.set_text((this.mConceptCardEquipList.CurrentPage + 1).ToString());
      this.mLastPageText.set_text((this.mConceptCardEquipList.LastPage + 1).ToString());
      if (!Object.op_Inequality((Object) this.mNextPageButton, (Object) null) || !Object.op_Inequality((Object) this.mPrevPageButton, (Object) null))
        return;
      bool flag1 = this.mConceptCardEquipList.LastPage > 0 && this.mConceptCardEquipList.CurrentPage < this.mConceptCardEquipList.LastPage;
      bool flag2 = this.mConceptCardEquipList.LastPage > 0 && this.mConceptCardEquipList.CurrentPage > 0;
      ((Selectable) this.mNextPageButton).set_interactable(flag1);
      ((Selectable) this.mPrevPageButton).set_interactable(flag2);
    }

    private void SelectCardIcon()
    {
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      ConceptCardIcon component = currentValue.GetComponent<ConceptCardIcon>("_self");
      this.mConceptCardEquipList.SelectCardIcon(component);
      this.SetActiveParamViewObject(Object.op_Equality((Object) component, (Object) null));
      this.mSelectedCardData = !Object.op_Inequality((Object) component, (Object) null) ? (ConceptCardData) null : component.ConceptCard;
      if (this.mSelectedCardData == null || this.mSelectedUnit == null)
        return;
      this.SetParam(this.mSelectedCardData, this.mSelectedUnit, this.mSelectedUnit.JobIndex);
    }

    private void SetParam(ConceptCardData card_data, UnitData unit, int job_index)
    {
      if (card_data == null || unit == null || unit.Jobs.Length <= job_index)
        return;
      JobData job = unit.Jobs[job_index];
      this.mName.set_text(card_data.Param.name);
      this.mLvText.set_text(card_data.Lv.ToString());
      this.mLvMaxText.set_text(card_data.CurrentLvCap.ToString());
      if (Object.op_Inequality((Object) this.mTrustObject, (Object) null))
        this.mTrustObject.SetActive(card_data.GetReward() != null);
      ConceptCardManager.SubstituteTrustFormat(card_data, this.mTrustText, (int) card_data.Trust, false);
      int num1 = 0;
      float num2 = 1f;
      if ((int) card_data.Lv < (int) card_data.CurrentLvCap)
      {
        int conceptCardLevelExp1 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp((int) card_data.Rarity, (int) card_data.Lv);
        int conceptCardLevelExp2 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp((int) card_data.Rarity, (int) card_data.Lv + 1);
        num2 = (float) ((int) card_data.Exp - conceptCardLevelExp1) / (float) (conceptCardLevelExp2 - conceptCardLevelExp1);
        num1 = conceptCardLevelExp2 - (int) card_data.Exp;
      }
      this.mExpText.set_text(num1.ToString());
      this.mExpSlider.set_value(num2);
      if (Object.op_Inequality((Object) this.mIcon, (Object) null))
        this.mIcon.Setup(card_data);
      this.mBaseAdd.Clear();
      this.mBaseMul.Clear();
      this.mUnitAdd.Clear();
      this.mUnitMul.Clear();
      BaseStatus fixed_status = new BaseStatus();
      BaseStatus scale_status = new BaseStatus();
      List<ConceptCardEquipEffect> enableEquipEffects = card_data.GetEnableEquipEffects(unit, job);
      for (int index = 0; index < enableEquipEffects.Count; ++index)
      {
        enableEquipEffects[index].GetStatus(ref fixed_status, ref scale_status);
        this.mBaseAdd.Add(fixed_status);
        this.mBaseMul.Add(scale_status);
      }
      this.mStatus.SetValues(this.mBaseAdd, this.mBaseMul, this.mUnitAdd, this.mUnitMul, false);
    }

    private void SetActiveParamViewObject(bool is_empty)
    {
      this.mParamViewObject.SetActive(!is_empty);
      this.mEmptyParamViewObject.SetActive(is_empty);
    }

    private void ClickOKButton()
    {
      bool flag = false;
      if (this.mSelectedUnit.ConceptCard != null && this.mSelectedCardData != null)
        flag = (long) this.mSelectedUnit.ConceptCard.UniqueID == (long) this.mSelectedCardData.UniqueID;
      if (this.mSelectedCardData == null || flag)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
      }
      else
      {
        UnitData owner = this.mSelectedCardData.GetOwner();
        if (owner != null && owner.UniqueID != this.mSelectedUnit.UniqueID)
          UIUtility.ConfirmBox(LocalizedText.Get("sys.CONCEPT_CARD_EQUIP_CONFIRM", new object[1]
          {
            (object) owner.UnitParam.name
          }), new UIUtility.DialogResultEvent(this.AcceptEquipConceptCard), new UIUtility.DialogResultEvent(this.CancelEquipConceptCard), (GameObject) null, false, -1, (string) null, (string) null);
        else
          this.AcceptEquipConceptCard(((Component) this).get_gameObject());
      }
    }

    private void AcceptEquipConceptCard(GameObject obj)
    {
      this.mFlownodeReqConceptCardSet.SetEquipParam((long) this.mSelectedCardData.UniqueID, this.mSelectedUnit.UniqueID);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
    }

    private void CancelEquipConceptCard(GameObject obj)
    {
    }

    private void ClickReleaseButton()
    {
      if (this.mSelectedUnit.ConceptCard == null || (long) this.mSelectedUnit.ConceptCard.UniqueID <= 0L)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
      }
      else
      {
        this.mFlownodeReqConceptCardSet.SetReleaseParam((long) this.mSelectedUnit.ConceptCard.UniqueID);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
      }
    }

    private void StartRequestConceptCardDatas()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
    }

    private void EndRequestConceptCardDatas()
    {
      this.IsInitalized = true;
      this.mConceptCardEquipList.Init(MonoSingleton<GameManager>.Instance.Player.ConceptCards, this.mSelectedUnit, false);
      this.mConceptCardEquipList.OpenSelectIconExistPage(this.mSelectedUnit.ConceptCard);
    }

    public void SetSortName()
    {
      if (!Object.op_Inequality((Object) this.SortText, (Object) null))
        return;
      this.SortText.set_text(LocalizedText.Get(ConceptCardListSortWindow.GetTypeString(this.SortType)));
    }

    public ConceptCardData GetSelectedCardData()
    {
      return this.mConceptCardEquipList.SelectedConceptCardData;
    }

    public void SetSelectedCardIcon(ConceptCardIcon card_icon)
    {
      this.mSelectedCardData = !Object.op_Inequality((Object) card_icon, (Object) null) ? (ConceptCardData) null : card_icon.ConceptCard;
    }

    public void OnChangeIgnoreEquipedCardToggle(bool isOn)
    {
      if (!this.IsInitalized)
        return;
      this.mConceptCardEquipList.IsIgnoreEquipedConceptCard = isOn;
      this.mConceptCardEquipList.Init(MonoSingleton<GameManager>.Instance.Player.ConceptCards, this.mSelectedUnit, true);
    }
  }
}
