namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_MapEquipAbility
    {
        public string iname;
        public int rank;
        public JSON_MapEquipSkill[] skills;

        public JSON_MapEquipAbility()
        {
            base..ctor();
            return;
        }
    }
}

