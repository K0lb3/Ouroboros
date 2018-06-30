namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_WeatherParam
    {
        public string iname;
        public string name;
        public string expr;
        public string icon;
        public string effect;
        public string[] buff_ids;
        public string[] cond_ids;

        public JSON_WeatherParam()
        {
            base..ctor();
            return;
        }
    }
}

