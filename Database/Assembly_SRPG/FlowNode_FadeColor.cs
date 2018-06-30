namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(1, "Finished", 1, 10), Pin(100, "Start", 0, 0), NodeType("UI/Fade (Color)", 0x7fe5)]
    public class FlowNode_FadeColor : FlowNode
    {
        public UnityEngine.Color Color;
        public float Time;
        public bool ForceReset;

        public FlowNode_FadeColor()
        {
            this.Color = UnityEngine.Color.get_clear();
            this.Time = 1f;
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0082;
            }
            if (FadeController.InstanceExists == null)
            {
                goto Label_001D;
            }
            if (this.ForceReset == null)
            {
                goto Label_0064;
            }
        Label_001D:
            FadeController.Instance.FadeTo(new UnityEngine.Color(&this.Color.r, &this.Color.g, &this.Color.b, 1f - &this.Color.a), 0f, 0);
        Label_0064:
            FadeController.Instance.FadeTo(this.Color, this.Time, 0);
            base.set_enabled(1);
        Label_0082:
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

