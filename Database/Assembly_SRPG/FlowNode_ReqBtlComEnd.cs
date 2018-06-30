namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;

    public class FlowNode_ReqBtlComEnd : FlowNode_Network
    {
        private OnSuccesDelegate mOnSuccessDelegate;

        public FlowNode_ReqBtlComEnd()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002E;
            }
            if (Network.ErrCode == 0xdac)
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
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_005E;
            }
            this.OnRetry();
            return;
        Label_005E:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                goto Label_00B9;
            }
            catch (Exception exception1)
            {
            Label_00A2:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00C9;
            }
        Label_00B9:
            Network.RemoveAPI();
            this.mOnSuccessDelegate();
        Label_00C9:
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

        public delegate void OnSuccesDelegate();
    }
}

