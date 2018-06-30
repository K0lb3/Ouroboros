namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_LoginInfoParam
    {
        public string iname;
        public string path;
        public int scene;
        public string begin_at;
        public string end_at;
        public int conditions;
        public int conditions_value;

        public JSON_LoginInfoParam()
        {
            base..ctor();
            return;
        }
    }
}

