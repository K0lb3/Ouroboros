namespace SRPG
{
    using System;

    public class JSON_Map
    {
        public int w;
        public int h;
        public JSON_MapGrid[] grid;
        public string description;

        public JSON_Map()
        {
            base..ctor();
            return;
        }
    }
}

