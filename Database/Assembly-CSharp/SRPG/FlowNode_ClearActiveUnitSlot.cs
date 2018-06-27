// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ClearActiveUnitSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Clear Slot", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("SRPG/ClearActiveUnitSlot", 32741)]
  public class FlowNode_ClearActiveUnitSlot : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (Object.op_Inequality((Object) PartyUnitSlot.Active, (Object) null))
      {
        PartyData dataOfClass = DataSource.FindDataOfClass<PartyData>(((Component) PartyUnitSlot.Active).get_gameObject(), (PartyData) null);
        if (dataOfClass != null)
          dataOfClass.SetUnitUniqueID(PartyUnitSlot.Active.Index, 0L);
      }
      this.ActivateOutputLinks(100);
    }
  }
}
