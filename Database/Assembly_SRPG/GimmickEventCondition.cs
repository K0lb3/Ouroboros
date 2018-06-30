namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class GimmickEventCondition
    {
        public GimmickEventTriggerType type;
        public List<Unit> units;
        public List<Unit> targets;
        public List<TrickData> td_targets;
        public string td_iname;
        public string td_tag;
        public List<Grid> grids;
        public int count;

        public GimmickEventCondition()
        {
            this.units = new List<Unit>();
            this.targets = new List<Unit>();
            this.td_targets = new List<TrickData>();
            base..ctor();
            return;
        }
    }
}

