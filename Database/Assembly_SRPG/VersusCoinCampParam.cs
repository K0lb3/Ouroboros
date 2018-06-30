namespace SRPG
{
    using System;

    public class VersusCoinCampParam
    {
        public string iname;
        public DateTime begin_at;
        public DateTime end_at;
        public int coinrate;

        public VersusCoinCampParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_VersusCoinCampParam json)
        {
            Exception exception;
            bool flag;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.coinrate = (json.rate <= 0) ? 1 : json.rate;
        Label_0032:
            try
            {
                if (string.IsNullOrEmpty(json.begin_at) != null)
                {
                    goto Label_0053;
                }
                this.begin_at = DateTime.Parse(json.begin_at);
            Label_0053:
                if (string.IsNullOrEmpty(json.end_at) != null)
                {
                    goto Label_0074;
                }
                this.end_at = DateTime.Parse(json.end_at);
            Label_0074:
                goto Label_0091;
            }
            catch (Exception exception1)
            {
            Label_0079:
                exception = exception1;
                DebugUtility.LogError(exception.Message);
                flag = 0;
                goto Label_0093;
            }
        Label_0091:
            return 1;
        Label_0093:
            return flag;
        }
    }
}

