namespace SRPG
{
    using System;

    public class Json_Gift
    {
        public string iname;
        public int num;
        public int rare;
        public int gold;
        public int coin;
        public int arenacoin;
        public int multicoin;
        public int kakeracoin;
        public Json_GiftConceptCard concept_card;

        public Json_Gift()
        {
            this.rare = -1;
            base..ctor();
            return;
        }
    }
}

