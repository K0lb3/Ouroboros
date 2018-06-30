namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_SectionParam
    {
        public string iname;
        public string name;
        public string expr;
        public long start;
        public long end;
        public int hide;
        public string home;
        public string unit;
        public string item;
        public string shop;
        public string inn;
        public string bar;
        public string bgm;
        public int story_part;
        public string release_key_quest;

        public JSON_SectionParam()
        {
            base..ctor();
            return;
        }
    }
}

