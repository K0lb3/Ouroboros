namespace SRPG
{
    using System;

    [Serializable]
    public class Json_Unit
    {
        public long iid;
        public string iname;
        public int rare;
        public int plus;
        public int lv;
        public int exp;
        public int fav;
        public Json_MasterAbility abil;
        public Json_CollaboAbility c_abil;
        public Json_Job[] jobs;
        public Json_UnitSelectable select;
        public string[] quest_clear_unlocks;
        public int elem;
        public JSON_ConceptCard concept_card;
        public Json_Tobira[] doors;
        public Json_Ability[] door_abils;

        public Json_Unit()
        {
            base..ctor();
            return;
        }
    }
}

