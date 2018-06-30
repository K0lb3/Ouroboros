namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_FriendPresentItemParam
    {
        public string iname;
        public string name;
        public string expr;
        public string item;
        public int num;
        public int zeny;
        public string begin_at;
        public string end_at;

        public JSON_FriendPresentItemParam()
        {
            base..ctor();
            return;
        }
    }
}

