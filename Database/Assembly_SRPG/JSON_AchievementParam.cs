namespace SRPG
{
    using System;

    public class JSON_AchievementParam
    {
        public int pk;
        public Fields fields;

        public JSON_AchievementParam()
        {
            base..ctor();
            return;
        }

        public class Fields
        {
            public int id;
            public string iname;
            public string ios;
            public string googleplay;

            public Fields()
            {
                base..ctor();
                return;
            }
        }
    }
}

