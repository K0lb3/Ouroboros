namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(100, "Finish", 1, 100), Pin(0x65, "Error", 1, 0x65), NodeType("Multi/VersusUnitDownload", 0x7fe5), Pin(0, "Start", 0, 0), Pin(1, "StartAudience", 0, 1)]
    public class FlowNode_VersusUnitDownload : FlowNode
    {
        public FlowNode_VersusUnitDownload()
        {
            base..ctor();
            return;
        }

        private void AddAssets(JSON_MyPhotonPlayerParam param)
        {
            if (param == null)
            {
                goto Label_0047;
            }
            AssetManager.PrepareAssets(AssetPath.UnitSkinImage(param.units[0].unit.UnitParam, param.units[0].unit.GetSelectedSkin(-1), param.units[0].unit.CurrentJobId));
        Label_0047:
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncDownload()
        {
            <AsyncDownload>c__IteratorCF rcf;
            rcf = new <AsyncDownload>c__IteratorCF();
            rcf.<>f__this = this;
            return rcf;
        }

        public override void OnActivate(int pinID)
        {
            JSON_MyPhotonPlayerParam param;
            List<MyPhoton.MyPlayer> list;
            MyPhoton.MyPlayer player;
            GameManager manager;
            JSON_MyPhotonRoomParam param2;
            int num;
            <OnActivate>c__AnonStorey280 storey;
            if (GameUtility.Config_UseAssetBundles.Value != null)
            {
                goto Label_0019;
            }
            base.ActivateOutputLinks(100);
            return;
        Label_0019:
            param = null;
            if (pinID != null)
            {
                goto Label_0097;
            }
            storey = new <OnActivate>c__AnonStorey280();
            storey.pt = PunMonoSingleton<MyPhoton>.Instance;
            if ((storey.pt != null) == null)
            {
                goto Label_0114;
            }
            list = storey.pt.GetRoomPlayerList();
            if (list == null)
            {
                goto Label_0114;
            }
            if (list.Count <= 1)
            {
                goto Label_0114;
            }
            player = list.Find(new Predicate<MyPhoton.MyPlayer>(storey.<>m__1D2));
            if (player == null)
            {
                goto Label_0114;
            }
            param = JSON_MyPhotonPlayerParam.Parse(player.json);
            this.AddAssets(param);
            goto Label_0114;
        Label_0097:
            if (pinID != 1)
            {
                goto Label_0114;
            }
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.AudienceRoom == null)
            {
                goto Label_0114;
            }
            param2 = JSON_MyPhotonRoomParam.Parse(manager.AudienceRoom.json);
            if (param2 == null)
            {
                goto Label_0114;
            }
            num = 0;
            goto Label_0104;
        Label_00D0:
            if (param2.players[num] == null)
            {
                goto Label_00FE;
            }
            param2.players[num].SetupUnits();
            this.AddAssets(param2.players[num]);
        Label_00FE:
            num += 1;
        Label_0104:
            if (num < ((int) param2.players.Length))
            {
                goto Label_00D0;
            }
        Label_0114:
            base.StartCoroutine(this.AsyncDownload());
            return;
        }

        [CompilerGenerated]
        private sealed class <AsyncDownload>c__IteratorCF : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal FlowNode_VersusUnitDownload <>f__this;

            public <AsyncDownload>c__IteratorCF()
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
                        goto Label_004E;

                    case 2:
                        goto Label_0079;

                    case 3:
                        goto Label_00A4;
                }
                goto Label_00AB;
            Label_0029:
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_0079;
            Label_0037:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_00AD;
            Label_004E:
                if (AssetDownloader.HasError == null)
                {
                    goto Label_0079;
                }
                this.<>f__this.ActivateOutputLinks(0x65);
                this.$current = null;
                this.$PC = 2;
                goto Label_00AD;
            Label_0079:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0037;
                }
                this.<>f__this.ActivateOutputLinks(100);
                this.$current = null;
                this.$PC = 3;
                goto Label_00AD;
            Label_00A4:
                this.$PC = -1;
            Label_00AB:
                return 0;
            Label_00AD:
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
        private sealed class <OnActivate>c__AnonStorey280
        {
            internal MyPhoton pt;

            public <OnActivate>c__AnonStorey280()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1D2(MyPhoton.MyPlayer p)
            {
                return ((p.playerID == this.pt.GetMyPlayer().playerID) == 0);
            }
        }
    }
}

