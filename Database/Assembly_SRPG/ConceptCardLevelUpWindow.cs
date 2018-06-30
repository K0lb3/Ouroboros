namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(11, "強化素材画面のタブを押した", 1, 11), Pin(1, "強化素材画面のタブを押した", 0, 1), Pin(0, "一括強化ボタンを押した", 0, 0), Pin(12, "錬金素材画面のタブを押した", 1, 12), Pin(2, "錬金素材画面のタブを押した", 0, 2), Pin(10, "一括強化のための選択素材の設定完了", 1, 10), Pin(4, "MAXボタンを押した", 0, 4), Pin(3, "選択クリアボタンを押した", 0, 3)]
    public class ConceptCardLevelUpWindow : ConceptCardDetailBase, IFlowInterface
    {
        public const int PIN_INPUT_PUSH_ENHANCE_BUTTON = 0;
        public const int PIN_INPUT_PUSH_ENHANCE_TOGGLE_BUTTON = 1;
        public const int PIN_INPUT_PUSH_TRUST_TOGGLE_BUTTON = 2;
        public const int PIN_INPUT_PUSH_CLEAR_BUTTON = 3;
        public const int PIN_INPUT_PUSH_MAX_BUTTON = 4;
        public const int PIN_OUTPUT_PUSH_ENHANCE_BUTTON = 10;
        public const int PIN_OUTPUT_PUSH_ENHANCE_TAB = 11;
        public const int PIN_OUTPUT_PUSH_TRUST_TAB = 12;
        [SerializeField]
        private GameObject SelectedCardIcon;
        [SerializeField]
        private RectTransform ListParent;
        [SerializeField]
        private GameObject ListItemTemplate;
        [SerializeField]
        private Text CurrentLevel;
        [SerializeField]
        private Text FinishedLevel;
        [SerializeField]
        private Text MaxLevel;
        [SerializeField]
        private Text NextExp;
        [SerializeField]
        private Slider CardLvSlider;
        [SerializeField]
        private Text GetAllExp;
        [SerializeField]
        private Button DecideBtn;
        [SerializeField]
        private Button MaxBtn;
        [SerializeField]
        private SliderAnimator AddLevelGauge;
        [SerializeField]
        private GameObject MainLevelup;
        [SerializeField]
        private GameObject MainTrust;
        [SerializeField]
        private Toggle TabEnhanceToggle;
        [SerializeField]
        private Toggle TabTrustToggle;
        [SerializeField]
        private RawImage TrustMasterRewardIcon;
        [SerializeField]
        private Image TrustMasterRewardFrame;
        [SerializeField]
        private GameObject TrustMasterRewardItemIconObject;
        [SerializeField]
        private ConceptCardIcon TrustMasterRewardCardIcon;
        [SerializeField]
        private Text TrustValueTxt;
        [SerializeField]
        private Text TrustPredictValueTxt;
        private Dictionary<string, int> mSelectExpMaterials;
        private List<ConceptCardMaterialData> mCacheMaxCardExpMaterialList;
        private List<ConceptCardLevelUpListItem> mCCExpListItem;
        private Dictionary<string, int> mSelectTrustMaterials;
        private List<ConceptCardMaterialData> mCacheMaxCardTrustMaterialList;
        private List<ConceptCardLevelUpListItem> mCCTrustListItem;
        private int mLv;
        private int mExp;
        private int mTrust;
        private TabState mTabState;
        [CompilerGenerated]
        private static Comparison<ConceptCardMaterialData> <>f__am$cache20;
        [CompilerGenerated]
        private static Comparison<ConceptCardMaterialData> <>f__am$cache21;
        [CompilerGenerated]
        private static Comparison<ConceptCardMaterialData> <>f__am$cache22;
        [CompilerGenerated]
        private static Comparison<ConceptCardMaterialData> <>f__am$cache23;

        public ConceptCardLevelUpWindow()
        {
            this.mSelectExpMaterials = new Dictionary<string, int>();
            this.mCacheMaxCardExpMaterialList = new List<ConceptCardMaterialData>();
            this.mCCExpListItem = new List<ConceptCardLevelUpListItem>();
            this.mSelectTrustMaterials = new Dictionary<string, int>();
            this.mCacheMaxCardTrustMaterialList = new List<ConceptCardMaterialData>();
            this.mCCTrustListItem = new List<ConceptCardLevelUpListItem>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <InitListItem>m__2CD(ConceptCardMaterialData a, ConceptCardMaterialData b)
        {
            return (b.Param.en_exp - a.Param.en_exp);
        }

        [CompilerGenerated]
        private static int <InitListItem>m__2CE(ConceptCardMaterialData a, ConceptCardMaterialData b)
        {
            return (b.Param.en_exp - a.Param.en_exp);
        }

        [CompilerGenerated]
        private static int <RefreshUseMaxItems>m__2D4(ConceptCardMaterialData a, ConceptCardMaterialData b)
        {
            return (b.Param.en_exp - a.Param.en_exp);
        }

        [CompilerGenerated]
        private static int <RefreshUseMaxItems>m__2D5(ConceptCardMaterialData a, ConceptCardMaterialData b)
        {
            return (b.Param.en_trust - a.Param.en_trust);
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_0021;

                case 1:
                    goto Label_0034;

                case 2:
                    goto Label_0047;

                case 3:
                    goto Label_005A;

                case 4:
                    goto Label_0065;
            }
            goto Label_0070;
        Label_0021:
            this.SetSelectMaterials();
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_0075;
        Label_0034:
            this.SetTabEnhance();
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            goto Label_0075;
        Label_0047:
            this.SetTabTrust();
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
            goto Label_0075;
        Label_005A:
            this.OnClear();
            goto Label_0075;
        Label_0065:
            this.OnMax();
            goto Label_0075;
        Label_0070:;
        Label_0075:
            return;
        }

        private unsafe void CalcCanCardLevelUpMax()
        {
            long num;
            ConceptCardMaterialData data;
            List<ConceptCardMaterialData>.Enumerator enumerator;
            int num2;
            int num3;
            int num4;
            long num5;
            int num6;
            int num7;
            ConceptCardMaterialData data2;
            int num8;
            ConceptCardMaterialData data3;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            long num14;
            long num15;
            int num16;
            ConceptCardMaterialData data4;
            int num17;
            int num18;
            int num19;
            Dictionary<string, int> dictionary;
            string str;
            int num20;
            Dictionary<string, int> dictionary2;
            if (this.mCacheMaxCardExpMaterialList != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0L;
            enumerator = this.mCacheMaxCardExpMaterialList.GetEnumerator();
        Label_001B:
            try
            {
                goto Label_005C;
            Label_0020:
                data = &enumerator.Current;
                num2 = data.Param.en_exp;
                num3 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(data.Param.iname);
                num4 = num2 * num3;
                num += (long) num4;
            Label_005C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0020;
                }
                goto Label_0079;
            }
            finally
            {
            Label_006D:
                ((List<ConceptCardMaterialData>.Enumerator) enumerator).Dispose();
            }
        Label_0079:
            if (num >= 0L)
            {
                goto Label_008B;
            }
            num = 0x7fffffffffffffffL;
        Label_008B:
            num5 = (long) Mathf.Min((float) base.mConceptCardData.GetExpToLevelMax(), (float) num);
            this.mSelectExpMaterials.Clear();
            num6 = 0;
            num7 = 0;
            goto Label_0299;
        Label_00B7:
            if (num5 > 0L)
            {
                goto Label_00C9;
            }
            num5 = 0L;
            goto Label_02AB;
        Label_00C9:
            data2 = this.mCacheMaxCardExpMaterialList[num7];
            num8 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(data2.Param.iname);
            if (data2 != null)
            {
                goto Label_0109;
            }
            if (num8 > 0)
            {
                goto Label_0109;
            }
            goto Label_0293;
        Label_0109:
            data3 = this.mCacheMaxCardExpMaterialList[num6];
            num9 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(data3.Param.iname);
            if (num7 == num6)
            {
                goto Label_0152;
            }
            if (data3 != null)
            {
                goto Label_0152;
            }
            if (num9 > 0)
            {
                goto Label_0152;
            }
            goto Label_0293;
        Label_0152:
            if (((long) data2.Param.en_exp) <= num5)
            {
                goto Label_016F;
            }
            num6 = num7;
            goto Label_0293;
        Label_016F:
            num10 = (int) (num5 / ((long) data2.Param.en_exp));
            num11 = Mathf.Min(num8, num10);
            num12 = data2.Param.en_exp * num11;
            num13 = data3.Param.en_exp;
            num14 = (long) Mathf.Abs((float) (num5 - ((long) num12)));
            num15 = (long) Mathf.Abs((float) (num5 - ((long) num13)));
            if (num14 <= num15)
            {
                goto Label_026E;
            }
            if (this.mSelectExpMaterials.ContainsKey(data3.Param.iname) == null)
            {
                goto Label_024D;
            }
            num16 = num9 - this.mSelectExpMaterials[data3.Param.iname];
            if (num16 <= 0)
            {
                goto Label_026E;
            }
            num20 = dictionary[str];
            (dictionary = this.mSelectExpMaterials)[str = data3.Param.iname] = num20 + 1;
            num5 = 0L;
            goto Label_02AB;
            goto Label_026E;
        Label_024D:
            this.mSelectExpMaterials.Add(data3.Param.iname, 1);
            num5 = 0L;
            goto Label_02AB;
        Label_026E:
            num5 -= (long) num12;
            this.mSelectExpMaterials.Add(data2.Param.iname, num11);
            num6 = num7;
        Label_0293:
            num7 += 1;
        Label_0299:
            if (num7 < this.mCacheMaxCardExpMaterialList.Count)
            {
                goto Label_00B7;
            }
        Label_02AB:
            if (num5 <= 0L)
            {
                goto Label_0378;
            }
            data4 = this.mCacheMaxCardExpMaterialList[num6];
            num17 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(data4.Param.iname);
            if (data4 == null)
            {
                goto Label_0378;
            }
            if (num17 <= 0)
            {
                goto Label_0378;
            }
            if (this.mSelectExpMaterials.ContainsKey(data4.Param.iname) == null)
            {
                goto Label_0360;
            }
            num18 = num17 - this.mSelectExpMaterials[data4.Param.iname];
            if (num18 <= 0)
            {
                goto Label_0378;
            }
            num20 = dictionary2[str];
            (dictionary2 = this.mSelectExpMaterials)[str = data4.Param.iname] = num20 + 1;
            goto Label_0378;
        Label_0360:
            this.mSelectExpMaterials.Add(data4.Param.iname, 1);
        Label_0378:
            if (this.mSelectExpMaterials.Count <= 0)
            {
                goto Label_03FA;
            }
            num19 = 0;
            goto Label_03E8;
        Label_0391:
            if (this.mSelectExpMaterials.ContainsKey(this.mCCExpListItem[num19].GetConceptCardIName()) == null)
            {
                goto Label_03E2;
            }
            this.mCCExpListItem[num19].SetUseParamItemSliderValue(this.mSelectExpMaterials[this.mCCExpListItem[num19].GetConceptCardIName()]);
        Label_03E2:
            num19 += 1;
        Label_03E8:
            if (num19 < this.mCCExpListItem.Count)
            {
                goto Label_0391;
            }
        Label_03FA:
            this.RefreshFinishedExpStatus();
            return;
        }

        private unsafe void CalcCanCardTrustUpMax()
        {
            long num;
            ConceptCardMaterialData data;
            List<ConceptCardMaterialData>.Enumerator enumerator;
            int num2;
            int num3;
            int num4;
            long num5;
            int num6;
            int num7;
            ConceptCardMaterialData data2;
            int num8;
            ConceptCardMaterialData data3;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            long num14;
            long num15;
            int num16;
            ConceptCardMaterialData data4;
            int num17;
            int num18;
            int num19;
            Dictionary<string, int> dictionary;
            string str;
            int num20;
            Dictionary<string, int> dictionary2;
            if (this.mCacheMaxCardTrustMaterialList != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0L;
            enumerator = this.mCacheMaxCardTrustMaterialList.GetEnumerator();
        Label_001B:
            try
            {
                goto Label_005C;
            Label_0020:
                data = &enumerator.Current;
                num2 = data.Param.en_trust;
                num3 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(data.Param.iname);
                num4 = num2 * num3;
                num += (long) num4;
            Label_005C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0020;
                }
                goto Label_0079;
            }
            finally
            {
            Label_006D:
                ((List<ConceptCardMaterialData>.Enumerator) enumerator).Dispose();
            }
        Label_0079:
            num5 = (long) Mathf.Min((float) (base.Master.FixParam.CardTrustMax - this.mTrust), (float) num);
            this.mSelectTrustMaterials.Clear();
            num6 = 0;
            num7 = 0;
            goto Label_0298;
        Label_00B6:
            if (num5 > 0L)
            {
                goto Label_00C8;
            }
            num5 = 0L;
            goto Label_02AA;
        Label_00C8:
            data2 = this.mCacheMaxCardTrustMaterialList[num7];
            num8 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(data2.Param.iname);
            if (data2 != null)
            {
                goto Label_0108;
            }
            if (num8 > 0)
            {
                goto Label_0108;
            }
            goto Label_0292;
        Label_0108:
            data3 = this.mCacheMaxCardTrustMaterialList[num6];
            num9 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(data3.Param.iname);
            if (num7 == num6)
            {
                goto Label_0151;
            }
            if (data3 != null)
            {
                goto Label_0151;
            }
            if (num9 > 0)
            {
                goto Label_0151;
            }
            goto Label_0292;
        Label_0151:
            if (((long) data2.Param.en_trust) <= num5)
            {
                goto Label_016E;
            }
            num6 = num7;
            goto Label_0292;
        Label_016E:
            num10 = (int) (num5 / ((long) data2.Param.en_trust));
            num11 = Mathf.Min(num8, num10);
            num12 = data2.Param.en_trust * num11;
            num13 = data3.Param.en_trust;
            num14 = (long) Mathf.Abs((float) (num5 - ((long) num12)));
            num15 = (long) Mathf.Abs((float) (num5 - ((long) num13)));
            if (num14 <= num15)
            {
                goto Label_026D;
            }
            if (this.mSelectTrustMaterials.ContainsKey(data3.Param.iname) == null)
            {
                goto Label_024C;
            }
            num16 = num9 - this.mSelectTrustMaterials[data3.Param.iname];
            if (num16 <= 0)
            {
                goto Label_026D;
            }
            num20 = dictionary[str];
            (dictionary = this.mSelectTrustMaterials)[str = data3.Param.iname] = num20 + 1;
            num5 = 0L;
            goto Label_02AA;
            goto Label_026D;
        Label_024C:
            this.mSelectTrustMaterials.Add(data3.Param.iname, 1);
            num5 = 0L;
            goto Label_02AA;
        Label_026D:
            num5 -= (long) num12;
            this.mSelectTrustMaterials.Add(data2.Param.iname, num11);
            num6 = num7;
        Label_0292:
            num7 += 1;
        Label_0298:
            if (num7 < this.mCacheMaxCardTrustMaterialList.Count)
            {
                goto Label_00B6;
            }
        Label_02AA:
            if (num5 <= 0L)
            {
                goto Label_0377;
            }
            data4 = this.mCacheMaxCardTrustMaterialList[num6];
            num17 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(data4.Param.iname);
            if (data4 == null)
            {
                goto Label_0377;
            }
            if (num17 <= 0)
            {
                goto Label_0377;
            }
            if (this.mSelectTrustMaterials.ContainsKey(data4.Param.iname) == null)
            {
                goto Label_035F;
            }
            num18 = num17 - this.mSelectTrustMaterials[data4.Param.iname];
            if (num18 <= 0)
            {
                goto Label_0377;
            }
            num20 = dictionary2[str];
            (dictionary2 = this.mSelectTrustMaterials)[str = data4.Param.iname] = num20 + 1;
            goto Label_0377;
        Label_035F:
            this.mSelectTrustMaterials.Add(data4.Param.iname, 1);
        Label_0377:
            if (this.mSelectTrustMaterials.Count <= 0)
            {
                goto Label_03F9;
            }
            num19 = 0;
            goto Label_03E7;
        Label_0390:
            if (this.mSelectTrustMaterials.ContainsKey(this.mCCTrustListItem[num19].GetConceptCardIName()) == null)
            {
                goto Label_03E1;
            }
            this.mCCTrustListItem[num19].SetUseParamItemSliderValue(this.mSelectTrustMaterials[this.mCCTrustListItem[num19].GetConceptCardIName()]);
        Label_03E1:
            num19 += 1;
        Label_03E7:
            if (num19 < this.mCCTrustListItem.Count)
            {
                goto Label_0390;
            }
        Label_03F9:
            this.RefreshFinishedTrustStatus();
            return;
        }

        private unsafe void InitListItem()
        {
            char[] chArray1;
            string str;
            string[] strArray;
            List<string> list;
            List<ConceptCardMaterialData> list2;
            ConceptCardMaterialData data;
            List<ConceptCardMaterialData>.Enumerator enumerator;
            GameObject obj2;
            ConceptCardLevelUpListItem item;
            chArray1 = new char[] { 0x7c };
            list = new List<string>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_EXPITEM_CHECKS, string.Empty).Split(chArray1));
            this.ListItemTemplate.SetActive(0);
            list2 = new List<ConceptCardMaterialData>();
            list2.AddRange(this.ConceptCardExpMaterials);
            list2.AddRange(this.ConceptCardTrustMaterials);
            enumerator = list2.GetEnumerator();
        Label_005B:
            try
            {
                goto Label_0199;
            Label_0060:
                data = &enumerator.Current;
                if (data.Num != null)
                {
                    goto Label_007F;
                }
                goto Label_0199;
            Label_007F:
                obj2 = Object.Instantiate<GameObject>(this.ListItemTemplate);
                obj2.SetActive(1);
                obj2.get_transform().SetParent(this.ListParent, 0);
                item = obj2.GetComponent<ConceptCardLevelUpListItem>();
                item.OnSelect = new ConceptCardLevelUpListItem.SelectExpItem(this.RefreshParamSelectItems);
                item.ChangeUseMax = new ConceptCardLevelUpListItem.ChangeToggleEvent(this.RefreshUseMaxItems);
                item.OnCheck = new ConceptCardLevelUpListItem.CheckSliderValue(this.OnCheck);
                if (list == null)
                {
                    goto Label_011A;
                }
                if (list.Count <= 0)
                {
                    goto Label_011A;
                }
                item.SetUseMax((list.IndexOf(data.Param.iname) == -1) == 0);
            Label_011A:
                item.AddConceptCardData(data);
                if (data.Param.type != 2)
                {
                    goto Label_0147;
                }
                this.mCCExpListItem.Add(item);
                goto Label_015C;
            Label_0147:
                item.SetExpObject(0);
                this.mCCTrustListItem.Add(item);
            Label_015C:
                if (item.IsUseMax() == null)
                {
                    goto Label_0199;
                }
                if (data.Param.type != 2)
                {
                    goto Label_018C;
                }
                this.mCacheMaxCardExpMaterialList.Add(data);
                goto Label_0199;
            Label_018C:
                this.mCacheMaxCardTrustMaterialList.Add(data);
            Label_0199:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0060;
                }
                goto Label_01B7;
            }
            finally
            {
            Label_01AA:
                ((List<ConceptCardMaterialData>.Enumerator) enumerator).Dispose();
            }
        Label_01B7:
            if (this.mCacheMaxCardExpMaterialList == null)
            {
                goto Label_01FB;
            }
            if (this.mCacheMaxCardExpMaterialList.Count <= 0)
            {
                goto Label_01FB;
            }
            if (<>f__am$cache20 != null)
            {
                goto Label_01F1;
            }
            <>f__am$cache20 = new Comparison<ConceptCardMaterialData>(ConceptCardLevelUpWindow.<InitListItem>m__2CD);
        Label_01F1:
            this.mCacheMaxCardExpMaterialList.Sort(<>f__am$cache20);
        Label_01FB:
            if (this.mCacheMaxCardTrustMaterialList == null)
            {
                goto Label_023F;
            }
            if (this.mCacheMaxCardTrustMaterialList.Count <= 0)
            {
                goto Label_023F;
            }
            if (<>f__am$cache21 != null)
            {
                goto Label_0235;
            }
            <>f__am$cache21 = new Comparison<ConceptCardMaterialData>(ConceptCardLevelUpWindow.<InitListItem>m__2CE);
        Label_0235:
            this.mCacheMaxCardTrustMaterialList.Sort(<>f__am$cache21);
        Label_023F:
            return;
        }

        private unsafe void InitSelectedCardData()
        {
            int num;
            int num2;
            int num3;
            OInt num4;
            OInt num5;
            if ((this.CurrentLevel != null) == null)
            {
                goto Label_002F;
            }
            this.CurrentLevel.set_text(&base.mConceptCardData.Lv.ToString());
        Label_002F:
            if ((this.FinishedLevel != null) == null)
            {
                goto Label_0067;
            }
            if ((this.CurrentLevel != null) == null)
            {
                goto Label_0067;
            }
            this.FinishedLevel.set_text(this.CurrentLevel.get_text());
        Label_0067:
            if ((this.MaxLevel != null) == null)
            {
                goto Label_00A1;
            }
            this.MaxLevel.set_text("/" + &base.mConceptCardData.CurrentLvCap.ToString());
        Label_00A1:
            ConceptCardUtility.GetExpParameter(base.mConceptCardData.Rarity, base.mConceptCardData.Exp, base.mConceptCardData.CurrentLvCap, &num, &num2, &num3);
            if ((this.NextExp != null) == null)
            {
                goto Label_00FF;
            }
            this.NextExp.set_text(&num2.ToString());
        Label_00FF:
            if ((this.CardLvSlider != null) == null)
            {
                goto Label_0126;
            }
            this.CardLvSlider.set_value(1f - (((float) num2) / ((float) num3)));
        Label_0126:
            if ((this.GetAllExp != null) == null)
            {
                goto Label_0147;
            }
            this.GetAllExp.set_text("0");
        Label_0147:
            return;
        }

        private void InitSetTab()
        {
            if (base.mConceptCardData.Lv == base.mConceptCardData.CurrentLvCap)
            {
                goto Label_0039;
            }
            if (MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardExpMaterial() != null)
            {
                goto Label_0045;
            }
        Label_0039:
            this.TabEnhanceToggle.set_interactable(0);
        Label_0045:
            if (base.mConceptCardData.GetReward() == null)
            {
                goto Label_0079;
            }
            if (base.mConceptCardData.GetTrustToLevelMax() == null)
            {
                goto Label_0079;
            }
            if (MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardTrustMaterial() != null)
            {
                goto Label_0085;
            }
        Label_0079:
            this.TabTrustToggle.set_interactable(0);
        Label_0085:
            if (this.TabEnhanceToggle.get_interactable() != null)
            {
                goto Label_009D;
            }
            this.mTabState = 1;
            return;
        Label_009D:
            if (this.TabTrustToggle.get_interactable() != null)
            {
                goto Label_00B5;
            }
            this.mTabState = 0;
            return;
        Label_00B5:
            this.mTabState = LoadTabState();
            return;
        }

        private void InitTrust()
        {
            ConceptCardTrustRewardItemParam param;
            bool flag;
            ConceptCardData data;
            string str;
            param = base.mConceptCardData.GetReward();
            if (param != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            flag = param.reward_type == 5;
            base.SwitchObject(flag, this.TrustMasterRewardCardIcon.get_gameObject(), this.TrustMasterRewardItemIconObject);
            if (flag == null)
            {
                goto Label_005E;
            }
            data = ConceptCardData.CreateConceptCardDataForDisplay(param.iname);
            if (data == null)
            {
                goto Label_0072;
            }
            this.TrustMasterRewardCardIcon.Setup(data);
            goto Label_0072;
        Label_005E:
            str = param.GetIconPath();
            base.LoadImage(str, this.TrustMasterRewardIcon);
        Label_0072:
            base.SetSprite(this.TrustMasterRewardFrame, param.GetFrameSprite());
            ConceptCardManager.SubstituteTrustFormat(base.mConceptCardData, this.TrustValueTxt, base.mConceptCardData.Trust, 0);
            ConceptCardManager.SubstituteTrustFormat(base.mConceptCardData, this.TrustPredictValueTxt, base.mConceptCardData.Trust, 0);
            return;
        }

        private unsafe void InitWindowButton()
        {
            int num;
            KeyValuePair<string, int> pair;
            Dictionary<string, int>.Enumerator enumerator;
            KeyValuePair<string, int> pair2;
            Dictionary<string, int>.Enumerator enumerator2;
            num = 0;
            if (this.mTabState != null)
            {
                goto Label_00A3;
            }
            this.MaxBtn.set_interactable((this.mCacheMaxCardExpMaterialList == null) ? 0 : (this.mCacheMaxCardExpMaterialList.Count > 0));
            this.DecideBtn.set_interactable(0);
            if (this.mSelectExpMaterials == null)
            {
                goto Label_0142;
            }
            enumerator = this.mSelectExpMaterials.GetEnumerator();
        Label_0057:
            try
            {
                goto Label_006E;
            Label_005C:
                pair = &enumerator.Current;
                num += &pair.Value;
            Label_006E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_005C;
                }
                goto Label_008B;
            }
            finally
            {
            Label_007F:
                ((Dictionary<string, int>.Enumerator) enumerator).Dispose();
            }
        Label_008B:
            if (num <= 0)
            {
                goto Label_0142;
            }
            this.DecideBtn.set_interactable(1);
            goto Label_0142;
        Label_00A3:
            if (this.mTabState != 1)
            {
                goto Label_0142;
            }
            this.MaxBtn.set_interactable((this.mCacheMaxCardTrustMaterialList == null) ? 0 : (this.mCacheMaxCardTrustMaterialList.Count > 0));
            this.DecideBtn.set_interactable(0);
            if (this.mSelectTrustMaterials == null)
            {
                goto Label_0142;
            }
            enumerator2 = this.mSelectTrustMaterials.GetEnumerator();
        Label_00FA:
            try
            {
                goto Label_0111;
            Label_00FF:
                pair2 = &enumerator2.Current;
                num += &pair2.Value;
            Label_0111:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00FF;
                }
                goto Label_012F;
            }
            finally
            {
            Label_0122:
                ((Dictionary<string, int>.Enumerator) enumerator2).Dispose();
            }
        Label_012F:
            if (num <= 0)
            {
                goto Label_0142;
            }
            this.DecideBtn.set_interactable(1);
        Label_0142:
            return;
        }

        private static unsafe TabState LoadTabState()
        {
            string str;
            int num;
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_PAGE_CHECKS) != null)
            {
                goto Label_0011;
            }
            return 0;
        Label_0011:
            str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_PAGE_CHECKS, string.Empty);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            num = 0;
            if (int.TryParse(str, &num) != null)
            {
                goto Label_003F;
            }
            return 0;
        Label_003F:
            return num;
        }

        private unsafe int OnCheck(string iname, int num)
        {
            Dictionary<string, int> dictionary;
            ConceptCardParam param;
            long num2;
            long num3;
            string str;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator;
            ConceptCardParam param2;
            long num4;
            int num5;
            int num6;
            int num7;
            string str2;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator2;
            ConceptCardParam param3;
            long num8;
            int num9;
            if (string.IsNullOrEmpty(iname) != null)
            {
                goto Label_0011;
            }
            if (num != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return -1;
        Label_0013:
            dictionary = null;
            if (this.mTabState != null)
            {
                goto Label_002C;
            }
            dictionary = this.mSelectExpMaterials;
            goto Label_0046;
        Label_002C:
            if (this.mTabState != 1)
            {
                goto Label_0044;
            }
            dictionary = this.mSelectTrustMaterials;
            goto Label_0046;
        Label_0044:
            return -1;
        Label_0046:
            if (dictionary.ContainsKey(iname) == null)
            {
                goto Label_0061;
            }
            if (dictionary[iname] <= num)
            {
                goto Label_0061;
            }
            return -1;
        Label_0061:
            param = base.Master.GetConceptCardParam(iname);
            if (param != null)
            {
                goto Label_0076;
            }
            return -1;
        Label_0076:
            if (MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(iname) != null)
            {
                goto Label_008D;
            }
            return -1;
        Label_008D:
            if (this.mTabState != null)
            {
                goto Label_014D;
            }
            num2 = (long) base.mConceptCardData.GetExpToLevelMax();
            num3 = 0L;
            enumerator = dictionary.Keys.GetEnumerator();
        Label_00B5:
            try
            {
                goto Label_00FF;
            Label_00BA:
                str = &enumerator.Current;
                if ((str == iname) == null)
                {
                    goto Label_00D5;
                }
                goto Label_00FF;
            Label_00D5:
                param2 = base.Master.GetConceptCardParam(str);
                if (param2 == null)
                {
                    goto Label_00FF;
                }
                num3 += (long) (param2.en_exp * dictionary[str]);
            Label_00FF:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00BA;
                }
                goto Label_011D;
            }
            finally
            {
            Label_0110:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_011D:
            num2 -= num3;
            num4 = (long) (param.en_exp * num);
            if (num2 >= num4)
            {
                goto Label_021F;
            }
            return Mathf.CeilToInt(((float) num2) / ((float) param.en_exp));
            goto Label_021F;
        Label_014D:
            num6 = base.Master.FixParam.CardTrustMax - base.mConceptCardData.Trust;
            num7 = 0;
            enumerator2 = dictionary.Keys.GetEnumerator();
        Label_0185:
            try
            {
                goto Label_01D0;
            Label_018A:
                str2 = &enumerator2.Current;
                if ((str2 == iname) == null)
                {
                    goto Label_01A5;
                }
                goto Label_01D0;
            Label_01A5:
                param3 = base.Master.GetConceptCardParam(str2);
                if (param3 == null)
                {
                    goto Label_01D0;
                }
                num7 += param3.en_trust * dictionary[str2];
            Label_01D0:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_018A;
                }
                goto Label_01EE;
            }
            finally
            {
            Label_01E1:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator2).Dispose();
            }
        Label_01EE:
            num6 -= num7;
            num8 = (long) (param.en_trust * num);
            if (((long) num6) >= num8)
            {
                goto Label_021F;
            }
            return Mathf.CeilToInt(((float) num6) / ((float) param.en_trust));
        Label_021F:
            return num;
        }

        private void OnClear()
        {
            Dictionary<string, int> dictionary;
            List<ConceptCardLevelUpListItem> list;
            int num;
            ConceptCardLevelUpListItem item;
            dictionary = null;
            list = null;
            if (this.mTabState != null)
            {
                goto Label_0022;
            }
            dictionary = this.mSelectExpMaterials;
            list = this.mCCExpListItem;
            goto Label_0042;
        Label_0022:
            if (this.mTabState != 1)
            {
                goto Label_0041;
            }
            dictionary = this.mSelectTrustMaterials;
            list = this.mCCTrustListItem;
            goto Label_0042;
        Label_0041:
            return;
        Label_0042:
            if (dictionary.Count <= 0)
            {
                goto Label_00A6;
            }
            num = 0;
            goto Label_0078;
        Label_0055:
            item = list[num].GetComponent<ConceptCardLevelUpListItem>();
            if ((item != null) == null)
            {
                goto Label_0074;
            }
            item.Reset();
        Label_0074:
            num += 1;
        Label_0078:
            if (num < list.Count)
            {
                goto Label_0055;
            }
            dictionary.Clear();
            if (this.mTabState != null)
            {
                goto Label_00A0;
            }
            this.RefreshFinishedExpStatus();
            goto Label_00A6;
        Label_00A0:
            this.RefreshFinishedTrustStatus();
        Label_00A6:
            return;
        }

        private void OnMax()
        {
            List<ConceptCardMaterialData> list;
            List<ConceptCardLevelUpListItem> list2;
            int num;
            ConceptCardLevelUpListItem item;
            list = null;
            list2 = null;
            if (this.mTabState != null)
            {
                goto Label_0022;
            }
            list = this.mCacheMaxCardExpMaterialList;
            list2 = this.mCCExpListItem;
            goto Label_0042;
        Label_0022:
            if (this.mTabState != 1)
            {
                goto Label_0041;
            }
            list = this.mCacheMaxCardTrustMaterialList;
            list2 = this.mCCTrustListItem;
            goto Label_0042;
        Label_0041:
            return;
        Label_0042:
            if (list == null)
            {
                goto Label_0054;
            }
            if (list.Count >= 0)
            {
                goto Label_0055;
            }
        Label_0054:
            return;
        Label_0055:
            num = 0;
            goto Label_007F;
        Label_005C:
            item = list2[num].GetComponent<ConceptCardLevelUpListItem>();
            if ((item != null) == null)
            {
                goto Label_007B;
            }
            item.Reset();
        Label_007B:
            num += 1;
        Label_007F:
            if (num < list2.Count)
            {
                goto Label_005C;
            }
            if (this.mTabState != null)
            {
                goto Label_00A1;
            }
            this.CalcCanCardLevelUpMax();
            goto Label_00A7;
        Label_00A1:
            this.CalcCanCardTrustUpMax();
        Label_00A7:
            return;
        }

        private unsafe void RefreshFinishedExpStatus()
        {
            int num;
            string str;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator;
            ConceptCardParam param;
            int num2;
            int num3;
            int num4;
            int num5;
            ConceptCardLevelUpListItem item;
            List<ConceptCardLevelUpListItem>.Enumerator enumerator2;
            float num6;
            float num7;
            float num8;
            int num9;
            int num10;
            int num11;
            int num12;
            if ((this.mSelectExpMaterials != null) && (this.mSelectExpMaterials.Count > 0))
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            num = 0;
            enumerator = this.mSelectExpMaterials.Keys.GetEnumerator();
        Label_0030:
            try
            {
                goto Label_0095;
            Label_0035:
                str = &enumerator.Current;
                param = base.Master.GetConceptCardParam(str);
                num2 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(str);
                if (param == null)
                {
                    goto Label_0095;
                }
                num3 = this.mSelectExpMaterials[str];
                if (num3 == null)
                {
                    goto Label_0095;
                }
                if (num3 <= num2)
                {
                    goto Label_0085;
                }
                goto Label_0095;
            Label_0085:
                num4 = param.en_exp * num3;
                num += num4;
            Label_0095:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0035;
                }
                goto Label_00B2;
            }
            finally
            {
            Label_00A6:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_00B2:
            num5 = base.Master.GetConceptCardLevelExp(base.mConceptCardData.Rarity, base.mConceptCardData.CurrentLvCap);
            this.mExp = Math.Min(base.mConceptCardData.Exp + num, num5);
            this.mLv = base.Master.CalcConceptCardLevel(base.mConceptCardData.Rarity, base.mConceptCardData.Exp + num, base.mConceptCardData.CurrentLvCap);
            enumerator2 = this.mCCExpListItem.GetEnumerator();
        Label_014E:
            try
            {
                goto Label_0173;
            Label_0153:
                item = &enumerator2.Current;
                item.SetInputLock(((this.mExp < num5) == 0) == 0);
            Label_0173:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0153;
                }
                goto Label_0191;
            }
            finally
            {
            Label_0184:
                ((List<ConceptCardLevelUpListItem>.Enumerator) enumerator2).Dispose();
            }
        Label_0191:
            if ((this.FinishedLevel != null) == null)
            {
                goto Label_0228;
            }
            this.FinishedLevel.set_text(&this.mLv.ToString());
            if (this.mLv < base.mConceptCardData.CurrentLvCap)
            {
                goto Label_01E8;
            }
            this.FinishedLevel.set_color(Color.get_red());
            goto Label_0228;
        Label_01E8:
            if (this.mLv <= base.mConceptCardData.Lv)
            {
                goto Label_0218;
            }
            this.FinishedLevel.set_color(Color.get_green());
            goto Label_0228;
        Label_0218:
            this.FinishedLevel.set_color(Color.get_white());
        Label_0228:
            if ((this.AddLevelGauge != null) == null)
            {
                goto Label_0300;
            }
            if ((this.mExp != base.mConceptCardData.Exp) && (num != null))
            {
                goto Label_0274;
            }
            this.AddLevelGauge.AnimateValue(0f, 0f);
            goto Label_0300;
        Label_0274:
            num6 = (float) (this.mExp - base.Master.GetConceptCardLevelExp(base.mConceptCardData.Rarity, base.mConceptCardData.Lv));
            num7 = (float) base.Master.GetConceptCardNextExp(base.mConceptCardData.Rarity, base.mConceptCardData.Lv);
            num8 = Mathf.Min(1f, Mathf.Clamp01(((float) num6) / num7));
            this.AddLevelGauge.AnimateValue(num8, 0f);
        Label_0300:
            if ((this.NextExp != null) == null)
            {
                goto Label_03EB;
            }
            num9 = 0;
            if (this.mExp >= num5)
            {
                goto Label_03D9;
            }
            num10 = base.Master.GetConceptCardLevelExp(base.mConceptCardData.Rarity, this.mLv);
            num11 = base.Master.GetConceptCardNextExp(base.mConceptCardData.Rarity, this.mLv);
            if (this.mExp < num10)
            {
                goto Label_03AE;
            }
            num11 = base.Master.GetConceptCardNextExp(base.mConceptCardData.Rarity, Math.Min(base.mConceptCardData.CurrentLvCap, this.mLv + 1));
        Label_03AE:
            num12 = this.mExp - num10;
            num9 = (num11 <= num12) ? 0 : (num11 - num12);
            num9 = Math.Max(0, num9);
        Label_03D9:
            this.NextExp.set_text(&num9.ToString());
        Label_03EB:
            if ((this.GetAllExp != null) == null)
            {
                goto Label_040E;
            }
            this.GetAllExp.set_text(&num.ToString());
        Label_040E:
            this.DecideBtn.set_interactable(num > 0);
            return;
        }

        private unsafe void RefreshFinishedTrustStatus()
        {
            int num;
            string str;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator;
            ConceptCardParam param;
            int num2;
            int num3;
            int num4;
            int num5;
            ConceptCardLevelUpListItem item;
            List<ConceptCardLevelUpListItem>.Enumerator enumerator2;
            float num6;
            float num7;
            if (this.mSelectTrustMaterials == null)
            {
                goto Label_001C;
            }
            if (this.mSelectTrustMaterials.Count > 0)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            num = 0;
            enumerator = this.mSelectTrustMaterials.Keys.GetEnumerator();
        Label_0030:
            try
            {
                goto Label_0095;
            Label_0035:
                str = &enumerator.Current;
                param = base.Master.GetConceptCardParam(str);
                num2 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(str);
                if (param == null)
                {
                    goto Label_0095;
                }
                num3 = this.mSelectTrustMaterials[str];
                if (num3 == null)
                {
                    goto Label_0095;
                }
                if (num3 <= num2)
                {
                    goto Label_0085;
                }
                goto Label_0095;
            Label_0085:
                num4 = param.en_trust * num3;
                num += num4;
            Label_0095:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0035;
                }
                goto Label_00B2;
            }
            finally
            {
            Label_00A6:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_00B2:
            num5 = base.Master.FixParam.CardTrustMax;
            this.mTrust = Math.Min(base.mConceptCardData.Trust + num, num5);
            enumerator2 = this.mCCTrustListItem.GetEnumerator();
        Label_00F5:
            try
            {
                goto Label_011A;
            Label_00FA:
                item = &enumerator2.Current;
                item.SetInputLock(((this.mTrust < num5) == 0) == 0);
            Label_011A:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00FA;
                }
                goto Label_0138;
            }
            finally
            {
            Label_012B:
                ((List<ConceptCardLevelUpListItem>.Enumerator) enumerator2).Dispose();
            }
        Label_0138:
            if ((this.TrustPredictValueTxt != null) == null)
            {
                goto Label_01C3;
            }
            ConceptCardManager.SubstituteTrustFormat(base.mConceptCardData, this.TrustPredictValueTxt, this.mTrust, 0);
            if (this.mTrust < num5)
            {
                goto Label_0183;
            }
            this.TrustPredictValueTxt.set_color(Color.get_red());
            goto Label_01C3;
        Label_0183:
            if (this.mTrust <= base.mConceptCardData.Trust)
            {
                goto Label_01B3;
            }
            this.TrustPredictValueTxt.set_color(Color.get_green());
            goto Label_01C3;
        Label_01B3:
            this.TrustPredictValueTxt.set_color(Color.get_white());
        Label_01C3:
            if ((this.AddLevelGauge != null) == null)
            {
                goto Label_026F;
            }
            if (this.mTrust == base.mConceptCardData.Trust)
            {
                goto Label_01F5;
            }
            if (num != null)
            {
                goto Label_020F;
            }
        Label_01F5:
            this.AddLevelGauge.AnimateValue(0f, 0f);
            goto Label_026F;
        Label_020F:
            num6 = (float) (base.Master.FixParam.CardTrustMax - base.mConceptCardData.Trust);
            num7 = Mathf.Min(1f, Mathf.Clamp01(((float) base.mConceptCardData.Trust) / num6));
            this.AddLevelGauge.AnimateValue(num7, 0f);
        Label_026F:
            this.DecideBtn.set_interactable(num > 0);
            return;
        }

        private void RefreshParamSelectItems(string iname, int value)
        {
            Dictionary<string, int> dictionary;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(iname) != null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            dictionary = null;
            if (this.mTabState != null)
            {
                goto Label_003B;
            }
            dictionary = this.mSelectExpMaterials;
            goto Label_0054;
        Label_003B:
            if (this.mTabState != 1)
            {
                goto Label_0053;
            }
            dictionary = this.mSelectTrustMaterials;
            goto Label_0054;
        Label_0053:
            return;
        Label_0054:
            if (dictionary.ContainsKey(iname) != null)
            {
                goto Label_006D;
            }
            dictionary.Add(iname, value);
            goto Label_0075;
        Label_006D:
            dictionary[iname] = value;
        Label_0075:
            if (this.mTabState != null)
            {
                goto Label_008B;
            }
            this.RefreshFinishedExpStatus();
            goto Label_0091;
        Label_008B:
            this.RefreshFinishedTrustStatus();
        Label_0091:
            return;
        }

        private void RefreshUseMaxItems(string iname, bool is_on)
        {
            ConceptCardMaterialData data;
            List<ConceptCardMaterialData> list;
            <RefreshUseMaxItems>c__AnonStorey325 storey;
            storey = new <RefreshUseMaxItems>c__AnonStorey325();
            storey.iname = iname;
            if (string.IsNullOrEmpty(storey.iname) == null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            data = null;
            list = null;
            if (this.mTabState != null)
            {
                goto Label_0051;
            }
            list = this.mCacheMaxCardExpMaterialList;
            data = this.ConceptCardExpMaterials.Find(new Predicate<ConceptCardMaterialData>(storey.<>m__2CF));
            goto Label_0082;
        Label_0051:
            if (this.mTabState != 1)
            {
                goto Label_0081;
            }
            list = this.mCacheMaxCardTrustMaterialList;
            data = this.ConceptCardTrustMaterials.Find(new Predicate<ConceptCardMaterialData>(storey.<>m__2D0));
            goto Label_0082;
        Label_0081:
            return;
        Label_0082:
            if (data != null)
            {
                goto Label_0089;
            }
            return;
        Label_0089:
            if (is_on != null)
            {
                goto Label_00C4;
            }
            if (list.FindIndex(new Predicate<ConceptCardMaterialData>(storey.<>m__2D1)) == -1)
            {
                goto Label_00E2;
            }
            list.RemoveAt(list.FindIndex(new Predicate<ConceptCardMaterialData>(storey.<>m__2D2)));
            goto Label_00E2;
        Label_00C4:
            if (list.Find(new Predicate<ConceptCardMaterialData>(storey.<>m__2D3)) != null)
            {
                goto Label_00E2;
            }
            list.Add(data);
        Label_00E2:
            if (this.mTabState != null)
            {
                goto Label_0115;
            }
            if (<>f__am$cache22 != null)
            {
                goto Label_0106;
            }
            <>f__am$cache22 = new Comparison<ConceptCardMaterialData>(ConceptCardLevelUpWindow.<RefreshUseMaxItems>m__2D4);
        Label_0106:
            list.Sort(<>f__am$cache22);
            goto Label_0138;
        Label_0115:
            if (<>f__am$cache23 != null)
            {
                goto Label_012E;
            }
            <>f__am$cache23 = new Comparison<ConceptCardMaterialData>(ConceptCardLevelUpWindow.<RefreshUseMaxItems>m__2D5);
        Label_012E:
            list.Sort(<>f__am$cache23);
        Label_0138:
            this.SaveSelectUseMax();
            this.MaxBtn.set_interactable((list == null) ? 0 : (list.Count > 0));
            return;
        }

        private unsafe void SavePage()
        {
            int num;
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_PAGE_CHECKS, &this.mTabState.ToString(), 1);
            return;
        }

        private void SaveSelectUseMax()
        {
            List<string> list;
            int num;
            int num2;
            string str;
            list = new List<string>();
            num = 0;
            goto Label_002D;
        Label_000D:
            list.Add(this.mCacheMaxCardExpMaterialList[num].Param.iname);
            num += 1;
        Label_002D:
            if (num < this.mCacheMaxCardExpMaterialList.Count)
            {
                goto Label_000D;
            }
            num2 = 0;
            goto Label_0065;
        Label_0045:
            list.Add(this.mCacheMaxCardTrustMaterialList[num2].Param.iname);
            num2 += 1;
        Label_0065:
            if (num2 < this.mCacheMaxCardTrustMaterialList.Count)
            {
                goto Label_0045;
            }
            str = (list.Count <= 0) ? string.Empty : string.Join("|", list.ToArray());
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_EXPITEM_CHECKS, str, 1);
            return;
        }

        private unsafe void SetSelectMaterials()
        {
            ConceptCardManager manager;
            List<SelecteConceptCardMaterial> list;
            Dictionary<string, int> dictionary;
            List<ConceptCardLevelUpListItem> list2;
            List<ConceptCardMaterialData> list3;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator;
            int num;
            int num2;
            ConceptCardMaterialData data;
            List<ConceptCardMaterialData>.Enumerator enumerator2;
            ConceptCardLevelUpListItem item;
            SelecteConceptCardMaterial material;
            <SetSelectMaterials>c__AnonStorey326 storey;
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            list = new List<SelecteConceptCardMaterial>();
            dictionary = null;
            list2 = null;
            list3 = null;
            if (this.mTabState != null)
            {
                goto Label_0046;
            }
            dictionary = this.mSelectExpMaterials;
            list2 = this.mCCExpListItem;
            list3 = this.ConceptCardExpMaterials;
            goto Label_006E;
        Label_0046:
            if (this.mTabState != 1)
            {
                goto Label_006D;
            }
            dictionary = this.mSelectTrustMaterials;
            list2 = this.mCCTrustListItem;
            list3 = this.ConceptCardTrustMaterials;
            goto Label_006E;
        Label_006D:
            return;
        Label_006E:
            storey = new <SetSelectMaterials>c__AnonStorey326();
            enumerator = dictionary.Keys.GetEnumerator();
        Label_0082:
            try
            {
                goto Label_01BD;
            Label_0087:
                storey.key = &enumerator.Current;
                if (base.Master.GetConceptCardParam(storey.key) != null)
                {
                    goto Label_00B1;
                }
                goto Label_01BD;
            Label_00B1:
                num = dictionary[storey.key];
                if (num > MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(storey.key))
                {
                    goto Label_01BD;
                }
                if (num > 0)
                {
                    goto Label_00EA;
                }
                goto Label_01BD;
            Label_00EA:
                num2 = 0;
                enumerator2 = list3.GetEnumerator();
            Label_00F6:
                try
                {
                    goto Label_019F;
                Label_00FB:
                    data = &enumerator2.Current;
                    if ((data.Param.iname == storey.key) == null)
                    {
                        goto Label_019F;
                    }
                    item = list2.Find(new Predicate<ConceptCardLevelUpListItem>(storey.<>m__2D6));
                    if ((item == null) == null)
                    {
                        goto Label_0148;
                    }
                    goto Label_019F;
                Label_0148:
                    material = new SelecteConceptCardMaterial();
                    material.mUniqueID = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialUniqueID(storey.key);
                    material.mSelectedData = item.GetConceptCardData();
                    material.mSelectNum = num;
                    list.Add(material);
                    num2 += 1;
                    if (num2 != num)
                    {
                        goto Label_019F;
                    }
                    goto Label_01AB;
                Label_019F:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_00FB;
                    }
                Label_01AB:
                    goto Label_01BD;
                }
                finally
                {
                Label_01B0:
                    ((List<ConceptCardMaterialData>.Enumerator) enumerator2).Dispose();
                }
            Label_01BD:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0087;
                }
                goto Label_01DB;
            }
            finally
            {
            Label_01CE:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_01DB:
            manager.BulkSelectedMaterialList = list;
            return;
        }

        private unsafe void SetTabEnhance()
        {
            ConceptCardLevelUpListItem item;
            List<ConceptCardLevelUpListItem>.Enumerator enumerator;
            ConceptCardLevelUpListItem item2;
            List<ConceptCardLevelUpListItem>.Enumerator enumerator2;
            this.mTabState = 0;
            if ((this.MainLevelup == null) != null)
            {
                goto Label_0029;
            }
            if ((this.MainTrust == null) == null)
            {
                goto Label_002A;
            }
        Label_0029:
            return;
        Label_002A:
            this.MainLevelup.SetActive(1);
            this.MainTrust.SetActive(0);
            enumerator = this.mCCExpListItem.GetEnumerator();
        Label_004E:
            try
            {
                goto Label_0067;
            Label_0053:
                item = &enumerator.Current;
                item.get_gameObject().SetActive(1);
            Label_0067:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0053;
                }
                goto Label_0084;
            }
            finally
            {
            Label_0078:
                ((List<ConceptCardLevelUpListItem>.Enumerator) enumerator).Dispose();
            }
        Label_0084:
            enumerator2 = this.mCCTrustListItem.GetEnumerator();
        Label_0090:
            try
            {
                goto Label_00A9;
            Label_0095:
                item2 = &enumerator2.Current;
                item2.get_gameObject().SetActive(0);
            Label_00A9:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0095;
                }
                goto Label_00C6;
            }
            finally
            {
            Label_00BA:
                ((List<ConceptCardLevelUpListItem>.Enumerator) enumerator2).Dispose();
            }
        Label_00C6:
            this.TabEnhanceToggle.set_isOn(1);
            this.TabTrustToggle.set_isOn(0);
            this.InitWindowButton();
            this.SavePage();
            return;
        }

        private unsafe void SetTabTrust()
        {
            ConceptCardLevelUpListItem item;
            List<ConceptCardLevelUpListItem>.Enumerator enumerator;
            ConceptCardLevelUpListItem item2;
            List<ConceptCardLevelUpListItem>.Enumerator enumerator2;
            this.mTabState = 1;
            if ((this.MainLevelup == null) != null)
            {
                goto Label_0029;
            }
            if ((this.MainTrust == null) == null)
            {
                goto Label_002A;
            }
        Label_0029:
            return;
        Label_002A:
            this.MainLevelup.SetActive(0);
            this.MainTrust.SetActive(1);
            enumerator = this.mCCExpListItem.GetEnumerator();
        Label_004E:
            try
            {
                goto Label_0067;
            Label_0053:
                item = &enumerator.Current;
                item.get_gameObject().SetActive(0);
            Label_0067:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0053;
                }
                goto Label_0084;
            }
            finally
            {
            Label_0078:
                ((List<ConceptCardLevelUpListItem>.Enumerator) enumerator).Dispose();
            }
        Label_0084:
            enumerator2 = this.mCCTrustListItem.GetEnumerator();
        Label_0090:
            try
            {
                goto Label_00A9;
            Label_0095:
                item2 = &enumerator2.Current;
                item2.get_gameObject().SetActive(1);
            Label_00A9:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0095;
                }
                goto Label_00C6;
            }
            finally
            {
            Label_00BA:
                ((List<ConceptCardLevelUpListItem>.Enumerator) enumerator2).Dispose();
            }
        Label_00C6:
            this.TabEnhanceToggle.set_isOn(0);
            this.TabTrustToggle.set_isOn(1);
            this.InitWindowButton();
            this.SavePage();
            return;
        }

        private void Start()
        {
            ConceptCardManager manager;
            ConceptCardIcon icon;
            ConceptCardManager manager2;
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            base.mConceptCardData = manager.SelectedConceptCardData;
            if (base.mConceptCardData != null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            this.mExp = base.mConceptCardData.Exp;
            this.mLv = base.mConceptCardData.Lv;
            this.mTrust = base.mConceptCardData.Trust;
            if ((this.SelectedCardIcon == null) == null)
            {
                goto Label_007F;
            }
            return;
        Label_007F:
            icon = this.SelectedCardIcon.GetComponent<ConceptCardIcon>();
            if ((icon == null) == null)
            {
                goto Label_0098;
            }
            return;
        Label_0098:
            if ((this.ListItemTemplate == null) == null)
            {
                goto Label_00AA;
            }
            return;
        Label_00AA:
            if ((this.ListItemTemplate.GetComponent<ConceptCardLevelUpListItem>() == null) == null)
            {
                goto Label_00C1;
            }
            return;
        Label_00C1:
            if (this.ConceptCardExpMaterials == null)
            {
                goto Label_00DC;
            }
            if (this.ConceptCardExpMaterials.Count != null)
            {
                goto Label_00F8;
            }
        Label_00DC:
            if (this.ConceptCardTrustMaterials == null)
            {
                goto Label_00F7;
            }
            if (this.ConceptCardTrustMaterials.Count != null)
            {
                goto Label_00F8;
            }
        Label_00F7:
            return;
        Label_00F8:
            icon.Setup(base.mConceptCardData);
            this.InitSelectedCardData();
            this.InitListItem();
            this.InitSetTab();
            if (this.mTabState != null)
            {
                goto Label_012C;
            }
            this.SetTabEnhance();
            goto Label_0132;
        Label_012C:
            this.SetTabTrust();
        Label_0132:
            this.InitTrust();
            this.InitWindowButton();
            manager2 = ConceptCardManager.Instance;
            if ((manager2 == null) == null)
            {
                goto Label_0151;
            }
            return;
        Label_0151:
            manager2.BulkSelectedMaterialList.Clear();
            return;
        }

        private List<ConceptCardMaterialData> ConceptCardExpMaterials
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.Player.ConceptCardExpMaterials;
            }
        }

        private List<ConceptCardMaterialData> ConceptCardTrustMaterials
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.Player.ConceptCardTrustMaterials;
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshUseMaxItems>c__AnonStorey325
        {
            internal string iname;

            public <RefreshUseMaxItems>c__AnonStorey325()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2CF(ConceptCardMaterialData p)
            {
                return (p.Param.iname == this.iname);
            }

            internal bool <>m__2D0(ConceptCardMaterialData p)
            {
                return (p.Param.iname == this.iname);
            }

            internal bool <>m__2D1(ConceptCardMaterialData p)
            {
                return (p.Param.iname == this.iname);
            }

            internal bool <>m__2D2(ConceptCardMaterialData p)
            {
                return (p.Param.iname == this.iname);
            }

            internal bool <>m__2D3(ConceptCardMaterialData p)
            {
                return (p.Param.iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <SetSelectMaterials>c__AnonStorey326
        {
            internal string key;

            public <SetSelectMaterials>c__AnonStorey326()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2D6(ConceptCardLevelUpListItem ccd)
            {
                return (ccd.GetConceptCardIName() == this.key);
            }
        }

        private enum TabState
        {
            Enhance,
            Trust
        }
    }
}

