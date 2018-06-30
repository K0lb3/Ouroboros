namespace SRPG
{
    using GR;
    using System;

    public class ChatChannel
    {
        public ChatChannelParam[] channels;

        public ChatChannel()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_ChatChannel json)
        {
            ChatChannelMasterParam[] paramArray;
            int num;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (json.channels == null)
            {
                goto Label_00B4;
            }
            this.channels = new ChatChannelParam[(int) json.channels.Length];
            paramArray = MonoSingleton<GameManager>.Instance.GetChatChannelMaster();
            num = 0;
            goto Label_00A6;
        Label_0037:
            this.channels[num] = json.channels[num];
            if (((int) paramArray.Length) < this.channels[num].id)
            {
                goto Label_00A2;
            }
            this.channels[num].category_id = paramArray[this.channels[num].id - 1].category_id;
            this.channels[num].name = paramArray[this.channels[num].id - 1].name;
        Label_00A2:
            num += 1;
        Label_00A6:
            if (num < ((int) json.channels.Length))
            {
                goto Label_0037;
            }
        Label_00B4:
            return;
        }
    }
}

