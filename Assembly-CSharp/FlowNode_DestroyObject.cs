// Decompiled with JetBrains decompiler
// Type: FlowNode_DestroyObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(10, "Destroy", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Destroy", 32741)]
public class FlowNode_DestroyObject : FlowNode
{
  [FlowNode.DropTarget(typeof (GameObject), false)]
  [FlowNode.ShowInInfo]
  public GameObject Target;

  public override void OnActivate(int pinID)
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null))
      return;
    Object.Destroy((Object) this.Target.get_gameObject());
  }
}
