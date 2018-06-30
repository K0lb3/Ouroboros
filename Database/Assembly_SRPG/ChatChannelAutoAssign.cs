namespace SRPG
{
    using System;

    public class ChatChannelAutoAssign
    {
        public int channel;

        public ChatChannelAutoAssign()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_ChatChannelAutoAssign json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.channel = json.channel;
            return;
        }
    }
}

