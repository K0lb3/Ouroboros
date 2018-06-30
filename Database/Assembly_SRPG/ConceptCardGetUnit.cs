namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "ユニットのレアリティ再設定完了", 1, 100), Pin(0, "ユニットのレアリティ再設定", 0, 0)]
    public class ConceptCardGetUnit : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private Animator m_ConceptCardAnimator;
        [SerializeField]
        private Animator m_UnitAnimator;
        [SerializeField]
        private RawImage m_ConceptCardImage;
        [SerializeField]
        private ImageArray m_ConceptCardFrame;
        [SerializeField]
        private RawImage m_UnitImage;
        [SerializeField]
        private RawImage m_UnitBlurImage;
        [SerializeField]
        private Text m_UnitTextDescription;
        private int m_UnitRarity;

        public ConceptCardGetUnit()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_001C;
            }
            this.SetUnitRarity(this.m_UnitRarity + 1);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_001C:
            return;
        }

        private void ResetConceptCardAnimationState()
        {
            if ((this.m_ConceptCardAnimator == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.m_ConceptCardAnimator.SetInteger("rariry", 0);
            this.m_ConceptCardAnimator.SetBool("shard", 0);
            this.m_ConceptCardAnimator.SetBool("item", 0);
            this.m_ConceptCardAnimator.SetBool("reset", 0);
            this.m_ConceptCardAnimator.SetBool("ccard", 0);
            return;
        }

        private void SetConceptCardRarity(int value)
        {
            if ((this.m_ConceptCardAnimator == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.m_ConceptCardAnimator.SetInteger("rariry", value);
            return;
        }

        private void SetSummonAnimationType(SummonAnimationType type)
        {
            if ((this.m_ConceptCardAnimator == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.m_ConceptCardAnimator.SetBool("shard", type == 0);
            this.m_ConceptCardAnimator.SetBool("item", type == 1);
            this.m_ConceptCardAnimator.SetBool("ccard", type == 2);
            return;
        }

        private void SetUnitRarity(int value)
        {
            if ((this.m_UnitAnimator == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.m_UnitAnimator.SetInteger("rariry", value);
            return;
        }

        public void Setup(ConceptCardData conceptCardData)
        {
            object[] objArray1;
            UnitParam param;
            ConceptCardParam param2;
        Label_0000:
            try
            {
                param = MonoSingleton<GameManager>.Instance.GetUnitParam(conceptCardData.Param.first_get_unit);
                param2 = conceptCardData.Param;
                if ((this.m_ConceptCardImage != null) == null)
                {
                    goto Label_004A;
                }
                if (param2 == null)
                {
                    goto Label_004A;
                }
                MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.m_ConceptCardImage, AssetPath.ConceptCard(param2));
            Label_004A:
                if ((this.m_UnitImage != null) == null)
                {
                    goto Label_007E;
                }
                if (param == null)
                {
                    goto Label_007E;
                }
                MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.m_UnitImage, AssetPath.UnitImage(param, param.GetJobId(0)));
            Label_007E:
                if ((this.m_UnitBlurImage != null) == null)
                {
                    goto Label_00B2;
                }
                if (param == null)
                {
                    goto Label_00B2;
                }
                MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.m_UnitBlurImage, AssetPath.UnitImage(param, param.GetJobId(0)));
            Label_00B2:
                if ((this.m_ConceptCardFrame != null) == null)
                {
                    goto Label_00D4;
                }
                this.m_ConceptCardFrame.ImageIndex = param2.rare;
            Label_00D4:
                if ((this.m_UnitTextDescription != null) == null)
                {
                    goto Label_0112;
                }
                objArray1 = new object[] { param2.name, param.name };
                this.m_UnitTextDescription.set_text(LocalizedText.Get("sys.CONCEPT_CARD_UNIT_GET_DESCRIPTION", objArray1));
            Label_0112:
                this.SetConceptCardRarity(param2.rare + 1);
                this.SetUnitRarity(param.rare + 1);
                this.m_UnitRarity = param.rare;
                base.StartCoroutine(this.WaitConceptCardEffectEnd());
                goto Label_0160;
            }
            catch
            {
            Label_014C:
                this.SetConceptCardRarity(1);
                this.SetUnitRarity(1);
                goto Label_0160;
            }
        Label_0160:
            return;
        }

        public static LoadRequest StartLoadPrefab()
        {
            return AssetManager.LoadAsync<GameObject>(GameSettings.Instance.ConceptCard_GetUnit);
        }

        [DebuggerHidden]
        private IEnumerator WaitConceptCardEffectEnd()
        {
            <WaitConceptCardEffectEnd>c__Iterator102 iterator;
            iterator = new <WaitConceptCardEffectEnd>c__Iterator102();
            iterator.<>f__this = this;
            return iterator;
        }

        private AnimatorStateInfo animatorStateInfo
        {
            get
            {
                return this.m_ConceptCardAnimator.GetCurrentAnimatorStateInfo(0);
            }
        }

        [CompilerGenerated]
        private sealed class <WaitConceptCardEffectEnd>c__Iterator102 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <conceptCardRarity>__0;
            internal string <animName>__1;
            internal int $PC;
            internal object $current;
            internal ConceptCardGetUnit <>f__this;

            public <WaitConceptCardEffectEnd>c__Iterator102()
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

            public unsafe bool MoveNext()
            {
                uint num;
                AnimatorStateInfo info;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_0083;

                    case 2:
                        goto Label_00B9;

                    case 3:
                        goto Label_00EC;
                }
                goto Label_00F3;
            Label_0029:
                this.<conceptCardRarity>__0 = this.<>f__this.m_ConceptCardAnimator.GetInteger("rariry");
                this.<>f__this.SetSummonAnimationType(2);
                this.<animName>__1 = string.Format("DropMaterial_CCard_rare{0}", (int) this.<conceptCardRarity>__0);
                goto Label_0083;
            Label_0070:
                this.$current = null;
                this.$PC = 1;
                goto Label_00F5;
            Label_0083:
                if (&this.<>f__this.animatorStateInfo.IsName(this.<animName>__1) == null)
                {
                    goto Label_0070;
                }
                goto Label_00B9;
            Label_00A6:
                this.$current = null;
                this.$PC = 2;
                goto Label_00F5;
            Label_00B9:
                if (GameUtility.IsAnimatorRunning(this.<>f__this.m_ConceptCardAnimator) != null)
                {
                    goto Label_00A6;
                }
                this.<>f__this.ResetConceptCardAnimationState();
                this.$current = null;
                this.$PC = 3;
                goto Label_00F5;
            Label_00EC:
                this.$PC = -1;
            Label_00F3:
                return 0;
            Label_00F5:
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

        private enum SummonAnimationType
        {
            Shard,
            Item,
            Card
        }
    }
}

