namespace SRPG
{
    using System;

    public class Json_GoogleReview
    {
        public Json_PlayerData player;
        public Json_Unit[] units;
        public Json_Item[] items;
        public Json_Mail[] mails;

        public Json_GoogleReview()
        {
            base..ctor();
            return;
        }
    }
}

