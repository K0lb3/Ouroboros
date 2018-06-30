namespace SRPG
{
    using System;
    using UnityEngine;

    public class TimeRecoveryValue
    {
        public OInt val;
        public OInt valMax;
        public OInt valRecover;
        public OLong interval;
        public OLong at;
        private float lastUpdateTime;

        public TimeRecoveryValue()
        {
            this.lastUpdateTime = -1f;
            base..ctor();
            return;
        }

        public long GetNextRecoverySec()
        {
            long num;
            long num2;
            int num3;
            if (this.val < this.valMax)
            {
                goto Label_001E;
            }
            return 0L;
        Label_001E:
            num2 = Network.GetServerTime() - this.at;
            num3 = (int) (num2 / this.interval);
            num2 -= this.interval * ((long) num3);
            return (this.interval - num2);
        }

        public void SubValue(int subval)
        {
            this.at = Network.GetServerTime();
            this.val = Math.Max(this.val - subval, 0);
            return;
        }

        public void Update()
        {
            int num;
            long num2;
            long num3;
            long num4;
            long num5;
            if (this.val < this.valMax)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            if (this.lastUpdateTime != Time.get_realtimeSinceStartup())
            {
                goto Label_002D;
            }
            return;
        Label_002D:
            this.lastUpdateTime = Time.get_realtimeSinceStartup();
            num = 0;
            num3 = Network.GetServerTime() - this.at;
            num4 = this.at;
            num5 = this.interval;
            num = (int) (num3 / num5);
            num4 += ((long) num) * num5;
            this.at = num4;
            this.val = Math.Min(this.val + num, this.valMax);
            return;
        }
    }
}

