// Decompiled with JetBrains decompiler
// Type: SRPG.TowerPartyWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(6, "画面アンロック", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(5, "画面ロック", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(1, "進む", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(31, "サポート選択完了", FlowNode.PinTypes.Output, 31)]
  [FlowNode.Pin(30, "サポート選択開始", FlowNode.PinTypes.Output, 30)]
  [FlowNode.Pin(21, "アイテム選択完了", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(20, "アイテム選択開始", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(11, "ユニット選択完了", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "ユニット選択開始", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(4, "開く", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "戻る", FlowNode.PinTypes.Output, 1)]
  public class TowerPartyWindow : PartyWindow2
  {
    private const int TowerLevel = 20;

    protected override RectTransform OnGetUnitListItem(int id, int old, RectTransform current)
    {
      RectTransform unitListItem = base.OnGetUnitListItem(id, old, current);
      if (id == 0 || this.mCurrentQuest == null || (this.mCurrentQuest.type != QuestTypes.Tower || this.mOwnUnits[id - 1].Lv >= 20))
        return unitListItem;
      ((Component) unitListItem).get_gameObject().SetActive(false);
      return unitListItem;
    }

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

    protected override PartyWindow2.EditPartyTypes GetEditPartyTypes()
    {
      return this.mCurrentQuest.type == QuestTypes.Tower ? PartyWindow2.EditPartyTypes.Tower : PartyWindow2.EditPartyTypes.Auto;
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
        if ((heroesAvailable || (int) allUnits[index].UnitParam.hero == 0) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (allUnits[index] != this.mCurrentParty.Units[0] || !selectedSlotIsEmpty) || numMainMembers > 1) && (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Tower || allUnits[index].Lv >= 20))
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(allUnits[index]) + 1);
      }
    }
  }
}
