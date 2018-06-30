namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "Success", 1, 10), Pin(0, "ダウンロード開始", 0, 0), NodeType("System/DownloadTowerMapSets", 0x7fe5)]
    public class FlowNode_DownloadTowerMapSets : FlowNode
    {
        [SerializeField]
        private int DownloadAssetNums;

        public FlowNode_DownloadTowerMapSets()
        {
            this.DownloadAssetNums = 10;
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator DownloadFloorParamAsync()
        {
            <DownloadFloorParamAsync>c__IteratorB4 rb;
            rb = new <DownloadFloorParamAsync>c__IteratorB4();
            rb.<>f__this = this;
            return rb;
        }

        [DebuggerHidden]
        private IEnumerator DownloadFloorParams()
        {
            <DownloadFloorParams>c__IteratorB3 rb;
            rb = new <DownloadFloorParams>c__IteratorB3();
            rb.<>f__this = this;
            return rb;
        }

        public static void DownloadMapSets(List<TowerFloorParam> floorParams)
        {
            int num;
            TowerFloorParam param;
            string str;
            if (floorParams != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            goto Label_0053;
        Label_000E:
            param = floorParams[num];
            if (param.map.Count <= 0)
            {
                goto Label_004F;
            }
            str = param.map[0].mapSetName;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_004F;
            }
            AssetManager.PrepareAssets(AssetPath.LocalMap(str));
        Label_004F:
            num += 1;
        Label_0053:
            if (num < floorParams.Count)
            {
                goto Label_000E;
            }
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0038;
            }
            base.set_enabled(1);
            if (Network.Mode != null)
            {
                goto Label_0029;
            }
            base.StartCoroutine(this.DownloadFloorParams());
            goto Label_0038;
        Label_0029:
            base.ActivateOutputLinks(1);
            base.set_enabled(0);
        Label_0038:
            return;
        }

        public int DownloadAssetNum
        {
            get
            {
                return this.DownloadAssetNums;
            }
        }

        [CompilerGenerated]
        private sealed class <DownloadFloorParamAsync>c__IteratorB4 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<TowerFloorParam> <floorParams>__0;
            internal TowerFloorParam <floorParam>__1;
            internal List<TowerFloorParam> <downloadParams>__2;
            internal int <index>__3;
            internal bool <isOpen>__4;
            internal int <i>__5;
            internal int $PC;
            internal object $current;
            internal FlowNode_DownloadTowerMapSets <>f__this;

            public <DownloadFloorParamAsync>c__IteratorB4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__196(TowerFloorParam fp)
            {
                return (fp.iname == this.<floorParam>__1.iname);
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
                        goto Label_002D;

                    case 1:
                        goto Label_00EB;

                    case 2:
                        goto Label_0142;

                    case 3:
                        goto Label_0196;

                    case 4:
                        goto Label_01FF;
                }
                goto Label_0206;
            Label_002D:
                this.<floorParams>__0 = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
                this.<floorParam>__1 = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
                this.<downloadParams>__2 = new List<TowerFloorParam>(this.<>f__this.DownloadAssetNums);
                this.<index>__3 = this.<floorParams>__0.FindIndex(new Predicate<TowerFloorParam>(this.<>m__196));
                if (this.<index>__3 == -1)
                {
                    goto Label_00EB;
                }
                this.<downloadParams>__2 = this.<floorParams>__0.GetRange(this.<index>__3, Mathf.Min(this.<>f__this.DownloadAssetNums, this.<floorParams>__0.Count - this.<index>__3));
                goto Label_00EB;
            Label_00D4:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0208;
            Label_00EB:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_00D4;
                }
                this.<isOpen>__4 = 0;
                FlowNode_DownloadTowerMapSets.DownloadMapSets(this.<downloadParams>__2);
                if (AssetDownloader.isDone != null)
                {
                    goto Label_011D;
                }
                this.<isOpen>__4 = 1;
                ProgressWindow.OpenGenericDownloadWindow();
            Label_011D:
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_0142;
            Label_012B:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_0208;
            Label_0142:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_012B;
                }
                DownloadUtility.DownloadTowerQuests(this.<downloadParams>__2);
                if (AssetDownloader.isDone != null)
                {
                    goto Label_0171;
                }
                if (this.<isOpen>__4 != null)
                {
                    goto Label_0171;
                }
                ProgressWindow.OpenGenericDownloadWindow();
            Label_0171:
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_0196;
            Label_017F:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_0208;
            Label_0196:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_017F;
                }
                ProgressWindow.Close();
                this.<i>__5 = 0;
                goto Label_01D6;
            Label_01B1:
                this.<downloadParams>__2[this.<i>__5].DownLoaded = 1;
                this.<i>__5 += 1;
            Label_01D6:
                if (this.<i>__5 < this.<downloadParams>__2.Count)
                {
                    goto Label_01B1;
                }
                this.$current = null;
                this.$PC = 4;
                goto Label_0208;
            Label_01FF:
                this.$PC = -1;
            Label_0206:
                return 0;
            Label_0208:
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
        private sealed class <DownloadFloorParams>c__IteratorB3 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<TowerFloorParam> <floorParams>__0;
            internal TowerFloorParam <floorParam>__1;
            internal List<TowerFloorParam> <downloadParams>__2;
            internal int <index>__3;
            internal int <i>__4;
            internal int $PC;
            internal object $current;
            internal FlowNode_DownloadTowerMapSets <>f__this;

            public <DownloadFloorParams>c__IteratorB3()
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
                        goto Label_0067;

                    case 2:
                        goto Label_0175;
                }
                goto Label_017C;
            Label_0025:
                if (GameUtility.Config_UseAssetBundles.Value == null)
                {
                    goto Label_006C;
                }
                if (AssetManager.AssetRevision <= 0)
                {
                    goto Label_006C;
                }
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.DownloadFloorParamAsync());
                this.$PC = 1;
                goto Label_017E;
            Label_0067:
                goto Label_0149;
            Label_006C:
                this.<floorParams>__0 = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
                this.<floorParam>__1 = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
                this.<downloadParams>__2 = new List<TowerFloorParam>(this.<>f__this.DownloadAssetNums);
                this.<index>__3 = this.<floorParam>__1.FloorIndex;
                if (this.<index>__3 == -1)
                {
                    goto Label_0102;
                }
                this.<downloadParams>__2 = this.<floorParams>__0.GetRange(this.<index>__3, Mathf.Min(this.<>f__this.DownloadAssetNums, this.<floorParams>__0.Count - this.<index>__3));
            Label_0102:
                this.<i>__4 = 0;
                goto Label_0133;
            Label_010E:
                this.<downloadParams>__2[this.<i>__4].DownLoaded = 1;
                this.<i>__4 += 1;
            Label_0133:
                if (this.<i>__4 < this.<downloadParams>__2.Count)
                {
                    goto Label_010E;
                }
            Label_0149:
                this.<>f__this.ActivateOutputLinks(1);
                this.<>f__this.set_enabled(0);
                this.$current = null;
                this.$PC = 2;
                goto Label_017E;
            Label_0175:
                this.$PC = -1;
            Label_017C:
                return 0;
            Label_017E:
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

