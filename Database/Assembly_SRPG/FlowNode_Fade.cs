namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(0x65, "Fade In", 0, 1), Pin(1, "Finished", 1, 10), Pin(100, "Fade Out", 0, 0), NodeType("UI/Fade", 0x7fe5)]
    public class FlowNode_Fade : FlowNode
    {
        public FlowNode_Fade()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0048;
            }
            if (FadeController.InstanceExists != null)
            {
                goto Label_0027;
            }
            FadeController.Instance.FadeTo(Color.get_clear(), 0f, 0);
        Label_0027:
            FadeController.Instance.FadeTo(Color.get_black(), 1f, 0);
            base.set_enabled(1);
            goto Label_008B;
        Label_0048:
            if (pinID != 0x65)
            {
                goto Label_008B;
            }
            if (FadeController.InstanceExists != null)
            {
                goto Label_006F;
            }
            FadeController.Instance.FadeTo(Color.get_black(), 0f, 0);
        Label_006F:
            FadeController.Instance.FadeTo(Color.get_clear(), 1f, 0);
            base.set_enabled(1);
        Label_008B:
            return;
        }

        private void Update()
        {
            if (FadeController.Instance.IsFading(0) != null)
            {
                goto Label_0020;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        Label_0020:
            return;
        }
    }
}

