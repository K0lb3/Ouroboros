namespace SRPG
{
    using System;

    [Serializable]
    public class Json_UnitSelectable
    {
        public long job;
        public Json_UnitJob[] quests;

        public Json_UnitSelectable()
        {
            base..ctor();
            return;
        }
    }
}

