namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TobiraCondsParam
    {
        public string unit_iname;
        public int category;
        public JSON_TobiraConditionParam[] conds;

        public JSON_TobiraCondsParam()
        {
            base..ctor();
            return;
        }
    }
}

