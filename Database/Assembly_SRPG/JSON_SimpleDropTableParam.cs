namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_SimpleDropTableParam
    {
        public string iname;
        public string begin_at;
        public string end_at;
        public string[] droplist;
        public string[] dropcards;

        public JSON_SimpleDropTableParam()
        {
            base..ctor();
            return;
        }
    }
}

