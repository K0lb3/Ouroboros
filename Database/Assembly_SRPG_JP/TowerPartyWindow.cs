// Decompiled with JetBrains decompiler
// Type: SRPG.TowerPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class TowerPartyWindow : PartyWindow2
  {
    protected override bool CheckMember(int numMainUnits)
    {
      if (numMainUnits <= 0)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_CANTSTART"), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
        return false;
      }
      if (this.mCurrentParty.Units[0] == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.LEADERNOTSET"), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
        return false;
      }
      string empty = string.Empty;
      if (this.mCurrentQuest.IsEntryQuestCondition((IEnumerable<UnitData>) this.mCurrentParty.Units, ref empty))
        return true;
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
      return false;
    }

    protected override int AvailableMainMemberSlots
    {
      get
      {
        if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Tower)
        {
          TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mCurrentQuest.iname);
          if (towerFloor != null && !towerFloor.can_help)
            return 5;
        }
        return base.AvailableMainMemberSlots;
      }
    }

    protected override void RegistPartyMember(List<UnitData> allUnits, bool heroesAvailable, bool selectedSlotIsEmpty, int numMainMembers)
    {
      for (int index = 0; index < allUnits.Count; ++index)
      {
        if ((heroesAvailable || !allUnits[index].UnitParam.IsHero()) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (allUnits[index] != this.mCurrentParty.Units[0] || !selectedSlotIsEmpty) || numMainMembers > 1) && (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Tower || allUnits[index].Lv >= this.mCurrentQuest.EntryCondition.ulvmin))
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(allUnits[index]) + 1);
      }
    }
  }
}
