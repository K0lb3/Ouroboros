namespace SRPG
{
    using System;

    public class ChargeCheckData
    {
        public int Age;
        public string[] AcceptIds;
        public string[] RejectIds;

        public ChargeCheckData()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ChargeCheckResponse json)
        {
            string[] strArray;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.Age = json.age;
            this.AcceptIds = json.accept_ids;
            this.RejectIds = json.reject_ids;
            if (this.RejectIds != null)
            {
                goto Label_0045;
            }
            strArray = new string[0];
            this.RejectIds = strArray;
        Label_0045:
            return 1;
        }
    }
}

