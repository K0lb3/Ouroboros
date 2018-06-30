namespace SRPG
{
    using System;

    public class QuestMissionTypeAttribute : Attribute
    {
        private QuestMissionValueType m_ValueType;

        public QuestMissionTypeAttribute(QuestMissionValueType valueType)
        {
            base..ctor();
            this.m_ValueType = valueType;
            return;
        }

        public QuestMissionValueType ValueType
        {
            get
            {
                return this.m_ValueType;
            }
        }
    }
}

