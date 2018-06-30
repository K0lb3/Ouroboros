namespace SRPG
{
    using System;

    public class Json_MailRead
    {
        public Json_PlayerData player;
        public Json_Unit[] units;
        public Json_Item[] items;
        public Json_Friend[] friends;
        public Json_Artifact[] artifacts;
        public Json_Mail[] processed;

        public Json_MailRead()
        {
            base..ctor();
            return;
        }
    }
}

