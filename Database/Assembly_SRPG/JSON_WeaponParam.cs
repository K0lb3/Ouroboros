namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_WeaponParam
    {
        public string iname;
        public int atk;
        public int formula;

        public JSON_WeaponParam()
        {
            base..ctor();
            return;
        }
    }
}

