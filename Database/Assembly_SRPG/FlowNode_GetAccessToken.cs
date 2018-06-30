namespace SRPG
{
    using GR;
    using Gsc.Auth;
    using System;

    [Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), NodeType("System/GetAccessToken", 0x7fe5)]
    public class FlowNode_GetAccessToken : FlowNode_Network
    {
        public FlowNode_GetAccessToken()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0039;
            }
            MyMetaps.TrackEvent("device_id", MonoSingleton<GameManager>.Instance.DeviceId);
            Network.SessionID = Session.DefaultSession.AccessToken;
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
        Label_0039:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_AccessToken> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0032;
            }
            code = Network.ErrCode;
            if (code == 0x1388)
            {
                goto Label_002B;
            }
            if (code == 0x1389)
            {
                goto Label_002B;
            }
            goto Label_0032;
        Label_002B:
            this.OnFailed();
            return;
        Label_0032:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_AccessToken>>(&www.text);
            if (response.body != null)
            {
                goto Label_0051;
            }
            this.OnFailed();
            return;
        Label_0051:
            Network.SessionID = response.body.access_token;
            MyMetaps.TrackEvent("device_id", MonoSingleton<GameManager>.Instance.DeviceId);
            Network.RemoveAPI();
            base.ActivateOutputLinks(1);
            base.set_enabled(0);
            return;
        }

        private class JSON_AccessToken
        {
            public string access_token;
            public int expires_in;

            public JSON_AccessToken()
            {
                base..ctor();
                return;
            }
        }
    }
}

