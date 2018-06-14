// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MapUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

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
    public byte is_rand;
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
  }
}
