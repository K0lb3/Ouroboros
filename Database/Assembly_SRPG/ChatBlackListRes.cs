namespace SRPG
{
    using System;

    public class ChatBlackListRes
    {
        public byte is_success;

        public ChatBlackListRes()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_ChatBlackListRes json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.is_success = json.is_success;
            return;
        }

        public bool IsSuccess
        {
            get
            {
                return (this.is_success == 1);
            }
        }
    }
}

