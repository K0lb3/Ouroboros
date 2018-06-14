// Decompiled with JetBrains decompiler
// Type: SRPG.GachaConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "BuyCoin", FlowNode.PinTypes.Output, 11)]
  public class GachaConfirmWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Text Confirm;
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
    private Button BuyCoinButton;
    [SerializeField]
    private GameObject CautionBox;
    [SerializeField]
    private GameObject AmountBox;
    [SerializeField]
    private GameObject AmountTicketBox;
    [SerializeField]
    private Text CurrentAmountTicket;
    [SerializeField]
    private Text ConfirmTicket;
    private string mConfirmText;
    private int mCost;
    private bool mIsShowCoinStatus;
    private GachaButton.GachaCostType mGachaCostTtype;
    private string mUseTicket;
    public GachaConfirmWindow.DecideEvent OnDecide;
    public GachaConfirmWindow.CancelEvent OnCancel;
    private GameManager gm;

    public GachaConfirmWindow()
    {
      base.\u002Ector();
    }

    public string ConfirmText
    {
      get
      {
        return this.mConfirmText;
      }
      set
      {
        this.mConfirmText = value;
      }
    }

    public int Cost
    {
      get
      {
        return this.mCost;
      }
      set
      {
        this.mCost = value;
      }
    }

    public bool IsShowCoinStatus
    {
      get
      {
        return this.mIsShowCoinStatus;
      }
      set
      {
        this.mIsShowCoinStatus = value;
      }
    }

    public GachaButton.GachaCostType GachaCostType
    {
      get
      {
        return this.mGachaCostTtype;
      }
      set
      {
        this.mGachaCostTtype = value;
      }
    }

    public string UseTicket
    {
      get
      {
        return this.mUseTicket;
      }
      set
      {
        this.mUseTicket = value;
      }
    }

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        this.gm = MonoSingleton<GameManager>.GetInstanceDirect();
      if (Object.op_Inequality((Object) this.Confirm, (Object) null))
        ((Component) this.Confirm).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.FreeCoin, (Object) null))
        ((Component) this.FreeCoin).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.PaidCoin, (Object) null))
        ((Component) this.PaidCoin).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.CurrentAmountCoin, (Object) null))
        ((Component) this.CurrentAmountCoin).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.CurrentAmountTicket, (Object) null))
        ((Component) this.CurrentAmountTicket).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.DecideButton, (Object) null))
      {
        ((Component) this.DecideButton).get_gameObject().SetActive(false);
        // ISSUE: method pointer
        ((UnityEvent) this.DecideButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickDecide)));
      }
      if (Object.op_Inequality((Object) this.BuyCoinButton, (Object) null))
      {
        ((Component) this.BuyCoinButton).get_gameObject().SetActive(false);
        // ISSUE: method pointer
        ((UnityEvent) this.BuyCoinButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickBuyCoin)));
      }
      if (Object.op_Inequality((Object) this.CancelButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CancelButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickCancel)));
      }
      if (Object.op_Inequality((Object) this.ConfirmTicket, (Object) null))
        ((Component) this.ConfirmTicket).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.AmountBox, (Object) null))
        this.AmountBox.SetActive(false);
      if (Object.op_Inequality((Object) this.AmountTicketBox, (Object) null))
        this.AmountTicketBox.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.gm, (Object) null))
        return;
      int num1 = this.gm.Player.FreeCoin + this.gm.Player.ComCoin;
      int paidCoin = this.gm.Player.PaidCoin;
      int coin = this.gm.Player.Coin;
      int num2 = coin - this.Cost;
      if (this.GachaCostType == GachaButton.GachaCostType.COIN_P)
        num2 = paidCoin - this.Cost;
      int itemAmount = this.gm.Player.GetItemAmount(this.UseTicket);
      bool flag = num2 >= 0;
      if (this.GachaCostType == GachaButton.GachaCostType.TICKET)
        flag = true;
      ((Component) this.DecideButton).get_gameObject().SetActive(flag);
      ((Component) this.BuyCoinButton).get_gameObject().SetActive(!flag);
      if (num2 < 0 && this.GachaCostType != GachaButton.GachaCostType.TICKET)
        this.mConfirmText = this.GachaCostType != GachaButton.GachaCostType.COIN_P ? LocalizedText.Get("sys.GACHA_TEXT_COIN_NOT_ENOUGH") : LocalizedText.Get("sys.GACHA_TEXT_PAIDCOIN_NOT_ENOUGH");
      if (Object.op_Inequality((Object) this.AmountBox, (Object) null))
        this.AmountBox.SetActive(this.GachaCostType != GachaButton.GachaCostType.TICKET);
      if (Object.op_Inequality((Object) this.AmountTicketBox, (Object) null))
        this.AmountTicketBox.SetActive(this.GachaCostType == GachaButton.GachaCostType.TICKET);
      if (Object.op_Inequality((Object) this.Confirm, (Object) null))
      {
        this.Confirm.set_text(this.mConfirmText);
        ((Component) this.Confirm).get_gameObject().SetActive(this.GachaCostType != GachaButton.GachaCostType.TICKET);
      }
      if (Object.op_Inequality((Object) this.ConfirmTicket, (Object) null))
      {
        this.ConfirmTicket.set_text(this.mConfirmText);
        ((Component) this.ConfirmTicket).get_gameObject().SetActive(this.GachaCostType == GachaButton.GachaCostType.TICKET);
      }
      if (Object.op_Inequality((Object) this.FreeCoin, (Object) null))
      {
        this.FreeCoin.set_text(num1.ToString());
        ((Component) this.FreeCoin).get_gameObject().SetActive(true);
      }
      if (Object.op_Inequality((Object) this.PaidCoin, (Object) null))
      {
        this.PaidCoin.set_text(paidCoin.ToString());
        ((Component) this.PaidCoin).get_gameObject().SetActive(true);
      }
      if (Object.op_Inequality((Object) this.CurrentAmountCoin, (Object) null))
      {
        this.CurrentAmountCoin.set_text(coin.ToString());
        ((Component) this.CurrentAmountCoin).get_gameObject().SetActive(true);
      }
      if (Object.op_Inequality((Object) this.CurrentAmountTicket, (Object) null))
      {
        this.CurrentAmountTicket.set_text(itemAmount.ToString());
        ((Component) this.CurrentAmountTicket).get_gameObject().SetActive(true);
      }
      if (!Object.op_Inequality((Object) this.CautionBox, (Object) null))
        return;
      this.CautionBox.SetActive(this.GachaCostType == GachaButton.GachaCostType.COIN);
    }

    private void OnClickDecide()
    {
      if (this.OnDecide != null)
        this.OnDecide();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void OnClickCancel()
    {
      if (this.OnCancel != null)
        this.OnCancel();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void OnClickBuyCoin()
    {
      if (this.OnCancel != null)
        this.OnCancel();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    public delegate void DecideEvent();

    public delegate void CancelEvent();
  }
}
