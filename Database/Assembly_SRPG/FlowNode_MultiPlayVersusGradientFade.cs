namespace SRPG
{
    using System;

    [Pin(10, "Finish", 1, 10), Pin(2, "Fade Out", 0, 2), NodeType("Multi/GradientFade", 0x7fe5), Pin(1, "Fade In", 0, 1)]
    public class FlowNode_MultiPlayVersusGradientFade : FlowNode
    {
        private const int PIN_IN_FADE_IN = 1;
        private const int PIN_IN_FADE_OUT = 2;
        private const int PIN_OUT_FINISH = 10;
        private bool mFading;

        public FlowNode_MultiPlayVersusGradientFade()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            MultiPlayVersusGradientFade fade;
            fade = MultiPlayVersusGradientFade.Instance;
            if ((fade == null) == null)
            {
                goto Label_001D;
            }
            DebugUtility.Log("MultiPlayVersus専用です");
            return;
        Label_001D:
            if (pinID != 1)
            {
                goto Label_002F;
            }
            fade.FadeIn();
            goto Label_0035;
        Label_002F:
            fade.FadeOut();
        Label_0035:
            base.set_enabled(1);
            this.mFading = 1;
            return;
        }

        private void Update()
        {
            MultiPlayVersusGradientFade fade;
            if (this.mFading != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            fade = MultiPlayVersusGradientFade.Instance;
            if ((fade == null) == null)
            {
                goto Label_0036;
            }
            this.mFading = 0;
            base.ActivateOutputLinks(10);
            base.set_enabled(0);
            return;
        Label_0036:
            if (fade.Fading == null)
            {
                goto Label_0042;
            }
            return;
        Label_0042:
            this.mFading = 0;
            base.ActivateOutputLinks(10);
            base.set_enabled(0);
            return;
        }
    }
}

