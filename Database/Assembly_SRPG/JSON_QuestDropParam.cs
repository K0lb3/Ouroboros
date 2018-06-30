namespace SRPG
{
    using System;

    public class JSON_QuestDropParam
    {
        public JSON_SimpleDropTableParam[] simpleDropTable;
        public JSON_SimpleLocalMapsParam[] simpleLocalMaps;
        public JSON_SimpleQuestDropParam[] simpleQuestDrops;

        public JSON_QuestDropParam()
        {
            base..ctor();
            return;
        }
    }
}

