namespace SRPG
{
    using System;

    public class VersusCoinParam
    {
        public string iname;
        public string coin_iname;
        public int win_cnt;
        public int lose_cnt;
        public int draw_cnt;

        public VersusCoinParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_VersusCoin json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.iname = json.iname;
            this.coin_iname = json.coin_iname;
            this.win_cnt = json.win_cnt;
            this.lose_cnt = json.lose_cnt;
            this.draw_cnt = json.draw_cnt;
            return;
        }
    }
}

