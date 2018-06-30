namespace SRPG
{
    using System;

    public class Json_GachaExtParam
    {
        public Json_GachaStepParam step;
        public Json_GachaLimitParam limit;
        public Json_GachaLimitCntParam limit_cnt;
        public long next_reset_time;
        public Json_GachaRedrawParam redraw;

        public Json_GachaExtParam()
        {
            base..ctor();
            return;
        }
    }
}

