namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_ConceptCardConditionsParam
    {
        public string iname;
        public int el_fire;
        public int el_watr;
        public int el_wind;
        public int el_thdr;
        public int el_lit;
        public int el_drk;
        public string un_group;
        public int units_cnds_type;
        public string job_group;
        public int jobs_cnds_type;
        public int sex;
        public int[] birth_id;

        public JSON_ConceptCardConditionsParam()
        {
            base..ctor();
            return;
        }
    }
}

