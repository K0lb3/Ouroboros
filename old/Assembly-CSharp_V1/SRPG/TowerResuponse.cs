// Decompiled with JetBrains decompiler
// Type: SRPG.TowerResuponse
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
    public int spd_score;
    public int tec_score;
    public int ret_score;
    public int rcv_score;
    public TowerResuponse.TowerRankParam[] SpdRank;
    public TowerResuponse.TowerRankParam[] TecRank;
    public byte[] lot_enemies;

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
      this.is_reset = (int) res.is_reset == 1;
      if (res.lot_enemies != null)
      {
        this.lot_enemies = new byte[res.lot_enemies.Length];
        for (int index = 0; index < res.lot_enemies.Length; ++index)
          this.lot_enemies[index] = (byte) res.lot_enemies[index];
      }
      this.Deserialize(res.edeck);
      if (res.rank != null)
      {
        this.turn_num = res.rank.turn_num;
        this.died_num = res.rank.died_num;
        this.retire_num = res.rank.retire_num;
        this.recover_num = res.rank.recovery_num;
        this.speedRank = res.rank.spd_rank;
        this.techRank = res.rank.tec_rank;
        this.spd_score = res.rank.spd_score;
        this.tec_score = res.rank.tec_score;
        this.ret_score = res.rank.ret_score;
        this.rcv_score = res.rank.rcv_score;
      }
      this.UpdateCurrentFloor();
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
        }
      }
      if (json.technical == null)
        return;
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
      }
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      TowerResuponse.\u003CSetQuestState\u003Ec__AnonStorey213 stateCAnonStorey213 = new TowerResuponse.\u003CSetQuestState\u003Ec__AnonStorey213();
      foreach (string condQuest in questParam.cond_quests)
      {
        // ISSUE: reference to a compiler-generated field
        stateCAnonStorey213.iname = condQuest;
        // ISSUE: reference to a compiler-generated method
        QuestParam questParam1 = referenceQuestList.Find(new Predicate<QuestParam>(stateCAnonStorey213.\u003C\u003Em__203));
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      TowerResuponse.\u003CCalcDamage\u003Ec__AnonStorey214 damageCAnonStorey214 = new TowerResuponse.\u003CCalcDamage\u003Ec__AnonStorey214();
      // ISSUE: reference to a compiler-generated field
      damageCAnonStorey214.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (damageCAnonStorey214.i = 0; damageCAnonStorey214.i < this.pdeck.Count; ++damageCAnonStorey214.i)
      {
        // ISSUE: reference to a compiler-generated method
        Unit unit = player.Find(new Predicate<Unit>(damageCAnonStorey214.\u003C\u003Em__204));
        if (unit != null)
        {
          // ISSUE: reference to a compiler-generated field
          int num = Mathf.Min(this.pdeck[damageCAnonStorey214.i].dmg, (int) unit.MaximumStatus.param.hp - 1);
          unit.Damage(num);
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

    public void CalcEnemyDamage(List<Unit> enemy)
    {
      if (this.edeck == null)
        return;
      for (int index1 = 0; index1 < this.edeck.Count; ++index1)
      {
        int index2 = int.Parse(this.edeck[index1].eid);
        if (index2 >= 0 && enemy.Count > index2)
        {
          Unit unit = enemy[index2];
          if (unit != null)
          {
            unit.Damage((int) unit.MaximumStatus.param.hp - this.edeck[index1].hp);
            unit.Gems = this.edeck[index1].jewel;
            this.edeck[index1].id = unit.UnitData.UniqueID;
            this.edeck[index1].iname = unit.UnitParam.iname;
          }
        }
      }
      enemy.RemoveAll((Predicate<Unit>) (x => (int) x.CurrentStatus.param.hp <= 0));
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
    }
  }
}
