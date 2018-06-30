namespace SRPG
{
    using GR;
    using System;

    [NodeType("UI/UpdateBadge", 0x7fe5), Pin(10, "Output", 1, 10), Pin(1, "Start", 0, 0)]
    public class FlowNode_UpdateBadge : FlowNode
    {
        public GameManager.BadgeTypes type;

        public FlowNode_UpdateBadge()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0017;
            }
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(this.type);
        Label_0017:
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

