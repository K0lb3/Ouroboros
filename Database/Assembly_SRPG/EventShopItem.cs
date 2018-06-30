namespace SRPG
{
    using GR;
    using System;

    public class EventShopItem : ShopItem
    {
        public string cost_iname;
        public bool update_type;
        private JSON_EventShopItemListSet.Cost cost;

        public EventShopItem()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_EventShopItemListSet json)
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
            base.id = json.id;
            base.iname = json.item.iname;
            base.num = json.item.num;
            base.max_num = json.item.maxnum;
            base.bougthnum = json.item.boughtnum;
            base.saleValue = json.cost.value;
            base.saleType = ShopData.String2SaleType(json.cost.type);
            if (json.cost.iname == null)
            {
                goto Label_00ED;
            }
            this.cost_iname = json.cost.iname;
            goto Label_00FD;
        Label_00ED:
            this.cost_iname = GlobalVars.EventShopItem.shop_cost_iname;
        Label_00FD:
            if (base.saleType != 6)
            {
                goto Label_0116;
            }
            if (this.cost_iname != null)
            {
                goto Label_0116;
            }
            return 0;
        Label_0116:
            base.is_reset = json.isreset == 1;
            base.start = json.start;
            base.end = json.end;
            base.is_soldout = json.sold > 0;
            if (json.children == null)
            {
                goto Label_0163;
            }
            base.children = json.children;
        Label_0163:
            if (json.children == null)
            {
                goto Label_017A;
            }
            base.shopItemType = 3;
            goto Label_01B2;
        Label_017A:
            base.shopItemType = ShopData.String2ShopItemType(json.item.itype);
            if (base.shopItemType != 4)
            {
                goto Label_01B2;
            }
            base.shopItemType = ShopData.Iname2ShopItemType(json.item.iname);
        Label_01B2:
            if (base.IsConceptCard == null)
            {
                goto Label_01DD;
            }
            MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(base.iname, json.item.has_count);
        Label_01DD:
            return 1;
        }

        public void SetShopItem(ShopItem shop_item)
        {
            base.id = shop_item.id;
            base.iname = shop_item.iname;
            base.is_soldout = shop_item.is_soldout;
            base.num = shop_item.num;
            base.saleType = shop_item.saleType;
            base.saleValue = shop_item.saleValue;
            return;
        }
    }
}

