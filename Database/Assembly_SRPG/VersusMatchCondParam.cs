namespace SRPG
{
    using System;

    public class VersusMatchCondParam
    {
        public OInt Floor;
        public OInt LvRange;
        public OInt FloorRange;

        public VersusMatchCondParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_VersusMatchCondParam json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.Floor = json.floor;
            this.LvRange = json.lvrang;
            this.FloorRange = json.floorrange;
            return;
        }
    }
}

