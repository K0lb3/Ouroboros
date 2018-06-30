namespace SRPG
{
    using System;

    public class QuestCampaignTrust
    {
        public string iname;
        public string concept_card;
        public int card_trust_lottery_rate;
        public int card_trust_qe_bonus;

        public QuestCampaignTrust()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_QuestCampaignTrust json)
        {
            this.iname = json.children_iname;
            this.concept_card = json.concept_card;
            this.card_trust_lottery_rate = json.card_trust_lottery_rate;
            this.card_trust_qe_bonus = json.card_trust_qe_bonus;
            return 1;
        }
    }
}

