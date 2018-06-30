namespace SRPG
{
    using GR;
    using System;

    [Pin(1, "Success", 1, 1), NodeType("Network/FriendList", 0x7fe5), Pin(2, "Request(フレンド申請も含む)", 0, 2), Pin(0, "Request", 0, 0)]
    public class FlowNode_FriendList : FlowNode_Network
    {
        private bool IsFollower;

        public FlowNode_FriendList()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID == null)
            {
                goto Label_000D;
            }
            if (pinID != 2)
            {
                goto Label_0059;
            }
        Label_000D:
            this.IsFollower = pinID == 2;
            if (base.get_enabled() == null)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            if (Network.Mode != 1)
            {
                goto Label_0035;
            }
            this.Success();
            return;
        Label_0035:
            base.ExecRequest(new ReqFriendList(this.IsFollower, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0059:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_FriendList> response;
            GameManager manager;
            InnWindow window;
            Exception exception;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            this.OnFailed();
            return;
        Label_0011:
            DebugMenu.Log("API", "friend:" + &www.text);
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_FriendList>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            manager = MonoSingleton<GameManager>.Instance;
        Label_0055:
            try
            {
                manager.Deserialize(response.body.friends, 1);
                if (this.IsFollower == null)
                {
                    goto Label_00A6;
                }
                manager.Player.FollowerNum = response.body.follower_count;
                window = base.get_gameObject().GetComponentInChildren<InnWindow>();
                if ((window != null) == null)
                {
                    goto Label_00A6;
                }
                window.Refresh();
            Label_00A6:
                manager.Player.FirstFriendCount = response.body.first_count;
                this.Success();
                goto Label_00D8;
            }
            catch (Exception exception1)
            {
            Label_00C7:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_00DF;
            }
        Label_00D8:
            base.set_enabled(0);
        Label_00DF:
            return;
        }

        private void Success()
        {
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

