namespace SRPG
{
    using System;

    public class VersusStreakWinBonusParam
    {
        public int id;
        public int wincnt;
        public VersusWinBonusRewardParam[] rewards;

        public VersusStreakWinBonusParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusStreakWinBonus json)
        {
            int num;
            int num2;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.id = json.id;
            this.wincnt = json.wincnt;
            if (json.rewards == null)
            {
                goto Label_00CC;
            }
            num = (int) json.rewards.Length;
            this.rewards = new VersusWinBonusRewardParam[num];
            if (this.rewards == null)
            {
                goto Label_00CC;
            }
            num2 = 0;
            goto Label_00C5;
        Label_0052:
            this.rewards[num2] = new VersusWinBonusRewardParam();
            this.rewards[num2].type = (int) Enum.ToObject(typeof(VERSUS_REWARD_TYPE), json.rewards[num2].item_type);
            this.rewards[num2].iname = json.rewards[num2].item_iname;
            this.rewards[num2].num = json.rewards[num2].item_num;
            num2 += 1;
        Label_00C5:
            if (num2 < num)
            {
                goto Label_0052;
            }
        Label_00CC:
            return 1;
        }
    }
}

