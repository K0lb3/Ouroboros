namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_UnitUnlockTimeParam
    {
        public string iname;
        public string name;
        public string begin_at;
        public string end_at;

        public JSON_UnitUnlockTimeParam()
        {
            base..ctor();
            return;
        }
    }
}

