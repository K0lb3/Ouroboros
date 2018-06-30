namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(0x65, "限定ショップが選択された", 1, 0x65)]
    public class LimitedShopList : SRPG_ListBase, IFlowInterface
    {
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public GameObject ItemTemplate;
        [Range(0f, 100f)]
        public int NowOpenShopCount;
        private List<LimitedShopListItem> limited_shop_list;

        public LimitedShopList()
        {
            this.limited_shop_list = new List<LimitedShopListItem>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void DestroyItems()
        {
            int num;
            if (this.limited_shop_list.Count > 0)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = this.NowOpenShopCount - 1;
            goto Label_004C;
        Label_0020:
            this.limited_shop_list[num].GetComponent<ListItemEvents>().OnSelect = null;
            Object.Destroy(this.limited_shop_list[num]);
            num -= 1;
        Label_004C:
            if (num >= 0)
            {
                goto Label_0020;
            }
            this.limited_shop_list.Clear();
            return;
        }

        private void OnSelectItem(GameObject go)
        {
            LimitedShopListItem item;
            item = go.GetComponent<LimitedShopListItem>();
            if ((item == null) == null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            GlobalVars.LimitedShopItem = item;
            GlobalVars.ShopType = 10;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        private void RefreshItems(JSON_ShopListArray.Shops[] shops)
        {
            int num;
            GameObject obj2;
            LimitedShopListItem item;
            this.NowOpenShopCount = (int) shops.Length;
            num = this.limited_shop_list.Count;
            goto Label_0090;
        Label_001A:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            item = obj2.GetComponent<LimitedShopListItem>();
            item.SetShopList(shops[num]);
            obj2.get_transform().SetParent(base.get_transform());
            obj2.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
            obj2.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            obj2.SetActive(1);
            this.limited_shop_list.Add(item);
            num += 1;
        Label_0090:
            if (num < this.NowOpenShopCount)
            {
                goto Label_001A;
            }
            return;
        }

        public void SetLimitedShopList(JSON_ShopListArray.Shops[] shops)
        {
            this.DestroyItems();
            this.RefreshItems(shops);
            return;
        }

        protected override void Start()
        {
            base.Start();
            return;
        }
    }
}

