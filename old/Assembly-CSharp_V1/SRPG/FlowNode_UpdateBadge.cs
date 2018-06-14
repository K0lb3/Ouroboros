// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UpdateBadge
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("UI/UpdateBadge", 32741)]
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
