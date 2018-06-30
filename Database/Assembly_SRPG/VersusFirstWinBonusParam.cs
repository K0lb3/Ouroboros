namespace SRPG
{
    using System;

    public class VersusFirstWinBonusParam
    {
        public int id;
        public DateTime begin_at;
        public DateTime end_at;
        public VersusWinBonusRewardParam[] rewards;

        public VersusFirstWinBonusParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusFirstWinBonus json)
        {
            int num;
            int num2;
            Exception exception;
            bool flag;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.id = json.id;
            if (json.rewards == null)
            {
                goto Label_00C0;
            }
            num = (int) json.rewards.Length;
            this.rewards = new VersusWinBonusRewardParam[num];
            if (this.rewards == null)
            {
                goto Label_00C0;
            }
            num2 = 0;
            goto Label_00B9;
        Label_0046:
            this.rewards[num2] = new VersusWinBonusRewardParam();
            this.rewards[num2].type = (int) Enum.ToObject(typeof(VERSUS_REWARD_TYPE), json.rewards[num2].item_type);
            this.rewards[num2].iname = json.rewards[num2].item_iname;
            this.rewards[num2].num = json.rewards[num2].item_num;
            num2 += 1;
        Label_00B9:
            if (num2 < num)
            {
                goto Label_0046;
            }
        Label_00C0:
            try
            {
                if (string.IsNullOrEmpty(json.begin_at) != null)
                {
                    goto Label_00E1;
                }
                this.begin_at = DateTime.Parse(json.begin_at);
            Label_00E1:
                if (string.IsNullOrEmpty(json.end_at) != null)
                {
                    goto Label_0102;
                }
                this.end_at = DateTime.Parse(json.end_at);
            Label_0102:
                goto Label_011F;
            }
            catch (Exception exception1)
            {
            Label_0107:
                exception = exception1;
                DebugUtility.LogError(exception.Message);
                flag = 0;
                goto Label_0121;
            }
        Label_011F:
            return 1;
        Label_0121:
            return flag;
        }
    }
}

