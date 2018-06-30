namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1)]
    public class ShopSellConfirmWindow : MonoBehaviour, IFlowInterface
    {
        public RectTransform ItemLayoutParent;
        public GameObject ItemTemplate;
        public Text TxtCaution;
        private List<GameObject> mSellItems;

        public ShopSellConfirmWindow()
        {
            this.mSellItems = new List<GameObject>(10);
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

        private void Refresh()
        {
            bool flag;
            int num;
            List<SellItem> list;
            int num2;
            int num3;
            GameObject obj2;
            GameObject obj3;
            if ((this.TxtCaution != null) == null)
            {
                goto Label_0061;
            }
            flag = 0;
            num = 0;
            goto Label_0040;
        Label_001A:
            if (GlobalVars.SellItemList[num].item.Rarity <= 1)
            {
                goto Label_003C;
            }
            flag = 1;
            goto Label_0050;
        Label_003C:
            num += 1;
        Label_0040:
            if (num < GlobalVars.SellItemList.Count)
            {
                goto Label_001A;
            }
        Label_0050:
            this.TxtCaution.get_gameObject().SetActive(flag);
        Label_0061:
            list = GlobalVars.SellItemList;
            num2 = 0;
            goto Label_0089;
        Label_006E:
            this.mSellItems[num2].get_gameObject().SetActive(0);
            num2 += 1;
        Label_0089:
            if (num2 < this.mSellItems.Count)
            {
                goto Label_006E;
            }
            num3 = 0;
            goto Label_010D;
        Label_00A2:
            if (num3 < this.mSellItems.Count)
            {
                goto Label_00E1;
            }
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.get_transform().SetParent(this.ItemLayoutParent, 0);
            this.mSellItems.Add(obj2);
        Label_00E1:
            obj3 = this.mSellItems[num3];
            DataSource.Bind<SellItem>(obj3, list[num3]);
            obj3.SetActive(1);
            num3 += 1;
        Label_010D:
            if (num3 < list.Count)
            {
                goto Label_00A2;
            }
            DataSource.Bind<List<SellItem>>(base.get_gameObject(), GlobalVars.SellItemList);
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

