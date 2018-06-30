namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_UnitGroupParam
    {
        public string iname;
        public string name;
        public string[] units;

        public JSON_UnitGroupParam()
        {
            base..ctor();
            return;
        }
    }
}

