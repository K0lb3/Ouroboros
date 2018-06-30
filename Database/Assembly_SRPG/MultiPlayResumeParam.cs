namespace SRPG
{
    using System;

    [Serializable]
    public class MultiPlayResumeParam
    {
        public MultiPlayResumeUnitData[] unit;
        public MultiPlayGimmickEventParam[] gimmick;
        public MultiPlayTrickParam[] trick;
        public uint[] rndseed;
        public uint[] dmgrndseed;
        public uint damageseed;
        public uint seed;
        public int unitcastindex;
        public int unitstartcount;
        public int treasurecount;
        public uint versusturn;
        public int resumeID;
        public int[] otherresume;
        public bool[] scr_ev_trg;
        public int ctm;
        public int ctt;
        public WeatherInfo wti;

        public MultiPlayResumeParam()
        {
            this.wti = new WeatherInfo();
            base..ctor();
            return;
        }

        [Serializable]
        public class WeatherInfo
        {
            public string wid;
            public int mun;
            public int rnk;
            public int rcp;
            public int ccl;

            public WeatherInfo()
            {
                base..ctor();
                return;
            }
        }
    }
}

