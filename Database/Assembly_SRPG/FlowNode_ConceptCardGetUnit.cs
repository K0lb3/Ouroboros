namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0, "開始", 0, 0), Pin(10, "終了", 1, 10), NodeType("ConceptCard/ConceptCardGetUnit")]
    public class FlowNode_ConceptCardGetUnit : FlowNode
    {
        private static List<ConceptCardData> s_ConceptCards;

        static FlowNode_ConceptCardGetUnit()
        {
            s_ConceptCards = new List<ConceptCardData>();
            return;
        }

        public FlowNode_ConceptCardGetUnit()
        {
            base..ctor();
            return;
        }

        public static void AddConceptCardData(ConceptCardData conceptCardData)
        {
            s_ConceptCards.Add(conceptCardData);
            return;
        }

        [DebuggerHidden]
        private IEnumerator DownloadRoutine()
        {
            <DownloadRoutine>c__IteratorB0 rb;
            rb = new <DownloadRoutine>c__IteratorB0();
            return rb;
        }

        [DebuggerHidden]
        private IEnumerator EffectRoutine(ConceptCardData conceptCardData)
        {
            <EffectRoutine>c__IteratorB1 rb;
            rb = new <EffectRoutine>c__IteratorB1();
            rb.conceptCardData = conceptCardData;
            rb.<$>conceptCardData = conceptCardData;
            return rb;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_003B;
            }
            if (s_ConceptCards == null)
            {
                goto Label_0032;
            }
            if (s_ConceptCards.Count <= 0)
            {
                goto Label_0032;
            }
            base.StartCoroutine(this.StartEffects());
            goto Label_003B;
        Label_0032:
            base.ActivateOutputLinks(10);
        Label_003B:
            return;
        }

        [DebuggerHidden]
        private IEnumerator StartEffects()
        {
            <StartEffects>c__IteratorAF raf;
            raf = new <StartEffects>c__IteratorAF();
            raf.<>f__this = this;
            return raf;
        }

        [CompilerGenerated]
        private sealed class <DownloadRoutine>c__IteratorB0 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal ConceptCardData <card>__1;
            internal UnitParam <up>__2;
            internal int $PC;
            internal object $current;

            public <DownloadRoutine>c__IteratorB0()
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
                        goto Label_00F1;
                }
                goto Label_0107;
            Label_0021:
                if (FlowNode_ConceptCardGetUnit.s_ConceptCards.Count <= 0)
                {
                    goto Label_0100;
                }
                this.<i>__0 = 0;
                goto Label_00A8;
            Label_003D:
                this.<card>__1 = FlowNode_ConceptCardGetUnit.s_ConceptCards[this.<i>__0];
                this.<up>__2 = MonoSingleton<GameManager>.Instance.GetUnitParam(this.<card>__1.Param.first_get_unit);
                if (this.<up>__2 == null)
                {
                    goto Label_008A;
                }
                DownloadUtility.DownloadUnit(this.<up>__2, null);
            Label_008A:
                DownloadUtility.DownloadConceptCard(this.<card>__1.Param);
                this.<i>__0 += 1;
            Label_00A8:
                if (this.<i>__0 < FlowNode_ConceptCardGetUnit.s_ConceptCards.Count)
                {
                    goto Label_003D;
                }
                if (AssetDownloader.isDone != null)
                {
                    goto Label_00CC;
                }
                ProgressWindow.OpenGenericDownloadWindow();
            Label_00CC:
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_00F1;
            Label_00DA:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0109;
            Label_00F1:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_00DA;
                }
                ProgressWindow.Close();
            Label_0100:
                this.$PC = -1;
            Label_0107:
                return 0;
            Label_0109:
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
        private sealed class <EffectRoutine>c__IteratorB1 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <loadRequest>__0;
            internal GameObject <obj>__1;
            internal ConceptCardGetUnit <conceptCardGetUnit>__2;
            internal ConceptCardData conceptCardData;
            internal int $PC;
            internal object $current;
            internal ConceptCardData <$>conceptCardData;

            public <EffectRoutine>c__IteratorB1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__192()
            {
                return (this.<obj>__1 != null);
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
                        goto Label_0029;

                    case 1:
                        goto Label_0061;

                    case 2:
                        goto Label_00C1;

                    case 3:
                        goto Label_00D4;
                }
                goto Label_00DB;
            Label_0029:
                this.<loadRequest>__0 = ConceptCardGetUnit.StartLoadPrefab();
                if (this.<loadRequest>__0.isDone != null)
                {
                    goto Label_0061;
                }
                this.$current = this.<loadRequest>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00DD;
            Label_0061:
                this.<obj>__1 = Object.Instantiate<GameObject>((GameObject) this.<loadRequest>__0.asset);
                this.<conceptCardGetUnit>__2 = this.<obj>__1.GetComponentInChildren<ConceptCardGetUnit>();
                this.<conceptCardGetUnit>__2.Setup(this.conceptCardData);
                this.$current = new WaitWhile(new Func<bool>(this.<>m__192));
                this.$PC = 2;
                goto Label_00DD;
            Label_00C1:
                this.$current = null;
                this.$PC = 3;
                goto Label_00DD;
            Label_00D4:
                this.$PC = -1;
            Label_00DB:
                return 0;
            Label_00DD:
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
        private sealed class <StartEffects>c__IteratorAF : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal FlowNode_ConceptCardGetUnit <>f__this;

            public <StartEffects>c__IteratorAF()
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
                        goto Label_004D;

                    case 2:
                        goto Label_0085;
                }
                goto Label_00B5;
            Label_0025:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.DownloadRoutine());
                this.$PC = 1;
                goto Label_00B7;
            Label_004D:
                goto Label_0090;
            Label_0052:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.EffectRoutine(FlowNode_ConceptCardGetUnit.s_ConceptCards[0]));
                this.$PC = 2;
                goto Label_00B7;
            Label_0085:
                FlowNode_ConceptCardGetUnit.s_ConceptCards.RemoveAt(0);
            Label_0090:
                if (FlowNode_ConceptCardGetUnit.s_ConceptCards.Count > 0)
                {
                    goto Label_0052;
                }
                this.<>f__this.ActivateOutputLinks(10);
                this.$PC = -1;
            Label_00B5:
                return 0;
            Label_00B7:
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

