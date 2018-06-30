namespace SRPG
{
    using System;
    using System.Text;

    [Pin(0, "Setup", 0, 0), NodeType("UI/Helpwindow", 0x7fe5), Pin(100, "Finish", 1, 100)]
    public class FlowNode_HelpWindow : FlowNode
    {
        public FlowNode_HelpWindow()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            StringBuilder builder;
            if (pinID != null)
            {
                goto Label_0051;
            }
            builder = new StringBuilder();
            builder.Append(Network.SiteHost);
            builder.Append("notice/detail/help/index");
            FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.CONFIG_BTN_HELP"));
            FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", builder.ToString());
            base.ActivateOutputLinks(100);
        Label_0051:
            return;
        }
    }
}

