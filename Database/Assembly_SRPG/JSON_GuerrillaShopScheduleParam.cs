namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_GuerrillaShopScheduleParam
    {
        public int id;
        public string begin_at;
        public string end_at;
        public int accum_ap;
        public string open_time;
        public string cool_time;
        public JSON_GuerrillaShopScheduleAdventParam[] advent;

        public JSON_GuerrillaShopScheduleParam()
        {
            base..ctor();
            return;
        }
    }
}

