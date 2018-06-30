namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TobiraConditionParam
    {
        public int conds_type;
        public string conds_iname;

        public JSON_TobiraConditionParam()
        {
            base..ctor();
            return;
        }
    }
}

