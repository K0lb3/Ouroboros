namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TobiraCondsUnitParam
    {
        public string id;
        public string unit_iname;
        public int lv;
        public int awake_lv;
        public string job_iname;
        public int job_lv;
        public int category;
        public int tobira_lv;

        public JSON_TobiraCondsUnitParam()
        {
            base..ctor();
            return;
        }
    }
}

