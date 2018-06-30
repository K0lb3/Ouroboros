namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;

    public class FlowNode_ReqBtlComCont : FlowNode_Network
    {
        private OnSuccesDelegate mOnSuccessDelegate;

        public FlowNode_ReqBtlComCont()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0040;
            }
            code = Network.ErrCode;
            if (code == 0xe10)
            {
                goto Label_002B;
            }
            if (code == 0xe11)
            {
                goto Label_0032;
            }
            goto Label_0039;
        Label_002B:
            this.OnBack();
            return;
        Label_0032:
            this.OnFailed();
            return;
        Label_0039:
            this.OnRetry();
            return;
        Label_0040:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0070;
            }
            this.OnRetry();
            return;
        Label_0070:
            Network.RemoveAPI();
            this.mOnSuccessDelegate(response.body);
            return;
        }

        public OnSuccesDelegate OnSuccessListeners
        {
            set
            {
                this.mOnSuccessDelegate = value;
                return;
            }
        }

        public delegate void OnSuccesDelegate(BattleCore.Json_Battle response);
    }
}

