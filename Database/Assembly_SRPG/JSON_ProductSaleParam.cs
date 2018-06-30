namespace SRPG
{
    using System;

    public class JSON_ProductSaleParam
    {
        public int pk;
        public Fields fields;

        public JSON_ProductSaleParam()
        {
            base..ctor();
            return;
        }

        public class Fields
        {
            public int id;
            public string product_id;
            public string platform;
            public string name;
            public string description;
            public int additional_free_coin;
            public int condition_type;
            public string condition_value;

            public Fields()
            {
                base..ctor();
                return;
            }
        }
    }
}

