// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayerLevelUp
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "レベルアップした", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Battle/プレイヤーレベルアップ", 32741)]
  [FlowNode.Pin(0, "実行", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "レベルアップしなかった", FlowNode.PinTypes.Output, 2)]
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
