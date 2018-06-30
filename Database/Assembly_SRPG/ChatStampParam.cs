namespace SRPG
{
    using System;

    public class ChatStampParam
    {
        public int id;
        public string img_id;
        public string iname;
        public bool IsPrivate;

        public ChatStampParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ChatStampParam json)
        {
            if (json == null)
            {
                goto Label_0011;
            }
            if (json.fields != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return 0;
        Label_0013:
            this.id = json.fields.id;
            this.img_id = json.fields.img_id;
            this.iname = json.fields.iname;
            this.IsPrivate = json.fields.is_private == 1;
            return 1;
        }
    }
}

