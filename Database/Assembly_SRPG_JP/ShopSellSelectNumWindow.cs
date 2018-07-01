// Decompiled with JetBrains decompiler
// Type: SRPG.ShopSellSelectNumWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "決定", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(101, "キャンセル", FlowNode.PinTypes.Output, 11)]
  public class ShopSellSelectNumWindow : MonoBehaviour, IFlowInterface
  {
    public Text TxtTitle;
    public Text TxtSellItemPriceStr;
    public Text TxtSellNumStr;
    public Text TxtSellTotalPriceStr;
    public Text TxtDecide;
    public Slider SellNumSlider;
    public Button BtnDecide;
    public Button BtnCancel;
    public Button BtnPlus;
    public Button BtnMinus;
    private int mSaveSellNum;

    public ShopSellSelectNumWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.TxtTitle, (Object) null))
        this.TxtTitle.set_text(LocalizedText.Get("sys.SHOP_SELL_SELECTNUM_TITLE"));
      if (Object.op_Inequality((Object) this.TxtSellItemPriceStr, (Object) null))
        this.TxtSellItemPriceStr.set_text(LocalizedText.Get("sys.SELL_PRICE"));
      if (Object.op_Inequality((Object) this.TxtSellNumStr, (Object) null))
        this.TxtSellNumStr.set_text(LocalizedText.Get("sys.SELL_NUM"));
      if (Object.op_Inequality((Object) this.TxtSellTotalPriceStr, (Object) null))
        this.TxtSellTotalPriceStr.set_text(LocalizedText.Get("sys.SELL_PRICE"));
      if (Object.op_Inequality((Object) this.TxtDecide, (Object) null))
        this.TxtDecide.set_text(LocalizedText.Get("sys.CMD_DECIDE"));
      if (Object.op_Inequality((Object) this.BtnDecide, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnDecide.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnDecide)));
      }
      if (Object.op_Inequality((Object) this.BtnCancel, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnCancel.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
      }
      if (Object.op_Inequality((Object) this.BtnPlus, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnPlus.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnAddNum)));
      }
      if (Object.op_Inequality((Object) this.BtnMinus, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnMinus.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnRemoveNum)));
      }
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void OnAddNum()
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem == null)
        return;
      if (selectSellItem.num < selectSellItem.item.Num)
        ++selectSellItem.num;
      if (!Object.op_Inequality((Object) this.SellNumSlider, (Object) null))
        return;
      this.SellNumSlider.set_value((float) selectSellItem.num);
    }

    private void OnRemoveNum()
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem == null)
        return;
      if (selectSellItem.num > 0)
        --selectSellItem.num;
      if (!Object.op_Inequality((Object) this.SellNumSlider, (Object) null))
        return;
      this.SellNumSlider.set_value((float) selectSellItem.num);
    }

    private void Refresh()
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem == null)
        return;
      if (Object.op_Inequality((Object) this.SellNumSlider, (Object) null))
      {
        ((UnityEventBase) this.SellNumSlider.get_onValueChanged()).RemoveAllListeners();
        this.SellNumSlider.set_maxValue((float) selectSellItem.item.Num);
        // ISSUE: method pointer
        ((UnityEvent<float>) this.SellNumSlider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnSellNumChanged)));
        this.SellNumSlider.set_value((float) selectSellItem.num);
      }
      this.mSaveSellNum = selectSellItem.num;
      DataSource.Bind<SellItem>(((Component) this).get_gameObject(), selectSellItem);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnSellNumChanged(float value)
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem == null)
        return;
      selectSellItem.num = (int) value;
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnDecide()
    {
      SellItem selectSellItem = GlobalVars.SelectSellItem;
      if (selectSellItem.num > 0)
      {
        if (!GlobalVars.SellItemList.Contains(selectSellItem))
          GlobalVars.SellItemList.Add(selectSellItem);
      }
      else
      {
        selectSellItem.index = -1;
        selectSellItem.num = 0;
        GlobalVars.SellItemList.Remove(selectSellItem);
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnCancel()
    {
      GlobalVars.SelectSellItem.num = this.mSaveSellNum;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
  }
}
