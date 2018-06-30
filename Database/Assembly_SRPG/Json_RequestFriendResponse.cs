namespace SRPG
{
    using System;

    public class Json_RequestFriendResponse
    {
        public Json_PlayerData player;
        public Json_RequestFriendErrors errors;

        public Json_RequestFriendResponse()
        {
            base..ctor();
            return;
        }
    }
}

