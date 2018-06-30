namespace SRPG
{
    using System;

    public class Json_GachaExcite
    {
        public int pk;
        public Fields fields;

        public Json_GachaExcite()
        {
            base..ctor();
            return;
        }

        public class Fields
        {
            public int id;
            public int rarity;
            public int weight;
            public string color1;
            public string color2;
            public string color3;
            public string color4;
            public string color5;

            public Fields()
            {
                base..ctor();
                return;
            }
        }
    }
}

