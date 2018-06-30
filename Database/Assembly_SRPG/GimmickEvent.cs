namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class GimmickEvent
    {
        public eGimmickEventType ev_type;
        public List<string> skills;
        public List<Unit> users;
        public List<Unit> targets;
        public List<TrickData> td_targets;
        public string td_iname;
        public string td_tag;
        public GimmickEventCondition condition;
        public int count;
        public bool IsCompleted;
        public bool IsStarter;
        public Unit starter;

        public GimmickEvent()
        {
            this.skills = new List<string>();
            this.users = new List<Unit>();
            this.targets = new List<Unit>();
            this.td_targets = new List<TrickData>();
            this.condition = new GimmickEventCondition();
            base..ctor();
            return;
        }
    }
}

