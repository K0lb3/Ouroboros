namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class ProductParamResponse
    {
        public List<ProductParam> products;

        public ProductParamResponse()
        {
            this.products = new List<ProductParam>();
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ProductParamResponse json)
        {
            int num;
            ProductParam param;
            if (json == null)
            {
                goto Label_0011;
            }
            if (json.products != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return 1;
        Label_0013:
            this.products.Clear();
            num = 0;
            goto Label_0050;
        Label_0025:
            param = new ProductParam();
            if (param.Deserialize(json.products[num]) != null)
            {
                goto Label_0040;
            }
            return 0;
        Label_0040:
            this.products.Add(param);
            num += 1;
        Label_0050:
            if (num < ((int) json.products.Length))
            {
                goto Label_0025;
            }
            return 1;
        }
    }
}

