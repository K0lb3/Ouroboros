namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(2, "変更前のスキルを非表示", 0, 2), Pin(1, "変更前のスキルを表示", 0, 1)]
    public class ConceptCardDescription : MonoBehaviour, IFlowInterface
    {
        public const int INPUT_ENABLE_BASE_SKILL = 1;
        public const int INPUT_DIABLE_BASE_SKILL = 2;
        [SerializeField]
        private string PREFAB_PATH_BONUS_WINDOW;
        [SerializeField]
        public ConceptCardDetailLevel Level;
        [SerializeField]
        public ConceptCardDetailStatus Status;
        [SerializeField]
        public ConceptCardDetailSkin SkinPrefab;
        [SerializeField]
        private GameObject AbilityTemplate;
        [SerializeField]
        private GameObject mCardAbilityDescriptionPrefab;
        [SerializeField]
        private ConceptCardDetailGetUnit GetUnit;
        [SerializeField]
        private Selectable m_ShowBaseToggle;
        private GameObject mAbilityDetailParent;
        private GameObject mCardAbilityDescription;
        private List<ConceptCardDetailSkin> Skin;
        private List<AbilityDeriveList> m_AbilityDeriveList;
        private ConceptCardData mConceptCardData;
        private UnitData m_UnitData;
        [SerializeField]
        private Button OpenBonusButton;
        private GameObject mBonusWindow;
        private Canvas abilityCanvas;
        private Canvas bonusCanvas;
        private static ConceptCardDescription _instance;
        private bool mIsEnhance;
        private ConceptCardEnhanceInfo mEnhanceInfo;

        public ConceptCardDescription()
        {
            this.PREFAB_PATH_BONUS_WINDOW = "UI/ConceptCardLevelBonusDetail";
            this.Skin = new List<ConceptCardDetailSkin>();
            this.m_AbilityDeriveList = new List<AbilityDeriveList>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_0015;
            }
            if (num == 2)
            {
                goto Label_0021;
            }
            goto Label_002D;
        Label_0015:
            this.SwitchBaseSkillEnable(1);
            goto Label_002D;
        Label_0021:
            this.SwitchBaseSkillEnable(0);
        Label_002D:
            return;
        }

        private void Awake()
        {
            _instance = this;
            return;
        }

        private AbilityDeriveList CreateAbilityListItem()
        {
            GameObject obj2;
            obj2 = Object.Instantiate<GameObject>(this.AbilityTemplate);
            obj2.get_gameObject().SetActive(1);
            obj2.get_transform().SetParent(this.AbilityTemplate.get_transform().get_parent(), 0);
            return obj2.GetComponent<AbilityDeriveList>();
        }

        public void CreatePrefab(GameObject ability_detail_parent)
        {
            this.CreateSkinPrefab();
            this.CreateSkillPrefab(ability_detail_parent);
            return;
        }

        public void CreateSkillPrefab(GameObject ability_detail_parent)
        {
            List<ConceptCardSkillDatailData> list;
            int num;
            int num2;
            ConceptCardSkillDatailData data;
            AbilityDeriveList list2;
            list = this.mConceptCardData.GetAbilityDatailData();
            this.mAbilityDetailParent = ability_detail_parent;
            if ((this.m_ShowBaseToggle != null) == null)
            {
                goto Label_0030;
            }
            this.m_ShowBaseToggle.set_interactable(0);
        Label_0030:
            num = 0;
            goto Label_0052;
        Label_0037:
            this.m_AbilityDeriveList[num].get_gameObject().SetActive(0);
            num += 1;
        Label_0052:
            if (num < this.m_AbilityDeriveList.Count)
            {
                goto Label_0037;
            }
            num2 = 0;
            goto Label_00FE;
        Label_006A:
            data = list[num2];
            list2 = null;
            if (num2 >= this.m_AbilityDeriveList.Count)
            {
                goto Label_0099;
            }
            list2 = this.m_AbilityDeriveList[num2];
            goto Label_00AE;
        Label_0099:
            list2 = this.CreateAbilityListItem();
            this.m_AbilityDeriveList.Add(list2);
        Label_00AE:
            list2.SetupWithConceptCard(data, this.m_UnitData, new ConceptCardDetailAbility.ClickDetail(this.OnClickDetail));
            if ((this.m_ShowBaseToggle != null) == null)
            {
                goto Label_00F2;
            }
            this.m_ShowBaseToggle.set_interactable(this.m_ShowBaseToggle.get_interactable() | list2.HasDerive);
        Label_00F2:
            GameUtility.SetGameObjectActive(list2, 1);
            num2 += 1;
        Label_00FE:
            if (num2 < list.Count)
            {
                goto Label_006A;
            }
            GameUtility.SetGameObjectActive(this.AbilityTemplate, 0);
            this.AbilityTemplate.get_gameObject().SetActive(0);
            return;
        }

        public void CreateSkinPrefab()
        {
            List<ConceptCardEquipEffect> list;
            int num;
            int num2;
            ConceptCardEquipEffect effect;
            ConceptCardDetailSkin skin;
            int num3;
            list = this.mConceptCardData.EquipEffects;
            num = 0;
            if (list == null)
            {
                goto Label_00BF;
            }
            num2 = 0;
            goto Label_00B3;
        Label_001B:
            effect = list[num2];
            if (string.IsNullOrEmpty(effect.Skin) != null)
            {
                goto Label_00AF;
            }
            skin = null;
            if (this.Skin.Count > num)
            {
                goto Label_0066;
            }
            skin = Object.Instantiate<ConceptCardDetailSkin>(this.SkinPrefab);
            this.Skin.Add(skin);
            goto Label_0074;
        Label_0066:
            skin = this.Skin[num];
        Label_0074:
            skin.get_gameObject().SetActive(1);
            skin.get_transform().SetParent(this.SkinPrefab.get_transform().get_parent(), 0);
            skin.SetEquipEffect(effect);
            num += 1;
        Label_00AF:
            num2 += 1;
        Label_00B3:
            if (num2 < list.Count)
            {
                goto Label_001B;
            }
        Label_00BF:
            num3 = num;
            goto Label_00E5;
        Label_00C7:
            this.Skin[num3].get_gameObject().SetActive(0);
            num3 += 1;
        Label_00E5:
            if (num3 < this.Skin.Count)
            {
                goto Label_00C7;
            }
            return;
        }

        public void OnClickDetail(ConceptCardSkillDatailData data)
        {
            DestroyEventListener local1;
            DestroyEventListener listener;
            if ((this.mAbilityDetailParent == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.mCardAbilityDescription != null) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            this.mCardAbilityDescription = Object.Instantiate<GameObject>(this.mCardAbilityDescriptionPrefab);
            DataSource.Bind<ConceptCardSkillDatailData>(this.mCardAbilityDescription, data);
            this.abilityCanvas = UIUtility.PushCanvas(0, -1);
            this.mCardAbilityDescription.get_transform().SetParent(this.abilityCanvas.get_transform(), 0);
            local1 = this.mCardAbilityDescription.AddComponent<DestroyEventListener>();
            local1.Listeners = (DestroyEventListener.DestroyEvent) Delegate.Combine(local1.Listeners, new DestroyEventListener.DestroyEvent(this.OnDestroyAbilityDescription));
            return;
        }

        public void OnClickOpenBonusButton()
        {
            if (this.mConceptCardData != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            base.StartCoroutine(this.OpenBonusWindow(this.mConceptCardData));
            return;
        }

        private void OnDestoryBonusWindow(GameObject obj)
        {
            if ((this.bonusCanvas == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            Object.Destroy(this.bonusCanvas.get_gameObject());
            this.bonusCanvas = null;
            return;
        }

        private void OnDestroy()
        {
            if ((this.abilityCanvas != null) == null)
            {
                goto Label_0028;
            }
            Object.Destroy(this.abilityCanvas.get_gameObject());
            this.abilityCanvas = null;
        Label_0028:
            return;
        }

        private void OnDestroyAbilityDescription(GameObject go)
        {
            if ((this.abilityCanvas != null) == null)
            {
                goto Label_0028;
            }
            Object.Destroy(this.abilityCanvas.get_gameObject());
            this.abilityCanvas = null;
        Label_0028:
            return;
        }

        [DebuggerHidden]
        private IEnumerator OpenBonusWindow(ConceptCardData concept_card)
        {
            <OpenBonusWindow>c__IteratorF4 rf;
            rf = new <OpenBonusWindow>c__IteratorF4();
            rf.concept_card = concept_card;
            rf.<$>concept_card = concept_card;
            rf.<>f__this = this;
            return rf;
        }

        private void Refresh()
        {
            int num;
            Scrollbar[] scrollbarArray;
            Scrollbar scrollbar;
            Scrollbar[] scrollbarArray2;
            int num2;
            if (this.mConceptCardData != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.Refresh(this.Level);
            this.Refresh(this.Status);
            if (this.Skin == null)
            {
                goto Label_005D;
            }
            num = 0;
            goto Label_004C;
        Label_0036:
            this.Refresh(this.Skin[num]);
            num += 1;
        Label_004C:
            if (num < this.Skin.Count)
            {
                goto Label_0036;
            }
        Label_005D:
            if ((this.GetUnit != null) == null)
            {
                goto Label_007A;
            }
            this.Refresh(this.GetUnit);
        Label_007A:
            scrollbarArray2 = base.GetComponentsInChildren<Scrollbar>();
            num2 = 0;
            goto Label_00A1;
        Label_008B:
            scrollbar = scrollbarArray2[num2];
            scrollbar.set_value(1f);
            num2 += 1;
        Label_00A1:
            if (num2 < ((int) scrollbarArray2.Length))
            {
                goto Label_008B;
            }
            return;
        }

        public void Refresh(ConceptCardDetailBase concept)
        {
            if ((concept != null) == null)
            {
                goto Label_0012;
            }
            concept.Refresh();
        Label_0012:
            return;
        }

        public unsafe void SetConceptCardData(ConceptCardData data, GameObject ability_detail_parent, bool bEnhance, bool is_first_get_unit, UnitData unitData)
        {
            int num;
            int num2;
            int num3;
            int num4;
            bool flag;
            this.mConceptCardData = data;
            if (this.mConceptCardData != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            this.m_UnitData = unitData;
            this.mIsEnhance = bEnhance;
            ConceptCardManager.CalcTotalExpTrust(&num, &num2, &num3);
            this.mEnhanceInfo = new ConceptCardEnhanceInfo(data, num, num2, num3);
            this.CreatePrefab(ability_detail_parent);
            this.SetParam(this.Level, this.mConceptCardData, num, num2, num3);
            this.SetParam(this.Status, this.mConceptCardData, num, num2, num3);
            if (this.Skin == null)
            {
                goto Label_00AC;
            }
            num4 = 0;
            goto Label_009B;
        Label_007F:
            this.SetParam(this.Skin[num4], this.mConceptCardData);
            num4 += 1;
        Label_009B:
            if (num4 < this.Skin.Count)
            {
                goto Label_007F;
            }
        Label_00AC:
            if ((this.GetUnit != null) == null)
            {
                goto Label_00E7;
            }
            if (is_first_get_unit != null)
            {
                goto Label_00D5;
            }
            this.GetUnit.get_gameObject().SetActive(0);
        Label_00D5:
            this.SetParam(this.GetUnit, this.mConceptCardData);
        Label_00E7:
            if ((this.OpenBonusButton != null) == null)
            {
                goto Label_013F;
            }
            flag = this.mConceptCardData.IsEnableAwake;
            if (this.mConceptCardData.Param.IsExistAddCardSkillBuffAwake() != null)
            {
                goto Label_0132;
            }
            if (this.mConceptCardData.Param.IsExistAddCardSkillBuffLvMax() != null)
            {
                goto Label_0132;
            }
            flag = 0;
        Label_0132:
            this.OpenBonusButton.set_interactable(flag);
        Label_013F:
            this.Refresh();
            return;
        }

        public void SetParam(ConceptCardDetailBase concept, ConceptCardData data)
        {
            if ((concept != null) == null)
            {
                goto Label_0013;
            }
            concept.SetParam(data);
        Label_0013:
            return;
        }

        public void SetParam(ConceptCardDetailBase concept, ConceptCardData data, int addExp, int addTrust, int addAwakeLv)
        {
            if ((concept != null) == null)
            {
                goto Label_0018;
            }
            concept.SetParam(data, addExp, addTrust, addAwakeLv);
        Label_0018:
            return;
        }

        private unsafe void SwitchBaseSkillEnable(bool enable)
        {
            AbilityDeriveList list;
            List<AbilityDeriveList>.Enumerator enumerator;
            enumerator = this.m_AbilityDeriveList.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0020;
            Label_0011:
                list = &enumerator.Current;
                list.SwitchBaseAbilityEnable(enable);
            Label_0020:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_003D;
            }
            finally
            {
            Label_0031:
                ((List<AbilityDeriveList>.Enumerator) enumerator).Dispose();
            }
        Label_003D:
            return;
        }

        public static bool IsEnhance
        {
            get
            {
                if ((_instance != null) == null)
                {
                    goto Label_001B;
                }
                return _instance.mIsEnhance;
            Label_001B:
                return 0;
            }
        }

        public static ConceptCardEnhanceInfo EnhanceInfo
        {
            get
            {
                if ((_instance != null) == null)
                {
                    goto Label_0025;
                }
                if (IsEnhance == null)
                {
                    goto Label_0025;
                }
                return _instance.mEnhanceInfo;
            Label_0025:
                return null;
            }
        }

        [CompilerGenerated]
        private sealed class <OpenBonusWindow>c__IteratorF4 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <request>__0;
            internal ConceptCardData concept_card;
            internal bool <is_level_max>__1;
            internal ConceptCardBonusWindow <bonus_window>__2;
            internal DestroyEventListener <del>__3;
            internal int $PC;
            internal object $current;
            internal ConceptCardData <$>concept_card;
            internal ConceptCardDescription <>f__this;

            public <OpenBonusWindow>c__IteratorF4()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_006F;
                }
                goto Label_0193;
            Label_0021:
                this.<request>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.PREFAB_PATH_BONUS_WINDOW);
                if (this.<request>__0 == null)
                {
                    goto Label_006F;
                }
                if (this.<request>__0.isDone != null)
                {
                    goto Label_006F;
                }
                this.$current = this.<request>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_0195;
            Label_006F:
                if ((this.<request>__0.asset != null) == null)
                {
                    goto Label_018C;
                }
                this.<is_level_max>__1 = (this.concept_card.Lv < this.concept_card.LvCap) == 0;
                this.<>f__this.mBonusWindow = Object.Instantiate(this.<request>__0.asset) as GameObject;
                this.<>f__this.bonusCanvas = UIUtility.PushCanvas(0, -1);
                this.<>f__this.mBonusWindow.get_transform().SetParent(this.<>f__this.bonusCanvas.get_transform(), 0);
                this.<bonus_window>__2 = this.<>f__this.mBonusWindow.GetComponent<ConceptCardBonusWindow>();
                this.<bonus_window>__2.Initailize(this.concept_card.Param, this.concept_card.AwakeCount, this.<is_level_max>__1);
                this.<del>__3 = this.<>f__this.mBonusWindow.AddComponent<DestroyEventListener>();
                this.<del>__3.Listeners = (DestroyEventListener.DestroyEvent) Delegate.Combine(this.<del>__3.Listeners, new DestroyEventListener.DestroyEvent(this.<>f__this.OnDestoryBonusWindow));
            Label_018C:
                this.$PC = -1;
            Label_0193:
                return 0;
            Label_0195:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public class ConceptCardEnhanceInfo
        {
            private ConceptCardData target;
            private int add_exp;
            private int add_trust;
            private int add_awake_lv;
            private int predict_lv;
            private int predict_awake;

            public ConceptCardEnhanceInfo(ConceptCardData _concept_card, int _add_exp, int _add_trust, int _add_awake_lv)
            {
                int num;
                int num2;
                int num3;
                base..ctor();
                this.target = _concept_card;
                this.add_exp = _add_exp;
                this.add_trust = _add_trust;
                this.add_awake_lv = _add_awake_lv;
                num = this.target.Exp + this.add_exp;
                num2 = Mathf.Min(this.target.LvCap, this.target.CurrentLvCap + this.add_awake_lv);
                this.predict_lv = MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel(this.target.Rarity, num, num2);
                num3 = this.add_awake_lv / MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
                this.predict_awake = Mathf.Min(this.target.AwakeCountCap, this.target.AwakeCount + num3);
                return;
            }

            public void Clear()
            {
                this.target = null;
                this.add_exp = 0;
                this.add_trust = 0;
                this.add_awake_lv = 0;
                this.predict_lv = 0;
                return;
            }

            public ConceptCardData Target
            {
                get
                {
                    return this.target;
                }
            }

            public int addExp
            {
                get
                {
                    return this.add_exp;
                }
            }

            public int addTrust
            {
                get
                {
                    return this.add_trust;
                }
            }

            public int addAwakeLv
            {
                get
                {
                    return this.add_awake_lv;
                }
            }

            public int predictLv
            {
                get
                {
                    return this.predict_lv;
                }
            }

            public int predictAwake
            {
                get
                {
                    return this.predict_awake;
                }
            }
        }
    }
}

