namespace SRPG
{
    using System;

    public class LogUnitEntry : BattleLog
    {
        public Unit self;
        public Unit kill_unit;

        public LogUnitEntry()
        {
            base..ctor();
            return;
        }
    }
}

