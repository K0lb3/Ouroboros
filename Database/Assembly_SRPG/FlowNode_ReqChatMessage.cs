namespace SRPG
{
    using GR;
    using System;

    [Pin(10, "Success", 1, 10), NodeType("System/ReqChatLog", 0x7fe5), Pin(0, "Request", 0, 0), Pin(100, "ChatFailure", 1, 100)]
    public class FlowNode_ReqChatMessage : FlowNode_Network
    {
        protected bool mSetup;

        public FlowNode_ReqChatMessage()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ChatLog> response;
            ChatLog log;
            Network.EErrCode code;
            if ((this == null) == null)
            {
                goto Label_0018;
            }
            Network.RemoveAPI();
            Network.IsIndicator = 1;
            return;
        Label_0018:
            if (Network.IsError == null)
            {
                goto Label_004B;
            }
            code = Network.ErrCode;
            Network.RemoveAPI();
            Network.IsIndicator = 1;
            base.set_enabled(0);
            this.mSetup = 0;
            base.ActivateOutputLinks(100);
            return;
        Label_004B:
            DebugMenu.Log("API", "chat:message:{" + &www.text + "}");
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChatLog>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            Network.IsIndicator = 1;
            log = new ChatLog();
            if (response.body == null)
            {
                goto Label_00E4;
            }
            log.Deserialize(response.body);
            MultiInvitationReceiveWindow.SetBadge((response.body.player == null) ? 0 : ((response.body.player.multi_inv == 0) == 0));
            goto Label_00EA;
        Label_00E4:
            MultiInvitationReceiveWindow.SetBadge(0);
        Label_00EA:
            this.Success(log);
            return;
        }

        public virtual void SetChatMessageInfo(int channel, long start_id, int limit, long exclude_id)
        {
        }

        public virtual void SetChatMessageInfo(string room_token, long start_id, int limit, long exclude_id, bool is_sys_msg_merge)
        {
        }

        protected virtual void Success(ChatLog log)
        {
            base.set_enabled(0);
            this.mSetup = 0;
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

