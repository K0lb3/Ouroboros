namespace SRPG
{
    using System;

    [Serializable]
    public class Json_LoginBonus
    {
        public string iname;
        public int num;
        public int coin;
        public Json_LoginBonusVip vip;

        public Json_LoginBonus()
        {
            base..ctor();
            return;
        }
    }
}

