namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    public class ProductSaleParam
    {
        public string ProductId;
        public string Platform;
        public string Name;
        public string Description;
        public int AdditionalFreeCoin;
        public Constrict Condition;

        public ProductSaleParam()
        {
            base..ctor();
            return;
        }

        public unsafe bool Deserialize(JSON_ProductSaleParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.ProductId = json.fields.product_id;
            this.Platform = json.fields.platform;
            this.Name = json.fields.name;
            this.Description = json.fields.description;
            this.AdditionalFreeCoin = json.fields.additional_free_coin;
            &this.Condition.type = json.fields.condition_type;
            &this.Condition.value = json.fields.condition_value;
            return 1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Constrict
        {
            public Type type;
            public string value;
            public int valueInt
            {
                get
                {
                    return int.Parse(this.value, 0xa7);
                }
            }
            public enum Type
            {
                None,
                TimesAMonth
            }
        }
    }
}

