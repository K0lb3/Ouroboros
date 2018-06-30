namespace SRPG
{
    using System;

    [Serializable]
    public class Json_Artifact
    {
        public string iname;
        public long iid;
        public int rare;
        public int exp;
        public int fav;

        public Json_Artifact()
        {
            base..ctor();
            return;
        }
    }
}

