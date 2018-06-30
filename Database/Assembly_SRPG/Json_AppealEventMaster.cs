namespace SRPG
{
    using System;

    public class JSON_AppealEventMaster
    {
        public int pk;
        public Fields fields;

        public JSON_AppealEventMaster()
        {
            base..ctor();
            return;
        }

        public class Fields
        {
            public string appeal_id;
            public string start_at;
            public string end_at;

            public Fields()
            {
                base..ctor();
                return;
            }
        }
    }
}

