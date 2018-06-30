namespace SRPG
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class SkillLockCondition
    {
        public int type;
        public int value;
        public List<int> x;
        public List<int> y;
        [NonSerialized]
        public bool unlock;

        public SkillLockCondition()
        {
            this.x = new List<int>();
            this.y = new List<int>();
            base..ctor();
            return;
        }

        public void Clear()
        {
            this.value = 0;
            this.x.Clear();
            this.y.Clear();
            return;
        }

        public void CopyTo(JSON_SkillLockCondition dsc)
        {
            dsc.type = this.type;
            dsc.value = this.value;
            dsc.x = this.x.ToArray();
            dsc.y = this.y.ToArray();
            return;
        }

        public void CopyTo(SkillLockCondition dsc)
        {
            dsc.type = this.type;
            dsc.value = this.value;
            dsc.x = this.x;
            dsc.y = this.y;
            dsc.unlock = this.unlock;
            return;
        }
    }
}

