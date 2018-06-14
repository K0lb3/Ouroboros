// Decompiled with JetBrains decompiler
// Type: FlowNode_DestroyObject
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Destroy", 32741)]
[FlowNode.Pin(10, "Destroy", FlowNode.PinTypes.Input, 0)]
public class FlowNode_DestroyObject : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (GameObject), false)]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null))
      return;
    Object.Destroy((Object) this.Target.get_gameObject());
  }
}
