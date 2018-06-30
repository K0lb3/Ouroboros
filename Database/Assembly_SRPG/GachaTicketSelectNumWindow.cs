namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0x68, "所持数が0orアイテムデータが存在しない", 1, 0x68), Pin(0x65, "チケット枚数を決定", 1, 0x65), Pin(0x67, "チケットのinameが指定されていない", 1, 0x67), Pin(0x66, "キャンセル", 1, 0x66)]
    public class GachaTicketSelectNumWindow : MonoBehaviour, IFlowInterface
    {
        private const int DefaultMaxNum = 10;
        [SerializeField]
        private Text WindowTitle;
        [SerializeField]
        private BitmapText UsedNum;
        [SerializeField]
        private Slider TicketNumSlider;
        [SerializeField]
        private GameObject AmountTicket;
        [SerializeField]
        private Button BtnDecide;
        [SerializeField]
        private Button BtnCancel;
        [SerializeField]
        private Button BtnPlus;
        [SerializeField]
        private Button BtnMinus;
        [SerializeField]
        private Button BtnMax;
        private int mSaveUseNum;
        private int mMaxNum;
        private GachaManager gacham;

        public GachaTicketSelectNumWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void OnAddNum()
        {
            if ((this.TicketNumSlider != null) == null)
            {
                goto Label_0043;
            }
            if (this.TicketNumSlider.get_maxValue() <= this.TicketNumSlider.get_value())
            {
                goto Label_0043;
            }
            this.TicketNumSlider.set_value(this.TicketNumSlider.get_value() + 1f);
        Label_0043:
            return;
        }

        private void OnMaxNum()
        {
            if ((this.TicketNumSlider != null) == null)
            {
                goto Label_0027;
            }
            this.TicketNumSlider.set_value(this.TicketNumSlider.get_maxValue());
        Label_0027:
            return;
        }

        private void OnRemoveNum()
        {
            if ((this.TicketNumSlider != null) == null)
            {
                goto Label_0043;
            }
            if (this.TicketNumSlider.get_minValue() >= this.TicketNumSlider.get_value())
            {
                goto Label_0043;
            }
            this.TicketNumSlider.set_value(this.TicketNumSlider.get_value() - 1f);
        Label_0043:
            return;
        }

        private unsafe void OnUseNumChanged(float value)
        {
            int num;
            num = (int) value;
            this.UsedNum.text = &num.ToString();
            this.gacham.UseTicketNum = (int) value;
            if ((this.BtnPlus != null) == null)
            {
                goto Label_005F;
            }
            this.BtnPlus.set_interactable(((this.TicketNumSlider.get_value() + 1f) > this.TicketNumSlider.get_maxValue()) == 0);
        Label_005F:
            if ((this.BtnMinus != null) == null)
            {
                goto Label_009C;
            }
            this.BtnMinus.set_interactable(((this.TicketNumSlider.get_value() - 1f) < this.TicketNumSlider.get_minValue()) == 0);
        Label_009C:
            return;
        }

        public unsafe void Refresh(ItemData data)
        {
            object[] objArray1;
            int num;
            float num2;
            num = data.Num;
            this.mMaxNum = Mathf.Min(num, 10);
            if (string.IsNullOrEmpty(FlowNode_Variable.Get("USE_TICKET_MAX")) != null)
            {
                goto Label_0030;
            }
            this.mMaxNum = 1;
        Label_0030:
            FlowNode_Variable.Set("USE_TICKET_MAX", string.Empty);
            if ((this.WindowTitle != null) == null)
            {
                goto Label_0079;
            }
            objArray1 = new object[] { data.Param.name };
            this.WindowTitle.set_text(LocalizedText.Get("sys.GACHA_TICKET_SELECT_TITLE", objArray1));
        Label_0079:
            if ((this.AmountTicket != null) == null)
            {
                goto Label_00A1;
            }
            DataSource.Bind<ItemData>(this.AmountTicket, data);
            GameParameter.UpdateAll(this.AmountTicket);
        Label_00A1:
            if ((this.TicketNumSlider != null) == null)
            {
                goto Label_0116;
            }
            this.TicketNumSlider.get_onValueChanged().RemoveAllListeners();
            this.TicketNumSlider.set_minValue(1f);
            this.TicketNumSlider.set_maxValue((float) this.mMaxNum);
            this.TicketNumSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnUseNumChanged));
            this.TicketNumSlider.set_value(this.TicketNumSlider.get_minValue());
        Label_0116:
            if ((this.BtnPlus != null) == null)
            {
                goto Label_0153;
            }
            this.BtnPlus.set_interactable(((this.TicketNumSlider.get_value() + 1f) > this.TicketNumSlider.get_maxValue()) == 0);
        Label_0153:
            if ((this.BtnMinus != null) == null)
            {
                goto Label_0190;
            }
            this.BtnMinus.set_interactable(((this.TicketNumSlider.get_value() - 1f) < this.TicketNumSlider.get_minValue()) == 0);
        Label_0190:
            this.UsedNum.text = &this.TicketNumSlider.get_value().ToString();
            this.gacham.UseTicketNum = (int) this.TicketNumSlider.get_value();
            return;
        }

        private void Start()
        {
            string str;
            ItemData data;
            if ((this.BtnPlus != null) == null)
            {
                goto Label_002D;
            }
            this.BtnPlus.get_onClick().AddListener(new UnityAction(this, this.OnAddNum));
        Label_002D:
            if ((this.BtnMinus != null) == null)
            {
                goto Label_005A;
            }
            this.BtnMinus.get_onClick().AddListener(new UnityAction(this, this.OnRemoveNum));
        Label_005A:
            if ((this.BtnMax != null) == null)
            {
                goto Label_0087;
            }
            this.BtnMax.get_onClick().AddListener(new UnityAction(this, this.OnMaxNum));
        Label_0087:
            str = FlowNode_Variable.Get("USE_TICKET_INAME");
            FlowNode_Variable.Set("USE_TICKET_INAME", string.Empty);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_00BF;
            }
            DebugUtility.LogError("不正なアイテムが指定されました");
            FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
            return;
        Label_00BF:
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(str);
            if (data == null)
            {
                goto Label_00E2;
            }
            if (data.Num >= 0)
            {
                goto Label_00F5;
            }
        Label_00E2:
            DebugUtility.LogError("所持していないアイテムが指定されました");
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
            return;
        Label_00F5:
            if ((this.gacham == null) == null)
            {
                goto Label_0111;
            }
            this.gacham = MonoSingleton<GachaManager>.Instance;
        Label_0111:
            this.Refresh(data);
            return;
        }
    }
}

