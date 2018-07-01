// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DeactivateInventorySlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("UI/DeactivateInventorySlot", 32741)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Deactivate", FlowNode.PinTypes.Input, 1)]
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
