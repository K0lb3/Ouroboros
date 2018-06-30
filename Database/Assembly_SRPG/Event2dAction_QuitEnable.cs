namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;

    [EventActionInfo("強制終了/許可(2D)", "強制終了を許可します", 0x555555, 0x444488)]
    public class Event2dAction_QuitEnable : EventAction
    {
        protected static EventQuit mQuit;
        private LoadRequest mResource;
        private static readonly string AssetPath;

        static Event2dAction_QuitEnable()
        {
            AssetPath = "Event2dAssets/BtnSkip";
            return;
        }

        public Event2dAction_QuitEnable()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <PreStart>m__181()
        {
            base.Sequence.OnQuit();
            return;
        }

        public override void OnActivate()
        {
            if ((null == mQuit) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            mQuit.get_gameObject().SetActive(1);
            base.ActivateNext();
            return;
        }

        protected override void OnDestroy()
        {
            mQuit = null;
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__IteratorA2 ra;
            ra = new <PreloadAssets>c__IteratorA2();
            ra.<>f__this = this;
            return ra;
        }

        public override void PreStart()
        {
            if ((null != mQuit) != null)
            {
                goto Label_001B;
            }
            if (this.mResource != null)
            {
                goto Label_001C;
            }
        Label_001B:
            return;
        Label_001C:
            mQuit = Object.Instantiate(this.mResource.asset) as EventQuit;
            mQuit.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            mQuit.get_transform().SetAsLastSibling();
            mQuit.OnClick = new UnityAction(this, this.<PreStart>m__181);
            mQuit.get_gameObject().SetActive(0);
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
        private sealed class <PreloadAssets>c__IteratorA2 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal Event2dAction_QuitEnable <>f__this;

            public <PreloadAssets>c__IteratorA2()
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
                this.<>f__this.mResource = AssetManager.LoadAsync<EventQuit>(Event2dAction_QuitEnable.AssetPath);
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

