namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using UnityEngine;

    [NodeType("System/キャッシュクリア", 0x7fe5), Pin(0x65, "Finished", 1, 0), Pin(100, "Out", 1, 0), Pin(0, "In", 0, 0)]
    public class FlowNode_ClearCache : FlowNode
    {
        public const int PINID_CLEAR = 0;
        public const int PINID_OUT = 100;
        public const int PINID_FINISHED = 0x65;

        public FlowNode_ClearCache()
        {
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator ClearCacheAsync()
        {
            <ClearCacheAsync>c__IteratorAE rae;
            rae = new <ClearCacheAsync>c__IteratorAE();
            rae.<>f__this = this;
            return rae;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            if (pinID == null)
            {
                goto Label_000D;
            }
            goto Label_0040;
        Label_000D:
            if (base.get_enabled() != null)
            {
                goto Label_0040;
            }
            CriticalSection.Enter(1);
            base.set_enabled(1);
            base.StartCoroutine(this.ClearCacheAsync());
            base.ActivateOutputLinks(100);
        Label_0040:
            return;
        }

        [CompilerGenerated]
        private sealed class <ClearCacheAsync>c__IteratorAE : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Canvas <modalCanvas>__0;
            internal GameObject <prefab>__1;
            internal GameObject <dialog>__2;
            internal Thread <thread>__3;
            internal int $PC;
            internal object $current;
            internal FlowNode_ClearCache <>f__this;

            public <ClearCacheAsync>c__IteratorAE()
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
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_00A0;

                    case 2:
                        goto Label_00D0;

                    case 3:
                        goto Label_0122;
                }
                goto Label_0179;
            Label_0029:
                this.<modalCanvas>__0 = UIUtility.PushCanvas(0, -1);
                this.<prefab>__1 = AssetManager.Load<GameObject>("UI/ExecutingClearCache");
                this.<dialog>__2 = Object.Instantiate<GameObject>(this.<prefab>__1);
                if ((this.<dialog>__2 != null) == null)
                {
                    goto Label_00A0;
                }
                this.<dialog>__2.get_transform().SetParent(this.<modalCanvas>__0.get_transform(), 0);
                goto Label_00A0;
            Label_0089:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_017B;
            Label_00A0:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0089;
                }
                MonoSingleton<MySound>.Instance.PrepareToClearCache();
                goto Label_00D0;
            Label_00B9:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_017B;
            Label_00D0:
                if (MonoSingleton<MySound>.Instance.IsReadyToClearCache() == null)
                {
                    goto Label_00B9;
                }
                this.<thread>__3 = new Thread(new ThreadStart(AssetDownloader.ClearCache));
                this.<thread>__3.Start(AssetDownloader.CachePath);
                goto Label_0122;
            Label_010B:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_017B;
            Label_0122:
                if (this.<thread>__3.IsAlive != null)
                {
                    goto Label_010B;
                }
                CriticalSection.Leave(1);
                AssetManager.UnloadAll();
                if ((this.<modalCanvas>__0 != null) == null)
                {
                    goto Label_0153;
                }
                UIUtility.PopCanvas();
            Label_0153:
                this.<>f__this.set_enabled(0);
                this.<>f__this.ActivateOutputLinks(0x65);
                goto Label_0179;
            Label_0179:
                return 0;
            Label_017B:
                return 1;
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

