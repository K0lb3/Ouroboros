// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_OnUnitIconClick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "Clicked", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Event/OnUnitIconClick", 58751)]
  public class FlowNode_OnUnitIconClick : FlowNode
  {
    public override void OnActivate(int pinID)
    {
    }

    public void Click()
    {
      this.ActivateOutputLinks(1);
    }
  }
}
