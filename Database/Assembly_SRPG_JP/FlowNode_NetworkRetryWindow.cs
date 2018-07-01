// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkRetryWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Create", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/NetworkRetryWindow", 32741)]
  public class FlowNode_NetworkRetryWindow : FlowNode
  {
    public GameObject Window;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || !Object.op_Inequality((Object) this.Window, (Object) null))
        return;
      Object.Instantiate<GameObject>((M0) this.Window);
    }
  }
}
