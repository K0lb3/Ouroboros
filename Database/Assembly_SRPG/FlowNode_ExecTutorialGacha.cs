namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [NodeType("Gacha/ExecTutorialGacha", 0x7fe5), Pin(1, "Request(チュートリアル引き直し確定)", 0, 1), Pin(0, "Request", 0, 0), Pin(13, "Pending(保留中の召喚がある)", 1, 13), Pin(12, "Success(引き直し確定)", 1, 12), Pin(11, "Failed", 1, 11), Pin(10, "Success", 1, 10)]
    public class FlowNode_ExecTutorialGacha : FlowNode_Network
    {
        private const int PIN_IN_REQUEST = 0;
        private const int PIN_IN_DECISION = 1;
        private const int PIN_OT_SUCCESS = 10;
        private const int PIN_OT_FAILED = 11;
        private const int PIN_OT_SUCCESS_DECISION_REDRAW = 12;
        private const int PIN_OT_PENDING = 13;
        [SerializeField]
        private string TutorialGachaID;
        [CompilerGenerated]
        private ExecType <API>k__BackingField;

        public FlowNode_ExecTutorialGacha()
        {
            this.TutorialGachaID = string.Empty;
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncDownload(UnitParam _uparam)
        {
            <AsyncDownload>c__IteratorB8 rb;
            rb = new <AsyncDownload>c__IteratorB8();
            rb._uparam = _uparam;
            rb.<$>_uparam = _uparam;
            rb.<>f__this = this;
            return rb;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(11);
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0092;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((FlowNode_Variable.Get("REDRAW_GACHA_PENDING") == "1") == null)
            {
                goto Label_0044;
            }
            FlowNode_Variable.Set("REDRAW_GACHA_PENDING", string.Empty);
            base.ActivateOutputLinks(13);
            return;
        Label_0044:
            if (string.IsNullOrEmpty(this.TutorialGachaID) == null)
            {
                goto Label_005F;
            }
            DebugUtility.LogError("チュートリアル召喚で使用したいIDが指定されていません");
            return;
        Label_005F:
            this.API = 1;
            base.ExecRequest(new ReqGachaExec(this.TutorialGachaID, new Network.ResponseCallback(this.ResponseCallback), 0, 0, 0));
            base.set_enabled(1);
            goto Label_00E4;
        Label_0092:
            if (pinID != 1)
            {
                goto Label_00E4;
            }
            if (base.get_enabled() == null)
            {
                goto Label_00A5;
            }
            return;
        Label_00A5:
            if (string.IsNullOrEmpty(this.TutorialGachaID) == null)
            {
                goto Label_00B6;
            }
            return;
        Label_00B6:
            this.API = 2;
            base.ExecRequest(new ReqGachaExec(this.TutorialGachaID, new Network.ResponseCallback(this.ResponseCallback), 0, 0, 1));
            base.set_enabled(1);
        Label_00E4:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_GachaResult> response;
            Exception exception;
            List<GachaDropData> list;
            Json_DropInfo info;
            Json_DropInfo[] infoArray;
            int num;
            GachaDropData data;
            GachaReceiptData data2;
            string str;
            GachaDropData data3;
            UnitParam param;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0030;
            }
            if (Network.ErrCode == 0xfa0)
            {
                goto Label_0022;
            }
            goto Label_0029;
        Label_0022:
            this.OnFailed();
            return;
        Label_0029:
            this.OnRetry();
            return;
        Label_0030:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaResult>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_0053:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body);
                goto Label_007F;
            }
            catch (Exception exception1)
            {
            Label_0068:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.Failure();
                goto Label_01A5;
            }
        Label_007F:
            list = new List<GachaDropData>();
            if (response.body.add == null)
            {
                goto Label_00F0;
            }
            if (((int) response.body.add.Length) <= 0)
            {
                goto Label_00F0;
            }
            infoArray = response.body.add;
            num = 0;
            goto Label_00E5;
        Label_00BD:
            info = infoArray[num];
            data = new GachaDropData();
            if (data.Deserialize(info) == null)
            {
                goto Label_00DF;
            }
            list.Add(data);
        Label_00DF:
            num += 1;
        Label_00E5:
            if (num < ((int) infoArray.Length))
            {
                goto Label_00BD;
            }
        Label_00F0:
            data2 = new GachaReceiptData();
            data2.Deserialize(response.body.receipt);
            GachaResultData.Init(list, null, null, data2, 0, response.body.is_pending, response.body.rest);
            MyMetaps.TrackSpend(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(data2.type), data2.iname, data2.val);
            if (this.API != 2)
            {
                goto Label_016C;
            }
            this.Success();
            return;
        Label_016C:
            data3 = GachaResultData.drops[0];
            if (data3 != null)
            {
                goto Label_018D;
            }
            DebugUtility.LogError("召喚結果が存在しません");
            this.Failure();
            return;
        Label_018D:
            param = data3.unit;
            base.StartCoroutine(this.AsyncDownload(param));
        Label_01A5:
            return;
        }

        private void Success()
        {
            int num;
            base.set_enabled(0);
            num = 10;
            if (this.API != 2)
            {
                goto Label_0019;
            }
            num = 12;
        Label_0019:
            base.ActivateOutputLinks(num);
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
        private sealed class <AsyncDownload>c__IteratorB8 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal UnitParam _uparam;
            internal int $PC;
            internal object $current;
            internal UnitParam <$>_uparam;
            internal FlowNode_ExecTutorialGacha <>f__this;

            public <AsyncDownload>c__IteratorB8()
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
                        goto Label_0074;

                    case 2:
                        goto Label_00AF;

                    case 3:
                        goto Label_0105;
                }
                goto Label_010C;
            Label_0029:
                if (this._uparam != null)
                {
                    goto Label_004E;
                }
                DebugUtility.LogError("ダウンロードしたいユニットの情報がありません");
                this.<>f__this.Failure();
                goto Label_010C;
            Label_004E:
                if (AssetDownloader.isDone != null)
                {
                    goto Label_007E;
                }
                goto Label_0074;
            Label_005D:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_010E;
            Label_0074:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_005D;
                }
            Label_007E:
                DownloadUtility.DownloadUnit(this._uparam, null);
                AssetDownloader.StartDownload(0, 1, 2);
                goto Label_00BE;
            Label_0098:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_010E;
            Label_00AF:
                if (AssetDownloader.HasError == null)
                {
                    goto Label_00BE;
                }
                goto Label_00C8;
            Label_00BE:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0098;
                }
            Label_00C8:
                if (AssetDownloader.HasError == null)
                {
                    goto Label_00E7;
                }
                AssetDownloader.Reset();
                this.<>f__this.Failure();
                goto Label_00F2;
            Label_00E7:
                this.<>f__this.Success();
            Label_00F2:
                this.$current = null;
                this.$PC = 3;
                goto Label_010E;
            Label_0105:
                this.$PC = -1;
            Label_010C:
                return 0;
            Label_010E:
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

