namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class GachaResultConceptCardDetail : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private GameObject Icon;
        [SerializeField]
        private Text ExprText;
        [SerializeField]
        private Text FlavorText;
        [SerializeField]
        private Text NameText;
        [SerializeField]
        private ScrollRect ScrollParent;
        [SerializeField]
        private Transform FloavorArea;
        private ConceptCardData m_Data;
        private float mDecelerationRate;

        public GachaResultConceptCardDetail()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Refresh()
        {
            ConceptCardIcon icon;
            if (this.m_Data != null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("真理念装のデータがセットされていません");
            return;
        Label_0016:
            icon = this.Icon.GetComponent<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_003A;
            }
            icon.Setup(this.m_Data);
        Label_003A:
            if ((this.NameText != null) == null)
            {
                goto Label_0066;
            }
            this.NameText.set_text(this.m_Data.Param.name);
        Label_0066:
            if ((this.ExprText != null) == null)
            {
                goto Label_0092;
            }
            this.ExprText.set_text(this.m_Data.Param.expr);
        Label_0092:
            if ((this.FlavorText != null) == null)
            {
                goto Label_00BE;
            }
            this.FlavorText.set_text(this.m_Data.Param.GetLocalizedTextFlavor());
        Label_00BE:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshScrollRect()
        {
            <RefreshScrollRect>c__Iterator10D iteratord;
            iteratord = new <RefreshScrollRect>c__Iterator10D();
            iteratord.<>f__this = this;
            return iteratord;
        }

        private unsafe void ResetScrollPosition()
        {
            RectTransform transform;
            Vector2 vector;
            if ((this.ScrollParent == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mDecelerationRate = this.ScrollParent.get_decelerationRate();
            this.ScrollParent.set_decelerationRate(0f);
            transform = this.FloavorArea as RectTransform;
            transform.set_anchoredPosition(new Vector2(&transform.get_anchoredPosition().x, 0f));
            base.StartCoroutine(this.RefreshScrollRect());
            return;
        }

        public void Setup(ConceptCardData _data)
        {
            this.m_Data = _data;
            this.Refresh();
            return;
        }

        [CompilerGenerated]
        private sealed class <RefreshScrollRect>c__Iterator10D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal GachaResultConceptCardDetail <>f__this;

            public <RefreshScrollRect>c__Iterator10D()
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
                        goto Label_0034;
                }
                goto Label_006C;
            Label_0021:
                this.$current = null;
                this.$PC = 1;
                goto Label_006E;
            Label_0034:
                if ((this.<>f__this.ScrollParent != null) == null)
                {
                    goto Label_0065;
                }
                this.<>f__this.ScrollParent.set_decelerationRate(this.<>f__this.mDecelerationRate);
            Label_0065:
                this.$PC = -1;
            Label_006C:
                return 0;
            Label_006E:
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

