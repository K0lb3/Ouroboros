namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;

    [Pin(14, "OverUse", 1, 14), Pin(12, "OutOfTerm", 1, 12), Pin(11, "NotFoundSerial", 1, 11), Pin(10, "NotFoundCampaign", 1, 10), Pin(2, "Failed", 1, 2), Pin(1, "Success", 1, 1), Pin(0, "CheckQRCodeAccess", 0, 0), NodeType("Request/QRCodeAccess", 0x7fe5), Pin(13, "AlreadyInputed", 1, 13)]
    public class FlowNode_ReqQRCodeAccess : FlowNode_Network
    {
        public FlowNode_ReqQRCodeAccess()
        {
            base..ctor();
            return;
        }

        private void Finished(string msg)
        {
            FlowNode_OnUrlSchemeLaunch.QRCampaignID = -1;
            FlowNode_OnUrlSchemeLaunch.QRSerialID = string.Empty;
            FlowNode_OnUrlSchemeLaunch.IsQRAccess = 0;
            base.set_enabled(0);
            if (string.IsNullOrEmpty(msg) != null)
            {
                goto Label_0034;
            }
            UIUtility.SystemMessage(null, msg, null, null, 0, -1);
        Label_0034:
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            string str;
            if (pinID != null)
            {
                goto Label_0050;
            }
            num = FlowNode_OnUrlSchemeLaunch.QRCampaignID;
            str = FlowNode_OnUrlSchemeLaunch.QRSerialID;
            if (num == -1)
            {
                goto Label_0049;
            }
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0049;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqQRCodeAccess(num, str, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0050;
        Label_0049:
            this.Finished(null);
        Label_0050:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_QRCodeAccess> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0052;
            }
            switch ((Network.ErrCode - 0x1f48))
            {
                case 0:
                    goto Label_0035;

                case 1:
                    goto Label_0035;

                case 2:
                    goto Label_0035;

                case 3:
                    goto Label_0035;

                case 4:
                    goto Label_0035;
            }
            goto Label_004B;
        Label_0035:
            Network.RemoveAPI();
            Network.ResetError();
            this.Finished(Network.ErrMsg);
            return;
        Label_004B:
            this.OnRetry();
            return;
        Label_0052:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_QRCodeAccess>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body.items == null)
            {
                goto Label_009A;
            }
            MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
        Label_009A:
            this.Finished(response.body.message);
            return;
        }

        private class JSON_QRCodeAccess
        {
            public Json_Item[] items;
            public string message;

            public JSON_QRCodeAccess()
            {
                this.message = string.Empty;
                base..ctor();
                return;
            }
        }
    }
}

