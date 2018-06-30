namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0, "ダウンロード開始", 0, 0), NodeType("Download/MultiTower", 0x7fe5), Pin(1, "Success", 1, 10)]
    public class FlowNode_MultiTowerDownload : FlowNode
    {
        [SerializeField]
        private int DownloadAssetNums;

        public FlowNode_MultiTowerDownload()
        {
            this.DownloadAssetNums = 10;
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator DownloadFloorParamAsync()
        {
            <DownloadFloorParamAsync>c__IteratorC0 rc;
            rc = new <DownloadFloorParamAsync>c__IteratorC0();
            rc.<>f__this = this;
            return rc;
        }

        public static void DownloadMapSets(List<MultiTowerFloorParam> floorParams)
        {
            int num;
            int num2;
            string str;
            if (floorParams != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            goto Label_0073;
        Label_000E:
            if (floorParams[num].map == null)
            {
                goto Label_006F;
            }
            num2 = 0;
            goto Label_0058;
        Label_0026:
            str = floorParams[num].map[num2].mapSetName;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0054;
            }
            AssetManager.PrepareAssets(AssetPath.LocalMap(str));
        Label_0054:
            num2 += 1;
        Label_0058:
            if (num2 < floorParams[num].map.Count)
            {
                goto Label_0026;
            }
        Label_006F:
            num += 1;
        Label_0073:
            if (num < floorParams.Count)
            {
                goto Label_000E;
            }
            return;
        }

        private void DownloadPlacementAsset(MultiTowerFloorParam param)
        {
            int num;
            string str;
            string str2;
            JSON_MapUnit unit;
            int num2;
            if (param.map != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_009D;
        Label_0013:
            if (string.IsNullOrEmpty(param.map[num].mapSetName) == null)
            {
                goto Label_0033;
            }
            goto Label_0099;
        Label_0033:
            str2 = AssetManager.LoadTextData(AssetPath.LocalMap(param.map[num].mapSetName));
            if (str2 != null)
            {
                goto Label_005C;
            }
            goto Label_0099;
        Label_005C:
            unit = JSONParser.parseJSONObject<JSON_MapUnit>(str2);
            if (unit == null)
            {
                goto Label_0099;
            }
            num2 = 0;
            goto Label_008A;
        Label_0071:
            DownloadUtility.LoadUnitIconMedium(unit.enemy[num2].iname);
            num2 += 1;
        Label_008A:
            if (num2 < ((int) unit.enemy.Length))
            {
                goto Label_0071;
            }
        Label_0099:
            num += 1;
        Label_009D:
            if (num < param.map.Count)
            {
                goto Label_0013;
            }
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0048;
            }
            base.set_enabled(1);
            if (GameUtility.Config_UseAssetBundles.Value == null)
            {
                goto Label_0039;
            }
            if (AssetManager.AssetRevision <= 0)
            {
                goto Label_0039;
            }
            base.StartCoroutine(this.DownloadFloorParamAsync());
            goto Label_0048;
        Label_0039:
            base.ActivateOutputLinks(1);
            base.set_enabled(0);
        Label_0048:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RequestDownloadFloors(List<MultiTowerFloorParam> floorParams)
        {
            <RequestDownloadFloors>c__IteratorC1 rc;
            rc = new <RequestDownloadFloors>c__IteratorC1();
            rc.floorParams = floorParams;
            rc.<$>floorParams = floorParams;
            rc.<>f__this = this;
            return rc;
        }

        public int DownloadAssetNum
        {
            get
            {
                return this.DownloadAssetNums;
            }
        }

        [CompilerGenerated]
        private sealed class <DownloadFloorParamAsync>c__IteratorC0 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal bool <isOpen>__1;
            internal List<MultiTowerFloorParam> <floors>__2;
            internal int $PC;
            internal object $current;
            internal FlowNode_MultiTowerDownload <>f__this;

            public <DownloadFloorParamAsync>c__IteratorC0()
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
                        goto Label_0031;

                    case 1:
                        goto Label_0075;

                    case 2:
                        goto Label_00C5;

                    case 3:
                        goto Label_00FD;

                    case 4:
                        goto Label_013C;

                    case 5:
                        goto Label_0177;
                }
                goto Label_017E;
            Label_0031:
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                this.<isOpen>__1 = 0;
                this.<floors>__2 = this.<gm>__0.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
                goto Label_0075;
            Label_005E:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0180;
            Label_0075:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_005E;
                }
                FlowNode_MultiTowerDownload.DownloadMapSets(this.<floors>__2);
                if (AssetDownloader.isDone != null)
                {
                    goto Label_00A0;
                }
                this.<isOpen>__1 = 1;
                ProgressWindow.OpenGenericDownloadWindow();
            Label_00A0:
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_00C5;
            Label_00AE:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_0180;
            Label_00C5:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_00AE;
                }
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.RequestDownloadFloors(this.<floors>__2));
                this.$PC = 3;
                goto Label_0180;
            Label_00FD:
                if (AssetDownloader.isDone != null)
                {
                    goto Label_0117;
                }
                if (this.<isOpen>__1 != null)
                {
                    goto Label_0117;
                }
                ProgressWindow.OpenGenericDownloadWindow();
            Label_0117:
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_013C;
            Label_0125:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 4;
                goto Label_0180;
            Label_013C:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0125;
                }
                ProgressWindow.Close();
                this.<>f__this.ActivateOutputLinks(1);
                this.<>f__this.set_enabled(0);
                this.$current = null;
                this.$PC = 5;
                goto Label_0180;
            Label_0177:
                this.$PC = -1;
            Label_017E:
                return 0;
            Label_0180:
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
        private sealed class <RequestDownloadFloors>c__IteratorC1 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<MultiTowerFloorParam> floorParams;
            internal int <i>__0;
            internal int $PC;
            internal object $current;
            internal List<MultiTowerFloorParam> <$>floorParams;
            internal FlowNode_MultiTowerDownload <>f__this;

            public <RequestDownloadFloors>c__IteratorC1()
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
                        goto Label_0047;

                    case 2:
                        goto Label_0086;

                    case 3:
                        goto Label_00BD;
                }
                goto Label_00C4;
            Label_0029:
                if (this.floorParams != null)
                {
                    goto Label_0047;
                }
                this.$current = null;
                this.$PC = 1;
                goto Label_00C6;
            Label_0047:
                this.<i>__0 = 0;
                goto Label_0094;
            Label_0053:
                this.<>f__this.DownloadPlacementAsset(this.floorParams[this.<i>__0]);
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_00C6;
            Label_0086:
                this.<i>__0 += 1;
            Label_0094:
                if (this.<i>__0 < this.floorParams.Count)
                {
                    goto Label_0053;
                }
                this.$current = null;
                this.$PC = 3;
                goto Label_00C6;
            Label_00BD:
                this.$PC = -1;
            Label_00C4:
                return 0;
            Label_00C6:
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

