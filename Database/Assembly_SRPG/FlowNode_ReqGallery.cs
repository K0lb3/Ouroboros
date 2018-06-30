namespace SRPG
{
    using System;

    [Pin(0, "Request", 0, 0), NodeType("System/ReqGallery", 0x7fe5), Pin(100, "Success", 1, 100), Pin(0x65, "InProgress", 1, 0x65)]
    public class FlowNode_ReqGallery : FlowNode_Network
    {
        public FlowNode_ReqGallery()
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
            if (Network.Mode != null)
            {
                goto Label_0033;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqGallery(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0039;
        Label_0033:
            this.Success();
        Label_0039:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_003B;
            }
            if (Network.ErrCode == 0x36b1)
            {
                goto Label_0020;
            }
            goto Label_0034;
        Label_0020:
            Network.ResetError();
            Network.RemoveAPI();
            base.ActivateOutputLinks(0x65);
            return;
        Label_0034:
            this.OnRetry();
            return;
        Label_003B:
            Network.RemoveAPI();
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

