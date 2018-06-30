namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class GalleryItem : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.UI.Button Button;
        [SerializeField]
        private GameObject UnknownImage;

        public GalleryItem()
        {
            base..ctor();
            return;
        }

        public void SetAvailable(bool isAvailable)
        {
            if ((this.Button != null) == null)
            {
                goto Label_001D;
            }
            this.Button.set_interactable(isAvailable);
        Label_001D:
            if ((this.UnknownImage != null) == null)
            {
                goto Label_003D;
            }
            this.UnknownImage.SetActive(isAvailable == 0);
        Label_003D:
            return;
        }
    }
}

