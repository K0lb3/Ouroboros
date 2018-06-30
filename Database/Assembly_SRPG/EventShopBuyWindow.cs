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

    [Pin(0x68, "商品の期限切れ警告表示", 1, 0x68), Pin(1, "Refresh", 0, 1), Pin(100, "アイテム選択単品", 1, 100), Pin(0x65, "アイテム選択セット", 1, 0x65), Pin(0x66, "アイテム更新", 1, 0x66), Pin(0x67, "武具選択単品", 1, 0x67)]
    public class EventShopBuyWindow : MonoBehaviour, IFlowInterface
    {
        public RectTransform ItemLayoutParent;
        public GameObject ItemTemplate;
        public Button BtnUpdated;
        public GameObject ObjUpdateBtn;
        public GameObject ObjUpdateTime;
        public GameObject ObjLineup;
        public GameObject ObjItemNumLimit;
        public Text TxtPossessionCoinNum;
        public GameObject ImgEventCoinType;
        public Text ShopName;
        public GameObject CoinPeriodBanner;
        public Text CoinPeriodText;
        public List<GameObject> mBuyItems;
        [CompilerGenerated]
        private static Func<ShopTimeOutItemInfo, bool> <>f__am$cacheD;
        [CompilerGenerated]
        private static Predicate<EventCoinData> <>f__am$cacheE;

        public EventShopBuyWindow()
        {
            this.mBuyItems = new List<GameObject>(12);
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <SetPossessionCoinParam>m__30F(EventCoinData f)
        {
            return f.iname.Equals(GlobalVars.EventShopItem.shop_cost_iname);
        }

        [CompilerGenerated]
        private static bool <ShowAndSaveTimeOutItem>m__30A(ShopTimeOutItemInfo t)
        {
            return (t.ShopId == GlobalVars.EventShopItem.shops.gname);
        }

        public void Activated(int pinID)
        {
            this.Refresh();
            return;
        }

        private unsafe void Awake()
        {
            object[] objArray1;
            char[] chArray1;
            bool flag;
            string[] strArray;
            bool flag2;
            DateTime time;
            string str;
            string str2;
            string str3;
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
            flag = GlobalVars.EventShopItem.btn_update;
            if ((this.ObjUpdateBtn != null) == null)
            {
                goto Label_0082;
            }
            this.ObjUpdateBtn.SetActive(flag);
        Label_0082:
            if ((this.ObjUpdateTime != null) == null)
            {
                goto Label_009F;
            }
            this.ObjUpdateTime.SetActive(flag);
        Label_009F:
            if ((this.ObjLineup != null) == null)
            {
                goto Label_00BC;
            }
            this.ObjLineup.SetActive(flag);
        Label_00BC:
            if ((this.ObjItemNumLimit != null) == null)
            {
                goto Label_00DC;
            }
            this.ObjItemNumLimit.SetActive(flag == 0);
        Label_00DC:
            if ((this.ShopName != null) == null)
            {
                goto Label_010C;
            }
            this.ShopName.set_text(GlobalVars.EventShopItem.shops.info.title);
        Label_010C:
            chArray1 = new char[] { 0x2d };
            flag2 = GlobalVars.EventShopItem.shops.gname.Split(chArray1)[0] == "EventSummon2";
            if ((this.CoinPeriodBanner != null) == null)
            {
                goto Label_01EA;
            }
            if ((this.CoinPeriodText != null) == null)
            {
                goto Label_01EA;
            }
            if (flag2 == null)
            {
                goto Label_01D2;
            }
            if (GlobalVars.NewSummonCoinInfo == null)
            {
                goto Label_01D2;
            }
            time = GameUtility.UnixtimeToLocalTime(GlobalVars.NewSummonCoinInfo.Period);
            str = &time.ToString("yyyy/M/dd");
            str2 = &time.ToString("H:mm");
            objArray1 = new object[] { str, str2 };
            str3 = LocalizedText.Get("sys.SUMMON_COIN_PERIOD_TEXT", objArray1);
            this.CoinPeriodText.set_text(str3);
            this.CoinPeriodBanner.SetActive(1);
            goto Label_01EA;
        Label_01D2:
            this.CoinPeriodText.set_text(null);
            this.CoinPeriodBanner.SetActive(0);
        Label_01EA:
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
            EventShopBuyList list;
            <OnSelect>c__AnonStorey336 storey;
            storey = new <OnSelect>c__AnonStorey336();
            storey.go = go;
            num = this.mBuyItems.FindIndex(new Predicate<GameObject>(storey.<>m__30E));
            list = this.mBuyItems[num].GetComponent<EventShopBuyList>();
            GlobalVars.ShopBuyIndex = list.eventShopItem.id;
            if (list.eventShopItem.IsArtifact == null)
            {
                goto Label_0060;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
            return;
        Label_0060:
            if (list.eventShopItem.IsSet != null)
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
            EventShopData data2;
            int num;
            int num2;
            DateTime time;
            List<EventShopItem> list;
            int num3;
            EventShopItem item;
            DateTime time2;
            TimeSpan span;
            GameObject obj2;
            GameObject obj3;
            EventShopBuyList list2;
            ArtifactParam param;
            ConceptCardData data3;
            ItemData data4;
            ListItemEvents events;
            Button button;
            MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            this.SetPossessionCoinParam();
            data2 = MonoSingleton<GameManager>.Instance.Player.GetEventShopData();
            DebugUtility.Assert((data2 == null) == 0, "ショップ情報が存在しない");
            num = 0;
            goto Label_006C;
        Label_0051:
            this.mBuyItems[num].get_gameObject().SetActive(0);
            num += 1;
        Label_006C:
            if (num < this.mBuyItems.Count)
            {
                goto Label_0051;
            }
            num2 = data2.items.Count;
            time = TimeManager.ServerTime;
            list = new List<EventShopItem>();
            num3 = 0;
            goto Label_02A4;
        Label_009F:
            item = data2.items[num3];
            if (item.end == null)
            {
                goto Label_0104;
            }
            time2 = TimeManager.FromUnixTime(item.end);
            if ((time >= time2) == null)
            {
                goto Label_00DB;
            }
            goto Label_029E;
        Label_00DB:
            span = time2 - time;
            if (&span.TotalHours >= 1.0)
            {
                goto Label_0104;
            }
            list.Add(item);
        Label_0104:
            if (num3 < this.mBuyItems.Count)
            {
                goto Label_0143;
            }
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.get_transform().SetParent(this.ItemLayoutParent, 0);
            this.mBuyItems.Add(obj2);
        Label_0143:
            obj3 = this.mBuyItems[num3];
            list2 = obj3.GetComponentInChildren<EventShopBuyList>();
            list2.eventShopItem = item;
            DataSource.Bind<EventShopItem>(obj3, item);
            if (item.IsArtifact == null)
            {
                goto Label_019F;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname);
            DataSource.Bind<ArtifactParam>(obj3, param);
            goto Label_0246;
        Label_019F:
            if (item.IsConceptCard == null)
            {
                goto Label_01C7;
            }
            data3 = ConceptCardData.CreateConceptCardDataForDisplay(item.iname);
            list2.SetupConceptCard(data3);
            goto Label_0246;
        Label_01C7:
            if (item.IsItem != null)
            {
                goto Label_01DF;
            }
            if (item.IsSet == null)
            {
                goto Label_0224;
            }
        Label_01DF:
            data4 = new ItemData();
            data4.Setup(0L, item.iname, item.num);
            DataSource.Bind<ItemData>(obj3, data4);
            DataSource.Bind<ItemParam>(obj3, MonoSingleton<GameManager>.Instance.GetItemParam(item.iname));
            goto Label_0246;
        Label_0224:
            DebugUtility.LogError(string.Format("不明な商品タイプが設定されています (shopitem.iname({0}) => {1})", item.iname, (EShopItemType) item.ShopItemType));
        Label_0246:
            events = obj3.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_026F;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Label_026F:
            button = obj3.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0296;
            }
            button.set_interactable(item.is_soldout == 0);
        Label_0296:
            obj3.SetActive(1);
        Label_029E:
            num3 += 1;
        Label_02A4:
            if (num3 < num2)
            {
                goto Label_009F;
            }
            this.ShowAndSaveTimeOutItem(list);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void SetPossessionCoinParam()
        {
            ItemParam param;
            List<EventCoinData> list;
            EventCoinData data;
            if ((this.ImgEventCoinType != null) == null)
            {
                goto Label_0032;
            }
            param = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.EventShopItem.shop_cost_iname);
            DataSource.Bind<ItemParam>(this.ImgEventCoinType, param);
        Label_0032:
            if ((this.TxtPossessionCoinNum != null) == null)
            {
                goto Label_0088;
            }
            if (<>f__am$cacheE != null)
            {
                goto Label_006C;
            }
            <>f__am$cacheE = new Predicate<EventCoinData>(EventShopBuyWindow.<SetPossessionCoinParam>m__30F);
        Label_006C:
            data = MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find(<>f__am$cacheE);
            DataSource.Bind<EventCoinData>(this.TxtPossessionCoinNum.get_gameObject(), data);
        Label_0088:
            return;
        }

        private unsafe void ShowAndSaveTimeOutItem(List<EventShopItem> nearbyTimeout)
        {
            string str;
            ShopTimeOutItemInfoArray array;
            ShopTimeOutItemInfo[] infoArray;
            List<EventShopItem> list;
            List<EventShopItem> list2;
            bool flag;
            List<EventShopItem>.Enumerator enumerator;
            ShopTimeOutItemInfo info;
            IEnumerable<ShopTimeOutItemInfo> enumerable;
            ShopTimeOutItemInfoArray array2;
            string str2;
            ShopTimeOutItemInfo[] infoArray2;
            ShopTimeOutItemInfoArray array3;
            string str3;
            <ShowAndSaveTimeOutItem>c__AnonStorey333 storey;
            <ShowAndSaveTimeOutItem>c__AnonStorey334 storey2;
            <ShowAndSaveTimeOutItem>c__AnonStorey335 storey3;
            GlobalVars.TimeOutShopItems = new List<ShopItem>();
            if (nearbyTimeout.Count <= 0)
            {
                goto Label_0254;
            }
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT) == null)
            {
                goto Label_01EA;
            }
            array = JsonUtility.FromJson<ShopTimeOutItemInfoArray>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, string.Empty));
            if (array.Infos != null)
            {
                goto Label_0053;
            }
            infoArray = new ShopTimeOutItemInfo[0];
            goto Label_0081;
        Label_0053:
            if (<>f__am$cacheD != null)
            {
                goto Label_0071;
            }
            <>f__am$cacheD = new Func<ShopTimeOutItemInfo, bool>(EventShopBuyWindow.<ShowAndSaveTimeOutItem>m__30A);
        Label_0071:
            infoArray = Enumerable.ToArray<ShopTimeOutItemInfo>(Enumerable.Where<ShopTimeOutItemInfo>(array.Infos, <>f__am$cacheD));
        Label_0081:
            list = new List<EventShopItem>();
            list2 = new List<EventShopItem>();
            flag = 0;
            storey = new <ShowAndSaveTimeOutItem>c__AnonStorey333();
            enumerator = nearbyTimeout.GetEnumerator();
        Label_00A0:
            try
            {
                goto Label_012E;
            Label_00A5:
                storey.item = &enumerator.Current;
                info = Enumerable.FirstOrDefault<ShopTimeOutItemInfo>(infoArray, new Func<ShopTimeOutItemInfo, bool>(storey.<>m__30B));
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
                ((List<EventShopItem>.Enumerator) enumerator).Dispose();
            }
        Label_014C:
            if (flag == null)
            {
                goto Label_01BF;
            }
            storey2 = new <ShowAndSaveTimeOutItem>c__AnonStorey334();
            storey2.shop = GlobalVars.EventShopItem.shops;
            enumerable = Enumerable.Select<EventShopItem, ShopTimeOutItemInfo>(list, new Func<EventShopItem, ShopTimeOutItemInfo>(storey2.<>m__30C));
            if (array.Infos == null)
            {
                goto Label_019A;
            }
            enumerable = Enumerable.Concat<ShopTimeOutItemInfo>(enumerable, array.Infos);
        Label_019A:
            array2 = new ShopTimeOutItemInfoArray(Enumerable.ToArray<ShopTimeOutItemInfo>(enumerable));
            str2 = JsonUtility.ToJson(array2);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, str2, 0);
        Label_01BF:
            GlobalVars.TimeOutShopItems = Enumerable.ToList<ShopItem>(Enumerable.Cast<ShopItem>(list2));
            if (list2.Count <= 0)
            {
                goto Label_0254;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
            goto Label_0254;
        Label_01EA:
            storey3 = new <ShowAndSaveTimeOutItem>c__AnonStorey335();
            storey3.shop = GlobalVars.EventShopItem.shops;
            array3 = new ShopTimeOutItemInfoArray(Enumerable.ToArray<ShopTimeOutItemInfo>(Enumerable.Select<EventShopItem, ShopTimeOutItemInfo>(nearbyTimeout, new Func<EventShopItem, ShopTimeOutItemInfo>(storey3.<>m__30D))));
            str3 = JsonUtility.ToJson(array3);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, str3, 0);
            GlobalVars.TimeOutShopItems = Enumerable.ToList<ShopItem>(Enumerable.Cast<ShopItem>(nearbyTimeout));
            FlowNode_GameObject.ActivateOutputLinks(this, 0x68);
        Label_0254:
            return;
        }

        private void Start()
        {
        }

        [CompilerGenerated]
        private sealed class <OnSelect>c__AnonStorey336
        {
            internal GameObject go;

            public <OnSelect>c__AnonStorey336()
            {
                base..ctor();
                return;
            }

            internal bool <>m__30E(GameObject p)
            {
                return (p == this.go);
            }
        }

        [CompilerGenerated]
        private sealed class <ShowAndSaveTimeOutItem>c__AnonStorey333
        {
            internal EventShopItem item;

            public <ShowAndSaveTimeOutItem>c__AnonStorey333()
            {
                base..ctor();
                return;
            }

            internal bool <>m__30B(ShopTimeOutItemInfo t)
            {
                return (t.ItemId == this.item.id);
            }
        }

        [CompilerGenerated]
        private sealed class <ShowAndSaveTimeOutItem>c__AnonStorey334
        {
            internal JSON_ShopListArray.Shops shop;

            public <ShowAndSaveTimeOutItem>c__AnonStorey334()
            {
                base..ctor();
                return;
            }

            internal ShopTimeOutItemInfo <>m__30C(EventShopItem target)
            {
                return new ShopTimeOutItemInfo(this.shop.gname, target.id, target.end);
            }
        }

        [CompilerGenerated]
        private sealed class <ShowAndSaveTimeOutItem>c__AnonStorey335
        {
            internal JSON_ShopListArray.Shops shop;

            public <ShowAndSaveTimeOutItem>c__AnonStorey335()
            {
                base..ctor();
                return;
            }

            internal ShopTimeOutItemInfo <>m__30D(EventShopItem item)
            {
                return new ShopTimeOutItemInfo(this.shop.gname, item.id, item.end);
            }
        }
    }
}

