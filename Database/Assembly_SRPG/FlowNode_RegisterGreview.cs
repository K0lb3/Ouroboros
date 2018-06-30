namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [NodeType("System/RegisterGreview", 0x7fe5), Pin(1, "Register Greview", 0, 1), Pin(10, "Success", 1, 10), Pin(11, "Failed", 1, 11)]
    public class FlowNode_RegisterGreview : FlowNode_Network
    {
        public FlowNode_RegisterGreview()
        {
            base..ctor();
            return;
        }

        private void Failure()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(11);
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != 1)
            {
                goto Label_0040;
            }
            if (Network.Mode != null)
            {
                goto Label_003A;
            }
            UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_GOOGLE_REVIEW"), new UIUtility.DialogResultEvent(this.OnSerialRegister), null, null, 0, -1, null, null);
            goto Label_0040;
        Label_003A:
            this.Success();
        Label_0040:
            return;
        }

        private void OnSerialRegister(GameObject go)
        {
            base.ExecRequest(new ReqGoogleReview(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_GoogleReview> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0038;
            }
            if (Network.ErrCode == 0x1f45)
            {
                goto Label_0020;
            }
            goto Label_0031;
        Label_0020:
            Network.RemoveAPI();
            Network.ResetError();
            this.Success();
            return;
        Label_0031:
            this.OnBack();
            return;
        Label_0038:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GoogleReview>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_005B:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body);
                goto Label_0087;
            }
            catch (Exception exception1)
            {
            Label_0070:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.Failure();
                goto Label_008D;
            }
        Label_0087:
            this.Success();
        Label_008D:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

