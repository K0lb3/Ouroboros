// Decompiled with JetBrains decompiler
// Type: SRPG.GachaConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "BuyCoin", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 1)]
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
    [SerializeField]
    private GameObject CautionRedrawText;
    private string mConfirmText;
    private int mCost;
    private bool mIsShowCoinStatus;
    private GachaCostType mGachaCostTtype;
    private string mUseTicket;
    public GachaConfirmWindow.DecideEvent OnDecide;
    public GachaConfirmWindow.CancelEvent OnCancel;
    private GameManager gm;
    private GachaRequestParam m_request;
    private Text RedrawText;
    private GameObject m_Default;
    private GameObject m_Redraw;

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

    public GachaCostType GachaCostType
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
      if (Object.op_Inequality((Object) this.CautionRedrawText, (Object) null))
      {
        SerializeValueBehaviour component = (SerializeValueBehaviour) this.CautionRedrawText.GetComponent<SerializeValueBehaviour>();
        if (Object.op_Inequality((Object) component, (Object) null))
          this.RedrawText = component.list.GetUILabel("text");
        this.CautionRedrawText.SetActive(false);
      }
      SerializeValueBehaviour component1 = (SerializeValueBehaviour) ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (Object.op_Inequality((Object) component1, (Object) null))
      {
        GameObject gameObject1 = component1.list.GetGameObject("default");
        GameObject gameObject2 = component1.list.GetGameObject("redraw");
        this.m_Default = gameObject1;
        this.m_Redraw = gameObject2;
      }
      this.Refresh();
    }

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.gm, (Object) null))
        return;
      int num1 = this.gm.Player.FreeCoin + this.gm.Player.ComCoin;
      int paidCoin = this.gm.Player.PaidCoin;
      int coin = this.gm.Player.Coin;
      int num2 = coin - this.m_request.Cost;
      if (this.m_request.CostType == GachaCostType.COIN_P)
        num2 = paidCoin - this.m_request.Cost;
      int itemAmount = this.gm.Player.GetItemAmount(this.m_request.Ticket);
      bool flag = num2 >= 0;
      if (this.m_request.CostType == GachaCostType.TICKET)
        flag = true;
      if (this.m_request.IsRedrawGacha)
        flag = true;
      ((Component) this.DecideButton).get_gameObject().SetActive(flag);
      ((Component) this.BuyCoinButton).get_gameObject().SetActive(!flag);
      string str = this.m_request.ConfirmText;
      if (num2 < 0 && this.m_request.CostType != GachaCostType.TICKET)
        str = this.m_request.CostType != GachaCostType.COIN_P ? LocalizedText.Get("sys.GACHA_TEXT_COIN_NOT_ENOUGH") : LocalizedText.Get("sys.GACHA_TEXT_PAIDCOIN_NOT_ENOUGH");
      if (Object.op_Inequality((Object) this.AmountBox, (Object) null))
        this.AmountBox.SetActive(this.m_request.CostType != GachaCostType.TICKET);
      if (Object.op_Inequality((Object) this.AmountTicketBox, (Object) null))
        this.AmountTicketBox.SetActive(this.m_request.CostType == GachaCostType.TICKET);
      if (Object.op_Inequality((Object) this.Confirm, (Object) null))
      {
        this.Confirm.set_text(str);
        ((Component) this.Confirm).get_gameObject().SetActive(this.m_request.CostType != GachaCostType.TICKET);
      }
      if (Object.op_Inequality((Object) this.ConfirmTicket, (Object) null))
      {
        this.Confirm.set_text(str);
        ((Component) this.ConfirmTicket).get_gameObject().SetActive(this.m_request.CostType == GachaCostType.TICKET);
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
      if (Object.op_Inequality((Object) this.CautionBox, (Object) null))
        this.CautionBox.SetActive(this.m_request.CostType == GachaCostType.COIN && this.m_request.Cost > 0);
      if (Object.op_Inequality((Object) this.CautionRedrawText, (Object) null) && this.m_request.IsRedrawGacha && Object.op_Inequality((Object) this.RedrawText, (Object) null))
      {
        this.RedrawText.set_text(LocalizedText.Get("sys.GACHA_REDRAW_CAUTION", new object[1]
        {
          (object) this.m_request.RedrawRest
        }));
        this.CautionRedrawText.SetActive(true);
      }
      if (Object.op_Inequality((Object) this.m_Default, (Object) null))
        this.m_Default.SetActive(!this.m_request.IsRedrawConfirm);
      if (!Object.op_Inequality((Object) this.m_Redraw, (Object) null))
        return;
      this.m_Redraw.SetActive(this.m_request.IsRedrawConfirm);
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

    public void Set(GachaRequestParam _param)
    {
      this.m_request = _param;
    }

    public delegate void DecideEvent();

    public delegate void CancelEvent();
  }
}
