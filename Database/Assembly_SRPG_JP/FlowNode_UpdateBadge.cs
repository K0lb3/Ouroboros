// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("UI/UpdateBadge", 32741)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_UpdateBadge : FlowNode
  {
    public GameManager.BadgeTypes type;

    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(this.type);
      this.ActivateOutputLinks(10);
    }
  }
}
