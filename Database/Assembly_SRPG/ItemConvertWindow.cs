namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "Initialize", 0, 0), Pin(2, "Output", 1, 1)]
    public class ItemConvertWindow : MonoBehaviour, IFlowInterface
    {
        public Transform ItemLayout;
        public GameObject ItemTemplate;
        public Text ConvertItemName;
        public Text ConvertItemNum;
        public Text ConvertResult;

        public ItemConvertWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0014;
            }
            this.Refresh();
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
        Label_0014:
            return;
        }

        private unsafe void Refresh()
        {
            int num;
            List<ItemData> list;
            int num2;
            ItemData data;
            GameObject obj2;
            SellItem item;
            int num3;
            if (GlobalVars.SellItemList != null)
            {
                goto Label_0019;
            }
            GlobalVars.SellItemList = new List<SellItem>();
            goto Label_0023;
        Label_0019:
            GlobalVars.SellItemList.Clear();
        Label_0023:
            num = 0;
            list = MonoSingleton<GameManager>.Instance.Player.Items;
            num2 = 0;
            goto Label_00FF;
        Label_003C:
            data = list[num2];
            if (data.ItemType == 9)
            {
                goto Label_0056;
            }
            goto Label_00FB;
        Label_0056:
            if (data.Num != null)
            {
                goto Label_0066;
            }
            goto Label_00FB;
        Label_0066:
            this.ConvertItemName.set_text(data.Param.name);
            this.ConvertItemNum.set_text(&data.Num.ToString());
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.get_transform().SetParent(this.ItemLayout, 0);
            obj2.SetActive(1);
            item = new SellItem();
            item.item = data;
            item.num = data.Num;
            GlobalVars.SellItemList.Add(item);
            num += data.Param.sell * data.Num;
        Label_00FB:
            num2 += 1;
        Label_00FF:
            if (num2 < list.Count)
            {
                goto Label_003C;
            }
            if ((this.ConvertResult != null) == null)
            {
                goto Label_013C;
            }
            this.ConvertResult.set_text(string.Format(LocalizedText.Get("sys.CONVERT_TO_GOLD"), (int) num));
        Label_013C:
            base.set_enabled(1);
            return;
        }

        private void Start()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0032;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0032;
            }
            this.ItemTemplate.get_gameObject().SetActive(0);
        Label_0032:
            return;
        }
    }
}

