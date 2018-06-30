namespace SRPG
{
    using System;

    public class Json_LoginBonusTable
    {
        public int count;
        public string type;
        public string prefab;
        public string[] bonus_units;
        public int lastday;
        public Json_LoginBonus[] bonuses;

        public Json_LoginBonusTable()
        {
            base..ctor();
            return;
        }
    }
}

