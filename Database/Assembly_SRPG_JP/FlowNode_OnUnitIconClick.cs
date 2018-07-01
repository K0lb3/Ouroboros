// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_OnUnitIconClick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Event/OnUnitIconClick", 58751)]
  [FlowNode.Pin(1, "Clicked", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_OnUnitIconClick : FlowNode
  {
    public void Click()
    {
      this.ActivateOutputLinks(1);
    }
  }
}
