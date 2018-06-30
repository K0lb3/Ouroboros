namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [Pin(100, "一覧取得した", 1, 100), NodeType("System/ReqConceptCard", 0x7fe5), Pin(0, "一覧取得", 0, 0)]
    public class FlowNode_ReqConceptCard : FlowNode_Network
    {
        private const int INPUT_CONCEPT_CARD_GET = 0;
        private const int OUTPUT_CONCEPT_CARD_GOT = 100;
        private bool mIsDataOverride;

        public FlowNode_ReqConceptCard()
        {
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator DownloadAssetsAndOutputPin()
        {
            <DownloadAssetsAndOutputPin>c__IteratorC3 rc;
            rc = new <DownloadAssetsAndOutputPin>c__IteratorC3();
            rc.<>f__this = this;
            return rc;
        }

        public override void OnActivate(int pinID)
        {
            long num;
            List<ConceptCardData> list;
            int num2;
            if (pinID != null)
            {
                goto Label_00AE;
            }
            if (GlobalVars.IsDirtyConceptCardData.Get() == null)
            {
                goto Label_00A5;
            }
            base.set_enabled(1);
            num = 0L;
            this.mIsDataOverride = 1;
            list = MonoSingleton<GameManager>.Instance.Player.ConceptCards;
            if (list == null)
            {
                goto Label_0088;
            }
            num2 = 0;
            goto Label_007C;
        Label_0043:
            if (num < list[num2].UniqueID)
            {
                goto Label_005F;
            }
            goto Label_0078;
        Label_005F:
            num = list[num2].UniqueID;
            this.mIsDataOverride = 0;
        Label_0078:
            num2 += 1;
        Label_007C:
            if (num2 < list.Count)
            {
                goto Label_0043;
            }
        Label_0088:
            base.ExecRequest(new ReqGetConceptCard(num, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_00AE;
        Label_00A5:
            base.ActivateOutputLinks(100);
        Label_00AE:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ConceptCardList> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ConceptCardList>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_003A:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.cards, this.mIsDataOverride);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.materials, this.mIsDataOverride);
                GlobalVars.IsDirtyConceptCardData.Set(0);
                goto Label_0091;
            }
            catch (Exception exception1)
            {
            Label_0080:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_009E;
            }
        Label_0091:
            base.StartCoroutine(this.DownloadAssetsAndOutputPin());
        Label_009E:
            return;
        }

        [CompilerGenerated]
        private sealed class <DownloadAssetsAndOutputPin>c__IteratorC3 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal FlowNode_DownloadAssets <FlowNode_DownLoadAssets>__0;
            internal int $PC;
            internal object $current;
            internal FlowNode_ReqConceptCard <>f__this;

            public <DownloadAssetsAndOutputPin>c__IteratorC3()
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
                        goto Label_0093;

                    case 2:
                        goto Label_00AB;
                }
                goto Label_00DC;
            Label_0025:
                this.<FlowNode_DownLoadAssets>__0 = this.<>f__this.GetComponent<FlowNode_DownloadAssets>();
                if ((this.<FlowNode_DownLoadAssets>__0 == null) == null)
                {
                    goto Label_00BB;
                }
                this.<FlowNode_DownLoadAssets>__0 = SRPG_Extensions.RequireComponent<FlowNode_DownloadAssets>(this.<>f__this);
                this.<FlowNode_DownLoadAssets>__0.ProgressBar = 1;
                this.<FlowNode_DownLoadAssets>__0.Download = 0x80;
                this.<FlowNode_DownLoadAssets>__0.OnActivate(0);
                this.$current = null;
                this.$PC = 1;
                goto Label_00DE;
            Label_0093:
                goto Label_00AB;
            Label_0098:
                this.$current = null;
                this.$PC = 2;
                goto Label_00DE;
            Label_00AB:
                if (this.<FlowNode_DownLoadAssets>__0.get_enabled() != null)
                {
                    goto Label_0098;
                }
            Label_00BB:
                this.<>f__this.ActivateOutputLinks(100);
                this.<>f__this.set_enabled(0);
                this.$PC = -1;
            Label_00DC:
                return 0;
            Label_00DE:
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

        public class Json_ConceptCardList
        {
            public JSON_ConceptCard[] cards;
            public JSON_ConceptCardMaterial[] materials;

            public Json_ConceptCardList()
            {
                base..ctor();
                return;
            }
        }
    }
}

