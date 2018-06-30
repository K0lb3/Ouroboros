namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(10, "Open", 0, 0), Pin(1, "Yes", 1, 1), Pin(11, "ForceClose", 0, 11), Pin(2, "No", 1, 2), NodeType("UI/ContinueWindow", 0x7fe5)]
    public class FlowNode_ContinueWindow : FlowNode
    {
        public FlowNode_ContinueWindow()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_004E;
            }
            if (ContinueWindow.Create(SceneBattle.Instance.continueWindowRes, new ContinueWindow.ResultEvent(this.OnDecide), new ContinueWindow.ResultEvent(this.OnCancel)) != null)
            {
                goto Label_0040;
            }
            this.OnCancel(null);
            goto Label_0049;
        Label_0040:
            base.ActivateOutputLinks(100);
        Label_0049:
            goto Label_0064;
        Label_004E:
            if (pinID != 11)
            {
                goto Label_0064;
            }
            ContinueWindow.ForceClose();
            base.ActivateOutputLinks(0x65);
        Label_0064:
            return;
        }

        private void OnCancel(GameObject dialog)
        {
            base.ActivateOutputLinks(2);
            return;
        }

        private void OnDecide(GameObject dialog)
        {
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

