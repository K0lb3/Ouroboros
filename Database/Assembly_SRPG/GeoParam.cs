namespace SRPG
{
    using System;

    [Serializable]
    public class GeoParam
    {
        public string iname;
        public string name;
        public OInt cost;
        public OBool DisableStopped;

        public GeoParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_GeoParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.name = json.name;
            this.cost = Math.Max(json.cost, 1);
            this.DisableStopped = (json.stop == 0) == 0;
            return 1;
        }
    }
}

