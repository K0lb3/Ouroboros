namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_EventTrigger
    {
        public int trg;
        public int type;
        public int detail;
        public string val;
        public string sval;
        public int ival;
        public int cnt;
        public string tag;

        public JSON_EventTrigger()
        {
            base..ctor();
            return;
        }
    }
}

