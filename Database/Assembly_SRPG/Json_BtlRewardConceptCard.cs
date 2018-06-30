namespace SRPG
{
    using System;

    public class Json_BtlRewardConceptCard
    {
        public string iname;
        public int num;
        public string get_unit;
        public long get_unit_iid;

        public Json_BtlRewardConceptCard()
        {
            base..ctor();
            return;
        }

        public bool IsGetUnit
        {
            get
            {
                return ((string.IsNullOrEmpty(this.get_unit) != null) ? 0 : ((this.get_unit_iid == 0L) == 0));
            }
        }
    }
}

