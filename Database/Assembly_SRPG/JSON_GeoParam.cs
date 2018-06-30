namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_GeoParam
    {
        public string iname;
        public string name;
        public int cost;
        public int stop;

        public JSON_GeoParam()
        {
            base..ctor();
            return;
        }
    }
}

