namespace SRPG
{
    using System;

    public class RaidQuestResult
    {
        public int index;
        public int pexp;
        public int uexp;
        public int gold;
        public QuestResult.DropItemData[] drops;

        public RaidQuestResult()
        {
            base..ctor();
            return;
        }
    }
}

