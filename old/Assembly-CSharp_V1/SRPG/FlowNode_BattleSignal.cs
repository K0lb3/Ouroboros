// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleSignal
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Out", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.NodeType("Battle/Signal", 4513092)]
  [FlowNode.Pin(0, "Resume", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Stop", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_BattleSignal : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
            SceneBattle.Instance.UISignal = false;
          this.ActivateOutputLinks(2);
          break;
        case 1:
          if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
            SceneBattle.Instance.UISignal = true;
          this.ActivateOutputLinks(2);
          break;
      }
    }
  }
}
