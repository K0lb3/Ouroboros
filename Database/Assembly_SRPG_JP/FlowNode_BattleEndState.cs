// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleEndState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "Cancel", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "Accept", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Battle/EndState", 32741)]
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
