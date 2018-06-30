namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_AIActionTable
    {
        public JSON_AIAction[] actions;
        public int looped;

        public JSON_AIActionTable()
        {
            base..ctor();
            return;
        }
    }
}

