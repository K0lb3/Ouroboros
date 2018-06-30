namespace SRPG
{
    using System;
    using UnityEngine;

    public class BookmarkToggleButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject OnImage;
        [SerializeField]
        private GameObject OffImage;
        [SerializeField]
        private GameObject Shadow;

        public BookmarkToggleButton()
        {
            base..ctor();
            return;
        }

        public void Activate(bool doActivate)
        {
            this.OnImage.SetActive(doActivate);
            this.OffImage.SetActive(doActivate == 0);
            return;
        }

        public void EnableShadow(bool enabled)
        {
            this.Shadow.SetActive(enabled);
            return;
        }
    }
}

