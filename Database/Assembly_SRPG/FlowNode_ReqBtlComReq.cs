namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;

    public class FlowNode_ReqBtlComReq : FlowNode_Network
    {
        private OnSuccesDelegate mOnSuccessDelegate;

        public FlowNode_ReqBtlComReq()
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
                goto Label_0071;
            }
            code = Network.ErrCode;
            switch ((code - 0xce4))
            {
                case 0:
                    goto Label_0047;

                case 1:
                    goto Label_004E;

                case 2:
                    goto Label_002C;

                case 3:
                    goto Label_0055;
            }
        Label_002C:
            if (code == 0xdac)
            {
                goto Label_005C;
            }
            if (code == 0xe74)
            {
                goto Label_0063;
            }
            goto Label_006A;
        Label_0047:
            this.OnBack();
            return;
        Label_004E:
            this.OnBack();
            return;
        Label_0055:
            this.OnBack();
            return;
        Label_005C:
            this.OnFailed();
            return;
        Label_0063:
            this.OnFailed();
            return;
        Label_006A:
            this.OnRetry();
            return;
        Label_0071:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_00A1;
            }
            this.OnRetry();
            return;
        Label_00A1:
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

