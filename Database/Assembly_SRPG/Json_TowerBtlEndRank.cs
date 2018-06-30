namespace SRPG
{
    using System;

    public class Json_TowerBtlEndRank
    {
        public int turn_num;
        public int died_num;
        public int retire_num;
        public int recovery_num;
        public int spd_rank;
        public int tec_rank;
        public int spd_score;
        public int tec_score;
        public int ret_score;
        public int rcv_score;

        public Json_TowerBtlEndRank()
        {
            base..ctor();
            return;
        }
    }
}

