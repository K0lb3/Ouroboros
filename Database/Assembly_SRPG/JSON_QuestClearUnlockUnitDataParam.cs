namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_QuestClearUnlockUnitDataParam
    {
        public string iname;
        public string uid;
        public int add;
        public int type;
        public string new_id;
        public string old_id;
        public string parent_id;
        public int ulv;
        public string aid;
        public int alv;
        public string[] qids;
        public int qcnd;

        public JSON_QuestClearUnlockUnitDataParam()
        {
            base..ctor();
            return;
        }
    }
}

