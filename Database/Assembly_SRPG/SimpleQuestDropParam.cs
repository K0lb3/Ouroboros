namespace SRPG
{
    using System;

    public class SimpleQuestDropParam
    {
        public string item_iname;
        public string[] questlist;

        public SimpleQuestDropParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_SimpleQuestDropParam json)
        {
            this.item_iname = json.iname;
            this.questlist = json.questlist;
            return 1;
        }
    }
}

