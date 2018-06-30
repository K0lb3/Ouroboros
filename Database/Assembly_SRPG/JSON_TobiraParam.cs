namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TobiraParam
    {
        public string unit_iname;
        public int enable;
        public int category;
        public string recipe_id;
        public string skill_iname;
        public JSON_TobiraLearnAbilityParam[] learn_abils;
        public string overwrite_ls_iname;
        public int priority;

        public JSON_TobiraParam()
        {
            base..ctor();
            return;
        }
    }
}

