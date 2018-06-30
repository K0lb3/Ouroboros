namespace SRPG
{
    using System;

    public class LogFailCondition : BattleLog
    {
        public Unit self;
        public Unit source;
        public EUnitCondition condition;

        public LogFailCondition()
        {
            base..ctor();
            return;
        }
    }
}

