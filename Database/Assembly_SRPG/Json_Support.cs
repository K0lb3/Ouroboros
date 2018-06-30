namespace SRPG
{
    using System;

    [Serializable]
    public class Json_Support
    {
        public string fuid;
        public string name;
        public int lv;
        public int cost;
        public Json_Unit unit;
        public int isFriend;

        public Json_Support()
        {
            base..ctor();
            return;
        }
    }
}

