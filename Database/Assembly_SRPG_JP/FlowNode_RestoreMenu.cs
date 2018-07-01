// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RestoreMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Out", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/Set Restore Menu", 32741)]
  public class FlowNode_RestoreMenu : FlowNode
  {
    [FlowNode.ShowInInfo]
    public RestorePoints RestorePoint;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      HomeWindow.SetRestorePoint(this.RestorePoint);
      this.ActivateOutputLinks(10);
    }
  }
}
