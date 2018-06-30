namespace SRPG
{
    using System;

    public class FreeGacha
    {
        public int num;
        public long at;

        public FreeGacha()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(Json_FreeGacha json)
        {
            this.num = json.num;
            this.at = json.at;
            return 1;
        }
    }
}

