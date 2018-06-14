// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetScrollPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("UI/SetScrollPosition")]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_SetScrollPosition : FlowNode
  {
    public ScrollRect ScrollRect;
    public Vector2 NormalizedPosition;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        this.ScrollRect.set_normalizedPosition(this.NormalizedPosition);
      this.ActivateOutputLinks(1);
    }
  }
}
