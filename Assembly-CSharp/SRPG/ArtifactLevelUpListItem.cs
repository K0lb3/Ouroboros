// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactLevelUpListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class ArtifactLevelUpListItem : MonoBehaviour
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
    public ArtifactLevelUpListItem.SelectExpItem OnSelect;
    public ArtifactLevelUpListItem.ChangeToggleEvent ChangeUseMax;
    public ArtifactLevelUpListItem.CheckSliderValue OnCheck;
    private string mCurrentItemID;
    private int mMaxValue;
    private bool IsLock;
    private float mPrevValue;

    public ArtifactLevelUpListItem()
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
      if (Object.op_Equality((Object) this.UseExpItemSlider, (Object) null) || Object.op_Equality((Object) this.SelectUseNum, (Object) null))
        return;
      ArtifactData dataOfClass1 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).get_gameObject(), (ArtifactData) null);
      if (dataOfClass1 == null)
        return;
      ItemData dataOfClass2 = DataSource.FindDataOfClass<ItemData>(((Component) this).get_gameObject(), (ItemData) null);
      if (dataOfClass2 == null)
        return;
      int num1 = dataOfClass1.GetGainExpCap() - dataOfClass1.Exp;
      int num2 = Mathf.Max(1, Mathf.Min(dataOfClass2.Num, Mathf.CeilToInt((float) num1 / (float) (int) dataOfClass2.Param.value)));
      this.mCurrentItemID = dataOfClass2.Param.iname;
      ((UnityEventBase) this.UseExpItemSlider.get_onValueChanged()).RemoveAllListeners();
      this.UseExpItemSlider.set_minValue(0.0f);
      this.UseExpItemSlider.set_maxValue((float) num2);
      // ISSUE: method pointer
      ((UnityEvent<float>) this.UseExpItemSlider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnSelectUseNumChanged)));
      this.UseExpItemSlider.set_value(this.UseExpItemSlider.get_minValue());
      this.SelectUseNum.set_text(this.UseExpItemSlider.get_value().ToString());
      if (Object.op_Inequality((Object) this.UseItemNum, (Object) null))
        this.UseItemNum.set_text(this.UseExpItemSlider.get_value().ToString());
      if (Object.op_Inequality((Object) this.CheckUseMax, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.CheckUseMax.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeUseMax)));
      }
      this.mMaxValue = num2;
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
        ((Selectable) this.PlusBtn).set_interactable(!this.IsLock && (Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null) && (double) this.UseExpItemSlider.get_value() < (double) this.UseExpItemSlider.get_maxValue()));
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

    public void SetUseExpItemSliderValue(int value)
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

    public delegate void SelectExpItem(string iname, int value);

    public delegate void ChangeToggleEvent(string iname, bool is_on);

    public delegate int CheckSliderValue(string iname, int value);
  }
}
