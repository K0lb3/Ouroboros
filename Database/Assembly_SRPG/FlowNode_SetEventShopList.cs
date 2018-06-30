namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(10, "Success", 1, 10), Pin(1, "Request", 0, 0), NodeType("System/SetEventShopList", 0x7fe5), Pin(2, "IsEventShop", 0, 2)]
    public class FlowNode_SetEventShopList : FlowNode_Network
    {
        private Mode mode;
        public EventShopList event_shop_list;
        private int inputPin;

        public FlowNode_SetEventShopList()
        {
            this.inputPin = 1;
            base..ctor();
            return;
        }

        private void EraseTimeOutPopupHistory(JSON_ShopListArray.Shops[] shops)
        {
            string str;
            string str2;
            ShopTimeOutItemInfoArray array;
            List<ShopTimeOutItemInfo> list;
            ShopTimeOutItemInfo[] infoArray;
            int num;
            ShopTimeOutItemInfoArray array2;
            string str3;
            <EraseTimeOutPopupHistory>c__AnonStorey27C storeyc;
            if (shops == null)
            {
                goto Label_000F;
            }
            if (((int) shops.Length) > 0)
            {
                goto Label_002C;
            }
        Label_000F:
            str = JsonUtility.ToJson(new ShopTimeOutItemInfoArray());
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, str, 0);
            goto Label_00E0;
        Label_002C:
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT) == null)
            {
                goto Label_00E0;
            }
            array = JsonUtility.FromJson<ShopTimeOutItemInfoArray>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, string.Empty));
            list = new List<ShopTimeOutItemInfo>();
            if (array.Infos == null)
            {
                goto Label_00BC;
            }
            storeyc = new <EraseTimeOutPopupHistory>c__AnonStorey27C();
            infoArray = array.Infos;
            num = 0;
            goto Label_00B1;
        Label_007A:
            storeyc.info = infoArray[num];
            if (Enumerable.Any<JSON_ShopListArray.Shops>(shops, new Func<JSON_ShopListArray.Shops, bool>(storeyc.<>m__1CA)) == null)
            {
                goto Label_00AB;
            }
            list.Add(storeyc.info);
        Label_00AB:
            num += 1;
        Label_00B1:
            if (num < ((int) infoArray.Length))
            {
                goto Label_007A;
            }
        Label_00BC:
            array2 = new ShopTimeOutItemInfoArray(list.ToArray());
            str3 = JsonUtility.ToJson(array2);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, str3, 0);
        Label_00E0:
            return;
        }

        public long GetEventShopPeriodEndAt(JSON_ShopListArray list)
        {
            long num;
            int num2;
            long num3;
            num = 0L;
            if ((list == null) || (((int) list.shops.Length) <= 0))
            {
                goto Label_004D;
            }
            num2 = 0;
            goto Label_003F;
        Label_001E:
            num3 = list.shops[num2].end;
            num = (num >= num3) ? num : num3;
            num2 += 1;
        Label_003F:
            if (num2 < ((int) list.shops.Length))
            {
                goto Label_001E;
            }
        Label_004D:
            return num;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID == 1)
            {
                goto Label_000E;
            }
            if (pinID != 2)
            {
                goto Label_005B;
            }
        Label_000E:
            if (base.get_enabled() == null)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            this.inputPin = pinID;
            if (Network.Mode != null)
            {
                goto Label_0055;
            }
            this.mode = 0;
            base.ExecRequest(new ReqEventShopList(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_005B;
        Label_0055:
            this.Success();
        Label_005B:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ShopListArray> response;
            int num;
            WebAPI.JSON_BodyResponse<JSON_CoinNum> response2;
            GlobalVars.SummonCoinInfo info;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0018;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0018:
            if (this.mode != null)
            {
                goto Label_0126;
            }
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ShopListArray>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            this.event_shop_list.DestroyItems();
            if (response.body.shops == null)
            {
                goto Label_00DC;
            }
            if (((int) response.body.shops.Length) <= 0)
            {
                goto Label_00DC;
            }
            num = 0;
            goto Label_0098;
        Label_007B:
            if (response.body.shops[num] != null)
            {
                goto Label_0094;
            }
            this.OnRetry();
            return;
        Label_0094:
            num += 1;
        Label_0098:
            if (num < ((int) response.body.shops.Length))
            {
                goto Label_007B;
            }
            MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
            if (this.inputPin == 2)
            {
                goto Label_00DC;
            }
            this.event_shop_list.AddEventShopList(response.body.shops);
        Label_00DC:
            this.event_shop_list.AddArenaShopList();
            this.event_shop_list.AddMultiShopList();
            this.EraseTimeOutPopupHistory(response.body.shops);
            this.mode = 1;
            base.ExecRequest(new ReqGetCoinNum(new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_01CF;
        Label_0126:
            if (this.mode != 1)
            {
                goto Label_01CF;
            }
            response2 = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_CoinNum>>(&www.text);
            Network.RemoveAPI();
            if (response2.body == null)
            {
                goto Label_018C;
            }
            if (response2.body.item == null)
            {
                goto Label_018C;
            }
            if (((int) response2.body.item.Length) <= 0)
            {
                goto Label_018C;
            }
            MonoSingleton<GameManager>.Instance.Player.Deserialize(response2.body.item);
        Label_018C:
            if (response2.body == null)
            {
                goto Label_01C9;
            }
            if (response2.body.newcoin == null)
            {
                goto Label_01C9;
            }
            info = new GlobalVars.SummonCoinInfo();
            info.Period = response2.body.newcoin.period;
            GlobalVars.NewSummonCoinInfo = info;
        Label_01C9:
            this.Success();
        Label_01CF:
            return;
        }

        public void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }

        [CompilerGenerated]
        private sealed class <EraseTimeOutPopupHistory>c__AnonStorey27C
        {
            internal ShopTimeOutItemInfo info;

            public <EraseTimeOutPopupHistory>c__AnonStorey27C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1CA(JSON_ShopListArray.Shops sh)
            {
                return (sh.gname == this.info.ShopId);
            }
        }

        private class JSON_CoinNum
        {
            public Json_Item[] item;
            public FlowNode_SetEventShopList.JSON_NewCoin newcoin;

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

