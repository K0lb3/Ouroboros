namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Close", 1, 0)]
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
        public DecideEvent OnDecide;
        public CancelEvent OnCancel;
        private GameManager gm;

        public AbilityRankUpPointAddWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Cancel()
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

        private void Decide()
        {
            if (this.OnDecide == null)
            {
                goto Label_0022;
            }
            this.OnDecide((int) this.SelectSlider.get_value());
        Label_0022:
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            return;
        }

        private void OnAdd()
        {
            if ((this.SelectSlider != null) == null)
            {
                goto Label_003D;
            }
            this.SelectSlider.set_value(Mathf.Min(this.SelectSlider.get_maxValue(), this.SelectSlider.get_value() + 1f));
        Label_003D:
            return;
        }

        private void OnRemove()
        {
            if ((this.SelectSlider != null) == null)
            {
                goto Label_003D;
            }
            this.SelectSlider.set_value(Mathf.Max(this.SelectSlider.get_minValue(), this.SelectSlider.get_value() - 1f));
        Label_003D:
            return;
        }

        private unsafe void OnValueChanged(float value)
        {
            object[] objArray1;
            int num;
            int num2;
            string str;
            int num3;
            num3 = (int) value;
            this.SelectTotalNum.set_text("+" + &num3.ToString());
            num = (int) this.SelectSlider.get_value();
            num2 = this.gm.MasterParam.FixParam.AbilityRankupPointCoinRate * num;
            objArray1 = new object[] { (int) num2, (int) num };
            str = LocalizedText.Get("sys.CONFIRM_ABILITY_RANKUP_POINT_ADD", objArray1);
            if ((this.ConfirmText != null) == null)
            {
                goto Label_0089;
            }
            this.ConfirmText.set_text(str);
        Label_0089:
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
            string str;
            float num6;
            if ((this.gm == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = this.gm.Player.FreeCoin + this.gm.Player.ComCoin;
            num2 = this.gm.Player.PaidCoin;
            num3 = this.gm.Player.Coin;
            if ((this.FreeCoin != null) == null)
            {
                goto Label_0079;
            }
            this.FreeCoin.set_text(&num.ToString());
        Label_0079:
            if ((this.PaidCoin != null) == null)
            {
                goto Label_009C;
            }
            this.PaidCoin.set_text(&num2.ToString());
        Label_009C:
            if ((this.CurrentAmountCoin != null) == null)
            {
                goto Label_00BF;
            }
            this.CurrentAmountCoin.set_text(&num3.ToString());
        Label_00BF:
            if ((this.SelectTotalNum != null) == null)
            {
                goto Label_00F9;
            }
            this.SelectTotalNum.set_text("+" + &this.SelectSlider.get_value().ToString());
        Label_00F9:
            num4 = (int) this.SelectSlider.get_value();
            num5 = this.gm.MasterParam.FixParam.AbilityRankupPointCoinRate * num4;
            objArray1 = new object[] { (int) num5, (int) num4 };
            str = LocalizedText.Get("sys.CONFIRM_ABILITY_RANKUP_POINT_ADD", objArray1);
            if ((this.ConfirmText != null) == null)
            {
                goto Label_0167;
            }
            this.ConfirmText.set_text(str);
        Label_0167:
            return;
        }

        private void Start()
        {
            int num;
            int num2;
            int num3;
            if ((this.gm == null) == null)
            {
                goto Label_001C;
            }
            this.gm = MonoSingleton<GameManager>.GetInstanceDirect();
        Label_001C:
            if ((this.CancelButton != null) == null)
            {
                goto Label_0049;
            }
            this.CancelButton.get_onClick().AddListener(new UnityAction(this, this.Cancel));
        Label_0049:
            if ((this.DecideButton != null) == null)
            {
                goto Label_0076;
            }
            this.DecideButton.get_onClick().AddListener(new UnityAction(this, this.Decide));
        Label_0076:
            if ((this.PlusButton != null) == null)
            {
                goto Label_00A3;
            }
            this.PlusButton.get_onClick().AddListener(new UnityAction(this, this.OnAdd));
        Label_00A3:
            if ((this.MinusButton != null) == null)
            {
                goto Label_00D0;
            }
            this.MinusButton.get_onClick().AddListener(new UnityAction(this, this.OnRemove));
        Label_00D0:
            if ((this.SelectSlider != null) == null)
            {
                goto Label_0194;
            }
            num = this.gm.Player.AbilityRankUpCountNum;
            num2 = this.gm.MasterParam.FixParam.AbilityRankUpPointMax - num;
            num3 = Mathf.Min(this.gm.Player.Coin, Math.Min(this.gm.MasterParam.FixParam.AbilityRankUpPointAddMax, num2));
            this.SelectSlider.set_minValue(1f);
            this.SelectSlider.set_maxValue((float) num3);
            this.SelectSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnValueChanged));
            this.SelectSlider.set_value(this.SelectSlider.get_minValue());
        Label_0194:
            this.Refresh();
            return;
        }

        public delegate void CancelEvent();

        public delegate void DecideEvent(int value);
    }
}

