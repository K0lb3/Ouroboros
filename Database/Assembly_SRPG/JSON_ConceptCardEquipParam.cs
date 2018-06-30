namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ConceptCardEquipParam
    {
        public string cnds_iname;
        public string card_skill;
        public string add_card_skill_buff_awake;
        public string add_card_skill_buff_lvmax;
        public string abil_iname;
        public string abil_iname_lvmax;
        public string statusup_skill;
        public string skin;

        public JSON_ConceptCardEquipParam()
        {
            base..ctor();
            return;
        }
    }
}

