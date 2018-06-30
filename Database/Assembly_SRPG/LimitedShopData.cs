namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class LimitedShopData
    {
        public List<LimitedShopItem> items;
        public int UpdateCount;

        public LimitedShopData()
        {
            this.items = new List<LimitedShopItem>();
            base..ctor();
            return;
        }

        public bool Deserialize(Json_LimitedShopBuyResponse response)
        {
            Exception exception;
            Exception exception2;
            JSON_LimitedShopItemListSet[] setArray;
            int num;
            LimitedShopItem item;
            Exception exception3;
            Exception exception4;
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
                goto Label_0155;
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
            catch (Exception exception5)
            {
            Label_0060:
                exception2 = exception5;
                DebugUtility.LogException(exception2);
                flag = 0;
                goto Label_0155;
            }
        Label_0074:
            if (response.shopitems != null)
            {
                goto Label_0081;
            }
            return 0;
        Label_0081:
            setArray = response.shopitems;
            num = 0;
            goto Label_00CD;
        Label_008F:
            item = this.items[num];
            if (item != null)
            {
                goto Label_00B8;
            }
            item = new LimitedShopItem();
            this.items.Add(item);
        Label_00B8:
            if (item.Deserialize(setArray[num]) != null)
            {
                goto Label_00C9;
            }
            return 0;
        Label_00C9:
            num += 1;
        Label_00CD:
            if (num < ((int) setArray.Length))
            {
                goto Label_008F;
            }
            if (response.mail_info != null)
            {
                goto Label_00E3;
            }
            return 0;
        Label_00E3:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.mail_info);
                goto Label_0113;
            }
            catch (Exception exception6)
            {
            Label_00FD:
                exception3 = exception6;
                DebugUtility.LogException(exception3);
                flag = 0;
                goto Label_0155;
            }
        Label_0113:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.units);
                goto Label_0143;
            }
            catch (Exception exception7)
            {
            Label_012D:
                exception4 = exception7;
                DebugUtility.LogException(exception4);
                flag = 0;
                goto Label_0155;
            }
        Label_0143:
            GlobalVars.ConceptCardNum.Set(response.concept_count);
            return 1;
        Label_0155:
            return flag;
        }

        public bool Deserialize(Json_LimitedShopResponse response)
        {
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
            GlobalVars.ConceptCardNum.Set(response.concept_count);
            return 1;
        }

        public bool Deserialize(Json_LimitedShopUpdateResponse response)
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

        public bool Deserialize(JSON_LimitedShopItemListSet[] shopitems)
        {
            int num;
            LimitedShopItem item;
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
            item = new LimitedShopItem();
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

        public ShopData GetShopData()
        {
            ShopData data;
            int num;
            data = new ShopData();
            data.items = new List<ShopItem>();
            num = 0;
            goto Label_0034;
        Label_0018:
            data.items[num] = this.items[num];
            num += 1;
        Label_0034:
            if (num < data.items.Count)
            {
                goto Label_0018;
            }
            data.UpdateCount = this.UpdateCount;
            return data;
        }

        public void SetShopData(ShopData shopData)
        {
            int num;
            shopData.items = new List<ShopItem>();
            num = 0;
            goto Label_0033;
        Label_0012:
            this.items[num].SetShopItem(shopData.items[num]);
            num += 1;
        Label_0033:
            if (num < shopData.items.Count)
            {
                goto Label_0012;
            }
            shopData.UpdateCount = this.UpdateCount;
            return;
        }
    }
}

