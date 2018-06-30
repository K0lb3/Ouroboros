namespace SRPG
{
    using System;

    public class EquipSkillSetting
    {
        public OString iname;
        public OInt rate;
        public SkillLockCondition cond;
        public OInt check_cnt;

        public EquipSkillSetting()
        {
            base..ctor();
            return;
        }
    }
}

