namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ConceptCardTrustRewardParam
    {
        public string iname;
        public JSON_ConceptCardTrustRewardItemParam[] rewards;

        public JSON_ConceptCardTrustRewardParam()
        {
            base..ctor();
            return;
        }
    }
}

