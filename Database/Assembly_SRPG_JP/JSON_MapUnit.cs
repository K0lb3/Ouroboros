// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class JSON_MapUnit
  {
    public JSON_MapPartyUnit[] party;
    public JSON_MapEnemyUnit[] enemy;
    public JSON_MapPartyUnit[] arena;
    public JSON_QuestMonitorCondition w_cond;
    public JSON_QuestMonitorCondition l_cond;
    public JSON_GimmickEvent[] gs;
    public JSON_MapEnemyUnit[] deck;
    public JSON_RandUnitTag[] rand_tag;
    public JSON_MapPartySubCT[] party_subs;
    public JSON_MapTrick[] tricks;

    public List<JSON_MapEnemyUnit> GetRandFixedUnit()
    {
      List<JSON_MapEnemyUnit> jsonMapEnemyUnitList = new List<JSON_MapEnemyUnit>();
      for (int index = 0; index < this.enemy.Length; ++index)
      {
        if (MonoSingleton<GameManager>.Instance.MasterParam.ContainsUnitID(this.enemy[index].iname))
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(this.enemy[index].iname);
          if (unitParam.type == EUnitType.Gem || unitParam.type == EUnitType.BreakObj || unitParam.type == EUnitType.Treasure)
            jsonMapEnemyUnitList.Add(this.enemy[index]);
        }
      }
      return jsonMapEnemyUnitList;
    }

    public JSON_MapEnemyUnit[] ReplacedRandEnemy(RandDeckResult[] rand_lot_result, bool delete_rand_symbol = true)
    {
      if (rand_lot_result == null || rand_lot_result.Length <= 0)
      {
        if (delete_rand_symbol)
          this.DeleteRandSymbolInEnemies();
        return this.enemy;
      }
      for (int index = 0; index < rand_lot_result.Length; ++index)
      {
        if (this.enemy.Length <= rand_lot_result[index].set_id)
        {
          DebugUtility.LogError("ランダム抽選結果と敵配置データに不整合が発生");
          return (JSON_MapEnemyUnit[]) null;
        }
        JSON_MapEnemyUnit jsonMapEnemyUnit1 = this.enemy[rand_lot_result[index].set_id];
        if (!jsonMapEnemyUnit1.IsRandSymbol)
        {
          DebugUtility.LogError("ランダム抽選結果と敵配置データに不整合が発生");
          return (JSON_MapEnemyUnit[]) null;
        }
        JSON_MapEnemyUnit jsonMapEnemyUnit2 = (JSON_MapEnemyUnit) JsonUtility.FromJson<JSON_MapEnemyUnit>(JsonUtility.ToJson((object) this.deck[rand_lot_result[index].id]));
        jsonMapEnemyUnit2.x = jsonMapEnemyUnit1.x;
        jsonMapEnemyUnit2.y = jsonMapEnemyUnit1.y;
        jsonMapEnemyUnit2.dir = jsonMapEnemyUnit1.dir;
        jsonMapEnemyUnit2.name = jsonMapEnemyUnit1.name;
        jsonMapEnemyUnit2.entries = jsonMapEnemyUnit1.entries;
        jsonMapEnemyUnit2.entries_and = jsonMapEnemyUnit1.entries_and;
        this.enemy[rand_lot_result[index].set_id] = jsonMapEnemyUnit2;
      }
      if (delete_rand_symbol)
        this.DeleteRandSymbolInEnemies();
      return this.enemy;
    }

    private void DeleteRandSymbolInEnemies()
    {
      List<JSON_MapEnemyUnit> jsonMapEnemyUnitList = new List<JSON_MapEnemyUnit>();
      for (int index = 0; index < this.enemy.Length; ++index)
      {
        if (!this.enemy[index].IsRandSymbol)
          jsonMapEnemyUnitList.Add(this.enemy[index]);
      }
      this.enemy = jsonMapEnemyUnitList.ToArray();
    }
  }
}
