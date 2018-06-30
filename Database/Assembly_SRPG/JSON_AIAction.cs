namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_AIAction
    {
        public string skill;
        public int type;
        public int turn;
        public int notBlock;
        public int noExecAct;
        public int nextActIdx;
        public int nextTurnAct;
        public int turnActIdx;
        public JSON_SkillLockCondition cond;

        public JSON_AIAction()
        {
            base..ctor();
            return;
        }
    }
}

