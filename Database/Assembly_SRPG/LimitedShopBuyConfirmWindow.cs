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

    [Pin(10, "Slider Plus", 0, 10), Pin(1, "Refresh", 0, 1), Pin(11, "Slider Minus", 0, 11)]
    public class LimitedShopBuyConfirmWindow : MonoBehaviour, IFlowInterface
    {
        private const int PINID_REFRESH = 1;
        private const int PINID_SLIDER_PLUS = 10;
        private const int PINID_SLIDER_MINUS = 11;
        public GameObject limited_item;
        public GameObject no_limited_item;
        public GameObject Sold;
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
        private LimitedShopItem mShopitem;
        [CompilerGenerated]
        private static Func<LimitedShopItem, bool> <>f__am$cache18;

        public LimitedShopBuyConfirmWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Refresh>m__351(LimitedShopItem item)
        {
            return (item.id == GlobalVars.ShopBuyIndex);
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
            LimitedShopData data;
            bool flag;
            GameParameter parameter;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            UnitData data2;
            bool flag2;
            int num7;
            JobData data3;
            int num8;
            GameObject obj2;
            GameObject obj3;
            ArtifactParam param;
            ConceptCardData data4;
            ItemData data5;
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
            data = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
            if (<>f__am$cache18 != null)
            {
                goto Label_0084;
            }
            <>f__am$cache18 = new Func<LimitedShopItem, bool>(LimitedShopBuyConfirmWindow.<Refresh>m__351);
        Label_0084:
            this.mShopitem = Enumerable.FirstOrDefault<LimitedShopItem>(data.items, <>f__am$cache18);
            flag = (this.mShopitem.IsNotLimited == null) ? 1 : (this.mShopitem.saleType == 7);
            if ((this.limited_item != null) == null)
            {
                goto Label_00D2;
            }
            this.limited_item.SetActive(flag);
        Label_00D2:
            if ((this.no_limited_item != null) == null)
            {
                goto Label_00F2;
            }
            this.no_limited_item.SetActive(flag == 0);
        Label_00F2:
            if ((this.Sold != null) == null)
            {
                goto Label_011C;
            }
            this.Sold.SetActive(this.mShopitem.IsNotLimited == 0);
        Label_011C:
            if ((this.SoldNum != null) == null)
            {
                goto Label_014C;
            }
            this.SoldNum.set_text(&this.mShopitem.remaining_num.ToString());
        Label_014C:
            this.mEnabledSlider = 0;
            if ((this.AmountSliderHolder != null) == null)
            {
                goto Label_0295;
            }
            if ((this.AmountSlider != null) == null)
            {
                goto Label_0295;
            }
            if ((this.AmountSliderNum != null) == null)
            {
                goto Label_0295;
            }
            if (this.mShopitem.IsNotLimited != null)
            {
                goto Label_0282;
            }
            if (this.mShopitem.remaining_num <= 1)
            {
                goto Label_0282;
            }
            this.mEnabledSlider = 1;
            parameter = this.LimitedItemPriceText.GetComponent<GameParameter>();
            if ((parameter != null) == null)
            {
                goto Label_01D0;
            }
            parameter.set_enabled(0);
        Label_01D0:
            this.AmountSliderHolder.SetActive(1);
            num2 = ShopData.GetRemainingCurrency(this.mShopitem);
            num3 = ShopData.GetBuyPrice(this.mShopitem);
            num4 = 1;
            if (num3 <= 0)
            {
                goto Label_0218;
            }
            goto Label_020C;
        Label_0206:
            num4 += 1;
        Label_020C:
            if ((num3 * num4) <= num2)
            {
                goto Label_0206;
            }
        Label_0218:
            num4 = Math.Max(Math.Min(num4 - 1, this.mShopitem.remaining_num), 1);
            this.AmountSlider.set_minValue(1f);
            this.AmountSlider.set_maxValue((float) num4);
            this.SetSliderValue(1f);
            this.AmountSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnSliderValueChanged));
            goto Label_0295;
        Label_0282:
            this.mEnabledSlider = 0;
            this.AmountSliderHolder.SetActive(0);
        Label_0295:
            if ((this.HasJem != null) == null)
            {
                goto Label_036C;
            }
            if ((this.HasCoin != null) == null)
            {
                goto Label_036C;
            }
            if ((this.HasZenny != null) == null)
            {
                goto Label_036C;
            }
            type = this.mShopitem.saleType;
            if (type == null)
            {
                goto Label_031A;
            }
            if (type == 1)
            {
                goto Label_02F1;
            }
            if (type == 7)
            {
                goto Label_02F1;
            }
            goto Label_0343;
        Label_02F1:
            this.HasJem.SetActive(1);
            this.HasCoin.SetActive(0);
            this.HasZenny.SetActive(0);
            goto Label_036C;
        Label_031A:
            this.HasJem.SetActive(0);
            this.HasCoin.SetActive(0);
            this.HasZenny.SetActive(1);
            goto Label_036C;
        Label_0343:
            this.HasJem.SetActive(0);
            this.HasCoin.SetActive(1);
            this.HasZenny.SetActive(0);
        Label_036C:
            if ((this.EnableEquipUnitWindow != null) == null)
            {
                goto Label_04D6;
            }
            this.EnableEquipUnitWindow.get_gameObject().SetActive(0);
            num5 = 0;
            num6 = 0;
            goto Label_04C9;
        Label_0399:
            data2 = list[num6];
            flag2 = 0;
            num7 = 0;
            goto Label_0408;
        Label_03AE:
            data3 = data2.Jobs[num7];
            if (data3.IsActivated != null)
            {
                goto Label_03CB;
            }
            goto Label_0402;
        Label_03CB:
            num8 = data3.FindEquipSlotByItemID(this.mShopitem.iname);
            if (num8 != -1)
            {
                goto Label_03EC;
            }
            goto Label_0402;
        Label_03EC:
            if (data3.CheckEnableEquipSlot(num8) == null)
            {
                goto Label_0402;
            }
            flag2 = 1;
            goto Label_0418;
        Label_0402:
            num7 += 1;
        Label_0408:
            if (num7 < ((int) data2.Jobs.Length))
            {
                goto Label_03AE;
            }
        Label_0418:
            if (flag2 != null)
            {
                goto Label_0424;
            }
            goto Label_04C3;
        Label_0424:
            if (num5 < this.mUnits.Count)
            {
                goto Label_047B;
            }
            this.UnitTemplate.SetActive(1);
            obj2 = Object.Instantiate<GameObject>(this.UnitTemplate);
            obj2.get_transform().SetParent(this.UnitLayoutParent, 0);
            this.mUnits.Add(obj2);
            this.UnitTemplate.SetActive(0);
        Label_047B:
            obj3 = this.mUnits[num5].get_gameObject();
            DataSource.Bind<UnitData>(obj3, data2);
            obj3.SetActive(1);
            this.EnableEquipUnitWindow.get_gameObject().SetActive(1);
            GameUtility.SetGameObjectActive(this.LayoutRight, 1);
            num5 += 1;
        Label_04C3:
            num6 += 1;
        Label_04C9:
            if (num6 < list.Count)
            {
                goto Label_0399;
            }
        Label_04D6:
            DataSource.Bind<LimitedShopItem>(base.get_gameObject(), this.mShopitem);
            if (this.mShopitem.IsArtifact == null)
            {
                goto Label_0525;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.mShopitem.iname);
            DataSource.Bind<ArtifactParam>(base.get_gameObject(), param);
            goto Label_066C;
        Label_0525:
            if (this.mShopitem.IsConceptCard == null)
            {
                goto Label_05C1;
            }
            GameUtility.SetGameObjectActive(this.ItemIconRoot, 0);
            GameUtility.SetGameObjectActive(this.ConceptCardIconRoot, 1);
            if ((this.ConceptCardDetail != null) == null)
            {
                goto Label_066C;
            }
            data4 = ConceptCardData.CreateConceptCardDataForDisplay(this.mShopitem.iname);
            GlobalVars.SelectedConceptCardData.Set(data4);
            GameUtility.SetGameObjectActive(this.ConceptCardDetail, 1);
            GameUtility.SetGameObjectActive(this.LayoutRight, 1);
            if ((this.TextDesc != null) == null)
            {
                goto Label_066C;
            }
            this.TextDesc.set_text(data4.Param.expr);
            goto Label_066C;
        Label_05C1:
            data5 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mShopitem.iname);
            if (data5 == null)
            {
                goto Label_061E;
            }
            DataSource.Bind<ItemData>(base.get_gameObject(), data5);
            if ((this.TextDesc != null) == null)
            {
                goto Label_066C;
            }
            this.TextDesc.set_text(data5.Param.Expr);
            goto Label_066C;
        Label_061E:
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(this.mShopitem.iname);
            if (param2 == null)
            {
                goto Label_066C;
            }
            if ((this.TextDesc != null) == null)
            {
                goto Label_066C;
            }
            DataSource.Bind<ItemParam>(base.get_gameObject(), param2);
            this.TextDesc.set_text(param2.Expr);
        Label_066C:
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

