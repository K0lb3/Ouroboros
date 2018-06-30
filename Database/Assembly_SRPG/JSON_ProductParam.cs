namespace SRPG
{
    using System;

    public class JSON_ProductParam
    {
        public string product_id;
        public string platform;
        public string name;
        public string description;
        public int additional_paid_coin;
        public int additional_free_coin;
        public JSON_ProductSaleInfo sale;

        public JSON_ProductParam()
        {
            base..ctor();
            return;
        }
    }
}

