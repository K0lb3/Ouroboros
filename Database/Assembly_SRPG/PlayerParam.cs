namespace SRPG
{
    using System;

    public class PlayerParam
    {
        public OInt pt;
        public OInt ucap;
        public OInt icap;
        public OInt ecap;
        public OInt fcap;

        public PlayerParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_PlayerParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.pt = json.pt;
            this.ucap = json.ucap;
            this.icap = json.icap;
            this.ecap = json.ecap;
            this.fcap = json.fcap;
            return 1;
        }
    }
}

