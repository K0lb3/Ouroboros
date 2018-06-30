namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "Request2(リクエスト2)", 0, 1), NodeType("Gacha/ReqGachaPending", 0x7fe5), Pin(0, "Request(リクエスト)", 0, 0), Pin(10, "Success(リクエスト成功)", 1, 10), Pin(11, "Failued(リクエスト失敗)", 1, 11), Pin(12, "NoPending(保留中の引き直し召喚がない)", 1, 12)]
    public class FlowNode_ReqGachaPending : FlowNode_Network
    {
        private const int PIN_IN_REQUEST = 0;
        private const int PIN_IN_REQUEST_FORCE = 1;
        private const int PIN_OT_SUCCESS = 10;
        private const int PIN_OT_FAILUED = 11;
        private const int PIN_OT_NO_PENDING = 12;
        private List<string> DownloadUnits;
        private List<string> DownloadArtifacts;
        private List<string> DownloadConceptCard;

        public FlowNode_ReqGachaPending()
        {
            this.DownloadUnits = new List<string>();
            this.DownloadArtifacts = new List<string>();
            this.DownloadConceptCard = new List<string>();
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncGachaResultData(List<GachaDropData> drops)
        {
            <AsyncGachaResultData>c__IteratorC4 rc;
            rc = new <AsyncGachaResultData>c__IteratorC4();
            rc.drops = drops;
            rc.<$>drops = drops;
            rc.<>f__this = this;
            return rc;
        }

        private void ExecGacha(string _gname)
        {
            FlowNode_ExecGacha2 gacha;
            GachaRequestParam param;
            gacha = base.GetComponent<FlowNode_ExecGacha2>();
            if ((gacha != null) == null)
            {
                goto Label_0028;
            }
            param = new GachaRequestParam(_gname);
            if (param == null)
            {
                goto Label_0028;
            }
            gacha.OnExecGachaDecision(param);
            return;
        Label_0028:
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(11);
            return;
        }

        public override void OnActivate(int pinID)
        {
            base.set_enabled(1);
            base.ExecRequest(new ReqGachaPending(new Network.ResponseCallback(this.ResponseCallback)));
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            List<GachaDropData> list;
            Json_DropInfo info;
            Json_DropInfo[] infoArray;
            int num;
            GachaDropData data;
            List<GachaDropData> list2;
            Json_DropInfo info2;
            Json_DropInfo[] infoArray2;
            int num2;
            GachaDropData data2;
            int num3;
            GachaReceiptData data3;
            int num4;
            bool flag;
            string str;
            <OnSuccess>c__AnonStorey276 storey;
            Network.EErrCode code;
            storey = new <OnSuccess>c__AnonStorey276();
            storey.<>f__this = this;
            if (Network.IsError == null)
            {
                goto Label_0027;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0027:
            storey.res = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PendingGachaResponse>>(&www.text);
            DebugUtility.Assert((storey.res == null) == 0, "res == null");
            Network.RemoveAPI();
            if (storey.res.body != null)
            {
                goto Label_0068;
            }
            return;
        Label_0068:
            list = new List<GachaDropData>();
            infoArray = storey.res.body.add;
            num = 0;
            goto Label_00AB;
        Label_0087:
            info = infoArray[num];
            data = new GachaDropData();
            if (data.Deserialize(info) == null)
            {
                goto Label_00A7;
            }
            list.Add(data);
        Label_00A7:
            num += 1;
        Label_00AB:
            if (num < ((int) infoArray.Length))
            {
                goto Label_0087;
            }
            list2 = new List<GachaDropData>();
            if (storey.res.body.add_mail == null)
            {
                goto Label_0122;
            }
            infoArray2 = storey.res.body.add_mail;
            num2 = 0;
            goto Label_0117;
        Label_00EC:
            info2 = infoArray2[num2];
            data2 = new GachaDropData();
            if (data2.Deserialize(info2) == null)
            {
                goto Label_0111;
            }
            list2.Add(data2);
        Label_0111:
            num2 += 1;
        Label_0117:
            if (num2 < ((int) infoArray2.Length))
            {
                goto Label_00EC;
            }
        Label_0122:
            num3 = 0;
            goto Label_0158;
        Label_012A:
            if (list[num3].type == 4)
            {
                goto Label_0142;
            }
            goto Label_0152;
        Label_0142:
            GlobalVars.IsDirtyConceptCardData.Set(1);
            goto Label_0165;
        Label_0152:
            num3 += 1;
        Label_0158:
            if (num3 < list.Count)
            {
                goto Label_012A;
            }
        Label_0165:
            data3 = new GachaReceiptData();
            data3.iname = storey.res.body.gacha.gname;
            num4 = 0;
            if (storey.res.body.rest <= 0)
            {
                goto Label_01B6;
            }
            num4 = storey.res.body.rest;
        Label_01B6:
            GachaResultData.Init(list, list2, null, data3, 0, (num4 <= 0) ? 0 : 1, num4);
            flag = 0;
            if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == null)
            {
                goto Label_0261;
            }
            str = string.Empty;
            if (num4 <= 0)
            {
                goto Label_0235;
            }
            if (storey.res.body.gacha.time_end <= Network.GetServerTime())
            {
                goto Label_0224;
            }
            flag = 1;
            goto Label_0230;
        Label_0224:
            str = LocalizedText.Get("sys.GACHA_REDRAW_CAUTION_OUTOF_PERIOD");
        Label_0230:
            goto Label_0241;
        Label_0235:
            str = LocalizedText.Get("sys.GACHA_REDRAW_CAUTION_LIMIT_UPPER");
        Label_0241:
            if (flag != null)
            {
                goto Label_0261;
            }
            UIUtility.SystemMessage(str, new UIUtility.DialogResultEvent(storey.<>m__1BB), null, 1, -1);
            return;
        Label_0261:
            base.StartCoroutine(this.AsyncGachaResultData(list));
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }

        [CompilerGenerated]
        private sealed class <AsyncGachaResultData>c__IteratorC4 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<GachaDropData> drops;
            internal GameManager <gm>__0;
            internal List<GachaDropData>.Enumerator <$s_572>__1;
            internal GachaDropData <drop>__2;
            internal string <iname>__3;
            internal string <iname>__4;
            internal EItemType <type>__5;
            internal UnitParam <param>__6;
            internal string <iname>__7;
            internal string <iname>__8;
            internal int <i>__9;
            internal UnitParam <param>__10;
            internal int <i>__11;
            internal ArtifactParam <param>__12;
            internal int <i>__13;
            internal ConceptCardParam <param>__14;
            internal int $PC;
            internal object $current;
            internal List<GachaDropData> <$>drops;
            internal FlowNode_ReqGachaPending <>f__this;

            public <AsyncGachaResultData>c__IteratorC4()
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
                        goto Label_0025;

                    case 1:
                        goto Label_031E;

                    case 2:
                        goto Label_0594;
                }
                goto Label_05B5;
            Label_0025:
                if (this.drops == null)
                {
                    goto Label_05B5;
                }
                if (this.drops.Count >= 0)
                {
                    goto Label_0046;
                }
                goto Label_05B5;
            Label_0046:
                this.<>f__this.DownloadUnits.Clear();
                this.<>f__this.DownloadArtifacts.Clear();
                this.<>f__this.DownloadConceptCard.Clear();
                this.<gm>__0 = MonoSingleton<GameManager>.GetInstanceDirect();
                if ((this.<gm>__0 == null) == null)
                {
                    goto Label_0097;
                }
                goto Label_05B5;
            Label_0097:
                this.<$s_572>__1 = this.drops.GetEnumerator();
            Label_00A8:
                try
                {
                    goto Label_02E0;
                Label_00AD:
                    this.<drop>__2 = &this.<$s_572>__1.Current;
                    if (this.<drop>__2.type != 2)
                    {
                        goto Label_011C;
                    }
                    this.<iname>__3 = this.<drop>__2.unit.iname;
                    if (this.<>f__this.DownloadUnits.IndexOf(this.<iname>__3) != -1)
                    {
                        goto Label_02E0;
                    }
                    this.<>f__this.DownloadUnits.Add(this.<iname>__3);
                    goto Label_02E0;
                Label_011C:
                    if (this.<drop>__2.type != 1)
                    {
                        goto Label_01D3;
                    }
                    this.<iname>__4 = this.<drop>__2.item.iname;
                    this.<type>__5 = this.<drop>__2.item.type;
                    if (this.<type>__5 != 1)
                    {
                        goto Label_02E0;
                    }
                    this.<param>__6 = this.<gm>__0.MasterParam.GetUnitParamForPiece(this.<iname>__4, 1);
                    if (this.<param>__6 != null)
                    {
                        goto Label_0192;
                    }
                    goto Label_02E0;
                Label_0192:
                    if (this.<>f__this.DownloadUnits.IndexOf(this.<param>__6.iname) != -1)
                    {
                        goto Label_02E0;
                    }
                    this.<>f__this.DownloadUnits.Add(this.<param>__6.iname);
                    goto Label_02E0;
                Label_01D3:
                    if (this.<drop>__2.type != 3)
                    {
                        goto Label_0231;
                    }
                    this.<iname>__7 = this.<drop>__2.artifact.iname;
                    if (this.<>f__this.DownloadArtifacts.IndexOf(this.<iname>__7) != -1)
                    {
                        goto Label_02E0;
                    }
                    this.<>f__this.DownloadArtifacts.Add(this.<iname>__7);
                    goto Label_02E0;
                Label_0231:
                    if (this.<drop>__2.type != 4)
                    {
                        goto Label_02E0;
                    }
                    this.<iname>__8 = this.<drop>__2.conceptcard.iname;
                    if (this.<>f__this.DownloadConceptCard.IndexOf(this.<iname>__8) != -1)
                    {
                        goto Label_028A;
                    }
                    this.<>f__this.DownloadConceptCard.Add(this.<iname>__8);
                Label_028A:
                    if (this.<drop>__2.cardunit == null)
                    {
                        goto Label_02E0;
                    }
                    if (this.<>f__this.DownloadUnits.IndexOf(this.<drop>__2.cardunit.iname) != -1)
                    {
                        goto Label_02E0;
                    }
                    this.<>f__this.DownloadUnits.Add(this.<drop>__2.cardunit.iname);
                Label_02E0:
                    if (&this.<$s_572>__1.MoveNext() != null)
                    {
                        goto Label_00AD;
                    }
                    goto Label_0306;
                }
                finally
                {
                Label_02F5:
                    ((List<GachaDropData>.Enumerator) this.<$s_572>__1).Dispose();
                }
            Label_0306:
                goto Label_031E;
            Label_030B:
                this.$current = null;
                this.$PC = 1;
                goto Label_05B7;
            Label_031E:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_030B;
                }
                if (this.<>f__this.DownloadUnits == null)
                {
                    goto Label_03E6;
                }
                if (this.<>f__this.DownloadUnits.Count <= 0)
                {
                    goto Label_03E6;
                }
                this.<i>__9 = 0;
                goto Label_03CB;
            Label_035A:
                if (string.IsNullOrEmpty(this.<>f__this.DownloadUnits[this.<i>__9]) != null)
                {
                    goto Label_03BD;
                }
                this.<param>__10 = this.<gm>__0.MasterParam.GetUnitParam(this.<>f__this.DownloadUnits[this.<i>__9]);
                if (this.<param>__10 == null)
                {
                    goto Label_03BD;
                }
                DownloadUtility.DownloadUnit(this.<param>__10, null);
            Label_03BD:
                this.<i>__9 += 1;
            Label_03CB:
                if (this.<i>__9 < this.<>f__this.DownloadUnits.Count)
                {
                    goto Label_035A;
                }
            Label_03E6:
                if (this.<>f__this.DownloadArtifacts == null)
                {
                    goto Label_04A3;
                }
                if (this.<>f__this.DownloadArtifacts.Count <= 0)
                {
                    goto Label_04A3;
                }
                this.<i>__11 = 0;
                goto Label_0488;
            Label_0418:
                if (string.IsNullOrEmpty(this.<>f__this.DownloadArtifacts[this.<i>__11]) != null)
                {
                    goto Label_047A;
                }
                this.<param>__12 = this.<gm>__0.MasterParam.GetArtifactParam(this.<>f__this.DownloadArtifacts[this.<i>__11]);
                if (this.<param>__12 == null)
                {
                    goto Label_047A;
                }
                DownloadUtility.DownloadArtifact(this.<param>__12);
            Label_047A:
                this.<i>__11 += 1;
            Label_0488:
                if (this.<i>__11 < this.<>f__this.DownloadArtifacts.Count)
                {
                    goto Label_0418;
                }
            Label_04A3:
                if (this.<>f__this.DownloadConceptCard == null)
                {
                    goto Label_0560;
                }
                if (this.<>f__this.DownloadConceptCard.Count <= 0)
                {
                    goto Label_0560;
                }
                this.<i>__13 = 0;
                goto Label_0545;
            Label_04D5:
                if (string.IsNullOrEmpty(this.<>f__this.DownloadConceptCard[this.<i>__13]) != null)
                {
                    goto Label_0537;
                }
                this.<param>__14 = this.<gm>__0.MasterParam.GetConceptCardParam(this.<>f__this.DownloadConceptCard[this.<i>__13]);
                if (this.<param>__14 == null)
                {
                    goto Label_0537;
                }
                DownloadUtility.DownloadConceptCard(this.<param>__14);
            Label_0537:
                this.<i>__13 += 1;
            Label_0545:
                if (this.<i>__13 < this.<>f__this.DownloadConceptCard.Count)
                {
                    goto Label_04D5;
                }
            Label_0560:
                if (AssetDownloader.isDone != null)
                {
                    goto Label_056F;
                }
                ProgressWindow.OpenGenericDownloadWindow();
            Label_056F:
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_0594;
            Label_057D:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_05B7;
            Label_0594:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_057D;
                }
                ProgressWindow.Close();
                this.<>f__this.Success();
                this.$PC = -1;
            Label_05B5:
                return 0;
            Label_05B7:
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
        private sealed class <OnSuccess>c__AnonStorey276
        {
            internal WebAPI.JSON_BodyResponse<Json_PendingGachaResponse> res;
            internal FlowNode_ReqGachaPending <>f__this;

            public <OnSuccess>c__AnonStorey276()
            {
                base..ctor();
                return;
            }

            internal void <>m__1BB(GameObject go)
            {
                this.<>f__this.ExecGacha(this.res.body.gacha.gname);
                return;
            }
        }
    }
}

