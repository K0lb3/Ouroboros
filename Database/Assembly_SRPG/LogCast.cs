namespace SRPG
{
    using System;

    public class LogCast : BattleLog
    {
        public Unit self;
        public int dx;
        public int dy;
        public ECastTypes type;

        public LogCast()
        {
            base..ctor();
            return;
        }
    }
}

