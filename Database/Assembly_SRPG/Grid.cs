namespace SRPG
{
    using System;

    [Serializable]
    public class Grid
    {
        public int x;
        public int y;
        public int height;
        public int cost;
        public byte step;
        public string tile;
        public GeoParam geo;

        public Grid()
        {
            this.cost = 1;
            this.step = 0x7f;
            base..ctor();
            return;
        }
    }
}

