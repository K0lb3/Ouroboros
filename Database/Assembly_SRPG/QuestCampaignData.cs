namespace SRPG
{
    using System;

    public class QuestCampaignData
    {
        public QuestCampaignValueTypes type;
        public string unit;
        public int value;

        public QuestCampaignData()
        {
            base..ctor();
            return;
        }

        public float GetRate()
        {
            return (((float) this.value) / 100f);
        }
    }
}

