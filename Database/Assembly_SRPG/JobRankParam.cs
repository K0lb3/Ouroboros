namespace SRPG
{
    using System;

    public class JobRankParam
    {
        public static readonly int MAX_RANKUP_EQUIPS;
        public int JobChangeCost;
        public string[] JobChangeItems;
        public int[] JobChangeItemNums;
        public int cost;
        public string[] equips;
        public BuffEffect.BuffValues[] buff_list;
        public OString[] learnings;

        static JobRankParam()
        {
            MAX_RANKUP_EQUIPS = 6;
            return;
        }

        public JobRankParam()
        {
            this.JobChangeItems = new string[3];
            this.JobChangeItemNums = new int[3];
            this.equips = new string[MAX_RANKUP_EQUIPS];
            base..ctor();
            return;
        }

        public unsafe bool Deserialize(JSON_JobRankParam json)
        {
            int num;
            int num2;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.JobChangeCost = json.chcost;
            this.JobChangeItems[0] = json.chitm1;
            this.JobChangeItems[1] = json.chitm2;
            this.JobChangeItems[2] = json.chitm3;
            this.JobChangeItemNums[0] = json.chnum1;
            this.JobChangeItemNums[1] = json.chnum2;
            this.JobChangeItemNums[2] = json.chnum3;
            this.cost = json.cost;
            this.equips[0] = json.eqid1;
            this.equips[1] = json.eqid2;
            this.equips[2] = json.eqid3;
            this.equips[3] = json.eqid4;
            this.equips[4] = json.eqid5;
            this.equips[5] = json.eqid6;
            this.learnings = null;
            num = 0;
            if (string.IsNullOrEmpty(json.learn1) != null)
            {
                goto Label_00E5;
            }
            num += 1;
        Label_00E5:
            if (string.IsNullOrEmpty(json.learn2) != null)
            {
                goto Label_00F9;
            }
            num += 1;
        Label_00F9:
            if (string.IsNullOrEmpty(json.learn3) != null)
            {
                goto Label_010D;
            }
            num += 1;
        Label_010D:
            if (num <= 0)
            {
                goto Label_01B2;
            }
            this.learnings = new OString[num];
            num2 = 0;
            if (string.IsNullOrEmpty(json.learn1) != null)
            {
                goto Label_0152;
            }
            *(&(this.learnings[num2++])) = json.learn1;
        Label_0152:
            if (string.IsNullOrEmpty(json.learn2) != null)
            {
                goto Label_0182;
            }
            *(&(this.learnings[num2++])) = json.learn2;
        Label_0182:
            if (string.IsNullOrEmpty(json.learn3) != null)
            {
                goto Label_01B2;
            }
            *(&(this.learnings[num2++])) = json.learn3;
        Label_01B2:
            return 1;
        }
    }
}

