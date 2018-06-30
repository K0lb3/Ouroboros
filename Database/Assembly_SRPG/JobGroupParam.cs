namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;

    public class JobGroupParam
    {
        public string iname;
        public string name;
        public string[] jobs;

        public JobGroupParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_JobGroupParam json)
        {
            this.iname = json.iname;
            this.jobs = json.jobs;
            this.name = json.name;
            return 1;
        }

        public bool IsInGroup(string job_iname)
        {
            <IsInGroup>c__AnonStorey2BF storeybf;
            storeybf = new <IsInGroup>c__AnonStorey2BF();
            storeybf.job_iname = job_iname;
            return ((Array.FindIndex<string>(this.jobs, new Predicate<string>(storeybf.<>m__235)) < 0) == 0);
        }

        [CompilerGenerated]
        private sealed class <IsInGroup>c__AnonStorey2BF
        {
            internal string job_iname;

            public <IsInGroup>c__AnonStorey2BF()
            {
                base..ctor();
                return;
            }

            internal bool <>m__235(string j)
            {
                return (j == this.job_iname);
            }
        }
    }
}

