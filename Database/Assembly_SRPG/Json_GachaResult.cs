namespace SRPG
{
    using System;

    public class Json_GachaResult
    {
        public Json_DropInfo[] add;
        public Json_DropInfo[] add_mail;
        public Json_GachaReceipt receipt;
        public Json_PlayerData player;
        public Json_Item[] items;
        public Json_Unit[] units;
        public Json_Mail[] mails;
        public Json_Artifact[] artifacts;
        public int is_pending;
        public int rest;

        public Json_GachaResult()
        {
            this.is_pending = -1;
            this.rest = -1;
            base..ctor();
            return;
        }
    }
}

