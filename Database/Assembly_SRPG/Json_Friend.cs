namespace SRPG
{
    using System;

    [Serializable]
    public class Json_Friend
    {
        public string uid;
        public string fuid;
        public string name;
        public string type;
        public int lv;
        public long lastlogin;
        public int is_multi_push;
        public string multi_comment;
        public Json_Unit unit;
        public string created_at;
        public int is_favorite;
        public string award;
        public string wish;
        public string status;

        public Json_Friend()
        {
            base..ctor();
            return;
        }
    }
}

