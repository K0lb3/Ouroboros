namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class EventShopData
    {
        public List<EventShopItem> items;
        public int UpdateCount;
        private ShopData mShopData;

        public EventShopData()
        {
            this.items = new List<EventShopItem>();
            this.mShopData = new ShopData();
            base..ctor();
            return;
        }

        public bool Deserialize(Json_EventShopBuyResponse response)
        {
            Exception exception;
            Exception exception2;
            JSON_EventShopItemListSet[] setArray;
            int num;
            EventShopItem item;
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
                goto Label_0157;
            }
        Label_003B:
            if (response.items != null)
            {
                goto Label_0048;
            }
            return 0;
        Label_0048:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
                goto Label_0076;
            }
            catch (Exception exception5)
            {
            Label_0062:
                exception2 = exception5;
                DebugUtility.LogException(exception2);
                flag = 0;
                goto Label_0157;
            }
        Label_0076:
            if (response.shopitems != null)
            {
                goto Label_0083;
            }
            return 0;
        Label_0083:
            setArray = response.shopitems;
            num = 0;
            goto Label_00CF;
        Label_0091:
            item = this.items[num];
            if (item != null)
            {
                goto Label_00BA;
            }
            item = new EventShopItem();
            this.items.Add(item);
        Label_00BA:
            if (item.Deserialize(setArray[num]) != null)
            {
                goto Label_00CB;
            }
            return 0;
        Label_00CB:
            num += 1;
        Label_00CF:
            if (num < ((int) setArray.Length))
            {
                goto Label_0091;
            }
            if (response.mail_info != null)
            {
                goto Label_00E5;
            }
            return 0;
        Label_00E5:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.mail_info);
                goto Label_0115;
            }
            catch (Exception exception6)
            {
            Label_00FF:
                exception3 = exception6;
                DebugUtility.LogException(exception3);
                flag = 0;
                goto Label_0157;
            }
        Label_0115:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.units);
                goto Label_0145;
            }
            catch (Exception exception7)
            {
            Label_012F:
                exception4 = exception7;
                DebugUtility.LogException(exception4);
                flag = 0;
                goto Label_0157;
            }
        Label_0145:
            GlobalVars.ConceptCardNum.Set(response.concept_count);
            return 1;
        Label_0157:
            return flag;
        }

        public bool Deserialize(Json_EventShopResponse response)
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

        public bool Deserialize(Json_EventShopUpdateResponse response)
        {
            Exception exception;
            Exception exception2;
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
                goto Label_00B2;
            }
        Label_003A:
            if (response.items != null)
            {
                goto Label_0047;
            }
            return 0;
        Label_0047:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.items);
                goto Label_0074;
            }
            catch (Exception exception3)
            {
            Label_0061:
                exception2 = exception3;
                DebugUtility.LogException(exception2);
                flag = 0;
                goto Label_00B2;
            }
        Label_0074:
            if (response.shopitems != null)
            {
                goto Label_0081;
            }
            return 0;
        Label_0081:
            if (this.Deserialize(response.shopitems) != null)
            {
                goto Label_0094;
            }
            return 0;
        Label_0094:
            this.UpdateCount = response.relcnt;
            GlobalVars.ConceptCardNum.Set(response.concept_count);
            return 1;
        Label_00B2:
            return flag;
        }

        public bool Deserialize(JSON_EventShopItemListSet[] shopitems)
        {
            int num;
            EventShopItem item;
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
            item = new EventShopItem();
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
            int num;
            this.mShopData.items = new List<ShopItem>();
            num = 0;
            goto Label_0037;
        Label_0017:
            this.mShopData.items.Add(this.items[num]);
            num += 1;
        Label_0037:
            if (num < this.items.Count)
            {
                goto Label_0017;
            }
            this.mShopData.UpdateCount = this.UpdateCount;
            return this.mShopData;
        }

        public void SetShopData(ShopData shopData)
        {
            this.mShopData = shopData;
            return;
        }
    }
}

