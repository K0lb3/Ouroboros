namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(0x69, "遠征コイン不足", 1, 15), Pin(0x6c, "マルチコイン不足", 1, 0x12), Pin(0x6d, "イベントコイン不足", 1, 0x13), Pin(100, "Success", 1, 10), Pin(0x65, "ショップ情報が存在しない", 1, 11), Pin(1, "Request", 0, 0), Pin(0x67, "ゴールド不足", 1, 13), Pin(0x68, "課金コイン不足", 1, 14), NodeType("System/BuyItemUpdate", 0x7fe5), Pin(0x6a, "アリーナコイン不足", 1, 0x10), Pin(0x6b, "欠片ポイント不足", 1, 0x11)]
    public class FlowNode_BuyItemUpdate : FlowNode_Network
    {
        private EShopType mShopType;

        public FlowNode_BuyItemUpdate()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            ShopData data;
            ShopParam param;
            PlayerData data2;
            int num;
            string str;
            ESaleType type;
            if (pinID != 1)
            {
                goto Label_0257;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            this.mShopType = GlobalVars.ShopType;
            data = MonoSingleton<GameManager>.Instance.Player.GetShopData(this.mShopType);
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
            base.set_enabled(0);
            if (data == null)
            {
                goto Label_005D;
            }
            if (param != null)
            {
                goto Label_0067;
            }
        Label_005D:
            base.ActivateOutputLinks(0x65);
            return;
        Label_0067:
            data2 = MonoSingleton<GameManager>.Instance.Player;
            num = data2.GetShopUpdateCost(this.mShopType, 0);
            switch (param.UpdateCostType)
            {
                case 0:
                    goto Label_00B0;

                case 1:
                    goto Label_00CB;

                case 2:
                    goto Label_00E6;

                case 3:
                    goto Label_0101;

                case 4:
                    goto Label_011C;

                case 5:
                    goto Label_0137;

                case 6:
                    goto Label_0152;
            }
            goto Label_0177;
        Label_00B0:
            if (data2.Gold >= num)
            {
                goto Label_017C;
            }
            base.ActivateOutputLinks(0x67);
            return;
            goto Label_017C;
        Label_00CB:
            if (data2.Coin >= num)
            {
                goto Label_017C;
            }
            base.ActivateOutputLinks(0x68);
            return;
            goto Label_017C;
        Label_00E6:
            if (data2.TourCoin >= num)
            {
                goto Label_017C;
            }
            base.ActivateOutputLinks(0x69);
            return;
            goto Label_017C;
        Label_0101:
            if (data2.ArenaCoin >= num)
            {
                goto Label_017C;
            }
            base.ActivateOutputLinks(0x6a);
            return;
            goto Label_017C;
        Label_011C:
            if (data2.PiecePoint >= num)
            {
                goto Label_017C;
            }
            base.ActivateOutputLinks(0x6b);
            return;
            goto Label_017C;
        Label_0137:
            if (data2.MultiCoin >= num)
            {
                goto Label_017C;
            }
            base.ActivateOutputLinks(0x6c);
            return;
            goto Label_017C;
        Label_0152:
            if (data2.EventCoinNum(GlobalVars.EventShopItem.shop_cost_iname) >= num)
            {
                goto Label_017C;
            }
            base.ActivateOutputLinks(0x6d);
            return;
            goto Label_017C;
        Label_0177:;
        Label_017C:
            if (Network.Mode != null)
            {
                goto Label_0237;
            }
            if (this.mShopType != 9)
            {
                goto Label_01C8;
            }
            base.ExecRequest(new ReqItemEventShopUpdate(GlobalVars.EventShopItem.shops.gname, GlobalVars.EventShopItem.shop_cost_iname, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_022B;
        Label_01C8:
            if (this.mShopType != 10)
            {
                goto Label_0200;
            }
            base.ExecRequest(new ReqItemShopUpdate(GlobalVars.LimitedShopItem.shops.gname, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_022B;
        Label_0200:
            str = ((EShopType) this.mShopType).ToString();
            base.ExecRequest(new ReqItemShopUpdate(str, new Network.ResponseCallback(this.ResponseCallback)));
        Label_022B:
            base.set_enabled(1);
            goto Label_0257;
        Label_0237:
            if (data2.CheckShopUpdateCost(GlobalVars.ShopType) != null)
            {
                goto Label_0251;
            }
            base.ActivateOutputLinks(0x68);
            return;
        Label_0251:
            this.Success();
        Label_0257:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_EventShopUpdateResponse> response;
            List<JSON_EventShopItemListSet> list;
            EventShopData data;
            WebAPI.JSON_BodyResponse<Json_LimitedShopUpdateResponse> response2;
            List<JSON_LimitedShopItemListSet> list2;
            LimitedShopData data2;
            WebAPI.JSON_BodyResponse<Json_ShopUpdateResponse> response3;
            ShopData data3;
            ShopParam param;
            PlayerData data4;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_003C;
            }
            code = Network.ErrCode;
            if (code == 0x1068)
            {
                goto Label_002E;
            }
            if (code == 0x1069)
            {
                goto Label_002E;
            }
            goto Label_0035;
        Label_002E:
            this.OnBack();
            return;
        Label_0035:
            this.OnRetry();
            return;
        Label_003C:
            if (GlobalVars.ShopType != 9)
            {
                goto Label_00E8;
            }
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_EventShopUpdateResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0078;
            }
            this.OnRetry();
            return;
        Label_0078:
            list = new List<JSON_EventShopItemListSet>(response.body.shopitems);
            response.body.shopitems = list.ToArray();
            Network.RemoveAPI();
            data = MonoSingleton<GameManager>.Instance.Player.GetEventShopData();
            if (data != null)
            {
                goto Label_00BB;
            }
            data = new EventShopData();
        Label_00BB:
            if (data.Deserialize(response.body) != null)
            {
                goto Label_00D3;
            }
            this.OnFailed();
            return;
        Label_00D3:
            MonoSingleton<GameManager>.Instance.Player.SetEventShopData(data);
            goto Label_0229;
        Label_00E8:
            if (GlobalVars.ShopType != 10)
            {
                goto Label_019B;
            }
            response2 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_LimitedShopUpdateResponse>>(&www.text);
            DebugUtility.Assert((response2 == null) == 0, "res == null");
            if (response2.body != null)
            {
                goto Label_0124;
            }
            this.OnRetry();
            return;
        Label_0124:
            list2 = new List<JSON_LimitedShopItemListSet>(response2.body.shopitems);
            response2.body.shopitems = list2.ToArray();
            Network.RemoveAPI();
            data2 = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
            if (data2 != null)
            {
                goto Label_016C;
            }
            data2 = new LimitedShopData();
        Label_016C:
            if (data2.Deserialize(response2.body) != null)
            {
                goto Label_0185;
            }
            this.OnFailed();
            return;
        Label_0185:
            MonoSingleton<GameManager>.Instance.Player.SetLimitedShopData(data2);
            goto Label_0229;
        Label_019B:
            response3 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopUpdateResponse>>(&www.text);
            DebugUtility.Assert((response3 == null) == 0, "res == null");
            if (response3.body != null)
            {
                goto Label_01CE;
            }
            this.OnRetry();
            return;
        Label_01CE:
            Network.RemoveAPI();
            data3 = MonoSingleton<GameManager>.Instance.Player.GetShopData(this.mShopType);
            if (data3 != null)
            {
                goto Label_01F8;
            }
            data3 = new ShopData();
        Label_01F8:
            if (data3.Deserialize(response3.body) != null)
            {
                goto Label_0212;
            }
            this.OnFailed();
            return;
        Label_0212:
            MonoSingleton<GameManager>.Instance.Player.SetShopData(this.mShopType, data3);
        Label_0229:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
            if (param == null)
            {
                goto Label_0274;
            }
            data4 = MonoSingleton<GameManager>.Instance.Player;
            MyMetaps.TrackSpendShopUpdate(param.UpdateCostType, this.mShopType, data4.GetShopUpdateCost(this.mShopType, 1));
        Label_0274:
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

