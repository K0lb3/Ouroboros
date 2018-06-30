namespace SRPG
{
    using System;

    public class JSON_AppealEventShopMaster
    {
        public Fields fields;

        public JSON_AppealEventShopMaster()
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

