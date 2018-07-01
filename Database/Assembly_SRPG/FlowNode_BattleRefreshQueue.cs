// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleRefreshQueue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Battle/RefreshQueue", 32741)]
  [FlowNode.Pin(0, "行動順更新", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_BattleRefreshQueue : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || !Object.op_Inequality((Object) UnitQueue.Instance, (Object) null))
        return;
      UnitQueue.Instance.Refresh(0);
      this.ActivateOutputLinks(1);
    }
  }
}
