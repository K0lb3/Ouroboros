// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectUnit
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/Select Unit", 32741)]
  [FlowNode.Pin(2, "Unit Not Found", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Selected", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Select", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_SelectUnit : FlowNode
  {
    public int EquipSlot = -1;
    public string UnitID;
    public long UniqueID;
    public bool KeepSelection;
    public bool SelectJob;
    public string JobID;
    public bool SelectEquipSlot;

    public override void OnActivate(int pinID)
    {
      UnitData unitData = (UnitData) null;
      if (pinID != 0)
        return;
      int pinID1 = 2;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (!string.IsNullOrEmpty(this.UnitID))
      {
        if ((unitData = player.FindUnitDataByUnitID(this.UnitID)) != null)
        {
          GlobalVars.SelectedUnitUniqueID.Set(unitData.UniqueID);
          pinID1 = 1;
        }
      }
      else if (this.UniqueID != 0L && (unitData = player.FindUnitDataByUniqueID(this.UniqueID)) != null)
      {
        GlobalVars.SelectedUnitUniqueID.Set(unitData.UniqueID);
        pinID1 = 1;
      }
      switch (pinID1)
      {
        case 1:
          if (this.SelectJob)
          {
            int index = 0;
            while (index < unitData.Jobs.Length && !(unitData.Jobs[index].JobID == this.JobID))
              ++index;
            if (index < unitData.Jobs.Length)
              GlobalVars.SelectedJobUniqueID.Set(unitData.Jobs[index].UniqueID);
            else if (!this.KeepSelection)
              GlobalVars.SelectedJobUniqueID.Set(0L);
          }
          if (this.SelectEquipSlot)
          {
            GlobalVars.SelectedEquipmentSlot.Set(this.EquipSlot);
            break;
          }
          break;
        case 2:
          if (!this.KeepSelection)
          {
            GlobalVars.SelectedUnitUniqueID.Set(0L);
            if (this.SelectJob)
              GlobalVars.SelectedJobUniqueID.Set(0L);
            if (this.SelectEquipSlot)
            {
              GlobalVars.SelectedEquipmentSlot.Set(-1);
              break;
            }
            break;
          }
          break;
      }
      this.ActivateOutputLinks(pinID1);
    }
  }
}
