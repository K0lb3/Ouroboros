// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MoveGameObject
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("MoveGameObject", 32741)]
  public class FlowNode_MoveGameObject : FlowNode
  {
    public float Time = 1f;
    public GameObject Target;
    public GameObject Destination;
    public ObjectAnimator.CurveType InterpolationMode;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Object.op_Inequality((Object) this.Target, (Object) null) && Object.op_Inequality((Object) this.Destination, (Object) null))
      {
        Transform transform = this.Destination.get_transform();
        ObjectAnimator.Get(this.Target).AnimateTo(transform.get_position(), transform.get_rotation(), this.Time, this.InterpolationMode);
      }
      this.ActivateOutputLinks(1);
    }
  }
}
