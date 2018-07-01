// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkRetryWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/NetworkRetryWindow", 32741)]
  [FlowNode.Pin(0, "Create", FlowNode.PinTypes.Input, 0)]
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
