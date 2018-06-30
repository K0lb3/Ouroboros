namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 1), NodeType("System/GetSessionID", 0x7fe5), Pin(0, "Request", 0, 0)]
    public class FlowNode_GetSessionID : FlowNode_Network
    {
        public FlowNode_GetSessionID()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0017;
            }
            MonoSingleton<GameManager>.Instance.InitAuth();
            this.Success();
        Label_0017:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_DeviceID> response;
            string str;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002E;
            }
            if (Network.ErrCode == 0x4b0)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnFailed();
            return;
        Label_0027:
            this.OnFailed();
            return;
        Label_002E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_DeviceID>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            str = response.body.device_id;
            MonoSingleton<GameManager>.Instance.SaveAuth(str);
            Network.RemoveAPI();
            base.ActivateOutputLinks(1);
            base.set_enabled(0);
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        private class JSON_DeviceID
        {
            public string device_id;

            public JSON_DeviceID()
            {
                base..ctor();
                return;
            }
        }

        private class JSON_SessionID
        {
            public string sid;

            public JSON_SessionID()
            {
                base..ctor();
                return;
            }
        }
    }
}

