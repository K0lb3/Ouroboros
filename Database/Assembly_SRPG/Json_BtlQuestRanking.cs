namespace SRPG
{
    using System;

    public class Json_BtlQuestRanking
    {
        public int is_new_score;
        public int is_join_reward;
        public int rank;

        public Json_BtlQuestRanking()
        {
            base..ctor();
            return;
        }

        public bool IsNewScore
        {
            get
            {
                return (this.is_new_score == 1);
            }
        }

        public bool IsJoinReward
        {
            get
            {
                return (this.is_join_reward == 1);
            }
        }
    }
}

