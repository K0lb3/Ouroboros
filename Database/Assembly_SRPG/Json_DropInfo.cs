namespace SRPG
{
    using System;

    public class Json_DropInfo
    {
        public string iname;
        public int num;
        public string iname_origin;
        public string type;
        public int is_new;
        public int rare;
        public string get_unit;
        public int is_gift;

        public Json_DropInfo()
        {
            this.iname = string.Empty;
            this.iname_origin = string.Empty;
            this.type = string.Empty;
            this.rare = -1;
            this.get_unit = string.Empty;
            base..ctor();
            return;
        }
    }
}

