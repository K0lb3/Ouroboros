namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), NodeType("System/ApproveFriend", 0x7fe5)]
    public class FlowNode_ReqApproveFriend : FlowNode_Network
    {
        private string req_fuid;

        public FlowNode_ReqApproveFriend()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            this.req_fuid = null;
            if (pinID != null)
            {
                goto Label_0097;
            }
            if (Network.Mode != 1)
            {
                goto Label_001F;
            }
            this.Success();
            return;
        Label_001F:
            str = null;
            if (string.IsNullOrEmpty(GlobalVars.SelectedFriendID) != null)
            {
                goto Label_003B;
            }
            str = GlobalVars.SelectedFriendID;
            goto Label_0064;
        Label_003B:
            if (GlobalVars.FoundFriend == null)
            {
                goto Label_0064;
            }
            if (string.IsNullOrEmpty(GlobalVars.FoundFriend.FUID) != null)
            {
                goto Label_0064;
            }
            str = GlobalVars.FoundFriend.FUID;
        Label_0064:
            if (str != null)
            {
                goto Label_0071;
            }
            this.Success();
            return;
        Label_0071:
            base.ExecRequest(new ReqFriendApprove(str, new Network.ResponseCallback(this.ResponseCallback)));
            this.req_fuid = str;
            base.set_enabled(1);
        Label_0097:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ApproveFriend> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0039;
            }
            code = Network.ErrCode;
            if (code == 0x170e)
            {
                goto Label_002B;
            }
            if (code == 0x170f)
            {
                goto Label_002B;
            }
            goto Label_0032;
        Label_002B:
            this.OnBack();
            return;
        Label_0032:
            this.OnRetry();
            return;
        Label_0039:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ApproveFriend>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0069;
            }
            this.OnRetry();
            return;
        Label_0069:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.RemoveFriendFollower(this.req_fuid);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.friends, 1);
                MonoSingleton<GameManager>.Instance.Player.FirstFriendCount = response.body.first_count;
                goto Label_00DA;
            }
            catch (Exception exception1)
            {
            Label_00C8:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_00E5;
            }
        Label_00DA:
            Network.RemoveAPI();
            this.Success();
        Label_00E5:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            GameParameter.UpdateValuesOfType(0xcf);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

