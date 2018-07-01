// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckRankMatchUnitSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  [FlowNode.Pin(202, "Unit Place NG", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(201, "Unit Slot NG", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(200, "Unit Slot OK", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(120, "Check Unit Slot", FlowNode.PinTypes.Input, 3)]
  [FlowNode.NodeType("Multi/CheckRankMatchUnitSlot", 32741)]
  public class FlowNode_CheckRankMatchUnitSlot : FlowNode
  {
    public const int PINID_CHECK_UNIT_SLOT = 120;
    public const int PINID_UNIT_SLOT_OK = 200;
    public const int PINID_UNIT_SLOT_NG = 201;
    public const int PINID_UNIT_PLACE_NG = 202;

    public override void OnActivate(int pinID)
    {
      if (pinID != 120)
        return;
      int lastSelectionIndex;
      PartyEditData loadTeamPreset = PartyUtility.LoadTeamPresets(PlayerPartyTypes.RankMatch, out lastSelectionIndex, false)[lastSelectionIndex];
      for (int index = 0; index < loadTeamPreset.PartyData.VSWAITMEMBER_START; ++index)
      {
        if (index + 1 > loadTeamPreset.Units.Length || loadTeamPreset.Units[index] == null)
        {
          this.ActivateOutputLinks(201);
          return;
        }
      }
      List<int> intList = new List<int>();
      for (int index = 0; index < loadTeamPreset.PartyData.VSWAITMEMBER_START; ++index)
      {
        int num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.RANKMATCH_ID_KEY + (object) index, -1);
        if (num >= 0)
        {
          if (intList.Contains(num))
          {
            this.ActivateOutputLinks(202);
            return;
          }
          intList.Add(num);
        }
      }
      this.ActivateOutputLinks(200);
    }
  }
}
