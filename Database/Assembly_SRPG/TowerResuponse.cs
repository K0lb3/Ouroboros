namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class TowerResuponse
    {
        public long rtime;
        public Status status;
        public List<PlayerUnit> pdeck;
        public List<EnemyUnit> edeck;
        private TowerFloorParam currentFloor;
        public string TowerID;
        public short reset_cost;
        public byte round;
        public bool is_reset;
        public int arrived_num;
        public int clear;
        public int speedRank;
        public int techRank;
        public int turn_num;
        public int died_num;
        public int retire_num;
        public int recover_num;
        public int challenge_num;
        public int lose_num;
        public int reset_num;
        public int spd_score;
        public int tec_score;
        public int ret_score;
        public int rcv_score;
        public int challenge_score;
        public int lose_score;
        public int reset_score;
        public TowerRankParam[] SpdRank;
        public TowerRankParam[] TecRank;
        public RandDeckResult[] lot_enemies;
        public TowerScore.ViewParam FloorScores;
        public TowerRankParam[] FloorSpdRank;
        public TowerRankParam[] FloorTecRank;
        [CompilerGenerated]
        private static Predicate<Unit> <>f__am$cache21;
        [CompilerGenerated]
        private static Predicate<UnitData> <>f__am$cache22;

        public TowerResuponse()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <CalcEnemyDamage>m__1BF(Unit x)
        {
            return ((x.IsGimmick != null) ? 0 : ((x.CurrentStatus.param.hp > 0) == 0));
        }

        [CompilerGenerated]
        private static bool <GetAvailableUnits>m__1C0(UnitData unitData)
        {
            return ((unitData.Lv < 20) == 0);
        }

        public void CalcDamage(List<Unit> player)
        {
            Unit unit;
            int num;
            <CalcDamage>c__AnonStorey278 storey;
            if (this.pdeck != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            storey = new <CalcDamage>c__AnonStorey278();
            storey.<>f__this = this;
            storey.i = 0;
            goto Label_008C;
        Label_0025:
            unit = player.Find(new Predicate<Unit>(storey.<>m__1BE));
            if (unit != null)
            {
                goto Label_0043;
            }
            goto Label_007E;
        Label_0043:
            num = Mathf.Min(this.pdeck[storey.i].dmg, unit.MaximumStatus.param.hp - 1);
            unit.Damage(num, 0);
        Label_007E:
            storey.i += 1;
        Label_008C:
            if (storey.i < this.pdeck.Count)
            {
                goto Label_0025;
            }
            return;
        }

        public void CalcEnemyDamage(List<Unit> enemy, bool is_menu)
        {
            List<Unit> list;
            int num;
            int num2;
            Unit unit;
            int num3;
            if (this.edeck != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            list = new List<Unit>();
            num = 0;
            goto Label_011F;
        Label_0019:
            num2 = int.Parse(this.edeck[num].eid);
            if (num2 < 0)
            {
                goto Label_011B;
            }
            if (enemy.Count > num2)
            {
                goto Label_0048;
            }
            goto Label_011B;
        Label_0048:
            unit = enemy[num2];
            if (unit != null)
            {
                goto Label_005B;
            }
            goto Label_011B;
        Label_005B:
            if (is_menu == null)
            {
                goto Label_0071;
            }
            if (unit.IsGimmick == null)
            {
                goto Label_0071;
            }
            goto Label_011B;
        Label_0071:
            if (unit.IsGimmick == null)
            {
                goto Label_009E;
            }
            if (this.edeck[num].hp != null)
            {
                goto Label_009E;
            }
            list.Add(unit);
            goto Label_011B;
        Label_009E:
            unit.Damage(unit.MaximumStatus.param.hp - this.edeck[num].hp, 0);
            unit.Gems = this.edeck[num].jewel;
            this.edeck[num].id = unit.UnitData.UniqueID;
            this.edeck[num].iname = unit.UnitParam.iname;
        Label_011B:
            num += 1;
        Label_011F:
            if (num < this.edeck.Count)
            {
                goto Label_0019;
            }
            if (<>f__am$cache21 != null)
            {
                goto Label_0149;
            }
            <>f__am$cache21 = new Predicate<Unit>(TowerResuponse.<CalcEnemyDamage>m__1BF);
        Label_0149:
            enemy.RemoveAll(<>f__am$cache21);
            num3 = 0;
            goto Label_0183;
        Label_015C:
            if (list[num3] != null)
            {
                goto Label_016E;
            }
            goto Label_017D;
        Label_016E:
            enemy.Remove(list[num3]);
        Label_017D:
            num3 += 1;
        Label_0183:
            if (num3 < list.Count)
            {
                goto Label_015C;
            }
            return;
        }

        public unsafe int CalcRecoverCost()
        {
            TowerParam param;
            DateTime time;
            DateTime time2;
            TimeSpan span;
            double num;
            double num2;
            double num3;
            double num4;
            DateTime time3;
            param = MonoSingleton<GameManager>.Instance.FindTower(this.TowerID);
            if (param != null)
            {
                goto Label_0019;
            }
            return 0;
        Label_0019:
            time = TimeManager.ServerTime;
            span = &TimeManager.FromUnixTime(this.rtime).AddMinutes(-1.0) - time;
            num = Math.Ceiling(&span.TotalMinutes);
            num2 = (double) param.unit_recover_minute;
            num3 = num / num2;
            num4 = ((double) param.unit_recover_coin) * num3;
            return Mathf.Clamp((int) Math.Ceiling(num4), 0, param.unit_recover_coin);
        }

        public bool CheckEnemyDeck()
        {
            return ((this.edeck == null) ? 0 : 1);
        }

        public void Deserialize(JSON_ReqTowerResuponse res)
        {
            TowerFloorParam param;
            JSON_ReqTowerResuponse.Json_TowerStatus status;
            int num;
            int num2;
            RandDeckResult result;
            if (res != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.TowerID = GlobalVars.SelectedTowerID;
            this.rtime = res.rtime;
            if (res.stats == null)
            {
                goto Label_003A;
            }
            this.Deserialize(res.stats);
            goto Label_0071;
        Label_003A:
            param = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(this.TowerID);
            if (param == null)
            {
                goto Label_0071;
            }
            status = new JSON_ReqTowerResuponse.Json_TowerStatus();
            status.fname = param.iname;
            status.questStates = 0;
            this.Deserialize(status);
        Label_0071:
            if (res.pdeck == null)
            {
                goto Label_010A;
            }
            this.pdeck = new List<PlayerUnit>();
            num = 0;
            goto Label_00FC;
        Label_008E:
            this.pdeck.Add(new PlayerUnit());
            this.pdeck[num].dmg = res.pdeck[num].damage;
            this.pdeck[num].unitname = res.pdeck[num].uname;
            this.pdeck[num].is_died = res.pdeck[num].is_died;
            num += 1;
        Label_00FC:
            if (num < ((int) res.pdeck.Length))
            {
                goto Label_008E;
            }
        Label_010A:
            this.reset_cost = res.reset_cost;
            this.round = res.round;
            this.is_reset = res.is_reset == 1;
            if (res.lot_enemies == null)
            {
                goto Label_01AF;
            }
            if (((int) res.lot_enemies.Length) <= 0)
            {
                goto Label_01AF;
            }
            this.lot_enemies = new RandDeckResult[(int) res.lot_enemies.Length];
            num2 = 0;
            goto Label_01A1;
        Label_0164:
            result = new RandDeckResult();
            result.id = res.lot_enemies[num2].id;
            result.set_id = res.lot_enemies[num2].set_id;
            this.lot_enemies[num2] = result;
            num2 += 1;
        Label_01A1:
            if (num2 < ((int) res.lot_enemies.Length))
            {
                goto Label_0164;
            }
        Label_01AF:
            this.Deserialize(res.edeck);
            this.Deserialize(res.rank);
            this.UpdateCurrentFloor();
            return;
        }

        private void Deserialize(JSON_ReqTowerResuponse.Json_RankStatus json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.turn_num = json.turn_num;
            this.died_num = json.died_num;
            this.retire_num = json.retire_num;
            this.recover_num = json.recovery_num;
            this.speedRank = json.spd_rank;
            this.techRank = json.tec_rank;
            this.spd_score = json.spd_score;
            this.tec_score = json.tec_score;
            this.ret_score = json.ret_score;
            this.rcv_score = json.rcv_score;
            this.challenge_num = json.challenge_num;
            this.lose_num = json.lose_num;
            this.reset_num = json.reset_num;
            this.challenge_score = json.challenge_score;
            this.lose_score = json.lose_score;
            this.reset_score = json.reset_score;
            return;
        }

        public unsafe void Deserialize(JSON_ReqTowerResuponse.Json_TowerEnemyUnit[] res)
        {
            int num;
            if (res == null)
            {
                goto Label_0085;
            }
            this.edeck = new List<EnemyUnit>();
            num = 0;
            goto Label_007C;
        Label_0018:
            this.edeck.Add(new EnemyUnit());
            this.edeck[num].hp = res[num].hp;
            this.edeck[num].jewel = res[num].jewel;
            this.edeck[num].eid = &res[num].eid.ToString();
            num += 1;
        Label_007C:
            if (num < ((int) res.Length))
            {
                goto Label_0018;
            }
        Label_0085:
            return;
        }

        public void Deserialize(JSON_ReqTowerResuponse.Json_TowerPlayerUnit[] res)
        {
            int num;
            if (res != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (res == null)
            {
                goto Label_0087;
            }
            this.pdeck = new List<PlayerUnit>();
            num = 0;
            goto Label_007E;
        Label_001F:
            this.pdeck.Add(new PlayerUnit());
            this.pdeck[num].dmg = res[num].damage;
            this.pdeck[num].unitname = res[num].uname;
            this.pdeck[num].is_died = res[num].is_died;
            num += 1;
        Label_007E:
            if (num < ((int) res.Length))
            {
                goto Label_001F;
            }
        Label_0087:
            return;
        }

        public unsafe void Deserialize(JSON_ReqTowerResuponse.Json_TowerStatus json)
        {
            TowerFloorParam param;
            List<TowerFloorParam> list;
            List<QuestParam> list2;
            short num;
            QuestParam param2;
            QuestParam param3;
            List<QuestParam>.Enumerator enumerator;
            this.status = new Status();
            this.status.fname = json.fname;
            this.status.state = json.questStates;
            param = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.status.fname);
            if (param != null)
            {
                goto Label_004A;
            }
            return;
        Label_004A:
            list = MonoSingleton<GameManager>.Instance.FindTowerFloors(param.tower_id);
            list2 = new List<QuestParam>();
            num = 0;
            goto Label_008C;
        Label_0068:
            list[num].FloorIndex = num;
            list2.Add(list[num].GetQuestParam());
            num = (short) (num + 1);
        Label_008C:
            if (num < list.Count)
            {
                goto Label_0068;
            }
            param2 = param.GetQuestParam();
            enumerator = list2.GetEnumerator();
        Label_00A8:
            try
            {
                goto Label_00BE;
            Label_00AD:
                param3 = &enumerator.Current;
                param3.state = 0;
            Label_00BE:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00AD;
                }
                goto Label_00DC;
            }
            finally
            {
            Label_00CF:
                ((List<QuestParam>.Enumerator) enumerator).Dispose();
            }
        Label_00DC:
            this.SetQuestState(list2, param2, 2, 1);
            param2.state = this.status.state;
            return;
        }

        public void Deserialize(ReqTowerRank.JSON_TowerRankResponse json)
        {
            int num;
            int num2;
            if (json == null)
            {
                goto Label_0200;
            }
            if (json.speed == null)
            {
                goto Label_00FD;
            }
            this.SpdRank = new TowerRankParam[(int) json.speed.Length];
            num = 0;
            goto Label_00EF;
        Label_002B:
            this.SpdRank[num] = new TowerRankParam();
            this.SpdRank[num].lv = json.speed[num].lv;
            this.SpdRank[num].name = json.speed[num].name;
            this.SpdRank[num].rank = json.speed[num].rank;
            this.SpdRank[num].score = json.speed[num].score;
            this.SpdRank[num].unit = new UnitData();
            this.SpdRank[num].unit.Deserialize(json.speed[num].unit);
            this.SpdRank[num].selected_award = json.speed[num].selected_award;
            num += 1;
        Label_00EF:
            if (num < ((int) json.speed.Length))
            {
                goto Label_002B;
            }
        Label_00FD:
            if (json.technical == null)
            {
                goto Label_01F4;
            }
            this.TecRank = new TowerRankParam[(int) json.technical.Length];
            num2 = 0;
            goto Label_01E6;
        Label_0122:
            this.TecRank[num2] = new TowerRankParam();
            this.TecRank[num2].lv = json.technical[num2].lv;
            this.TecRank[num2].name = json.technical[num2].name;
            this.TecRank[num2].rank = json.technical[num2].rank;
            this.TecRank[num2].score = json.technical[num2].score;
            this.TecRank[num2].unit = new UnitData();
            this.TecRank[num2].unit.Deserialize(json.technical[num2].unit);
            this.TecRank[num2].selected_award = json.technical[num2].selected_award;
            num2 += 1;
        Label_01E6:
            if (num2 < ((int) json.technical.Length))
            {
                goto Label_0122;
            }
        Label_01F4:
            this.Deserialize(json.rank);
        Label_0200:
            return;
        }

        public bool ExistDamagedUnit()
        {
            int num;
            if (this.pdeck != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            num = 0;
            goto Label_0031;
        Label_0014:
            if (this.pdeck[num].dmg <= 0)
            {
                goto Label_002D;
            }
            return 1;
        Label_002D:
            num += 1;
        Label_0031:
            if (num < this.pdeck.Count)
            {
                goto Label_0014;
            }
            return 0;
        }

        public PlayerUnit FindPlayerUnit(UnitData unit)
        {
            PlayerUnit unit2;
            <FindPlayerUnit>c__AnonStorey279 storey;
            storey = new <FindPlayerUnit>c__AnonStorey279();
            storey.unit = unit;
            if (this.pdeck != null)
            {
                goto Label_001A;
            }
            return null;
        Label_001A:
            return this.pdeck.Find(new Predicate<PlayerUnit>(storey.<>m__1C1));
        }

        public List<UnitData> GetAvailableUnits()
        {
            List<UnitData> list;
            if (<>f__am$cache22 != null)
            {
                goto Label_0027;
            }
            <>f__am$cache22 = new Predicate<UnitData>(TowerResuponse.<GetAvailableUnits>m__1C0);
        Label_0027:
            return MonoSingleton<GameManager>.Instance.Player.Units.FindAll(<>f__am$cache22);
        }

        public TowerFloorParam GetCurrentFloor()
        {
            return this.currentFloor;
        }

        public int GetDiedUnitNum()
        {
            int num;
            int num2;
            num = 0;
            if (this.pdeck != null)
            {
                goto Label_000F;
            }
            return num;
        Label_000F:
            num2 = 0;
            goto Label_0034;
        Label_0016:
            if (this.pdeck[num2].isDied == null)
            {
                goto Label_0030;
            }
            num += 1;
        Label_0030:
            num2 += 1;
        Label_0034:
            if (num2 < this.pdeck.Count)
            {
                goto Label_0016;
            }
            return num;
        }

        public int GetPlayerUnitHP(UnitData data)
        {
            PlayerUnit unit;
            int num;
            if (this.pdeck != null)
            {
                goto Label_0021;
            }
            return data.Status.param.hp;
        Label_0021:
            unit = this.FindPlayerUnit(data);
            if (unit != null)
            {
                goto Label_0045;
            }
            return data.Status.param.hp;
        Label_0045:
            num = Mathf.Min(unit.dmg, data.Status.param.hp - 1);
            return (data.Status.param.hp - num);
        }

        public int GetUnitDamage(UnitData unit_data)
        {
            PlayerUnit unit;
            if (this.pdeck != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            unit = this.FindPlayerUnit(unit_data);
            if (unit != null)
            {
                goto Label_001D;
            }
            return 0;
        Label_001D:
            return unit.dmg;
        }

        public void OnFloorRanking(ReqTowerFloorRanking.Json_Response json)
        {
            int num;
            int num2;
            this.FloorScores = new TowerScore.ViewParam();
            this.FloorSpdRank = null;
            this.FloorTecRank = null;
            if (json != null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            if (json.score == null)
            {
                goto Label_00C5;
            }
            this.FloorScores.TurnNum = json.score.turn_num;
            this.FloorScores.DiedNum = json.score.died_num;
            this.FloorScores.RecoveryNum = json.score.recovery_num;
            this.FloorScores.RetireNum = json.score.retire_num;
            this.FloorScores.FloorResetNum = json.score.reset_num;
            this.FloorScores.LoseNum = json.score.lose_num;
            this.FloorScores.ChallengeNum = json.score.challenge_num;
        Label_00C5:
            if (json.speed == null)
            {
                goto Label_01BC;
            }
            this.FloorSpdRank = new TowerRankParam[(int) json.speed.Length];
            num = 0;
            goto Label_01AE;
        Label_00EA:
            this.FloorSpdRank[num] = new TowerRankParam();
            this.FloorSpdRank[num].lv = json.speed[num].lv;
            this.FloorSpdRank[num].name = json.speed[num].name;
            this.FloorSpdRank[num].rank = json.speed[num].rank;
            this.FloorSpdRank[num].score = json.speed[num].score;
            this.FloorSpdRank[num].unit = new UnitData();
            this.FloorSpdRank[num].unit.Deserialize(json.speed[num].unit);
            this.FloorSpdRank[num].selected_award = json.speed[num].selected_award;
            num += 1;
        Label_01AE:
            if (num < ((int) json.speed.Length))
            {
                goto Label_00EA;
            }
        Label_01BC:
            if (json.technical == null)
            {
                goto Label_02B3;
            }
            this.FloorTecRank = new TowerRankParam[(int) json.technical.Length];
            num2 = 0;
            goto Label_02A5;
        Label_01E1:
            this.FloorTecRank[num2] = new TowerRankParam();
            this.FloorTecRank[num2].lv = json.technical[num2].lv;
            this.FloorTecRank[num2].name = json.technical[num2].name;
            this.FloorTecRank[num2].rank = json.technical[num2].rank;
            this.FloorTecRank[num2].score = json.technical[num2].score;
            this.FloorTecRank[num2].unit = new UnitData();
            this.FloorTecRank[num2].unit.Deserialize(json.technical[num2].unit);
            this.FloorTecRank[num2].selected_award = json.technical[num2].selected_award;
            num2 += 1;
        Label_02A5:
            if (num2 < ((int) json.technical.Length))
            {
                goto Label_01E1;
            }
        Label_02B3:
            return;
        }

        public void OnFloorReset()
        {
            this.edeck = null;
            this.lot_enemies = null;
            return;
        }

        private void SetQuestState(List<QuestParam> referenceQuestList, QuestParam questParam, QuestStates state, bool cond_recursive)
        {
            string[] strArray;
            int num;
            QuestParam param;
            <SetQuestState>c__AnonStorey277 storey;
            if (questParam != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            questParam.state = state;
            if (cond_recursive != null)
            {
                goto Label_0016;
            }
            return;
        Label_0016:
            if (questParam.cond_quests != null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            storey = new <SetQuestState>c__AnonStorey277();
            strArray = questParam.cond_quests;
            num = 0;
            goto Label_0061;
        Label_0036:
            storey.iname = strArray[num];
            param = referenceQuestList.Find(new Predicate<QuestParam>(storey.<>m__1BD));
            this.SetQuestState(referenceQuestList, param, state, cond_recursive);
            num += 1;
        Label_0061:
            if (num < ((int) strArray.Length))
            {
                goto Label_0036;
            }
            return;
        }

        private void UpdateCurrentFloor()
        {
            TowerFloorParam param;
            if (this.status != null)
            {
                goto Label_0025;
            }
            this.currentFloor = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID);
            goto Label_00B1;
        Label_0025:
            this.currentFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.status.fname);
            DebugUtility.Assert((this.currentFloor == null) == 0, string.Format("フロア [{0}] が見つかりません", this.status.fname));
            if (this.currentFloor != null)
            {
                goto Label_0072;
            }
            return;
        Label_0072:
            if (this.status.state != 2)
            {
                goto Label_00B1;
            }
            param = MonoSingleton<GameManager>.Instance.FindNextTowerFloor(this.currentFloor.tower_id, this.currentFloor.iname);
            if (param == null)
            {
                goto Label_00B1;
            }
            this.currentFloor = param;
        Label_00B1:
            return;
        }

        private void UpdateFloorQuestsState()
        {
        }

        public TowerClear.TOWER_CLEAR_FLAG ClearFlag
        {
            get
            {
                return this.clear;
            }
        }

        public bool IsNotClear
        {
            get
            {
                return (this.ClearFlag == 0);
            }
        }

        public bool IsFirstClear
        {
            get
            {
                return ((this.ClearFlag != 2) ? 0 : (this.arrived_num > 0));
            }
        }

        public bool IsRoundClear
        {
            get
            {
                return ((this.ClearFlag != 1) ? 0 : (this.arrived_num == 0));
            }
        }

        public long FreeRecoverTime
        {
            get
            {
                return this.rtime;
            }
        }

        public DateTime FreeRecoverEndTime
        {
            get
            {
                DateTime time;
                TowerParam param;
                time = TimeManager.FromUnixTime(this.rtime);
                param = MonoSingleton<GameManager>.Instance.FindTower(this.TowerID);
                if (param == null)
                {
                    goto Label_0032;
                }
                &time.AddHours((double) param.unit_recover_minute);
            Label_0032:
                return time;
            }
        }

        [CompilerGenerated]
        private sealed class <CalcDamage>c__AnonStorey278
        {
            internal int i;
            internal TowerResuponse <>f__this;

            public <CalcDamage>c__AnonStorey278()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1BE(Unit x)
            {
                return (x.UnitParam.iname == this.<>f__this.pdeck[this.i].unitname);
            }
        }

        [CompilerGenerated]
        private sealed class <FindPlayerUnit>c__AnonStorey279
        {
            internal UnitData unit;

            public <FindPlayerUnit>c__AnonStorey279()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1C1(TowerResuponse.PlayerUnit x)
            {
                return (x.unitname == this.unit.UnitParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <SetQuestState>c__AnonStorey277
        {
            internal string iname;

            public <SetQuestState>c__AnonStorey277()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1BD(QuestParam q)
            {
                return (q.iname == this.iname);
            }
        }

        public class EnemyUnit
        {
            public long id;
            public string iname;
            public string eid;
            public int hp;
            public int jewel;

            public EnemyUnit()
            {
                base..ctor();
                return;
            }
        }

        public class PlayerUnit
        {
            public string unitname;
            public int dmg;
            public int is_died;

            public PlayerUnit()
            {
                base..ctor();
                return;
            }

            public bool isDied
            {
                get
                {
                    return (this.is_died == 1);
                }
            }
        }

        public class Status
        {
            public string fname;
            public QuestStates state;

            public Status()
            {
                base..ctor();
                return;
            }
        }

        public class TowerRankParam
        {
            public string name;
            public int lv;
            public int rank;
            public int score;
            public string uid;
            public UnitData unit;
            public string selected_award;

            public TowerRankParam()
            {
                base..ctor();
                return;
            }
        }
    }
}

