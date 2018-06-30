namespace SRPG
{
    using System;

    public class ReqChargeCheck : WebAPI
    {
        public ReqChargeCheck(PaymentManager.Product[] products, bool isPurchase, Network.ResponseCallback response)
        {
            int num;
            base..ctor();
            base.name = "charge/check";
            base.body = string.Empty;
            base.body = base.body + "\"targets\":[";
            num = 0;
            goto Label_00D0;
        Label_0039:
            base.body = base.body + "{";
            base.body = base.body + "\"product_id\":\"" + products[num].productID + "\",";
            base.body = base.body + "\"price\":" + ((double) products[num].sellPrice);
            base.body = base.body + "}";
            if (num == (((int) products.Length) - 1))
            {
                goto Label_00CC;
            }
            base.body = base.body + ",";
        Label_00CC:
            num += 1;
        Label_00D0:
            if (num < ((int) products.Length))
            {
                goto Label_0039;
            }
            base.body = base.body + "],";
            base.body = base.body + "\"currency\":\"JPY\",";
            base.body = base.body + "\"is_purchase\":" + ((isPurchase == null) ? "0" : "1");
            base.body = WebAPI.GetRequestString(base.body);
            base.callback = response;
            return;
        }
    }
}

