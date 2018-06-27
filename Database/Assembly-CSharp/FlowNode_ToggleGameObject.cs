// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleGameObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Toggle/GameObject", 32741)]
[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 2)]
public class FlowNode_ToggleGameObject : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), true)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 10:
        if (Object.op_Inequality((Object) this.Target, (Object) null))
        {
          this.Target.SetActive(true);
          break;
        }
        break;
      case 11:
        if (Object.op_Inequality((Object) this.Target, (Object) null))
        {
          this.Target.SetActive(false);
          break;
        }
        break;
    }
    this.ActivateOutputLinks(1);
  }
}
