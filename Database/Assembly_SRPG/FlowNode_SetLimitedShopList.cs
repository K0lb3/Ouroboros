namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(2, "IsLimitedShop", 0, 2), NodeType("System/SetLimitedShopList", 0x7fe5), Pin(1, "Request", 0, 0), Pin(10, "Success", 1, 10)]
    public class FlowNode_SetLimitedShopList : FlowNode_Network
    {
        public LimitedShopList limited_shop_list;
        private int inputPin;

        public FlowNode_SetLimitedShopList()
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
            <EraseTimeOutPopupHistory>c__AnonStorey27D storeyd;
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
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, str, 0);
            goto Label_00E0;
        Label_002C:
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT) == null)
            {
                goto Label_00E0;
            }
            array = JsonUtility.FromJson<ShopTimeOutItemInfoArray>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, string.Empty));
            list = new List<ShopTimeOutItemInfo>();
            if (array.Infos == null)
            {
                goto Label_00BC;
            }
            storeyd = new <EraseTimeOutPopupHistory>c__AnonStorey27D();
            infoArray = array.Infos;
            num = 0;
            goto Label_00B1;
        Label_007A:
            storeyd.info = infoArray[num];
            if (Enumerable.Any<JSON_ShopListArray.Shops>(shops, new Func<JSON_ShopListArray.Shops, bool>(storeyd.<>m__1CB)) == null)
            {
                goto Label_00AB;
            }
            list.Add(storeyd.info);
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
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, str3, 0);
        Label_00E0:
            return;
        }

        public long GetLimitedShopPeriodEndAt(JSON_ShopListArray list)
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
                goto Label_0054;
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
                goto Label_004E;
            }
            base.ExecRequest(new ReqLimitedShopList(new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0054;
        Label_004E:
            this.Success();
        Label_0054:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ShopListArray> response;
            int num;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ShopListArray>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            if (response.body.shops == null)
            {
                goto Label_005D;
            }
            if (((int) response.body.shops.Length) > 0)
            {
                goto Label_0079;
            }
        Label_005D:
            MonoSingleton<GameManager>.Instance.LimitedShopEndAt = 0L;
            MonoSingleton<GameManager>.Instance.LimitedShopList = null;
            goto Label_00FD;
        Label_0079:
            num = 0;
            goto Label_009D;
        Label_0080:
            if (response.body.shops[num] != null)
            {
                goto Label_0099;
            }
            this.OnRetry();
            return;
        Label_0099:
            num += 1;
        Label_009D:
            if (num < ((int) response.body.shops.Length))
            {
                goto Label_0080;
            }
            MonoSingleton<GameManager>.Instance.LimitedShopEndAt = this.GetLimitedShopPeriodEndAt(response.body);
            if (this.inputPin == 2)
            {
                goto Label_00E8;
            }
            this.limited_shop_list.SetLimitedShopList(response.body.shops);
        Label_00E8:
            MonoSingleton<GameManager>.Instance.LimitedShopList = response.body.shops;
        Label_00FD:
            this.EraseTimeOutPopupHistory(response.body.shops);
            this.Success();
            return;
        }

        public void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }

        [CompilerGenerated]
        private sealed class <EraseTimeOutPopupHistory>c__AnonStorey27D
        {
            internal ShopTimeOutItemInfo info;

            public <EraseTimeOutPopupHistory>c__AnonStorey27D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1CB(JSON_ShopListArray.Shops sh)
            {
                return (sh.gname == this.info.ShopId);
            }
        }
    }
}

