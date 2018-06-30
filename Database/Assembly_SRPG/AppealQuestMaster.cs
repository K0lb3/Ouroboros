namespace SRPG
{
    using System;

    public class AppealQuestMaster
    {
        public string appeal_id;
        public long start_at;
        public long end_at;

        public AppealQuestMaster()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_AppealQuestMaster json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.appeal_id = json.fields.appeal_id;
            this.start_at = TimeManager.FromDateTime(DateTime.Parse(json.fields.start_at));
            this.end_at = TimeManager.FromDateTime(DateTime.Parse(json.fields.end_at));
            return 1;
        }
    }
}

