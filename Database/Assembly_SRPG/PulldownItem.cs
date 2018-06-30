namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class PulldownItem : MonoBehaviour
    {
        public UnityEngine.UI.Text Text;
        public UnityEngine.UI.Graphic Graphic;
        public int Value;
        public Image Overray;

        public PulldownItem()
        {
            base..ctor();
            return;
        }

        public virtual void OnStatusChanged(bool enabled)
        {
        }
    }
}

