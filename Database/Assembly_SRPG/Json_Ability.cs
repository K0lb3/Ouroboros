namespace SRPG
{
    using System;

    [Serializable]
    public class Json_Ability
    {
        public long iid;
        public string iname;
        public int exp;

        public Json_Ability()
        {
            base..ctor();
            return;
        }
    }
}

