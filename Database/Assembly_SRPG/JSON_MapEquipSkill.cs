namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_MapEquipSkill
    {
        public string iname;
        public int rate;
        public JSON_SkillLockCondition cond;
        public int check_cnt;

        public JSON_MapEquipSkill()
        {
            base..ctor();
            return;
        }
    }
}

