namespace SRPG
{
    using System;

    public class ChatBlackListParam
    {
        public string name;
        public string uid;
        public long blocked_at;
        public long lastlogin;
        public string icon;
        public string skin_iname;
        public string job_iname;
        public int exp;
        public Json_Unit unit;

        public ChatBlackListParam()
        {
            base..ctor();
            return;
        }
    }
}

