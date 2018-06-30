namespace SRPG
{
    using System;

    public class JSON_ChatLog
    {
        public ChatLogParam[] messages;
        public Player player;

        public JSON_ChatLog()
        {
            base..ctor();
            return;
        }
    }
}

