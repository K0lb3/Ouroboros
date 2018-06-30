namespace SRPG
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class JSON_SkillLockCondition
    {
        public int type;
        public int value;
        public int[] x;
        public int[] y;

        public JSON_SkillLockCondition()
        {
            int[] numArray2;
            int[] numArray1;
            numArray1 = new int[] { -1 };
            this.x = numArray1;
            numArray2 = new int[] { -1 };
            this.y = numArray2;
            base..ctor();
            return;
        }

        public void CopyTo(JSON_SkillLockCondition dsc)
        {
            dsc.type = this.type;
            dsc.value = this.value;
            dsc.x = this.x;
            dsc.y = this.y;
            return;
        }

        public void CopyTo(SkillLockCondition dsc)
        {
            dsc.type = this.type;
            dsc.value = this.value;
            dsc.x = new List<int>(this.x);
            dsc.y = new List<int>(this.y);
            return;
        }
    }
}

