namespace SRPG
{
    using System;

    public class ItemSelectListItemData
    {
        public string iiname;
        public short id;
        public short num;
        public ItemParam param;

        public ItemSelectListItemData()
        {
            base..ctor();
            return;
        }

        public void Deserialize(Json_ItemSelectItem json)
        {
            this.iiname = json.iname;
            this.id = json.id;
            this.num = json.num;
            return;
        }
    }
}

