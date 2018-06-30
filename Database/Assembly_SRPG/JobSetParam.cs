namespace SRPG
{
    using GR;
    using System;

    public class JobSetParam
    {
        public string iname;
        public string job;
        public int lock_rarity;
        public int lock_awakelv;
        public JobLock[] lock_jobs;
        public string jobchange;
        public string target_unit;

        public JobSetParam()
        {
            base..ctor();
            return;
        }

        public bool ContainsJob(string iname)
        {
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if ((iname == this.job) == null)
            {
                goto Label_0020;
            }
            return 1;
        Label_0020:
            if (this.jobchange != null)
            {
                goto Label_002D;
            }
            return 0;
        Label_002D:
            if ((iname == this.jobchange) == null)
            {
                goto Label_0040;
            }
            return 1;
        Label_0040:
            return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetJobSetParam(this.jobchange).ContainsJob(iname);
        }

        public bool Deserialize(JSON_JobSetParam json)
        {
            int num;
            int num2;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.job = json.job;
            this.jobchange = json.cjob;
            this.target_unit = json.target_unit;
            this.lock_rarity = json.lrare;
            this.lock_awakelv = json.lplus;
            this.lock_jobs = null;
            num = 0;
            if (string.IsNullOrEmpty(json.ljob1) != null)
            {
                goto Label_006D;
            }
            num += 1;
        Label_006D:
            if (string.IsNullOrEmpty(json.ljob2) != null)
            {
                goto Label_0081;
            }
            num += 1;
        Label_0081:
            if (string.IsNullOrEmpty(json.ljob3) != null)
            {
                goto Label_0095;
            }
            num += 1;
        Label_0095:
            if (num <= 0)
            {
                goto Label_017F;
            }
            this.lock_jobs = new JobLock[num];
            num2 = 0;
            if (string.IsNullOrEmpty(json.ljob1) != null)
            {
                goto Label_00F1;
            }
            this.lock_jobs[num2] = new JobLock();
            this.lock_jobs[num2].iname = json.ljob1;
            this.lock_jobs[num2].lv = json.llv1;
            num2 += 1;
        Label_00F1:
            if (string.IsNullOrEmpty(json.ljob2) != null)
            {
                goto Label_0138;
            }
            this.lock_jobs[num2] = new JobLock();
            this.lock_jobs[num2].iname = json.ljob2;
            this.lock_jobs[num2].lv = json.llv2;
            num2 += 1;
        Label_0138:
            if (string.IsNullOrEmpty(json.ljob3) != null)
            {
                goto Label_017F;
            }
            this.lock_jobs[num2] = new JobLock();
            this.lock_jobs[num2].iname = json.ljob3;
            this.lock_jobs[num2].lv = json.llv3;
            num2 += 1;
        Label_017F:
            return 1;
        }

        public class JobLock
        {
            public string iname;
            public int lv;

            public JobLock()
            {
                base..ctor();
                return;
            }
        }
    }
}

