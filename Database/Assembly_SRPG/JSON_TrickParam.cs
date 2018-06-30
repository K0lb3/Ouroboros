namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TrickParam
    {
        public string iname;
        public string name;
        public string expr;
        public int dmg_type;
        public int dmg_val;
        public int calc;
        public int elem;
        public int atk_det;
        public string buff;
        public string cond;
        public int kb_rate;
        public int kb_val;
        public int target;
        public int visual;
        public int count;
        public int clock;
        public int is_no_ow;
        public string marker;
        public string effect;
        public int eff_target;
        public int eff_shape;
        public int eff_scope;
        public int eff_height;

        public JSON_TrickParam()
        {
            base..ctor();
            return;
        }
    }
}

