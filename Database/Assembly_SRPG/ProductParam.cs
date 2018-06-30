namespace SRPG
{
    using System;

    public class ProductParam
    {
        public string mProductId;
        public string mPlatform;
        public string mName;
        public string mDescription;
        public int mAdditionalPaidCoin;
        public int mAdditionalFreeCoin;
        public ProductSaleInfo mSale;

        public ProductParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ProductParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mProductId = json.product_id;
            this.mPlatform = json.platform;
            this.mName = json.name;
            this.mDescription = json.description;
            this.mAdditionalPaidCoin = json.additional_paid_coin;
            this.mAdditionalFreeCoin = json.additional_free_coin;
            if (json.sale == null)
            {
                goto Label_00FD;
            }
            this.mSale = new ProductSaleInfo();
            this.mSale.Name = (string.IsNullOrEmpty(json.sale.name) == null) ? json.sale.name : string.Empty;
            this.mSale.Description = (string.IsNullOrEmpty(json.sale.description) == null) ? json.sale.description : string.Empty;
            this.mSale.AdditionalFreeCoin = (json.sale.additional_free_coin > 0) ? json.sale.additional_free_coin : 0;
        Label_00FD:
            return 1;
        }

        public string ProductId
        {
            get
            {
                return this.mProductId;
            }
        }

        public string Platform
        {
            get
            {
                return this.mPlatform;
            }
        }

        public string Name
        {
            get
            {
                return (((this.mSale == null) || (string.IsNullOrEmpty(this.mSale.Name) != null)) ? this.mName : this.mSale.Name);
            }
        }

        public string Description
        {
            get
            {
                return (((this.mSale == null) || (string.IsNullOrEmpty(this.mSale.Description) != null)) ? this.mDescription : this.mSale.Description);
            }
        }

        public int AdditionalPaidCoin
        {
            get
            {
                return this.mAdditionalPaidCoin;
            }
        }

        public int AdditionalFreeCoin
        {
            get
            {
                return (((this.mSale == null) || (this.mSale.AdditionalFreeCoin <= 0)) ? this.mAdditionalFreeCoin : this.mSale.AdditionalFreeCoin);
            }
        }

        public class ProductSaleInfo
        {
            public string Name;
            public string Description;
            public int AdditionalFreeCoin;

            public ProductSaleInfo()
            {
                base..ctor();
                return;
            }
        }
    }
}

