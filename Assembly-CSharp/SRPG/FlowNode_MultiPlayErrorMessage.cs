// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayErrorMessage
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayErrorMessage", 32741)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Closed", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_MultiPlayErrorMessage : FlowNode
  {
    private GameObject winGO;

    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      this.winGO = UIUtility.SystemMessage((string) null, Network.ErrMsg, (UIUtility.DialogResultEvent) (go =>
      {
        if (!Object.op_Inequality((Object) this.winGO, (Object) null))
          return;
        this.winGO = (GameObject) null;
        this.ActivateOutputLinks(100);
      }), (GameObject) null, false, -1);
      Network.ResetError();
    }
  }
}
