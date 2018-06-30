namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_UnlockParam
    {
        public string iname;
        public int lv;
        public int vip;

        public JSON_UnlockParam()
        {
            base..ctor();
            return;
        }
    }
}

