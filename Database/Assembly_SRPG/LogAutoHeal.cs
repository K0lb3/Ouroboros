namespace SRPG
{
    using System;

    public class LogAutoHeal : BattleLog
    {
        public Unit self;
        public HealType type;
        public int value;
        public int beforeHp;
        public int beforeMp;

        public LogAutoHeal()
        {
            base..ctor();
            return;
        }

        public enum HealType
        {
            Hp,
            Jewel
        }
    }
}

