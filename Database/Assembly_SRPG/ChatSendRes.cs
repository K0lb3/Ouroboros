namespace SRPG
{
    using System;

    public class ChatSendRes
    {
        public byte is_success;

        public ChatSendRes()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_ChatSendRes json)
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

