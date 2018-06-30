namespace SRPG
{
    using System;

    public class TowerQuestMissionTypeAttribute : Attribute
    {
        private QuestMissionProgressJudgeType m_ProgressJudgeType;

        public TowerQuestMissionTypeAttribute(QuestMissionProgressJudgeType progressJudgeType)
        {
            base..ctor();
            this.m_ProgressJudgeType = progressJudgeType;
            return;
        }

        public QuestMissionProgressJudgeType ProgressJudgeType
        {
            get
            {
                return this.m_ProgressJudgeType;
            }
        }
    }
}

