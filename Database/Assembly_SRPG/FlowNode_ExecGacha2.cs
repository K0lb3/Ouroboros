namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [Pin(4, "Success", 1, 4), Pin(40, "Comp Gacha", 0, 40), Pin(8, "有償幻晶石不足", 1, 8), Pin(7, "幻晶石不足", 1, 7), NodeType("System/ExecGacha2", 0x7fe5), Pin(10, "Single Gacha", 0, 10), Pin(6, "ゼニー不足", 1, 6), Pin(20, "Multi Gacha", 0, 20), Pin(9, "有償召喚リセット待ち", 1, 9), Pin(11, "Success(引き直し召喚確定)", 1, 11), Pin(12, "Error(引き直し召喚の期間外実行)", 1, 12), Pin(5, "Failed", 1, 5), Pin(30, "Free Gacha", 0, 30)]
    public class FlowNode_ExecGacha2 : FlowNode_Network
    {
        private const int PIN_OT_SUCCESS_DECISION_REDRAW = 11;
        private const int PIN_OT_ERROR_OUT_OF_PERIOD = 12;
        private GachaTypes mCurrentGachaType;
        private bool mUseOneMore;
        private List<string> DownloadUnits;
        private List<string> DownloadArtifacts;
        private List<string> DownloadConceptCards;
        private List<AssetList.Item> mQueue;
        [CompilerGenerated]
        private ExecType <API>k__BackingField;

        public FlowNode_ExecGacha2()
        {
            this.DownloadUnits = new List<string>();
            this.DownloadArtifacts = new List<string>();
            this.DownloadConceptCards = new List<string>();
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncGachaResultData(List<GachaDropData> drops)
        {
            <AsyncGachaResultData>c__IteratorB7 rb;
            rb = new <AsyncGachaResultData>c__IteratorB7();
            rb.drops = drops;
            rb.<$>drops = drops;
            rb.<>f__this = this;
            return rb;
        }

        private void ExecGacha(string iname, int is_free, int num, int is_decision)
        {
            base.ExecRequest(new ReqGachaExec(iname, new Network.ResponseCallback(this.ResponseCallback), is_free, num, is_decision));
            base.set_enabled(1);
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            this.mUseOneMore = 0;
            base.ActivateOutputLinks(5);
            return;
        }

        public void OnExecGacha(GachaRequestParam _rparam)
        {
            if (_rparam != null)
            {
                goto Label_000C;
            }
            this.Failure();
        Label_000C:
            this.mUseOneMore = _rparam.IsUseOneMore;
            this.API = 1;
            this.Request(_rparam);
            return;
        }

        public void OnExecGachaDecision(GachaRequestParam _rparam)
        {
            if (_rparam != null)
            {
                goto Label_000C;
            }
            this.Failure();
        Label_000C:
            this.API = 2;
            this.ExecGacha(_rparam.Iname, (_rparam.IsFree == null) ? 0 : 1, (_rparam.IsTicketGacha == null) ? 0 : _rparam.Num, 1);
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_GachaResult> response;
            ItemData data;
            int num;
            Exception exception;
            int num2;
            List<int> list;
            List<GachaDropData> list2;
            Json_DropInfo info;
            Json_DropInfo[] infoArray;
            int num3;
            GachaDropData data2;
            List<GachaDropData> list3;
            Json_DropInfo info2;
            Json_DropInfo[] infoArray2;
            int num4;
            GachaDropData data3;
            int num5;
            GachaReceiptData data4;
            string str;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_006D;
            }
            code = Network.ErrCode;
            switch ((code - 0xfa0))
            {
                case 0:
                    goto Label_0043;

                case 1:
                    goto Label_004A;

                case 2:
                    goto Label_0051;

                case 3:
                    goto Label_0032;

                case 4:
                    goto Label_0058;
            }
        Label_0032:
            if (code == 0xfaa)
            {
                goto Label_005F;
            }
            goto Label_0066;
        Label_0043:
            this.OnFailed();
            return;
        Label_004A:
            this.OnBack();
            return;
        Label_0051:
            this.OnBack();
            return;
        Label_0058:
            this.PaidGachaLimitOver();
            return;
        Label_005F:
            this.OutofPeriod();
            return;
        Label_0066:
            this.OnRetry();
            return;
        Label_006D:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaResult>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID("IT_US_SUMMONS_01");
            num = (data != null) ? data.Num : 0;
        Label_00B8:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body);
                goto Label_00E4;
            }
            catch (Exception exception1)
            {
            Label_00CD:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.Failure();
                goto Label_0324;
            }
        Label_00E4:
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID("IT_US_SUMMONS_01");
            num2 = (data != null) ? data.Num : 0;
            list = new List<int>();
            list.Add(num);
            list.Add(num2);
            list2 = new List<GachaDropData>();
            if (response.body.add == null)
            {
                goto Label_019A;
            }
            if (((int) response.body.add.Length) <= 0)
            {
                goto Label_019A;
            }
            infoArray = response.body.add;
            num3 = 0;
            goto Label_018F;
        Label_0164:
            info = infoArray[num3];
            data2 = new GachaDropData();
            if (data2.Deserialize(info) == null)
            {
                goto Label_0189;
            }
            list2.Add(data2);
        Label_0189:
            num3 += 1;
        Label_018F:
            if (num3 < ((int) infoArray.Length))
            {
                goto Label_0164;
            }
        Label_019A:
            list3 = new List<GachaDropData>();
            if (response.body.add_mail == null)
            {
                goto Label_01FC;
            }
            infoArray2 = response.body.add_mail;
            num4 = 0;
            goto Label_01F1;
        Label_01C6:
            info2 = infoArray2[num4];
            data3 = new GachaDropData();
            if (data3.Deserialize(info2) == null)
            {
                goto Label_01EB;
            }
            list3.Add(data3);
        Label_01EB:
            num4 += 1;
        Label_01F1:
            if (num4 < ((int) infoArray2.Length))
            {
                goto Label_01C6;
            }
        Label_01FC:
            num5 = 0;
            goto Label_0233;
        Label_0204:
            if (list2[num5].type == 4)
            {
                goto Label_021D;
            }
            goto Label_022D;
        Label_021D:
            GlobalVars.IsDirtyConceptCardData.Set(1);
            goto Label_0241;
        Label_022D:
            num5 += 1;
        Label_0233:
            if (num5 < list2.Count)
            {
                goto Label_0204;
            }
        Label_0241:
            data4 = new GachaReceiptData();
            data4.Deserialize(response.body.receipt);
            GachaResultData.Init(list2, list3, list, data4, this.mUseOneMore, response.body.is_pending, response.body.rest);
            if (GachaResultData.IsRedrawGacha == null)
            {
                goto Label_02A4;
            }
            if (GachaResultData.IsRedrawGacha == null)
            {
                goto Label_02EE;
            }
            if (this.API != 2)
            {
                goto Label_02EE;
            }
        Label_02A4:
            MyMetaps.TrackSpend(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data4.type), data4.iname, data4.val);
            MonoSingleton<GameManager>.Instance.Player.OnGacha(this.mCurrentGachaType, list2.Count);
        Label_02EE:
            if (this.API != 2)
            {
                goto Label_0315;
            }
            FlowNode_Variable.Set("REDRAW_GACHA_PENDING", string.Empty);
            GachaResultData.Reset();
            this.Success();
            return;
        Label_0315:
            base.StartCoroutine(this.AsyncGachaResultData(list2));
        Label_0324:
            return;
        }

        private void OutofPeriod()
        {
            if (this.API != 2)
            {
                goto Label_0017;
            }
            this.OnFailed();
            goto Label_0031;
        Label_0017:
            base.set_enabled(0);
            Network.RemoveAPI();
            Network.ResetError();
            base.ActivateOutputLinks(12);
        Label_0031:
            return;
        }

        private void PaidGachaLimitOver()
        {
            base.set_enabled(0);
            Network.RemoveAPI();
            Network.ResetError();
            base.ActivateOutputLinks(9);
            return;
        }

        public void Request(GachaRequestParam _param)
        {
            if (_param.IsRedrawConfirm != null)
            {
                goto Label_00E5;
            }
            if ((_param.IsPaid == null) || (MonoSingleton<GameManager>.Instance.Player.PaidCoin >= _param.Cost))
            {
                goto Label_0040;
            }
            base.ActivateOutputLinks(8);
            base.set_enabled(0);
            return;
        Label_0040:
            if ((_param.IsTicketGacha != null) || (_param.IsFree != null))
            {
                goto Label_00C7;
            }
            if (_param.CostType != 3)
            {
                goto Label_0091;
            }
            if (MonoSingleton<GameManager>.Instance.Player.Gold >= _param.Cost)
            {
                goto Label_00C7;
            }
            base.ActivateOutputLinks(6);
            base.set_enabled(0);
            return;
            goto Label_00C7;
        Label_0091:
            if ((_param.CostType != 1) || (MonoSingleton<GameManager>.Instance.Player.Coin >= _param.Cost))
            {
                goto Label_00C7;
            }
            base.ActivateOutputLinks(7);
            base.set_enabled(0);
            return;
        Label_00C7:
            if (_param.IsGold == null)
            {
                goto Label_00DE;
            }
            this.mCurrentGachaType = 0;
            goto Label_00E5;
        Label_00DE:
            this.mCurrentGachaType = 1;
        Label_00E5:
            this.ExecGacha(_param.Iname, (_param.IsFree == null) ? 0 : 1, (_param.IsTicketGacha == null) ? 0 : _param.Num, 0);
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            if (this.API != 1)
            {
                goto Label_0020;
            }
            base.ActivateOutputLinks(4);
            goto Label_0035;
        Label_0020:
            if (this.API != 2)
            {
                goto Label_0035;
            }
            base.ActivateOutputLinks(11);
        Label_0035:
            return;
        }

        private ExecType API
        {
            [CompilerGenerated]
            get
            {
                return this.<API>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<API>k__BackingField = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <AsyncGachaResultData>c__IteratorB7 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<GachaDropData> drops;
            internal GameManager <gm>__0;
            internal List<GachaDropData>.Enumerator <$s_531>__1;
            internal GachaDropData <drop>__2;
            internal string <iname>__3;
            internal string <iname>__4;
            internal EItemType <type>__5;
            internal UnitParam <unit_param>__6;
            internal string <unit_iname>__7;
            internal string <iname>__8;
            internal string <iname>__9;
            internal int <i>__10;
            internal UnitParam <unit>__11;
            internal int <i>__12;
            internal ArtifactParam <artifact>__13;
            internal int <i>__14;
            internal ConceptCardParam <conceptcard>__15;
            internal int $PC;
            internal object $current;
            internal List<GachaDropData> <$>drops;
            internal FlowNode_ExecGacha2 <>f__this;

            public <AsyncGachaResultData>c__IteratorB7()
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
                        goto Label_02DF;

                    case 2:
                        goto Label_04EF;
                }
                goto Label_0510;
            Label_0025:
                if (this.drops == null)
                {
                    goto Label_0510;
                }
                if (this.drops.Count >= 0)
                {
                    goto Label_0046;
                }
                goto Label_0510;
            Label_0046:
                this.<gm>__0 = MonoSingleton<GameManager>.GetInstanceDirect();
                this.<$s_531>__1 = this.drops.GetEnumerator();
            Label_0062:
                try
                {
                    goto Label_02A1;
                Label_0067:
                    this.<drop>__2 = &this.<$s_531>__1.Current;
                    if (this.<drop>__2.type != 2)
                    {
                        goto Label_00D6;
                    }
                    this.<iname>__3 = this.<drop>__2.unit.iname;
                    if (this.<>f__this.DownloadUnits.IndexOf(this.<iname>__3) != -1)
                    {
                        goto Label_02A1;
                    }
                    this.<>f__this.DownloadUnits.Add(this.<iname>__3);
                    goto Label_02A1;
                Label_00D6:
                    if (this.<drop>__2.type != 1)
                    {
                        goto Label_0194;
                    }
                    this.<iname>__4 = this.<drop>__2.item.iname;
                    this.<type>__5 = this.<drop>__2.item.type;
                    if (this.<type>__5 != 1)
                    {
                        goto Label_02A1;
                    }
                    this.<unit_param>__6 = this.<gm>__0.MasterParam.GetUnitParamForPiece(this.<iname>__4, 1);
                    if (this.<unit_param>__6 != null)
                    {
                        goto Label_014C;
                    }
                    goto Label_02A1;
                Label_014C:
                    this.<unit_iname>__7 = this.<unit_param>__6.iname;
                    if (this.<>f__this.DownloadUnits.IndexOf(this.<unit_iname>__7) != -1)
                    {
                        goto Label_02A1;
                    }
                    this.<>f__this.DownloadUnits.Add(this.<unit_iname>__7);
                    goto Label_02A1;
                Label_0194:
                    if (this.<drop>__2.type != 3)
                    {
                        goto Label_01F2;
                    }
                    this.<iname>__8 = this.<drop>__2.artifact.iname;
                    if (this.<>f__this.DownloadArtifacts.IndexOf(this.<iname>__8) != -1)
                    {
                        goto Label_02A1;
                    }
                    this.<>f__this.DownloadArtifacts.Add(this.<iname>__8);
                    goto Label_02A1;
                Label_01F2:
                    if (this.<drop>__2.type != 4)
                    {
                        goto Label_02A1;
                    }
                    this.<iname>__9 = this.<drop>__2.conceptcard.iname;
                    if (this.<>f__this.DownloadConceptCards.IndexOf(this.<iname>__9) != -1)
                    {
                        goto Label_024B;
                    }
                    this.<>f__this.DownloadConceptCards.Add(this.<iname>__9);
                Label_024B:
                    if (this.<drop>__2.cardunit == null)
                    {
                        goto Label_02A1;
                    }
                    if (this.<>f__this.DownloadUnits.IndexOf(this.<drop>__2.cardunit.iname) != -1)
                    {
                        goto Label_02A1;
                    }
                    this.<>f__this.DownloadUnits.Add(this.<drop>__2.cardunit.iname);
                Label_02A1:
                    if (&this.<$s_531>__1.MoveNext() != null)
                    {
                        goto Label_0067;
                    }
                    goto Label_02C7;
                }
                finally
                {
                Label_02B6:
                    ((List<GachaDropData>.Enumerator) this.<$s_531>__1).Dispose();
                }
            Label_02C7:
                goto Label_02DF;
            Label_02CC:
                this.$current = null;
                this.$PC = 1;
                goto Label_0512;
            Label_02DF:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_02CC;
                }
                if ((this.<gm>__0 != null) == null)
                {
                    goto Label_04BB;
                }
                this.<i>__10 = 0;
                goto Label_0372;
            Label_0306:
                if (string.IsNullOrEmpty(this.<>f__this.DownloadUnits[this.<i>__10]) != null)
                {
                    goto Label_0364;
                }
                this.<unit>__11 = this.<gm>__0.GetUnitParam(this.<>f__this.DownloadUnits[this.<i>__10]);
                if (this.<unit>__11 == null)
                {
                    goto Label_0364;
                }
                DownloadUtility.DownloadUnit(this.<unit>__11, null);
            Label_0364:
                this.<i>__10 += 1;
            Label_0372:
                if (this.<i>__10 < this.<>f__this.DownloadUnits.Count)
                {
                    goto Label_0306;
                }
                this.<i>__12 = 0;
                goto Label_0409;
            Label_0399:
                if (string.IsNullOrEmpty(this.<>f__this.DownloadArtifacts[this.<i>__12]) != null)
                {
                    goto Label_03FB;
                }
                this.<artifact>__13 = this.<gm>__0.MasterParam.GetArtifactParam(this.<>f__this.DownloadArtifacts[this.<i>__12]);
                if (this.<artifact>__13 == null)
                {
                    goto Label_03FB;
                }
                DownloadUtility.DownloadArtifact(this.<artifact>__13);
            Label_03FB:
                this.<i>__12 += 1;
            Label_0409:
                if (this.<i>__12 < this.<>f__this.DownloadArtifacts.Count)
                {
                    goto Label_0399;
                }
                this.<i>__14 = 0;
                goto Label_04A0;
            Label_0430:
                if (string.IsNullOrEmpty(this.<>f__this.DownloadConceptCards[this.<i>__14]) != null)
                {
                    goto Label_0492;
                }
                this.<conceptcard>__15 = this.<gm>__0.MasterParam.GetConceptCardParam(this.<>f__this.DownloadConceptCards[this.<i>__14]);
                if (this.<conceptcard>__15 == null)
                {
                    goto Label_0492;
                }
                DownloadUtility.DownloadConceptCard(this.<conceptcard>__15);
            Label_0492:
                this.<i>__14 += 1;
            Label_04A0:
                if (this.<i>__14 < this.<>f__this.DownloadConceptCards.Count)
                {
                    goto Label_0430;
                }
            Label_04BB:
                if (AssetDownloader.isDone != null)
                {
                    goto Label_04CA;
                }
                ProgressWindow.OpenGenericDownloadWindow();
            Label_04CA:
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_04EF;
            Label_04D8:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_0512;
            Label_04EF:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_04D8;
                }
                ProgressWindow.Close();
                this.<>f__this.Success();
                this.$PC = -1;
            Label_0510:
                return 0;
            Label_0512:
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

        private enum ExecType : byte
        {
            NONE = 0,
            DEFAULT = 1,
            DECISION = 2
        }
    }
}

