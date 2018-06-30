namespace SRPG
{
    using System;

    [Serializable]
    public class Json_Job
    {
        public long iid;
        public string iname;
        public int rank;
        public string cur_skin;
        public Json_Equip[] equips;
        public Json_Ability[] abils;
        public Json_Artifact[] artis;
        public Json_JobSelectable select;
        public string unit_image;

        public Json_Job()
        {
            base..ctor();
            return;
        }
    }
}

