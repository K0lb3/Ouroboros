namespace SRPG
{
    using System;

    public class PaidGacha
    {
        public int num;
        public long at;

        public PaidGacha()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(Json_PaidGacha json)
        {
            this.num = json.num;
            this.at = json.at;
            return 1;
        }
    }
}

