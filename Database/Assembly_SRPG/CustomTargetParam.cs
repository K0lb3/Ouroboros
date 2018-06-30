namespace SRPG
{
    using System;

    public class CustomTargetParam
    {
        public string iname;
        public string[] units;
        public string[] jobs;
        public string[] unit_groups;
        public string[] job_groups;
        public string first_job;
        public string second_job;
        public string third_job;
        public ESex sex;
        public int birth_id;
        public int element;

        public CustomTargetParam()
        {
            base..ctor();
            return;
        }

        public unsafe bool Deserialize(JSON_CustomTargetParam json)
        {
            string[] textArray1;
            int num;
            int num2;
            int num3;
            int num4;
            string[] strArray;
            string str;
            string str2;
            string[] strArray2;
            int num5;
            this.iname = json.iname;
            if (json.units == null)
            {
                goto Label_0053;
            }
            this.units = new string[(int) json.units.Length];
            num = 0;
            goto Label_0045;
        Label_0031:
            this.units[num] = json.units[num];
            num += 1;
        Label_0045:
            if (num < ((int) json.units.Length))
            {
                goto Label_0031;
            }
        Label_0053:
            if (json.jobs == null)
            {
                goto Label_009A;
            }
            this.jobs = new string[(int) json.jobs.Length];
            num2 = 0;
            goto Label_008C;
        Label_0078:
            this.jobs[num2] = json.jobs[num2];
            num2 += 1;
        Label_008C:
            if (num2 < ((int) json.jobs.Length))
            {
                goto Label_0078;
            }
        Label_009A:
            if (json.unit_groups == null)
            {
                goto Label_00E1;
            }
            this.unit_groups = new string[(int) json.unit_groups.Length];
            num3 = 0;
            goto Label_00D3;
        Label_00BF:
            this.unit_groups[num3] = json.unit_groups[num3];
            num3 += 1;
        Label_00D3:
            if (num3 < ((int) json.unit_groups.Length))
            {
                goto Label_00BF;
            }
        Label_00E1:
            if (json.job_groups == null)
            {
                goto Label_0128;
            }
            this.job_groups = new string[(int) json.job_groups.Length];
            num4 = 0;
            goto Label_011A;
        Label_0106:
            this.job_groups[num4] = json.job_groups[num4];
            num4 += 1;
        Label_011A:
            if (num4 < ((int) json.job_groups.Length))
            {
                goto Label_0106;
            }
        Label_0128:
            this.first_job = json.first_job;
            this.second_job = json.second_job;
            this.third_job = json.third_job;
            this.sex = json.sex;
            this.birth_id = json.birth_id;
            textArray1 = new string[] { &json.dark.ToString(), &json.shine.ToString(), &json.thunder.ToString(), &json.wind.ToString(), &json.water.ToString(), &json.fire.ToString() };
            strArray = textArray1;
            str = string.Empty;
            strArray2 = strArray;
            num5 = 0;
            goto Label_01EB;
        Label_01D3:
            str2 = strArray2[num5];
            str = str + str2;
            num5 += 1;
        Label_01EB:
            if (num5 < ((int) strArray2.Length))
            {
                goto Label_01D3;
            }
            this.element = Convert.ToInt32(str, 2);
            return 1;
        }
    }
}

