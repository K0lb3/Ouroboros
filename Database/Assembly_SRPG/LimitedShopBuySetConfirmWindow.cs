namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(10, "Slider Plus", 0, 10), Pin(1, "Refresh", 0, 1), Pin(11, "Slider Minus", 0, 11), Pin(100, "武具詳細情報セット(in)", 0, 100), Pin(0x65, "武具詳細情報セット(out)", 1, 0x65)]
    public class LimitedShopBuySetConfirmWindow : MonoBehaviour, IFlowInterface
    {
        private const int PINID_REFRESH = 1;
        private const int PINID_SLIDER_PLUS = 10;
        private const int PINID_SLIDER_MINUS = 11;
        private const int PINID_ARTIFACT_DETAIL_SET_INPUT = 100;
        private const int PINID_ARTIFACT_DETAIL_SET_OUTPUT = 0x65;
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public GameObject ItemTemplate;
        public GameObject ItemParent;
        public GameObject ItemWindow;
        public GameObject ArtifactWindow;
        private List<LimitedShopSetItemListElement> limited_shop_item_set_list;
        public StatusList ArtifactStatus;
        private ArtifactParam mArtifactParam;
        private bool mIsShowArtifactJob;
        public GameObject ArtifactAbility;
        public Animator ArtifactAbilityAnimation;
        public string AbilityListItemState;
        public int AbilityListItem_Hidden;
        public int AbilityListItem_Unlocked;
        public Text AmountNum;
        public GameObject Sold;
        [Space(20f)]
        public GameObject ItemAmountSliderHolder;
        public Slider ItemAmountSlider;
        public Text ItemAmountSliderNum;
        public Button ItemIncrementButton;
        public Button ItemDecrementButton;
        [Space(20f)]
        public GameObject ArtifactAmountSliderHolder;
        public Slider ArtifactAmountSlider;
        public Text ArtifactAmountSliderNum;
        public Button ArtifactIncrementButton;
        public Button ArtifactDecrementButton;
        [Space(20f)]
        public Text LimitedItemPriceText;
        [HeaderBar("▼セット効果確認用のボタン"), SerializeField]
        private Button m_SetEffectsButton;
        private GameObject AmountSliderHolder;
        private Slider AmountSlider;
        private Text AmountSliderNum;
        private Button IncrementButton;
        private Button DecrementButton;
        private bool mEnabledSlider;
        private LimitedShopItem mShopitem;
        [CompilerGenerated]
        private static Func<LimitedShopItem, bool> <>f__am$cache22;

        public LimitedShopBuySetConfirmWindow()
        {
            this.limited_shop_item_set_list = new List<LimitedShopSetItemListElement>();
            this.AbilityListItem_Unlocked = 2;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Refresh>m__352(LimitedShopItem item)
        {
            return (item.id == GlobalVars.ShopBuyIndex);
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 10)
            {
                goto Label_0031;
            }
            if (num == 11)
            {
                goto Label_003C;
            }
            if (num == 1)
            {
                goto Label_0026;
            }
            if (num == 100)
            {
                goto Label_0047;
            }
            goto Label_005A;
        Label_0026:
            this.Refresh();
            goto Label_005A;
        Label_0031:
            this.IncrementSliderValue();
            goto Label_005A;
        Label_003C:
            this.DecrementSliderValue();
            goto Label_005A;
        Label_0047:
            this.SetArtifactDetailData();
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
        Label_005A:
            return;
        }

        private void Awake()
        {
        }

        public void CloseJobList()
        {
            this.mIsShowArtifactJob = 0;
            return;
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
            LimitedShopData data;
            ArtifactParam param;
            ArtifactData data2;
            Json_Artifact artifact;
            BaseStatus status;
            BaseStatus status2;
            AbilityParam param2;
            List<AbilityData> list;
            bool flag;
            int num;
            AbilityData data3;
            ItemData data4;
            int num2;
            GameObject obj2;
            Vector3 vector;
            LimitedShopSetItemListElement element;
            StringBuilder builder;
            ArtifactParam param3;
            ConceptCardData data5;
            ItemData data6;
            GameParameter parameter;
            int num3;
            int num4;
            int num5;
            int num6;
            data = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
            if (<>f__am$cache22 != null)
            {
                goto Label_002F;
            }
            <>f__am$cache22 = new Func<LimitedShopItem, bool>(LimitedShopBuySetConfirmWindow.<Refresh>m__352);
        Label_002F:
            this.mShopitem = Enumerable.FirstOrDefault<LimitedShopItem>(data.items, <>f__am$cache22);
            this.ItemWindow.SetActive(this.mShopitem.IsArtifact == 0);
            this.ArtifactWindow.SetActive(this.mShopitem.IsArtifact);
            if ((this.AmountNum != null) == null)
            {
                goto Label_009D;
            }
            this.AmountNum.set_text(&this.mShopitem.remaining_num.ToString());
        Label_009D:
            if ((this.Sold != null) == null)
            {
                goto Label_00C7;
            }
            this.Sold.SetActive(this.mShopitem.IsNotLimited == 0);
        Label_00C7:
            if (this.mShopitem.IsArtifact == null)
            {
                goto Label_02BD;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.mShopitem.iname);
            DataSource.Bind<ArtifactParam>(base.get_gameObject(), param);
            this.mArtifactParam = param;
            data2 = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iname = param.iname;
            artifact.rare = param.rareini;
            data2.Deserialize(artifact);
            status = new BaseStatus();
            status2 = new BaseStatus();
            data2.GetHomePassiveBuffStatus(&status, &status2, null, 0, 1);
            this.ArtifactStatus.SetValues(status, status2, 0);
            if (param.abil_inames == null)
            {
                goto Label_0243;
            }
            if (((int) param.abil_inames.Length) <= 0)
            {
                goto Label_0243;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(param.abil_inames[0]);
            list = data2.LearningAbilities;
            flag = 0;
            if (list == null)
            {
                goto Label_01F7;
            }
            num = 0;
            goto Label_01E9;
        Label_01A7:
            data3 = list[num];
            if (data3 != null)
            {
                goto Label_01BE;
            }
            goto Label_01E3;
        Label_01BE:
            if ((param2.iname == data3.Param.iname) == null)
            {
                goto Label_01E3;
            }
            flag = 1;
            goto Label_01F7;
        Label_01E3:
            num += 1;
        Label_01E9:
            if (num < list.Count)
            {
                goto Label_01A7;
            }
        Label_01F7:
            DataSource.Bind<AbilityParam>(this.ArtifactAbility, param2);
            if (flag == null)
            {
                goto Label_0227;
            }
            this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Unlocked);
            goto Label_023E;
        Label_0227:
            this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
        Label_023E:
            goto Label_025A;
        Label_0243:
            this.ArtifactAbilityAnimation.SetInteger(this.AbilityListItemState, this.AbilityListItem_Hidden);
        Label_025A:
            if ((this.m_SetEffectsButton != null) == null)
            {
                goto Label_054E;
            }
            if ((this.m_SetEffectsButton != null) == null)
            {
                goto Label_054E;
            }
            if (param == null)
            {
                goto Label_054E;
            }
            this.m_SetEffectsButton.set_interactable(MonoSingleton<GameManager>.Instance.MasterParam.ExistSkillAbilityDeriveDataWithArtifact(param.iname));
            if (this.m_SetEffectsButton.get_interactable() == null)
            {
                goto Label_054E;
            }
            ArtifactSetList.SetSelectedArtifactParam(param);
            goto Label_054E;
        Label_02BD:
            data4 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mShopitem.iname);
            this.limited_shop_item_set_list.Clear();
            if (this.mShopitem.IsSet == null)
            {
                goto Label_0521;
            }
            num2 = 0;
            goto Label_050D;
        Label_02FC:
            obj2 = null;
            if (num2 >= this.limited_shop_item_set_list.Count)
            {
                goto Label_032A;
            }
            obj2 = this.limited_shop_item_set_list[num2].get_gameObject();
            goto Label_0337;
        Label_032A:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
        Label_0337:
            if ((obj2 != null) == null)
            {
                goto Label_0507;
            }
            obj2.SetActive(1);
            vector = obj2.get_transform().get_localScale();
            obj2.get_transform().SetParent(this.ItemParent.get_transform());
            obj2.get_transform().set_localScale(vector);
            element = obj2.GetComponent<LimitedShopSetItemListElement>();
            builder = GameUtility.GetStringBuilder();
            if (this.mShopitem.children[num2].IsArtifact == null)
            {
                goto Label_03EF;
            }
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.mShopitem.children[num2].iname);
            if (param3 == null)
            {
                goto Label_03E1;
            }
            builder.Append(param3.name);
        Label_03E1:
            element.ArtifactParam = param3;
            goto Label_04A5;
        Label_03EF:
            if (this.mShopitem.children[num2].IsConceptCard == null)
            {
                goto Label_044A;
            }
            data5 = ConceptCardData.CreateConceptCardDataForDisplay(this.mShopitem.children[num2].iname);
            if (data5 == null)
            {
                goto Label_043C;
            }
            builder.Append(data5.Param.name);
        Label_043C:
            element.SetupConceptCard(data5);
            goto Label_04A5;
        Label_044A:
            data6 = new ItemData();
            data6.Setup(0L, this.mShopitem.children[num2].iname, this.mShopitem.children[num2].num);
            if (data6 == null)
            {
                goto Label_049C;
            }
            builder.Append(data6.Param.name);
        Label_049C:
            element.itemData = data6;
        Label_04A5:
            builder.Append("\x00d7");
            builder.Append(&this.mShopitem.children[num2].num.ToString());
            element.itemName.set_text(builder.ToString());
            element.SetShopItemDesc(this.mShopitem.children[num2]);
            this.limited_shop_item_set_list.Add(element);
        Label_0507:
            num2 += 1;
        Label_050D:
            if (num2 < ((int) this.mShopitem.children.Length))
            {
                goto Label_02FC;
            }
        Label_0521:
            DataSource.Bind<ItemData>(base.get_gameObject(), data4);
            DataSource.Bind<ItemParam>(base.get_gameObject(), MonoSingleton<GameManager>.Instance.GetItemParam(this.mShopitem.iname));
        Label_054E:
            if (this.mShopitem.IsArtifact == null)
            {
                goto Label_059F;
            }
            this.AmountSliderHolder = this.ArtifactAmountSliderHolder;
            this.AmountSlider = this.ArtifactAmountSlider;
            this.AmountSliderNum = this.ArtifactAmountSliderNum;
            this.IncrementButton = this.ArtifactIncrementButton;
            this.DecrementButton = this.ArtifactDecrementButton;
            goto Label_05DB;
        Label_059F:
            this.AmountSliderHolder = this.ItemAmountSliderHolder;
            this.AmountSlider = this.ItemAmountSlider;
            this.AmountSliderNum = this.ItemAmountSliderNum;
            this.IncrementButton = this.ItemIncrementButton;
            this.DecrementButton = this.ItemDecrementButton;
        Label_05DB:
            this.mEnabledSlider = 0;
            if ((this.AmountSliderHolder != null) == null)
            {
                goto Label_0724;
            }
            if ((this.AmountSlider != null) == null)
            {
                goto Label_0724;
            }
            if ((this.AmountSliderNum != null) == null)
            {
                goto Label_0724;
            }
            if (this.mShopitem.IsNotLimited != null)
            {
                goto Label_0711;
            }
            if (this.mShopitem.remaining_num <= 1)
            {
                goto Label_0711;
            }
            this.mEnabledSlider = 1;
            parameter = this.LimitedItemPriceText.GetComponent<GameParameter>();
            if ((parameter != null) == null)
            {
                goto Label_065F;
            }
            parameter.set_enabled(0);
        Label_065F:
            this.AmountSliderHolder.SetActive(1);
            num3 = ShopData.GetRemainingCurrency(this.mShopitem);
            num4 = ShopData.GetBuyPrice(this.mShopitem);
            num5 = 1;
            if (num4 <= 0)
            {
                goto Label_06A7;
            }
            goto Label_069B;
        Label_0695:
            num5 += 1;
        Label_069B:
            if ((num4 * num5) <= num3)
            {
                goto Label_0695;
            }
        Label_06A7:
            num5 = Math.Max(Math.Min(num5 - 1, this.mShopitem.remaining_num), 1);
            this.AmountSlider.set_minValue(1f);
            this.AmountSlider.set_maxValue((float) num5);
            this.SetSliderValue(1f);
            this.AmountSlider.get_onValueChanged().AddListener(new UnityAction<float>(this, this.OnSliderValueChanged));
            goto Label_0724;
        Label_0711:
            this.mEnabledSlider = 0;
            this.AmountSliderHolder.SetActive(0);
        Label_0724:
            DataSource.Bind<LimitedShopItem>(base.get_gameObject(), this.mShopitem);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void SetArtifactDetailData()
        {
            ArtifactParam param;
            if (this.mShopitem.IsArtifact == null)
            {
                goto Label_0031;
            }
            ArtifactDetailWindow.SetArtifactParam(MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.mShopitem.iname));
        Label_0031:
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

        public void ShowJobList()
        {
            if (this.mIsShowArtifactJob != null)
            {
                goto Label_0016;
            }
            if (this.mArtifactParam != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            GlobalVars.ConditionJobs = this.mArtifactParam.condition_jobs;
            this.mIsShowArtifactJob = 1;
            return;
        }

        private void Start()
        {
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

