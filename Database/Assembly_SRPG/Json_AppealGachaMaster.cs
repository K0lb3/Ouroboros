namespace SRPG
{
    using System;

    public class JSON_AppealGachaMaster
    {
        public int pk;
        public Fields fields;

        public JSON_AppealGachaMaster()
        {
            base..ctor();
            return;
        }

        public class Fields
        {
            public string appeal_id;
            public string start_at;
            public string end_at;
            public int flag_new;

            public Fields()
            {
                base..ctor();
                return;
            }
        }
    }
}

