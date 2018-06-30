namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class EnemyDropList
    {
        public List<SimpleDropTableList> drp_tbls;

        public EnemyDropList()
        {
            this.drp_tbls = new List<SimpleDropTableList>();
            base..ctor();
            return;
        }
    }
}

