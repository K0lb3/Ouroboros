// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleInteractable
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Toggle/Interactable", 32741)]
public class FlowNode_ToggleInteractable : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
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
