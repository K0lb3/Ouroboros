// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityRankUpPointAddWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 0)]
  public class AbilityRankUpPointAddWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Text ConfirmText;
    [SerializeField]
    private Text FreeCoin;
    [SerializeField]
    private Text PaidCoin;
    [SerializeField]
    private Text CurrentAmountCoin;
    [SerializeField]
    private Button CancelButton;
    [SerializeField]
    private Button DecideButton;
    [SerializeField]
    private Slider SelectSlider;
    [SerializeField]
    private Button PlusButton;
    [SerializeField]
    private Button MinusButton;
    [SerializeField]
    private Text SelectTotalNum;
    public AbilityRankUpPointAddWindow.DecideEvent OnDecide;
    public AbilityRankUpPointAddWindow.CancelEvent OnCancel;
    private GameManager gm;

    public AbilityRankUpPointAddWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.gm, (UnityEngine.Object) null))
        this.gm = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CancelButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CancelButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(Cancel)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DecideButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DecideButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(Decide)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PlusButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.PlusButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnAdd)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MinusButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MinusButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnRemove)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectSlider, (UnityEngine.Object) null))
      {
        int num = Mathf.Min(this.gm.Player.Coin, Math.Min((int) this.gm.MasterParam.FixParam.AbilityRankUpPointAddMax, (int) this.gm.MasterParam.FixParam.AbilityRankUpPointMax - this.gm.Player.AbilityRankUpCountNum));
        this.SelectSlider.set_minValue(1f);
        this.SelectSlider.set_maxValue((float) num);
        // ISSUE: method pointer
        ((UnityEvent<float>) this.SelectSlider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnValueChanged)));
        this.SelectSlider.set_value(this.SelectSlider.get_minValue());
      }
      this.Refresh();
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.gm, (UnityEngine.Object) null))
        return;
      int num1 = this.gm.Player.FreeCoin + this.gm.Player.ComCoin;
      int paidCoin = this.gm.Player.PaidCoin;
      int coin = this.gm.Player.Coin;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FreeCoin, (UnityEngine.Object) null))
        this.FreeCoin.set_text(num1.ToString());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PaidCoin, (UnityEngine.Object) null))
        this.PaidCoin.set_text(paidCoin.ToString());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentAmountCoin, (UnityEngine.Object) null))
        this.CurrentAmountCoin.set_text(coin.ToString());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectTotalNum, (UnityEngine.Object) null))
        this.SelectTotalNum.set_text("+" + this.SelectSlider.get_value().ToString());
      int num2 = (int) this.SelectSlider.get_value();
      string str = LocalizedText.Get("sys.CONFIRM_ABILITY_RANKUP_POINT_ADD", (object) ((int) this.gm.MasterParam.FixParam.AbilityRankupPointCoinRate * num2), (object) num2);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConfirmText, (UnityEngine.Object) null))
        return;
      this.ConfirmText.set_text(str);
    }

    private void OnAdd()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectSlider, (UnityEngine.Object) null))
        return;
      this.SelectSlider.set_value(Mathf.Min(this.SelectSlider.get_maxValue(), this.SelectSlider.get_value() + 1f));
    }

    private void OnRemove()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectSlider, (UnityEngine.Object) null))
        return;
      this.SelectSlider.set_value(Mathf.Max(this.SelectSlider.get_minValue(), this.SelectSlider.get_value() - 1f));
    }

    private void Cancel()
    {
      if (this.OnCancel != null)
        this.OnCancel();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void Decide()
    {
      if (this.OnDecide != null)
        this.OnDecide((int) this.SelectSlider.get_value());
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void OnValueChanged(float value)
    {
      this.SelectTotalNum.set_text("+" + ((int) value).ToString());
      int num = (int) this.SelectSlider.get_value();
      string str = LocalizedText.Get("sys.CONFIRM_ABILITY_RANKUP_POINT_ADD", (object) ((int) this.gm.MasterParam.FixParam.AbilityRankupPointCoinRate * num), (object) num);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConfirmText, (UnityEngine.Object) null))
        return;
      this.ConfirmText.set_text(str);
    }

    public void Activated(int pinID)
    {
    }

    public delegate void DecideEvent(int value);

    public delegate void CancelEvent();
  }
}
