namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(12, "扉の強化リクエスト", 1, 0x1f), Pin(11, "扉の開放リクエスト", 1, 0x15), Pin(2, "扉の開放ボタンクリック", 0, 20), Pin(4, "扉の開放条件詳細クリック", 0, 40), Pin(5, "扉の強化成功", 0, 50), Pin(6, "扉の解放成功", 0, 60), Pin(1, "扉選択", 0, 10), Pin(7, "扉の強化画面を閉じる", 0, 70), Pin(0, "表示更新", 0, 0), Pin(3, "扉の強化ボタンクリック", 0, 30)]
    public class UnitTobiraInventory : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_TOBIRA_REFRESH = 0;
        private const int INPUT_TOBIRA_SELECT = 1;
        private const int INPUT_TOBIRA_OPEN = 2;
        private const int INPUT_TOBIRA_ENHANCE = 3;
        private const int INPUT_TOBIRA_OPEN_DETAIL = 4;
        private const int INPUT_TOBIRA_SUCCESS_ENHANCE = 5;
        private const int INPUT_TOBIRA_SUCCESS_OPEN = 6;
        private const int INPUT_TOBIRA_CLOSE = 7;
        private const int OUTPUT_TOBIRA_OPEN = 11;
        private const int OUTPUT_TOBIRA_ENHANCE = 12;
        [SerializeField]
        private string PREFAB_PATH_TOBIRA_ENHANCE_WINDOW;
        [SerializeField]
        private GameObject UnitAttachTarget;
        [SerializeField]
        private GameObject TobiraTemplate;
        [SerializeField]
        private UnitTobiraParamWindow TobiraParamWindow;
        private UnitData mCurrentUnit;
        private TobiraData mCurrentTobira;
        private List<UnitTobiraItem> mTobiraList;
        private UnitTobiraEnhanceWindow mTobiraEnhanceWindow;
        private TobiraParam.Category mSelectedTobiraCategory;
        private static long mInitTimeUniqueID;
        [CompilerGenerated]
        private static Action<UnitTobiraItem> <>f__am$cacheA;
        [CompilerGenerated]
        private static Predicate<TobiraData> <>f__am$cacheB;

        public UnitTobiraInventory()
        {
            this.PREFAB_PATH_TOBIRA_ENHANCE_WINDOW = "UI/TobiraEnhanceItemWindow";
            this.mTobiraList = new List<UnitTobiraItem>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <RefreshSelected>m__483(TobiraData tobira)
        {
            return (tobira.Param.TobiraCategory == GlobalVars.PreBattleUnitTobiraCategory);
        }

        [CompilerGenerated]
        private void <RefreshSelected>m__484(UnitTobiraItem tobira)
        {
            tobira.Select(tobira.Category == this.mCurrentTobira.Param.TobiraCategory);
            return;
        }

        [CompilerGenerated]
        private static void <RefreshStatus>m__482(UnitTobiraItem tobira)
        {
            Object.Destroy(tobira.get_gameObject());
            return;
        }

        public void Activated(int pinID)
        {
            SerializeValueList list;
            int num;
            <Activated>c__AnonStorey3DF storeydf;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_0038;

                case 2:
                    goto Label_010D;

                case 3:
                    goto Label_0118;

                case 4:
                    goto Label_0158;

                case 5:
                    goto Label_0123;

                case 6:
                    goto Label_0135;

                case 7:
                    goto Label_0147;
            }
            goto Label_0158;
        Label_002D:
            this.RefreshAll();
            goto Label_0158;
        Label_0038:
            storeydf = new <Activated>c__AnonStorey3DF();
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list != null)
            {
                goto Label_0054;
            }
            goto Label_0158;
        Label_0054:
            storeydf.item = list.GetComponent<UnitTobiraItem>("_self");
            if ((storeydf.item == null) == null)
            {
                goto Label_007B;
            }
            goto Label_0158;
        Label_007B:
            this.mSelectedTobiraCategory = storeydf.item.Category;
            GlobalVars.PreBattleUnitTobiraCategory.Set(this.mSelectedTobiraCategory);
            this.mCurrentTobira = this.mCurrentUnit.TobiraData.Find(new Predicate<TobiraData>(storeydf.<>m__485));
            this.mTobiraList.ForEach(new Action<UnitTobiraItem>(storeydf.<>m__486));
            this.TobiraParamWindow.Refresh(this.mCurrentUnit, this.mCurrentTobira, storeydf.item.Param);
            UnitEnhanceV3.Instance.RefreshTobiraBgAnimation(this.mCurrentTobira, 0);
            goto Label_0158;
        Label_010D:
            this.OnTobiraOpenBtn();
            goto Label_0158;
        Label_0118:
            this.OnTobiraEnhanceBtn();
            goto Label_0158;
        Label_0123:
            this.RefreshCurrentUnit();
            this.SuccessTobiraEnhance();
            goto Label_0158;
        Label_0135:
            this.RefreshCurrentUnit();
            this.SuccessTobiraOpen();
            goto Label_0158;
        Label_0147:
            UnitEnhanceV3.Instance.TobiraUIActive(0, 0);
        Label_0158:
            return;
        }

        public void Init(bool is_restore)
        {
            bool flag;
            <Init>c__AnonStorey3DE storeyde;
            if ((this.UnitAttachTarget != null) == null)
            {
                goto Label_001F;
            }
            if (this.RefreshCurrentUnit() != null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            GlobalVars.SelectedEquipmentSlot.Set(-1);
            this.RefreshStatus();
            if (is_restore == null)
            {
                goto Label_0047;
            }
            this.RefreshSelected();
            this.OnTobiraEnhanceBtn();
            goto Label_00F2;
        Label_0047:
            if (this.mTobiraList.Count <= 0)
            {
                goto Label_00F2;
            }
            storeyde = new <Init>c__AnonStorey3DE();
            storeyde.default_category = this.mTobiraList[0].Category;
            this.mSelectedTobiraCategory = storeyde.default_category;
            GlobalVars.PreBattleUnitTobiraCategory.Set(storeyde.default_category);
            this.mTobiraList.ForEach(new Action<UnitTobiraItem>(storeyde.<>m__480));
            this.mCurrentTobira = this.mCurrentUnit.TobiraData.Find(new Predicate<TobiraData>(storeyde.<>m__481));
            this.TobiraParamWindow.Refresh(this.mCurrentUnit, this.mCurrentTobira, this.mTobiraList[0].Param);
        Label_00F2:
            UnitEnhanceV3.Instance.RefreshTobiraBgAnimation(this.mCurrentTobira, 1);
            mInitTimeUniqueID = GlobalVars.SelectedUnitUniqueID;
            return;
        }

        private void OnDestroy()
        {
            if ((this.mTobiraEnhanceWindow != null) == null)
            {
                goto Label_0028;
            }
            Object.Destroy(this.mTobiraEnhanceWindow.get_gameObject());
            this.mTobiraEnhanceWindow = null;
        Label_0028:
            return;
        }

        private void OnTobiraEnhanceBtn()
        {
            GameObject obj2;
            GameObject obj3;
            this.ResetSelectedUnitUniqueID();
            obj2 = AssetManager.Load<GameObject>(this.PREFAB_PATH_TOBIRA_ENHANCE_WINDOW);
            if ((obj2 != null) == null)
            {
                goto Label_005F;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            this.mTobiraEnhanceWindow = obj3.GetComponent<UnitTobiraEnhanceWindow>();
            this.mTobiraEnhanceWindow.Initialize(this.mCurrentUnit, this.mCurrentTobira);
            this.mTobiraEnhanceWindow.OnCallback = new UnitTobiraEnhanceWindow.CallbackEvent(this.RequestTobiraEnhance);
        Label_005F:
            return;
        }

        private void OnTobiraOpenBtn()
        {
            if ((UnitEnhanceV3.Instance != null) == null)
            {
                goto Label_001A;
            }
            UnitEnhanceV3.Instance.BeginStatusChangeCheck();
        Label_001A:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            return;
        }

        [DebuggerHidden]
        private IEnumerator PlayLevelupEffect()
        {
            <PlayLevelupEffect>c__Iterator179 iterator;
            iterator = new <PlayLevelupEffect>c__Iterator179();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator PlayOpenEffect()
        {
            <PlayOpenEffect>c__Iterator178 iterator;
            iterator = new <PlayOpenEffect>c__Iterator178();
            iterator.<>f__this = this;
            return iterator;
        }

        private void RefreshAll()
        {
            this.RefreshStatus();
            this.RefreshSelected();
            UnitEnhanceV3.Instance.RefreshTobiraBgAnimation(this.mCurrentTobira, 0);
            return;
        }

        private bool RefreshCurrentUnit()
        {
            UnitData data;
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            if (data == null)
            {
                goto Label_0039;
            }
            this.mCurrentUnit = new UnitData();
            this.mCurrentUnit.Setup(data);
            return 1;
        Label_0039:
            return 0;
        }

        private void RefreshSelected()
        {
            if (<>f__am$cacheB != null)
            {
                goto Label_0024;
            }
            <>f__am$cacheB = new Predicate<TobiraData>(UnitTobiraInventory.<RefreshSelected>m__483);
        Label_0024:
            this.mCurrentTobira = this.mCurrentUnit.TobiraData.Find(<>f__am$cacheB);
            this.mTobiraList.ForEach(new Action<UnitTobiraItem>(this.<RefreshSelected>m__484));
            this.TobiraParamWindow.Refresh(this.mCurrentUnit, this.mCurrentTobira, this.mCurrentTobira.Param);
            return;
        }

        private void RefreshStatus()
        {
            int num;
            GameObject obj2;
            UnitTobiraItem item;
            if (this.mCurrentUnit == null)
            {
                goto Label_001C;
            }
            if ((this.TobiraTemplate == null) == null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            DataSource.Bind<UnitData>(this.UnitAttachTarget, this.mCurrentUnit);
            GameParameter.UpdateAll(this.UnitAttachTarget);
            this.TobiraTemplate.SetActive(0);
            if (<>f__am$cacheA != null)
            {
                goto Label_0063;
            }
            <>f__am$cacheA = new Action<UnitTobiraItem>(UnitTobiraInventory.<RefreshStatus>m__482);
        Label_0063:
            this.mTobiraList.ForEach(<>f__am$cacheA);
            this.mTobiraList.Clear();
            num = 1;
            goto Label_00E3;
        Label_007F:
            obj2 = Object.Instantiate<GameObject>(this.TobiraTemplate);
            obj2.get_transform().SetParent(this.TobiraTemplate.get_transform().get_parent(), 0);
            obj2.SetActive(1);
            item = obj2.GetComponent<UnitTobiraItem>();
            if ((item == null) == null)
            {
                goto Label_00C6;
            }
            goto Label_00DF;
        Label_00C6:
            item.Initialize(this.mCurrentUnit, num);
            this.mTobiraList.Add(item);
        Label_00DF:
            num += 1;
        Label_00E3:
            if (num < 8)
            {
                goto Label_007F;
            }
            return;
        }

        private void RequestTobiraEnhance()
        {
            this.ResetSelectedUnitUniqueID();
            if ((UnitEnhanceV3.Instance != null) == null)
            {
                goto Label_0020;
            }
            UnitEnhanceV3.Instance.BeginStatusChangeCheck();
        Label_0020:
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
            return;
        }

        private void ResetSelectedUnitUniqueID()
        {
            if (this.mCurrentUnit != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            GlobalVars.SelectedUnitUniqueID.Set(mInitTimeUniqueID);
            return;
        }

        private void SuccessTobiraEnhance()
        {
            base.StartCoroutine(this.PlayLevelupEffect());
            return;
        }

        private void SuccessTobiraOpen()
        {
            base.StartCoroutine(this.PlayOpenEffect());
            return;
        }

        public static long InitTimeUniqueID
        {
            get
            {
                return mInitTimeUniqueID;
            }
        }

        [CompilerGenerated]
        private sealed class <Activated>c__AnonStorey3DF
        {
            internal UnitTobiraItem item;

            public <Activated>c__AnonStorey3DF()
            {
                base..ctor();
                return;
            }

            internal bool <>m__485(TobiraData tobira)
            {
                return (tobira.Param.TobiraCategory == this.item.Category);
            }

            internal void <>m__486(UnitTobiraItem tobira)
            {
                tobira.Select(tobira.Category == this.item.Category);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Init>c__AnonStorey3DE
        {
            internal TobiraParam.Category default_category;

            public <Init>c__AnonStorey3DE()
            {
                base..ctor();
                return;
            }

            internal void <>m__480(UnitTobiraItem tobira)
            {
                tobira.Select(tobira.Category == this.default_category);
                return;
            }

            internal bool <>m__481(TobiraData tobira)
            {
                return (tobira.Param.TobiraCategory == this.default_category);
            }
        }

        [CompilerGenerated]
        private sealed class <PlayLevelupEffect>c__Iterator179 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitTobiraInventory <>f__this;

            public <PlayLevelupEffect>c__Iterator179()
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
                        goto Label_005E;
                }
                goto Label_0065;
            Label_0021:
                this.<>f__this.RefreshAll();
                this.$current = this.<>f__this.StartCoroutine(UnitEnhanceV3.Instance.PlayTobiraLevelupEffect(this.<>f__this.mCurrentTobira));
                this.$PC = 1;
                goto Label_0067;
            Label_005E:
                this.$PC = -1;
            Label_0065:
                return 0;
            Label_0067:
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

        [CompilerGenerated]
        private sealed class <PlayOpenEffect>c__Iterator178 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitTobiraInventory <>f__this;

            public <PlayOpenEffect>c__Iterator178()
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
                        goto Label_005E;
                }
                goto Label_0065;
            Label_0021:
                this.<>f__this.RefreshAll();
                this.$current = this.<>f__this.StartCoroutine(UnitEnhanceV3.Instance.PlayTobiraOpenEffect(this.<>f__this.mSelectedTobiraCategory));
                this.$PC = 1;
                goto Label_0067;
            Label_005E:
                this.$PC = -1;
            Label_0065:
                return 0;
            Label_0067:
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
    }
}

