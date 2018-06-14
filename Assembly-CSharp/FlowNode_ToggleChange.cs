// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
[FlowNode.NodeType("Event/ToggleChange", 58751)]
[FlowNode.Pin(2, "disable", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "enable", FlowNode.PinTypes.Input, 0)]
public class FlowNode_ToggleChange : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (Toggle), true)]
  public Toggle Target;

  public override void OnActivate(int pinID)
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null))
      return;
    if (pinID == 1)
    {
      this.Target.set_isOn(true);
    }
    else
    {
      if (pinID != 2)
        return;
      this.Target.set_isOn(false);
    }
  }
}
