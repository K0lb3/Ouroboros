namespace SRPG
{
    using System;

    public class Json_TrophyPlayerDataAll
    {
        public Json_TrophyPlayerData player;
        public Json_Unit[] units;
        public Json_Item[] items;
        public Json_TrophyConceptCards concept_cards;

        public Json_TrophyPlayerDataAll()
        {
            base..ctor();
            return;
        }
    }
}

