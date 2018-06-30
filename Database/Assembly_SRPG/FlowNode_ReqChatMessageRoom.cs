namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/ReqChatLogRoom", 0x7fe5)]
    public class FlowNode_ReqChatMessageRoom : FlowNode_ReqChatMessage
    {
        private string mRoomToken;
        private long mStartID;
        private int mLimit;
        private long mExcludeID;
        private bool mIsSystemMessageMerge;

        public FlowNode_ReqChatMessageRoom()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            bool flag;
            base.set_enabled(1);
            if (base.mSetup != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (pinID != null)
            {
                goto Label_0080;
            }
            Network.IsIndicator = 0;
            flag = 0;
            if ((MonoSingleton<GameManager>.Instance != null) == null)
            {
                goto Label_0050;
            }
            if (MonoSingleton<GameManager>.Instance.Player == null)
            {
                goto Label_0050;
            }
            flag = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
        Label_0050:
            base.ExecRequest(new ReqChatMessageRoom(this.mStartID, this.mRoomToken, this.mLimit, this.mExcludeID, flag, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0080:
            return;
        }

        private void ResetParam()
        {
            this.mRoomToken = null;
            this.mStartID = 0L;
            this.mLimit = 0;
            this.mExcludeID = 0L;
            this.mIsSystemMessageMerge = 0;
            base.mSetup = 0;
            return;
        }

        public override void SetChatMessageInfo(string room_token, long start_id, int limit, long exclude_id, bool is_sys_msg_merge)
        {
            this.ResetParam();
            this.mRoomToken = room_token;
            this.mStartID = start_id;
            this.mLimit = limit;
            this.mExcludeID = exclude_id;
            this.mIsSystemMessageMerge = is_sys_msg_merge;
            base.mSetup = 1;
            return;
        }

        protected override void Success(ChatLog log)
        {
            ChatWindow window;
            window = base.get_gameObject().GetComponent<ChatWindow>();
            if ((window != null) == null)
            {
                goto Label_003D;
            }
            if (this.mIsSystemMessageMerge == null)
            {
                goto Label_0035;
            }
            window.SetChatLogAndSystemMessageMerge(log, this.mExcludeID);
            goto Label_003D;
        Label_0035:
            window.SetChatLog(log, 2);
        Label_003D:
            base.Success(log);
            return;
        }
    }
}

