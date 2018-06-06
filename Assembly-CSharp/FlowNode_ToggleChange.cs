// Decompiled with JetBrains decompiler
// Type: FlowNode_ToggleChange
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
[FlowNode.Pin(1, "enable", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Event/ToggleChange", 58751)]
public class FlowNode_ToggleChange : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (Toggle), true)]
  public Toggle Target;

  public override void OnActivate(int pinID)
  {
    if (pinID != 1 || !Object.op_Inequality((Object) this.Target, (Object) null))
      return;
    this.Target.set_isOn(true);
  }
}
