namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_QuestPartyParam
    {
        public string iname;
        public int type_1;
        public int type_2;
        public int type_3;
        public int type_4;
        public int support_type;
        public int subtype_1;
        public int subtype_2;
        public string unit_1;
        public string unit_2;
        public string unit_3;
        public string unit_4;
        public string subunit_1;
        public string subunit_2;
        public int l_npc_rare;

        public JSON_QuestPartyParam()
        {
            base..ctor();
            return;
        }
    }
}

