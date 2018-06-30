namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/フィルタ(2D)", "画面に効果を適応します", 0x555555, 0x444488)]
    public class Event2dAction_Filter : EventAction
    {
        public FilterType filter;

        public Event2dAction_Filter()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            FilterType type;
            switch (this.filter)
            {
                case 0:
                    goto Label_001E;

                case 1:
                    goto Label_0050;

                case 2:
                    goto Label_0037;
            }
            goto Label_0069;
        Label_001E:
            Shader.DisableKeyword("EVENT_SEPIA_ON");
            Shader.DisableKeyword("EVENT_MONOCHROME_ON");
            goto Label_0069;
        Label_0037:
            Shader.EnableKeyword("EVENT_SEPIA_ON");
            Shader.DisableKeyword("EVENT_MONOCHROME_ON");
            goto Label_0069;
        Label_0050:
            Shader.DisableKeyword("EVENT_SEPIA_ON");
            Shader.EnableKeyword("EVENT_MONOCHROME_ON");
        Label_0069:
            base.ActivateNext();
            return;
        }

        public enum FilterType
        {
            None,
            Monochrome,
            Sepia
        }
    }
}

