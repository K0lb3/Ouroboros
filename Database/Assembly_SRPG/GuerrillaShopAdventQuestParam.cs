namespace SRPG
{
    using System;

    public class GuerrillaShopAdventQuestParam
    {
        public int id;
        public string qid;

        public GuerrillaShopAdventQuestParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_GuerrillaShopAdventQuestParam json)
        {
            this.id = json.id;
            this.qid = json.qid;
            return 1;
        }
    }
}

