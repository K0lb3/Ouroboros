// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardLevelUpListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardLevelUpListItem : MonoBehaviour
  {
    [SerializeField]
    private Slider UseExpItemSlider;
    [SerializeField]
    private Button PlusBtn;
    [SerializeField]
    private Button MinusBtn;
    [SerializeField]
    private Text SelectUseNum;
    [SerializeField]
    private Toggle CheckUseMax;
    [SerializeField]
    private Text UseItemNum;
    [SerializeField]
    private Text ConceptCardEnExp;
    [SerializeField]
    private ConceptCardIcon CardIcon;
    [SerializeField]
    private Text CardNum;
    [SerializeField]
    private GameObject ExpObject;
    [SerializeField]
    private int MaxCardNum;
    public ConceptCardLevelUpListItem.SelectExpItem OnSelect;
    public ConceptCardLevelUpListItem.ChangeToggleEvent ChangeUseMax;
    public ConceptCardLevelUpListItem.CheckSliderValue OnCheck;
    private string mCurrentItemID;
    private int mMaxValue;
    private bool IsLock;
    private float mPrevValue;
    private ConceptCardMaterialData mConceptCardMaterialData;
    private ConceptCardData mCardData;

    public ConceptCardLevelUpListItem()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.PlusBtn, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.PlusBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnAddNum)));
      }
      if (Object.op_Inequality((Object) this.MinusBtn, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MinusBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnRemoveNum)));
      }
      this.Init();
    }

    private void Init()
    {
      if (Object.op_Equality((Object) this.UseExpItemSlider, (Object) null) || Object.op_Equality((Object) this.SelectUseNum, (Object) null) || (Object.op_Equality((Object) this.CardIcon, (Object) null) || this.mConceptCardMaterialData == null))
        return;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam((string) this.mConceptCardMaterialData.IName);
      if (conceptCardParam == null)
        return;
      JSON_ConceptCard json = new JSON_ConceptCard();
      json.iid = (long) this.mConceptCardMaterialData.UniqueID;
      json.iname = (string) this.mConceptCardMaterialData.IName;
      json.exp = conceptCardParam.en_exp;
      json.trust = conceptCardParam.en_trust;
      this.mCardData = new ConceptCardData();
      this.mCardData.Deserialize(json);
      this.CardIcon.Setup(this.mCardData);
      this.mCurrentItemID = (string) this.mConceptCardMaterialData.IName;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      int num;
      if (this.mConceptCardMaterialData.Param.type == eCardType.Enhance_exp)
      {
        num = Mathf.Min(instance.SelectedConceptCardData.GetExpToLevelMax() / this.mConceptCardMaterialData.Param.en_exp, (int) this.mConceptCardMaterialData.Num);
        if (this.mConceptCardMaterialData.Param.en_exp * num < instance.SelectedConceptCardData.GetExpToLevelMax() && num < (int) this.mConceptCardMaterialData.Num)
          ++num;
      }
      else
      {
        if (this.mConceptCardMaterialData.Param.type != eCardType.Enhance_trust)
          return;
        num = Mathf.Min(instance.SelectedConceptCardData.GetTrustToLevelMax() / this.mConceptCardMaterialData.Param.en_trust, (int) this.mConceptCardMaterialData.Num);
        if (this.mConceptCardMaterialData.Param.en_trust * num < instance.SelectedConceptCardData.GetTrustToLevelMax() && num < (int) this.mConceptCardMaterialData.Num)
          ++num;
      }
      ((UnityEventBase) this.UseExpItemSlider.get_onValueChanged()).RemoveAllListeners();
      this.UseExpItemSlider.set_minValue(0.0f);
      this.UseExpItemSlider.set_maxValue((float) num);
      // ISSUE: method pointer
      ((UnityEvent<float>) this.UseExpItemSlider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnSelectUseNumChanged)));
      this.UseExpItemSlider.set_value(this.UseExpItemSlider.get_minValue());
      this.SelectUseNum.set_text(this.UseExpItemSlider.get_value().ToString());
      if (Object.op_Inequality((Object) this.UseItemNum, (Object) null))
        this.UseItemNum.set_text(this.UseExpItemSlider.get_value().ToString());
      if (Object.op_Inequality((Object) this.ConceptCardEnExp, (Object) null))
        this.ConceptCardEnExp.set_text(this.mCardData.Param.en_exp.ToString());
      this.mMaxValue = num;
      if (Object.op_Inequality((Object) this.CardNum, (Object) null))
        this.CardNum.set_text(Mathf.Min((int) this.mConceptCardMaterialData.Num, this.MaxCardNum).ToString());
      if (Object.op_Inequality((Object) this.CheckUseMax, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.CheckUseMax.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeUseMax)));
      }
      if (Object.op_Inequality((Object) this.PlusBtn, (Object) null))
        ((Selectable) this.PlusBtn).set_interactable(Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null) && (double) this.UseExpItemSlider.get_value() < (double) this.UseExpItemSlider.get_maxValue());
      if (!Object.op_Inequality((Object) this.MinusBtn, (Object) null))
        return;
      ((Selectable) this.MinusBtn).set_interactable(Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null) && (double) this.UseExpItemSlider.get_value() > (double) this.UseExpItemSlider.get_minValue());
    }

    private void Refresh(float value)
    {
      if (Object.op_Equality((Object) this.UseExpItemSlider, (Object) null))
        return;
      this.UseExpItemSlider.set_value((float) Mathf.Min(this.mMaxValue, (int) value));
      if (Object.op_Inequality((Object) this.SelectUseNum, (Object) null))
        this.SelectUseNum.set_text(((int) this.UseExpItemSlider.get_value()).ToString());
      if (Object.op_Inequality((Object) this.UseItemNum, (Object) null))
        this.UseItemNum.set_text(((int) this.UseExpItemSlider.get_value()).ToString());
      if (Object.op_Inequality((Object) this.PlusBtn, (Object) null))
        ((Selectable) this.PlusBtn).set_interactable(Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null) && (double) this.UseExpItemSlider.get_value() < (double) this.UseExpItemSlider.get_maxValue() && !this.IsLock);
      if (Object.op_Inequality((Object) this.MinusBtn, (Object) null))
        ((Selectable) this.MinusBtn).set_interactable(Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null) && (double) this.UseExpItemSlider.get_value() > (double) this.UseExpItemSlider.get_minValue());
      this.mPrevValue = this.UseExpItemSlider.get_value();
    }

    private void OnSelectUseNumChanged(float value)
    {
      if (this.OnCheck != null && (double) value > (double) this.mPrevValue)
      {
        int num = this.OnCheck(this.mCurrentItemID, (int) value);
        if (num >= 0)
        {
          value = (float) num;
          this.UseExpItemSlider.set_value(value);
        }
      }
      if ((double) value > (double) this.mPrevValue && this.IsLock)
      {
        this.UseExpItemSlider.set_value(this.mPrevValue);
      }
      else
      {
        if (this.OnSelect != null)
          this.OnSelect(this.mCurrentItemID, (int) value);
        this.Refresh(value);
      }
    }

    private void OnAddNum()
    {
      if (!Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null))
        return;
      Slider useExpItemSlider = this.UseExpItemSlider;
      useExpItemSlider.set_value(useExpItemSlider.get_value() + 1f);
    }

    private void OnRemoveNum()
    {
      if (!Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null))
        return;
      Slider useExpItemSlider = this.UseExpItemSlider;
      useExpItemSlider.set_value(useExpItemSlider.get_value() - 1f);
      this.IsLock = false;
      ((Selectable) this.PlusBtn).set_interactable(true);
    }

    public void OnChangeUseMax(bool value)
    {
      if (this.ChangeUseMax == null)
        return;
      this.ChangeUseMax(this.mCurrentItemID, value);
    }

    public bool IsUseMax()
    {
      if (Object.op_Equality((Object) this.CheckUseMax, (Object) null))
        return false;
      return this.CheckUseMax.get_isOn();
    }

    public void SetUseParamItemSliderValue(int value)
    {
      if (value < 0)
        return;
      ((UnityEventBase) this.UseExpItemSlider.get_onValueChanged()).RemoveAllListeners();
      this.Refresh((float) value);
      // ISSUE: method pointer
      ((UnityEvent<float>) this.UseExpItemSlider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnSelectUseNumChanged)));
    }

    public void Reset()
    {
      if (Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null))
        this.UseExpItemSlider.set_value(this.UseExpItemSlider.get_minValue());
      if (!Object.op_Inequality((Object) this.SelectUseNum, (Object) null))
        return;
      this.SelectUseNum.set_text("0");
    }

    public void SetInputLock(bool islock)
    {
      if (Object.op_Inequality((Object) this.PlusBtn, (Object) null))
        ((Selectable) this.PlusBtn).set_interactable(islock);
      if (Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null))
        ((Selectable) this.UseExpItemSlider).set_interactable(islock ? islock : (double) this.UseExpItemSlider.get_value() != 0.0);
      this.IsLock = !islock;
    }

    public void SetUseMax(bool is_on)
    {
      this.CheckUseMax.set_isOn(is_on);
    }

    public void AddConceptCardData(ConceptCardMaterialData material_data)
    {
      this.mConceptCardMaterialData = material_data;
    }

    public string GetConceptCardIName()
    {
      if (this.mConceptCardMaterialData == null)
        return (string) null;
      return (string) this.mConceptCardMaterialData.IName;
    }

    public ConceptCardData GetConceptCardData()
    {
      return this.mCardData;
    }

    public void SetExpObject(bool flag)
    {
      if (Object.op_Equality((Object) this.ExpObject, (Object) null))
        return;
      this.ExpObject.SetActive(flag);
    }

    public delegate void SelectExpItem(string iname, int value);

    public delegate void ChangeToggleEvent(string iname, bool is_on);

    public delegate int CheckSliderValue(string iname, int value);
  }
}
