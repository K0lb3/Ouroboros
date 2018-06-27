// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayFirstContact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "exist", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "yes", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Multi/MultiPlayFirstContact", 32741)]
  [FlowNode.Pin(2, "no", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_MultiPlayFirstContact : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ActivateOutputLinks(Object.op_Equality((Object) SceneBattle.Instance, (Object) null) || SceneBattle.Instance.FirstContact <= 0 ? 2 : 1);
    }
  }
}
