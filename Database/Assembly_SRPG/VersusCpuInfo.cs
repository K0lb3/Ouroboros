namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "Refresh", 0, 100), Pin(200, "Selected", 1, 200)]
    public class VersusCpuInfo : MonoBehaviour, IFlowInterface
    {
        public ListItemEvents CpuPlayerTemplate;
        public GameObject CpuList;
        public GameObject MapInfo;
        public GameObject PartyInfo;
        public Color[] RankColor;
        private List<ListItemEvents> mVersusMember;

        public VersusCpuInfo()
        {
            this.RankColor = new Color[0];
            this.mVersusMember = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0015;
            }
            base.StartCoroutine(this.RefreshEnemy());
        Label_0015:
            return;
        }

        private void Awake()
        {
            GlobalVars.SelectedPartyIndex.Set(7);
            return;
        }

        [DebuggerHidden]
        private IEnumerator DownloadUnitImage()
        {
            <DownloadUnitImage>c__Iterator17C iteratorc;
            iteratorc = new <DownloadUnitImage>c__Iterator17C();
            return iteratorc;
        }

        private void OnOpenDetail(GameObject _go)
        {
            VersusCpuData data;
            UnitData data2;
            data2 = DataSource.FindDataOfClass<VersusCpuData>(_go, null).Units[0];
            if (data2 == null)
            {
                goto Label_0020;
            }
            data2.ShowTooltip(_go, 0, null);
        Label_0020:
            return;
        }

        private void OnSelect(GameObject go)
        {
            VersusCpuData data;
            data = DataSource.FindDataOfClass<VersusCpuData>(go, null);
            if (data != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            MonoSingleton<GameManager>.Instance.IsVSCpuBattle = 1;
            GlobalVars.VersusCpu.Set(data);
            FlowNode_GameObject.ActivateOutputLinks(this, 200);
            return;
        }

        private void RefreshData()
        {
            GameManager manager;
            QuestParam param;
            PartyData data;
            manager = MonoSingleton<GameManager>.Instance;
            if ((this.MapInfo != null) == null)
            {
                goto Label_0040;
            }
            param = manager.FindQuest(GlobalVars.SelectedQuestID);
            if (param == null)
            {
                goto Label_0040;
            }
            DataSource.Bind<QuestParam>(this.MapInfo, param);
            GameParameter.UpdateAll(this.MapInfo);
        Label_0040:
            if ((this.PartyInfo != null) == null)
            {
                goto Label_0094;
            }
            GlobalVars.SelectedPartyIndex.Set(7);
            data = manager.Player.Partys[GlobalVars.SelectedPartyIndex];
            if (data == null)
            {
                goto Label_0094;
            }
            DataSource.Bind<PartyData>(this.PartyInfo, data);
            GameParameter.UpdateAll(this.PartyInfo);
        Label_0094:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshEnemy()
        {
            <RefreshEnemy>c__Iterator17B iteratorb;
            iteratorb = new <RefreshEnemy>c__Iterator17B();
            iteratorb.<>f__this = this;
            return iteratorb;
        }

        private void Start()
        {
            if ((this.CpuPlayerTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.CpuPlayerTemplate.get_gameObject().SetActive(0);
        Label_0022:
            this.RefreshData();
            return;
        }

        private void Update()
        {
        }

        [CompilerGenerated]
        private sealed class <DownloadUnitImage>c__Iterator17C : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<VersusCpuData> <cpuData>__0;
            internal int <i>__1;
            internal int $PC;
            internal object $current;

            public <DownloadUnitImage>c__Iterator17C()
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
                        goto Label_0159;
                }
                goto Label_016F;
            Label_0021:
                this.<cpuData>__0 = MonoSingleton<GameManager>.Instance.VersusCpuData;
                this.<i>__1 = 0;
                goto Label_010F;
            Label_003D:
                AssetManager.PrepareAssets(AssetPath.UnitSkinImage(this.<cpuData>__0[this.<i>__1].Units[0].UnitParam, this.<cpuData>__0[this.<i>__1].Units[0].GetSelectedSkin(-1), this.<cpuData>__0[this.<i>__1].Units[0].CurrentJobId));
                AssetManager.PrepareAssets(AssetPath.UnitSkinImage2(this.<cpuData>__0[this.<i>__1].Units[0].UnitParam, this.<cpuData>__0[this.<i>__1].Units[0].GetSelectedSkin(-1), this.<cpuData>__0[this.<i>__1].Units[0].CurrentJobId));
                this.<i>__1 += 1;
            Label_010F:
                if (this.<i>__1 < this.<cpuData>__0.Count)
                {
                    goto Label_003D;
                }
                if (AssetDownloader.isDone != null)
                {
                    goto Label_0159;
                }
                ProgressWindow.OpenGenericDownloadWindow();
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_0159;
            Label_0142:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0171;
            Label_0159:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0142;
                }
                ProgressWindow.Close();
                this.$PC = -1;
            Label_016F:
                return 0;
            Label_0171:
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
        private sealed class <RefreshEnemy>c__Iterator17B : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal List<VersusCpuData> <cpuData>__1;
            internal int <i>__2;
            internal ListItemEvents <item>__3;
            internal Transform <bg>__4;
            internal Image <img>__5;
            internal Transform <rank>__6;
            internal ImageArray <img>__7;
            internal int $PC;
            internal object $current;
            internal VersusCpuInfo <>f__this;

            public <RefreshEnemy>c__Iterator17B()
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

            public unsafe bool MoveNext()
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
                        goto Label_007A;
                }
                goto Label_02E9;
            Label_0021:
                if ((this.<>f__this.CpuPlayerTemplate == null) != null)
                {
                    goto Label_02E9;
                }
                if ((this.<>f__this.CpuList == null) == null)
                {
                    goto Label_0052;
                }
                goto Label_02E9;
            Label_0052:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.DownloadUnitImage());
                this.$PC = 1;
                goto Label_02EB;
            Label_007A:
                this.<i>__0 = 0;
                goto Label_00B4;
            Label_0086:
                Object.Destroy(this.<>f__this.mVersusMember[this.<i>__0].get_gameObject());
                this.<i>__0 += 1;
            Label_00B4:
                if (this.<i>__0 < this.<>f__this.mVersusMember.Count)
                {
                    goto Label_0086;
                }
                this.<>f__this.mVersusMember.Clear();
                this.<cpuData>__1 = MonoSingleton<GameManager>.Instance.VersusCpuData;
                this.<i>__2 = 0;
                goto Label_02CC;
            Label_00FB:
                this.<item>__3 = Object.Instantiate<ListItemEvents>(this.<>f__this.CpuPlayerTemplate);
                if ((this.<item>__3 != null) == null)
                {
                    goto Label_02BE;
                }
                DataSource.Bind<VersusCpuData>(this.<item>__3.get_gameObject(), this.<cpuData>__1[this.<i>__2]);
                this.<item>__3.get_transform().SetParent(this.<>f__this.CpuList.get_transform(), 0);
                this.<item>__3.get_gameObject().SetActive(1);
                this.<item>__3.OnSelect = new ListItemEvents.ListItemEvent(this.<>f__this.OnSelect);
                this.<item>__3.OnOpenDetail = new ListItemEvents.ListItemEvent(this.<>f__this.OnOpenDetail);
                GameParameter.UpdateAll(this.<item>__3.get_gameObject());
                this.<bg>__4 = this.<item>__3.get_transform().FindChild("mask/lank_bg");
                if ((this.<bg>__4 != null) == null)
                {
                    goto Label_0249;
                }
                this.<img>__5 = this.<bg>__4.GetComponent<Image>();
                if ((this.<img>__5 != null) == null)
                {
                    goto Label_0249;
                }
                if (this.<i>__2 >= ((int) this.<>f__this.RankColor.Length))
                {
                    goto Label_0249;
                }
                this.<img>__5.set_color(*(&(this.<>f__this.RankColor[this.<i>__2])));
            Label_0249:
                this.<rank>__6 = this.<item>__3.get_transform().FindChild("icon_Lank");
                if ((this.<rank>__6 != null) == null)
                {
                    goto Label_02A8;
                }
                this.<img>__7 = this.<rank>__6.GetComponent<ImageArray>();
                if ((this.<img>__7 != null) == null)
                {
                    goto Label_02A8;
                }
                this.<img>__7.ImageIndex = this.<i>__2;
            Label_02A8:
                this.<>f__this.mVersusMember.Add(this.<item>__3);
            Label_02BE:
                this.<i>__2 += 1;
            Label_02CC:
                if (this.<i>__2 < this.<cpuData>__1.Count)
                {
                    goto Label_00FB;
                }
                this.$PC = -1;
            Label_02E9:
                return 0;
            Label_02EB:
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

