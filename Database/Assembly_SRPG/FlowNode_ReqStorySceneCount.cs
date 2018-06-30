namespace SRPG
{
    using System;

    [Pin(0, "Requesst", 0, 0), Pin(10, "Success", 1, 10), NodeType("System/ReqStorySceneCount", 0x7fe5)]
    public class FlowNode_ReqStorySceneCount : FlowNode_Network
    {
        public FlowNode_ReqStorySceneCount()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_002E;
            }
            base.set_enabled(1);
            base.ExecRequest(new ReqStorySceneCount(GlobalVars.ReplaySelectedQuestID, new Network.ResponseCallback(this.ResponseCallback)));
        Label_002E:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            this.OnRetry();
            return;
        Label_0011:
            Network.RemoveAPI();
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

