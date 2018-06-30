namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class LogMapTrick : BattleLog
    {
        public SRPG.TrickData TrickData;
        public List<TargetInfo> TargetInfoLists;

        public LogMapTrick()
        {
            this.TargetInfoLists = new List<TargetInfo>();
            base..ctor();
            return;
        }

        public class TargetInfo
        {
            public Unit Target;
            public bool IsEffective;
            public int Heal;
            public int Damage;
            public EUnitCondition FailCondition;
            public EUnitCondition CureCondition;
            public Grid KnockBackGrid;

            public TargetInfo()
            {
                base..ctor();
                return;
            }
        }
    }
}

