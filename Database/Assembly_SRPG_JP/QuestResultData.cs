// Decompiled with JetBrains decompiler
// Type: SRPG.QuestResultData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class QuestResultData
  {
    public Dictionary<long, UnitData.CharacterQuestParam> CharacterQuest = new Dictionary<long, UnitData.CharacterQuestParam>();
    public Dictionary<long, string> SkillUnlocks = new Dictionary<long, string>();
    public Dictionary<long, string> CollaboSkillUnlocks = new Dictionary<long, string>();
    public int StartExp;
    public int StartGold;
    public int StartBonusFlags;
    public BattleCore.Record Record;
    public UnitGetParam GetUnits;
    public bool IsFirstWin;

    public QuestResultData(PlayerData player, int bonusFlags, BattleCore.Record record, List<Unit> units, bool isFirstWin)
    {
      this.StartExp = player.Exp;
      this.StartGold = player.Gold;
      this.Record = record;
      this.StartBonusFlags = bonusFlags;
      this.IsFirstWin = isFirstWin;
      if (this.Record.items != null)
      {
        List<ItemParam> itemParamList = new List<ItemParam>();
        if (this.Record.items.Count != 0)
        {
          for (int index = 0; index < this.Record.items.Count; ++index)
          {
            if (this.Record.items[index].itemParam != null)
              itemParamList.Add(this.Record.items[index].itemParam);
          }
        }
        this.GetUnits = new UnitGetParam(itemParamList.ToArray());
      }
      if (units.Count > 1)
      {
        for (int i = 0; i < units.Count; ++i)
        {
          if (units[i] != null && units[i].Side == EUnitSide.Player && (units[i].UnitType == EUnitType.Unit && player.Units.Find((Predicate<UnitData>) (u => u.UniqueID == units[i].UnitData.UniqueID)) != null))
          {
            UnitData.CharacterQuestParam charaEpisodeData = units[i].UnitData.GetCurrentCharaEpisodeData();
            if (charaEpisodeData != null)
              this.CharacterQuest.Add(units[i].UnitData.UniqueID, charaEpisodeData);
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
