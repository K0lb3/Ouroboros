namespace SRPG
{
    using GR;
    using System;

    [Pin(2, "Success", 1, 1), NodeType("System/ReqFgGAuth", 0x7fe5), Pin(1, "Request", 0, 0)]
    public class FlowNode_ReqFgGAuth : FlowNode_Network
    {
        private const int PIN_ID_REQUEST = 1;
        private const int PIN_ID_SUCCESS = 2;
        private ReqFgGAuth.eAuthStatus mAuthStatusBefore;

        public FlowNode_ReqFgGAuth()
        {
            base..ctor();
            return;
        }

        private bool CheckStatusChanged(ReqFgGAuth.eAuthStatus status)
        {
            if (this.mAuthStatusBefore != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return ((this.mAuthStatusBefore == status) == 0);
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000E;
            }
            this.Success(1);
        Label_000E:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_FgGAuth> response;
            int num;
            Network.EErrCode code;
            int num2;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnFailed();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_FgGAuth>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            num = response.body.auth_status;
            MonoSingleton<GameManager>.Instance.FgGAuthHost = response.body.auth_url;
            Network.RemoveAPI();
            num2 = num;
            switch ((num2 - 1))
            {
                case 0:
                    goto Label_0076;

                case 1:
                    goto Label_0082;

                case 2:
                    goto Label_008E;
            }
            goto Label_00C0;
        Label_0076:
            this.Success(1);
            goto Label_00CB;
        Label_0082:
            this.Success(2);
            goto Label_00CB;
        Label_008E:
            this.Success(3);
            if (GameUtility.GetCurrentScene() != 3)
            {
                goto Label_00CB;
            }
            if (this.CheckStatusChanged(3) == null)
            {
                goto Label_00CB;
            }
            MonoSingleton<GameManager>.Instance.Player.OnFgGIDLogin();
            goto Label_00CB;
        Label_00C0:
            this.OnFailed();
        Label_00CB:
            return;
        }

        private void Success(ReqFgGAuth.eAuthStatus authStatus)
        {
            MonoSingleton<GameManager>.Instance.AuthStatus = authStatus;
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        }

        private class JSON_FgGAuth
        {
            public int auth_status;
            public string auth_url;

            public JSON_FgGAuth()
            {
                base..ctor();
                return;
            }
        }
    }
}

