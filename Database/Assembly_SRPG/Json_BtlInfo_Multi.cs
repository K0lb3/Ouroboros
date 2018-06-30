namespace SRPG
{
    using System;

    public class Json_BtlInfo_Multi : BattleCore.Json_BtlInfo
    {
        public string plid;
        public string seat;
        public int floor;

        public Json_BtlInfo_Multi()
        {
            base..ctor();
            return;
        }
    }
}

