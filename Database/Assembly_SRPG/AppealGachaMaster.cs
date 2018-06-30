namespace SRPG
{
    using System;

    public class AppealGachaMaster
    {
        public string appeal_id;
        public long start_at;
        public long end_at;
        public bool is_new;

        public AppealGachaMaster()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_AppealGachaMaster json)
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
            this.is_new = (json.fields.flag_new != null) ? 1 : 0;
            return 1;
        }
    }
}

