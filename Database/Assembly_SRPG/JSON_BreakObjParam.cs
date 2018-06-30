namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_BreakObjParam
    {
        public string iname;
        public string name;
        public string expr;
        public string unit_id;
        public int clash_type;
        public int ai_type;
        public int side_type;
        public int ray_type;
        public int is_ui;
        public string rest_hps;
        public int clock;
        public int appear_dir;

        public JSON_BreakObjParam()
        {
            base..ctor();
            return;
        }
    }
}

