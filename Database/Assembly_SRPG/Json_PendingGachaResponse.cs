namespace SRPG
{
    using System;

    public class Json_PendingGachaResponse
    {
        public Json_DropInfo[] add;
        public Json_DropInfo[] add_mail;
        public Json_PendingGachaParam gacha;
        public int rest;

        public Json_PendingGachaResponse()
        {
            base..ctor();
            return;
        }
    }
}

