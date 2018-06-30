namespace SRPG
{
    using System;

    public class Json_ShopBuyConceptCard
    {
        public string iname;
        public int num;
        public string get_unit;

        public Json_ShopBuyConceptCard()
        {
            base..ctor();
            return;
        }

        public bool IsGetConceptCardUnit
        {
            get
            {
                return (string.IsNullOrEmpty(this.get_unit) == 0);
            }
        }
    }
}

