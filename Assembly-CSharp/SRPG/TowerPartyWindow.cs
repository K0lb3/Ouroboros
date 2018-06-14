// Decompiled with JetBrains decompiler
// Type: SRPG.TowerPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class TowerPartyWindow : PartyWindow2
  {
    protected override void SetFriendSlot()
    {
      if (!Object.op_Inequality((Object) this.FriendSlot, (Object) null))
        return;
      this.FriendSlot.OnSelect = new GenericSlot.SelectEvent(((PartyWindow2) this).OnUnitSlotClick);
      if (this.mCurrentQuest.type != QuestTypes.Tower)
        return;
      TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mCurrentQuest.iname);
      if (towerFloor == null)
        return;
      ((Component) this.FriendSlot).get_gameObject().SetActive(towerFloor.can_help);
      ((Component) this.SupportSkill).get_gameObject().SetActive(towerFloor.can_help);
    }

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
      if (this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units, ref empty))
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
        if ((heroesAvailable || (int) allUnits[index].UnitParam.hero == 0) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (allUnits[index] != this.mCurrentParty.Units[0] || !selectedSlotIsEmpty) || numMainMembers > 1) && (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Tower || allUnits[index].Lv >= this.mCurrentQuest.EntryCondition.ulvmin))
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(allUnits[index]) + 1);
      }
    }
  }
}
