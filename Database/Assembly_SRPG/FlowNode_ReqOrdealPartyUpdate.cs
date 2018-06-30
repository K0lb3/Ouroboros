namespace SRPG
{
    using System;

    [Pin(1, "Success", 1, 1), NodeType("System/ReqOrdealPartyUpdate", 0x7fe5), Pin(0, "Request", 0, 0), Pin(0x3e8, "Failed", 1, 0x3e8)]
    public class FlowNode_ReqOrdealPartyUpdate : FlowNode_Network
    {
        public FlowNode_ReqOrdealPartyUpdate()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_003B;
            }
            if (Network.Mode != 1)
            {
                goto Label_0018;
            }
            this.Success();
            return;
        Label_0018:
            base.set_enabled(1);
            base.ExecRequest(new ReqOrdealPartyUpdate(new Network.ResponseCallback(this.ResponseCallback), GlobalVars.OrdealParties));
        Label_003B:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            if (Network.IsError == null)
            {
                goto Label_0028;
            }
            Network.RemoveAPI();
            Network.ResetError();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x3e8);
            return;
        Label_0028:
            Network.RemoveAPI();
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

