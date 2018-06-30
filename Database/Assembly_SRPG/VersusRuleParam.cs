namespace SRPG
{
    using System;

    public class VersusRuleParam
    {
        public static readonly int THREE_ON_THREE;
        public static readonly int FIVE_ON_FIVE;
        public int id;
        public int coin;
        public int coinrate;
        public DateTime begin_at;
        public DateTime end_at;
        public VS_MODE mode;

        static VersusRuleParam()
        {
            THREE_ON_THREE = 3;
            FIVE_ON_FIVE = 5;
            return;
        }

        public VersusRuleParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusRule json)
        {
            Exception exception;
            bool flag;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.id = json.id;
            this.mode = json.vsmode;
            this.coin = json.getcoin;
            this.coinrate = (json.rate <= 0) ? 1 : json.rate;
        Label_004A:
            try
            {
                if (string.IsNullOrEmpty(json.begin_at) != null)
                {
                    goto Label_006B;
                }
                this.begin_at = DateTime.Parse(json.begin_at);
            Label_006B:
                if (string.IsNullOrEmpty(json.end_at) != null)
                {
                    goto Label_008C;
                }
                this.end_at = DateTime.Parse(json.end_at);
            Label_008C:
                goto Label_00A9;
            }
            catch (Exception exception1)
            {
            Label_0091:
                exception = exception1;
                DebugUtility.LogError(exception.Message);
                flag = 0;
                goto Label_00AB;
            }
        Label_00A9:
            return 1;
        Label_00AB:
            return flag;
        }
    }
}

