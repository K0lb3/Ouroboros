// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattleFocusUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "フォーカス", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Battle/FocusUnit", 32741)]
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
