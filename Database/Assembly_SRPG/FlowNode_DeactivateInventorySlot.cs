// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DeactivateInventorySlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Deactivate", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("UI/DeactivateInventorySlot", 32741)]
  public class FlowNode_DeactivateInventorySlot : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      InventorySlot.Active = (InventorySlot) null;
      MonoSingleton<GameManager>.Instance.Player.SaveInventory();
      this.ActivateOutputLinks(100);
    }
  }
}
