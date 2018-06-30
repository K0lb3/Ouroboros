namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class ItemSelectListData
    {
        public List<ItemSelectListItemData> items;

        public ItemSelectListData()
        {
            base..ctor();
            return;
        }

        public void Deserialize(Json_ItemSelectResponse json)
        {
            int num;
            ItemSelectListItemData data;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (json.select != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            this.items = new List<ItemSelectListItemData>();
            num = 0;
            goto Label_0085;
        Label_0025:
            data = new ItemSelectListItemData();
            this.items.Add(data);
            this.items[num].Deserialize(json.select[num]);
            this.items[num].param = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.items[num].iiname);
            num += 1;
        Label_0085:
            if (num < ((int) json.select.Length))
            {
                goto Label_0025;
            }
            return;
        }
    }
}

