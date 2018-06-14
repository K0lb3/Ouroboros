// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleInteractable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Toggle/Interactable", 32741)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
public class FlowNode_ToggleInteractable : FlowNode
{
  [FlowNode.DropTarget(typeof (GameObject), true)]
  [FlowNode.ShowInInfo]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    Selectable selectable = !Object.op_Inequality((Object) this.Target, (Object) null) ? (Selectable) null : (Selectable) this.Target.GetComponent<Selectable>();
    switch (pinID)
    {
      case 10:
        if (Object.op_Inequality((Object) selectable, (Object) null))
        {
          selectable.set_interactable(true);
          break;
        }
        break;
      case 11:
        if (Object.op_Inequality((Object) selectable, (Object) null))
        {
          selectable.set_interactable(false);
          break;
        }
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
