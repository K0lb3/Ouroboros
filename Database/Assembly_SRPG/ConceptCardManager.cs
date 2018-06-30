namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(14, "VisionMaster再生", 0, 14), Pin(13, "グループスキル強化再生", 0, 13), Pin(0x6f, "トラストマスター再生後", 1, 0x6f), Pin(11, "トラストマスター再生", 0, 11), Pin(0x70, "限界突破アニメ再生後", 1, 0x70), Pin(12, "限界突破アニメ再生", 0, 12), Pin(110, "強化アニメ再生後", 1, 110), Pin(10, "強化アニメ再生", 0, 10), Pin(1, "選択素材等クリア", 0, 1), Pin(0, "初期化", 0, 0), Pin(200, "TIPS表示", 1, 200), Pin(0x72, "VisionMaster再生後", 1, 0x72), Pin(0x71, "グループスキル強化再生後", 1, 0x71)]
    public class ConceptCardManager : MonoBehaviour, IFlowInterface
    {
        public const int PIN_INIT = 0;
        public const int PIN_CLEAR_MAT = 1;
        public const int PIN_SELL = 3;
        public const int PIN_ENHANCE_ANIM = 10;
        public const int PIN_TRUSTMASTER_ANIM = 11;
        public const int PIN_AWAKE_ANIM = 12;
        public const int PIN_GROUPSKILL_POWERUP_ANIM = 13;
        public const int PIN_GROUPSKILL_MAX_POWERUP_ANIM = 14;
        public const int PIN_ENHANCE_ANIM_OUTPUT = 110;
        public const int PIN_TRUSTMASTER_ANIM_OUTPUT = 0x6f;
        public const int PIN_AWAKE_ANIM_OUTPUT = 0x70;
        public const int PIN_GROUPSKILL_POWERUP_ANIM_OUTPUT = 0x71;
        public const int PIN_GROUPSKILL_MAX_POWERUP_ANIM_OUTPUT = 0x72;
        public const int PIN_TIPS_EQUIPMENT_OUTPUT = 200;
        private static ConceptCardManager _instance;
        [SerializeField]
        private GameObject mConceptCardBranceList;
        [SerializeField]
        private GameObject mConceptCardEnhanceList;
        [SerializeField]
        private GameObject mConceptCardSellList;
        [SerializeField]
        private GameObject mConceptCardDetail;
        [SerializeField]
        private GameObject mConceptCardCheck;
        [Space(10f)]
        private ConceptCardDetailLevel mLevelObject;
        [HideInInspector]
        public ConceptCardListFilterWindow.Type FilterType;
        [HideInInspector]
        public bool ToggleSameSelectCard;
        [HideInInspector]
        public ConceptCardListSortWindow.Type SortType;
        [HideInInspector]
        public ConceptCardListSortWindow.Type SortOrderType;
        private OLong mSelectedUniqueID;
        private MultiConceptCard mSelectedMaterials;
        [HideInInspector]
        public int CostConceptCardRare;
        private List<SelecteConceptCardMaterial> mBulkSelectedMaterialList;
        private ConceptCardData mSelectedConceptCardMaterial;
        [CompilerGenerated]
        private static Func<ConceptCardData, bool> <>f__am$cache10;

        public ConceptCardManager()
        {
            this.mSelectedMaterials = new MultiConceptCard();
            this.mBulkSelectedMaterialList = new List<SelecteConceptCardMaterial>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <get_SelectedConceptCardData>m__2DB(ConceptCardData ccd)
        {
            return (ccd.UniqueID == this.mSelectedUniqueID);
        }

        [CompilerGenerated]
        private static bool <Init>m__2DC(ConceptCardData card)
        {
            return (card.Param.type == 1);
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch ((num - 10))
            {
                case 0:
                    goto Label_0047;

                case 1:
                    goto Label_007F;

                case 2:
                    goto Label_0063;

                case 3:
                    goto Label_009B;

                case 4:
                    goto Label_00B7;
            }
            if (num == null)
            {
                goto Label_0031;
            }
            if (num == 1)
            {
                goto Label_003C;
            }
            goto Label_00D3;
        Label_0031:
            this.Init();
            goto Label_00D3;
        Label_003C:
            this.ClearMaterials();
            goto Label_00D3;
        Label_0047:
            this.mLevelObject.StartLevelupAnimation(new ConceptCardDetailLevel.EffectCallBack(this.EnhanceAnimCallBack));
            goto Label_00D3;
        Label_0063:
            this.mLevelObject.StartAwakeAnimation(new ConceptCardDetailLevel.EffectCallBack(this.AwakeAnimCallBack));
            goto Label_00D3;
        Label_007F:
            this.mLevelObject.StartTrustMasterAnimation(new ConceptCardDetailLevel.EffectCallBack(this.TrustMasterAnimCallBack));
            goto Label_00D3;
        Label_009B:
            this.mLevelObject.StartGroupSkillPowerUpAnimation(new ConceptCardDetailLevel.EffectCallBack(this.GroupSkillPowerUpAnimCallBack));
            goto Label_00D3;
        Label_00B7:
            this.mLevelObject.StartGroupSkillMaxPowerUpAnimation(new ConceptCardDetailLevel.EffectCallBack(this.GroupSkillMaxPowerUpAnimCallBack));
        Label_00D3:
            return;
        }

        private void Awake()
        {
            _instance = this;
            this.LoadSortFilterData();
            return;
        }

        private void AwakeAnimCallBack()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x70);
            return;
        }

        public static unsafe void CalcTotalExpTrust(out int mixTotalExp, out int mixTrustExp, out int mixTotalAwakeLv)
        {
            ConceptCardData data;
            MultiConceptCard card;
            if ((Instance == null) == null)
            {
                goto Label_001A;
            }
            *((int*) mixTotalExp) = 0;
            *((int*) mixTrustExp) = 0;
            *((int*) mixTotalAwakeLv) = 0;
            return;
        Label_001A:
            data = Instance.SelectedConceptCardData;
            card = Instance.SelectedMaterials;
            CalcTotalExpTrust(data, card, mixTotalExp, mixTrustExp, mixTotalAwakeLv);
            return;
        }

        public static unsafe void CalcTotalExpTrust(ConceptCardData selectedCard, MultiConceptCard materials, out int mixTotalExp, out int mixTrustExp, out int mixTotalAwakeLv)
        {
            int num;
            ConceptCardData data;
            List<ConceptCardData>.Enumerator enumerator;
            num = 0;
            *((int*) mixTotalExp) = 0;
            *((int*) mixTrustExp) = 0;
            *((int*) mixTotalAwakeLv) = 0;
            enumerator = materials.GetList().GetEnumerator();
        Label_0018:
            try
            {
                goto Label_00C6;
            Label_001D:
                data = &enumerator.Current;
                *((int*) mixTotalExp) += data.MixExp;
                *((int*) mixTrustExp) += data.Param.en_trust;
                if (selectedCard == null)
                {
                    goto Label_0084;
                }
                if ((selectedCard.Param.iname == data.Param.iname) == null)
                {
                    goto Label_0084;
                }
                *((int*) mixTrustExp) += MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustPileUp;
            Label_0084:
                if (selectedCard == null)
                {
                    goto Label_00C6;
                }
                if ((selectedCard.Param.iname == data.Param.iname) == null)
                {
                    goto Label_00C6;
                }
                if ((selectedCard.AwakeCount + num) >= selectedCard.AwakeCountCap)
                {
                    goto Label_00C6;
                }
                num += 1;
            Label_00C6:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001D;
                }
                goto Label_00E3;
            }
            finally
            {
            Label_00D7:
                ((List<ConceptCardData>.Enumerator) enumerator).Dispose();
            }
        Label_00E3:
            *((int*) mixTotalAwakeLv) = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap * num;
            return;
        }

        public static unsafe void CalcTotalExpTrustMaterialData(out int mixTotalExp, out int mixTrustExp)
        {
            ConceptCardManager manager;
            SelecteConceptCardMaterial material;
            List<SelecteConceptCardMaterial>.Enumerator enumerator;
            *((int*) mixTotalExp) = 0;
            *((int*) mixTrustExp) = 0;
            manager = Instance;
            if ((manager == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            if (manager.BulkSelectedMaterialList.Count != null)
            {
                goto Label_002A;
            }
            return;
        Label_002A:
            enumerator = manager.BulkSelectedMaterialList.GetEnumerator();
        Label_0036:
            try
            {
                goto Label_0076;
            Label_003B:
                material = &enumerator.Current;
                *((int*) mixTotalExp) += material.mSelectedData.MixExp * material.mSelectNum;
                *((int*) mixTrustExp) += material.mSelectedData.Param.en_trust * material.mSelectNum;
            Label_0076:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_003B;
                }
                goto Label_0093;
            }
            finally
            {
            Label_0087:
                ((List<SelecteConceptCardMaterial>.Enumerator) enumerator).Dispose();
            }
        Label_0093:
            return;
        }

        private void CallConceptCardInit(GameObject obj)
        {
            ConceptCardList list;
            if ((obj == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            list = ((obj != null) == null) ? null : obj.GetComponent<ConceptCardList>();
            if ((list == null) == null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            list.Init();
            return;
        }

        private void ClearMaterials()
        {
            this.mSelectedMaterials.Clear();
            return;
        }

        private void EnhanceAnimCallBack()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 110);
            return;
        }

        public static unsafe void GalcTotalMixZeny(MultiConceptCard materials, out int totalMixZeny)
        {
            ConceptCardData data;
            List<ConceptCardData>.Enumerator enumerator;
            *((int*) totalMixZeny) = 0;
            enumerator = materials.GetList().GetEnumerator();
        Label_000F:
            try
            {
                goto Label_002C;
            Label_0014:
                data = &enumerator.Current;
                *((int*) totalMixZeny) += data.Param.en_cost;
            Label_002C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0014;
                }
                goto Label_0049;
            }
            finally
            {
            Label_003D:
                ((List<ConceptCardData>.Enumerator) enumerator).Dispose();
            }
        Label_0049:
            return;
        }

        public static unsafe void GalcTotalMixZenyMaterialData(out int totalMixZeny)
        {
            ConceptCardManager manager;
            SelecteConceptCardMaterial material;
            List<SelecteConceptCardMaterial>.Enumerator enumerator;
            *((int*) totalMixZeny) = 0;
            manager = Instance;
            if ((manager == null) == null)
            {
                goto Label_0016;
            }
            return;
        Label_0016:
            if (manager.BulkSelectedMaterialList.Count != null)
            {
                goto Label_0027;
            }
            return;
        Label_0027:
            enumerator = manager.BulkSelectedMaterialList.GetEnumerator();
        Label_0033:
            try
            {
                goto Label_005C;
            Label_0038:
                material = &enumerator.Current;
                *((int*) totalMixZeny) += material.mSelectedData.Param.en_cost * material.mSelectNum;
            Label_005C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0038;
                }
                goto Label_0079;
            }
            finally
            {
            Label_006D:
                ((List<SelecteConceptCardMaterial>.Enumerator) enumerator).Dispose();
            }
        Label_0079:
            return;
        }

        public static unsafe void GalcTotalSellZeny(MultiConceptCard materials, out int totalSellZeny)
        {
            ConceptCardData data;
            List<ConceptCardData>.Enumerator enumerator;
            *((int*) totalSellZeny) = 0;
            enumerator = materials.GetList().GetEnumerator();
        Label_000F:
            try
            {
                goto Label_0027;
            Label_0014:
                data = &enumerator.Current;
                *((int*) totalSellZeny) += data.SellGold;
            Label_0027:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0014;
                }
                goto Label_0044;
            }
            finally
            {
            Label_0038:
                ((List<ConceptCardData>.Enumerator) enumerator).Dispose();
            }
        Label_0044:
            return;
        }

        public unsafe void GetTotalExp(out int mixTotalExp, out int mixTrustExp)
        {
            ConceptCardData data;
            List<ConceptCardData>.Enumerator enumerator;
            *((int*) mixTotalExp) = 0;
            *((int*) mixTrustExp) = 0;
            enumerator = this.SelectedMaterials.GetList().GetEnumerator();
        Label_0017:
            try
            {
                goto Label_003F;
            Label_001C:
                data = &enumerator.Current;
                *((int*) mixTotalExp) += data.MixExp;
                *((int*) mixTrustExp) += data.Param.en_trust;
            Label_003F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001C;
                }
                goto Label_005C;
            }
            finally
            {
            Label_0050:
                ((List<ConceptCardData>.Enumerator) enumerator).Dispose();
            }
        Label_005C:
            return;
        }

        public static unsafe string GetWarningTextByMaterials(MultiConceptCard materials)
        {
            string str;
            bool flag;
            ConceptCardData data;
            List<ConceptCardData>.Enumerator enumerator;
            str = string.Empty;
            flag = 0;
            enumerator = materials.GetList().GetEnumerator();
        Label_0014:
            try
            {
                goto Label_0034;
            Label_0019:
                data = &enumerator.Current;
                if (data.Rarity < 3)
                {
                    goto Label_0034;
                }
                flag = 1;
            Label_0034:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0019;
                }
                goto Label_0051;
            }
            finally
            {
            Label_0045:
                ((List<ConceptCardData>.Enumerator) enumerator).Dispose();
            }
        Label_0051:
            if (flag == null)
            {
                goto Label_0062;
            }
            str = LocalizedText.Get("sys.CONCEPT_CARD_WARNING_HIGH_RARITY");
        Label_0062:
            return str;
        }

        private void GroupSkillMaxPowerUpAnimCallBack()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x72);
            return;
        }

        private void GroupSkillPowerUpAnimCallBack()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x71);
            return;
        }

        private void Init()
        {
            ConceptCardDetail detail;
            ConceptCardDescription description;
            this.CallConceptCardInit(this.mConceptCardBranceList);
            this.CallConceptCardInit(this.mConceptCardEnhanceList);
            this.CallConceptCardInit(this.mConceptCardSellList);
            this.CallConceptCardInit(this.mConceptCardDetail);
            detail = this.mConceptCardDetail.GetComponent<ConceptCardDetail>();
            detail.Init();
            description = detail.Description;
            this.mLevelObject = description.Level;
            this.CallConceptCardInit(this.mConceptCardCheck);
            MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardTrophyAll();
            if (<>f__am$cache10 != null)
            {
                goto Label_0097;
            }
            <>f__am$cache10 = new Func<ConceptCardData, bool>(ConceptCardManager.<Init>m__2DC);
        Label_0097:
            if (Enumerable.Any<ConceptCardData>(MonoSingleton<GameManager>.Instance.Player.ConceptCards, <>f__am$cache10) == null)
            {
                goto Label_00B1;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 200);
        Label_00B1:
            return;
        }

        public bool IsEqualsSelectedConceptCardData(ConceptCardData ccd)
        {
            ConceptCardData data;
            if (ccd != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            data = this.SelectedConceptCardData;
            if (data != null)
            {
                goto Label_0017;
            }
            return 0;
        Label_0017:
            return (ccd.UniqueID == data.UniqueID);
        }

        public void LoadSortFilterData()
        {
            this.FilterType = ConceptCardListFilterWindow.LoadData();
            this.SortType = ConceptCardListSortWindow.LoadDataType();
            this.SortOrderType = ConceptCardListSortWindow.LoadDataOrderType();
            return;
        }

        private void OnDestroy()
        {
            _instance = null;
            return;
        }

        public static unsafe string ParseTrustFormat(int trust)
        {
            int num;
            int num2;
            float num3;
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
            num2 = Mathf.Min(trust, num) / 10;
            num2 *= 10;
            num3 = ((float) num2) / 100f;
            return &num3.ToString("F1");
        }

        public unsafe void SetupBulkLevelupAnimation()
        {
            int num;
            int num2;
            CalcTotalExpTrustMaterialData(&num, &num2);
            this.mLevelObject.SetupLevelupAnimation(num, num2);
            return;
        }

        public unsafe void SetupLevelupAnimation()
        {
            int num;
            int num2;
            int num3;
            CalcTotalExpTrust(this.SelectedConceptCardData, this.SelectedMaterials, &num, &num2, &num3);
            this.mLevelObject.SetupLevelupAnimation(num, num2);
            return;
        }

        public static void SubstituteTrustFormat(ConceptCardData card, Text txt, int trust, bool notChangeColor)
        {
            string str;
            if ((txt == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (card != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            str = ParseTrustFormat(trust);
            txt.set_text(str);
            if (notChangeColor == null)
            {
                goto Label_0029;
            }
            return;
        Label_0029:
            if (trust < MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax)
            {
                goto Label_0063;
            }
            if (card.GetReward() == null)
            {
                goto Label_0063;
            }
            txt.set_color(Color.get_red());
            goto Label_006E;
        Label_0063:
            txt.set_color(Color.get_white());
        Label_006E:
            return;
        }

        private void TrustMasterAnimCallBack()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x6f);
            return;
        }

        public static ConceptCardManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public bool IsBranceListActive
        {
            get
            {
                return GameObjectExtensions.GetActive(this.mConceptCardBranceList);
            }
        }

        public bool IsEnhanceListActive
        {
            get
            {
                return GameObjectExtensions.GetActive(this.mConceptCardEnhanceList);
            }
        }

        public bool IsSellListActive
        {
            get
            {
                return GameObjectExtensions.GetActive(this.mConceptCardSellList);
            }
        }

        public bool IsDetailActive
        {
            get
            {
                return GameObjectExtensions.GetActive(this.mConceptCardDetail);
            }
        }

        public ConceptCardData SelectedConceptCardData
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.Player.ConceptCards.Find(new Predicate<ConceptCardData>(this.<get_SelectedConceptCardData>m__2DB));
            }
            set
            {
                this.mSelectedUniqueID = value.UniqueID;
                return;
            }
        }

        public ConceptCardData SelectedConceptCardMaterialData
        {
            get
            {
                return this.mSelectedConceptCardMaterial;
            }
            set
            {
                this.mSelectedConceptCardMaterial = value;
                return;
            }
        }

        public MultiConceptCard SelectedMaterials
        {
            get
            {
                return this.mSelectedMaterials;
            }
            set
            {
                this.mSelectedMaterials = value;
                return;
            }
        }

        public List<SelecteConceptCardMaterial> BulkSelectedMaterialList
        {
            get
            {
                return this.mBulkSelectedMaterialList;
            }
            set
            {
                this.mBulkSelectedMaterialList = value;
                return;
            }
        }
    }
}

