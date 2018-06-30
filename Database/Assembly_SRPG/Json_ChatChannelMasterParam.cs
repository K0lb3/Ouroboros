namespace SRPG
{
    using System;

    public class Json_ChatChannelMasterParam
    {
        public int pk;
        public Fields fields;

        public Json_ChatChannelMasterParam()
        {
            base..ctor();
            return;
        }

        public class Fields
        {
            public int id;
            public byte category_id;
            public string name;

            public Fields()
            {
                base..ctor();
                return;
            }
        }
    }
}

