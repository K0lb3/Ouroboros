namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;

    [EventActionInfo("強制終了/許可", "スクリプトの強制終了を有効にします。", 0x555555, 0x444488)]
    public class EventAction_EnableQuit : EventAction
    {
        protected static EventQuit mQuit;
        private LoadRequest mResource;
        private static readonly string AssetPath;

        static EventAction_EnableQuit()
        {
            AssetPath = "UI/BtnSkip_movie";
            return;
        }

        public EventAction_EnableQuit()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <PreStart>m__170()
        {
            if ((base.Sequence != null) == null)
            {
                goto Label_0027;
            }
            this.SkipButtonAction(base.Sequence, mQuit.get_gameObject());
        Label_0027:
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
            mQuit.get_transform().SetAsLastSibling();
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
            <PreloadAssets>c__Iterator8A iteratora;
            iteratora = new <PreloadAssets>c__Iterator8A();
            iteratora.<>f__this = this;
            return iteratora;
        }

        public override void PreStart()
        {
            EventQuit quit;
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
            quit = EventQuit.Find();
            if ((quit == null) == null)
            {
                goto Label_004D;
            }
            mQuit = Object.Instantiate(this.mResource.asset) as EventQuit;
            goto Label_0053;
        Label_004D:
            mQuit = quit;
        Label_0053:
            mQuit.get_transform().SetParent(base.ActiveCanvas.get_transform(), 0);
            mQuit.get_transform().SetAsLastSibling();
            mQuit.OnClick = new UnityAction(this, this.<PreStart>m__170);
            mQuit.get_gameObject().SetActive(0);
            return;
        }

        private void SkipButtonAction(EventScript.Sequence inEventScriptSequence, GameObject inSkipButtonGameObject)
        {
            GlobalVars.IsSkipQuestDemo = 1;
            inEventScriptSequence.GoToEndState();
            inSkipButtonGameObject.SetActive(0);
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
        private sealed class <PreloadAssets>c__Iterator8A : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal EventAction_EnableQuit <>f__this;

            public <PreloadAssets>c__Iterator8A()
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
                this.<>f__this.mResource = AssetManager.LoadAsync<EventQuit>(EventAction_EnableQuit.AssetPath);
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

