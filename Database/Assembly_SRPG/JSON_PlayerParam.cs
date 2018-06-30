namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_PlayerParam
    {
        public int pt;
        public int ucap;
        public int icap;
        public int ecap;
        public int fcap;

        public JSON_PlayerParam()
        {
            base..ctor();
            return;
        }
    }
}

