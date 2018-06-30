namespace SRPG
{
    using System;

    [Serializable]
    public class UnitMonitorCondition
    {
        public string iname;
        public string tag;
        public int turn;
        public int x;
        public int y;

        public UnitMonitorCondition()
        {
            this.iname = string.Empty;
            this.tag = string.Empty;
            this.x = -1;
            this.y = -1;
            base..ctor();
            return;
        }
    }
}

