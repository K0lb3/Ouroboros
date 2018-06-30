namespace SRPG
{
    using System;

    [Serializable]
    public class MultiPlayResumeShield
    {
        public string inm;
        public int nhp;
        public int mhp;
        public int ntu;
        public int mtu;
        public int drt;
        public int dvl;

        public MultiPlayResumeShield()
        {
            base..ctor();
            return;
        }
    }
}

