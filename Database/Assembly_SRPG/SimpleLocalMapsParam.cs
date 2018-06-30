namespace SRPG
{
    using System;

    public class SimpleLocalMapsParam
    {
        public string iname;
        public string[] droplist;

        public SimpleLocalMapsParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_SimpleLocalMapsParam json)
        {
            this.iname = json.iname;
            this.droplist = json.droplist;
            return 1;
        }
    }
}

