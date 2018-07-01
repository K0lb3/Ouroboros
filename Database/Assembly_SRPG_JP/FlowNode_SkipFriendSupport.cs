// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SkipFriendSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("SRPG/SkipFriendSupport", 32741)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(100, "In", FlowNode.PinTypes.Input, 0)]
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
