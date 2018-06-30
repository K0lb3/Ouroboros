namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ReturnItemWindow : MonoBehaviour, IFlowInterface
    {
        public Transform ItemLayout;
        public GameObject ItemTemplate;
        public Text Title;
        public List<ItemData> ReturnItems;

        public ReturnItemWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            this.Refresh();
            return;
        }

        private void Awake()
        {
        }

        private void Refresh()
        {
            int num;
            ItemData data;
            GameObject obj2;
            if (this.ReturnItems == null)
            {
                goto Label_0098;
            }
            this.ItemTemplate.SetActive(1);
            num = 0;
            goto Label_007B;
        Label_001E:
            data = this.ReturnItems[num];
            if (string.IsNullOrEmpty(data.ItemID) != null)
            {
                goto Label_0077;
            }
            if (data.Num != null)
            {
                goto Label_004B;
            }
            goto Label_0077;
        Label_004B:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.get_transform().SetParent(this.ItemLayout, 0);
            DataSource.Bind<ItemData>(obj2, data);
            obj2.SetActive(1);
        Label_0077:
            num += 1;
        Label_007B:
            if (num < this.ReturnItems.Count)
            {
                goto Label_001E;
            }
            this.ItemTemplate.SetActive(0);
        Label_0098:
            base.set_enabled(1);
            return;
        }

        private void Start()
        {
            if (this.ReturnItems != null)
            {
                goto Label_001C;
            }
            this.ReturnItems = GlobalVars.ReturnItems;
            GlobalVars.ReturnItems = null;
        Label_001C:
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0049;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0049;
            }
            this.ItemTemplate.SetActive(0);
        Label_0049:
            if ((this.Title != null) == null)
            {
                goto Label_006F;
            }
            this.Title.set_text(LocalizedText.Get("sys.RETURN_ITEM_TITLE"));
        Label_006F:
            this.Refresh();
            return;
        }
    }
}

