namespace SRPG
{
    using GR;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0x6c, "課金コイン不足", 1, 0x12), Pin(0x6b, "ゴールド不足", 1, 0x11), Pin(0x69, "購入済み", 1, 15), Pin(1, "Request", 0, 0), NodeType("System/BuyItem", 0x7fe5), Pin(0x7a, "ショップラインナップが更新された", 1, 0x1b), Pin(0x6a, "アイテム所持上限", 1, 0x10), Pin(0x79, "ショップ期間外", 1, 0x1a), Pin(110, "アリーナコイン不足", 1, 20), Pin(0x6d, "遠征コイン不足", 1, 0x13), Pin(120, "購入期間外", 1, 0x19), Pin(0x72, "有償石不足", 1, 0x18), Pin(0x71, "イベントコイン不足", 1, 0x17), Pin(0x70, "マルチコイン不足", 1, 0x16), Pin(0x6f, "カケラポイント不足", 1, 0x15), Pin(100, "Success", 1, 10), Pin(0x68, "ショップ情報がない", 1, 14)]
    public class FlowNode_BuyItem : FlowNode_Network
    {
        private const int PIN_OT_SHOP_BUY_OUTOF_ITEM_PERIOD = 120;
        private const int PIN_OT_SHOP_REFRESH_ITEM_LIST = 0x79;
        private const int PIN_OT_SHOP_BUY_OUTOF_PERIOD = 0x7a;
        private EShopType mShopType;
        [CompilerGenerated]
        private static Func<ShopItem, bool> <>f__am$cache1;
        [CompilerGenerated]
        private static Func<ShopItem, bool> <>f__am$cache2;

        public FlowNode_BuyItem()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <OnActivate>m__187(ShopItem item)
        {
            return (item.id == GlobalVars.ShopBuyIndex);
        }

        [CompilerGenerated]
        private void <OnSuccess>m__188(GameObject go)
        {
            base.ActivateOutputLinks(120);
            return;
        }

        [CompilerGenerated]
        private void <OnSuccess>m__189(GameObject go)
        {
            base.ActivateOutputLinks(0x79);
            return;
        }

        [CompilerGenerated]
        private void <OnSuccess>m__18A(GameObject go)
        {
            base.ActivateOutputLinks(0x7a);
            return;
        }

        [CompilerGenerated]
        private static bool <OnSuccess>m__18B(ShopItem item)
        {
            return (item.id == GlobalVars.ShopBuyIndex);
        }

        public bool CheckCanBuy(ShopItem shopitem, int buy, int check, int pin)
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
            ShopData data2;
            ShopItem item;
            int num;
            ItemParam param;
            ArtifactParam param2;
            ConceptCardParam param3;
            int num2;
            ShopParam param4;
            string str;
            ESaleType type;
            if (pinID != 1)
            {
                goto Label_033C;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            base.set_enabled(0);
            data2 = data.GetShopData(GlobalVars.ShopType);
            if (data2 != null)
            {
                goto Label_0035;
            }
            base.ActivateOutputLinks(0x68);
            return;
        Label_0035:
            if (<>f__am$cache1 != null)
            {
                goto Label_0053;
            }
            <>f__am$cache1 = new Func<ShopItem, bool>(FlowNode_BuyItem.<OnActivate>m__187);
        Label_0053:
            item = Enumerable.FirstOrDefault<ShopItem>(data2.items, <>f__am$cache1);
            if (item.is_soldout == null)
            {
                goto Label_0073;
            }
            base.ActivateOutputLinks(0x69);
            return;
        Label_0073:
            num = 0;
            param = null;
            if (item.IsArtifact == null)
            {
                goto Label_00AD;
            }
            num = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname).GetBuyNum(item.saleType);
            goto Label_0160;
        Label_00AD:
            if (item.IsConceptCard == null)
            {
                goto Label_0112;
            }
            if (MonoSingleton<GameManager>.Instance.Player.CheckConceptCardCapacity(item.num * GlobalVars.ShopBuyAmount) != null)
            {
                goto Label_0160;
            }
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(item.iname);
            if (param3 == null)
            {
                goto Label_0160;
            }
            if (param3.type != 1)
            {
                goto Label_0160;
            }
            base.ActivateOutputLinks(0x6a);
            return;
            goto Label_0160;
        Label_0112:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(item.iname);
            if (item.IsSet != null)
            {
                goto Label_0152;
            }
            if (data.CheckItemCapacity(param, item.num * GlobalVars.ShopBuyAmount) != null)
            {
                goto Label_0152;
            }
            base.ActivateOutputLinks(0x6a);
            return;
        Label_0152:
            num = param.GetBuyNum(item.saleType);
        Label_0160:
            switch (item.saleType)
            {
                case 0:
                    goto Label_0194;

                case 1:
                    goto Label_01AF;

                case 2:
                    goto Label_01E5;

                case 3:
                    goto Label_0200;

                case 4:
                    goto Label_021B;

                case 5:
                    goto Label_0236;

                case 6:
                    goto Label_0251;

                case 7:
                    goto Label_01CA;
            }
            goto Label_0265;
        Label_0194:
            if (this.CheckCanBuy(item, num, data.Gold, 0x6b) != null)
            {
                goto Label_0265;
            }
            return;
            goto Label_0265;
        Label_01AF:
            if (this.CheckCanBuy(item, num, data.Coin, 0x6c) != null)
            {
                goto Label_0265;
            }
            return;
            goto Label_0265;
        Label_01CA:
            if (this.CheckCanBuy(item, num, data.PaidCoin, 0x72) != null)
            {
                goto Label_0265;
            }
            return;
            goto Label_0265;
        Label_01E5:
            if (this.CheckCanBuy(item, num, data.TourCoin, 0x6d) != null)
            {
                goto Label_0265;
            }
            return;
            goto Label_0265;
        Label_0200:
            if (this.CheckCanBuy(item, num, data.ArenaCoin, 110) != null)
            {
                goto Label_0265;
            }
            return;
            goto Label_0265;
        Label_021B:
            if (this.CheckCanBuy(item, num, data.PiecePoint, 0x6f) != null)
            {
                goto Label_0265;
            }
            return;
            goto Label_0265;
        Label_0236:
            if (this.CheckCanBuy(item, num, data.MultiCoin, 0x70) != null)
            {
                goto Label_0265;
            }
            return;
            goto Label_0265;
        Label_0251:
            DebugUtility.Assert("There is no common price in the event coin.");
            base.ActivateOutputLinks(0x71);
            return;
        Label_0265:
            this.mShopType = GlobalVars.ShopType;
            num2 = GlobalVars.ShopBuyIndex;
            if (Network.Mode != 1)
            {
                goto Label_02D3;
            }
            if (param == null)
            {
                goto Label_033C;
            }
            data.DEBUG_BUY_ITEM(this.mShopType, num2);
            param4 = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
            data.OnBuyAtShop(param4.iname, param.iname, item.num);
            this.Success();
            goto Label_033C;
        Label_02D3:
            if (this.mShopType != 11)
            {
                goto Label_0303;
            }
            base.ExecRequest(new ReqItemGuerrillaShopBuypaid(num2, GlobalVars.ShopBuyAmount, new Network.ResponseCallback(this.ResponseCallback)));
            goto Label_0335;
        Label_0303:
            str = ((EShopType) this.mShopType).ToString();
            base.ExecRequest(new ReqItemShopBuypaid(str, num2, GlobalVars.ShopBuyAmount, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0335:
            base.set_enabled(1);
        Label_033C:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ShopBuyResponse> response;
            ShopData data;
            Json_ShopBuyConceptCard[] cardArray;
            int num;
            ConceptCardData data2;
            ShopParam param;
            PlayerData data3;
            ShopItem item;
            string str;
            int num2;
            ArtifactParam param2;
            ItemParam param3;
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
            UIUtility.SystemMessage(null, Network.ErrMsg, new UIUtility.DialogResultEvent(this.<OnSuccess>m__188), null, 0, -1);
            base.set_enabled(0);
            Network.RemoveAPI();
            Network.ResetError();
            return;
        Label_007F:
            UIUtility.SystemMessage(null, Network.ErrMsg, new UIUtility.DialogResultEvent(this.<OnSuccess>m__189), null, 0, -1);
            base.set_enabled(0);
            Network.RemoveAPI();
            Network.ResetError();
            return;
        Label_00AC:
            UIUtility.SystemMessage(null, Network.ErrMsg, new UIUtility.DialogResultEvent(this.<OnSuccess>m__18A), null, 0, -1);
            base.set_enabled(0);
            Network.RemoveAPI();
            Network.ResetError();
            return;
        Label_00D9:
            this.OnRetry();
            return;
        Label_00E0:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopBuyResponse>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0110;
            }
            this.OnRetry();
            return;
        Label_0110:
            Network.RemoveAPI();
            data = MonoSingleton<GameManager>.Instance.Player.GetShopData(this.mShopType);
            if (data != null)
            {
                goto Label_0137;
            }
            data = new ShopData();
        Label_0137:
            if (data.Deserialize(response.body) != null)
            {
                goto Label_014F;
            }
            this.OnFailed();
            return;
        Label_014F:
            MonoSingleton<GameManager>.Instance.Player.SetShopData(this.mShopType, data);
            if (response.body.cards == null)
            {
                goto Label_01E3;
            }
            if (((int) response.body.cards.Length) <= 0)
            {
                goto Label_01E3;
            }
            GlobalVars.IsDirtyConceptCardData.Set(1);
            cardArray = response.body.cards;
            num = 0;
            goto Label_01DA;
        Label_01A6:
            if (cardArray[num] != null)
            {
                goto Label_01B3;
            }
            goto Label_01D6;
        Label_01B3:
            if (cardArray[num].IsGetConceptCardUnit == null)
            {
                goto Label_01D6;
            }
            FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(cardArray[num].iname));
        Label_01D6:
            num += 1;
        Label_01DA:
            if (num < ((int) cardArray.Length))
            {
                goto Label_01A6;
            }
        Label_01E3:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(this.mShopType);
            if (param == null)
            {
                goto Label_0328;
            }
            data3 = MonoSingleton<GameManager>.Instance.Player;
            if (<>f__am$cache2 != null)
            {
                goto Label_022B;
            }
            <>f__am$cache2 = new Func<ShopItem, bool>(FlowNode_BuyItem.<OnSuccess>m__18B);
        Label_022B:
            item = Enumerable.FirstOrDefault<ShopItem>(data.items, <>f__am$cache2);
            str = item.iname;
            if (item.isSetSaleValue == null)
            {
                goto Label_026B;
            }
            MyMetaps.TrackSpendShop(item.saleType, this.mShopType, item.saleValue);
            goto Label_0311;
        Label_026B:
            num2 = 0;
            if (item.IsArtifact == null)
            {
                goto Label_02AF;
            }
            num2 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname).GetBuyNum(item.saleType) * item.num;
            goto Label_02F4;
        Label_02AF:
            if (item.IsConceptCard == null)
            {
                goto Label_02C9;
            }
            num2 = item.saleValue;
            goto Label_02F4;
        Label_02C9:
            num2 = MonoSingleton<GameManager>.Instance.GetItemParam(item.iname).GetBuyNum(item.saleType) * item.num;
        Label_02F4:
            if (num2 <= 0)
            {
                goto Label_0311;
            }
            MyMetaps.TrackSpendShop(item.saleType, this.mShopType, num2);
        Label_0311:
            data3.OnBuyAtShop(param.iname, str, item.num);
        Label_0328:
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

