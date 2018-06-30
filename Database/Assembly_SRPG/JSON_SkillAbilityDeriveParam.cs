namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_SkillAbilityDeriveParam
    {
        public string iname;
        public int trig_type_1;
        public string trig_iname_1;
        public int trig_type_2;
        public string trig_iname_2;
        public int trig_type_3;
        public string trig_iname_3;
        public string[] base_abils;
        public string[] derive_abils;
        public string[] base_skills;
        public string[] derive_skills;

        public JSON_SkillAbilityDeriveParam()
        {
            base..ctor();
            return;
        }
    }
}

