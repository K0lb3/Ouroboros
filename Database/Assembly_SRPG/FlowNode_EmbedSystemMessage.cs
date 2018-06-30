namespace SRPG
{
    using System;

    [NodeType("UI/EmbedSystemMessage", 0x7fe5), Pin(10, "Open", 0, 0), Pin(100, "Opened", 1, 100), Pin(1, "Done", 1, 1)]
    public class FlowNode_EmbedSystemMessage : FlowNode
    {
        public string m_Msg;

        public FlowNode_EmbedSystemMessage()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            bool flag;
            string str;
            if (pinID != 10)
            {
                goto Label_0057;
            }
            flag = 0;
            str = LocalizedText.Get(this.m_Msg, &flag);
            if (flag == null)
            {
                goto Label_0036;
            }
            EmbedSystemMessage.Create(str, new EmbedSystemMessage.SystemMessageEvent(this.OnSystemMessageEvent));
            goto Label_004E;
        Label_0036:
            EmbedSystemMessage.Create(this.m_Msg, new EmbedSystemMessage.SystemMessageEvent(this.OnSystemMessageEvent));
        Label_004E:
            base.ActivateOutputLinks(100);
        Label_0057:
            return;
        }

        private void OnSystemMessageEvent(bool yes)
        {
            if (yes == null)
            {
                goto Label_0013;
            }
            base.ActivateOutputLinks(1);
            goto Label_001B;
        Label_0013:
            base.ActivateOutputLinks(2);
        Label_001B:
            return;
        }
    }
}

