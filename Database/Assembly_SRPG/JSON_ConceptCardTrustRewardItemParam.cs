namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ConceptCardTrustRewardItemParam
    {
        public int reward_type;
        public string reward_iname;
        public int reward_num;

        public JSON_ConceptCardTrustRewardItemParam()
        {
            base..ctor();
            return;
        }
    }
}

