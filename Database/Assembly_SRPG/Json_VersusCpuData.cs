namespace SRPG
{
    using System;

    public class Json_VersusCpuData
    {
        public string name;
        public int lv;
        public Json_Unit[] units;
        public int[] place;
        public int score;

        public Json_VersusCpuData()
        {
            base..ctor();
            return;
        }
    }
}

