namespace SRPG
{
    using System;

    public class UnitSelectListItemData
    {
        public string iname;
        public UnitParam param;

        public UnitSelectListItemData()
        {
            base..ctor();
            return;
        }

        public void Deserialize(Json_UnitSelectItem json)
        {
            this.iname = json.iname;
            return;
        }
    }
}

