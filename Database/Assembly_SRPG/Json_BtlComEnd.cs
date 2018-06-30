namespace SRPG
{
    using System;

    public class Json_BtlComEnd : Json_PlayerDataAll
    {
        public JSON_QuestProgress[] quests;
        public JSON_TrophyProgress[] trophyprogs;
        public Json_BtlQuestRanking quest_ranking;
        public Json_FirstClearItem[] fclr_items;
        public Json_BtlRewardConceptCard[] cards;
        public int is_mail_cards;

        public Json_BtlComEnd()
        {
            base..ctor();
            return;
        }
    }
}

