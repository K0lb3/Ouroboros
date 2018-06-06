// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_OnUnitIconClick
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
