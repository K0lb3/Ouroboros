namespace SRPG
{
    using System;

    public class Json_FriendArray
    {
        public Json_Friend[] friends;

        public Json_FriendArray()
        {
            this.friends = new Json_Friend[0];
            base..ctor();
            return;
        }
    }
}

