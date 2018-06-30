namespace SRPG
{
    using System;

    [Serializable]
    public class Json_JobSelectable
    {
        public long[] abils;
        public long[] artifacts;

        public Json_JobSelectable()
        {
            base..ctor();
            return;
        }
    }
}

