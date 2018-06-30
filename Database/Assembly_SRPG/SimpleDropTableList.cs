namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class SimpleDropTableList
    {
        public List<SimpleDropTableParam> smp_drp_tbls;

        public SimpleDropTableList()
        {
            this.smp_drp_tbls = new List<SimpleDropTableParam>();
            base..ctor();
            return;
        }
    }
}

