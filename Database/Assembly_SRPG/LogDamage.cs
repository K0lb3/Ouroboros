namespace SRPG
{
    using System;

    public class LogDamage : BattleLog
    {
        public Unit self;
        public int damage;

        public LogDamage()
        {
            base..ctor();
            return;
        }
    }
}

