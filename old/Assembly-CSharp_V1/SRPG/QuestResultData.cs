// Decompiled with JetBrains decompiler
// Type: SRPG.QuestResultData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class QuestResultData
  {
    public int StartExp;
    public int StartGold;
    public int StartBonusFlags;
    public Dictionary<long, UnitData.CharacterQuestParam> CharacterQuest;
    public Dictionary<long, string> SkillUnlocks;
    public BattleCore.Record Record;
    public UnitGetParam GetUnits;
    public bool IsFirstWin;
    public Dictionary<long, string> CollaboSkillUnlocks;

    public QuestResultData(PlayerData player, int bonusFlags, BattleCore.Record record, List<Unit> units, bool isFirstWin)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      QuestResultData.\u003CQuestResultData\u003Ec__AnonStorey26A dataCAnonStorey26A = new QuestResultData.\u003CQuestResultData\u003Ec__AnonStorey26A();
      // ISSUE: reference to a compiler-generated field
      dataCAnonStorey26A.units = units;
      this.CharacterQuest = new Dictionary<long, UnitData.CharacterQuestParam>();
      this.SkillUnlocks = new Dictionary<long, string>();
      this.CollaboSkillUnlocks = new Dictionary<long, string>();
      // ISSUE: explicit constructor call
      base.\u002Ector();
      this.StartExp = player.Exp;
      this.StartGold = player.Gold;
      this.Record = record;
      this.StartBonusFlags = bonusFlags;
      this.IsFirstWin = isFirstWin;
      if (this.Record.items != null)
        this.GetUnits = new UnitGetParam(this.Record.items.ToArray());
      // ISSUE: reference to a compiler-generated field
      if (dataCAnonStorey26A.units.Count > 1)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        QuestResultData.\u003CQuestResultData\u003Ec__AnonStorey26B dataCAnonStorey26B = new QuestResultData.\u003CQuestResultData\u003Ec__AnonStorey26B();
        // ISSUE: reference to a compiler-generated field
        dataCAnonStorey26B.\u003C\u003Ef__ref\u0024618 = dataCAnonStorey26A;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        for (dataCAnonStorey26B.i = 0; dataCAnonStorey26B.i < dataCAnonStorey26A.units.Count; ++dataCAnonStorey26B.i)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          if (dataCAnonStorey26A.units[dataCAnonStorey26B.i] != null && dataCAnonStorey26A.units[dataCAnonStorey26B.i].Side == EUnitSide.Player && (dataCAnonStorey26A.units[dataCAnonStorey26B.i].UnitType == EUnitType.Unit && player.Units.Find(new Predicate<UnitData>(dataCAnonStorey26B.\u003C\u003Em__2CD)) != null))
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            UnitData.CharacterQuestParam charaEpisodeData = dataCAnonStorey26A.units[dataCAnonStorey26B.i].UnitData.GetCurrentCharaEpisodeData();
            if (charaEpisodeData != null)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              this.CharacterQuest.Add(dataCAnonStorey26A.units[dataCAnonStorey26B.i].UnitData.UniqueID, charaEpisodeData);
            }
          }
        }
      }
      List<UnitData> units1 = player.Units;
      for (int index = 0; index < units1.Count; ++index)
      {
        string str1 = units1[index].UnlockedSkillIds();
        this.SkillUnlocks.Add(units1[index].UniqueID, str1);
        string str2 = units1[index].UnlockedCollaboSkillIds();
        this.CollaboSkillUnlocks.Add(units1[index].UniqueID, str2);
      }
    }
  }
}
