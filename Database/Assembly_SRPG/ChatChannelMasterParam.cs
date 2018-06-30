namespace SRPG
{
    using System;

    public class ChatChannelMasterParam
    {
        public int id;
        public byte category_id;
        public string name;

        public ChatChannelMasterParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(Json_ChatChannelMasterParam json)
        {
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidCastException();
        Label_000C:
            this.id = json.fields.id;
            this.category_id = json.fields.category_id;
            this.name = json.fields.name;
            return;
        }
    }
}

