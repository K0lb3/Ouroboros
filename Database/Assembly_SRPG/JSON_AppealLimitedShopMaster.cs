namespace SRPG
{
    using System;

    public class JSON_AppealLimitedShopMaster
    {
        public int pk;
        public Fields fields;

        public JSON_AppealLimitedShopMaster()
        {
            base..ctor();
            return;
        }

        public class Fields
        {
            public string appeal_id;
            public string start_at;
            public string end_at;
            public int priority;
            public float position_chara;
            public float position_text;

            public Fields()
            {
                base..ctor();
                return;
            }
        }
    }
}

