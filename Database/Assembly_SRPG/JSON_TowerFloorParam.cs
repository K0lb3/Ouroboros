namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TowerFloorParam
    {
        public string iname;
        public string title;
        public string name;
        public string expr;
        public string cond;
        public string tower_id;
        public string cond_quest;
        public string rdy_cnd;
        public string reward_id;
        public JSON_MapParam[] map;
        public string deck;
        public byte[] tag;
        public byte hp_recover_rate;
        public byte pt;
        public byte lv;
        public byte joblv;
        public byte can_help;
        public byte floor;
        public byte is_unit_chg;
        public int naut;
        public string me_id;
        public int is_wth_no_chg;
        public string wth_set_id;
        public string mission;

        public JSON_TowerFloorParam()
        {
            base..ctor();
            return;
        }

        public JSON_QuestParam ConvertQuestParam()
        {
            string[] textArray1;
            JSON_QuestParam param;
            int num;
            JSON_MapParam param2;
            param = new JSON_QuestParam();
            param.iname = this.iname;
            param.title = this.title;
            param.name = this.name;
            param.expr = this.expr;
            param.cond = this.cond;
            textArray1 = new string[] { this.cond_quest };
            param.cond_quests = textArray1;
            param.map = new JSON_MapParam[(int) this.map.Length];
            param.rdy_cnd = this.rdy_cnd;
            param.lv = this.lv;
            param.pt = this.pt;
            num = 0;
            goto Label_0107;
        Label_0095:
            param2 = new JSON_MapParam();
            param2.set = this.map[num].set;
            param2.scn = this.map[num].scn;
            param2.bgm = this.map[num].bgm;
            param2.btl = this.map[num].btl;
            param2.ev = this.map[num].ev;
            param.map[num] = param2;
            num += 1;
        Label_0107:
            if (num < ((int) this.map.Length))
            {
                goto Label_0095;
            }
            param.type = 7;
            param.notcon = 1;
            param.notitm = 1;
            param.gold = 0;
            param.is_unit_chg = this.is_unit_chg;
            param.naut = this.naut;
            param.me_id = this.me_id;
            param.is_wth_no_chg = this.is_wth_no_chg;
            param.wth_set_id = this.wth_set_id;
            param.tower_mission = this.mission;
            return param;
        }
    }
}

