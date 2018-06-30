namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "Request", 1, 0)]
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
            base..ctor();
            return;
        }

        public void Down()
        {
            this.UseItemNum -= 1;
            this.UseItemNum = Mathf.Max(this.UseItemNum, 1);
            this.Refresh();
            return;
        }

        public int GetMaxNum()
        {
            GameManager manager;
            int num;
            int num2;
            manager = MonoSingleton<GameManager>.Instance;
            num = manager.Player.StaminaStockCap - manager.Player.Stamina;
            if (this.mData.Param.value != null)
            {
                goto Label_0035;
            }
            return 1;
        Label_0035:
            num2 = (num - 1) / this.mData.Param.value;
            num2 += 1;
            return Mathf.Min(num2, this.mData.Num);
        }

        private void OnEnable()
        {
            ItemData data;
            this.slider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnValueChanged));
            data = DataSource.FindDataOfClass<ItemData>(this.HealApBase, null);
            this.mData = data;
            this.slider.set_maxValue((float) this.GetMaxNum());
            this.slider.set_minValue(1f);
            this.UseItemNum = 1;
            this.Refresh();
            return;
        }

        public void OnValueChanged(float value)
        {
            this.UseItemNum = Mathf.FloorToInt(value);
            this.UseItemNum = Mathf.Max(this.UseItemNum, 1);
            this.slider.set_value((float) this.UseItemNum);
            this.Refresh();
            return;
        }

        public unsafe void Refresh()
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            int num;
            int num2;
            int num3;
            this.slider.set_maxValue((float) this.GetMaxNum());
            this.slider.set_value((float) this.UseItemNum);
            this.Num.set_text(&this.UseItemNum.ToString());
            num3 = this.UseItemNum * this.mData.Param.value;
            this.HealApNum.set_text(&num3.ToString());
            objArray1 = new object[] { this.mData.Param.name };
            this.ItemName.set_text(LocalizedText.Get("sys.TEXT_APHEAL_ITEM_NUM", objArray1));
            objArray2 = new object[] { this.mData.Param.name };
            this.ItemCheckUse.set_text(LocalizedText.Get("sys.TEXT_APHEAL_CHECK_USE", objArray2));
            GameParameter.UpdateAll(base.get_gameObject());
            this.up_button.set_interactable(this.slider.get_maxValue() > this.slider.get_value());
            this.down_button.set_interactable(this.slider.get_minValue() < this.slider.get_value());
            if ((this.CurrentAP != null) == null)
            {
                goto Label_016F;
            }
            num = MonoSingleton<GameManager>.Instance.Player.Stamina;
            num2 = MonoSingleton<GameManager>.Instance.Player.StaminaStockCap;
            objArray3 = new object[] { (int) num, (int) num2 };
            this.CurrentAP.set_text(LocalizedText.Get("sys.TEXT_DENOMINATOR", objArray3));
        Label_016F:
            return;
        }

        public void Up()
        {
            this.UseItemNum += 1;
            this.UseItemNum = Mathf.Min(this.UseItemNum, this.GetMaxNum());
            this.Refresh();
            return;
        }

        public int HealNum
        {
            get
            {
                return (this.mData.Param.value * this.UseItemNum);
            }
        }

        public bool IsOverFlow
        {
            get
            {
                GameManager manager;
                manager = MonoSingleton<GameManager>.Instance;
                return ((this.HealNum + manager.Player.Stamina) > manager.Player.StaminaStockCap);
            }
        }
    }
}

