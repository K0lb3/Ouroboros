namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(150, "装備スロット解除", 0, 150), AddComponentMenu("SRPG/UI/アイテムリスト"), Pin(100, "アイテムリセット", 0, 0), Pin(0x65, "アイテム決定", 0, 1), Pin(110, "全アイテム表示", 0, 10), Pin(0x6f, "回復アイテム表示", 0, 11), Pin(0x70, "攻撃アイテム表示", 0, 12)]
    public class ItemList : SRPG_ListBase, IFlowInterface
    {
        private ItemData[] mInventoryCache;
        public static ItemData SelectedItem;
        public GameObject ItemTemplate;
        public ItemFilters Filter;

        public ItemList()
        {
            this.mInventoryCache = new ItemData[5];
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0013;
            }
            this.ResetInventory();
            goto Label_00AF;
        Label_0013:
            if (pinID != 0x65)
            {
                goto Label_0026;
            }
            this.CacheInventory();
            goto Label_00AF;
        Label_0026:
            if (pinID != 110)
            {
                goto Label_0040;
            }
            this.Filter = 0;
            this.RefreshItems();
            goto Label_00AF;
        Label_0040:
            if (pinID != 0x6f)
            {
                goto Label_005A;
            }
            this.Filter = 1;
            this.RefreshItems();
            goto Label_00AF;
        Label_005A:
            if (pinID != 0x70)
            {
                goto Label_0074;
            }
            this.Filter = 2;
            this.RefreshItems();
            goto Label_00AF;
        Label_0074:
            if (pinID != 150)
            {
                goto Label_00AF;
            }
            if ((InventorySlot.Active != null) == null)
            {
                goto Label_00AF;
            }
            MonoSingleton<GameManager>.Instance.Player.SetInventory(InventorySlot.Active.Index, null);
            this.InventoryChanged();
        Label_00AF:
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
            return;
        }

        private void CacheInventory()
        {
            PlayerData data;
            int num;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_0026;
        Label_0012:
            this.mInventoryCache[num] = data.Inventory[num];
            num += 1;
        Label_0026:
            if (num < ((int) this.mInventoryCache.Length))
            {
                goto Label_0012;
            }
            return;
        }

        private void InventoryChanged()
        {
            GameParameter.UpdateValuesOfType(0x2e);
            GameParameter.UpdateValuesOfType(0x2f);
            GameParameter.UpdateValuesOfType(0x35);
            GameParameter.UpdateValuesOfType(0x7f);
            return;
        }

        private void OnCloseDetail(GameObject go)
        {
        }

        private void OnOpenDetail(GameObject go)
        {
        }

        private void OnSelect(GameObject go)
        {
            ItemData data;
            int num;
            data = DataSource.FindDataOfClass<ItemData>(go, null);
            if (data == null)
            {
                goto Label_0078;
            }
            if ((InventorySlot.Active != null) == null)
            {
                goto Label_0078;
            }
            num = 0;
            goto Label_0051;
        Label_0025:
            if (MonoSingleton<GameManager>.Instance.Player.Inventory[num] != data)
            {
                goto Label_004D;
            }
            MonoSingleton<GameManager>.Instance.Player.SetInventory(num, null);
        Label_004D:
            num += 1;
        Label_0051:
            if (num < 5)
            {
                goto Label_0025;
            }
            MonoSingleton<GameManager>.Instance.Player.SetInventory(InventorySlot.Active.Index, data);
            this.InventoryChanged();
        Label_0078:
            return;
        }

        private void RefreshItems()
        {
            ListItemEvents[] eventsArray;
            int num;
            ListItemEvents events;
            ItemData data;
            SkillEffectTypes types;
            eventsArray = base.Items;
            num = ((int) eventsArray.Length) - 1;
            goto Label_016B;
        Label_0012:
            events = eventsArray[num];
            events.get_gameObject().SetActive(0);
            data = DataSource.FindDataOfClass<ItemData>(events.get_gameObject(), null);
            if (data == null)
            {
                goto Label_0167;
            }
            if (data.ItemType == null)
            {
                goto Label_0045;
            }
            goto Label_0167;
        Label_0045:
            if (data.Skill != null)
            {
                goto Label_005F;
            }
            Debug.LogError("消費アイテムに対してスキル効果が設定されていない");
            goto Label_0167;
        Label_005F:
            if (this.Filter != null)
            {
                goto Label_007B;
            }
            events.get_gameObject().SetActive(1);
            goto Label_0167;
        Label_007B:
            if (this.Filter != 1)
            {
                goto Label_00FC;
            }
            types = data.Skill.EffectType;
            switch ((types - 4))
            {
                case 0:
                    goto Label_00D5;

                case 1:
                    goto Label_00D5;

                case 2:
                    goto Label_00AD;

                case 3:
                    goto Label_00D5;
            }
        Label_00AD:
            switch ((types - 12))
            {
                case 0:
                    goto Label_00D5;

                case 1:
                    goto Label_00C7;

                case 2:
                    goto Label_00C7;

                case 3:
                    goto Label_00D5;
            }
        Label_00C7:
            if (types == 0x13)
            {
                goto Label_00D5;
            }
            goto Label_00E6;
        Label_00D5:
            events.get_gameObject().SetActive(1);
            goto Label_00F7;
        Label_00E6:
            events.get_gameObject().SetActive(0);
        Label_00F7:
            goto Label_0167;
        Label_00FC:
            if (this.Filter != 2)
            {
                goto Label_0167;
            }
            types = data.Skill.EffectType;
            if (types == 2)
            {
                goto Label_0145;
            }
            if (types == 6)
            {
                goto Label_0145;
            }
            if (types == 11)
            {
                goto Label_0145;
            }
            if (types == 20)
            {
                goto Label_0145;
            }
            if (types == 0x1c)
            {
                goto Label_0145;
            }
            goto Label_0156;
        Label_0145:
            events.get_gameObject().SetActive(1);
            goto Label_0167;
        Label_0156:
            events.get_gameObject().SetActive(0);
        Label_0167:
            num -= 1;
        Label_016B:
            if (num >= 0)
            {
                goto Label_0012;
            }
            return;
        }

        private void ResetInventory()
        {
            PlayerData data;
            int num;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_0025;
        Label_0012:
            data.SetInventory(num, this.mInventoryCache[num]);
            num += 1;
        Label_0025:
            if (num < ((int) this.mInventoryCache.Length))
            {
                goto Label_0012;
            }
            this.InventoryChanged();
            return;
        }

        protected override void Start()
        {
            PlayerData data;
            int num;
            GameObject obj2;
            ListItemEvents events;
            this.CacheInventory();
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_00E1;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_00D0;
        Label_0029:
            if (data.Items[num].Num > 0)
            {
                goto Label_0045;
            }
            goto Label_00CC;
        Label_0045:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            DataSource.Bind<ItemData>(obj2, data.Items[num]);
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_00AC;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenDetail);
            events.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseDetail);
        Label_00AC:
            obj2.get_transform().SetParent(base.get_transform(), 0);
            obj2.SetActive(1);
            base.AddItem(events);
        Label_00CC:
            num += 1;
        Label_00D0:
            if (num < data.Items.Count)
            {
                goto Label_0029;
            }
        Label_00E1:
            base.Start();
            this.RefreshItems();
            return;
        }

        public enum ItemFilters
        {
            All,
            Potions,
            Offensive
        }
    }
}

