namespace SRPG
{
    using System;
    using UnityEngine;

    public class QuestCampaignCreate : MonoBehaviour
    {
        [SerializeField]
        private GameObject QuestCampaignItem;
        private GameObject mGoQuestCampaignItem;

        public QuestCampaignCreate()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            Vector2 vector;
            Vector3 vector2;
            if ((this.QuestCampaignItem == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mGoQuestCampaignItem = Object.Instantiate<GameObject>(this.QuestCampaignItem);
            this.mGoQuestCampaignItem.SetActive(1);
            vector = this.mGoQuestCampaignItem.GetComponent<RectTransform>().get_anchoredPosition();
            vector2 = this.mGoQuestCampaignItem.get_transform().get_localScale();
            this.mGoQuestCampaignItem.get_transform().SetParent(base.get_transform());
            this.mGoQuestCampaignItem.GetComponent<RectTransform>().set_anchoredPosition(vector);
            this.mGoQuestCampaignItem.get_transform().set_localScale(vector2);
            return;
        }

        public QuestCampaignList GetQuestCampaignList
        {
            get
            {
                if ((this.mGoQuestCampaignItem == null) == null)
                {
                    goto Label_0013;
                }
                return null;
            Label_0013:
                return this.mGoQuestCampaignItem.GetComponent<QuestCampaignList>();
            }
        }
    }
}

