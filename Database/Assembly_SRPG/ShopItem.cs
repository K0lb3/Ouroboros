namespace SRPG
{
    using GR;
    using System;

    public class ShopItem
    {
        public int id;
        public string iname;
        public int num;
        public ESaleType saleType;
        public bool is_soldout;
        public int saleValue;
        public int max_num;
        public int bougthnum;
        public Json_ShopItemDesc[] children;
        public bool is_reset;
        public long start;
        public long end;
        public int discount;
        protected EShopItemType shopItemType;

        public ShopItem()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(Json_ShopItem json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (json.item != null)
            {
                goto Label_0015;
            }
            return 0;
        Label_0015:
            if (string.IsNullOrEmpty(json.item.iname) == null)
            {
                goto Label_002C;
            }
            return 0;
        Label_002C:
            if (json.cost != null)
            {
                goto Label_0039;
            }
            return 0;
        Label_0039:
            if (string.IsNullOrEmpty(json.cost.type) == null)
            {
                goto Label_0050;
            }
            return 0;
        Label_0050:
            this.id = json.id;
            this.iname = json.item.iname;
            this.num = json.item.num;
            this.max_num = json.item.maxnum;
            this.bougthnum = json.item.boughtnum;
            this.saleValue = json.cost.value;
            this.saleType = ShopData.String2SaleType(json.cost.type);
            this.is_reset = json.isreset == 1;
            this.is_soldout = json.sold > 0;
            this.start = json.start;
            this.end = json.end;
            this.discount = json.discount;
            if (json.children == null)
            {
                goto Label_0120;
            }
            this.children = json.children;
        Label_0120:
            if (json.children == null)
            {
                goto Label_0137;
            }
            this.shopItemType = 3;
            goto Label_016F;
        Label_0137:
            this.shopItemType = ShopData.String2ShopItemType(json.item.itype);
            if (this.shopItemType != 4)
            {
                goto Label_016F;
            }
            this.shopItemType = ShopData.Iname2ShopItemType(json.item.iname);
        Label_016F:
            if (this.IsConceptCard == null)
            {
                goto Label_019A;
            }
            MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(this.iname, json.item.has_count);
        Label_019A:
            return 1;
        }

        public int remaining_num
        {
            get
            {
                return (this.max_num - this.bougthnum);
            }
        }

        public bool IsNotLimited
        {
            get
            {
                return (this.max_num == 0);
            }
        }

        public bool isSetSaleValue
        {
            get
            {
                return (this.saleValue > 0);
            }
        }

        public bool IsSet
        {
            get
            {
                return ((this.children == null) ? 0 : (((int) this.children.Length) > 0));
            }
        }

        public bool IsItem
        {
            get
            {
                return (this.shopItemType == 0);
            }
        }

        public bool IsArtifact
        {
            get
            {
                return (this.shopItemType == 1);
            }
        }

        public bool IsConceptCard
        {
            get
            {
                return (this.shopItemType == 2);
            }
        }

        public EShopItemType ShopItemType
        {
            get
            {
                return this.shopItemType;
            }
        }
    }
}

