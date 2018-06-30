namespace SRPG
{
    using System;

    public class UnlockParam
    {
        public string iname;
        public UnlockTargets UnlockTarget;
        public int PlayerLevel;
        public int VipRank;

        public UnlockParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_UnlockParam json)
        {
            bool flag;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
        Label_0014:
            try
            {
                this.UnlockTarget = (int) Enum.Parse(typeof(UnlockTargets), json.iname);
                goto Label_0046;
            }
            catch (Exception)
            {
            Label_0039:
                flag = 0;
                goto Label_0060;
            }
        Label_0046:
            this.PlayerLevel = json.lv;
            this.VipRank = json.vip;
            return 1;
        Label_0060:
            return flag;
        }
    }
}

