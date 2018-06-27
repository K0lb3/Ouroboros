// Decompiled with JetBrains decompiler
// Type: FlowNode_OnFriendSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(1, "Selected", FlowNode.PinTypes.Output, 0)]
[AddComponentMenu("")]
[FlowNode.NodeType("Event/OnFriendSelect", 58751)]
public class FlowNode_OnFriendSelect : FlowNodePersistent
{
  public override void OnActivate(int pinID)
  {
  }

  public void Selected()
  {
    this.ActivateOutputLinks(1);
  }
}
