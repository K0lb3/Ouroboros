namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class QuestParam
    {
        private BitArray bit_array;
        private static readonly int MULTI_MAX_TOTAL_UNIT;
        private static readonly int MULTI_MAX_PLAYER_UNIT;
        public string iname;
        public string title;
        public string name;
        public string expr;
        private short cond_index;
        private short world_index;
        private short ChapterID_index;
        public string mission;
        public string[] cond_quests;
        public ShareStringList units;
        public QuestDifficulties difficulty;
        public string navigation;
        public short dailyCount;
        public short dailyReset;
        public string storyTextID;
        public QuestStates state;
        public int clear_missions;
        public int[] mission_values;
        public QuestBonusObjective[] bonusObjective;
        public QuestPartyParam questParty;
        public short point;
        public short aplv;
        public short challengeLimit;
        public int pexp;
        public int uexp;
        public int gold;
        public int mcoin;
        public OShort clock_win;
        public OShort clock_lose;
        public OShort win;
        public OShort lose;
        public QuestTypes type;
        public SubQuestTypes subtype;
        public short lv;
        public OShort multi;
        public OShort multiDead;
        public OShort playerNum;
        public OShort unitNum;
        public List<MapParam> map;
        public string event_start;
        public string event_clear;
        private short ticket_index;
        public long start;
        public long end;
        public long key_end;
        public int key_cnt;
        public int key_limit;
        public string VersusThumnail;
        public string MapBuff;
        public int VersusMoveCount;
        public int DamageUpprPl;
        public int DamageUpprEn;
        public int DamageRatePl;
        public int DamageRateEn;
        public string MapEffectId;
        public string WeatherSetId;
        public string[] FirstClearItems;
        public bool gps_enable;
        public string AllowedJobs;
        public Tags AllowedTags;
        public int PhysBonus;
        public int MagBonus;
        public string ItemLayout;
        public ChapterParam Chapter;
        private int[] AtkTypeMags;
        public QuestCondParam EntryCondition;
        public QuestCondParam EntryConditionCh;
        public bool IsExtra;
        [CompilerGenerated]
        private int <dayReset>k__BackingField;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, bool> <>f__am$cache48;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, bool> <>f__am$cache49;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, string> <>f__am$cache4A;
        [CompilerGenerated]
        private static Dictionary<string, int> <>f__switch$map11;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, bool> <>f__am$cache4C;

        static QuestParam()
        {
            MULTI_MAX_TOTAL_UNIT = 4;
            MULTI_MAX_PLAYER_UNIT = 2;
            return;
        }

        public QuestParam()
        {
            this.bit_array = new BitArray(0x12);
            this.cond_index = -1;
            this.world_index = -1;
            this.ChapterID_index = -1;
            this.units = new ShareStringList(3);
            this.clock_win = 0;
            this.clock_lose = 0;
            this.win = 0;
            this.lose = 0;
            this.multi = 0;
            this.multiDead = 0;
            this.playerNum = 0;
            this.unitNum = 0;
            this.map = new List<MapParam>(BattleCore.MAX_MAP);
            this.ticket_index = -1;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <GetEntryQuestConditionsInternal>m__261(PartySlotTypeUnitPair slot)
        {
            return (slot.Type == 0);
        }

        [CompilerGenerated]
        private static bool <GetEntryQuestConditionsInternal>m__262(PartySlotTypeUnitPair slot)
        {
            return ((((slot.Type == 2) || (slot.Type == 3)) || (slot.Type == 4)) ? 1 : (slot.Type == 5));
        }

        [CompilerGenerated]
        private static string <GetEntryQuestConditionsInternal>m__263(PartySlotTypeUnitPair slot)
        {
            return slot.Unit;
        }

        [CompilerGenerated]
        private static bool <GetPartyCondType>m__264(PartySlotTypeUnitPair slot)
        {
            return (slot.Type == 0);
        }

        private unsafe void AddWeatherNameLists(List<string> wth_name_lists, List<string> wth_id_lists)
        {
            GameManager manager;
            string str;
            List<string>.Enumerator enumerator;
            WeatherParam param;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) != null)
            {
                goto Label_0018;
            }
            if (wth_id_lists != null)
            {
                goto Label_0019;
            }
        Label_0018:
            return;
        Label_0019:
            enumerator = wth_id_lists.GetEnumerator();
        Label_0020:
            try
            {
                goto Label_005D;
            Label_0025:
                str = &enumerator.Current;
                param = manager.MasterParam.GetWeatherParam(str);
                if (param == null)
                {
                    goto Label_005D;
                }
                if (wth_name_lists.Contains(param.Name) != null)
                {
                    goto Label_005D;
                }
                wth_name_lists.Add(param.Name);
            Label_005D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0025;
                }
                goto Label_007A;
            }
            finally
            {
            Label_006E:
                ((List<string>.Enumerator) enumerator).Dispose();
            }
        Label_007A:
            return;
        }

        public bool CheckAllowedAutoBattle()
        {
            QuestTypes types;
            switch (this.type)
            {
                case 0:
                    goto Label_0052;

                case 1:
                    goto Label_007A;

                case 2:
                    goto Label_007A;

                case 3:
                    goto Label_0052;

                case 4:
                    goto Label_0052;

                case 5:
                    goto Label_0052;

                case 6:
                    goto Label_0052;

                case 7:
                    goto Label_0052;

                case 8:
                    goto Label_007A;

                case 9:
                    goto Label_007A;

                case 10:
                    goto Label_0052;

                case 11:
                    goto Label_0052;

                case 12:
                    goto Label_007A;

                case 13:
                    goto Label_0052;

                case 14:
                    goto Label_007A;

                case 15:
                    goto Label_0052;
            }
            goto Label_007A;
        Label_0052:
            if (this.FirstAutoPlayProhibit == null)
            {
                goto Label_0073;
            }
            return ((this.state != 2) ? 0 : this.AllowAutoPlay);
        Label_0073:
            return this.AllowAutoPlay;
        Label_007A:
            return 0;
        }

        public bool CheckAllowedRetreat()
        {
            if (this.type != 3)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            return this.AllowRetreat;
        }

        public bool CheckDisableAbilities()
        {
            return this.DisableAbilities;
        }

        public bool CheckDisableContinue()
        {
            return this.DisableContinue;
        }

        public bool CheckDisableItems()
        {
            return this.DisableItems;
        }

        public bool CheckEnableChallange()
        {
            int num;
            int num2;
            num = this.GetChallangeLimit();
            if (num != null)
            {
                goto Label_000F;
            }
            return 1;
        Label_000F:
            if (this.GetChallangeCount() >= num)
            {
                goto Label_001F;
            }
            return 1;
        Label_001F:
            return 0;
        }

        public bool CheckEnableEntrySubMembers()
        {
            QuestTypes types;
            switch (this.type)
            {
                case 0:
                    goto Label_004A;

                case 1:
                    goto Label_004C;

                case 2:
                    goto Label_004C;

                case 3:
                    goto Label_004A;

                case 4:
                    goto Label_004A;

                case 5:
                    goto Label_004A;

                case 6:
                    goto Label_004A;

                case 7:
                    goto Label_004A;

                case 8:
                    goto Label_004C;

                case 9:
                    goto Label_004C;

                case 10:
                    goto Label_004A;

                case 11:
                    goto Label_004A;

                case 12:
                    goto Label_004C;

                case 13:
                    goto Label_004A;
            }
            goto Label_004C;
        Label_004A:
            return 1;
        Label_004C:
            return 0;
        }

        public bool CheckEnableGainedExp()
        {
            return ((this.type == 3) == 0);
        }

        public bool CheckEnableGainedGold()
        {
            return ((this.type == 3) == 0);
        }

        public bool CheckEnableGainedItem()
        {
            return ((this.type == 3) == 0);
        }

        public bool CheckEnableQuestResult()
        {
            if (this.type == 3)
            {
                goto Label_0018;
            }
            if (this.type != 2)
            {
                goto Label_001A;
            }
        Label_0018:
            return 0;
        Label_001A:
            return 1;
        }

        public bool CheckEnableReset()
        {
            FixParam param;
            if (this.difficulty == 1)
            {
                goto Label_001A;
            }
            if (this.difficulty == 2)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            return (MonoSingleton<GameManager>.Instance.MasterParam.FixParam.EliteResetMax > this.dailyReset);
        }

        public bool CheckEnableSuspendStart()
        {
            if (this.type == 3)
            {
                goto Label_0031;
            }
            if (this.type == 2)
            {
                goto Label_0031;
            }
            if (this.type == 1)
            {
                goto Label_0031;
            }
            if (this.type != 14)
            {
                goto Label_0033;
            }
        Label_0031:
            return 0;
        Label_0033:
            return 1;
        }

        public bool CheckMissionValueIsDefault(int index)
        {
            return (this.GetMissionValue(index) == -1);
        }

        public void Deserialize(JSON_QuestParam json)
        {
            char[] chArray1;
            object[] objArray2;
            object[] objArray1;
            string str;
            string[] strArray;
            int num;
            ObjectiveParam param;
            int num2;
            ObjectiveParam param2;
            int num3;
            MagnificationParam param3;
            QuestCondParam param4;
            QuestCondParam param5;
            int num4;
            int num5;
            int num6;
            MapParam param6;
            int num7;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.iname = json.iname;
            this.name = json.name;
            this.expr = json.expr;
            this.cond = json.cond;
            this.mission = json.mission;
            this.pexp = json.pexp;
            this.uexp = json.uexp;
            this.gold = json.gold;
            this.mcoin = json.mcoin;
            this.point = CheckCast.to_short(json.pt);
            this.multi = CheckCast.to_short(json.multi);
            this.multiDead = CheckCast.to_short(json.multi_dead);
            this.playerNum = CheckCast.to_short(json.pnum);
            this.unitNum = CheckCast.to_short((json.unum <= MULTI_MAX_PLAYER_UNIT) ? json.unum : MULTI_MAX_PLAYER_UNIT);
            this.aplv = CheckCast.to_short(json.aplv);
            this.challengeLimit = CheckCast.to_short(json.limit);
            this.dayReset = json.dayreset;
            if (this.multi == null)
            {
                goto Label_0203;
            }
            if ((json.pnum * json.unum) <= MULTI_MAX_TOTAL_UNIT)
            {
                goto Label_01A5;
            }
            objArray1 = new object[] { "iname:", json.iname, " / Current total unit is ", (int) (json.pnum * json.unum), ". Please set the total number of units to", (int) MULTI_MAX_TOTAL_UNIT };
            DebugUtility.LogError(string.Concat(objArray1));
        Label_01A5:
            if (json.unum <= MULTI_MAX_PLAYER_UNIT)
            {
                goto Label_0203;
            }
            objArray2 = new object[] { "iname:", json.iname, " / Current 1 player unit is ", (int) json.unum, ". Please set the 1 player number of units to", (int) MULTI_MAX_PLAYER_UNIT };
            DebugUtility.LogError(string.Concat(objArray2));
        Label_0203:
            this.key_limit = json.key_limit;
            this.clock_win = CheckCast.to_short(json.ctw);
            this.clock_lose = CheckCast.to_short(json.ctl);
            this.lv = CheckCast.to_short(Math.Max(json.lv, 1));
            this.win = CheckCast.to_short(json.win);
            this.lose = CheckCast.to_short(json.lose);
            this.type = (byte) json.type;
            this.subtype = (byte) json.subtype;
            this.cond_quests = null;
            this.units.Clear();
            this.ChapterID = json.area;
            this.world = json.world;
            this.storyTextID = json.text;
            this.hidden = (json.hide == 0) == 0;
            this.replayLimit = (json.replay_limit == 0) == 0;
            this.ticket = json.ticket;
            this.title = json.title;
            this.navigation = json.nav;
            this.AllowedJobs = json.ajob;
            this.AllowedTags = 0;
            if (string.IsNullOrEmpty(json.atag) != null)
            {
                goto Label_0396;
            }
            chArray1 = new char[] { 0x2c };
            strArray = json.atag.Split(chArray1);
            num = 0;
            goto Label_038D;
        Label_0357:
            if (string.IsNullOrEmpty(strArray[num]) != null)
            {
                goto Label_0389;
            }
            this.AllowedTags = (byte) (this.AllowedTags | ((byte) Enum.Parse(typeof(Tags), strArray[num])));
        Label_0389:
            num += 1;
        Label_038D:
            if (num < ((int) strArray.Length))
            {
                goto Label_0357;
            }
        Label_0396:
            this.PhysBonus = json.phyb + 100;
            this.MagBonus = json.magb + 100;
            this.IsBeginner = (0 == json.bgnr) == 0;
            this.ItemLayout = json.i_lyt;
            this.notSearch = (json.not_search == null) ? 0 : 1;
            param = MonoSingleton<GameManager>.GetInstanceDirect().FindObjective(json.mission);
            if (param == null)
            {
                goto Label_04E7;
            }
            this.bonusObjective = new QuestBonusObjective[(int) param.objective.Length];
            num2 = 0;
            goto Label_04D8;
        Label_041C:
            this.bonusObjective[num2] = new QuestBonusObjective();
            this.bonusObjective[num2].Type = param.objective[num2].type;
            this.bonusObjective[num2].TypeParam = param.objective[num2].val;
            this.bonusObjective[num2].item = param.objective[num2].item;
            this.bonusObjective[num2].itemNum = param.objective[num2].num;
            this.bonusObjective[num2].itemType = param.objective[num2].item_type;
            this.bonusObjective[num2].IsTakeoverProgress = param.objective[num2].IsTakeoverProgress;
            num2 += 1;
        Label_04D8:
            if (num2 < ((int) param.objective.Length))
            {
                goto Label_041C;
            }
        Label_04E7:
            param2 = MonoSingleton<GameManager>.GetInstanceDirect().FindTowerObjective(json.tower_mission);
            if (param2 == null)
            {
                goto Label_0602;
            }
            this.bonusObjective = new QuestBonusObjective[(int) param2.objective.Length];
            num3 = 0;
            goto Label_05DE;
        Label_051C:
            this.bonusObjective[num3] = new QuestBonusObjective();
            this.bonusObjective[num3].Type = param2.objective[num3].type;
            this.bonusObjective[num3].TypeParam = param2.objective[num3].val;
            this.bonusObjective[num3].item = param2.objective[num3].item;
            this.bonusObjective[num3].itemNum = param2.objective[num3].num;
            this.bonusObjective[num3].itemType = param2.objective[num3].item_type;
            this.bonusObjective[num3].IsTakeoverProgress = param2.objective[num3].IsTakeoverProgress;
            num3 += 1;
        Label_05DE:
            if (num3 < ((int) param2.objective.Length))
            {
                goto Label_051C;
            }
            this.mission_values = new int[(int) param2.objective.Length];
        Label_0602:
            param3 = MonoSingleton<GameManager>.GetInstanceDirect().FindMagnification(json.atk_mag);
            if ((param3 == null) || (param3.atkMagnifications == null))
            {
                goto Label_0634;
            }
            this.AtkTypeMags = param3.atkMagnifications;
        Label_0634:
            param4 = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCond(json.rdy_cnd);
            if (param4 == null)
            {
                goto Label_0655;
            }
            this.EntryCondition = param4;
        Label_0655:
            param5 = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestCond(json.rdy_cnd_ch);
            if (param5 == null)
            {
                goto Label_0676;
            }
            this.EntryConditionCh = param5;
        Label_0676:
            this.difficulty = (byte) json.mode;
            if (json.units == null)
            {
                goto Label_06D4;
            }
            this.units.Setup((int) json.units.Length);
            num4 = 0;
            goto Label_06C5;
        Label_06A9:
            this.units.Set(num4, json.units[num4]);
            num4 += 1;
        Label_06C5:
            if (num4 < ((int) json.units.Length))
            {
                goto Label_06A9;
            }
        Label_06D4:
            if (json.cond_quests == null)
            {
                goto Label_0721;
            }
            this.cond_quests = new string[(int) json.cond_quests.Length];
            num5 = 0;
            goto Label_0712;
        Label_06FA:
            this.cond_quests[num5] = json.cond_quests[num5];
            num5 += 1;
        Label_0712:
            if (num5 < ((int) json.cond_quests.Length))
            {
                goto Label_06FA;
            }
        Label_0721:
            this.map.Clear();
            if (json.map == null)
            {
                goto Label_0778;
            }
            num6 = 0;
            goto Label_0769;
        Label_073F:
            param6 = new MapParam();
            param6.Deserialize(json.map[num6]);
            this.map.Add(param6);
            num6 += 1;
        Label_0769:
            if (num6 < ((int) json.map.Length))
            {
                goto Label_073F;
            }
        Label_0778:
            this.event_start = json.evst;
            this.event_clear = json.evw;
            this.AllowRetreat = json.retr == 0;
            this.AllowAutoPlay = (json.naut == null) ? 1 : (json.naut == 2);
            this.FirstAutoPlayProhibit = json.naut == 2;
            this.Silent = (json.swin == 0) == 0;
            this.DisableAbilities = (json.notabl == 0) == 0;
            this.DisableItems = (json.notitm == 0) == 0;
            this.DisableContinue = (json.notcon == 0) == 0;
            this.UseFixEditor = (json.fix_editor == 0) == 0;
            this.IsNoStartVoice = (json.is_no_start_voice == 0) == 0;
            this.UseSupportUnit = json.sprt == 0;
            this.IsUnitChange = (json.is_unit_chg == 0) == 0;
            this.VersusThumnail = json.thumnail;
            this.MapBuff = json.mskill;
            this.VersusMoveCount = json.vsmovecnt;
            this.DamageUpprPl = json.dmg_up_pl;
            this.DamageUpprEn = json.dmg_up_en;
            this.DamageRatePl = json.dmg_rt_pl;
            this.DamageRateEn = json.dmg_rt_en;
            this.IsExtra = json.extra == 1;
            this.ShowReviewPopup = json.review == 1;
            this.IsMultiLeaderSkill = (json.is_multileader == 0) == 0;
            this.MapEffectId = json.me_id;
            this.IsWeatherNoChange = (json.is_wth_no_chg == 0) == 0;
            this.WeatherSetId = json.wth_set_id;
            if (json.fclr_items == null)
            {
                goto Label_0953;
            }
            this.FirstClearItems = new string[(int) json.fclr_items.Length];
            num7 = 0;
            goto Label_0944;
        Label_092C:
            this.FirstClearItems[num7] = json.fclr_items[num7];
            num7 += 1;
        Label_0944:
            if (num7 < ((int) json.fclr_items.Length))
            {
                goto Label_092C;
            }
        Label_0953:
            this.questParty = MonoSingleton<GameManager>.GetInstanceDirect().FindQuestParty(json.party_id);
            return;
        }

        public List<QuestParam> DetectNotClearConditionQuests()
        {
            GameManager manager;
            List<QuestParam> list;
            int num;
            QuestParam param;
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.IsBeginner() != null)
            {
                goto Label_0023;
            }
            if (this.IsBeginner == null)
            {
                goto Label_0023;
            }
            return null;
        Label_0023:
            if (this.cond_quests == null)
            {
                goto Label_0077;
            }
            list = new List<QuestParam>();
            num = 0;
            goto Label_0067;
        Label_003B:
            param = manager.FindQuest(this.cond_quests[num]);
            if (param == null)
            {
                goto Label_0063;
            }
            if (param.state == 2)
            {
                goto Label_0063;
            }
            list.Add(param);
        Label_0063:
            num += 1;
        Label_0067:
            if (num < ((int) this.cond_quests.Length))
            {
                goto Label_003B;
            }
            return list;
        Label_0077:
            return null;
        }

        public List<string> GetAddQuestInfo(bool is_inc_title)
        {
            List<string> list;
            GameManager manager;
            MapEffectParam param;
            string str;
            WeatherSetParam param2;
            List<string> list2;
            string str2;
            int num;
            list = new List<string>();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_001A;
            }
            return list;
        Label_001A:
            if (string.IsNullOrEmpty(this.MapEffectId) != null)
            {
                goto Label_008A;
            }
            param = manager.GetMapEffectParam(this.MapEffectId);
            if (param == null)
            {
                goto Label_008A;
            }
            str = string.Empty;
            if (is_inc_title == null)
            {
                goto Label_0054;
            }
            str = LocalizedText.Get("sys.PARTYEDITOR_COND_MAP_EFFECT");
        Label_0054:
            str = ((str + LocalizedText.Get("sys.PARTYEDITOR_COND_MAP_EFFECT_HEAD")) + param.Name) + LocalizedText.Get("sys.PARTYEDITOR_COND_MAP_EFFECT_BOTTOM");
            list.Add(str);
        Label_008A:
            if (string.IsNullOrEmpty(this.WeatherSetId) != null)
            {
                goto Label_016F;
            }
            param2 = manager.GetWeatherSetParam(this.WeatherSetId);
            if (param2 == null)
            {
                goto Label_016F;
            }
            list2 = new List<string>();
            this.AddWeatherNameLists(list2, param2.StartWeatherIdLists);
            this.AddWeatherNameLists(list2, param2.ChangeWeatherIdLists);
            if (list2.Count == null)
            {
                goto Label_016F;
            }
            str2 = string.Empty;
            if (is_inc_title == null)
            {
                goto Label_00F9;
            }
            str2 = LocalizedText.Get("sys.PARTYEDITOR_COND_WEATHER");
        Label_00F9:
            str2 = str2 + LocalizedText.Get("sys.PARTYEDITOR_COND_WEATHER_HEAD");
            num = 0;
            goto Label_0146;
        Label_0114:
            if (num == null)
            {
                goto Label_012E;
            }
            str2 = str2 + LocalizedText.Get("sys.PARTYEDITOR_COND_WEATHER_SEP");
        Label_012E:
            str2 = str2 + list2[num];
            num += 1;
        Label_0146:
            if (num < list2.Count)
            {
                goto Label_0114;
            }
            str2 = str2 + LocalizedText.Get("sys.PARTYEDITOR_COND_WEATHER_BOTTOM");
            list.Add(str2);
        Label_016F:
            return list;
        }

        public int GetAtkTypeMag(AttackDetailTypes type)
        {
            if (this.AtkTypeMags == null)
            {
                goto Label_0014;
            }
            return this.AtkTypeMags[type];
        Label_0014:
            return 0;
        }

        public int GetChallangeCount()
        {
            if (this.IsKeyQuest == null)
            {
                goto Label_002E;
            }
            if (this.Chapter == null)
            {
                goto Label_002E;
            }
            if (this.Chapter.GetKeyQuestType() != 2)
            {
                goto Label_002E;
            }
            return this.key_cnt;
        Label_002E:
            return this.dailyCount;
        }

        public int GetChallangeLimit()
        {
            if (this.IsKeyQuest == null)
            {
                goto Label_002E;
            }
            if (this.Chapter == null)
            {
                goto Label_002E;
            }
            if (this.Chapter.GetKeyQuestType() != 2)
            {
                goto Label_002E;
            }
            return this.key_limit;
        Label_002E:
            return this.challengeLimit;
        }

        public int GetClearMissionNum()
        {
            int num;
            int num2;
            num = 0;
            if (this.bonusObjective == null)
            {
                goto Label_0044;
            }
            if (((int) this.bonusObjective.Length) <= 0)
            {
                goto Label_0044;
            }
            num2 = 0;
            goto Label_0036;
        Label_0022:
            if (this.IsMissionClear(num2) == null)
            {
                goto Label_0032;
            }
            num += 1;
        Label_0032:
            num2 += 1;
        Label_0036:
            if (num2 < ((int) this.bonusObjective.Length))
            {
                goto Label_0022;
            }
        Label_0044:
            return num;
        }

        public static unsafe CollaboSkillParam.Pair GetCollaboSkillQuest(string quest_id)
        {
            CollaboSkillParam.Pair pair;
            GameManager manager;
            QuestParam param;
            List<QuestParam> list;
            QuestParam param2;
            List<QuestParam>.Enumerator enumerator;
            <GetCollaboSkillQuest>c__AnonStorey2E4 storeye;
            storeye = new <GetCollaboSkillQuest>c__AnonStorey2E4();
            storeye.quest_id = quest_id;
            pair = null;
            manager = MonoSingleton<GameManager>.Instance;
            if ((manager == null) == null)
            {
                goto Label_0025;
            }
            return null;
        Label_0025:
            param = Array.Find<QuestParam>(manager.Quests, new Predicate<QuestParam>(storeye.<>m__265));
            if (param != null)
            {
                goto Label_0046;
            }
            return null;
        Label_0046:
            list = GetSameChapterIDQuestParam(param.ChapterID);
            if (list != null)
            {
                goto Label_005A;
            }
            return null;
        Label_005A:
            enumerator = list.GetEnumerator();
        Label_0062:
            try
            {
                goto Label_0088;
            Label_0067:
                param2 = &enumerator.Current;
                pair = CollaboSkillParam.IsLearnQuest(param2.iname);
                if (pair == null)
                {
                    goto Label_0088;
                }
                goto Label_0094;
            Label_0088:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0067;
                }
            Label_0094:
                goto Label_00A6;
            }
            finally
            {
            Label_0099:
                ((List<QuestParam>.Enumerator) enumerator).Dispose();
            }
        Label_00A6:
            return pair;
        }

        public List<string> GetEntryQuestConditions(bool titled, bool includeUnitLv, bool includeUnits)
        {
            return this.GetEntryQuestConditionsInternal(this.EntryCondition, titled, includeUnitLv, includeUnits);
        }

        public List<string> GetEntryQuestConditionsCh(bool titled, bool includeUnitLv, bool includeUnits)
        {
            return this.GetEntryQuestConditionsInternal(this.EntryConditionCh, titled, includeUnitLv, includeUnits);
        }

        private List<string> GetEntryQuestConditionsInternal(QuestCondParam condParam, bool titled, bool includeUnitLv, bool includeUnits)
        {
            object[] objArray1;
            List<string> list;
            GameManager manager;
            int num;
            int num2;
            int num3;
            string str;
            int num4;
            int num5;
            int num6;
            string str2;
            string[] strArray;
            PartySlotTypeUnitPair[] pairArray;
            List<string> list2;
            int num7;
            UnitParam param;
            string str3;
            int num8;
            string str4;
            List<string> list3;
            int num9;
            JobParam param2;
            string str5;
            int num10;
            string str6;
            string str7;
            int num11;
            int num12;
            int num13;
            string str8;
            string str9;
            int num14;
            int num15;
            string str10;
            int num16;
            int num17;
            string str11;
            string str12;
            int num18;
            string str13;
            string str14;
            string str15;
            int num19;
            int num20;
            string str16;
            int num21;
            int num22;
            string str17;
            list = new List<string>();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((condParam == null) || ((condParam.plvmin <= 0) && (condParam.plvmax <= 0)))
            {
                goto Label_010D;
            }
            num = manager.MasterParam.GetPlayerLevelCap();
            num2 = Math.Max(condParam.plvmin, 1);
            num3 = Math.Min(condParam.plvmax, num);
            str = string.Empty;
            if (titled == null)
            {
                goto Label_00B0;
            }
            str = LocalizedText.Get("sys.PARTYEDITOR_COND_PLV");
            if (num2 <= 0)
            {
                goto Label_0080;
            }
            str = str + ((int) num2);
        Label_0080:
            str = str + LocalizedText.Get("sys.TILDE");
            if (num3 <= 0)
            {
                goto Label_0105;
            }
            str = str + ((int) num3);
            goto Label_0105;
        Label_00B0:
            if (num2 <= 0)
            {
                goto Label_00D0;
            }
            str = str + LocalizedText.Get("sys.PLV") + ((int) num2);
        Label_00D0:
            str = str + LocalizedText.Get("sys.TILDE");
            if (num3 <= 0)
            {
                goto Label_0105;
            }
            str = str + LocalizedText.Get("sys.PLV") + ((int) num3);
        Label_0105:
            list.Add(str);
        Label_010D:
            if (((condParam == null) || (includeUnitLv == null)) || ((condParam.ulvmin <= 0) && (condParam.ulvmax <= 0)))
            {
                goto Label_021B;
            }
            num4 = manager.MasterParam.GetUnitMaxLevel();
            num5 = Math.Max(condParam.ulvmin, 1);
            num6 = Math.Min(condParam.ulvmax, num4);
            str2 = string.Empty;
            if (titled == null)
            {
                goto Label_01BC;
            }
            str2 = LocalizedText.Get("sys.PARTYEDITOR_COND_ULV");
            if (num5 <= 0)
            {
                goto Label_018C;
            }
            str2 = str2 + ((int) num5);
        Label_018C:
            str2 = str2 + LocalizedText.Get("sys.TILDE");
            if (num6 <= 0)
            {
                goto Label_0213;
            }
            str2 = str2 + ((int) num6);
            goto Label_0213;
        Label_01BC:
            if (num5 <= 0)
            {
                goto Label_01DE;
            }
            str2 = str2 + LocalizedText.Get("sys.ULV") + ((int) num5);
        Label_01DE:
            str2 = str2 + LocalizedText.Get("sys.TILDE");
            if (num6 <= 0)
            {
                goto Label_0213;
            }
            str2 = str2 + LocalizedText.Get("sys.ULV") + ((int) num6);
        Label_0213:
            list.Add(str2);
        Label_021B:
            if (includeUnits == null)
            {
                goto Label_0417;
            }
            strArray = null;
            strArray = (condParam != null) ? condParam.unit : null;
            if ((condParam == null) || (condParam.party_type != 1))
            {
                goto Label_0258;
            }
            strArray = condParam.unit;
            goto Label_0316;
        Label_0258:
            if (this.questParty == null)
            {
                goto Label_02FC;
            }
            if (<>f__am$cache48 != null)
            {
                goto Label_028A;
            }
            <>f__am$cache48 = new Func<PartySlotTypeUnitPair, bool>(QuestParam.<GetEntryQuestConditionsInternal>m__261);
        Label_028A:
            if (Enumerable.Any<PartySlotTypeUnitPair>(this.questParty.GetMainSubSlots(), <>f__am$cache48) == null)
            {
                goto Label_02A1;
            }
            strArray = null;
            goto Label_02F7;
        Label_02A1:
            if (<>f__am$cache49 != null)
            {
                goto Label_02C4;
            }
            <>f__am$cache49 = new Func<PartySlotTypeUnitPair, bool>(QuestParam.<GetEntryQuestConditionsInternal>m__262);
        Label_02C4:
            if (<>f__am$cache4A != null)
            {
                goto Label_02E6;
            }
            <>f__am$cache4A = new Func<PartySlotTypeUnitPair, string>(QuestParam.<GetEntryQuestConditionsInternal>m__263);
        Label_02E6:
            strArray = Enumerable.ToArray<string>(Enumerable.Select<PartySlotTypeUnitPair, string>(Enumerable.Where<PartySlotTypeUnitPair>(this.questParty.GetMainSubSlots(), <>f__am$cache49), <>f__am$cache4A));
        Label_02F7:
            goto Label_0316;
        Label_02FC:
            if ((condParam == null) || (condParam.party_type != 2))
            {
                goto Label_0316;
            }
            strArray = condParam.unit;
        Label_0316:
            if ((strArray == null) || (((int) strArray.Length) <= 0))
            {
                goto Label_0417;
            }
            list2 = new List<string>();
            num7 = 0;
            goto Label_037B;
        Label_0336:
            param = manager.GetUnitParam(strArray[num7]);
            if (param != null)
            {
                goto Label_034F;
            }
            goto Label_0375;
        Label_034F:
            if (list2.Contains(param.name) == null)
            {
                goto Label_0367;
            }
            goto Label_0375;
        Label_0367:
            list2.Add(param.name);
        Label_0375:
            num7 += 1;
        Label_037B:
            if (num7 < ((int) strArray.Length))
            {
                goto Label_0336;
            }
            if (list2.Count <= 0)
            {
                goto Label_0417;
            }
            str3 = string.Empty;
            num8 = 0;
            goto Label_03D1;
        Label_03A2:
            str3 = str3 + ((num8 <= 0) ? string.Empty : "、") + list2[num8];
            num8 += 1;
        Label_03D1:
            if (num8 < list2.Count)
            {
                goto Label_03A2;
            }
            if (string.IsNullOrEmpty(str3) != null)
            {
                goto Label_0417;
            }
            str4 = string.Empty;
            if (titled == null)
            {
                goto Label_0404;
            }
            str4 = LocalizedText.Get("sys.PARTYEDITOR_COND_UNIT");
        Label_0404:
            str4 = str4 + str3;
            list.Add(str4);
        Label_0417:
            if (((condParam == null) || (condParam.job == null)) || (((int) condParam.job.Length) <= 0))
            {
                goto Label_052E;
            }
            list3 = new List<string>();
            num9 = 0;
            goto Label_048E;
        Label_0445:
            param2 = manager.GetJobParam(condParam.job[num9]);
            if (param2 != null)
            {
                goto Label_0462;
            }
            goto Label_0488;
        Label_0462:
            if (list3.Contains(param2.name) == null)
            {
                goto Label_047A;
            }
            goto Label_0488;
        Label_047A:
            list3.Add(param2.name);
        Label_0488:
            num9 += 1;
        Label_048E:
            if (num9 < ((int) condParam.job.Length))
            {
                goto Label_0445;
            }
            if (list3.Count <= 0)
            {
                goto Label_052E;
            }
            str5 = string.Empty;
            num10 = 0;
            goto Label_04E8;
        Label_04B9:
            str5 = str5 + ((num10 <= 0) ? string.Empty : "、") + list3[num10];
            num10 += 1;
        Label_04E8:
            if (num10 < list3.Count)
            {
                goto Label_04B9;
            }
            if (string.IsNullOrEmpty(str5) != null)
            {
                goto Label_052E;
            }
            str6 = string.Empty;
            if (titled == null)
            {
                goto Label_051B;
            }
            str6 = LocalizedText.Get("sys.PARTYEDITOR_COND_JOB");
        Label_051B:
            str6 = str6 + str5;
            list.Add(str6);
        Label_052E:
            if (((condParam == null) || (condParam.jobset == null)) || ((((int) condParam.jobset.Length) <= 0) || (Array.IndexOf<int>(condParam.jobset, 1) == -1)))
            {
                goto Label_0617;
            }
            str7 = string.Empty;
            num11 = 0;
            num12 = 0;
            goto Label_05BD;
        Label_0571:
            if (condParam.jobset[num11] != null)
            {
                goto Label_0584;
            }
            goto Label_05B7;
        Label_0584:
            num13 = num11 + 1;
            str7 = str7 + ((num12 <= 0) ? string.Empty : "、") + ((int) num13);
            num12 += 1;
        Label_05B7:
            num11 += 1;
        Label_05BD:
            if (num11 < ((int) condParam.jobset.Length))
            {
                goto Label_0571;
            }
            if (string.IsNullOrEmpty(str7) != null)
            {
                goto Label_0617;
            }
            str8 = string.Empty;
            if (titled == null)
            {
                goto Label_05F1;
            }
            str8 = LocalizedText.Get("sys.PARTYEDITOR_COND_JOBINDEX");
        Label_05F1:
            objArray1 = new object[] { str7 };
            str8 = str8 + LocalizedText.Get("sys.PARTYEDITOR_COND_JOBINDEX_VALUE", objArray1);
            list.Add(str8);
        Label_0617:
            if ((condParam == null) || ((condParam.rmin <= 0) && (condParam.rmax <= 0)))
            {
                goto Label_0704;
            }
            str9 = string.Empty;
            if (titled == null)
            {
                goto Label_064E;
            }
            str9 = LocalizedText.Get("sys.PARTYEDITOR_COND_RARITY");
        Label_064E:
            num14 = Math.Max(condParam.rmin - 1, 0);
            num15 = Math.Max(condParam.rmax - 1, 0);
            if (num14 != num15)
            {
                goto Label_069B;
            }
            str9 = str9 + LocalizedText.Get("sys.RARITY_STAR_" + ((int) num14));
            goto Label_06FC;
        Label_069B:
            if (num14 <= 0)
            {
                goto Label_06C2;
            }
            str9 = str9 + LocalizedText.Get("sys.RARITY_STAR_" + ((int) num14));
        Label_06C2:
            str9 = str9 + LocalizedText.Get("sys.TILDE");
            if (num15 <= 0)
            {
                goto Label_06FC;
            }
            str9 = str9 + LocalizedText.Get("sys.RARITY_STAR_" + ((int) num15));
        Label_06FC:
            list.Add(str9);
        Label_0704:
            if ((condParam == null) || (condParam.isElemLimit == null))
            {
                goto Label_07CF;
            }
            str10 = string.Empty;
            num16 = 0;
            num17 = 0;
            goto Label_0788;
        Label_0727:
            if (num16 != null)
            {
                goto Label_0733;
            }
            goto Label_0782;
        Label_0733:
            if (condParam.elem[num16] != null)
            {
                goto Label_0746;
            }
            goto Label_0782;
        Label_0746:
            str10 = str10 + ((num17 <= 0) ? string.Empty : "、") + LocalizedText.Get("sys.UNIT_ELEMENT_" + ((int) num16));
            num17 += 1;
        Label_0782:
            num16 += 1;
        Label_0788:
            if (num16 < ((int) condParam.elem.Length))
            {
                goto Label_0727;
            }
            if (string.IsNullOrEmpty(str10) != null)
            {
                goto Label_07CF;
            }
            str11 = string.Empty;
            if (titled == null)
            {
                goto Label_07BC;
            }
            str11 = LocalizedText.Get("sys.PARTYEDITOR_COND_ELEM");
        Label_07BC:
            str11 = str11 + str10;
            list.Add(str11);
        Label_07CF:
            if (((condParam == null) || (condParam.birth == null)) || (((int) condParam.birth.Length) <= 0))
            {
                goto Label_0873;
            }
            str12 = string.Empty;
            num18 = 0;
            goto Label_082C;
        Label_07FD:
            str12 = str12 + ((num18 <= 0) ? string.Empty : "、") + condParam.birth[num18];
            num18 += 1;
        Label_082C:
            if (num18 < ((int) condParam.birth.Length))
            {
                goto Label_07FD;
            }
            if (string.IsNullOrEmpty(str12) != null)
            {
                goto Label_0873;
            }
            str13 = string.Empty;
            if (titled == null)
            {
                goto Label_0860;
            }
            str13 = LocalizedText.Get("sys.PARTYEDITOR_COND_BIRTH");
        Label_0860:
            str13 = str13 + str12;
            list.Add(str13);
        Label_0873:
            if (condParam == null)
            {
                goto Label_08D8;
            }
            if (condParam.sex == null)
            {
                goto Label_08D8;
            }
            str14 = LocalizedText.Get("sys.SEX_" + ((int) condParam.sex));
            if (string.IsNullOrEmpty(str14) != null)
            {
                goto Label_08D8;
            }
            str15 = string.Empty;
            if (titled == null)
            {
                goto Label_08C5;
            }
            str15 = LocalizedText.Get("sys.PARTYEDITOR_COND_SEX");
        Label_08C5:
            str15 = str15 + str14;
            list.Add(str15);
        Label_08D8:
            if (condParam == null)
            {
                goto Label_097E;
            }
            if (condParam.hmin > 0)
            {
                goto Label_08F6;
            }
            if (condParam.hmax <= 0)
            {
                goto Label_097E;
            }
        Label_08F6:
            num19 = condParam.hmin;
            num20 = condParam.hmax;
            str16 = string.Empty;
            if (titled == null)
            {
                goto Label_091F;
            }
            str16 = LocalizedText.Get("sys.PARTYEDITOR_COND_HEIGHT");
        Label_091F:
            if (num19 <= 0)
            {
                goto Label_0941;
            }
            str16 = str16 + ((int) num19) + LocalizedText.Get("sys.CM_HEIGHT");
        Label_0941:
            str16 = str16 + LocalizedText.Get("sys.TILDE");
            if (num20 <= 0)
            {
                goto Label_0976;
            }
            str16 = str16 + ((int) num20) + LocalizedText.Get("sys.CM_HEIGHT");
        Label_0976:
            list.Add(str16);
        Label_097E:
            if (condParam == null)
            {
                goto Label_0A24;
            }
            if (condParam.wmin > 0)
            {
                goto Label_099C;
            }
            if (condParam.wmax <= 0)
            {
                goto Label_0A24;
            }
        Label_099C:
            num21 = condParam.wmin;
            num22 = condParam.wmax;
            str17 = string.Empty;
            if (titled == null)
            {
                goto Label_09C5;
            }
            str17 = LocalizedText.Get("sys.PARTYEDITOR_COND_WEIGHT");
        Label_09C5:
            if (num21 <= 0)
            {
                goto Label_09E7;
            }
            str17 = str17 + ((int) num21) + LocalizedText.Get("sys.KG_WEIGHT");
        Label_09E7:
            str17 = str17 + LocalizedText.Get("sys.TILDE");
            if (num22 <= 0)
            {
                goto Label_0A1C;
            }
            str17 = str17 + ((int) num22) + LocalizedText.Get("sys.KG_WEIGHT");
        Label_0A1C:
            list.Add(str17);
        Label_0A24:
            return list;
        }

        public int GetMissionValue(int index)
        {
            if (this.mission_values == null)
            {
                goto Label_0019;
            }
            if (index < ((int) this.mission_values.Length))
            {
                goto Label_001B;
            }
        Label_0019:
            return 0;
        Label_001B:
            return this.mission_values[index];
        }

        public PartyCondType GetPartyCondType()
        {
            PartySlotTypeUnitPair[] pairArray;
            bool flag;
            if (this.type != 6)
            {
                goto Label_0028;
            }
            if (this.EntryConditionCh == null)
            {
                goto Label_003F;
            }
            return this.EntryConditionCh.party_type;
            goto Label_003F;
        Label_0028:
            if (this.EntryCondition == null)
            {
                goto Label_003F;
            }
            return this.EntryCondition.party_type;
        Label_003F:
            if (this.questParty == null)
            {
                goto Label_0085;
            }
            if (<>f__am$cache4C != null)
            {
                goto Label_006F;
            }
            <>f__am$cache4C = new Func<PartySlotTypeUnitPair, bool>(QuestParam.<GetPartyCondType>m__264);
        Label_006F:
            if ((Enumerable.Any<PartySlotTypeUnitPair>(this.questParty.GetMainSubSlots(), <>f__am$cache4C) == 0) == null)
            {
                goto Label_0085;
            }
            return 2;
        Label_0085:
            return 0;
        }

        public unsafe void GetPartyTypes(out PlayerPartyTypes playerPartyType, out PlayerPartyTypes enemyPartyType)
        {
            QuestTypes types;
            switch ((this.type - 1))
            {
                case 0:
                    goto Label_0062;

                case 1:
                    goto Label_004C;

                case 2:
                    goto Label_0083;

                case 3:
                    goto Label_0083;

                case 4:
                    goto Label_0057;

                case 5:
                    goto Label_0083;

                case 6:
                    goto Label_006D;

                case 7:
                    goto Label_0083;

                case 8:
                    goto Label_0083;

                case 9:
                    goto Label_0078;

                case 10:
                    goto Label_0083;

                case 11:
                    goto Label_0083;

                case 12:
                    goto Label_0057;

                case 13:
                    goto Label_0062;
            }
            goto Label_0083;
        Label_004C:
            *((int*) playerPartyType) = 3;
            *((int*) enemyPartyType) = 4;
            goto Label_008E;
        Label_0057:
            *((int*) playerPartyType) = 1;
            *((int*) enemyPartyType) = 1;
            goto Label_008E;
        Label_0062:
            *((int*) playerPartyType) = 2;
            *((int*) enemyPartyType) = 2;
            goto Label_008E;
        Label_006D:
            *((int*) playerPartyType) = 6;
            *((int*) enemyPartyType) = 6;
            goto Label_008E;
        Label_0078:
            *((int*) playerPartyType) = 1;
            *((int*) enemyPartyType) = 1;
            goto Label_008E;
        Label_0083:
            *((int*) playerPartyType) = 0;
            *((int*) enemyPartyType) = 0;
        Label_008E:
            return;
        }

        public QuestCondParam GetQuestCondParam()
        {
            if (this.type != 6)
            {
                goto Label_0013;
            }
            return this.EntryConditionCh;
        Label_0013:
            return this.EntryCondition;
        }

        public static List<QuestParam> GetSameChapterIDQuestParam(string chapter_id)
        {
            List<QuestParam> list;
            GameManager manager;
            QuestParam[] paramArray;
            <GetSameChapterIDQuestParam>c__AnonStorey2E5 storeye;
            storeye = new <GetSameChapterIDQuestParam>c__AnonStorey2E5();
            storeye.chapter_id = chapter_id;
            list = null;
            manager = MonoSingleton<GameManager>.Instance;
            if ((manager == null) == null)
            {
                goto Label_0023;
            }
            return list;
        Label_0023:
            paramArray = Array.FindAll<QuestParam>(manager.Quests, new Predicate<QuestParam>(storeye.<>m__266));
            if (paramArray != null)
            {
                goto Label_0043;
            }
            return list;
        Label_0043:
            list = new List<QuestParam>(paramArray);
            return list;
        }

        public int GetSelectMainMemberNum()
        {
            PartyData data;
            int num;
            QuestTypes types;
            data = MonoSingleton<GameManager>.Instance.Player.Partys[GlobalVars.SelectedPartyIndex];
            num = 0;
            switch (this.type)
            {
                case 0:
                    goto Label_0073;

                case 1:
                    goto Label_0073;

                case 2:
                    goto Label_0081;

                case 3:
                    goto Label_0073;

                case 4:
                    goto Label_0081;

                case 5:
                    goto Label_0081;

                case 6:
                    goto Label_0073;

                case 7:
                    goto Label_0081;

                case 8:
                    goto Label_008D;

                case 9:
                    goto Label_008D;

                case 10:
                    goto Label_0081;

                case 11:
                    goto Label_0081;

                case 12:
                    goto Label_008D;

                case 13:
                    goto Label_0081;

                case 14:
                    goto Label_0073;

                case 15:
                    goto Label_0081;
            }
            goto Label_008D;
        Label_0073:
            num = data.MAX_MAINMEMBER - 1;
            goto Label_008D;
        Label_0081:
            num = data.MAX_MAINMEMBER;
        Label_008D:
            return num;
        }

        public void GotoEventListChapter()
        {
            FlowNode_Variable.Set("SHOW_CHAPTER", "1");
            GlobalVars.ReqEventPageListType = 0;
            GlobalVars.SelectedQuestID = null;
            GlobalVars.SelectedChapter.Set(null);
            GlobalVars.SelectedSection.Set("WD_DAILY");
            GlobalVars.SelectedStoryPart.Set(1);
            return;
        }

        public void GotoEventListQuest(string questID, string chapter)
        {
            ChapterParam param;
            param = (chapter == null) ? null : MonoSingleton<GameManager>.Instance.FindArea(chapter);
            if ((param == null) || (param.IsKeyQuest() == null))
            {
                goto Label_006E;
            }
            FlowNode_Variable.Set("SHOW_CHAPTER", "1");
            GlobalVars.ReqEventPageListType = 1;
            GlobalVars.SelectedQuestID = null;
            GlobalVars.SelectedChapter.Set(null);
            GlobalVars.SelectedSection.Set("WD_DAILY");
            GlobalVars.SelectedStoryPart.Set(1);
            goto Label_00CE;
        Label_006E:;
        Label_0089:
            FlowNode_Variable.Set("SHOW_CHAPTER", ((string.IsNullOrEmpty(questID) == null) && (string.IsNullOrEmpty(chapter) == null)) ? "0" : "1");
            GlobalVars.ReqEventPageListType = 0;
            GlobalVars.SelectedQuestID = questID;
            GlobalVars.SelectedChapter.Set(chapter);
            GlobalVars.SelectedSection.Set("WD_DAILY");
            GlobalVars.SelectedStoryPart.Set(1);
        Label_00CE:
            return;
        }

        public bool HasMission()
        {
            if (this.bonusObjective == null)
            {
                goto Label_001B;
            }
            if (((int) this.bonusObjective.Length) <= 0)
            {
                goto Label_001B;
            }
            return 1;
        Label_001B:
            return 0;
        }

        public bool IsAvailableUnit(UnitData unit)
        {
            return this.IsAvailableUnitInternal(this.EntryCondition, unit);
        }

        public bool IsAvailableUnitCh(UnitData unit)
        {
            return this.IsAvailableUnitInternal(this.EntryConditionCh, unit);
        }

        public bool IsAvailableUnitInternal(QuestCondParam condition, UnitData unit)
        {
            string str;
            string[] strArray;
            int num;
            if (condition != null)
            {
                goto Label_0008;
            }
            return 1;
        Label_0008:
            if (condition.unit == null)
            {
                goto Label_0021;
            }
            if (((int) condition.unit.Length) > 0)
            {
                goto Label_0023;
            }
        Label_0021:
            return 1;
        Label_0023:
            strArray = condition.unit;
            num = 0;
            goto Label_004C;
        Label_0031:
            str = strArray[num];
            if ((unit.UnitID == str) == null)
            {
                goto Label_0048;
            }
            return 1;
        Label_0048:
            num += 1;
        Label_004C:
            if (num < ((int) strArray.Length))
            {
                goto Label_0031;
            }
            return 0;
        }

        public bool IsCharacterQuest()
        {
            return (((this.type != 6) || (this.world == null)) ? 0 : (this.world == GameSettings.Instance.CharacterQuestSection));
        }

        public bool IsDateUnlock(long serverTime)
        {
            long num;
            GameManager manager;
            if (serverTime >= 0L)
            {
                goto Label_0013;
            }
            num = Network.GetServerTime();
            goto Label_0015;
        Label_0013:
            num = serverTime;
        Label_0015:
            if (MonoSingleton<GameManager>.Instance.Player.IsBeginner() != null)
            {
                goto Label_0038;
            }
            if (this.IsBeginner == null)
            {
                goto Label_0038;
            }
            return 0;
        Label_0038:
            if (this.IsJigen == null)
            {
                goto Label_005F;
            }
            if (this.start > num)
            {
                goto Label_005D;
            }
            if (num >= this.end)
            {
                goto Label_005D;
            }
            return 1;
        Label_005D:
            return 0;
        Label_005F:
            return (this.hidden == 0);
        }

        public unsafe bool IsEntryQuestCondition(UnitData unit)
        {
            string str;
            str = null;
            return this.IsEntryQuestCondition(this.EntryCondition, unit, &str);
        }

        public bool IsEntryQuestCondition(UnitData unit, ref string error)
        {
            return this.IsEntryQuestCondition(this.EntryCondition, unit, error);
        }

        public bool IsEntryQuestCondition(IEnumerable<UnitData> entryUnits, ref string error)
        {
            GameManager manager;
            int num;
            UnitData data;
            IEnumerator<UnitData> enumerator;
            bool flag;
            *(error) = string.Empty;
            if (this.EntryCondition != null)
            {
                goto Label_0014;
            }
            return 1;
        Label_0014:
            num = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
            if (this.EntryCondition.plvmax <= 0)
            {
                goto Label_0051;
            }
            if (num <= this.EntryCondition.plvmax)
            {
                goto Label_0051;
            }
            *(error) = "sys.PARTYEDITOR_PLV";
            return 0;
        Label_0051:
            if (this.EntryCondition.plvmin <= 0)
            {
                goto Label_007C;
            }
            if (num >= this.EntryCondition.plvmin)
            {
                goto Label_007C;
            }
            *(error) = "sys.PARTYEDITOR_PLV";
            return 0;
        Label_007C:
            enumerator = entryUnits.GetEnumerator();
        Label_0083:
            try
            {
                goto Label_00AF;
            Label_0088:
                data = enumerator.Current;
                if (data != null)
                {
                    goto Label_009A;
                }
                goto Label_00AF;
            Label_009A:
                if (this.IsEntryQuestCondition(data, error) != null)
                {
                    goto Label_00AF;
                }
                flag = 0;
                goto Label_00CC;
            Label_00AF:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0088;
                }
                goto Label_00CA;
            }
            finally
            {
            Label_00BF:
                if (enumerator != null)
                {
                    goto Label_00C3;
                }
            Label_00C3:
                enumerator.Dispose();
            }
        Label_00CA:
            return 1;
        Label_00CC:
            return flag;
        }

        private bool IsEntryQuestCondition(QuestCondParam condition, UnitData unit, ref string error)
        {
            int num;
            int num2;
            int num3;
            JobData data;
            JobParam param;
            int num4;
            UnitData data2;
            TowerResuponse resuponse;
            TowerResuponse.PlayerUnit unit2;
            <IsEntryQuestCondition>c__AnonStorey2E3 storeye;
            storeye = new <IsEntryQuestCondition>c__AnonStorey2E3();
            storeye.unit = unit;
            *(error) = string.Empty;
            if (condition != null)
            {
                goto Label_003F;
            }
            if (this.type != 7)
            {
                goto Label_003D;
            }
            DebugUtility.LogError("塔 コンディションが入っていません iname = " + this.iname);
        Label_003D:
            return 1;
        Label_003F:
            if (storeye.unit != null)
            {
                goto Label_004D;
            }
            return 0;
        Label_004D:
            if (condition.unit == null)
            {
                goto Label_008C;
            }
            if (((int) condition.unit.Length) <= 0)
            {
                goto Label_008C;
            }
            if (Array.IndexOf<string>(condition.unit, storeye.unit.UnitID) != -1)
            {
                goto Label_008C;
            }
            *(error) = "sys.PARTYEDITOR_UNIT";
            return 0;
        Label_008C:
            if (condition.sex == null)
            {
                goto Label_00BC;
            }
            if (condition.sex == storeye.unit.UnitParam.sex)
            {
                goto Label_00BC;
            }
            *(error) = "sys.PARTYEDITOR_SEX";
            return 0;
        Label_00BC:
            num = Math.Max(condition.rmax - 1, 0);
            if (condition.rmax <= 0)
            {
                goto Label_00F2;
            }
            if (storeye.unit.Rarity <= num)
            {
                goto Label_00F2;
            }
            *(error) = "sys.PARTYEDITOR_RARITY";
            return 0;
        Label_00F2:
            num2 = Math.Max(condition.rmin - 1, 0);
            if (condition.rmin <= 0)
            {
                goto Label_0128;
            }
            if (storeye.unit.Rarity >= num2)
            {
                goto Label_0128;
            }
            *(error) = "sys.PARTYEDITOR_RARITY";
            return 0;
        Label_0128:
            if (condition.hmax <= 0)
            {
                goto Label_016F;
            }
            if (storeye.unit.UnitParam.height == null)
            {
                goto Label_0166;
            }
            if (storeye.unit.UnitParam.height <= condition.hmax)
            {
                goto Label_016F;
            }
        Label_0166:
            *(error) = "sys.PARTYEDITOR_HEIGHT";
            return 0;
        Label_016F:
            if (condition.hmin <= 0)
            {
                goto Label_01B6;
            }
            if (storeye.unit.UnitParam.height == null)
            {
                goto Label_01AD;
            }
            if (storeye.unit.UnitParam.height >= condition.hmin)
            {
                goto Label_01B6;
            }
        Label_01AD:
            *(error) = "sys.PARTYEDITOR_HEIGHT";
            return 0;
        Label_01B6:
            if (condition.wmax <= 0)
            {
                goto Label_01FD;
            }
            if (storeye.unit.UnitParam.weight == null)
            {
                goto Label_01F4;
            }
            if (storeye.unit.UnitParam.weight <= condition.wmax)
            {
                goto Label_01FD;
            }
        Label_01F4:
            *(error) = "sys.PARTYEDITOR_WEIGHT";
            return 0;
        Label_01FD:
            if (condition.wmin <= 0)
            {
                goto Label_0244;
            }
            if (storeye.unit.UnitParam.weight == null)
            {
                goto Label_023B;
            }
            if (storeye.unit.UnitParam.weight >= condition.wmin)
            {
                goto Label_0244;
            }
        Label_023B:
            *(error) = "sys.PARTYEDITOR_WEIGHT";
            return 0;
        Label_0244:
            if (condition.jobset == null)
            {
                goto Label_02A7;
            }
            if (((int) condition.jobset.Length) <= 0)
            {
                goto Label_02A7;
            }
            if (Array.IndexOf<int>(condition.jobset, 1) == -1)
            {
                goto Label_02A7;
            }
            num3 = storeye.unit.JobIndex;
            if (num3 < 0)
            {
                goto Label_029E;
            }
            if (num3 >= ((int) condition.jobset.Length))
            {
                goto Label_029E;
            }
            if (condition.jobset[num3] != null)
            {
                goto Label_02A7;
            }
        Label_029E:
            *(error) = "sys.PARTYEDITOR_JOBINDEX";
            return 0;
        Label_02A7:
            if (condition.birth == null)
            {
                goto Label_02F0;
            }
            if (((int) condition.birth.Length) <= 0)
            {
                goto Label_02F0;
            }
            if (Array.IndexOf<string>(condition.birth, storeye.unit.UnitParam.birth) != -1)
            {
                goto Label_02F0;
            }
            *(error) = "sys.PARTYEDITOR_BIRTH";
            return 0;
        Label_02F0:
            if (condition.isElemLimit == null)
            {
                goto Label_031C;
            }
            if (condition.elem[storeye.unit.Element] != null)
            {
                goto Label_031C;
            }
            *(error) = "sys.PARTYEDITOR_ELEM";
            return 0;
        Label_031C:
            if (condition.job == null)
            {
                goto Label_03C2;
            }
            data = storeye.unit.CurrentJob;
            if (data == null)
            {
                goto Label_0345;
            }
            if (data.Param != null)
            {
                goto Label_034E;
            }
        Label_0345:
            *(error) = "sys.PARTYEDITOR_JOB";
            return 0;
        Label_034E:
            if (Array.IndexOf<string>(condition.job, data.JobID) != -1)
            {
                goto Label_03C2;
            }
            if (string.IsNullOrEmpty(data.Param.origin) == null)
            {
                goto Label_0383;
            }
            *(error) = "sys.PARTYEDITOR_JOB";
            return 0;
        Label_0383:
            param = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(data.Param.origin);
            if (param == null)
            {
                goto Label_03B9;
            }
            if (Array.IndexOf<string>(condition.job, param.iname) != -1)
            {
                goto Label_03C2;
            }
        Label_03B9:
            *(error) = "sys.PARTYEDITOR_JOB";
            return 0;
        Label_03C2:
            num4 = storeye.unit.CalcLevel();
            if (condition.ulvmax <= 0)
            {
                goto Label_03F2;
            }
            if (num4 <= condition.ulvmax)
            {
                goto Label_03F2;
            }
            *(error) = "sys.PARTYEDITOR_ULV";
            return 0;
        Label_03F2:
            if (condition.ulvmin <= 0)
            {
                goto Label_0414;
            }
            if (num4 >= condition.ulvmin)
            {
                goto Label_0414;
            }
            *(error) = "sys.PARTYEDITOR_ULV";
            return 0;
        Label_0414:
            if (this.type != 7)
            {
                goto Label_0493;
            }
            if (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(storeye.unit.UniqueID) == null)
            {
                goto Label_0493;
            }
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            if (resuponse.pdeck == null)
            {
                goto Label_0493;
            }
            unit2 = resuponse.pdeck.Find(new Predicate<TowerResuponse.PlayerUnit>(storeye.<>m__260));
            if (unit2 == null)
            {
                goto Label_0493;
            }
            if (unit2.isDied == null)
            {
                goto Label_0493;
            }
            *(error) = "sys.ERROR_TOWER_DEAD_UNIT";
            return 0;
        Label_0493:
            return 1;
        }

        public bool IsEntryQuestConditionCh(UnitData unit, ref string error)
        {
            return this.IsEntryQuestCondition(this.EntryConditionCh, unit, error);
        }

        public bool IsKeyUnlock(long serverTime)
        {
            long num;
            if (this.Chapter == null)
            {
                goto Label_002D;
            }
            if (serverTime >= 0L)
            {
                goto Label_001E;
            }
            num = Network.GetServerTime();
            goto Label_0020;
        Label_001E:
            num = serverTime;
        Label_0020:
            return this.Chapter.IsKeyUnlock(num);
        Label_002D:
            return 0;
        }

        public bool IsMissionClear(int index)
        {
            return (((this.clear_missions & (1 << (index & 0x1f))) == 0) == 0);
        }

        public bool IsMissionCompleteALL()
        {
            int num;
            if (this.bonusObjective == null)
            {
                goto Label_0042;
            }
            if (((int) this.bonusObjective.Length) <= 0)
            {
                goto Label_0042;
            }
            num = 0;
            goto Label_0032;
        Label_0020:
            if (this.IsMissionClear(num) != null)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            num += 1;
        Label_0032:
            if (num < ((int) this.bonusObjective.Length))
            {
                goto Label_0020;
            }
            return 1;
        Label_0042:
            return 0;
        }

        public bool IsQuestCondition()
        {
            GameManager manager;
            int num;
            QuestParam param;
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.IsBeginner() != null)
            {
                goto Label_0023;
            }
            if (this.IsBeginner == null)
            {
                goto Label_0023;
            }
            return 0;
        Label_0023:
            if (this.cond_quests == null)
            {
                goto Label_006A;
            }
            num = 0;
            goto Label_005C;
        Label_0035:
            param = manager.FindQuest(this.cond_quests[num]);
            if (param == null)
            {
                goto Label_0058;
            }
            if (param.state == 2)
            {
                goto Label_0058;
            }
            return 0;
        Label_0058:
            num += 1;
        Label_005C:
            if (num < ((int) this.cond_quests.Length))
            {
                goto Label_0035;
            }
        Label_006A:
            return 1;
        }

        public bool IsReplayDateUnlock(long serverTime)
        {
            if (this.replayLimit != null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            return this.IsDateUnlock(serverTime);
        }

        public bool IsUnitAllowed(UnitData unit)
        {
            string str;
            int num;
            int num2;
            if (unit != null)
            {
                goto Label_0008;
            }
            return 1;
        Label_0008:
            if (string.IsNullOrEmpty(this.AllowedJobs) != null)
            {
                goto Label_00A7;
            }
            if (unit.CurrentJob == null)
            {
                goto Label_00A7;
            }
            if (unit.CurrentJob.Param == null)
            {
                goto Label_00A7;
            }
            str = unit.CurrentJob.Param.iname;
            num = str.Length;
            num2 = this.AllowedJobs.IndexOf(str);
            if (num2 < 0)
            {
                goto Label_00A5;
            }
            if (0 >= num2)
            {
                goto Label_007B;
            }
            if (this.AllowedJobs[num2 - 1] != 0x2c)
            {
                goto Label_00A5;
            }
        Label_007B:
            if ((num2 + num) >= this.AllowedJobs.Length)
            {
                goto Label_00A7;
            }
            if (this.AllowedJobs[(num2 + num) - 1] == 0x2c)
            {
                goto Label_00A7;
            }
        Label_00A5:
            return 0;
        Label_00A7:
            if (this.AllowedTags == null)
            {
                goto Label_0114;
            }
            if (((byte) (this.AllowedTags & 1)) == null)
            {
                goto Label_00D3;
            }
            if (unit.UnitParam.sex == 1)
            {
                goto Label_00D3;
            }
            return 0;
        Label_00D3:
            if (((byte) (this.AllowedTags & 2)) == null)
            {
                goto Label_00F4;
            }
            if (unit.UnitParam.sex == 2)
            {
                goto Label_00F4;
            }
            return 0;
        Label_00F4:
            if (((byte) (this.AllowedTags & 4)) == null)
            {
                goto Label_0114;
            }
            if (unit.UnitParam.IsHero() != null)
            {
                goto Label_0114;
            }
            return 0;
        Label_0114:
            return 1;
        }

        public static PlayerPartyTypes QuestToPartyIndex(QuestTypes type)
        {
            PlayerPartyTypes types;
            QuestTypes types2;
            types2 = type;
            switch (types2)
            {
                case 0:
                    goto Label_0083;

                case 1:
                    goto Label_0058;

                case 2:
                    goto Label_005F;

                case 3:
                    goto Label_0083;

                case 4:
                    goto Label_0051;

                case 5:
                    goto Label_0083;

                case 6:
                    goto Label_0066;

                case 7:
                    goto Label_006D;

                case 8:
                    goto Label_0074;

                case 9:
                    goto Label_0074;

                case 10:
                    goto Label_0083;

                case 11:
                    goto Label_0051;

                case 12:
                    goto Label_0083;

                case 13:
                    goto Label_0083;

                case 14:
                    goto Label_0058;

                case 15:
                    goto Label_0083;

                case 0x10:
                    goto Label_007B;
            }
            goto Label_0083;
        Label_0051:
            types = 1;
            goto Label_008A;
        Label_0058:
            types = 2;
            goto Label_008A;
        Label_005F:
            types = 3;
            goto Label_008A;
        Label_0066:
            types = 5;
            goto Label_008A;
        Label_006D:
            types = 6;
            goto Label_008A;
        Label_0074:
            types = 7;
            goto Label_008A;
        Label_007B:
            types = 10;
            goto Label_008A;
        Label_0083:
            types = 0;
        Label_008A:
            return types;
        }

        public int RequiredApWithPlayerLv(int playerLv, bool campaign)
        {
            int num;
            QuestCampaignData[] dataArray;
            QuestCampaignData data;
            QuestCampaignData[] dataArray2;
            int num2;
            if (playerLv >= this.aplv)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            num = this.point;
            if (campaign == null)
            {
                goto Label_0066;
            }
            dataArray2 = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this);
            num2 = 0;
            goto Label_005C;
        Label_0031:
            data = dataArray2[num2];
            if (data.type != 4)
            {
                goto Label_0056;
            }
            num = Mathf.FloorToInt(((float) num) * data.GetRate());
            goto Label_0066;
        Label_0056:
            num2 += 1;
        Label_005C:
            if (num2 < ((int) dataArray2.Length))
            {
                goto Label_0031;
            }
        Label_0066:
            return num;
        }

        public void SetAtkTypeMag(int[] mags)
        {
            this.AtkTypeMags = mags;
            return;
        }

        public void SetChallangeCount(int count)
        {
            if (this.IsKeyQuest == null)
            {
                goto Label_002F;
            }
            if (this.Chapter == null)
            {
                goto Label_002F;
            }
            if (this.Chapter.GetKeyQuestType() != 2)
            {
                goto Label_002F;
            }
            this.key_cnt = count;
            return;
        Label_002F:
            this.dailyCount = CheckCast.to_short(count);
            return;
        }

        public void SetMissionFlag(int index, bool isClear)
        {
            if (this.bonusObjective == null)
            {
                goto Label_0019;
            }
            if (index < ((int) this.bonusObjective.Length))
            {
                goto Label_001A;
            }
        Label_0019:
            return;
        Label_001A:
            if (isClear == null)
            {
                goto Label_003B;
            }
            this.clear_missions |= 1 << ((index & 0x1f) & 0x1f);
            goto Label_0052;
        Label_003B:
            this.clear_missions &= ~(1 << ((index & 0x1f) & 0x1f));
        Label_0052:
            return;
        }

        public void SetMissionValue(int index, int value)
        {
            if (this.mission_values == null)
            {
                goto Label_0019;
            }
            if (index < ((int) this.mission_values.Length))
            {
                goto Label_001A;
            }
        Label_0019:
            return;
        Label_001A:
            this.mission_values[index] = value;
            return;
        }

        public static unsafe QuestTypes ToQuestType(string name)
        {
            string str;
            Dictionary<string, int> dictionary;
            int num;
            str = name;
            if (str == null)
            {
                goto Label_0127;
            }
            if (<>f__switch$map11 != null)
            {
                goto Label_00BF;
            }
            dictionary = new Dictionary<string, int>(13);
            dictionary.Add("Story", 0);
            dictionary.Add("multi", 1);
            dictionary.Add("Arena", 2);
            dictionary.Add("Tutorial", 3);
            dictionary.Add("Free", 4);
            dictionary.Add("Event", 5);
            dictionary.Add("Character", 6);
            dictionary.Add("tower", 7);
            dictionary.Add("vs", 8);
            dictionary.Add("vs_tower", 8);
            dictionary.Add("rm", 9);
            dictionary.Add("multi_tower", 10);
            dictionary.Add("multi_areaquest", 11);
            <>f__switch$map11 = dictionary;
        Label_00BF:
            if (<>f__switch$map11.TryGetValue(str, &num) == null)
            {
                goto Label_0127;
            }
            switch (num)
            {
                case 0:
                    goto Label_010C;

                case 1:
                    goto Label_010E;

                case 2:
                    goto Label_0110;

                case 3:
                    goto Label_0112;

                case 4:
                    goto Label_0114;

                case 5:
                    goto Label_0116;

                case 6:
                    goto Label_0118;

                case 7:
                    goto Label_011A;

                case 8:
                    goto Label_011C;

                case 9:
                    goto Label_011E;

                case 10:
                    goto Label_0121;

                case 11:
                    goto Label_0124;
            }
            goto Label_0127;
        Label_010C:
            return 0;
        Label_010E:
            return 1;
        Label_0110:
            return 2;
        Label_0112:
            return 3;
        Label_0114:
            return 4;
        Label_0116:
            return 5;
        Label_0118:
            return 6;
        Label_011A:
            return 7;
        Label_011C:
            return 8;
        Label_011E:
            return 0x10;
        Label_0121:
            return 12;
        Label_0124:
            return 14;
        Label_0127:
            return 0;
        }

        public bool TransSectionGotoElite(UIUtility.DialogResultEvent callback)
        {
            PlayerData data;
            QuestParam[] paramArray;
            QuestParam param;
            int num;
            string str;
            string str2;
            string str3;
            int num2;
            ChapterParam[] paramArray2;
            int num3;
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            param = null;
            num = ((int) paramArray.Length) - 1;
            goto Label_003A;
        Label_001F:
            if (paramArray[num].difficulty != 1)
            {
                goto Label_0036;
            }
            param = paramArray[num];
            goto Label_0041;
        Label_0036:
            num -= 1;
        Label_003A:
            if (num >= 0)
            {
                goto Label_001F;
            }
        Label_0041:
            if (param != null)
            {
                goto Label_0069;
            }
            this.TransSectionGotoNormal();
            str = LocalizedText.Get("sys.EQUEST_UNAVAILABLE");
            UIUtility.SystemMessage(null, str, callback, null, 0, -1);
            return 0;
        Label_0069:
            str2 = param.ChapterID;
            str3 = "WD_01";
            num2 = 1;
            paramArray2 = MonoSingleton<GameManager>.Instance.Chapters;
            num3 = 0;
            goto Label_00CD;
        Label_008F:
            if ((paramArray2[num3].iname == str2) == null)
            {
                goto Label_00C7;
            }
            str3 = paramArray2[num3].section;
            num2 = paramArray2[num3].sectionParam.storyPart;
            goto Label_00D8;
        Label_00C7:
            num3 += 1;
        Label_00CD:
            if (num3 < ((int) paramArray2.Length))
            {
                goto Label_008F;
            }
        Label_00D8:
            GlobalVars.SelectedQuestID = param.iname;
            GlobalVars.SelectedChapter.Set(str2);
            GlobalVars.SelectedSection.Set(str3);
            GlobalVars.SelectedStoryPart.Set(num2);
            return 1;
        }

        public bool TransSectionGotoEvent(string questID, UIUtility.DialogResultEvent callback)
        {
            PlayerData data;
            QuestParam param;
            string str;
            data = MonoSingleton<GameManager>.Instance.Player;
            param = MonoSingleton<GameManager>.Instance.FindQuest(questID);
            if (data.IsQuestAvailable(questID) != null)
            {
                goto Label_0042;
            }
            str = LocalizedText.Get("sys.EVENT_UNAVAILABLE");
            UIUtility.SystemMessage(null, str, callback, null, 0, -1);
            this.GotoEventListChapter();
            return 0;
        Label_0042:
            this.GotoEventListQuest(questID, (param == null) ? null : param.ChapterID);
            return 1;
        }

        public bool TransSectionGotoNormal()
        {
            FlowNode_SelectLatestChapter.SelectLatestChapter();
            return 1;
        }

        public unsafe bool TransSectionGotoQuest(string questID, out QuestTypes quest_type, UIUtility.DialogResultEvent callback)
        {
            GameManager manager;
            QuestParam param;
            string str;
            QuestTypes types;
            manager = MonoSingleton<GameManager>.Instance;
            *((sbyte*) quest_type) = 0;
            if (string.IsNullOrEmpty(questID) == null)
            {
                goto Label_0020;
            }
            this.TransSectionGotoNormal();
            *((sbyte*) quest_type) = 0;
            return 1;
        Label_0020:
            param = manager.FindQuest(questID);
            if (param != null)
            {
                goto Label_0051;
            }
            this.TransSectionGotoNormal();
            *((sbyte*) quest_type) = 0;
            str = LocalizedText.Get("sys.QUEST_UNAVAILABLE");
            UIUtility.SystemMessage(null, str, callback, null, 0, -1);
            return 0;
        Label_0051:
            switch ((param.type - 1))
            {
                case 0:
                    goto Label_009D;

                case 1:
                    goto Label_0102;

                case 2:
                    goto Label_0102;

                case 3:
                    goto Label_0102;

                case 4:
                    goto Label_00BC;

                case 5:
                    goto Label_00EB;

                case 6:
                    goto Label_00A5;

                case 7:
                    goto Label_0102;

                case 8:
                    goto Label_0102;

                case 9:
                    goto Label_00BC;

                case 10:
                    goto Label_0102;

                case 11:
                    goto Label_0102;

                case 12:
                    goto Label_00D3;

                case 13:
                    goto Label_009D;
            }
            goto Label_0102;
        Label_009D:
            *((sbyte*) quest_type) = 1;
            goto Label_0119;
        Label_00A5:
            *((sbyte*) quest_type) = 7;
            if (this.TransSectionGotoTower(questID, quest_type) != null)
            {
                goto Label_0119;
            }
            return 0;
            goto Label_0119;
        Label_00BC:
            *((sbyte*) quest_type) = 5;
            if (this.TransSectionGotoEvent(questID, callback) != null)
            {
                goto Label_0119;
            }
            return 0;
            goto Label_0119;
        Label_00D3:
            *((sbyte*) quest_type) = 13;
            if (this.TransSectionGotoEvent(questID, callback) != null)
            {
                goto Label_0119;
            }
            return 0;
            goto Label_0119;
        Label_00EB:
            *((sbyte*) quest_type) = 6;
            if (this.TransSelectionGotoCharacter(questID, callback) != null)
            {
                goto Label_0119;
            }
            return 0;
            goto Label_0119;
        Label_0102:
            *((sbyte*) quest_type) = 0;
            if (this.TransSectionGotoStory(questID, callback) != null)
            {
                goto Label_0119;
            }
            return 0;
        Label_0119:
            return 1;
        }

        public bool TransSectionGotoStory(string questID, UIUtility.DialogResultEvent callback)
        {
            PlayerData data;
            QuestParam param;
            string str;
            string str2;
            string str3;
            int num;
            ChapterParam[] paramArray;
            int num2;
            data = MonoSingleton<GameManager>.Instance.Player;
            param = MonoSingleton<GameManager>.Instance.FindQuest(questID);
            if (data.IsQuestAvailable(questID) != null)
            {
                goto Label_0043;
            }
            str = LocalizedText.Get("sys.QUEST_UNAVAILABLE");
            UIUtility.SystemMessage(null, str, callback, null, 0, -1);
            this.TransSectionGotoNormal();
            return 0;
        Label_0043:
            str2 = (param == null) ? null : param.ChapterID;
            str3 = "WD_01";
            num = 1;
            paramArray = MonoSingleton<GameManager>.Instance.Chapters;
            num2 = 0;
            goto Label_00B1;
        Label_0074:
            if ((paramArray[num2].iname == str2) == null)
            {
                goto Label_00AB;
            }
            str3 = paramArray[num2].section;
            num = paramArray[num2].sectionParam.storyPart;
            goto Label_00BC;
        Label_00AB:
            num2 += 1;
        Label_00B1:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0074;
            }
        Label_00BC:
            GlobalVars.SelectedQuestID = questID;
            GlobalVars.SelectedChapter.Set(str2);
            GlobalVars.SelectedSection.Set(str3);
            GlobalVars.SelectedStoryPart.Set(num);
            return 1;
        }

        public bool TransSectionGotoStoryExtra(UIUtility.DialogResultEvent callback)
        {
            PlayerData data;
            QuestParam[] paramArray;
            QuestParam param;
            int num;
            string str;
            string str2;
            string str3;
            int num2;
            ChapterParam[] paramArray2;
            int num3;
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            param = null;
            num = ((int) paramArray.Length) - 1;
            goto Label_003A;
        Label_001F:
            if (paramArray[num].difficulty != 2)
            {
                goto Label_0036;
            }
            param = paramArray[num];
            goto Label_0041;
        Label_0036:
            num -= 1;
        Label_003A:
            if (num >= 0)
            {
                goto Label_001F;
            }
        Label_0041:
            if (param != null)
            {
                goto Label_0069;
            }
            this.TransSectionGotoNormal();
            str = LocalizedText.Get("sys.EQUEST_UNAVAILABLE");
            UIUtility.SystemMessage(null, str, callback, null, 0, -1);
            return 0;
        Label_0069:
            str2 = param.ChapterID;
            str3 = "WD_01";
            num2 = 1;
            paramArray2 = MonoSingleton<GameManager>.Instance.Chapters;
            num3 = 0;
            goto Label_00CD;
        Label_008F:
            if ((paramArray2[num3].iname == str2) == null)
            {
                goto Label_00C7;
            }
            str3 = paramArray2[num3].section;
            num2 = paramArray2[num3].sectionParam.storyPart;
            goto Label_00D8;
        Label_00C7:
            num3 += 1;
        Label_00CD:
            if (num3 < ((int) paramArray2.Length))
            {
                goto Label_008F;
            }
        Label_00D8:
            GlobalVars.SelectedQuestID = param.iname;
            GlobalVars.SelectedChapter.Set(str2);
            GlobalVars.SelectedSection.Set(str3);
            GlobalVars.SelectedStoryPart.Set(num2);
            return 1;
        }

        public unsafe bool TransSectionGotoTower(string questID, out QuestTypes quest_type)
        {
            TowerFloorParam param;
            PlayerData data;
            *((sbyte*) quest_type) = 7;
            param = null;
            if (string.IsNullOrEmpty(questID) != null)
            {
                goto Label_0048;
            }
            param = MonoSingleton<GameManager>.Instance.FindTowerFloor(questID);
            if (param != null)
            {
                goto Label_0048;
            }
            DebugUtility.LogError("[クエストID = " + questID + "]が見つかりません。");
            this.GotoEventListChapter();
            GlobalVars.ReqEventPageListType = 2;
            *((sbyte*) quest_type) = 5;
            return 1;
        Label_0048:
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data.CheckUnlock(8) == null)
            {
                goto Label_0089;
            }
            if (param == null)
            {
                goto Label_0075;
            }
            GlobalVars.SelectedTowerID = param.tower_id;
            goto Label_0084;
        Label_0075:
            this.GotoEventListChapter();
            GlobalVars.ReqEventPageListType = 2;
            *((sbyte*) quest_type) = 5;
        Label_0084:
            goto Label_009E;
        Label_0089:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 8);
            return 0;
        Label_009E:
            return 1;
        }

        public bool TransSelectionGotoCharacter(string questID, UIUtility.DialogResultEvent callback)
        {
            PlayerData data;
            string str;
            CollaboSkillParam.Pair pair;
            data = MonoSingleton<GameManager>.Instance.Player;
            FlowNode_Variable.Set("CHARA_QUEST_FROM_MISSION", "0");
            if (data.IsQuestAvailable(questID) != null)
            {
                goto Label_003F;
            }
            str = LocalizedText.Get("sys.CHARACTER_UNAVAILABLE");
            UIUtility.SystemMessage(null, str, callback, null, 0, -1);
            return 0;
        Label_003F:
            FlowNode_Variable.Set("CHARA_QUEST_FROM_MISSION", "1");
            pair = GetCollaboSkillQuest(questID);
            if (pair == null)
            {
                goto Label_0075;
            }
            FlowNode_Variable.Set("CHARA_QUEST_TYPE", "COLLABO");
            GlobalVars.SelectedCollaboSkillPair = pair;
            goto Label_0084;
        Label_0075:
            FlowNode_Variable.Set("CHARA_QUEST_TYPE", "CHARA");
        Label_0084:
            return 1;
        }

        public string cond
        {
            get
            {
                return Singleton<ShareVariable>.Instance.str.Get(0, this.cond_index);
            }
            set
            {
                this.cond_index = Singleton<ShareVariable>.Instance.str.Set(0, value);
                return;
            }
        }

        public string world
        {
            get
            {
                return Singleton<ShareVariable>.Instance.str.Get(1, this.world_index);
            }
            set
            {
                this.world_index = Singleton<ShareVariable>.Instance.str.Set(1, value);
                return;
            }
        }

        public string ChapterID
        {
            get
            {
                return Singleton<ShareVariable>.Instance.str.Get(2, this.ChapterID_index);
            }
            set
            {
                this.ChapterID_index = Singleton<ShareVariable>.Instance.str.Set(2, value);
                return;
            }
        }

        public bool notSearch
        {
            get
            {
                return this.bit_array.Get(1);
            }
            set
            {
                this.bit_array.Set(1, value);
                return;
            }
        }

        public int dayReset
        {
            [CompilerGenerated]
            get
            {
                return this.<dayReset>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<dayReset>k__BackingField = value;
                return;
            }
        }

        public bool IsMulti
        {
            get
            {
                return ((this.multi == 0) == 0);
            }
        }

        public bool IsMultiEvent
        {
            get
            {
                return ((this.multi < 100) == 0);
            }
        }

        public bool IsMultiVersus
        {
            get
            {
                return ((this.multi == 2) ? 1 : (this.multi == 0x66));
            }
        }

        public bool IsMultiAreaQuest
        {
            get
            {
                return (this.type == 14);
            }
        }

        public string ticket
        {
            get
            {
                return Singleton<ShareVariable>.Instance.str.Get(4, this.ticket_index);
            }
            set
            {
                this.ticket_index = Singleton<ShareVariable>.Instance.str.Set(4, value);
                return;
            }
        }

        public bool AllowRetreat
        {
            get
            {
                return this.bit_array.Get(5);
            }
            set
            {
                this.bit_array.Set(5, value);
                return;
            }
        }

        public bool AllowAutoPlay
        {
            get
            {
                return this.bit_array.Get(6);
            }
            set
            {
                this.bit_array.Set(6, value);
                return;
            }
        }

        public bool FirstAutoPlayProhibit
        {
            get
            {
                return this.bit_array.Get(0x10);
            }
            set
            {
                this.bit_array.Set(0x10, value);
                return;
            }
        }

        public bool Silent
        {
            get
            {
                return this.bit_array.Get(7);
            }
            set
            {
                this.bit_array.Set(7, value);
                return;
            }
        }

        public bool DisableAbilities
        {
            get
            {
                return this.bit_array.Get(8);
            }
            set
            {
                this.bit_array.Set(8, value);
                return;
            }
        }

        public bool DisableItems
        {
            get
            {
                return this.bit_array.Get(9);
            }
            set
            {
                this.bit_array.Set(9, value);
                return;
            }
        }

        public bool DisableContinue
        {
            get
            {
                return this.bit_array.Get(10);
            }
            set
            {
                this.bit_array.Set(10, value);
                return;
            }
        }

        public bool IsUnitChange
        {
            get
            {
                return this.bit_array.Get(14);
            }
            set
            {
                this.bit_array.Set(14, value);
                return;
            }
        }

        public bool IsMultiLeaderSkill
        {
            get
            {
                return this.bit_array.Get(15);
            }
            set
            {
                this.bit_array.Set(15, value);
                return;
            }
        }

        public bool IsWeatherNoChange
        {
            get
            {
                return this.bit_array.Get(0x11);
            }
            set
            {
                this.bit_array.Set(0x11, value);
                return;
            }
        }

        public bool hidden
        {
            get
            {
                return this.bit_array.Get(3);
            }
            set
            {
                this.bit_array.Set(3, value);
                return;
            }
        }

        public bool replayLimit
        {
            get
            {
                return this.bit_array.Get(4);
            }
            set
            {
                this.bit_array.Set(4, value);
                return;
            }
        }

        public bool ShowReviewPopup
        {
            get
            {
                return this.bit_array.Get(0);
            }
            set
            {
                this.bit_array.Set(0, value);
                return;
            }
        }

        public bool IsScenario
        {
            get
            {
                return ((this.map.Count == null) ? 1 : string.IsNullOrEmpty(this.map[0].mapSetName));
            }
        }

        public bool IsStory
        {
            get
            {
                return (this.type == 0);
            }
        }

        public bool IsGps
        {
            get
            {
                return (this.type == 10);
            }
        }

        public bool IsVersus
        {
            get
            {
                return ((this.type == 8) ? 1 : (this.type == 9));
            }
        }

        public bool IsRankMatch
        {
            get
            {
                return (this.type == 0x10);
            }
        }

        public bool IsKeyQuest
        {
            get
            {
                return ((this.Chapter == null) ? 0 : this.Chapter.IsKeyQuest());
            }
        }

        public bool IsQuestDrops
        {
            get
            {
                return (((((this.type == null) || (this.type == 4)) || ((this.type == 11) || (this.type == 6))) || (((this.type == 5) || (this.type == 1)) || ((this.type == 10) || (this.type == 13)))) ? 1 : (this.type == 14));
            }
        }

        public bool IsMultiTower
        {
            get
            {
                return (this.type == 12);
            }
        }

        public int GainPlayerExp
        {
            get
            {
                return this.pexp;
            }
        }

        public int GainUnitExp
        {
            get
            {
                return this.uexp;
            }
        }

        public int OverClockTimeWin
        {
            get
            {
                return this.clock_win;
            }
        }

        public int OverClockTimeLose
        {
            get
            {
                return this.clock_lose;
            }
        }

        public bool IsBeginner
        {
            get
            {
                return this.bit_array.Get(2);
            }
            set
            {
                this.bit_array.Set(2, value);
                return;
            }
        }

        public bool UseFixEditor
        {
            get
            {
                return this.bit_array.Get(11);
            }
            set
            {
                this.bit_array.Set(11, value);
                return;
            }
        }

        public bool IsNoStartVoice
        {
            get
            {
                return this.bit_array.Get(12);
            }
            set
            {
                this.bit_array.Set(12, value);
                return;
            }
        }

        public bool UseSupportUnit
        {
            get
            {
                return this.bit_array.Get(13);
            }
            set
            {
                this.bit_array.Set(13, value);
                return;
            }
        }

        public int MissionNum
        {
            get
            {
                if (this.HasMission() != null)
                {
                    goto Label_000D;
                }
                return 0;
            Label_000D:
                return (int) this.bonusObjective.Length;
            }
        }

        public bool IsJigen
        {
            get
            {
                return ((this.end == 0L) == 0);
            }
        }

        [CompilerGenerated]
        private sealed class <GetCollaboSkillQuest>c__AnonStorey2E4
        {
            internal string quest_id;

            public <GetCollaboSkillQuest>c__AnonStorey2E4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__265(QuestParam p)
            {
                return (p.iname == this.quest_id);
            }
        }

        [CompilerGenerated]
        private sealed class <GetSameChapterIDQuestParam>c__AnonStorey2E5
        {
            internal string chapter_id;

            public <GetSameChapterIDQuestParam>c__AnonStorey2E5()
            {
                base..ctor();
                return;
            }

            internal bool <>m__266(QuestParam p)
            {
                return (p.ChapterID == this.chapter_id);
            }
        }

        [CompilerGenerated]
        private sealed class <IsEntryQuestCondition>c__AnonStorey2E3
        {
            internal UnitData unit;

            public <IsEntryQuestCondition>c__AnonStorey2E3()
            {
                base..ctor();
                return;
            }

            internal bool <>m__260(TowerResuponse.PlayerUnit x)
            {
                return (this.unit.UnitParam.iname == x.unitname);
            }
        }

        private enum BitType
        {
            ShowReviewPopup,
            notSearch,
            IsBeginner,
            hidden,
            replayLimit,
            AllowRetreat,
            AllowAutoPlay,
            Silent,
            DisableAbilities,
            DisableItems,
            DisableContinue,
            UseFixEditor,
            IsNoStartVoice,
            UseSupportUnit,
            UnitChange,
            IsMultiLeaderSkill,
            FirstAutoPlayProbihit,
            IsWeatherNoChange,
            MAX_BIT_ARRAY
        }

        [Flags]
        public enum Tags : byte
        {
            MAL = 1,
            FEM = 2,
            HERO = 4
        }
    }
}

