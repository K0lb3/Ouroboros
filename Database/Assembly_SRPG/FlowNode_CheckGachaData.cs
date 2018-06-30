namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0x63, "エラー発生", 1, 0x63), Pin(0, "In", 0, 0), Pin(11, "ダウンロード完了", 1, 11), NodeType("System/CheckGachaData", 0x7fe5), Pin(100, "キャンセル", 1, 12), Pin(10, "ダウンロード開始", 1, 10), Pin(1, "確認", 0, 1)]
    public class FlowNode_CheckGachaData : FlowNode
    {
        public const int PINID_IN = 0;
        public List<string> DownloadUnits;
        public List<ArtifactParam> DownloadArtifacts;
        private List<AssetList.Item> mQueue;

        public FlowNode_CheckGachaData()
        {
            this.DownloadUnits = new List<string>();
            this.DownloadArtifacts = new List<ArtifactParam>();
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncWork(bool confirm)
        {
            <AsyncWork>c__IteratorAC rac;
            rac = new <AsyncWork>c__IteratorAC();
            rac.confirm = confirm;
            rac.<$>confirm = confirm;
            rac.<>f__this = this;
            return rac;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            GachaParam[] paramArray;
            int num;
            int num2;
            string str;
            int num3;
            ArtifactParam param;
            string str2;
            AssetList.Item item;
            if (pinID != null)
            {
                goto Label_01BA;
            }
            if (GameUtility.Config_UseAssetBundles.Value != null)
            {
                goto Label_001F;
            }
            base.ActivateOutputLinks(11);
            return;
        Label_001F:
            if (base.get_enabled() == null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.Gachas;
            num = 0;
            goto Label_0199;
        Label_003F:
            if (paramArray[num] != null)
            {
                goto Label_004C;
            }
            goto Label_0195;
        Label_004C:
            if (paramArray[num].units == null)
            {
                goto Label_00F3;
            }
            if (paramArray[num].units.Count <= 0)
            {
                goto Label_00F3;
            }
            num2 = 0;
            goto Label_00E0;
        Label_0073:
            if (manager.Player.FindUnitDataByUnitID(paramArray[num].units[num2].iname) != null)
            {
                goto Label_00DC;
            }
            str = paramArray[num].units[num2].iname;
            if (this.DownloadUnits.IndexOf(str) != -1)
            {
                goto Label_00DC;
            }
            this.DownloadUnits.Add(paramArray[num].units[num2].iname);
        Label_00DC:
            num2 += 1;
        Label_00E0:
            if (num2 < paramArray[num].units.Count)
            {
                goto Label_0073;
            }
        Label_00F3:
            if (paramArray[num].artifacts == null)
            {
                goto Label_0195;
            }
            if (paramArray[num].artifacts.Count <= 0)
            {
                goto Label_0195;
            }
            num3 = 0;
            goto Label_0181;
        Label_011B:
            param = paramArray[num].artifacts[num3];
            if (param == null)
            {
                goto Label_017B;
            }
            str2 = AssetPath.Artifacts(param);
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_017B;
            }
            item = AssetManager.AssetList.FindItemByPath(str2);
            if (item == null)
            {
                goto Label_017B;
            }
            if (AssetManager.IsAssetInCache(item.IDStr) != null)
            {
                goto Label_017B;
            }
            this.DownloadArtifacts.Add(param);
        Label_017B:
            num3 += 1;
        Label_0181:
            if (num3 < paramArray[num].artifacts.Count)
            {
                goto Label_011B;
            }
        Label_0195:
            num += 1;
        Label_0199:
            if (num < ((int) paramArray.Length))
            {
                goto Label_003F;
            }
            base.set_enabled(1);
            base.StartCoroutine(this.AsyncWork(pinID == 1));
        Label_01BA:
            return;
        }

        [CompilerGenerated]
        private sealed class <AsyncWork>c__IteratorAC : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal bool confirm;
            internal int <i>__1;
            internal int <i>__2;
            internal UnitParam <unit>__3;
            internal int <i>__4;
            internal int $PC;
            internal object $current;
            internal bool <$>confirm;
            internal FlowNode_CheckGachaData <>f__this;

            public <AsyncWork>c__IteratorAC()
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
                        goto Label_0029;

                    case 1:
                        goto Label_004C;

                    case 2:
                        goto Label_020A;

                    case 3:
                        goto Label_0246;
                }
                goto Label_024D;
            Label_0029:
                this.<gm>__0 = MonoSingleton<GameManager>.GetInstanceDirect();
                goto Label_004C;
            Label_0039:
                this.$current = null;
                this.$PC = 1;
                goto Label_024F;
            Label_004C:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0039;
                }
                if (this.confirm != null)
                {
                    goto Label_01D6;
                }
                if (this.<>f__this.mQueue == null)
                {
                    goto Label_00D2;
                }
                this.<i>__1 = 0;
                goto Label_00AB;
            Label_007D:
                AssetDownloader.Add(this.<>f__this.mQueue[this.<i>__1].IDStr);
                this.<i>__1 += 1;
            Label_00AB:
                if (this.<i>__1 < this.<>f__this.mQueue.Count)
                {
                    goto Label_007D;
                }
                this.<>f__this.mQueue = null;
            Label_00D2:
                if ((this.<gm>__0 != null) == null)
                {
                    goto Label_0176;
                }
                this.<i>__2 = 0;
                goto Label_015B;
            Label_00EF:
                if (string.IsNullOrEmpty(this.<>f__this.DownloadUnits[this.<i>__2]) != null)
                {
                    goto Label_014D;
                }
                this.<unit>__3 = this.<gm>__0.GetUnitParam(this.<>f__this.DownloadUnits[this.<i>__2]);
                if (this.<unit>__3 == null)
                {
                    goto Label_014D;
                }
                DownloadUtility.DownloadUnit(this.<unit>__3, null);
            Label_014D:
                this.<i>__2 += 1;
            Label_015B:
                if (this.<i>__2 < this.<>f__this.DownloadUnits.Count)
                {
                    goto Label_00EF;
                }
            Label_0176:
                if (this.<>f__this.DownloadArtifacts == null)
                {
                    goto Label_01D6;
                }
                this.<i>__4 = 0;
                goto Label_01BB;
            Label_0192:
                DownloadUtility.DownloadArtifact(this.<>f__this.DownloadArtifacts[this.<i>__4]);
                this.<i>__4 += 1;
            Label_01BB:
                if (this.<i>__4 < this.<>f__this.DownloadArtifacts.Count)
                {
                    goto Label_0192;
                }
            Label_01D6:
                if (AssetDownloader.isDone != null)
                {
                    goto Label_01E5;
                }
                ProgressWindow.OpenGenericDownloadWindow();
            Label_01E5:
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_020A;
            Label_01F3:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_024F;
            Label_020A:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_01F3;
                }
                ProgressWindow.Close();
                this.<>f__this.set_enabled(0);
                this.<>f__this.ActivateOutputLinks(11);
                this.$current = null;
                this.$PC = 3;
                goto Label_024F;
            Label_0246:
                this.$PC = -1;
            Label_024D:
                return 0;
            Label_024F:
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

