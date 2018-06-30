namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_SimpleQuestDropParam
    {
        public string iname;
        public string[] questlist;

        public JSON_SimpleQuestDropParam()
        {
            base..ctor();
            return;
        }
    }
}

