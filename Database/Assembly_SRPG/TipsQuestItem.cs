namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class TipsQuestItem : MonoBehaviour
    {
        [SerializeField]
        private GameObject BadgeObj;
        [SerializeField]
        private GameObject CompleteObj;
        [SerializeField]
        private Text TitleTxt;
        [SerializeField]
        private Text NameTxt;
        public string Title;
        public string Name;
        public bool IsCompleted;

        public TipsQuestItem()
        {
            base..ctor();
            return;
        }

        public void UpdateContent()
        {
            if ((this.BadgeObj != null) == null)
            {
                goto Label_0025;
            }
            this.BadgeObj.SetActive(this.IsCompleted == 0);
        Label_0025:
            if ((this.CompleteObj != null) == null)
            {
                goto Label_0047;
            }
            this.CompleteObj.SetActive(this.IsCompleted);
        Label_0047:
            if ((this.TitleTxt != null) == null)
            {
                goto Label_0069;
            }
            this.TitleTxt.set_text(this.Title);
        Label_0069:
            if ((this.NameTxt != null) == null)
            {
                goto Label_008B;
            }
            this.NameTxt.set_text(this.Name);
        Label_008B:
            return;
        }
    }
}

