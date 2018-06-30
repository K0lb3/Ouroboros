namespace SRPG
{
    using System;

    public class Json_GachaHistoryItem
    {
        public string itype;
        public string iname;
        public int is_new;
        public int num;
        public int rare;
        public int convert_piece;

        public Json_GachaHistoryItem()
        {
            this.rare = -1;
            base..ctor();
            return;
        }
    }
}

