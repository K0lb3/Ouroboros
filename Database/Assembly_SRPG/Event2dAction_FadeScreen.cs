namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("フェード(2D)", "画面をフェードイン・フェードアウトさせます", 0x555555, 0x444488)]
    public class Event2dAction_FadeScreen : EventAction
    {
        public bool FadeOut;
        public bool ChangeColor;
        public bool Async;
        public float FadeTime;
        private Event2dFade mEvent2dFade;
        private LoadRequest mResource;
        private static readonly string AssetPath;
        private Color FadeInColorWhite;
        private Color FadeInColorBlack;

        static Event2dAction_FadeScreen()
        {
            AssetPath = "Event2dAssets/Event2dFade";
            return;
        }

        public Event2dAction_FadeScreen()
        {
            this.FadeTime = 1f;
            this.FadeInColorWhite = new Color(1f, 1f, 1f, 0f);
            this.FadeInColorBlack = new Color(0f, 0f, 0f, 0f);
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if ((this.mEvent2dFade == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mEvent2dFade.get_gameObject().SetActive(1);
            this.StartFade();
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__IteratorA1 ra;
            ra = new <PreloadAssets>c__IteratorA1();
            ra.<>f__this = this;
            return ra;
        }

        public override void PreStart()
        {
            if ((this.mEvent2dFade != null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mEvent2dFade = Event2dFade.Find();
            if ((this.mEvent2dFade != null) == null)
            {
                goto Label_002F;
            }
            return;
        Label_002F:
            this.mEvent2dFade = Object.Instantiate(this.mResource.asset) as Event2dFade;
            this.mEvent2dFade.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            this.mEvent2dFade.get_transform().SetAsLastSibling();
            this.mEvent2dFade.get_gameObject().SetActive(0);
            return;
        }

        private void StartFade()
        {
            if (this.FadeOut == null)
            {
                goto Label_004C;
            }
            if (this.ChangeColor == null)
            {
                goto Label_0031;
            }
            this.mEvent2dFade.FadeTo(Color.get_white(), this.FadeTime);
            goto Label_0047;
        Label_0031:
            this.mEvent2dFade.FadeTo(Color.get_black(), this.FadeTime);
        Label_0047:
            goto Label_008A;
        Label_004C:
            if (this.ChangeColor == null)
            {
                goto Label_0073;
            }
            this.mEvent2dFade.FadeTo(this.FadeInColorWhite, this.FadeTime);
            goto Label_008A;
        Label_0073:
            this.mEvent2dFade.FadeTo(this.FadeInColorBlack, this.FadeTime);
        Label_008A:
            if (this.Async == null)
            {
                goto Label_009C;
            }
            base.ActivateNext();
            return;
        Label_009C:
            return;
        }

        public override void Update()
        {
            if (this.mEvent2dFade.IsFading == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            base.ActivateNext();
            return;
        }

        public override bool IsPreloadAssets
        {
            get
            {
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__IteratorA1 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal Event2dAction_FadeScreen <>f__this;

            public <PreloadAssets>c__IteratorA1()
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
                        goto Label_0058;
                }
                goto Label_0086;
            Label_0021:
                this.<>f__this.mResource = AssetManager.LoadAsync<Event2dFade>(Event2dAction_FadeScreen.AssetPath);
                this.$current = this.<>f__this.mResource.StartCoroutine();
                this.$PC = 1;
                goto Label_0088;
            Label_0058:
                if ((this.<>f__this.mResource.asset == null) == null)
                {
                    goto Label_007F;
                }
                this.<>f__this.mResource = null;
            Label_007F:
                this.$PC = -1;
            Label_0086:
                return 0;
            Label_0088:
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

