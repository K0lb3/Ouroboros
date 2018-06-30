namespace SRPG
{
    using System;

    public class GachaReceiptData
    {
        public string iname;
        public string type;
        public int val;

        public GachaReceiptData()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(Json_GachaReceipt json)
        {
            this.Init();
            if (json != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            this.iname = json.iname;
            this.type = json.type;
            this.val = json.val;
            return 1;
        }

        public void Init()
        {
            this.iname = null;
            this.type = null;
            this.val = 0;
            return;
        }
    }
}

