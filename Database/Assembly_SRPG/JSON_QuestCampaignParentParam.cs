namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_QuestCampaignParentParam
    {
        public string iname;
        public string begin_at;
        public string end_at;
        public string[] children;

        public JSON_QuestCampaignParentParam()
        {
            base..ctor();
            return;
        }
    }
}

