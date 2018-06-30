namespace SRPG
{
    using System;

    public class MultiFuid
    {
        public string fuid;
        public string status;

        public MultiFuid()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(Json_MultiFuids json)
        {
            this.fuid = json.fuid;
            this.status = json.status;
            return 1;
        }
    }
}

