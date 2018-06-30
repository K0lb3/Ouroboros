namespace SRPG
{
    using System;

    public class Json_VersusEndEnd : Json_PlayerDataAll
    {
        public JSON_QuestProgress[] quests;
        public int wincnt;
        public int win_bonus;
        public int key;
        public int rankup;
        public int floor;
        public int arravied;

        public Json_VersusEndEnd()
        {
            base..ctor();
            return;
        }
    }
}

