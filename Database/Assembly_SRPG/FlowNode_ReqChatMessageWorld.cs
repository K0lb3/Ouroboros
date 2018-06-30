namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/ReqChatLogWorld", 0x7fe5)]
    public class FlowNode_ReqChatMessageWorld : FlowNode_ReqChatMessage
    {
        private int mChannel;
        private long mStartID;
        private int mLimit;
        private long mExcludeID;

        public FlowNode_ReqChatMessageWorld()
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
            base.ExecRequest(new ReqChatMessage(this.mStartID, this.mChannel, this.mLimit, this.mExcludeID, flag, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0080:
            return;
        }

        private void ResetParam()
        {
            this.mChannel = 0;
            this.mStartID = 0L;
            this.mLimit = 0;
            this.mExcludeID = 0L;
            base.mSetup = 0;
            return;
        }

        public override void SetChatMessageInfo(int channel, long start_id, int limit, long exclude_id)
        {
            this.ResetParam();
            this.mChannel = channel;
            this.mStartID = start_id;
            this.mLimit = limit;
            this.mExcludeID = exclude_id;
            base.mSetup = 1;
            return;
        }

        protected override void Success(ChatLog log)
        {
            ChatWindow window;
            window = base.get_gameObject().GetComponent<ChatWindow>();
            if ((window != null) == null)
            {
                goto Label_0020;
            }
            window.SetChatLog(log, 1);
        Label_0020:
            base.Success(log);
            return;
        }
    }
}

