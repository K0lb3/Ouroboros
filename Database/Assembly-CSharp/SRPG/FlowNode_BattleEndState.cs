// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleEndState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "Accept", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Battle/EndState", 32741)]
  [FlowNode.Pin(1, "Cancel", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_BattleEndState : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
      {
        SceneBattle.Instance.GotoNextState();
      }
      else
      {
        if (pinID != 1)
          return;
        SceneBattle.Instance.GotoPreviousState();
      }
    }
  }
}
