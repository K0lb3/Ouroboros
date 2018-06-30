namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using UnityEngine;

    [Pin(0xca, "Finish", 1, 0), Pin(0x65, "Delete", 0, 0), Pin(0xc9, "Not Exist", 1, 0), NodeType("System/CheckOldData", 0x7fe5), Pin(200, "Exist", 1, 0), Pin(100, "Check", 0, 0)]
    public class FlowNode_CheckOldData : FlowNode
    {
        private readonly int PINID_CHECK;
        private readonly int PINID_DELETE;
        private readonly int PINID_EXIST;
        private readonly int PINID_NOT_EXIST;
        private readonly int PINID_FINISH;

        public FlowNode_CheckOldData()
        {
            this.PINID_CHECK = 100;
            this.PINID_DELETE = 0x65;
            this.PINID_EXIST = 200;
            this.PINID_NOT_EXIST = 0xc9;
            this.PINID_FINISH = 0xca;
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator DeleteOldData()
        {
            <DeleteOldData>c__IteratorAD rad;
            rad = new <DeleteOldData>c__IteratorAD();
            rad.<>f__this = this;
            return rad;
        }

        public static void DeleteThread(object param)
        {
            string str;
            str = param as string;
            if (Directory.Exists(str) == null)
            {
                goto Label_0019;
            }
            Directory.Delete(str, 1);
        Label_0019:
            return;
        }

        private bool IsExist()
        {
            return Directory.Exists(AssetDownloader.OldDownloadPath);
        }

        public override void OnActivate(int pinID)
        {
            if (GameUtility.Config_UseAssetBundles.Value != null)
            {
                goto Label_0021;
            }
            base.ActivateOutputLinks(this.PINID_NOT_EXIST);
            goto Label_0077;
        Label_0021:
            if (pinID != this.PINID_CHECK)
            {
                goto Label_0058;
            }
            if (this.IsExist() == null)
            {
                goto Label_0046;
            }
            base.ActivateOutputLinks(this.PINID_EXIST);
            return;
        Label_0046:
            base.ActivateOutputLinks(this.PINID_NOT_EXIST);
            goto Label_0077;
        Label_0058:
            if (pinID != this.PINID_DELETE)
            {
                goto Label_0077;
            }
            CriticalSection.Enter(1);
            base.StartCoroutine(this.DeleteOldData());
        Label_0077:
            return;
        }

        [CompilerGenerated]
        private sealed class <DeleteOldData>c__IteratorAD : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Canvas <parent>__0;
            internal GameObject <prefab>__1;
            internal GameObject <dialog>__2;
            internal Thread <thread>__3;
            internal int $PC;
            internal object $current;
            internal FlowNode_CheckOldData <>f__this;

            public <DeleteOldData>c__IteratorAD()
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
                        goto Label_0021;

                    case 1:
                        goto Label_00BF;
                }
                goto Label_010E;
            Label_0021:
                this.<parent>__0 = UIUtility.PushCanvas(0, -1);
                this.<prefab>__1 = AssetManager.Load<GameObject>("UI/OptimizeData");
                this.<dialog>__2 = Object.Instantiate<GameObject>(this.<prefab>__1);
                if ((this.<dialog>__2 != null) == null)
                {
                    goto Label_007C;
                }
                this.<dialog>__2.get_transform().SetParent(this.<parent>__0.get_transform(), 0);
            Label_007C:
                this.<thread>__3 = new Thread(new ParameterizedThreadStart(FlowNode_CheckOldData.DeleteThread));
                this.<thread>__3.Start(AssetDownloader.OldDownloadPath);
                goto Label_00BF;
            Label_00A8:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0110;
            Label_00BF:
                if (this.<thread>__3.IsAlive != null)
                {
                    goto Label_00A8;
                }
                CriticalSection.Leave(1);
                if ((this.<parent>__0 != null) == null)
                {
                    goto Label_00EB;
                }
                UIUtility.PopCanvas();
            Label_00EB:
                this.<>f__this.ActivateOutputLinks(this.<>f__this.PINID_FINISH);
                goto Label_010E;
            Label_010E:
                return 0;
            Label_0110:
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

