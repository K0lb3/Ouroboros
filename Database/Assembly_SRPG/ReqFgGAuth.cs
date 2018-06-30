namespace SRPG
{
    using System;

    public class ReqFgGAuth : WebAPI
    {
        public ReqFgGAuth(Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "achieve/auth";
            base.callback = response;
            return;
        }

        public enum eAuthStatus
        {
            None,
            Disable,
            NotSynchronized,
            Synchronized
        }
    }
}

