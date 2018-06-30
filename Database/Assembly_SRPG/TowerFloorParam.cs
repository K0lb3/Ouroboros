namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class TowerFloorParam
    {
        public List<MapParam> map;
        private QuestParam CachedQuestParam;
        private QuestParam BaseQuest;
        public string iname;
        public string title;
        public string name;
        public string expr;
        public string cond;
        public string tower_id;
        public string cond_quest;
        public string rdy_cnd;
        public string reward_id;
        public short pt;
        public short FloorIndex;
        public byte floor;
        private byte hp_recover_rate;
        public byte lv;
        public byte joblv;
        public bool can_help;
        public string deck;
        public byte[] tag_num;
        public bool DownLoaded;
        public string error_messarge;
        public byte is_unit_chg;
        public string me_id;
        public int is_wth_no_chg;
        public string wth_set_id;
        public string mission;
        public int naut;

        public TowerFloorParam()
        {
            this.map = new List<MapParam>(BattleCore.MAX_MAP);
            base..ctor();
            return;
        }

        public int CalcHelaNum(int max_hp)
        {
            return (int) (((float) max_hp) * (((float) this.hp_recover_rate) / 100f));
        }

        public QuestParam Clone(QuestParam original, bool useCache)
        {
            if (useCache == null)
            {
                goto Label_0036;
            }
            if (this.CachedQuestParam != null)
            {
                goto Label_002F;
            }
            this.CachedQuestParam = this.ConvertQuestParam((original == null) ? this.BaseQuest : original);
        Label_002F:
            return this.CachedQuestParam;
        Label_0036:
            return this.ConvertQuestParam((original == null) ? this.BaseQuest : original);
        }

        private QuestParam ConvertQuestParam(QuestParam original)
        {
            string[] textArray1;
            JSON_QuestParam param;
            int num;
            JSON_MapParam param2;
            TowerRewardParam param3;
            List<TowerRewardItem> list;
            int num2;
            TowerRewardItem item;
            QuestParam param4;
            if (original != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            param = new JSON_QuestParam();
            param.iname = this.iname;
            param.title = this.title;
            param.name = this.name;
            param.expr = this.expr;
            param.cond = this.cond;
            textArray1 = new string[] { this.cond_quest };
            param.cond_quests = textArray1;
            param.map = new JSON_MapParam[this.map.Count];
            param.rdy_cnd = this.rdy_cnd;
            param.lv = this.lv;
            param.pt = this.pt;
            num = 0;
            goto Label_0126;
        Label_00A0:
            param2 = new JSON_MapParam();
            param2.set = this.map[num].mapSetName;
            param2.scn = this.map[num].mapSceneName;
            param2.bgm = this.map[num].bgmName;
            param2.btl = this.map[num].battleSceneName;
            param2.ev = this.map[num].eventSceneName;
            param.map[num] = param2;
            num += 1;
        Label_0126:
            if (num < this.map.Count)
            {
                goto Label_00A0;
            }
            param.naut = this.naut;
            param.area = original.ChapterID;
            param.type = 7;
            param.notcon = 1;
            param.notitm = 1;
            param.gold = 0;
            param.is_unit_chg = this.is_unit_chg;
            param.me_id = this.me_id;
            param.is_wth_no_chg = this.is_wth_no_chg;
            param.wth_set_id = this.wth_set_id;
            param.tower_mission = this.mission;
            param3 = MonoSingleton<GameManager>.Instance.FindTowerReward(this.reward_id);
            if (param3 == null)
            {
                goto Label_0213;
            }
            list = param3.GetTowerRewardItem();
            num2 = 0;
            goto Label_0205;
        Label_01CE:
            item = list[num2];
            if (item != null)
            {
                goto Label_01E5;
            }
            goto Label_01FF;
        Label_01E5:
            if (item.type != 1)
            {
                goto Label_01FF;
            }
            param.gold = item.num;
        Label_01FF:
            num2 += 1;
        Label_0205:
            if (num2 < list.Count)
            {
                goto Label_01CE;
            }
        Label_0213:
            param4 = new QuestParam();
            param4.Deserialize(param);
            return param4;
        }

        public void Deserialize(JSON_TowerFloorParam json)
        {
            int num;
            MapParam param;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.iname = json.iname;
            this.title = json.title;
            this.name = json.name;
            this.expr = json.expr;
            this.cond = json.cond;
            this.tower_id = json.tower_id;
            this.cond_quest = json.cond_quest;
            this.hp_recover_rate = json.hp_recover_rate;
            this.pt = json.pt;
            this.lv = json.lv;
            this.joblv = json.joblv;
            this.can_help = json.can_help == 1;
            this.rdy_cnd = json.rdy_cnd;
            this.reward_id = json.reward_id;
            this.floor = json.floor;
            this.is_unit_chg = json.is_unit_chg;
            this.me_id = json.me_id;
            this.is_wth_no_chg = json.is_wth_no_chg;
            this.wth_set_id = json.wth_set_id;
            this.naut = json.naut;
            this.map.Clear();
            this.mission = json.mission;
            if (json.map == null)
            {
                goto Label_015A;
            }
            num = 0;
            goto Label_014C;
        Label_0128:
            param = new MapParam();
            param.Deserialize(json.map[num]);
            this.map.Add(param);
            num += 1;
        Label_014C:
            if (num < ((int) json.map.Length))
            {
                goto Label_0128;
            }
        Label_015A:
            this.BaseQuest = MonoSingleton<GameManager>.Instance.FindBaseQuest(7, this.tower_id);
            return;
        }

        public int GetFloorNo()
        {
            return int.Parse(Regex.Replace(this.name, "[^0-9]", string.Empty));
        }

        public QuestParam GetQuestParam()
        {
            return this.Clone(null, 1);
        }
    }
}

