namespace SRPG
{
    using System;

    [Pin(2, "No", 1, 2), Pin(100, "Opened", 1, 100), NodeType("UI/EmbedWindowYesNo", 0x7fe5), Pin(10, "Open", 0, 0), Pin(1, "Yes", 1, 1)]
    public class FlowNode_EmbedWindowYesNo : FlowNode
    {
        public string m_Msg;

        public FlowNode_EmbedWindowYesNo()
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
            EmbedWindowYesNo.Create(str, new EmbedWindowYesNo.YesNoWindowEvent(this.OnYesNoWindowEvent));
            goto Label_004E;
        Label_0036:
            EmbedWindowYesNo.Create(this.m_Msg, new EmbedWindowYesNo.YesNoWindowEvent(this.OnYesNoWindowEvent));
        Label_004E:
            base.ActivateOutputLinks(100);
        Label_0057:
            return;
        }

        private void OnYesNoWindowEvent(bool yes)
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

