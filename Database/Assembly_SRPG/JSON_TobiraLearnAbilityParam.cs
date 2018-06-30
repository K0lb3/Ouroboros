namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TobiraLearnAbilityParam
    {
        public string abil_iname;
        public int learn_lv;
        public int add_type;
        public string abil_overwrite;

        public JSON_TobiraLearnAbilityParam()
        {
            base..ctor();
            return;
        }
    }
}

