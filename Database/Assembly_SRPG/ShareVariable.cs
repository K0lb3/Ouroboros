namespace SRPG
{
    using GR;
    using System;

    public class ShareVariable : Singleton<ShareVariable>
    {
        public ShareString str;

        public ShareVariable()
        {
            this.str = new ShareString();
            base..ctor();
            return;
        }
    }
}

