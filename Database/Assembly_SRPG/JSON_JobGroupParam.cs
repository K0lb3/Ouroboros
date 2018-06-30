namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_JobGroupParam
    {
        public string iname;
        public string name;
        public string[] jobs;

        public JSON_JobGroupParam()
        {
            base..ctor();
            return;
        }
    }
}

