namespace SRPG
{
    using System;

    public class Json_MailOption
    {
        public int totalCount;
        public int totalPage;
        public int currentPage;
        public byte hasNext;
        public byte hasPrev;

        public Json_MailOption()
        {
            base..ctor();
            return;
        }
    }
}

