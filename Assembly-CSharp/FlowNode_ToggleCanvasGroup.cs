// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleCanvasGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(1, "Turn On Block Raycasts", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(6, "Turn Off interactive", FlowNode.PinTypes.Input, 6)]
[FlowNode.Pin(5, "Turn On interactive", FlowNode.PinTypes.Input, 5)]
[FlowNode.Pin(4, "Hide", FlowNode.PinTypes.Input, 4)]
[FlowNode.Pin(3, "Show", FlowNode.PinTypes.Input, 3)]
[FlowNode.Pin(2, "Turn Off Block Raycasts", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(0, "Out", FlowNode.PinTypes.Output, 999)]
[FlowNode.NodeType("Toggle/CanvasGroup", 32741)]
public class FlowNode_ToggleCanvasGroup : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (CanvasGroup), true)]
  public CanvasGroup Target;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 1:
        if (Object.op_Inequality((Object) this.Target, (Object) null))
          this.Target.set_blocksRaycasts(true);
        this.ActivateOutputLinks(0);
        break;
      case 2:
        if (Object.op_Inequality((Object) this.Target, (Object) null))
          this.Target.set_blocksRaycasts(false);
        this.ActivateOutputLinks(0);
        break;
      case 3:
        if (Object.op_Inequality((Object) this.Target, (Object) null))
          this.Target.set_alpha(1f);
        this.ActivateOutputLinks(0);
        break;
      case 4:
        if (Object.op_Inequality((Object) this.Target, (Object) null))
          this.Target.set_alpha(0.0f);
        this.ActivateOutputLinks(0);
        break;
      case 5:
        if (Object.op_Inequality((Object) this.Target, (Object) null))
          this.Target.set_interactable(true);
        this.ActivateOutputLinks(0);
        break;
      case 6:
        if (Object.op_Inequality((Object) this.Target, (Object) null))
          this.Target.set_interactable(false);
        this.ActivateOutputLinks(0);
        break;
    }
  }
}
