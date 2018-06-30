namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_BannerParam
    {
        public string iname;
        public string type;
        public string sval;
        public string banr;
        public string banr_sprite;
        public string begin_at;
        public string end_at;
        public int priority;
        public string message;
        public int is_not_home;

        public JSON_BannerParam()
        {
            base..ctor();
            return;
        }
    }
}

