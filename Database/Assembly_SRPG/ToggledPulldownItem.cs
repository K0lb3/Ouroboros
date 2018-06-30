namespace SRPG
{
    using System;
    using UnityEngine;

    public class ToggledPulldownItem : PulldownItem
    {
        public GameObject imageOn;
        public GameObject imageOff;

        public ToggledPulldownItem()
        {
            base..ctor();
            return;
        }

        public override void OnStatusChanged(bool enabled)
        {
            if ((this.imageOn != null) == null)
            {
                goto Label_001D;
            }
            this.imageOn.SetActive(enabled);
        Label_001D:
            if ((this.imageOff != null) == null)
            {
                goto Label_003D;
            }
            this.imageOff.SetActive(enabled == 0);
        Label_003D:
            return;
        }
    }
}

