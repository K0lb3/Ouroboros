namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(11, "Slider Minus", 0, 11), Pin(10, "Slider Plus", 0, 10), Pin(1, "Refresh", 0, 1)]
    public class EventShopBuyConfirmWindow : MonoBehaviour, IFlowInterface
    {
        private const int PINID_REFRESH = 1;
        private const int PINID_SLIDER_PLUS = 10;
        private const int PINID_SLIDER_MINUS = 11;
        public GameObject limited_item;
        public GameObject no_limited_item;
        public Text SoldNum;
        public Text TextDesc;
        [HeaderBar("▼アイコン表示用オブジェクト")]
        public GameObject ItemIconRoot;
        public GameObject ConceptCardIconRoot;
        [HeaderBar("▼右側の表示領域")]
        public GameObject LayoutRight;
        public GameObject EnableEquipUnitWindow;
        public RectTransform UnitLayoutParent;
        public GameObject UnitTemplate;
        public GameObject ConceptCardDetail;
        [HeaderBar("▼まとめ買い用")]
        public GameObject AmountSliderHolder;
        public Slider AmountSlider;
        public Text AmountSliderNum;
        public Button IncrementButton;
        public Button DecrementButton;
        public Text LimitedItemPriceText;
        [HeaderBar("▼所持 幻晶石/ゼニー等")]
        public GameObject HasJem;
        public GameObject HasCoin;
        public GameObject HasZenny;
        private List<GameObject> mUnits;
        private bool mEnabledSlider;
        private EventShopItem mShopitem;
        [CompilerGenerated]
        private static Func<EventShopItem, bool> <>f__am$cache17;

        public EventShopBuyConfirmWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Refresh>m__307(EventShopItem item)
        {
            return (item.id == GlobalVars.ShopBuyIndex);
        }

        [CompilerGenerated]
        private bool <Refresh>m__308(EventCoinData f)
        {
            return f.iname.Equals(this.mShopitem.cost_iname);
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 10)
            {
                goto Label_0029;
            }
            if (num == 11)
            {
                goto Label_0034;
            }
            if (num == 1)
            {
                goto Label_001E;
            }
            goto Label_003F;
        Label_001E:
            this.Refresh();
            goto Label_003F;
        Label_0029:
            this.IncrementSliderValue();
            goto Label_003F;
        Label_0034:
            this.DecrementSliderValue();
        Label_003F:
            return;
        }

        private void Awake()
        {
        }

        private void DecrementSliderValue()
        {
            this.SetSliderValue(this.AmountSlider.get_value() - 1f);
            return;
        }

        private void IncrementSliderValue()
        {
            this.SetSliderValue(this.AmountSlider.get_value() + 1f);
            return;
        }

        private void OnSliderValueChanged(float newValue)
        {
            this.SetSliderValue(newValue);
            return;
        }

        private unsafe void Refresh()
        {
            List<UnitData> list;
            int num;
            EventShopData data;
            GameParameter parameter;
            int num2;
            int num3;
            int num4;
            List<EventCoinData> list2;
            EventCoinData data2;
            int num5;
            int num6;
            UnitData data3;
            bool flag;
            int num7;
            JobData data4;
            int num8;
            GameObject obj2;
            GameObject obj3;
            ArtifactParam param;
            ConceptCardData data5;
            ItemData data6;
            ItemParam param2;
            int num9;
            ESaleType type;
            if ((this.UnitTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            list = MonoSingleton<GameManager>.Instance.Player.Units;
            num = 0;
            goto Label_0044;
        Label_0029:
            this.mUnits[num].get_gameObject().SetActive(0);
            num += 1;
        Label_0044:
            if (num < this.mUnits.Count)
            {
                goto Label_0029;
            }
            data = MonoSingleton<GameManager>.Instance.Player.GetEventShopData();
            if (<>f__am$cache17 != null)
            {
                goto Label_0084;
            }
            <>f__am$cache17 = new Func<EventShopItem, bool>(EventShopBuyConfirmWindow.<Refresh>m__307);
        Label_0084:
            this.mShopitem = Enumerable.FirstOrDefault<EventShopItem>(data.items, <>f__am$cache17);
            if ((this.limited_item != null) == null)
            {
                goto Label_00BD;
            }
            this.limited_item.SetActive(this.mShopitem.IsNotLimited == 0);
        Label_00BD:
            if ((this.no_limited_item != null) == null)
            {
                goto Label_00E4;
            }
            this.no_limited_item.SetActive(this.mShopitem.IsNotLimited);
        Label_00E4:
            if ((this.SoldNum != null) == null)
            {
                goto Label_0114;
            }
            this.SoldNum.set_text(&this.mShopitem.remaining_num.ToString());
        Label_0114:
            this.mEnabledSlider = 0;
            if ((this.AmountSliderHolder != null) == null)
            {
                goto Label_025A;
            }
            if ((this.AmountSlider != null) == null)
            {
                goto Label_025A;
            }
            if ((this.AmountSliderNum != null) == null)
            {
                goto Label_025A;
            }
            if (this.mShopitem.IsNotLimited != null)
            {
                goto Label_0247;
            }
            if (this.mShopitem.remaining_num <= 1)
            {
                goto Label_0247;
            }
            this.mEnabledSlider = 1;
            parameter = this.LimitedItemPriceText.GetComponent<GameParameter>();
            if ((parameter != null) == null)
            {
                goto Label_0195;
            }
            parameter.set_enabled(0);
        Label_0195:
            this.AmountSliderHolder.SetActive(1);
            num2 = ShopData.GetRemainingCurrency(this.mShopitem);
            num3 = ShopData.GetBuyPrice(this.mShopitem);
            num4 = 1;
            if (num3 <= 0)
            {
                goto Label_01DD;
            }
            goto Label_01D1;
        Label_01CB:
            num4 += 1;
        Label_01D1:
            if ((num3 * num4) <= num2)
            {
                goto Label_01CB;
            }
        Label_01DD:
            num4 = Math.Max(Math.Min(num4 - 1, this.mShopitem.remaining_num), 1);
            this.AmountSlider.set_minValue(1f);
            this.AmountSlider.set_maxValue((float) num4);
            this.SetSliderValue(1f);
            this.AmountSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnSliderValueChanged));
            goto Label_025A;
        Label_0247:
            this.mEnabledSlider = 0;
            this.AmountSliderHolder.SetActive(0);
        Label_025A:
            if ((this.HasJem != null) == null)
            {
                goto Label_0364;
            }
            if ((this.HasCoin != null) == null)
            {
                goto Label_0364;
            }
            if ((this.HasZenny != null) == null)
            {
                goto Label_0364;
            }
            type = this.mShopitem.saleType;
            if (type == null)
            {
                goto Label_02DF;
            }
            if (type == 1)
            {
                goto Label_02B6;
            }
            if (type == 7)
            {
                goto Label_02B6;
            }
            goto Label_0308;
        Label_02B6:
            this.HasJem.SetActive(1);
            this.HasCoin.SetActive(0);
            this.HasZenny.SetActive(0);
            goto Label_0364;
        Label_02DF:
            this.HasJem.SetActive(0);
            this.HasCoin.SetActive(0);
            this.HasZenny.SetActive(1);
            goto Label_0364;
        Label_0308:
            this.HasJem.SetActive(0);
            this.HasCoin.SetActive(1);
            this.HasZenny.SetActive(0);
            data2 = MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find(new Predicate<EventCoinData>(this.<Refresh>m__308));
            DataSource.Bind<EventCoinData>(this.HasCoin, data2);
        Label_0364:
            if ((this.EnableEquipUnitWindow != null) == null)
            {
                goto Label_04CE;
            }
            this.EnableEquipUnitWindow.get_gameObject().SetActive(0);
            num5 = 0;
            num6 = 0;
            goto Label_04C1;
        Label_0391:
            data3 = list[num6];
            flag = 0;
            num7 = 0;
            goto Label_0400;
        Label_03A6:
            data4 = data3.Jobs[num7];
            if (data4.IsActivated != null)
            {
                goto Label_03C3;
            }
            goto Label_03FA;
        Label_03C3:
            num8 = data4.FindEquipSlotByItemID(this.mShopitem.iname);
            if (num8 != -1)
            {
                goto Label_03E4;
            }
            goto Label_03FA;
        Label_03E4:
            if (data4.CheckEnableEquipSlot(num8) == null)
            {
                goto Label_03FA;
            }
            flag = 1;
            goto Label_0410;
        Label_03FA:
            num7 += 1;
        Label_0400:
            if (num7 < ((int) data3.Jobs.Length))
            {
                goto Label_03A6;
            }
        Label_0410:
            if (flag != null)
            {
                goto Label_041C;
            }
            goto Label_04BB;
        Label_041C:
            if (num5 < this.mUnits.Count)
            {
                goto Label_0473;
            }
            this.UnitTemplate.SetActive(1);
            obj2 = Object.Instantiate<GameObject>(this.UnitTemplate);
            obj2.get_transform().SetParent(this.UnitLayoutParent, 0);
            this.mUnits.Add(obj2);
            this.UnitTemplate.SetActive(0);
        Label_0473:
            obj3 = this.mUnits[num5].get_gameObject();
            DataSource.Bind<UnitData>(obj3, data3);
            obj3.SetActive(1);
            this.EnableEquipUnitWindow.get_gameObject().SetActive(1);
            GameUtility.SetGameObjectActive(this.LayoutRight, 1);
            num5 += 1;
        Label_04BB:
            num6 += 1;
        Label_04C1:
            if (num6 < list.Count)
            {
                goto Label_0391;
            }
        Label_04CE:
            DataSource.Bind<EventShopItem>(base.get_gameObject(), this.mShopitem);
            if (this.mShopitem.IsArtifact == null)
            {
                goto Label_051D;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.mShopitem.iname);
            DataSource.Bind<ArtifactParam>(base.get_gameObject(), param);
            goto Label_0664;
        Label_051D:
            if (this.mShopitem.IsConceptCard == null)
            {
                goto Label_05B9;
            }
            GameUtility.SetGameObjectActive(this.ItemIconRoot, 0);
            GameUtility.SetGameObjectActive(this.ConceptCardIconRoot, 1);
            if ((this.ConceptCardDetail != null) == null)
            {
                goto Label_0664;
            }
            data5 = ConceptCardData.CreateConceptCardDataForDisplay(this.mShopitem.iname);
            GlobalVars.SelectedConceptCardData.Set(data5);
            GameUtility.SetGameObjectActive(this.ConceptCardDetail, 1);
            GameUtility.SetGameObjectActive(this.LayoutRight, 1);
            if ((this.TextDesc != null) == null)
            {
                goto Label_0664;
            }
            this.TextDesc.set_text(data5.Param.expr);
            goto Label_0664;
        Label_05B9:
            data6 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mShopitem.iname);
            if (data6 == null)
            {
                goto Label_0616;
            }
            DataSource.Bind<ItemData>(base.get_gameObject(), data6);
            if ((this.TextDesc != null) == null)
            {
                goto Label_0664;
            }
            this.TextDesc.set_text(data6.Param.Expr);
            goto Label_0664;
        Label_0616:
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(this.mShopitem.iname);
            if (param2 == null)
            {
                goto Label_0664;
            }
            if ((this.TextDesc != null) == null)
            {
                goto Label_0664;
            }
            DataSource.Bind<ItemParam>(base.get_gameObject(), param2);
            this.TextDesc.set_text(param2.Expr);
        Label_0664:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private unsafe void SetSliderValue(float newValue)
        {
            int num;
            int num2;
            int num3;
            this.AmountSlider.set_value(newValue);
            num3 = (int) this.AmountSlider.get_value();
            this.AmountSliderNum.set_text(&num3.ToString());
            if (this.AmountSlider.get_value() > this.AmountSlider.get_minValue())
            {
                goto Label_0057;
            }
            this.DecrementButton.set_interactable(0);
            goto Label_0063;
        Label_0057:
            this.DecrementButton.set_interactable(1);
        Label_0063:
            if (this.AmountSlider.get_value() < this.AmountSlider.get_maxValue())
            {
                goto Label_008F;
            }
            this.IncrementButton.set_interactable(0);
            goto Label_009B;
        Label_008F:
            this.IncrementButton.set_interactable(1);
        Label_009B:
            if (this.AmountSlider.get_maxValue() != 1f)
            {
                goto Label_00D6;
            }
            if (this.AmountSlider.get_minValue() != 1f)
            {
                goto Label_00D6;
            }
            this.AmountSlider.set_interactable(0);
            goto Label_00E2;
        Label_00D6:
            this.AmountSlider.set_interactable(1);
        Label_00E2:
            num2 = ShopData.GetBuyPrice(this.mShopitem) * ((int) this.AmountSlider.get_value());
            this.LimitedItemPriceText.set_text(&num2.ToString());
            return;
        }

        private void Start()
        {
            if ((this.UnitTemplate != null) == null)
            {
                goto Label_002D;
            }
            if (this.UnitTemplate.get_activeInHierarchy() == null)
            {
                goto Label_002D;
            }
            this.UnitTemplate.SetActive(0);
        Label_002D:
            this.mUnits = new List<GameObject>(MonoSingleton<GameManager>.Instance.Player.Units.Count);
            this.Refresh();
            return;
        }

        public void UpdateBuyAmount()
        {
            if (this.mEnabledSlider == null)
            {
                goto Label_0021;
            }
            GlobalVars.ShopBuyAmount = (int) this.AmountSlider.get_value();
            goto Label_0027;
        Label_0021:
            GlobalVars.ShopBuyAmount = 1;
        Label_0027:
            return;
        }
    }
}

