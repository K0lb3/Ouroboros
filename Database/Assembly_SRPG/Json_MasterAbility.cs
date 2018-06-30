namespace SRPG
{
    using System;

    [Serializable]
    public class Json_MasterAbility
    {
        public long iid;
        public string iname;
        public int exp;

        public Json_MasterAbility()
        {
            base..ctor();
            return;
        }
    }
}

