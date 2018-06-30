namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_MapTrick
    {
        public string id;
        public int gx;
        public int gy;
        public string tag;

        public JSON_MapTrick()
        {
            base..ctor();
            return;
        }

        public void CopyTo(JSON_MapTrick dst)
        {
            dst.id = this.id;
            dst.gx = this.gx;
            dst.gy = this.gy;
            dst.tag = this.tag;
            return;
        }
    }
}

