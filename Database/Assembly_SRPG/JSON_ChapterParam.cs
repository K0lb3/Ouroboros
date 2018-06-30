namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ChapterParam
    {
        public string iname;
        public string name;
        public string expr;
        public string world;
        public long start;
        public long end;
        public string parent;
        public int hide;
        public string chap;
        public string banr;
        public string item;
        public string keyitem1;
        public int keynum1;
        public string keyitem2;
        public int keynum2;
        public string keyitem3;
        public int keynum3;
        public long keytime;
        public string hurl;

        public JSON_ChapterParam()
        {
            base..ctor();
            return;
        }
    }
}

