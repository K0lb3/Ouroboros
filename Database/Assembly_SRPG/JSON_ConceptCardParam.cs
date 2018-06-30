namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ConceptCardParam
    {
        public string iname;
        public string name;
        public string expr;
        public int type;
        public string icon;
        public int rare;
        public int lvcap;
        public int sell;
        public int en_cost;
        public int en_exp;
        public int en_trust;
        public string trust_reward;
        public string first_get_unit;
        public JSON_ConceptCardEquipParam[] effects;
        public int not_sale;

        public JSON_ConceptCardParam()
        {
            base..ctor();
            return;
        }
    }
}

