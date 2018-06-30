namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class SceneDummyStartup : Scene
    {
        private static bool mResolutionChanged;

        public SceneDummyStartup()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            object[] objArray1;
            int num;
            int num2;
            float num3;
            int num4;
            int num5;
            int num6;
            base.Awake();
            MonoSingleton<UrlScheme>.Instance.Ensure();
            MonoSingleton<PaymentManager>.Instance.Ensure();
            MonoSingleton<NetworkError>.Instance.Ensure();
            if (mResolutionChanged != null)
            {
                goto Label_00AC;
            }
            num = ScreenUtility.DefaultScreenWidth;
            num2 = ScreenUtility.DefaultScreenHeight;
            num3 = ((float) num) / ((float) num2);
            num4 = Mathf.Min(num2, 750);
            num5 = Mathf.FloorToInt(num3 * ((float) num4));
            num6 = num4;
            ScreenUtility.SetResolution(num5, num6);
            mResolutionChanged = 1;
            objArray1 = new object[] { (int) num5, (int) num6, (int) Screen.get_width(), (int) Screen.get_height() };
            Debug.Log(string.Format("Changing Resolution to [{0} x {1}] from [{2} x {3}]", objArray1));
        Label_00AC:
            return;
        }

        [DebuggerHidden]
        private IEnumerator Start()
        {
            <Start>c__Iterator80 iterator;
            iterator = new <Start>c__Iterator80();
            return iterator;
        }

        [CompilerGenerated]
        private sealed class <Start>c__Iterator80 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal int $PC;
            internal object $current;

            public <Start>c__Iterator80()
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
                        goto Label_003D;
                }
                goto Label_0076;
            Label_0021:
                this.$current = new WaitForSeconds(0.1f);
                this.$PC = 1;
                goto Label_0078;
            Label_003D:
                this.<gm>__0 = MonoSingleton<GameManager>.GetInstanceDirect();
                if ((this.<gm>__0 != null) == null)
                {
                    goto Label_0064;
                }
                Object.DestroyImmediate(this.<gm>__0);
            Label_0064:
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                this.$PC = -1;
            Label_0076:
                return 0;
            Label_0078:
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

