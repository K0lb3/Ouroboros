namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using UnityEngine;

    public class FadeInOut : AnimEvent
    {
        public bool IsFadeOut;
        public bool IsAdditive;
        public Color FadeColor;

        public FadeInOut()
        {
            this.FadeColor = new Color(0f, 0f, 0f, 1f);
            base..ctor();
            return;
        }

        public override void OnStart(GameObject go)
        {
            if (this.IsFadeOut == null)
            {
                goto Label_0016;
            }
            this.FadeColor = Color.get_clear();
        Label_0016:
            FadeController.Instance.FadeTo(this.FadeColor, base.End - base.Start, (this.IsAdditive == null) ? 2 : 1);
            return;
        }
    }
}

