namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(0x67, "アリーナショップが選択された", 1, 0x67), Pin(0x66, "更新なし：イベントショップが選択された", 1, 0x66), Pin(0x65, "更新あり：イベントショップが選択された", 1, 0x65), Pin(1, "所持コイン更新", 0, 1), Pin(0, "指定イベントショップの商品を開く", 0, 0), Pin(0x68, "マルチショップが選択された", 1, 0x68)]
    public class EventShopList : SRPG_ListBase, IFlowInterface
    {
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public GameObject ItemTemplate;
        public GameObject ArenaShopTemplate;
        public GameObject MultiShopTemplate;

        public EventShopList()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            GlobalVars.CoinListSelectionType type;
            if (pinID != null)
            {
                goto Label_0081;
            }
            switch ((GlobalVars.SelectionCoinListType - 1))
            {
                case 0:
                    goto Label_0025;

                case 1:
                    goto Label_0067;

                case 2:
                    goto Label_0074;
            }
            goto Label_0081;
        Label_0025:
            if ((GlobalVars.SelectionEventShop != null) == null)
            {
                goto Label_0081;
            }
            GlobalVars.EventShopItem = GlobalVars.SelectionEventShop.EventShopInfo;
            GlobalVars.ShopType = 9;
            Network.RequestAPI(new ReqGetCoinNum(new SRPG.Network.ResponseCallback(this.ResponseCallback)), 0);
            goto Label_0081;
        Label_0067:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
            goto Label_0081;
        Label_0074:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
        Label_0081:
            if (pinID != 1)
            {
                goto Label_0093;
            }
            GameParameter.UpdateAll(base.get_gameObject());
        Label_0093:
            return;
        }

        public void AddArenaShopList()
        {
            GameObject obj2;
            obj2 = Object.Instantiate<GameObject>(this.ArenaShopTemplate);
            obj2.get_transform().SetParent(base.get_transform());
            obj2.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
            obj2.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectArenaItem);
            obj2.SetActive(1);
            return;
        }

        public void AddEventShopList(JSON_ShopListArray.Shops[] shops)
        {
            int num;
            Json_ShopMsgResponse response;
            GameObject obj2;
            EventShopListItem item;
            num = 0;
            goto Label_009C;
        Label_0007:
            response = ParseMsg(shops[num]);
            if (response == null)
            {
                goto Label_0098;
            }
            if (response.hide == null)
            {
                goto Label_0026;
            }
            goto Label_0098;
        Label_0026:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            item = obj2.GetComponent<EventShopListItem>();
            item.SetShopList(shops[num], response);
            obj2.get_transform().SetParent(base.get_transform());
            obj2.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
            obj2.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            obj2.SetActive(1);
            GlobalVars.EventShopListItems.Add(item);
        Label_0098:
            num += 1;
        Label_009C:
            if (num < ((int) shops.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public void AddMultiShopList()
        {
            GameObject obj2;
            obj2 = Object.Instantiate<GameObject>(this.MultiShopTemplate);
            obj2.get_transform().SetParent(base.get_transform());
            obj2.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
            obj2.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectMultiItem);
            obj2.SetActive(1);
            return;
        }

        public void DestroyItems()
        {
            int num;
            if (GlobalVars.EventShopListItems.Count > 0)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            num = GlobalVars.EventShopListItems.Count - 1;
            goto Label_0063;
        Label_0023:
            if ((GlobalVars.EventShopListItems[num] != null) == null)
            {
                goto Label_005F;
            }
            GlobalVars.EventShopListItems[num].GetComponent<ListItemEvents>().OnSelect = null;
            Object.Destroy(GlobalVars.EventShopListItems[num]);
        Label_005F:
            num -= 1;
        Label_0063:
            if (num >= 0)
            {
                goto Label_0023;
            }
            GlobalVars.EventShopListItems.Clear();
            return;
        }

        private void OnSelectArenaItem(GameObject go)
        {
            GlobalVars.ShopType = 5;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
            return;
        }

        private void OnSelectItem(GameObject go)
        {
            EventShopListItem item;
            item = go.GetComponent<EventShopListItem>();
            if ((item == null) == null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            GlobalVars.EventShopItem = item.EventShopInfo;
            GlobalVars.ShopType = 9;
            Network.RequestAPI(new ReqGetCoinNum(new SRPG.Network.ResponseCallback(this.ResponseCallback)), 0);
            return;
        }

        private void OnSelectMultiItem(GameObject go)
        {
            GlobalVars.ShopType = 6;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
            return;
        }

        public static Json_ShopMsgResponse ParseMsg(JSON_ShopListArray.Shops shops)
        {
            Json_ShopMsgResponse response;
        Label_0000:
            try
            {
                response = JSONParser.parseJSONObject<Json_ShopMsgResponse>(shops.info.msg);
                goto Label_0042;
            }
            catch (Exception)
            {
            Label_001B:
                Debug.LogError("Parse failed: " + shops.info.msg);
                response = null;
                goto Label_0042;
            }
        Label_0042:
            return response;
        }

        private unsafe void ResponseCallback(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_CoinNum> response;
            GlobalVars.SummonCoinInfo info;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_001C;
            }
            FlowNode_Network.Retry();
            return;
        Label_001C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_CoinNum>>(&www.text);
            Network.RemoveAPI();
            if (response.body == null)
            {
                goto Label_0076;
            }
            if (response.body.item == null)
            {
                goto Label_0076;
            }
            if (((int) response.body.item.Length) <= 0)
            {
                goto Label_0076;
            }
            MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.item);
        Label_0076:
            if (response.body == null)
            {
                goto Label_00B3;
            }
            if (response.body.newcoin == null)
            {
                goto Label_00B3;
            }
            info = new GlobalVars.SummonCoinInfo();
            info.Period = response.body.newcoin.period;
            GlobalVars.NewSummonCoinInfo = info;
        Label_00B3:
            if (GlobalVars.EventShopItem.btn_update == null)
            {
                goto Label_00CF;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            goto Label_00D7;
        Label_00CF:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
        Label_00D7:
            return;
        }

        protected override void Start()
        {
            if (this.ItemTemplate == null)
            {
                goto Label_001C;
            }
            this.ItemTemplate.SetActive(0);
        Label_001C:
            if (this.ArenaShopTemplate == null)
            {
                goto Label_0038;
            }
            this.ArenaShopTemplate.SetActive(0);
        Label_0038:
            if (this.MultiShopTemplate == null)
            {
                goto Label_0054;
            }
            this.MultiShopTemplate.SetActive(0);
        Label_0054:
            base.Start();
            return;
        }

        private class JSON_CoinNum
        {
            public Json_Item[] item;
            public EventShopList.JSON_NewCoin newcoin;

            public JSON_CoinNum()
            {
                base..ctor();
                return;
            }
        }

        private class JSON_NewCoin
        {
            public long period;

            public JSON_NewCoin()
            {
                base..ctor();
                return;
            }
        }
    }
}

