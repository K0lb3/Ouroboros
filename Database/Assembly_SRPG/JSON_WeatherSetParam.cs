namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_WeatherSetParam
    {
        public string iname;
        public string name;
        public string[] st_wth;
        public int[] st_rate;
        public int ch_cl_min;
        public int ch_cl_max;
        public string[] ch_wth;
        public int[] ch_rate;

        public JSON_WeatherSetParam()
        {
            base..ctor();
            return;
        }
    }
}

