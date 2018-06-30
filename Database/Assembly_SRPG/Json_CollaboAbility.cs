namespace SRPG
{
    using System;

    [Serializable]
    public class Json_CollaboAbility
    {
        public long iid;
        public string iname;
        public int exp;
        public Json_CollaboSkill[] skills;

        public Json_CollaboAbility()
        {
            base..ctor();
            return;
        }
    }
}

