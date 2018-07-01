// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Navigation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "Show", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Destory", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(2, "Discard", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("UI/Navigation", 32741)]
  public class FlowNode_Navigation : FlowNode
  {
    public NavigationWindow Template;
    [StringIsTextID(false)]
    public string TextID;
    public NavigationWindow.Alignment Alignment;

    protected override void OnDestroy()
    {
      base.OnDestroy();
      NavigationWindow.DiscardCurrent();
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          NavigationWindow.Show(this.Template, LocalizedText.Get(this.TextID), this.Alignment);
          this.ActivateOutputLinks(10);
          break;
        case 2:
          NavigationWindow.DiscardCurrent();
          this.ActivateOutputLinks(10);
          this.ActivateOutputLinks(11);
          break;
      }
    }
  }
}
