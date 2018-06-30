namespace SRPG
{
    using GR;
    using System;

    [Pin(0x3e8, "報酬受取完了", 1, 0x3e8), NodeType("System/ReqConceptCardTrustMaster", 0x7fe5), Pin(100, "報酬未受取のトラストマスター達成", 0, 100)]
    public class FlowNode_ReqConceptCardTrustMaster : FlowNode_Network
    {
        private const int INPUT_TRUSTMASTER_ON = 100;
        private const int OUTPUT_TRUSTMASTER_ON = 0x3e8;
        private int mOutPutPinId;

        public FlowNode_ReqConceptCardTrustMaster()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            ConceptCardManager manager;
            int num;
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            num = pinID;
            if (num == 100)
            {
                goto Label_0022;
            }
            goto Label_005A;
        Label_0022:
            base.ExecRequest(new ReqTrustMasterConceptCard(manager.SelectedConceptCardData.UniqueID, 1, new Network.ResponseCallback(this.ResponseCallback)));
            this.mOutPutPinId = 0x3e8;
        Label_005A:
            base.set_enabled(1);
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ConceptCardTrustMaster> response;
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
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ConceptCardTrustMaster>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_003A:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.concept_card);
                goto Label_006A;
            }
            catch (Exception exception1)
            {
            Label_0059:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_007E;
            }
        Label_006A:
            base.ActivateOutputLinks(this.mOutPutPinId);
            base.set_enabled(0);
        Label_007E:
            return;
        }

        public class Json_ConceptCardTrustMaster
        {
            public JSON_ConceptCard concept_card;

            public Json_ConceptCardTrustMaster()
            {
                base..ctor();
                return;
            }
        }
    }
}

