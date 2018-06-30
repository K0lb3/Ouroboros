namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;

    public class FlowNode_ReqBtlComResume : FlowNode_Network
    {
        private OnSuccesDelegate mOnSuccessDelegate;

        public FlowNode_ReqBtlComResume()
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
                goto Label_002E;
            }
            if (Network.ErrCode == 0xd48)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnFailed();
            return;
        Label_0027:
            this.OnRetry();
            return;
        Label_002E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_005E;
            }
            this.OnRetry();
            return;
        Label_005E:
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

