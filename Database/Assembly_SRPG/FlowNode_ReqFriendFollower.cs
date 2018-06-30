namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), NodeType("Network/ReqFriendFollower", 0x7fe5)]
    public class FlowNode_ReqFriendFollower : FlowNode_Network
    {
        public FlowNode_ReqFriendFollower()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0042;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (Network.Mode != 1)
            {
                goto Label_0024;
            }
            this.Success();
            return;
        Label_0024:
            base.ExecRequest(new ReqFriendFollower(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0042:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            GameManager manager;
            Exception exception;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            this.OnFailed();
            return;
        Label_0011:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            manager = MonoSingleton<GameManager>.Instance;
        Label_003A:
            try
            {
                manager.Deserialize(response.body.friends, 3);
                this.Success();
                goto Label_0068;
            }
            catch (Exception exception1)
            {
            Label_0057:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_006F;
            }
        Label_0068:
            base.set_enabled(0);
        Label_006F:
            return;
        }

        private void Success()
        {
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

