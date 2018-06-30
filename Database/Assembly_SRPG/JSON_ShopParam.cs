namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ShopParam
    {
        public string iname;
        public int upd_type;
        public int[] upd_costs;

        public JSON_ShopParam()
        {
            base..ctor();
            return;
        }
    }
}

