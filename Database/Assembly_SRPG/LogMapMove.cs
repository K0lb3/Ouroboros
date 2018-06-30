namespace SRPG
{
    using System;

    public class LogMapMove : BattleLog
    {
        public Unit self;
        public int ex;
        public int ey;
        public EUnitDirection dir;
        public bool auto;

        public LogMapMove()
        {
            base..ctor();
            return;
        }
    }
}

