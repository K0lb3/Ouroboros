namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_InitUnit
    {
        public string iname;
        public int rare;
        public int exp;
        public int[] party;

        public JSON_InitUnit()
        {
            base..ctor();
            return;
        }
    }
}

