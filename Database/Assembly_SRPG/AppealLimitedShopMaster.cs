namespace SRPG
{
    using System;

    public class AppealLimitedShopMaster
    {
        public string appeal_id;
        public long start_at;
        public long end_at;
        public int priority;
        public float pos_x_chara;
        public float pos_x_text;

        public AppealLimitedShopMaster()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_AppealLimitedShopMaster json)
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
            this.priority = json.fields.priority;
            this.pos_x_chara = json.fields.position_chara;
            this.pos_x_text = json.fields.position_text;
            return 1;
        }
    }
}

