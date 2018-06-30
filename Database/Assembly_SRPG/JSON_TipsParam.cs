namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TipsParam
    {
        public string iname;
        public int type;
        public int order;
        public string title;
        public string text;
        public string[] images;
        public int hide;
        public string cond_text;

        public JSON_TipsParam()
        {
            base..ctor();
            return;
        }
    }
}

