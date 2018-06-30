namespace SRPG
{
    using System;

    [Pin(11, "Failed", 1, 11), NodeType("Gacha/ReqDecideTutorialGacha"), Pin(0, "Request", 0, 0), Pin(10, "Success", 1, 10)]
    public class FlowNode_ReqDecideTutorialGacha : FlowNode_Network
    {
        private const int PIN_IN_REQUEST = 0;
        private const int PIN_OU_SUCCESS = 10;
        private const int PIN_OU_FAILED = 11;

        public FlowNode_ReqDecideTutorialGacha()
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
            if (pinID != null)
            {
                goto Label_001F;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.set_enabled(1);
            this.Success();
        Label_001F:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            this.Success();
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

