namespace SRPG
{
    using System;

    public class ConceptCardTrustRewardParam
    {
        public string iname;
        public ConceptCardTrustRewardItemParam[] rewards;

        public ConceptCardTrustRewardParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ConceptCardTrustRewardParam json)
        {
            int num;
            ConceptCardTrustRewardItemParam param;
            this.iname = json.iname;
            if (json.rewards == null)
            {
                goto Label_0067;
            }
            this.rewards = new ConceptCardTrustRewardItemParam[(int) json.rewards.Length];
            num = 0;
            goto Label_0059;
        Label_0031:
            param = new ConceptCardTrustRewardItemParam();
            if (param.Deserialize(json.rewards[num]) != null)
            {
                goto Label_004C;
            }
            return 0;
        Label_004C:
            this.rewards[num] = param;
            num += 1;
        Label_0059:
            if (num < ((int) json.rewards.Length))
            {
                goto Label_0031;
            }
        Label_0067:
            return 1;
        }
    }
}

