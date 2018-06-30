namespace SRPG
{
    using System;

    public class Json_Party
    {
        public string ptype;
        public long[] units;
        public int flg_sel;
        public int flg_seldef;

        public Json_Party()
        {
            base..ctor();
            return;
        }
    }
}

