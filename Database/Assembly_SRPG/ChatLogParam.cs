namespace SRPG
{
    using System;

    public class ChatLogParam
    {
        public long id;
        public byte message_type;
        public string fuid;
        public string uid;
        public string icon;
        public string skin_iname;
        public string job_iname;
        public string message;
        public int stamp_id;
        public string name;
        public long posted_at;

        public ChatLogParam()
        {
            base..ctor();
            return;
        }
    }
}

