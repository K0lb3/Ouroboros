namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(1, "Refresh", 0, 1)]
    public class UnitCompositeWindow : MonoBehaviour, IFlowInterface
    {
        public RectTransform ItemLayoutParent;
        public RectTransform CommonItemLayoutParent;
        public GameObject ItemTemplate;
        public GameObject CommonItemTemplate;
        private ItemParam mItemParam;
        private List<GameObject> mConsumeObjects;

        public UnitCompositeWindow()
        {
            this.mConsumeObjects = new List<GameObject>(10);
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Refresh();
        Label_000D:
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

        public ItemData CreateItemData(string iname, int num)
        {
            Json_Item item;
            ItemData data;
            item = new Json_Item();
            item.iname = iname;
            item.num = num;
            data = new ItemData();
            data.Deserialize(item);
            return data;
        }

        private unsafe void Refresh()
        {
            int num;
            bool flag;
            Dictionary<string, int> dictionary;
            NeedEquipItemList list;
            int num2;
            int num3;
            KeyValuePair<string, int> pair;
            Dictionary<string, int>.Enumerator enumerator;
            GameObject obj2;
            GameObject obj3;
            ConsumeItemData data;
            byte num4;
            Dictionary<byte, NeedEquipItemDictionary>.KeyCollection.Enumerator enumerator2;
            NeedEquipItemDictionary dictionary2;
            ItemParam param;
            int num5;
            ItemParam param2;
            GameObject obj4;
            ItemData data2;
            GameObject obj5;
            ItemData data3;
            ItemData data4;
            CommonConvertItem item;
            this.mItemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.SelectedCreateItemID);
            num = 0;
            flag = 0;
            dictionary = null;
            list = new NeedEquipItemList();
            MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateItem(this.mItemParam, &flag, &num, &dictionary, list);
            num2 = 0;
            goto Label_0064;
        Label_0046:
            this.mConsumeObjects[num2].get_gameObject().SetActive(0);
            num2 += 1;
        Label_0064:
            if (num2 < this.mConsumeObjects.Count)
            {
                goto Label_0046;
            }
            if (dictionary == null)
            {
                goto Label_0145;
            }
            num3 = 0;
            enumerator = dictionary.GetEnumerator();
        Label_0087:
            try
            {
                goto Label_0127;
            Label_008C:
                pair = &enumerator.Current;
                if (num3 < this.mConsumeObjects.Count)
                {
                    goto Label_00D4;
                }
                obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
                obj2.get_transform().SetParent(this.ItemLayoutParent, 0);
                this.mConsumeObjects.Add(obj2);
            Label_00D4:
                obj3 = this.mConsumeObjects[num3];
                data = new ConsumeItemData();
                data.param = MonoSingleton<GameManager>.Instance.GetItemParam(&pair.Key);
                data.num = &pair.Value;
                DataSource.Bind<ConsumeItemData>(obj3, data);
                obj3.SetActive(1);
                num3 += 1;
            Label_0127:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_008C;
                }
                goto Label_0145;
            }
            finally
            {
            Label_0138:
                ((Dictionary<string, int>.Enumerator) enumerator).Dispose();
            }
        Label_0145:
            enumerator2 = list.CommonNeedNum.Keys.GetEnumerator();
        Label_0157:
            try
            {
                goto Label_02D2;
            Label_015C:
                num4 = &enumerator2.Current;
                dictionary2 = list.CommonNeedNum[num4];
                param = dictionary2.CommonItemParam;
                if (param == null)
                {
                    goto Label_02D2;
                }
                num5 = 0;
                goto Label_02BF;
            Label_018C:
                param2 = dictionary2.list[num5].Param;
                if (param2 != null)
                {
                    goto Label_01AD;
                }
                goto Label_02B9;
            Label_01AD:
                if ((param2.cmn_type - 1) != 2)
                {
                    goto Label_0207;
                }
                obj4 = Object.Instantiate<GameObject>(this.ItemTemplate);
                obj4.get_gameObject().SetActive(1);
                obj4.get_transform().SetParent(this.ItemLayoutParent, 0);
                data2 = this.CreateItemData(param2.iname, 1);
                DataSource.Bind<ItemData>(obj4, data2);
                goto Label_02B9;
            Label_0207:
                obj5 = Object.Instantiate<GameObject>(this.CommonItemTemplate);
                obj5.get_gameObject().SetActive(1);
                obj5.get_transform().SetParent(this.CommonItemLayoutParent, 0);
                data3 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param2.iname);
                if (data3 != null)
                {
                    goto Label_0263;
                }
                data3 = this.CreateItemData(param.iname, 0);
            Label_0263:
                data4 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.iname);
                if (data4 != null)
                {
                    goto Label_0292;
                }
                data4 = this.CreateItemData(param.iname, 0);
            Label_0292:
                obj5.GetComponent<CommonConvertItem>().Bind(data3, data4, dictionary2.list[num5].NeedPiece);
            Label_02B9:
                num5 += 1;
            Label_02BF:
                if (num5 < dictionary2.list.Count)
                {
                    goto Label_018C;
                }
            Label_02D2:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_015C;
                }
                goto Label_02F0;
            }
            finally
            {
            Label_02E3:
                ((Dictionary<byte, NeedEquipItemDictionary>.KeyCollection.Enumerator) enumerator2).Dispose();
            }
        Label_02F0:
            DataSource.Bind<ItemParam>(base.get_gameObject(), this.mItemParam);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }
    }
}

