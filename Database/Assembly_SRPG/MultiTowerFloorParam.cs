namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class MultiTowerFloorParam
    {
        public List<MapParam> map;
        private QuestParam CachedQuestParam;
        private QuestParam BaseQuest;
        public int id;
        public string title;
        public string name;
        public string expr;
        public string cond;
        public string tower_id;
        public int cond_floor;
        public string reward_id;
        public short pt;
        public short FloorIndex;
        public short floor;
        public short lv;
        public short joblv;
        public short unitnum;
        public short notcon;
        public bool DownLoaded;
        public string error_messarge;
        public string me_id;
        public int is_wth_no_chg;
        public string wth_set_id;

        public MultiTowerFloorParam()
        {
            this.map = new List<MapParam>(BattleCore.MAX_MAP);
            base..ctor();
            return;
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

        private unsafe QuestParam ConvertQuestParam(QuestParam original)
        {
            string[] textArray1;
            GameManager manager;
            JSON_QuestParam param;
            QuestParam param2;
            int num;
            JSON_MapParam param3;
            TowerRewardParam param4;
            List<TowerRewardItem> list;
            int num2;
            TowerRewardItem item;
            QuestParam param5;
            if (original != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            manager = MonoSingleton<GameManager>.Instance;
            param = new JSON_QuestParam();
            param.iname = this.tower_id + "_" + &this.floor.ToString();
            param.title = this.title;
            param.name = this.name;
            param.expr = this.expr;
            param.cond = this.cond;
            if (this.cond_floor == null)
            {
                goto Label_00AD;
            }
            param2 = manager.FindQuest(this.tower_id + "_" + ((int) this.cond_floor));
            if (param2 == null)
            {
                goto Label_00AD;
            }
            textArray1 = new string[] { param2.iname };
            param.cond_quests = textArray1;
        Label_00AD:
            param.map = new JSON_MapParam[this.map.Count];
            param.lv = this.lv;
            param.pt = this.pt;
            num = 0;
            goto Label_016F;
        Label_00E2:
            param3 = new JSON_MapParam();
            param3.set = this.map[num].mapSetName;
            param3.scn = this.map[num].mapSceneName;
            param3.bgm = this.map[num].bgmName;
            param3.btl = this.map[num].battleSceneName;
            param3.ev = this.map[num].eventSceneName;
            param.map[num] = param3;
            num += 1;
        Label_016F:
            if (num < this.map.Count)
            {
                goto Label_00E2;
            }
            param.area = original.ChapterID;
            param.type = original.type;
            param.notcon = this.notcon;
            param.notitm = 1;
            param.pnum = original.playerNum;
            param.gold = 0;
            param.is_unit_chg = 0;
            param.multi = 1;
            param.me_id = this.me_id;
            param.is_wth_no_chg = this.is_wth_no_chg;
            param.wth_set_id = this.wth_set_id;
            param4 = MonoSingleton<GameManager>.Instance.FindTowerReward(this.reward_id);
            if (param4 == null)
            {
                goto Label_0264;
            }
            list = param4.GetTowerRewardItem();
            num2 = 0;
            goto Label_0256;
        Label_021F:
            item = list[num2];
            if (item != null)
            {
                goto Label_0236;
            }
            goto Label_0250;
        Label_0236:
            if (item.type != 1)
            {
                goto Label_0250;
            }
            param.gold = item.num;
        Label_0250:
            num2 += 1;
        Label_0256:
            if (num2 < list.Count)
            {
                goto Label_021F;
            }
        Label_0264:
            param5 = new QuestParam();
            param5.Deserialize(param);
            param5.EntryCondition = original.EntryCondition;
            param5.unitNum = this.unitnum;
            return param5;
        }

        public void Deserialize(JSON_MultiTowerFloorParam json)
        {
            int num;
            MapParam param;
            GameManager manager;
            QuestParam param2;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.id = json.id;
            this.title = json.title;
            this.name = json.name;
            this.expr = json.expr;
            this.cond = json.cond;
            this.tower_id = json.tower_id;
            this.cond_floor = json.cond_floor;
            this.pt = json.pt;
            this.lv = json.lv;
            this.joblv = json.joblv;
            this.reward_id = json.reward_id;
            this.floor = json.floor;
            this.unitnum = json.unitnum;
            this.notcon = json.notcon;
            this.me_id = json.me_id;
            this.is_wth_no_chg = json.is_wth_no_chg;
            this.wth_set_id = json.wth_set_id;
            this.map.Clear();
            if (json.map == null)
            {
                goto Label_0127;
            }
            num = 0;
            goto Label_0119;
        Label_00F5:
            param = new MapParam();
            param.Deserialize(json.map[num]);
            this.map.Add(param);
            num += 1;
        Label_0119:
            if (num < ((int) json.map.Length))
            {
                goto Label_00F5;
            }
        Label_0127:
            manager = MonoSingleton<GameManager>.Instance;
            this.BaseQuest = manager.FindQuest(this.tower_id);
            param2 = this.GetQuestParam();
            manager.AddMTQuest(param2.iname, param2);
            return;
        }

        public QuestParam GetQuestParam()
        {
            return this.Clone(null, 1);
        }
    }
}

