// Decompiled with JetBrains decompiler
// Type: SRPG.QuestResultData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
      QuestResultData.\u003CQuestResultData\u003Ec__AnonStorey372 dataCAnonStorey372 = new QuestResultData.\u003CQuestResultData\u003Ec__AnonStorey372();
      // ISSUE: reference to a compiler-generated field
      dataCAnonStorey372.units = units;
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
      {
        ItemParam[] paramLsit = new ItemParam[0];
        if (this.Record.items.Count != 0)
        {
          paramLsit = new ItemParam[this.Record.items.Count];
          for (int index = 0; index < this.Record.items.Count; ++index)
            paramLsit[index] = this.Record.items[index].mItemParam;
        }
        this.GetUnits = new UnitGetParam(paramLsit);
      }
      // ISSUE: reference to a compiler-generated field
      if (dataCAnonStorey372.units.Count > 1)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        QuestResultData.\u003CQuestResultData\u003Ec__AnonStorey373 dataCAnonStorey373 = new QuestResultData.\u003CQuestResultData\u003Ec__AnonStorey373();
        // ISSUE: reference to a compiler-generated field
        dataCAnonStorey373.\u003C\u003Ef__ref\u0024882 = dataCAnonStorey372;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        for (dataCAnonStorey373.i = 0; dataCAnonStorey373.i < dataCAnonStorey372.units.Count; ++dataCAnonStorey373.i)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          if (dataCAnonStorey372.units[dataCAnonStorey373.i] != null && dataCAnonStorey372.units[dataCAnonStorey373.i].Side == EUnitSide.Player && (dataCAnonStorey372.units[dataCAnonStorey373.i].UnitType == EUnitType.Unit && player.Units.Find(new Predicate<UnitData>(dataCAnonStorey373.\u003C\u003Em__3F7)) != null))
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            UnitData.CharacterQuestParam charaEpisodeData = dataCAnonStorey372.units[dataCAnonStorey373.i].UnitData.GetCurrentCharaEpisodeData();
            if (charaEpisodeData != null)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              this.CharacterQuest.Add(dataCAnonStorey372.units[dataCAnonStorey373.i].UnitData.UniqueID, charaEpisodeData);
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
