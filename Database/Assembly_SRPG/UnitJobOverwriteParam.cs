namespace SRPG
{
    using System;

    public class UnitJobOverwriteParam
    {
        private StatusParam status;
        public string mUnitIname;
        public string mJobIname;
        public int mAvoid;
        public int mInimp;

        public UnitJobOverwriteParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_UnitJobOverwriteParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mUnitIname = json.unit_iname;
            this.mJobIname = json.job_iname;
            this.mAvoid = json.avoid;
            this.mInimp = json.inimp;
            this.status = new StatusParam();
            this.status.hp = json.hp;
            this.status.mp = json.mp;
            this.status.atk = json.atk;
            this.status.def = json.def;
            this.status.mag = json.mag;
            this.status.mnd = json.mnd;
            this.status.dex = json.dex;
            this.status.spd = json.spd;
            this.status.cri = json.cri;
            this.status.luk = json.luk;
            return 1;
        }

        public StatusParam mStatus
        {
            get
            {
                return this.status;
            }
        }
    }
}

