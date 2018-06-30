namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_QuestCampaignTrust
    {
        public string children_iname;
        public string concept_card;
        public int card_trust_lottery_rate;
        public int card_trust_qe_bonus;

        public JSON_QuestCampaignTrust()
        {
            base..ctor();
            return;
        }
    }
}

