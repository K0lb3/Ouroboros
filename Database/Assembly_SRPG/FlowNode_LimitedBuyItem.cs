namespace SRPG
{
    using GR;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0x7a, "ショップラインナップが更新された", 1, 0x1b), Pin(0x69, "購入済み", 1, 15), Pin(0x6c, "課金コイン不足", 1, 0x12), Pin(0x6b, "ゴールド不足", 1, 0x11), Pin(0x6d, "遠征コイン不足", 1, 0x13), Pin(0x6a, "アイテム所持上限", 1, 0x10), Pin(0x68, "ショップ情報がない", 1, 14), Pin(100, "Success", 1, 10), Pin(1, "Request", 0, 0), NodeType("System/LimitedBuyItem", 0x7fe5), Pin(110, "アリーナコイン不足", 1, 20), Pin(0x6f, "カケラポイント不足", 1, 0x15), Pin(0x70, "マルチコイン不足", 1, 0x16), Pin(0x71, "有償石不足", 1, 0x17), Pin(0x72, "イベントコイン不足", 1, 0x18), Pin(120, "購入期間外", 1, 0x19), Pin(0x79, "ショップ期間外", 1, 0x1a)]
    public class FlowNode_LimitedBuyItem : FlowNode_Network
    {
        private const int PIN_OT_SHOP_BUY_OUTOF_ITEM_PERIOD = 120;
        private const int PIN_OT_SHOP_REFRESH_ITEM_LIST = 0x79;
        private const int PIN_OT_SHOP_BUY_OUTOF_PERIOD = 0x7a;
        private EShopType mShopType;
        [CompilerGenerated]
        private static Func<LimitedShopItem, bool> <>f__am$cache1;
        [CompilerGenerated]
        private static Func<LimitedShopItem, bool> <>f__am$cache2;

        public FlowNode_LimitedBuyItem()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <OnActivate>m__19E(LimitedShopItem item)
        {
            return (item.id == GlobalVars.ShopBuyIndex);
        }

        [CompilerGenerated]
        private void <OnSuccess>m__19F(GameObject go)
        {
            base.ActivateOutputLinks(120);
            return;
        }

        [CompilerGenerated]
        private void <OnSuccess>m__1A0(GameObject go)
        {
            base.ActivateOutputLinks(0x79);
            return;
        }

        [CompilerGenerated]
        private void <OnSuccess>m__1A1(GameObject go)
        {
            base.ActivateOutputLinks(0x7a);
            return;
        }

        [CompilerGenerated]
        private static bool <OnSuccess>m__1A2(LimitedShopItem item)
        {
            return (item.id == GlobalVars.ShopBuyIndex);
        }

        public bool CheckCanBuy(LimitedShopItem shopitem, int buy, int check, int pin)
        {
            int num;
            num = (shopitem.isSetSaleValue == null) ? (buy * shopitem.num) : shopitem.saleValue;
            if (check >= num)
            {
                goto Label_0031;
            }
            base.ActivateOutputLinks(pin);
            return 0;
        Label_0031:
            return 1;
        }

        public override void OnActivate(int pinID)
        {
            PlayerData data;
            LimitedShopData data2;
            LimitedShopItem item;
            int num;
            ArtifactParam param;
            ConceptCardParam param2;
            ItemParam param3;
            string str;
            int num2;
            ESaleType type;
            if (pinID != 1)
            {
                goto Label_02C3;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            base.set_enabled(0);
            data2 = data.GetLimitedShopData();
            if (data2 != null)
            {
                goto Label_0030;
            }
            base.ActivateOutputLinks(0x68);
            return;
        Label_0030:
            if (<>f__am$cache1 != null)
            {
                goto Label_004E;
            }
            <>f__am$cache1 = new Func<LimitedShopItem, bool>(FlowNode_LimitedBuyItem.<OnActivate>m__19E);
        Label_004E:
            item = Enumerable.FirstOrDefault<LimitedShopItem>(data2.items, <>f__am$cache1);
            if (item.is_soldout == null)
            {
                goto Label_006E;
            }
            base.ActivateOutputLinks(0x69);
            return;
        Label_006E:
            num = 0;
            if (item.IsArtifact == null)
            {
                goto Label_00A5;
            }
            num = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname).GetBuyNum(item.saleType);
            goto Label_0158;
        Label_00A5:
            if (item.IsConceptCard == null)
            {
                goto Label_010A;
            }
            if (MonoSingleton<GameManager>.Instance.Player.CheckConceptCardCapacity(item.num * GlobalVars.ShopBuyAmount) != null)
            {
                goto Label_0158;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(item.iname);
            if (param2 == null)
            {
                goto Label_0158;
            }
            if (param2.type != 1)
            {
                goto Label_0158;
            }
            base.ActivateOutputLinks(0x6a);
            return;
            goto Label_0158;
        Label_010A:
            param3 = MonoSingleton<GameManager>.Instance.GetItemParam(item.iname);
            if (item.IsSet != null)
            {
                goto Label_014A;
            }
            if (data.CheckItemCapacity(param3, item.num * GlobalVars.ShopBuyAmount) != null)
            {
                goto Label_014A;
            }
            base.ActivateOutputLinks(0x6a);
            return;
        Label_014A:
            num = param3.GetBuyNum(item.saleType);
        Label_0158:
            switch (item.saleType)
            {
                case 0:
                    goto Label_018C;

                case 1:
                    goto Label_01A7;

                case 2:
                    goto Label_01DD;

                case 3:
                    goto Label_01F8;

                case 4:
                    goto Label_0213;

                case 5:
                    goto Label_022E;

                case 6:
                    goto Label_0249;

                case 7:
                    goto Label_01C2;
            }
            goto Label_0253;
        Label_018C:
            if (this.CheckCanBuy(item, num, data.Gold, 0x6b) != null)
            {
                goto Label_0253;
            }
            return;
            goto Label_0253;
        Label_01A7:
            if (this.CheckCanBuy(item, num, data.Coin, 0x6c) != null)
            {
                goto Label_0253;
            }
            return;
            goto Label_0253;
        Label_01C2:
            if (this.CheckCanBuy(item, num, data.PaidCoin, 0x71) != null)
            {
                goto Label_0253;
            }
            return;
            goto Label_0253;
        Label_01DD:
            if (this.CheckCanBuy(item, num, data.TourCoin, 0x6d) != null)
            {
                goto Label_0253;
            }
            return;
            goto Label_0253;
        Label_01F8:
            if (this.CheckCanBuy(item, num, data.ArenaCoin, 110) != null)
            {
                goto Label_0253;
            }
            return;
            goto Label_0253;
        Label_0213:
            if (this.CheckCanBuy(item, num, data.PiecePoint, 0x6f) != null)
            {
                goto Label_0253;
            }
            return;
            goto Label_0253;
        Label_022E:
            if (this.CheckCanBuy(item, num, data.MultiCoin, 0x70) != null)
            {
                goto Label_0253;
            }
            return;
            goto Label_0253;
        Label_0249:
            base.ActivateOutputLinks(0x71);
            return;
        Label_0253:
            if (Network.Mode != 1)
            {
                goto Label_0279;
            }
            data.DEBUG_BUY_ITEM(GlobalVars.ShopType, GlobalVars.ShopBuyIndex);
            this.Success();
            goto Label_02C3;
        Label_0279:
            this.mShopType = GlobalVars.ShopType;
            str = GlobalVars.LimitedShopItem.shops.gname;
            num2 = GlobalVars.ShopBuyIndex;
            base.ExecRequest(new ReqItemLimitedShopBuypaid(str, num2, GlobalVars.ShopBuyAmount, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_02C3:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_LimitedShopBuyResponse> response;
            LimitedShopData data;
            LimitedShopItem item;
            Json_ShopBuyConceptCard[] cardArray;
            int num;
            ConceptCardData data2;
            int num2;
            ArtifactParam param;
            ItemParam param2;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_00E0;
            }
            code = Network.ErrCode;
            switch ((code - 0x10cc))
            {
                case 0:
                    goto Label_004B;

                case 1:
                    goto Label_004B;

                case 2:
                    goto Label_004B;

                case 3:
                    goto Label_004B;

                case 4:
                    goto Label_004B;

                case 5:
                    goto Label_007F;

                case 6:
                    goto Label_0052;
            }
            if (code == 0x1133)
            {
                goto Label_00AC;
            }
            goto Label_00D9;
        Label_004B:
            this.OnBack();
            return;
        Label_0052:
            UIUtility.SystemMessage(null, Network.ErrMsg, new UIUtility.DialogResultEvent(this.<OnSuccess>m__19F), null, 0, -1);
            base.set_enabled(0);
            Network.RemoveAPI();
            Network.ResetError();
            return;
        Label_007F:
            UIUtility.SystemMessage(null, Network.ErrMsg, new UIUtility.DialogResultEvent(this.<OnSuccess>m__1A0), null, 0, -1);
            base.set_enabled(0);
            Network.RemoveAPI();
            Network.ResetError();
            return;
        Label_00AC:
            UIUtility.SystemMessage(null, Network.ErrMsg, new UIUtility.DialogResultEvent(this.<OnSuccess>m__1A1), null, 0, -1);
            base.set_enabled(0);
            Network.RemoveAPI();
            Network.ResetError();
            return;
        Label_00D9:
            this.OnRetry();
            return;
        Label_00E0:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_LimitedShopBuyResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0110;
            }
            this.OnRetry();
            return;
        Label_0110:
            Network.RemoveAPI();
            data = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
            if (data != null)
            {
                goto Label_0131;
            }
            data = new LimitedShopData();
        Label_0131:
            if (data.Deserialize(response.body) != null)
            {
                goto Label_0149;
            }
            this.OnFailed();
            return;
        Label_0149:
            MonoSingleton<GameManager>.Instance.Player.SetLimitedShopData(data);
            if (<>f__am$cache2 != null)
            {
                goto Label_0177;
            }
            <>f__am$cache2 = new Func<LimitedShopItem, bool>(FlowNode_LimitedBuyItem.<OnSuccess>m__1A2);
        Label_0177:
            item = Enumerable.FirstOrDefault<LimitedShopItem>(data.items, <>f__am$cache2);
            if (response.body.cards == null)
            {
                goto Label_0207;
            }
            if (((int) response.body.cards.Length) <= 0)
            {
                goto Label_0207;
            }
            GlobalVars.IsDirtyConceptCardData.Set(1);
            cardArray = response.body.cards;
            num = 0;
            goto Label_01FD;
        Label_01C4:
            if (cardArray[num] != null)
            {
                goto Label_01D2;
            }
            goto Label_01F7;
        Label_01D2:
            if (cardArray[num].IsGetConceptCardUnit == null)
            {
                goto Label_01F7;
            }
            FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(cardArray[num].iname));
        Label_01F7:
            num += 1;
        Label_01FD:
            if (num < ((int) cardArray.Length))
            {
                goto Label_01C4;
            }
        Label_0207:
            if (item.isSetSaleValue == null)
            {
                goto Label_022F;
            }
            MyMetaps.TrackSpendShop(item.saleType, this.mShopType, item.saleValue);
            goto Label_02CB;
        Label_022F:
            num2 = 0;
            if (item.IsArtifact == null)
            {
                goto Label_026F;
            }
            num2 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname).GetBuyNum(item.saleType) * item.num;
            goto Label_02AF;
        Label_026F:
            if (item.IsConceptCard == null)
            {
                goto Label_0287;
            }
            num2 = item.saleValue;
            goto Label_02AF;
        Label_0287:
            num2 = MonoSingleton<GameManager>.Instance.GetItemParam(item.iname).GetBuyNum(item.saleType) * item.num;
        Label_02AF:
            if (num2 <= 0)
            {
                goto Label_02CB;
            }
            MyMetaps.TrackSpendShop(item.saleType, this.mShopType, num2);
        Label_02CB:
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

