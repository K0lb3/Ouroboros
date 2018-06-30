namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class UnitSelectListData
    {
        public List<UnitSelectListItemData> items;

        public UnitSelectListData()
        {
            base..ctor();
            return;
        }

        public void Deserialize(Json_UnitSelectResponse json)
        {
            int num;
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
            this.items = new List<UnitSelectListItemData>();
            num = 0;
            goto Label_0083;
        Label_0025:
            this.items.Add(new UnitSelectListItemData());
            this.items[num].Deserialize(json.select[num]);
            this.items[num].param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.items[num].iname);
            num += 1;
        Label_0083:
            if (num < ((int) json.select.Length))
            {
                goto Label_0025;
            }
            return;
        }
    }
}

