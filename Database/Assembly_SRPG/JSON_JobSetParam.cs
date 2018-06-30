namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_JobSetParam
    {
        public string iname;
        public string job;
        public int lrare;
        public int lplus;
        public string ljob1;
        public int llv1;
        public string ljob2;
        public int llv2;
        public string ljob3;
        public int llv3;
        public string cjob;
        public string target_unit;
        public int is_final_job;

        public JSON_JobSetParam()
        {
            base..ctor();
            return;
        }
    }
}

