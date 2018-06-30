namespace SRPG
{
    using System;

    public class LogCureCondition : BattleLog
    {
        public Unit self;
        public EUnitCondition condition;

        public LogCureCondition()
        {
            base..ctor();
            return;
        }
    }
}

