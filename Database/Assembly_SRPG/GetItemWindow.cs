namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(1, "Refresh", 0, 1), Pin(100, "アイテム選択", 1, 100), Pin(0x65, "アイテム更新", 1, 0x65)]
    public class GetItemWindow : MonoBehaviour, IFlowInterface
    {
        public RectTransform ItemLayoutParent;
        public GameObject ItemTemplate;
        private List<GameObject> ItemSelectItem;

        public GetItemWindow()
        {
            this.ItemSelectItem = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
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

        private void OnSelect(GameObject go)
        {
            ItemSelectListItemData data;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            GlobalVars.ItemSelectListItemData = DataSource.FindDataOfClass<ItemSelectListItemData>(go, null);
            return;
        }

        public void Refresh(ItemSelectListItemData[] shopdata)
        {
            PlayerData data;
            int num;
            int num2;
            int num3;
            ItemSelectListItemData data2;
            GameObject obj2;
            GameObject obj3;
            ItemData data3;
            ListItemEvents events;
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_003F;
        Label_0024:
            this.ItemSelectItem[num].get_gameObject().SetActive(0);
            num += 1;
        Label_003F:
            if (num < this.ItemSelectItem.Count)
            {
                goto Label_0024;
            }
            num2 = (int) shopdata.Length;
            num3 = 0;
            goto Label_011A;
        Label_005B:
            data2 = shopdata[num3];
            if (num3 < this.ItemSelectItem.Count)
            {
                goto Label_009E;
            }
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.get_transform().SetParent(this.ItemLayoutParent, 0);
            this.ItemSelectItem.Add(obj2);
        Label_009E:
            obj3 = this.ItemSelectItem[num3];
            DataSource.Bind<ItemSelectListItemData>(obj3, data2);
            data3 = data.FindItemDataByItemID(data2.iiname);
            DataSource.Bind<ItemData>(obj3, data3);
            DataSource.Bind<ItemParam>(obj3, MonoSingleton<GameManager>.Instance.GetItemParam(data2.iiname));
            events = obj3.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_010E;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Label_010E:
            obj3.SetActive(1);
            num3 += 1;
        Label_011A:
            if (num3 < num2)
            {
                goto Label_005B;
            }
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
        }
    }
}

