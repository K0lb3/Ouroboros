// Decompiled with JetBrains decompiler
// Type: SRPG.HealApBar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Output, 0)]
  public class HealApBar : MonoBehaviour
  {
    public Slider slider;
    public int UseItemNum;
    public GameObject HealApBase;
    public Text Num;
    public Text HealApNum;
    public Text ItemName;
    public Text ItemCheckUse;
    private ItemData mData;
    public Button up_button;
    public Button down_button;
    public Text CurrentAP;

    public HealApBar()
    {
      base.\u002Ector();
    }

    public int HealNum
    {
      get
      {
        return this.mData.Param.value * this.UseItemNum;
      }
    }

    public bool IsOverFlow
    {
      get
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        return this.HealNum + instance.Player.Stamina > instance.Player.StaminaStockCap;
      }
    }

    private void OnEnable()
    {
      // ISSUE: method pointer
      ((UnityEvent<float>) this.slider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnValueChanged)));
      this.mData = DataSource.FindDataOfClass<ItemData>(this.HealApBase, (ItemData) null);
      this.slider.set_maxValue((float) this.GetMaxNum());
      this.slider.set_minValue(1f);
      this.UseItemNum = 1;
      this.Refresh();
    }

    public void Refresh()
    {
      this.slider.set_maxValue((float) this.GetMaxNum());
      this.slider.set_value((float) this.UseItemNum);
      this.Num.set_text(this.UseItemNum.ToString());
      this.HealApNum.set_text((this.UseItemNum * this.mData.Param.value).ToString());
      this.ItemName.set_text(LocalizedText.Get("sys.TEXT_APHEAL_ITEM_NUM", new object[1]
      {
        (object) this.mData.Param.name
      }));
      this.ItemCheckUse.set_text(LocalizedText.Get("sys.TEXT_APHEAL_CHECK_USE", new object[1]
      {
        (object) this.mData.Param.name
      }));
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      ((Selectable) this.up_button).set_interactable((double) this.slider.get_maxValue() > (double) this.slider.get_value());
      ((Selectable) this.down_button).set_interactable((double) this.slider.get_minValue() < (double) this.slider.get_value());
      if (!Object.op_Inequality((Object) this.CurrentAP, (Object) null))
        return;
      this.CurrentAP.set_text(LocalizedText.Get("sys.TEXT_DENOMINATOR", (object) MonoSingleton<GameManager>.Instance.Player.Stamina, (object) MonoSingleton<GameManager>.Instance.Player.StaminaStockCap));
    }

    public int GetMaxNum()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num = instance.Player.StaminaStockCap - instance.Player.Stamina;
      if (this.mData.Param.value == 0)
        return 1;
      return Mathf.Min((num - 1) / this.mData.Param.value + 1, this.mData.Num);
    }

    public void OnValueChanged(float value)
    {
      this.UseItemNum = Mathf.FloorToInt(value);
      this.UseItemNum = Mathf.Max(this.UseItemNum, 1);
      this.slider.set_value((float) this.UseItemNum);
      this.Refresh();
    }

    public void Up()
    {
      ++this.UseItemNum;
      this.UseItemNum = Mathf.Min(this.UseItemNum, this.GetMaxNum());
      this.Refresh();
    }

    public void Down()
    {
      --this.UseItemNum;
      this.UseItemNum = Mathf.Max(this.UseItemNum, 1);
      this.Refresh();
    }
  }
}
