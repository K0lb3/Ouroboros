namespace SRPG
{
    using System;

    public class Json_Mail
    {
        public long mid;
        public string msg;
        public Json_Gift[] gifts;
        public int read;
        public long post_at;
        public int period;

        public Json_Mail()
        {
            base..ctor();
            return;
        }
    }
}

