namespace SRPG
{
    using System;

    public class JSON_ChatStampParam
    {
        public int pk;
        public Fields fields;

        public JSON_ChatStampParam()
        {
            base..ctor();
            return;
        }

        public class Fields
        {
            public int id;
            public string img_id;
            public string iname;
            public int is_private;

            public Fields()
            {
                base..ctor();
                return;
            }
        }
    }
}

