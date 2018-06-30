namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;

    [NodeType("AVProVideo/MediaPlayerDispatchFinishEvent"), Pin(0, "Input", 0, 0), Pin(10, "Output", 1, 10)]
    public class FlowNode_MediaPlayerDispatchFinishEvent : FlowNode
    {
        public OnEnd onEnd;

        public FlowNode_MediaPlayerDispatchFinishEvent()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0025;
            }
            if (this.onEnd == null)
            {
                goto Label_001C;
            }
            this.onEnd();
        Label_001C:
            base.ActivateOutputLinks(10);
        Label_0025:
            return;
        }

        public delegate void OnEnd();
    }
}

