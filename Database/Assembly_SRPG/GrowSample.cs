namespace SRPG
{
    using System;

    public class GrowSample
    {
        public OInt lv;
        public OInt scale;
        public BaseStatus status;

        public GrowSample()
        {
            this.lv = 0;
            this.scale = 0;
            this.status = new BaseStatus();
            base..ctor();
            return;
        }
    }
}

