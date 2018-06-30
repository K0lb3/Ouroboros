namespace SRPG
{
    using System;

    public class ReqQuestRankingPartyData
    {
        public int m_ScheduleID;
        public RankingQuestType m_RankingType;
        public string m_QuestID;
        public string m_TargetUID;

        public ReqQuestRankingPartyData()
        {
            base..ctor();
            return;
        }
    }
}

