namespace SRPG
{
    using GR;
    using System;

    [Pin(2, "Failed", 1, 2), Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0), NodeType("System/ReqCoinShop", 0x7fe5)]
    public class FlowNode_ReqCoinShop : FlowNode_Network
    {
        private Mode mode;

        public FlowNode_ReqCoinShop()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0024;
            }
            this.mode = 0;
            base.ExecRequest(new ReqEventShopList(new Network.ResponseCallback(this.ResponseCallback)));
        Label_0024:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            char[] chArray1;
            WebAPI.JSON_BodyResponse<JSON_ShopListArray> response;
            int num;
            JSON_ShopListArray.Shops shops;
            JSON_ShopListArray.Shops[] shopsArray;
            int num2;
            string[] strArray;
            EventShopInfo info;
            Json_ShopMsgResponse response2;
            WebAPI.JSON_BodyResponse<JSON_CoinNum> response3;
            GlobalVars.SummonCoinInfo info2;
            if (Network.IsError == null)
            {
                goto Label_0011;
            }
            this.OnRetry();
            return;
        Label_0011:
            if (this.mode != null)
            {
                goto Label_0187;
            }
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ShopListArray>>(&www.text);
            Network.RemoveAPI();
            if ((response.body.shops == null) || (((int) response.body.shops.Length) <= 0))
            {
                goto Label_023E;
            }
            num = 0;
            goto Label_0075;
        Label_0058:
            if (response.body.shops[num] != null)
            {
                goto Label_0071;
            }
            this.OnRetry();
            return;
        Label_0071:
            num += 1;
        Label_0075:
            if (num < ((int) response.body.shops.Length))
            {
                goto Label_0058;
            }
            shopsArray = response.body.shops;
            num2 = 0;
            goto Label_0178;
        Label_009C:
            shops = shopsArray[num2];
            chArray1 = new char[] { 0x2d };
            if ((shops.gname.Split(chArray1)[0] == "EventSummon2") == null)
            {
                goto Label_0172;
            }
            MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
            info = new EventShopInfo();
            info.shops = shops;
            response2 = EventShopList.ParseMsg(shops);
            if (response2 == null)
            {
                goto Label_0172;
            }
            info.banner_sprite = response2.banner;
            info.shop_cost_iname = response2.costiname;
            if (response2.update == null)
            {
                goto Label_0145;
            }
            info.btn_update = (response2.update.Equals("on") == null) ? 0 : 1;
        Label_0145:
            GlobalVars.EventShopItem = info;
            GlobalVars.ShopType = 9;
            this.mode = 1;
            base.ExecRequest(new ReqGetCoinNum(new Network.ResponseCallback(this.ResponseCallback)));
            return;
        Label_0172:
            num2 += 1;
        Label_0178:
            if (num2 < ((int) shopsArray.Length))
            {
                goto Label_009C;
            }
            goto Label_023E;
        Label_0187:
            if (this.mode != 1)
            {
                goto Label_023E;
            }
            response3 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_CoinNum>>(&www.text);
            Network.RemoveAPI();
            if (response3.body == null)
            {
                goto Label_01F2;
            }
            if (response3.body.item == null)
            {
                goto Label_01F2;
            }
            if (((int) response3.body.item.Length) <= 0)
            {
                goto Label_01F2;
            }
            MonoSingleton<GameManager>.Instance.Player.Deserialize(response3.body.item);
        Label_01F2:
            if (response3.body == null)
            {
                goto Label_0235;
            }
            if (response3.body.newcoin == null)
            {
                goto Label_0235;
            }
            info2 = new GlobalVars.SummonCoinInfo();
            info2.Period = response3.body.newcoin.period;
            GlobalVars.NewSummonCoinInfo = info2;
        Label_0235:
            base.ActivateOutputLinks(1);
            return;
        Label_023E:
            base.ActivateOutputLinks(2);
            return;
        }

        private class JSON_CoinNum
        {
            public Json_Item[] item;
            public FlowNode_ReqCoinShop.JSON_NewCoin newcoin;

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

        private enum Mode
        {
            GetShopList,
            GetCoinNum
        }
    }
}

