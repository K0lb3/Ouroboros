namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.EventSystems;

    [AddComponentMenu("Scripts/SRPG/Scene/Startup")]
    public class SceneStartup : Scene
    {
        private const string Key_ClearCache = "CLEARCACHE";
        private static bool mResolutionChanged;
        public bool AutoStart;

        public SceneStartup()
        {
            this.AutoStart = 1;
            base..ctor();
            return;
        }

        private void Awake()
        {
            TextAsset asset;
            base.Awake();
            MonoSingleton<UrlScheme>.Instance.Ensure();
            MonoSingleton<PaymentManager>.Instance.Ensure();
            MonoSingleton<NetworkError>.Instance.Ensure();
            MonoSingleton<WatchManager>.Instance.Ensure();
            asset = Resources.Load<TextAsset>("appserveraddr");
            if ((asset != null) == null)
            {
                goto Label_0050;
            }
            Network.SetDefaultHostConfigured(asset.get_text());
        Label_0050:
            return;
        }

        [DebuggerHidden]
        private IEnumerator Start()
        {
            <Start>c__Iterator84 iterator;
            iterator = new <Start>c__Iterator84();
            iterator.<>f__this = this;
            return iterator;
        }

        [CompilerGenerated]
        private sealed class <Start>c__Iterator84 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string <startScene>__0;
            internal int $PC;
            internal object $current;
            internal SceneStartup <>f__this;

            public <Start>c__Iterator84()
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
                        goto Label_004C;
                }
                goto Label_0083;
            Label_0021:
                Object.DontDestroyOnLoad(EventSystem.get_current().get_gameObject());
                this.$current = new WaitForSeconds(0.1f);
                this.$PC = 1;
                goto Label_0085;
            Label_004C:
                GameCenterManager.Auth();
                MyMetaps.Setup();
                if (this.<>f__this.AutoStart == null)
                {
                    goto Label_007C;
                }
                this.<startScene>__0 = "Splash";
                GameUtility.RequestScene(this.<startScene>__0);
            Label_007C:
                this.$PC = -1;
            Label_0083:
                return 0;
            Label_0085:
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

