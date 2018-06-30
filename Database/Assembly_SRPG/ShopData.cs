namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ShopData
    {
        public List<ShopItem> items;
        public int UpdateCount;
        public bool btn_update;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$mapD;

        public ShopData()
        {
            this.items = new List<ShopItem>();
            base..ctor();
            return;
        }

        public bool Deserialize(Json_ShopBuyResponse response)
        {
            Exception exception;
            Exception exception2;
            Json_ShopItem[] itemArray;
            int num;
            ShopItem item;
            Exception exception3;
            bool flag;
            if (response.currencies != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.currencies);
                goto Label_003B;
            }
            catch (Exception exception1)
            {
            Label_0027:
                exception = exception1;
                DebugUtility.LogException(exception);
                flag = 0;
                goto Label_0118;
            }
        Label_003B:
            if (response.items == null)
            {
                goto Label_0074;
            }
        Label_0046:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
                goto Label_0074;
            }
            catch (Exception exception4)
            {
            Label_0060:
                exception2 = exception4;
                DebugUtility.LogException(exception2);
                flag = 0;
                goto Label_0118;
            }
        Label_0074:
            if (response.shopitems != null)
            {
                goto Label_0081;
            }
            return 0;
        Label_0081:
            itemArray = response.shopitems;
            num = 0;
            goto Label_00CD;
        Label_008F:
            item = this.items[num];
            if (item != null)
            {
                goto Label_00B8;
            }
            item = new ShopItem();
            this.items.Add(item);
        Label_00B8:
            if (item.Deserialize(itemArray[num]) != null)
            {
                goto Label_00C9;
            }
            return 0;
        Label_00C9:
            num += 1;
        Label_00CD:
            if (num < ((int) itemArray.Length))
            {
                goto Label_008F;
            }
        Label_00D6:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.units);
                goto Label_0106;
            }
            catch (Exception exception5)
            {
            Label_00F0:
                exception3 = exception5;
                DebugUtility.LogException(exception3);
                flag = 0;
                goto Label_0118;
            }
        Label_0106:
            GlobalVars.ConceptCardNum.Set(response.concept_count);
            return 1;
        Label_0118:
            return flag;
        }

        public bool Deserialize(Json_ShopResponse response)
        {
            Json_ShopMsgResponse response2;
            if (response.shopitems != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (this.Deserialize(response.shopitems) != null)
            {
                goto Label_0020;
            }
            return 0;
        Label_0020:
            this.UpdateCount = response.relcnt;
            this.btn_update = 1;
            if ((string.IsNullOrEmpty(response.msg) != null) || (response.msg.StartsWith("{") == null))
            {
                goto Label_0086;
            }
            response2 = JSONParser.parseJSONObject<Json_ShopMsgResponse>(response.msg);
            this.btn_update = (response2.update.Equals("on") == null) ? 0 : 1;
        Label_0086:
            GlobalVars.ConceptCardNum.Set(response.concept_count);
            return 1;
        }

        public bool Deserialize(Json_ShopUpdateResponse response)
        {
            Exception exception;
            bool flag;
            if (response.currencies != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.currencies);
                goto Label_003A;
            }
            catch (Exception exception1)
            {
            Label_0027:
                exception = exception1;
                DebugUtility.LogException(exception);
                flag = 0;
                goto Label_0078;
            }
        Label_003A:
            if (response.shopitems != null)
            {
                goto Label_0047;
            }
            return 0;
        Label_0047:
            if (this.Deserialize(response.shopitems) != null)
            {
                goto Label_005A;
            }
            return 0;
        Label_005A:
            this.UpdateCount = response.relcnt;
            GlobalVars.ConceptCardNum.Set(response.concept_count);
            return 1;
        Label_0078:
            return flag;
        }

        public bool Deserialize(Json_ShopItem[] shopitems)
        {
            int num;
            ShopItem item;
            if (shopitems != null)
            {
                goto Label_0008;
            }
            return 1;
        Label_0008:
            this.items.Clear();
            num = 0;
            goto Label_0040;
        Label_001A:
            item = new ShopItem();
            if (item.Deserialize(shopitems[num]) != null)
            {
                goto Label_0030;
            }
            return 0;
        Label_0030:
            this.items.Add(item);
            num += 1;
        Label_0040:
            if (num < ((int) shopitems.Length))
            {
                goto Label_001A;
            }
            return 1;
        }

        public static int GetBuyPrice(ShopItem shopItem)
        {
            int num;
            ItemParam param;
            ESaleType type;
            if (shopItem.isSetSaleValue == null)
            {
                goto Label_0012;
            }
            return shopItem.saleValue;
        Label_0012:
            num = 0;
            if (shopItem == null)
            {
                goto Label_00E4;
            }
            param = MonoSingleton<GameManager>.Instance.GetItemParam(shopItem.iname);
            if (param == null)
            {
                goto Label_00E4;
            }
            switch (shopItem.saleType)
            {
                case 0:
                    goto Label_0063;

                case 1:
                    goto Label_0076;

                case 2:
                    goto Label_0089;

                case 3:
                    goto Label_009C;

                case 4:
                    goto Label_00AF;

                case 5:
                    goto Label_00C2;

                case 6:
                    goto Label_00D5;

                case 7:
                    goto Label_0076;
            }
            goto Label_00E4;
        Label_0063:
            num = shopItem.num * param.buy;
            goto Label_00E4;
        Label_0076:
            num = shopItem.num * param.coin;
            goto Label_00E4;
        Label_0089:
            num = shopItem.num * param.tour_coin;
            goto Label_00E4;
        Label_009C:
            num = shopItem.num * param.arena_coin;
            goto Label_00E4;
        Label_00AF:
            num = shopItem.num * param.piece_point;
            goto Label_00E4;
        Label_00C2:
            num = shopItem.num * param.multi_coin;
            goto Label_00E4;
        Label_00D5:
            DebugUtility.Assert("There is no common price in the event coin.");
        Label_00E4:
            return num;
        }

        public static int GetRemainingCurrency(ShopItem shopitem)
        {
            PlayerData data;
            EventShopItem item;
            ESaleType type;
            data = MonoSingleton<GameManager>.Instance.Player;
            switch (shopitem.saleType)
            {
                case 0:
                    goto Label_003D;

                case 1:
                    goto Label_0044;

                case 2:
                    goto Label_0052;

                case 3:
                    goto Label_0059;

                case 4:
                    goto Label_0060;

                case 5:
                    goto Label_0067;

                case 6:
                    goto Label_006E;

                case 7:
                    goto Label_004B;
            }
            goto Label_0092;
        Label_003D:
            return data.Gold;
        Label_0044:
            return data.Coin;
        Label_004B:
            return data.PaidCoin;
        Label_0052:
            return data.TourCoin;
        Label_0059:
            return data.ArenaCoin;
        Label_0060:
            return data.PiecePoint;
        Label_0067:
            return data.MultiCoin;
        Label_006E:
            if ((shopitem as EventShopItem) == null)
            {
                goto Label_0092;
            }
            item = (EventShopItem) shopitem;
            return data.EventCoinNum(item.cost_iname);
        Label_0092:
            return 0;
        }

        public static EShopItemType Iname2ShopItemType(string iname)
        {
            ItemParam param;
            ArtifactParam param2;
            ConceptCardParam param3;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_001A;
            }
            DebugUtility.LogError("inameが空文字です");
            goto Label_007B;
        Label_001A:
            param = null;
            param2 = null;
            param3 = null;
            if (MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(iname) == null)
            {
                goto Label_0039;
            }
            return 0;
        Label_0039:
            if (MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname) == null)
            {
                goto Label_0052;
            }
            return 2;
        Label_0052:
            if (MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(iname) == null)
            {
                goto Label_006B;
            }
            return 1;
        Label_006B:
            DebugUtility.LogError(string.Format("不明な識別子です (iname => {0})", iname));
        Label_007B:
            return 4;
        }

        public static string ShopItemType2String(EShopItemType itype)
        {
            if (itype == null)
            {
                goto Label_000D;
            }
            if (itype != 3)
            {
                goto Label_0013;
            }
        Label_000D:
            return "item";
        Label_0013:
            if (itype != 1)
            {
                goto Label_0020;
            }
            return "artifact";
        Label_0020:
            if (itype != 2)
            {
                goto Label_002D;
            }
            return "concept_card";
        Label_002D:
            DebugUtility.LogError(string.Format("不明な商品タイプです (itype => {0})", (EShopItemType) itype));
            return string.Empty;
        }

        public static unsafe ESaleType String2SaleType(string type)
        {
            string str;
            Dictionary<string, int> dictionary;
            int num;
            str = type;
            if (str == null)
            {
                goto Label_00CC;
            }
            if (<>f__switch$mapD != null)
            {
                goto Label_007F;
            }
            dictionary = new Dictionary<string, int>(8);
            dictionary.Add("gold", 0);
            dictionary.Add("coin", 1);
            dictionary.Add("coin_p", 2);
            dictionary.Add("tc", 3);
            dictionary.Add("ac", 4);
            dictionary.Add("ec", 5);
            dictionary.Add("pp", 6);
            dictionary.Add("mc", 7);
            <>f__switch$mapD = dictionary;
        Label_007F:
            if (<>f__switch$mapD.TryGetValue(str, &num) == null)
            {
                goto Label_00CC;
            }
            switch (num)
            {
                case 0:
                    goto Label_00BC;

                case 1:
                    goto Label_00BE;

                case 2:
                    goto Label_00C0;

                case 3:
                    goto Label_00C2;

                case 4:
                    goto Label_00C4;

                case 5:
                    goto Label_00C6;

                case 6:
                    goto Label_00C8;

                case 7:
                    goto Label_00CA;
            }
            goto Label_00CC;
        Label_00BC:
            return 0;
        Label_00BE:
            return 1;
        Label_00C0:
            return 7;
        Label_00C2:
            return 2;
        Label_00C4:
            return 3;
        Label_00C6:
            return 6;
        Label_00C8:
            return 4;
        Label_00CA:
            return 5;
        Label_00CC:
            return 1;
        }

        public static EShopItemType String2ShopItemType(string itype)
        {
            if (string.IsNullOrEmpty(itype) == null)
            {
                goto Label_001A;
            }
            DebugUtility.LogError("商品タイプが空文字です");
            goto Label_0060;
        Label_001A:
            if ((itype == "item") == null)
            {
                goto Label_002C;
            }
            return 0;
        Label_002C:
            if ((itype == "concept_card") == null)
            {
                goto Label_003E;
            }
            return 2;
        Label_003E:
            if ((itype == "artifact") == null)
            {
                goto Label_0050;
            }
            return 1;
        Label_0050:
            DebugUtility.LogError(string.Format("不明な商品タイプです (itype => {0})", itype));
        Label_0060:
            return 4;
        }
    }
}

