namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_CustomTargetParam
    {
        public string iname;
        public string name;
        public string[] units;
        public string[] jobs;
        public string[] unit_groups;
        public string[] job_groups;
        public string first_job;
        public string second_job;
        public string third_job;
        public int sex;
        public int birth_id;
        public int fire;
        public int water;
        public int wind;
        public int thunder;
        public int shine;
        public int dark;

        public JSON_CustomTargetParam()
        {
            base..ctor();
            return;
        }
    }
}

