// Decompiled with JetBrains decompiler
// Type: FlowNode_OnPointerRelease
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(0, "Released", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/OnPointerRelease", 58751)]
public class FlowNode_OnPointerRelease : FlowNodePersistent
{
  private bool mPressed;

  public override void OnActivate(int pinID)
  {
  }

  private void OnDisable()
  {
    this.mPressed = false;
  }

  private void Update()
  {
    bool mPressed = this.mPressed;
    this.mPressed = Input.GetMouseButton(0);
    if (this.mPressed || !mPressed)
      return;
    this.ActivateOutputLinks(0);
  }
}
