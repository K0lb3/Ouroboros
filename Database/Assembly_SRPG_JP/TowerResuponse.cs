// Decompiled with JetBrains decompiler
// Type: SRPG.TowerResuponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class TowerResuponse
  {
    public long rtime;
    public TowerResuponse.Status status;
    public List<TowerResuponse.PlayerUnit> pdeck;
    public List<TowerResuponse.EnemyUnit> edeck;
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
    public TowerResuponse.TowerRankParam[] SpdRank;
    public TowerResuponse.TowerRankParam[] TecRank;
    public RandDeckResult[] lot_enemies;
    public TowerScore.ViewParam FloorScores;
    public TowerResuponse.TowerRankParam[] FloorSpdRank;
    public TowerResuponse.TowerRankParam[] FloorTecRank;

    public TowerClear.TOWER_CLEAR_FLAG ClearFlag
    {
      get
      {
        return (TowerClear.TOWER_CLEAR_FLAG) this.clear;
      }
    }

    public bool IsNotClear
    {
      get
      {
        return this.ClearFlag == TowerClear.TOWER_CLEAR_FLAG.NOT_CLEAR;
      }
    }

    public bool IsFirstClear
    {
      get
      {
        if (this.ClearFlag == TowerClear.TOWER_CLEAR_FLAG.CLEAR_AND_SCORE)
          return this.arrived_num > 0;
        return false;
      }
    }

    public bool IsRoundClear
    {
      get
      {
        if (this.ClearFlag == TowerClear.TOWER_CLEAR_FLAG.CLEAR)
          return this.arrived_num == 0;
        return false;
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
        DateTime dateTime = TimeManager.FromUnixTime(this.rtime);
        TowerParam tower = MonoSingleton<GameManager>.Instance.FindTower(this.TowerID);
        if (tower != null)
          dateTime.AddHours((double) tower.unit_recover_minute);
        return dateTime;
      }
    }

    public void Deserialize(JSON_ReqTowerResuponse res)
    {
      if (res == null)
        return;
      this.TowerID = GlobalVars.SelectedTowerID;
      this.rtime = res.rtime;
      if (res.stats != null)
      {
        this.Deserialize(res.stats);
      }
      else
      {
        TowerFloorParam firstTowerFloor = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(this.TowerID);
        if (firstTowerFloor != null)
          this.Deserialize(new JSON_ReqTowerResuponse.Json_TowerStatus()
          {
            fname = firstTowerFloor.iname,
            questStates = QuestStates.New
          });
      }
      if (res.pdeck != null)
      {
        this.pdeck = new List<TowerResuponse.PlayerUnit>();
        for (int index = 0; index < res.pdeck.Length; ++index)
        {
          this.pdeck.Add(new TowerResuponse.PlayerUnit());
          this.pdeck[index].dmg = res.pdeck[index].damage;
          this.pdeck[index].unitname = res.pdeck[index].uname;
          this.pdeck[index].is_died = res.pdeck[index].is_died;
        }
      }
      this.reset_cost = res.reset_cost;
      this.round = res.round;
      this.is_reset = res.is_reset == (byte) 1;
      if (res.lot_enemies != null && res.lot_enemies.Length > 0)
      {
        this.lot_enemies = new RandDeckResult[res.lot_enemies.Length];
        for (int index = 0; index < res.lot_enemies.Length; ++index)
          this.lot_enemies[index] = new RandDeckResult()
          {
            id = res.lot_enemies[index].id,
            set_id = res.lot_enemies[index].set_id
          };
      }
      this.Deserialize(res.edeck);
      this.Deserialize(res.rank);
      this.UpdateCurrentFloor();
    }

    private void Deserialize(JSON_ReqTowerResuponse.Json_RankStatus json)
    {
      if (json == null)
        return;
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
    }

    public void Deserialize(JSON_ReqTowerResuponse.Json_TowerPlayerUnit[] res)
    {
      if (res == null || res == null)
        return;
      this.pdeck = new List<TowerResuponse.PlayerUnit>();
      for (int index = 0; index < res.Length; ++index)
      {
        this.pdeck.Add(new TowerResuponse.PlayerUnit());
        this.pdeck[index].dmg = res[index].damage;
        this.pdeck[index].unitname = res[index].uname;
        this.pdeck[index].is_died = res[index].is_died;
      }
    }

    public void Deserialize(JSON_ReqTowerResuponse.Json_TowerEnemyUnit[] res)
    {
      if (res == null)
        return;
      this.edeck = new List<TowerResuponse.EnemyUnit>();
      for (int index = 0; index < res.Length; ++index)
      {
        this.edeck.Add(new TowerResuponse.EnemyUnit());
        this.edeck[index].hp = res[index].hp;
        this.edeck[index].jewel = res[index].jewel;
        this.edeck[index].eid = res[index].eid.ToString();
      }
    }

    public void Deserialize(ReqTowerRank.JSON_TowerRankResponse json)
    {
      if (json == null)
        return;
      if (json.speed != null)
      {
        this.SpdRank = new TowerResuponse.TowerRankParam[json.speed.Length];
        for (int index = 0; index < json.speed.Length; ++index)
        {
          this.SpdRank[index] = new TowerResuponse.TowerRankParam();
          this.SpdRank[index].lv = json.speed[index].lv;
          this.SpdRank[index].name = json.speed[index].name;
          this.SpdRank[index].rank = json.speed[index].rank;
          this.SpdRank[index].score = json.speed[index].score;
          this.SpdRank[index].unit = new UnitData();
          this.SpdRank[index].unit.Deserialize(json.speed[index].unit);
          this.SpdRank[index].selected_award = json.speed[index].selected_award;
        }
      }
      if (json.technical != null)
      {
        this.TecRank = new TowerResuponse.TowerRankParam[json.technical.Length];
        for (int index = 0; index < json.technical.Length; ++index)
        {
          this.TecRank[index] = new TowerResuponse.TowerRankParam();
          this.TecRank[index].lv = json.technical[index].lv;
          this.TecRank[index].name = json.technical[index].name;
          this.TecRank[index].rank = json.technical[index].rank;
          this.TecRank[index].score = json.technical[index].score;
          this.TecRank[index].unit = new UnitData();
          this.TecRank[index].unit.Deserialize(json.technical[index].unit);
          this.TecRank[index].selected_award = json.technical[index].selected_award;
        }
      }
      this.Deserialize(json.rank);
    }

    private void UpdateCurrentFloor()
    {
      if (this.status == null)
      {
        this.currentFloor = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID);
      }
      else
      {
        this.currentFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.status.fname);
        DebugUtility.Assert(this.currentFloor != null, string.Format("フロア [{0}] が見つかりません", (object) this.status.fname));
        if (this.currentFloor == null || this.status.state != QuestStates.Cleared)
          return;
        TowerFloorParam nextTowerFloor = MonoSingleton<GameManager>.Instance.FindNextTowerFloor(this.currentFloor.tower_id, this.currentFloor.iname);
        if (nextTowerFloor == null)
          return;
        this.currentFloor = nextTowerFloor;
      }
    }

    public void Deserialize(JSON_ReqTowerResuponse.Json_TowerStatus json)
    {
      this.status = new TowerResuponse.Status();
      this.status.fname = json.fname;
      this.status.state = json.questStates;
      TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.status.fname);
      if (towerFloor == null)
        return;
      List<TowerFloorParam> towerFloors = MonoSingleton<GameManager>.Instance.FindTowerFloors(towerFloor.tower_id);
      List<QuestParam> referenceQuestList = new List<QuestParam>();
      for (short index = 0; (int) index < towerFloors.Count; ++index)
      {
        towerFloors[(int) index].FloorIndex = index;
        referenceQuestList.Add(towerFloors[(int) index].GetQuestParam());
      }
      QuestParam questParam = towerFloor.GetQuestParam();
      using (List<QuestParam>.Enumerator enumerator = referenceQuestList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.state = QuestStates.New;
      }
      this.SetQuestState(referenceQuestList, questParam, QuestStates.Cleared, true);
      questParam.state = this.status.state;
    }

    private void UpdateFloorQuestsState()
    {
    }

    private void SetQuestState(List<QuestParam> referenceQuestList, QuestParam questParam, QuestStates state, bool cond_recursive)
    {
      if (questParam == null)
        return;
      questParam.state = state;
      if (!cond_recursive || questParam.cond_quests == null)
        return;
      foreach (string condQuest in questParam.cond_quests)
      {
        string iname = condQuest;
        QuestParam questParam1 = referenceQuestList.Find((Predicate<QuestParam>) (q => q.iname == iname));
        this.SetQuestState(referenceQuestList, questParam1, state, cond_recursive);
      }
    }

    public TowerFloorParam GetCurrentFloor()
    {
      return this.currentFloor;
    }

    public void CalcDamage(List<Unit> player)
    {
      if (this.pdeck == null)
        return;
      for (int i = 0; i < this.pdeck.Count; ++i)
      {
        Unit unit = player.Find((Predicate<Unit>) (x => x.UnitParam.iname == this.pdeck[i].unitname));
        if (unit != null)
        {
          int num = Mathf.Min(this.pdeck[i].dmg, (int) unit.MaximumStatus.param.hp - 1);
          unit.Damage(num, false);
        }
      }
    }

    public int GetPlayerUnitHP(UnitData data)
    {
      if (this.pdeck == null)
        return (int) data.Status.param.hp;
      TowerResuponse.PlayerUnit playerUnit = this.FindPlayerUnit(data);
      if (playerUnit == null)
        return (int) data.Status.param.hp;
      int num = Mathf.Min(playerUnit.dmg, (int) data.Status.param.hp - 1);
      return (int) data.Status.param.hp - num;
    }

    public int GetUnitDamage(UnitData unit_data)
    {
      if (this.pdeck == null)
        return 0;
      TowerResuponse.PlayerUnit playerUnit = this.FindPlayerUnit(unit_data);
      if (playerUnit == null)
        return 0;
      return playerUnit.dmg;
    }

    public void CalcEnemyDamage(List<Unit> enemy, bool is_menu = false)
    {
      if (this.edeck == null)
        return;
      List<Unit> unitList = new List<Unit>();
      for (int index1 = 0; index1 < this.edeck.Count; ++index1)
      {
        int index2 = int.Parse(this.edeck[index1].eid);
        if (index2 >= 0 && enemy.Count > index2)
        {
          Unit unit = enemy[index2];
          if (unit != null && (!is_menu || !unit.IsGimmick))
          {
            if (unit.IsGimmick && this.edeck[index1].hp == 0)
            {
              unitList.Add(unit);
            }
            else
            {
              unit.Damage((int) unit.MaximumStatus.param.hp - this.edeck[index1].hp, false);
              unit.Gems = this.edeck[index1].jewel;
              this.edeck[index1].id = unit.UnitData.UniqueID;
              this.edeck[index1].iname = unit.UnitParam.iname;
            }
          }
        }
      }
      enemy.RemoveAll((Predicate<Unit>) (x =>
      {
        if (!x.IsGimmick)
          return (int) x.CurrentStatus.param.hp <= 0;
        return false;
      }));
      for (int index = 0; index < unitList.Count; ++index)
      {
        if (unitList[index] != null)
          enemy.Remove(unitList[index]);
      }
    }

    public int CalcRecoverCost()
    {
      TowerParam tower = MonoSingleton<GameManager>.Instance.FindTower(this.TowerID);
      if (tower == null)
        return 0;
      double num = Math.Ceiling((TimeManager.FromUnixTime(this.rtime).AddMinutes(-1.0) - TimeManager.ServerTime).TotalMinutes) / (double) tower.unit_recover_minute;
      return Mathf.Clamp((int) Math.Ceiling((double) tower.unit_recover_coin * num), 0, (int) tower.unit_recover_coin);
    }

    public bool ExistDamagedUnit()
    {
      if (this.pdeck == null)
        return false;
      for (int index = 0; index < this.pdeck.Count; ++index)
      {
        if (this.pdeck[index].dmg > 0)
          return true;
      }
      return false;
    }

    public int GetDiedUnitNum()
    {
      int num = 0;
      if (this.pdeck == null)
        return num;
      for (int index = 0; index < this.pdeck.Count; ++index)
      {
        if (this.pdeck[index].isDied)
          ++num;
      }
      return num;
    }

    public List<UnitData> GetAvailableUnits()
    {
      return MonoSingleton<GameManager>.Instance.Player.Units.FindAll((Predicate<UnitData>) (unitData => unitData.Lv >= 20));
    }

    public TowerResuponse.PlayerUnit FindPlayerUnit(UnitData unit)
    {
      if (this.pdeck == null)
        return (TowerResuponse.PlayerUnit) null;
      return this.pdeck.Find((Predicate<TowerResuponse.PlayerUnit>) (x => x.unitname == unit.UnitParam.iname));
    }

    public void OnFloorReset()
    {
      this.edeck = (List<TowerResuponse.EnemyUnit>) null;
      this.lot_enemies = (RandDeckResult[]) null;
    }

    public void OnFloorRanking(ReqTowerFloorRanking.Json_Response json)
    {
      this.FloorScores = new TowerScore.ViewParam();
      this.FloorSpdRank = (TowerResuponse.TowerRankParam[]) null;
      this.FloorTecRank = (TowerResuponse.TowerRankParam[]) null;
      if (json == null)
        return;
      if (json.score != null)
      {
        this.FloorScores.TurnNum = json.score.turn_num;
        this.FloorScores.DiedNum = json.score.died_num;
        this.FloorScores.RecoveryNum = json.score.recovery_num;
        this.FloorScores.RetireNum = json.score.retire_num;
        this.FloorScores.FloorResetNum = json.score.reset_num;
        this.FloorScores.LoseNum = json.score.lose_num;
        this.FloorScores.ChallengeNum = json.score.challenge_num;
      }
      if (json.speed != null)
      {
        this.FloorSpdRank = new TowerResuponse.TowerRankParam[json.speed.Length];
        for (int index = 0; index < json.speed.Length; ++index)
        {
          this.FloorSpdRank[index] = new TowerResuponse.TowerRankParam();
          this.FloorSpdRank[index].lv = json.speed[index].lv;
          this.FloorSpdRank[index].name = json.speed[index].name;
          this.FloorSpdRank[index].rank = json.speed[index].rank;
          this.FloorSpdRank[index].score = json.speed[index].score;
          this.FloorSpdRank[index].unit = new UnitData();
          this.FloorSpdRank[index].unit.Deserialize(json.speed[index].unit);
          this.FloorSpdRank[index].selected_award = json.speed[index].selected_award;
        }
      }
      if (json.technical == null)
        return;
      this.FloorTecRank = new TowerResuponse.TowerRankParam[json.technical.Length];
      for (int index = 0; index < json.technical.Length; ++index)
      {
        this.FloorTecRank[index] = new TowerResuponse.TowerRankParam();
        this.FloorTecRank[index].lv = json.technical[index].lv;
        this.FloorTecRank[index].name = json.technical[index].name;
        this.FloorTecRank[index].rank = json.technical[index].rank;
        this.FloorTecRank[index].score = json.technical[index].score;
        this.FloorTecRank[index].unit = new UnitData();
        this.FloorTecRank[index].unit.Deserialize(json.technical[index].unit);
        this.FloorTecRank[index].selected_award = json.technical[index].selected_award;
      }
    }

    public bool CheckEnemyDeck()
    {
      return this.edeck != null;
    }

    public class Status
    {
      public string fname;
      public QuestStates state;
    }

    public class PlayerUnit
    {
      public string unitname;
      public int dmg;
      public int is_died;

      public bool isDied
      {
        get
        {
          return this.is_died == 1;
        }
      }
    }

    public class EnemyUnit
    {
      public long id;
      public string iname;
      public string eid;
      public int hp;
      public int jewel;
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
    }
  }
}
