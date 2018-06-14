// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayerLevelUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(2, "レベルアップしなかった", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("Battle/プレイヤーレベルアップ", 32741)]
  [FlowNode.Pin(0, "実行", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "レベルアップした", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_PlayerLevelUp : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      int num = PlayerData.CalcLevelFromExp((int) GlobalVars.PlayerExpOld);
      if (PlayerData.CalcLevelFromExp((int) GlobalVars.PlayerExpNew) == num)
        this.ActivateOutputLinks(2);
      else
        this.ActivateOutputLinks(1);
    }
  }
}
