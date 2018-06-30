namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_AwardParam
    {
        public int id;
        public string iname;
        public string name;
        public string expr;
        public string icon;
        public string bg;
        public string txt_img;
        public string start_at;
        public int grade;
        public int tab;

        public JSON_AwardParam()
        {
            base..ctor();
            return;
        }
    }
}

