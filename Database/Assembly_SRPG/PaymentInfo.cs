namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;

    public class PaymentInfo
    {
        public string productId;
        public long at;
        private int _numMonghly;

        public PaymentInfo()
        {
            base..ctor();
            return;
        }

        public PaymentInfo(string productId_, int numMonthly_)
        {
            base..ctor();
            this.productId = productId_;
            this._numMonghly = numMonthly_;
            this.at = Network.GetServerTime();
            return;
        }

        public void AddNum(int num)
        {
            this._numMonghly = this.numMonthly + num;
            this.at = Network.GetServerTime();
            return;
        }

        public bool Deserialize(Json_PaymentInfo json)
        {
            this.productId = json.pid;
            this._numMonghly = json.num_m;
            this.at = json.at;
            return 1;
        }

        public int numMonthly
        {
            get
            {
                DateTime time;
                DateTime time2;
                time = TimeManager.FromUnixTime(Network.GetServerTime());
                time2 = TimeManager.FromUnixTime(this.at);
                if (&time.Year > &time2.Year)
                {
                    goto Label_003D;
                }
                if (&time.Month <= &time2.Month)
                {
                    goto Label_0044;
                }
            Label_003D:
                this._numMonghly = 0;
            Label_0044:
                return this._numMonghly;
            }
        }
    }
}

