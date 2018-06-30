namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1), Pin(0x65, "イベントショップが押下された", 1, 0x65)]
    public class EventShopCoinList : MonoBehaviour, IFlowInterface
    {
        private const int PIN_ID_REFRESH = 1;
        private const int PIN_ID_SHOPBTN = 0x65;
        [SerializeField]
        private GameObject ItemTemplate;
        [SerializeField]
        private GameObject ArenaTemplate;
        [SerializeField]
        private GameObject MultiTemplate;
        [SerializeField]
        private ListExtras ScrollView;
        private List<GameObject> mEventCoinListItems;

        public EventShopCoinList()
        {
            this.mEventCoinListItems = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_000E;
            }
            goto Label_0019;
        Label_000E:
            this.OnRefresh();
        Label_0019:
            return;
        }

        private void ActivateOutputLinks(int pinID)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, pinID);
            return;
        }

        private void Awake()
        {
            GlobalVars.SelectionEventShop = null;
            GlobalVars.SelectionCoinListType = 0;
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0039;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0039;
            }
            this.ItemTemplate.SetActive(0);
        Label_0039:
            if ((this.ArenaTemplate != null) == null)
            {
                goto Label_0066;
            }
            if (this.ArenaTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0066;
            }
            this.ArenaTemplate.SetActive(0);
        Label_0066:
            if ((this.MultiTemplate != null) == null)
            {
                goto Label_0093;
            }
            if (this.MultiTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0093;
            }
            this.MultiTemplate.SetActive(0);
        Label_0093:
            return;
        }

        private GameObject CreateListItem(EventCoinData eventcoin_data)
        {
            GameObject obj2;
            EventCoinListItem item;
            Button button;
            ListItemEvents events;
            EventShopListItem item2;
            bool flag;
            <CreateListItem>c__AnonStorey337 storey;
            storey = new <CreateListItem>c__AnonStorey337();
            storey.eventcoin_data = eventcoin_data;
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            item = obj2.GetComponent<EventCoinListItem>();
            button = item.Button.GetComponent<Button>();
            events = item.Button.GetComponent<ListItemEvents>();
            if (((button != null) == null) || ((events != null) == null))
            {
                goto Label_00F9;
            }
            item2 = GlobalVars.EventShopListItems.Find(new Predicate<EventShopListItem>(storey.<>m__310));
            flag = 0;
            if ((((item2 != null) == null) || (item2.EventShopInfo.shops == null)) || (item2.EventShopInfo.shops.unlock == null))
            {
                goto Label_00CD;
            }
            if (((item2.EventShopInfo.shops.unlock.flg != 1) ? 0 : 1) == null)
            {
                goto Label_00CD;
            }
            flag = 1;
        Label_00CD:
            if (flag == null)
            {
                goto Label_00F2;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
            button.set_interactable(1);
            goto Label_00F9;
        Label_00F2:
            button.set_interactable(0);
        Label_00F9:
            return obj2;
        }

        private GameObject CreateOtherListItem(GameObject template, ListItemEvents.ListItemEvent func, bool is_button_enable)
        {
            GameObject obj2;
            EventCoinListItem item;
            Button button;
            ListItemEvents events;
            obj2 = Object.Instantiate<GameObject>(template);
            item = obj2.GetComponent<EventCoinListItem>();
            button = item.Button.GetComponent<Button>();
            events = item.Button.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_004B;
            }
            if (is_button_enable == null)
            {
                goto Label_004B;
            }
            events.OnSelect = func;
            button.set_interactable(1);
            goto Label_0052;
        Label_004B:
            button.set_interactable(0);
        Label_0052:
            return obj2;
        }

        private void OnRefresh()
        {
            this.UpdateItems();
            return;
        }

        private void OnSelect(GameObject go)
        {
            EventShopListItem item;
            <OnSelect>c__AnonStorey338 storey;
            storey = new <OnSelect>c__AnonStorey338();
            storey.coindata = DataSource.FindDataOfClass<EventCoinData>(go, null);
            if (storey.coindata.iname == null)
            {
                goto Label_004E;
            }
            GlobalVars.SelectionCoinListType = 1;
            GlobalVars.SelectionEventShop = GlobalVars.EventShopListItems.Find(new Predicate<EventShopListItem>(storey.<>m__311));
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
        Label_004E:
            return;
        }

        private void OnSelectArenaShop(GameObject go)
        {
            GlobalVars.SelectionCoinListType = 2;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        private void OnSelectMultiShop(GameObject go)
        {
            GlobalVars.SelectionCoinListType = 3;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        private void UpdateItems()
        {
            List<EventCoinData> list;
            Transform transform;
            int num;
            GameObject obj2;
            EventCoinData data;
            PlayerData data2;
            bool flag;
            GameObject obj3;
            GameObject obj4;
            MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            list = MonoSingleton<GameManager>.Instance.Player.EventCoinList;
            transform = base.get_transform();
            num = 0;
            goto Label_0082;
        Label_003F:
            obj2 = this.CreateListItem(list[num]);
            obj2.get_transform().SetParent(transform, 0);
            this.mEventCoinListItems.Add(obj2);
            obj2.SetActive(1);
            data = list[num];
            DataSource.Bind<EventCoinData>(obj2, data);
            num += 1;
        Label_0082:
            if (num < list.Count)
            {
                goto Label_003F;
            }
            if ((this.ArenaTemplate != null) == null)
            {
                goto Label_00F5;
            }
            flag = MonoSingleton<GameManager>.Instance.Player.CheckUnlock(0x10);
            obj3 = this.CreateOtherListItem(this.ArenaTemplate, new ListItemEvents.ListItemEvent(this.OnSelectArenaShop), flag);
            obj3.get_transform().SetParent(transform, 0);
            this.mEventCoinListItems.Add(obj3);
            obj3.SetActive(1);
        Label_00F5:
            if ((this.MultiTemplate != null) == null)
            {
                goto Label_0144;
            }
            obj4 = this.CreateOtherListItem(this.MultiTemplate, new ListItemEvents.ListItemEvent(this.OnSelectMultiShop), 1);
            obj4.get_transform().SetParent(transform, 0);
            this.mEventCoinListItems.Add(obj4);
            obj4.SetActive(1);
        Label_0144:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        [CompilerGenerated]
        private sealed class <CreateListItem>c__AnonStorey337
        {
            internal EventCoinData eventcoin_data;

            public <CreateListItem>c__AnonStorey337()
            {
                base..ctor();
                return;
            }

            internal bool <>m__310(EventShopListItem f)
            {
                return f.EventShopInfo.shop_cost_iname.Equals(this.eventcoin_data.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <OnSelect>c__AnonStorey338
        {
            internal EventCoinData coindata;

            public <OnSelect>c__AnonStorey338()
            {
                base..ctor();
                return;
            }

            internal bool <>m__311(EventShopListItem f)
            {
                return f.EventShopInfo.shop_cost_iname.Equals(this.coindata.iname);
            }
        }
    }
}

