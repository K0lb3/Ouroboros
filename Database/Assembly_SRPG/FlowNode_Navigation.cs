namespace SRPG
{
    using System;

    [Pin(1, "Show", 0, 0), Pin(11, "Destory", 1, 3), Pin(10, "Output", 1, 2), Pin(2, "Discard", 0, 1), NodeType("UI/Navigation", 0x7fe5)]
    public class FlowNode_Navigation : FlowNode
    {
        public NavigationWindow Template;
        [StringIsTextID(false)]
        public string TextID;
        public SRPG.NavigationWindow.Alignment Alignment;

        public FlowNode_Navigation()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_0015;
            }
            if (num == 2)
            {
                goto Label_003F;
            }
            goto Label_005B;
        Label_0015:
            NavigationWindow.Show(this.Template, LocalizedText.Get(this.TextID), this.Alignment);
            base.ActivateOutputLinks(10);
            goto Label_005B;
        Label_003F:
            NavigationWindow.DiscardCurrent();
            base.ActivateOutputLinks(10);
            base.ActivateOutputLinks(11);
        Label_005B:
            return;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            NavigationWindow.DiscardCurrent();
            return;
        }
    }
}

