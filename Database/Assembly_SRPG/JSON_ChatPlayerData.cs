namespace SRPG
{
    using System;

    public class JSON_ChatPlayerData
    {
        public string name;
        public int exp;
        public long lastlogin;
        public byte is_friend;
        public byte is_favorite;
        public string fuid;
        public Json_Unit unit;
        public string award;

        public JSON_ChatPlayerData()
        {
            base..ctor();
            return;
        }
    }
}

