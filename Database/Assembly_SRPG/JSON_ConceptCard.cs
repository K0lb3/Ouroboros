namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ConceptCard
    {
        public long iid;
        public string iname;
        public int exp;
        public int trust;
        public int fav;
        public int trust_bonus;
        public int plus;

        public JSON_ConceptCard()
        {
            base..ctor();
            return;
        }
    }
}

