namespace SRPG
{
    using System;

    public class AIAction
    {
        public OString skill;
        public OInt type;
        public OInt turn;
        public OBool notBlock;
        public eAIActionNoExecAct noExecAct;
        public int nextActIdx;
        public eAIActionNextTurnAct nextTurnAct;
        public int turnActIdx;
        public SkillLockCondition cond;

        public AIAction()
        {
            base..ctor();
            return;
        }
    }
}

