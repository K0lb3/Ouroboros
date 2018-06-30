namespace SRPG
{
    using System;

    [Serializable]
    public class Json_Equip
    {
        public long iid;
        public string iname;
        public int exp;

        public Json_Equip()
        {
            base..ctor();
            return;
        }
    }
}

