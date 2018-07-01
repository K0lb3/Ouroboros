// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayFirstContact
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "no", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Multi/MultiPlayFirstContact", 32741)]
  [FlowNode.Pin(0, "exist", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "yes", FlowNode.PinTypes.Output, 0)]
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
