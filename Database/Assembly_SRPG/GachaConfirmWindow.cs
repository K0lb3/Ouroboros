namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(11, "BuyCoin", 1, 11), Pin(1, "Close", 1, 1)]
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
        private SRPG.GachaCostType mGachaCostTtype;
        private string mUseTicket;
        public DecideEvent OnDecide;
        public CancelEvent OnCancel;
        private GameManager gm;
        private GachaRequestParam m_request;
        private Text RedrawText;
        private GameObject m_Default;
        private GameObject m_Redraw;

        public GachaConfirmWindow()
        {
            this.mConfirmText = string.Empty;
            this.mIsShowCoinStatus = 1;
            this.mUseTicket = string.Empty;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void OnClickBuyCoin()
        {
            if (this.OnCancel == null)
            {
                goto Label_0016;
            }
            this.OnCancel();
        Label_0016:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            return;
        }

        private void OnClickCancel()
        {
            if (this.OnCancel == null)
            {
                goto Label_0016;
            }
            this.OnCancel();
        Label_0016:
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            return;
        }

        private void OnClickDecide()
        {
            if (this.OnDecide == null)
            {
                goto Label_0016;
            }
            this.OnDecide();
        Label_0016:
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            return;
        }

        private unsafe void Refresh()
        {
            object[] objArray1;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            bool flag;
            string str;
            string str2;
            if ((this.gm == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = this.gm.Player.FreeCoin + this.gm.Player.ComCoin;
            num2 = this.gm.Player.PaidCoin;
            num3 = this.gm.Player.Coin;
            num4 = num3 - this.m_request.Cost;
            if (this.m_request.CostType != 2)
            {
                goto Label_0083;
            }
            num4 = num2 - this.m_request.Cost;
        Label_0083:
            num5 = this.gm.Player.GetItemAmount(this.m_request.Ticket);
            flag = (num4 < 0) == 0;
            if (this.m_request.CostType != 4)
            {
                goto Label_00BD;
            }
            flag = 1;
        Label_00BD:
            if (this.m_request.IsRedrawGacha == null)
            {
                goto Label_00D0;
            }
            flag = 1;
        Label_00D0:
            this.DecideButton.get_gameObject().SetActive(flag);
            this.BuyCoinButton.get_gameObject().SetActive(flag == 0);
            str = this.m_request.ConfirmText;
            if ((num4 >= 0) || (this.m_request.CostType == 4))
            {
                goto Label_014A;
            }
            if (this.m_request.CostType != 2)
            {
                goto Label_013E;
            }
            str = LocalizedText.Get("sys.GACHA_TEXT_PAIDCOIN_NOT_ENOUGH");
            goto Label_014A;
        Label_013E:
            str = LocalizedText.Get("sys.GACHA_TEXT_COIN_NOT_ENOUGH");
        Label_014A:
            if ((this.AmountBox != null) == null)
            {
                goto Label_0177;
            }
            this.AmountBox.SetActive((this.m_request.CostType == 4) == 0);
        Label_0177:
            if ((this.AmountTicketBox != null) == null)
            {
                goto Label_01A1;
            }
            this.AmountTicketBox.SetActive(this.m_request.CostType == 4);
        Label_01A1:
            if ((this.Confirm != null) == null)
            {
                goto Label_01E0;
            }
            this.Confirm.set_text(str);
            this.Confirm.get_gameObject().SetActive((this.m_request.CostType == 4) == 0);
        Label_01E0:
            if ((this.ConfirmTicket != null) == null)
            {
                goto Label_021C;
            }
            this.Confirm.set_text(str);
            this.ConfirmTicket.get_gameObject().SetActive(this.m_request.CostType == 4);
        Label_021C:
            if ((this.FreeCoin != null) == null)
            {
                goto Label_0250;
            }
            this.FreeCoin.set_text(&num.ToString());
            this.FreeCoin.get_gameObject().SetActive(1);
        Label_0250:
            if ((this.PaidCoin != null) == null)
            {
                goto Label_0284;
            }
            this.PaidCoin.set_text(&num2.ToString());
            this.PaidCoin.get_gameObject().SetActive(1);
        Label_0284:
            if ((this.CurrentAmountCoin != null) == null)
            {
                goto Label_02B8;
            }
            this.CurrentAmountCoin.set_text(&num3.ToString());
            this.CurrentAmountCoin.get_gameObject().SetActive(1);
        Label_02B8:
            if ((this.CurrentAmountTicket != null) == null)
            {
                goto Label_02EC;
            }
            this.CurrentAmountTicket.set_text(&num5.ToString());
            this.CurrentAmountTicket.get_gameObject().SetActive(1);
        Label_02EC:
            if ((this.CautionBox != null) == null)
            {
                goto Label_032A;
            }
            this.CautionBox.SetActive((this.m_request.CostType != 1) ? 0 : (this.m_request.Cost > 0));
        Label_032A:
            if ((this.CautionRedrawText != null) == null)
            {
                goto Label_039A;
            }
            if (this.m_request.IsRedrawGacha == null)
            {
                goto Label_039A;
            }
            if ((this.RedrawText != null) == null)
            {
                goto Label_039A;
            }
            objArray1 = new object[] { (int) this.m_request.RedrawRest };
            str2 = LocalizedText.Get("sys.GACHA_REDRAW_CAUTION", objArray1);
            this.RedrawText.set_text(str2);
            this.CautionRedrawText.SetActive(1);
        Label_039A:
            if ((this.m_Default != null) == null)
            {
                goto Label_03C4;
            }
            this.m_Default.SetActive(this.m_request.IsRedrawConfirm == 0);
        Label_03C4:
            if ((this.m_Redraw != null) == null)
            {
                goto Label_03EB;
            }
            this.m_Redraw.SetActive(this.m_request.IsRedrawConfirm);
        Label_03EB:
            return;
        }

        public void Set(GachaRequestParam _param)
        {
            this.m_request = _param;
            return;
        }

        private void Start()
        {
            SerializeValueBehaviour behaviour;
            Text text;
            SerializeValueBehaviour behaviour2;
            GameObject obj2;
            GameObject obj3;
            if ((MonoSingleton<GameManager>.GetInstanceDirect() != null) == null)
            {
                goto Label_001B;
            }
            this.gm = MonoSingleton<GameManager>.GetInstanceDirect();
        Label_001B:
            if ((this.Confirm != null) == null)
            {
                goto Label_003D;
            }
            this.Confirm.get_gameObject().SetActive(0);
        Label_003D:
            if ((this.FreeCoin != null) == null)
            {
                goto Label_005F;
            }
            this.FreeCoin.get_gameObject().SetActive(0);
        Label_005F:
            if ((this.PaidCoin != null) == null)
            {
                goto Label_0081;
            }
            this.PaidCoin.get_gameObject().SetActive(0);
        Label_0081:
            if ((this.CurrentAmountCoin != null) == null)
            {
                goto Label_00A3;
            }
            this.CurrentAmountCoin.get_gameObject().SetActive(0);
        Label_00A3:
            if ((this.CurrentAmountTicket != null) == null)
            {
                goto Label_00C5;
            }
            this.CurrentAmountTicket.get_gameObject().SetActive(0);
        Label_00C5:
            if ((this.DecideButton != null) == null)
            {
                goto Label_0103;
            }
            this.DecideButton.get_gameObject().SetActive(0);
            this.DecideButton.get_onClick().AddListener(new UnityAction(this, this.OnClickDecide));
        Label_0103:
            if ((this.BuyCoinButton != null) == null)
            {
                goto Label_0141;
            }
            this.BuyCoinButton.get_gameObject().SetActive(0);
            this.BuyCoinButton.get_onClick().AddListener(new UnityAction(this, this.OnClickBuyCoin));
        Label_0141:
            if ((this.CancelButton != null) == null)
            {
                goto Label_016E;
            }
            this.CancelButton.get_onClick().AddListener(new UnityAction(this, this.OnClickCancel));
        Label_016E:
            if ((this.ConfirmTicket != null) == null)
            {
                goto Label_0190;
            }
            this.ConfirmTicket.get_gameObject().SetActive(0);
        Label_0190:
            if ((this.AmountBox != null) == null)
            {
                goto Label_01AD;
            }
            this.AmountBox.SetActive(0);
        Label_01AD:
            if ((this.AmountTicketBox != null) == null)
            {
                goto Label_01CA;
            }
            this.AmountTicketBox.SetActive(0);
        Label_01CA:
            if ((this.CautionRedrawText != null) == null)
            {
                goto Label_0217;
            }
            behaviour = this.CautionRedrawText.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_020B;
            }
            text = behaviour.list.GetUILabel("text");
            this.RedrawText = text;
        Label_020B:
            this.CautionRedrawText.SetActive(0);
        Label_0217:
            behaviour2 = base.GetComponent<SerializeValueBehaviour>();
            if ((behaviour2 != null) == null)
            {
                goto Label_025C;
            }
            obj2 = behaviour2.list.GetGameObject("default");
            obj3 = behaviour2.list.GetGameObject("redraw");
            this.m_Default = obj2;
            this.m_Redraw = obj3;
        Label_025C:
            this.Refresh();
            return;
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
                return;
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
                return;
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
                return;
            }
        }

        public SRPG.GachaCostType GachaCostType
        {
            get
            {
                return this.mGachaCostTtype;
            }
            set
            {
                this.mGachaCostTtype = value;
                return;
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
                return;
            }
        }

        public delegate void CancelEvent();

        public delegate void DecideEvent();
    }
}

