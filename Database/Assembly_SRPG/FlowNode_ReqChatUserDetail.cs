namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1), NodeType("System/ReqChatUserDetail", 0x7fe5)]
    public class FlowNode_ReqChatUserDetail : FlowNode_Network
    {
        [SerializeField]
        private ChatPlayerWindow window;
        [SerializeField]
        private FriendDetailWindow detail;

        public FlowNode_ReqChatUserDetail()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != null)
            {
                goto Label_0030;
            }
            str = FlowNode_Variable.Get("SelectUserID");
            base.ExecRequest(new ReqChatUserProfile(str, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(0);
        Label_0030:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ChatPlayerData> response;
            ChatPlayerData data;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            this.OnBack();
            return;
        Label_0011:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatPlayerData>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            data = new ChatPlayerData();
            data.Deserialize(response.body);
            if ((this.window != null) == null)
            {
                goto Label_0063;
            }
            this.window.Player = data;
        Label_0063:
            if ((this.detail != null) == null)
            {
                goto Label_0080;
            }
            this.detail.SetChatPlayerData(data);
        Label_0080:
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

