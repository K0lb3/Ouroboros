namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [NodeType("UI/SetScrollPosition"), Pin(0, "In", 0, 0), Pin(1, "Out", 1, 1)]
    public class FlowNode_SetScrollPosition : FlowNode
    {
        public UnityEngine.UI.ScrollRect ScrollRect;
        public Vector2 NormalizedPosition;

        public FlowNode_SetScrollPosition()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0030;
            }
            if ((this.ScrollRect != null) == null)
            {
                goto Label_0028;
            }
            this.ScrollRect.set_normalizedPosition(this.NormalizedPosition);
        Label_0028:
            base.ActivateOutputLinks(1);
        Label_0030:
            return;
        }
    }
}

