namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Close", 1, 1)]
    public class RaidSettingsWindow : MonoBehaviour, IFlowInterface
    {
        public RaidSettingsEvent OnAccept;
        public string DebugQuestID;
        public SRPG_Button AddButton;
        public SRPG_Button SubButton;
        public UnityEngine.UI.Slider Slider;
        public Text CountText;
        public Text APText;
        public Text CostText;
        public SRPG_Button OKButton;
        public GameObject Ticket;
        private QuestParam mQuest;
        private int mCount;
        private int mCountMax;
        private int mLimit;
        private bool mStarted;
        private int mLastTicketCount;
        private bool mInsideRefresh;

        public RaidSettingsWindow()
        {
            this.mLastTicketCount = -1;
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        public void Close()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            return;
        }

        private unsafe void CountChanged()
        {
            PlayerData data;
            bool flag;
            int num;
            int num2;
            Selectable selectable;
            Selectable selectable2;
            int num3;
            if ((this.AddButton != null) == null)
            {
                goto Label_002A;
            }
            this.AddButton.set_interactable(this.mCount < this.mCountMax);
        Label_002A:
            if ((this.SubButton != null) == null)
            {
                goto Label_004F;
            }
            this.SubButton.set_interactable(this.mCount > 1);
        Label_004F:
            data = MonoSingleton<GameManager>.Instance.Player;
            flag = 1;
            if ((this.APText != null) == null)
            {
                goto Label_0087;
            }
            this.APText.set_text(&data.Stamina.ToString());
        Label_0087:
            if (this.mQuest == null)
            {
                goto Label_0102;
            }
            num2 = this.mQuest.RequiredApWithPlayerLv(data.Lv, 1) * this.mCount;
            flag &= (data.Stamina < num2) == 0;
            if ((this.CostText != null) == null)
            {
                goto Label_0102;
            }
            this.CostText.set_text(&num2.ToString());
            selectable = this.CostText.GetComponent<Selectable>();
            if ((selectable != null) == null)
            {
                goto Label_0102;
            }
            selectable.set_interactable(flag);
        Label_0102:
            if ((this.CountText != null) == null)
            {
                goto Label_014B;
            }
            this.CountText.set_text(&this.mCount.ToString());
            selectable2 = this.CountText.GetComponent<Selectable>();
            if ((selectable2 != null) == null)
            {
                goto Label_014B;
            }
            selectable2.set_interactable(flag);
        Label_014B:
            return;
        }

        private int GetTicketNum()
        {
            if (this.mQuest == null)
            {
                goto Label_003B;
            }
            if (string.IsNullOrEmpty(this.mQuest.ticket) != null)
            {
                goto Label_003B;
            }
            return MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mQuest.ticket);
        Label_003B:
            return -1;
        }

        private void OnAddClick()
        {
            if (this.mCount >= this.mCountMax)
            {
                goto Label_0042;
            }
            this.mCount += 1;
            if ((this.Slider != null) == null)
            {
                goto Label_0042;
            }
            this.Slider.set_value((float) this.mCount);
        Label_0042:
            return;
        }

        private void OnOKClick(SRPG_Button button)
        {
            if (this.OnAccept == null)
            {
                goto Label_0017;
            }
            this.OnAccept(this);
        Label_0017:
            return;
        }

        private void OnPlayerStaminaChange()
        {
            GameManager local1;
            if ((this == null) == null)
            {
                goto Label_0033;
            }
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnStaminaChange = (GameManager.StaminaChangeEvent) Delegate.Remove(local1.OnStaminaChange, new GameManager.StaminaChangeEvent(this.OnPlayerStaminaChange));
            return;
        Label_0033:
            this.CountChanged();
            return;
        }

        private void OnSliderChange(float value)
        {
            if (this.mInsideRefresh != null)
            {
                goto Label_0029;
            }
            this.mCount = Mathf.Clamp(Mathf.FloorToInt(value), 1, this.mCountMax);
            this.CountChanged();
        Label_0029:
            return;
        }

        private void OnSubClick()
        {
            if (this.mCount <= 1)
            {
                goto Label_003D;
            }
            this.mCount -= 1;
            if ((this.Slider != null) == null)
            {
                goto Label_003D;
            }
            this.Slider.set_value((float) this.mCount);
        Label_003D:
            return;
        }

        public void Refresh()
        {
            ItemParam param;
            ItemData data;
            int num;
            if ((this.mQuest != null) && (string.IsNullOrEmpty(this.mQuest.ticket) == null))
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            this.mInsideRefresh = 1;
            param = (string.IsNullOrEmpty(this.mQuest.ticket) != null) ? null : MonoSingleton<GameManager>.Instance.GetItemParam(this.mQuest.ticket);
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(param);
            num = (data == null) ? 0 : data.Num;
            this.mLastTicketCount = num;
            this.mCountMax = Mathf.Min(num, this.mLimit);
            if (this.mQuest.GetChallangeLimit() <= 0)
            {
                goto Label_00CF;
            }
            this.mCountMax = Mathf.Min(this.mCountMax, this.mQuest.GetChallangeLimit() - this.mQuest.GetChallangeCount());
        Label_00CF:
            this.mCount = Mathf.Min(this.mCount, this.mCountMax);
            if ((this.Ticket != null) == null)
            {
                goto Label_010F;
            }
            DataSource.Bind<ItemData>(this.Ticket, data);
            DataSource.Bind<ItemParam>(this.Ticket, param);
        Label_010F:
            if ((this.Slider != null) == null)
            {
                goto Label_015F;
            }
            this.Slider.set_maxValue((float) this.mCountMax);
            if (Mathf.FloorToInt(this.Slider.get_value()) == this.mCount)
            {
                goto Label_015F;
            }
            this.Slider.set_value((float) this.mCount);
        Label_015F:
            this.CountChanged();
            GameParameter.UpdateAll(base.get_gameObject());
            this.mInsideRefresh = 0;
            return;
        }

        public void Setup(QuestParam quest, int count, int max)
        {
            this.mQuest = quest;
            this.mLimit = max;
            if (count < 0)
            {
                goto Label_0022;
            }
            this.mCount = Mathf.Max(1, count);
        Label_0022:
            if (this.mStarted == null)
            {
                goto Label_0033;
            }
            this.Refresh();
        Label_0033:
            return;
        }

        private void Start()
        {
            GameManager local1;
            if ((this.AddButton != null) == null)
            {
                goto Label_002D;
            }
            this.AddButton.get_onClick().AddListener(new UnityAction(this, this.OnAddClick));
        Label_002D:
            if ((this.SubButton != null) == null)
            {
                goto Label_005A;
            }
            this.SubButton.get_onClick().AddListener(new UnityAction(this, this.OnSubClick));
        Label_005A:
            if ((this.Slider != null) == null)
            {
                goto Label_0097;
            }
            this.Slider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnSliderChange));
            this.Slider.set_minValue(1f);
        Label_0097:
            if ((this.OKButton != null) == null)
            {
                goto Label_00BF;
            }
            this.OKButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnOKClick));
        Label_00BF:
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnStaminaChange = (GameManager.StaminaChangeEvent) Delegate.Combine(local1.OnStaminaChange, new GameManager.StaminaChangeEvent(this.OnPlayerStaminaChange));
            this.mStarted = 1;
            if (this.mQuest == null)
            {
                goto Label_00FD;
            }
            this.Refresh();
        Label_00FD:
            return;
        }

        private void Update()
        {
            int num;
            num = this.GetTicketNum();
            if (this.mLastTicketCount == num)
            {
                goto Label_0020;
            }
            this.mLastTicketCount = num;
            this.Refresh();
        Label_0020:
            return;
        }

        public int Count
        {
            get
            {
                return this.mCount;
            }
        }

        public delegate void RaidSettingsEvent(RaidSettingsWindow settings);
    }
}

