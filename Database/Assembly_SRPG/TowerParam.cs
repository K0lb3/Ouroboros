namespace SRPG
{
    using System;

    public class TowerParam
    {
        public string iname;
        public string name;
        public string expr;
        public string banr;
        public string bg;
        public string floor_bg_open;
        public string floor_bg_close;
        public short unit_recover_minute;
        public short unit_recover_coin;
        public string prefabPath;
        public string eventURL;
        public bool can_unit_recover;
        public bool is_down;
        public bool is_view_ranking;
        public short unlock_level;
        public string unlock_quest;
        public bool is_unlock;
        public string URL;
        public short floor_reset_coin;
        public string score_iname;

        public TowerParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TowerParam json)
        {
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.iname = json.iname;
            this.name = json.name;
            this.expr = json.expr;
            this.banr = json.banr;
            this.prefabPath = json.item;
            this.bg = json.bg;
            this.floor_bg_open = json.floor_bg_open;
            this.floor_bg_close = json.floor_bg_close;
            this.can_unit_recover = json.can_unit_recover == 1;
            this.unit_recover_minute = json.unit_recover_minute;
            this.unit_recover_coin = json.unit_recover_coin;
            this.eventURL = json.eventURL;
            this.is_down = json.is_down > 0;
            this.is_view_ranking = json.is_view_ranking > 0;
            this.unlock_level = json.unlock_level;
            this.unlock_quest = json.unlock_quest;
            this.URL = json.url;
            this.floor_reset_coin = json.floor_reset_coin;
            this.score_iname = json.score_iname;
            return;
        }
    }
}

