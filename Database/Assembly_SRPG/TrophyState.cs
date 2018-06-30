namespace SRPG
{
    using System;

    public class TrophyState
    {
        public string iname;
        public bool IsEnded;
        public int[] Count;
        public int StartYMD;
        public DateTime RewardedAt;
        public bool IsDirty;
        public TrophyParam Param;
        public bool IsSending;

        public TrophyState()
        {
            this.Count = new int[0];
            base..ctor();
            return;
        }

        public bool IsCompleted
        {
            get
            {
                int num;
                if (this.Param == null)
                {
                    goto Label_0025;
                }
                if (((int) this.Count.Length) >= ((int) this.Param.Objectives.Length))
                {
                    goto Label_0027;
                }
            Label_0025:
                return 0;
            Label_0027:
                num = 0;
                goto Label_0053;
            Label_002E:
                if (this.Count[num] >= this.Param.Objectives[num].RequiredCount)
                {
                    goto Label_004F;
                }
                return 0;
            Label_004F:
                num += 1;
            Label_0053:
                if (num >= ((int) this.Param.Objectives.Length))
                {
                    goto Label_0074;
                }
                if (num < ((int) this.Count.Length))
                {
                    goto Label_002E;
                }
            Label_0074:
                return 1;
            }
        }
    }
}

