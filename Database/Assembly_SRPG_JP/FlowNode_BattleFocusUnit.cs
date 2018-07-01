// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleFocusUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Battle/FocusUnit", 32741)]
  [FlowNode.Pin(0, "フォーカス", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_BattleFocusUnit : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      SceneBattle.Instance.ResetMoveCamera();
    }
  }
}
