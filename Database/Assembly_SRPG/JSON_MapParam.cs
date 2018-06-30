namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_MapParam
    {
        public string scn;
        public string set;
        public string btl;
        public string bgm;
        public string ev;

        public JSON_MapParam()
        {
            base..ctor();
            return;
        }
    }
}

