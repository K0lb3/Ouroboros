namespace SRPG
{
    using System;

    [Serializable]
    public class UnitEntryTrigger
    {
        public int type;
        public string unit;
        public string skill;
        public int value;
        public int x;
        public int y;
        [NonSerialized]
        public bool on;

        public UnitEntryTrigger()
        {
            this.unit = string.Empty;
            this.skill = string.Empty;
            base..ctor();
            return;
        }

        public void Clear()
        {
            this.unit = string.Empty;
            this.skill = string.Empty;
            this.value = 0;
            this.x = 0;
            this.y = 0;
            return;
        }
    }
}

