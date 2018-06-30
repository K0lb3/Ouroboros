namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class TipsItem : MonoBehaviour
    {
        [SerializeField]
        private GameObject BadgeObj;
        [SerializeField]
        private GameObject CompleteObj;
        [SerializeField]
        private Text TitleObj;
        [SerializeField]
        private GameObject OverayImageObj;
        [SerializeField]
        private Button SelfButton;
        public string Title;
        public bool IsCompleted;
        public bool IsHidden;

        public TipsItem()
        {
            base..ctor();
            return;
        }

        public void UpdateContent()
        {
            if ((this.BadgeObj != null) == null)
            {
                goto Label_0033;
            }
            this.BadgeObj.SetActive((this.IsHidden != null) ? 0 : (this.IsCompleted == 0));
        Label_0033:
            if ((this.CompleteObj != null) == null)
            {
                goto Label_0063;
            }
            this.CompleteObj.SetActive((this.IsHidden != null) ? 0 : this.IsCompleted);
        Label_0063:
            if ((this.TitleObj != null) == null)
            {
                goto Label_0085;
            }
            this.TitleObj.set_text(this.Title);
        Label_0085:
            if ((this.OverayImageObj != null) == null)
            {
                goto Label_00A7;
            }
            this.OverayImageObj.SetActive(this.IsHidden);
        Label_00A7:
            if ((this.SelfButton != null) == null)
            {
                goto Label_00CC;
            }
            this.SelfButton.set_interactable(this.IsHidden == 0);
        Label_00CC:
            return;
        }
    }
}

