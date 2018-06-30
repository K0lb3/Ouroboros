namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1), Pin(0x66, "アイテム更新", 1, 0x66), Pin(0x65, "アイテム選択セット", 1, 0x65), Pin(100, "アイテム選択単品", 1, 100), Pin(0x68, "商品の期限切れ警告表示", 1, 0x68), Pin(0x67, "武具選択単品", 1, 0x67)]
    public class LimitedShopBuyWindow : MonoBehaviour, IFlowInterface
    {
        public RectTransform ItemLayoutParent;
        public GameObject ItemTemplate;
        public Button BtnUpdated;
        public Text TxtTitle;
        public Text TxtUpdateTime;
        public Text TxtUpdated;
        public GameObject Updated;
        public GameObject Update;
        public Text ShopName;
        public List<GameObject> mBuyItems;
        public GameObject Lineup;
        [CompilerGenerated]
        private static Func<ShopTimeOutItemInfo, bool> <>f__am$cacheB;

        public LimitedShopBuyWindow()
        {
            this.mBuyItems = new List<GameObject>(12);
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <ShowAndSaveTimeOutItem>m__353(ShopTimeOutItemInfo t)
        {
            return (t.ShopId == GlobalVars.LimitedShopItem.shops.gname);
        }

        public void Activated(int pinID)
        {
            this.Refresh();
            return;
        }

        private void Awake()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_002D;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_002D;
            }
            this.ItemTemplate.SetActive(0);
        Label_002D:
            if ((this.BtnUpdated != null) == null)
            {
                goto Label_005A;
            }
            this.BtnUpdated.get_onClick().AddListener(new UnityAction(this, this.OnItemUpdated));
        Label_005A:
            if ((this.TxtTitle != null) == null)
            {
                goto Label_0080;
            }
            this.TxtTitle.set_text(LocalizedText.Get("sys.SHOP_BUY_TITLE"));
        Label_0080:
            if ((this.TxtUpdateTime != null) == null)
            {
                goto Label_00A6;
            }
            this.TxtUpdateTime.set_text(LocalizedText.Get("sys.UPDATE_TIME"));
        Label_00A6:
            if ((this.TxtUpdated != null) == null)
            {
                goto Label_00CC;
            }
            this.TxtUpdated.set_text(LocalizedText.Get("sys.CMD_UPDATED"));
        Label_00CC:
            return;
        }

        private void OnDestroy()
        {
            GlobalVars.TimeOutShopItems = null;
            return;
        }

        private void OnItemUpdated()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
            return;
        }

        private void OnSelect(GameObject go)
        {
            int num;
            LimitedShopBuyList list;
            <OnSelect>c__AnonStorey356 storey;
            storey = new <OnSelect>c__AnonStorey356();
            storey.go = go;
            num = this.mBuyItems.FindIndex(new Predicate<GameObject>(storey.<>m__357));
            list = this.mBuyItems[num].GetComponent<LimitedShopBuyList>();
            GlobalVars.ShopBuyIndex = list.limitedShopItem.id;
            if (list.limitedShopItem.IsArtifact == null)
            {
                goto Label_0060;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
            return;
        Label_0060:
            if (list.limitedShopItem.IsSet != null)
            {
                goto Label_007D;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_0085;
        Label_007D:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
        Label_0085:
            return;
        }

        private unsafe void Refresh()
        {
            PlayerData data;
            LimitedShopData data2;
            int num;
            int num2;
            DateTime time;
            List<LimitedShopItem> list;
            int num3;
            LimitedShopItem item;
            DateTime time2;
            TimeSpan span;
            GameObject obj2;
            GameObject obj3;
            LimitedShopBuyList list2;
            ArtifactParam param;
            ConceptCardData data3;
            ItemData data4;
            ListItemEvents events;
            Button button;
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            data2 = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
            DebugUtility.Assert((data2 == null) == 0, "ショップ情報が存在しない");
            this.ShopName.set_text(GlobalVars.LimitedShopItem.shops.info.title);
            if ((this.Updated != null) == null)
            {
                goto Label_007A;
            }
            this.Updated.SetActive(GlobalVars.LimitedShopItem.btn_update);
        Label_007A:
            if ((this.Update != null) == null)
            {
                goto Label_00A0;
            }
            this.Update.SetActive(GlobalVars.LimitedShopItem.btn_update);
        Label_00A0:
            if ((this.Lineup != null) == null)
            {
                goto Label_00C6;
            }
            this.Lineup.SetActive(GlobalVars.LimitedShopItem.btn_update);
        Label_00C6:
            num = 0;
            goto Label_00E8;
        Label_00CD:
            this.mBuyItems[num].get_gameObject().SetActive(0);
            num += 1;
        Label_00E8:
            if (num < this.mBuyItems.Count)
            {
                goto Label_00CD;
            }
            num2 = data2.items.Count;
            time = TimeManager.ServerTime;
            list = new List<LimitedShopItem>();
            num3 = 0;
            goto Label_0336;
        Label_011B:
            item = data2.items[num3];
            if (item.end == null)
            {
                goto Label_0180;
            }
            time2 = TimeManager.FromUnixTime(item.end);
            if ((time >= time2) == null)
            {
                goto Label_0157;
            }
            goto Label_0330;
        Label_0157:
            span = time2 - time;
            if (&span.TotalHours >= 1.0)
            {
                goto Label_0180;
            }
            list.Add(item);
        Label_0180:
            if (num3 < this.mBuyItems.Count)
            {
                goto Label_01BF;
            }
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.get_transform().SetParent(this.ItemLayoutParent, 0);
            this.mBuyItems.Add(obj2);
        Label_01BF:
            obj3 = this.mBuyItems[num3];
            list2 = obj3.GetComponentInChildren<LimitedShopBuyList>();
            list2.limitedShopItem = item;
            DataSource.Bind<LimitedShopItem>(obj3, item);
            list2.amount.SetActive(item.IsSet == 0);
            if (item.IsArtifact == null)
            {
                goto Label_0231;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname);
            DataSource.Bind<ArtifactParam>(obj3, param);
            goto Label_02D8;
        Label_0231:
            if (item.IsConceptCard == null)
            {
                goto Label_0259;
            }
            data3 = ConceptCardData.CreateConceptCardDataForDisplay(item.iname);
            list2.SetupConceptCard(data3);
            goto Label_02D8;
        Label_0259:
            if (item.IsItem != null)
            {
                goto Label_0271;
            }
            if (item.IsSet == null)
            {
                goto Label_02B6;
            }
        Label_0271:
            data4 = new ItemData();
            data4.Setup(0L, item.iname, item.num);
            DataSource.Bind<ItemData>(obj3, data4);
            DataSource.Bind<ItemParam>(obj3, MonoSingleton<GameManager>.Instance.GetItemParam(item.iname));
            goto Label_02D8;
        Label_02B6:
            DebugUtility.LogError(string.Format("不明な商品タイプが設定されています (shopitem.iname({0}) => {1})", item.iname, (EShopItemType) item.ShopItemType));
        Label_02D8:
            events = obj3.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0301;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Label_0301:
            button = obj3.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0328;
            }
            button.set_interactable(item.is_soldout == 0);
        Label_0328:
            obj3.SetActive(1);
        Label_0330:
            num3 += 1;
        Label_0336:
            if (num3 < num2)
            {
                goto Label_011B;
            }
            this.ShowAndSaveTimeOutItem(list);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private unsafe void ShowAndSaveTimeOutItem(List<LimitedShopItem> nearbyTimeout)
        {
            string str;
            ShopTimeOutItemInfoArray array;
            ShopTimeOutItemInfo[] infoArray;
            List<LimitedShopItem> list;
            List<LimitedShopItem> list2;
            bool flag;
            List<LimitedShopItem>.Enumerator enumerator;
            ShopTimeOutItemInfo info;
            IEnumerable<ShopTimeOutItemInfo> enumerable;
            ShopTimeOutItemInfoArray array2;
            string str2;
            ShopTimeOutItemInfo[] infoArray2;
            ShopTimeOutItemInfoArray array3;
            string str3;
            <ShowAndSaveTimeOutItem>c__AnonStorey353 storey;
            <ShowAndSaveTimeOutItem>c__AnonStorey354 storey2;
            <ShowAndSaveTimeOutItem>c__AnonStorey355 storey3;
            GlobalVars.TimeOutShopItems = new List<ShopItem>();
            if (nearbyTimeout.Count <= 0)
            {
                goto Label_0254;
            }
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT) == null)
            {
                goto Label_01EA;
            }
            array = JsonUtility.FromJson<ShopTimeOutItemInfoArray>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, string.Empty));
            if (array.Infos != null)
            {
                goto Label_0053;
            }
            infoArray = new ShopTimeOutItemInfo[0];
            goto Label_0081;
        Label_0053:
            if (<>f__am$cacheB != null)
            {
                goto Label_0071;
            }
            <>f__am$cacheB = new Func<ShopTimeOutItemInfo, bool>(LimitedShopBuyWindow.<ShowAndSaveTimeOutItem>m__353);
        Label_0071:
            infoArray = Enumerable.ToArray<ShopTimeOutItemInfo>(Enumerable.Where<ShopTimeOutItemInfo>(array.Infos, <>f__am$cacheB));
        Label_0081:
            list = new List<LimitedShopItem>();
            list2 = new List<LimitedShopItem>();
            flag = 0;
            storey = new <ShowAndSaveTimeOutItem>c__AnonStorey353();
            enumerator = nearbyTimeout.GetEnumerator();
        Label_00A0:
            try
            {
                goto Label_012E;
            Label_00A5:
                storey.item = &enumerator.Current;
                info = Enumerable.FirstOrDefault<ShopTimeOutItemInfo>(infoArray, new Func<ShopTimeOutItemInfo, bool>(storey.<>m__354));
                if (info == null)
                {
                    goto Label_0110;
                }
                if (storey.item.end <= info.End)
                {
                    goto Label_012E;
                }
                list2.Add(storey.item);
                info.End = storey.item.end;
                flag = 1;
                goto Label_012E;
            Label_0110:
                list2.Add(storey.item);
                list.Add(storey.item);
                flag = 1;
            Label_012E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00A5;
                }
                goto Label_014C;
            }
            finally
            {
            Label_013F:
                ((List<LimitedShopItem>.Enumerator) enumerator).Dispose();
            }
        Label_014C:
            if (flag == null)
            {
                goto Label_01BF;
            }
            storey2 = new <ShowAndSaveTimeOutItem>c__AnonStorey354();
            storey2.shop = GlobalVars.LimitedShopItem.shops;
            enumerable = Enumerable.Select<LimitedShopItem, ShopTimeOutItemInfo>(list, new Func<LimitedShopItem, ShopTimeOutItemInfo>(storey2.<>m__355));
            if (array.Infos == null)
            {
                goto Label_019A;
            }
            enumerable = Enumerable.Concat<ShopTimeOutItemInfo>(enumerable, array.Infos);
        Label_019A:
            array2 = new ShopTimeOutItemInfoArray(Enumerable.ToArray<ShopTimeOutItemInfo>(enumerable));
            str2 = JsonUtility.ToJson(array2);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, str2, 0);
        Label_01BF:
            GlobalVars.TimeOutShopItems = Enumerable.ToList<ShopItem>(Enumerable.Cast<ShopItem>(list2));
            if (list2.Count <= 0)
            {
                goto Label_0254;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
            goto Label_0254;
        Label_01EA:
            storey3 = new <ShowAndSaveTimeOutItem>c__AnonStorey355();
            storey3.shop = GlobalVars.LimitedShopItem.shops;
            array3 = new ShopTimeOutItemInfoArray(Enumerable.ToArray<ShopTimeOutItemInfo>(Enumerable.Select<LimitedShopItem, ShopTimeOutItemInfo>(nearbyTimeout, new Func<LimitedShopItem, ShopTimeOutItemInfo>(storey3.<>m__356))));
            str3 = JsonUtility.ToJson(array3);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, str3, 0);
            GlobalVars.TimeOutShopItems = Enumerable.ToList<ShopItem>(Enumerable.Cast<ShopItem>(nearbyTimeout));
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
        Label_0254:
            return;
        }

        [CompilerGenerated]
        private sealed class <OnSelect>c__AnonStorey356
        {
            internal GameObject go;

            public <OnSelect>c__AnonStorey356()
            {
                base..ctor();
                return;
            }

            internal bool <>m__357(GameObject p)
            {
                return (p == this.go);
            }
        }

        [CompilerGenerated]
        private sealed class <ShowAndSaveTimeOutItem>c__AnonStorey353
        {
            internal LimitedShopItem item;

            public <ShowAndSaveTimeOutItem>c__AnonStorey353()
            {
                base..ctor();
                return;
            }

            internal bool <>m__354(ShopTimeOutItemInfo t)
            {
                return (t.ItemId == this.item.id);
            }
        }

        [CompilerGenerated]
        private sealed class <ShowAndSaveTimeOutItem>c__AnonStorey354
        {
            internal JSON_ShopListArray.Shops shop;

            public <ShowAndSaveTimeOutItem>c__AnonStorey354()
            {
                base..ctor();
                return;
            }

            internal ShopTimeOutItemInfo <>m__355(LimitedShopItem target)
            {
                return new ShopTimeOutItemInfo(this.shop.gname, target.id, target.end);
            }
        }

        [CompilerGenerated]
        private sealed class <ShowAndSaveTimeOutItem>c__AnonStorey355
        {
            internal JSON_ShopListArray.Shops shop;

            public <ShowAndSaveTimeOutItem>c__AnonStorey355()
            {
                base..ctor();
                return;
            }

            internal ShopTimeOutItemInfo <>m__356(LimitedShopItem item)
            {
                return new ShopTimeOutItemInfo(this.shop.gname, item.id, item.end);
            }
        }
    }
}

