namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;

    public class FlowNode_ReqBtlColoReq : FlowNode_Network
    {
        private OnSuccesDelegate mOnSuccessDelegate;

        public FlowNode_ReqBtlColoReq()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_006A;
            }
            switch ((Network.ErrCode - 0xed8))
            {
                case 0:
                    goto Label_0039;

                case 1:
                    goto Label_0040;

                case 2:
                    goto Label_0047;

                case 3:
                    goto Label_004E;

                case 4:
                    goto Label_0055;

                case 5:
                    goto Label_005C;
            }
            goto Label_0063;
        Label_0039:
            this.OnBack();
            return;
        Label_0040:
            this.OnBack();
            return;
        Label_0047:
            this.OnFailed();
            return;
        Label_004E:
            this.OnBack();
            return;
        Label_0055:
            this.OnFailed();
            return;
        Label_005C:
            this.OnBack();
            return;
        Label_0063:
            this.OnRetry();
            return;
        Label_006A:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_009A;
            }
            this.OnRetry();
            return;
        Label_009A:
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

