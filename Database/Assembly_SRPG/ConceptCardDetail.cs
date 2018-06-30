namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(10, "通常パラメータ表示", 0, 10), Pin(0x66, "未受取トラストマスター達成", 1, 0x66), Pin(12, "未受取トラストマスター達成", 0, 12), Pin(11, "強化パラメータ表示", 0, 11), Pin(13, "一括強化後の処理", 0, 13)]
    public class ConceptCardDetail : MonoBehaviour, IFlowInterface
    {
        public const int PIN_REFRESH_PARAM = 10;
        public const int PIN_REFRESH_ENH_PARAM = 11;
        public const int PIN_TRUSTMASTER_START = 12;
        public const int PIN_ENHANCE_BULK_CHECK = 13;
        public const int PIN_TRUSTMASTER_END = 0x66;
        [SerializeField]
        private RawImage mIllustImage;
        [SerializeField]
        private ImageArray mIllustFrame;
        [SerializeField]
        private Text mCardNameText;
        [SerializeField]
        private Text mFlavorText;
        [SerializeField]
        private Toggle mFavoriteToggle;
        [SerializeField]
        private Button EnhanceButton;
        [SerializeField]
        private Button EnhanceExecButton;
        [SerializeField]
        private StarGauge mStarGauge;
        private ConceptCardDescription mConceptCardDescription;
        [SerializeField]
        private GameObject mConceptCardDescriptionPrefab;
        [SerializeField]
        private Transform mConceptCardDescriptionParent;
        [SerializeField]
        private Button EnhanceBulkButton;
        private ConceptCardData mConceptCardData;

        public ConceptCardDetail()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch ((num - 10))
            {
                case 0:
                    goto Label_0020;

                case 1:
                    goto Label_0032;

                case 2:
                    goto Label_003E;

                case 3:
                    goto Label_0056;
            }
            goto Label_0062;
        Label_0020:
            this.SetParam(0);
            this.CheckTrsutMaster();
            goto Label_0062;
        Label_0032:
            this.SetParam(1);
            goto Label_0062;
        Label_003E:
            base.StartCoroutine(this.TrustMasterUpdate(this.mConceptCardData));
            goto Label_0062;
        Label_0056:
            this.SetParam(0);
        Label_0062:
            return;
        }

        public void CheckTrsutMaster()
        {
            ConceptCardManager manager;
            if (this.mConceptCardData != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mConceptCardData.Trust < MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax)
            {
                goto Label_0078;
            }
            if (this.mConceptCardData.TrustBonus != null)
            {
                goto Label_0078;
            }
            if (this.mConceptCardData.GetReward() == null)
            {
                goto Label_0078;
            }
            manager = base.GetComponentInParent<ConceptCardManager>();
            if ((manager != null) == null)
            {
                goto Label_0078;
            }
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(manager, "TRUST_MASTER");
        Label_0078:
            return;
        }

        public void Init()
        {
            GameObject obj2;
            obj2 = Object.Instantiate<GameObject>(this.mConceptCardDescriptionPrefab);
            obj2.get_transform().SetParent(this.mConceptCardDescriptionParent, 0);
            this.mConceptCardDescription = obj2.GetComponentInChildren<ConceptCardDescription>();
            return;
        }

        private void Refresh()
        {
            string str;
            string str2;
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
            if ((this.mIllustImage != null) == null)
            {
                goto Label_0061;
            }
            str = AssetPath.ConceptCard(this.mConceptCardData.Param);
            str2 = Path.GetFileName(str);
            if ((this.mIllustImage.get_mainTexture().get_name() != str2) == null)
            {
                goto Label_0061;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mIllustImage, str);
        Label_0061:
            if ((this.mIllustFrame != null) == null)
            {
                goto Label_00AB;
            }
            num = Mathf.Min(Mathf.Max(this.mConceptCardData.Rarity, 0), ((int) this.mIllustFrame.Images.Length) - 1);
            this.mIllustFrame.ImageIndex = num;
        Label_00AB:
            this.SetText(this.mCardNameText, this.mConceptCardData.Param.name);
            this.SetFlavorTextText();
            this.SetFavoriteToggle(this.mConceptCardData.Favorite);
            if ((this.mStarGauge != null) == null)
            {
                goto Label_0129;
            }
            this.mStarGauge.Max = this.mConceptCardData.Rarity + 1;
            this.mStarGauge.Value = this.mConceptCardData.Rarity + 1;
        Label_0129:
            scrollbarArray2 = base.GetComponentsInChildren<Scrollbar>();
            num2 = 0;
            goto Label_0154;
        Label_013B:
            scrollbar = scrollbarArray2[num2];
            scrollbar.set_value(1f);
            num2 += 1;
        Label_0154:
            if (num2 < ((int) scrollbarArray2.Length))
            {
                goto Label_013B;
            }
            return;
        }

        public void RefreshEnhanceBulkButton()
        {
            bool flag;
            ConceptCardManager manager;
            bool flag2;
            bool flag3;
            if ((this.EnhanceBulkButton == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            flag = 1;
            if (MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardExpMaterial() != null)
            {
                goto Label_003E;
            }
            if (MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardTrustMaterial() != null)
            {
                goto Label_003E;
            }
            flag = 0;
        Label_003E:
            manager = base.GetComponentInParent<ConceptCardManager>();
            if ((manager != null) == null)
            {
                goto Label_00FD;
            }
            if (manager.SelectedConceptCardData == null)
            {
                goto Label_00FD;
            }
            flag2 = 0;
            flag3 = 0;
            if (manager.SelectedConceptCardData.Lv == manager.SelectedConceptCardData.CurrentLvCap)
            {
                goto Label_0099;
            }
            if (MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardExpMaterial() != null)
            {
                goto Label_009B;
            }
        Label_0099:
            flag2 = 1;
        Label_009B:
            if (manager.SelectedConceptCardData.GetReward() == null)
            {
                goto Label_00ED;
            }
            if (manager.SelectedConceptCardData.Trust == MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax)
            {
                goto Label_00ED;
            }
            if (MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardTrustMaterial() != null)
            {
                goto Label_00EF;
            }
        Label_00ED:
            flag3 = 1;
        Label_00EF:
            if (flag2 == null)
            {
                goto Label_00FD;
            }
            if (flag3 == null)
            {
                goto Label_00FD;
            }
            flag = 0;
        Label_00FD:
            this.EnhanceBulkButton.set_interactable(flag);
            return;
        }

        public void RefreshEnhanceButton()
        {
            bool flag;
            if ((this.EnhanceButton == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            flag = 1;
            if (this.mConceptCardData.Lv < this.mConceptCardData.LvCap)
            {
                goto Label_0079;
            }
            if (this.mConceptCardData.Trust >= MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax)
            {
                goto Label_0077;
            }
            if (this.mConceptCardData.GetReward() != null)
            {
                goto Label_0079;
            }
        Label_0077:
            flag = 0;
        Label_0079:
            this.EnhanceButton.set_interactable(flag);
            return;
        }

        public void RefreshEnhanceExecButton()
        {
            ConceptCardManager manager;
            bool flag;
            if ((this.EnhanceExecButton == null) != null)
            {
                goto Label_001C;
            }
            if (this.mConceptCardData != null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            manager = base.GetComponentInParent<ConceptCardManager>();
            if ((manager == null) == null)
            {
                goto Label_0031;
            }
            return;
        Label_0031:
            flag = 1;
            if (0 < manager.SelectedMaterials.Count)
            {
                goto Label_0046;
            }
            flag = 0;
        Label_0046:
            this.EnhanceExecButton.set_interactable(flag);
            return;
        }

        public void SetFavoriteToggle(bool is_on)
        {
            if ((this.mFavoriteToggle != null) == null)
            {
                goto Label_001D;
            }
            this.mFavoriteToggle.set_isOn(is_on);
        Label_001D:
            return;
        }

        public void SetFlavorTextText()
        {
            string str;
            str = this.mConceptCardData.Param.GetLocalizedTextFlavor();
            this.SetText(this.mFlavorText, str);
            return;
        }

        public void SetParam(bool bEnhance)
        {
            ConceptCardManager manager;
            manager = base.GetComponentInParent<ConceptCardManager>();
            if ((manager == null) == null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            this.mConceptCardData = manager.SelectedConceptCardData;
            if (this.mConceptCardData != null)
            {
                goto Label_002C;
            }
            return;
        Label_002C:
            this.mConceptCardDescription.SetConceptCardData(this.mConceptCardData, base.get_gameObject(), bEnhance, 0, null);
            this.Refresh();
            this.RefreshEnhanceButton();
            this.RefreshEnhanceExecButton();
            this.RefreshEnhanceBulkButton();
            return;
        }

        public void SetText(Text text, string str)
        {
            if ((text != null) == null)
            {
                goto Label_0013;
            }
            text.set_text(str);
        Label_0013:
            return;
        }

        private void Start()
        {
        }

        [DebuggerHidden]
        private IEnumerator TrustMasterUpdate(ConceptCardData cardData)
        {
            <TrustMasterUpdate>c__IteratorF5 rf;
            rf = new <TrustMasterUpdate>c__IteratorF5();
            rf.cardData = cardData;
            rf.<$>cardData = cardData;
            rf.<>f__this = this;
            return rf;
        }

        public ConceptCardDescription Description
        {
            get
            {
                return this.mConceptCardDescription;
            }
        }

        [CompilerGenerated]
        private sealed class <TrustMasterUpdate>c__IteratorF5 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal ConceptCardEffectRoutine <routine>__0;
            internal Canvas <overlayCanvas>__1;
            internal ConceptCardData cardData;
            internal int $PC;
            internal object $current;
            internal ConceptCardData <$>cardData;
            internal ConceptCardDetail <>f__this;

            public <TrustMasterUpdate>c__IteratorF5()
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
                        goto Label_0025;

                    case 1:
                        goto Label_0066;

                    case 2:
                        goto Label_008F;
                }
                goto Label_00B3;
            Label_0025:
                this.<routine>__0 = new ConceptCardEffectRoutine();
                this.<overlayCanvas>__1 = UIUtility.PushCanvas(0, -1);
                this.$current = this.<routine>__0.PlayTrustMaster(this.<overlayCanvas>__1, this.cardData);
                this.$PC = 1;
                goto Label_00B5;
            Label_0066:
                this.$current = this.<routine>__0.PlayTrustMasterReward(this.<overlayCanvas>__1, this.cardData);
                this.$PC = 2;
                goto Label_00B5;
            Label_008F:
                Object.Destroy(this.<overlayCanvas>__1.get_gameObject());
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 0x66);
                this.$PC = -1;
            Label_00B3:
                return 0;
            Label_00B5:
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

