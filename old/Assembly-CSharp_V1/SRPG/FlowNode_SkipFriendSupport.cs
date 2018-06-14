// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SkipFriendSupport
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(100, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("SRPG/SkipFriendSupport", 32741)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SkipFriendSupport : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 100)
        return;
      GlobalVars.SelectedSupport.Set((SupportData) null);
      this.ActivateOutputLinks(1);
    }
  }
}
