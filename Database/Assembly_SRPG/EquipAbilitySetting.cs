namespace SRPG
{
    using System;

    public class EquipAbilitySetting
    {
        public OString iname;
        public OInt rank;
        public EquipSkillSetting[] skills;

        public EquipAbilitySetting()
        {
            base..ctor();
            return;
        }
    }
}

