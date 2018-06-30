namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_InnerObjective
    {
        public int type;
        public string val;
        public string item;
        public int num;
        public int item_type;
        public int is_takeover_progress;

        public JSON_InnerObjective()
        {
            base..ctor();
            return;
        }

        public bool IsTakeoverProgress
        {
            get
            {
                return (this.is_takeover_progress == 1);
            }
        }
    }
}

