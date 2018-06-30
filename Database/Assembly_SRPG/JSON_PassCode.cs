namespace SRPG
{
    using System;

    internal class JSON_PassCode
    {
        public string passcode;
        public int expires_in;

        public JSON_PassCode()
        {
            this.passcode = string.Empty;
            base..ctor();
            return;
        }
    }
}

