namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1), Pin(100, "決定", 1, 10), Pin(0x65, "キャンセル", 1, 11)]
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
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Refresh();
        Label_000D:
            return;
        }

        private void Awake()
        {
        }

        private void OnAddNum()
        {
            SellItem item;
            item = GlobalVars.SelectSellItem;
            if (item != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (item.num >= item.item.Num)
            {
                goto Label_0031;
            }
            item.num += 1;
        Label_0031:
            if ((this.SellNumSlider != null) == null)
            {
                goto Label_0054;
            }
            this.SellNumSlider.set_value((float) item.num);
        Label_0054:
            return;
        }

        private void OnCancel()
        {
            GlobalVars.SelectSellItem.num = this.mSaveSellNum;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        private void OnDecide()
        {
            SellItem item;
            item = GlobalVars.SelectSellItem;
            if (item.num <= 0)
            {
                goto Label_0032;
            }
            if (GlobalVars.SellItemList.Contains(item) != null)
            {
                goto Label_004C;
            }
            GlobalVars.SellItemList.Add(item);
            goto Label_004C;
        Label_0032:
            item.index = -1;
            item.num = 0;
            GlobalVars.SellItemList.Remove(item);
        Label_004C:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void OnRemoveNum()
        {
            SellItem item;
            item = GlobalVars.SelectSellItem;
            if (item != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (item.num <= 0)
            {
                goto Label_0027;
            }
            item.num -= 1;
        Label_0027:
            if ((this.SellNumSlider != null) == null)
            {
                goto Label_004A;
            }
            this.SellNumSlider.set_value((float) item.num);
        Label_004A:
            return;
        }

        private void OnSellNumChanged(float value)
        {
            SellItem item;
            item = GlobalVars.SelectSellItem;
            if (item != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            item.num = (int) value;
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Refresh()
        {
            SellItem item;
            item = GlobalVars.SelectSellItem;
            if (item != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((this.SellNumSlider != null) == null)
            {
                goto Label_0073;
            }
            this.SellNumSlider.get_onValueChanged().RemoveAllListeners();
            this.SellNumSlider.set_maxValue((float) item.item.Num);
            this.SellNumSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnSellNumChanged));
            this.SellNumSlider.set_value((float) item.num);
        Label_0073:
            this.mSaveSellNum = item.num;
            DataSource.Bind<SellItem>(base.get_gameObject(), item);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
            if ((this.TxtTitle != null) == null)
            {
                goto Label_0026;
            }
            this.TxtTitle.set_text(LocalizedText.Get("sys.SHOP_SELL_SELECTNUM_TITLE"));
        Label_0026:
            if ((this.TxtSellItemPriceStr != null) == null)
            {
                goto Label_004C;
            }
            this.TxtSellItemPriceStr.set_text(LocalizedText.Get("sys.SELL_PRICE"));
        Label_004C:
            if ((this.TxtSellNumStr != null) == null)
            {
                goto Label_0072;
            }
            this.TxtSellNumStr.set_text(LocalizedText.Get("sys.SELL_NUM"));
        Label_0072:
            if ((this.TxtSellTotalPriceStr != null) == null)
            {
                goto Label_0098;
            }
            this.TxtSellTotalPriceStr.set_text(LocalizedText.Get("sys.SELL_PRICE"));
        Label_0098:
            if ((this.TxtDecide != null) == null)
            {
                goto Label_00BE;
            }
            this.TxtDecide.set_text(LocalizedText.Get("sys.CMD_DECIDE"));
        Label_00BE:
            if ((this.BtnDecide != null) == null)
            {
                goto Label_00EB;
            }
            this.BtnDecide.get_onClick().AddListener(new UnityAction(this, this.OnDecide));
        Label_00EB:
            if ((this.BtnCancel != null) == null)
            {
                goto Label_0118;
            }
            this.BtnCancel.get_onClick().AddListener(new UnityAction(this, this.OnCancel));
        Label_0118:
            if ((this.BtnPlus != null) == null)
            {
                goto Label_0145;
            }
            this.BtnPlus.get_onClick().AddListener(new UnityAction(this, this.OnAddNum));
        Label_0145:
            if ((this.BtnMinus != null) == null)
            {
                goto Label_0172;
            }
            this.BtnMinus.get_onClick().AddListener(new UnityAction(this, this.OnRemoveNum));
        Label_0172:
            this.Refresh();
            return;
        }
    }
}

