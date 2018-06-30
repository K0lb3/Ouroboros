namespace SRPG
{
    using GR;
    using System;
    using UnityEngine.UI;

    [NodeType("Network/gauth_passcode", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqHikkoshiCode : FlowNode_Network
    {
        public Text HikkoshiCodeText;
        public Text ExpireTimeText;

        public FlowNode_ReqHikkoshiCode()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_004A;
            }
            if (Network.Mode != 1)
            {
                goto Label_0018;
            }
            this.Success();
            return;
        Label_0018:
            base.ExecRequest(new ReqGAuthPasscode(MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.DeviceId, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_004A:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            object[] objArray1;
            WebAPI.JSON_BodyResponse<JSON_PassCode> response;
            DateTime time;
            string str;
            Network.EErrCode code;
            DateTime time2;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_PassCode>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0047;
            }
            this.OnRetry();
            return;
        Label_0047:
            Network.RemoveAPI();
            if ((this.HikkoshiCodeText != null) == null)
            {
                goto Label_0073;
            }
            this.HikkoshiCodeText.set_text(response.body.passcode);
        Label_0073:
            if ((this.ExpireTimeText != null) == null)
            {
                goto Label_011B;
            }
            time = &DateTime.Now.AddSeconds((double) response.body.expires_in);
            str = LocalizedText.Get("sys.HIKKOSHICODE_EXPIRETIME");
            objArray1 = new object[] { (int) &time.Year, (int) &time.Month, (int) &time.Day, (int) &time.Hour, (int) &time.Minute, (int) &time.Second };
            this.ExpireTimeText.set_text(string.Format(str, objArray1));
        Label_011B:
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

