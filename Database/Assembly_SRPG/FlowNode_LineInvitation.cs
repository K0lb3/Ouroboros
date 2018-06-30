namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;

    [Pin(200, "Read Done", 1, 0), Pin(200, "Send Done", 1, 0), Pin(110, "Read", 0, 0), Pin(100, "Send", 0, 0), NodeType("LINE/Invitation", 0x7fe5)]
    public class FlowNode_LineInvitation : FlowNode
    {
        public FlowNode_LineInvitation()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            if (pinID != 100)
            {
                goto Label_0053;
            }
            str = LocalizedText.Get("sys.MP_LINE_INVITATION");
            DebugUtility.Log("LINE招待:" + str);
            Application.OpenURL(LocalizedText.Get("sys.MP_LINE_HTTP") + WWW.EscapeURL(str, Encoding.UTF8));
            base.ActivateOutputLinks(200);
            goto Label_005B;
        Label_0053:
            if (pinID != 110)
            {
                goto Label_005B;
            }
        Label_005B:
            return;
        }
    }
}

