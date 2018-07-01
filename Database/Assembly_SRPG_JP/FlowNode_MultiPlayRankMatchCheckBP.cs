// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayRankMatchCheckBP
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(11, "Disable", FlowNode.PinTypes.Output, 11)]
  [FlowNode.NodeType("Multi/MultiPlayRankMatchCheckBP", 32741)]
  [FlowNode.ShowInInspector]
  [FlowNode.Pin(0, "Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Enable", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_MultiPlayRankMatchCheckBP : FlowNode
  {
    private const int PIN_IN_CHECK = 0;
    private const int PIN_OUT_ENABLE = 10;
    private const int PIN_OUT_DISABLE = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.RankMatchBattlePoint > 0)
        this.ActivateOutputLinks(10);
      else
        this.ActivateOutputLinks(11);
    }
  }
}
