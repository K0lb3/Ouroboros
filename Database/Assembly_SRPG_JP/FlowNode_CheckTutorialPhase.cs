// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckTutorialPhase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(0, "Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(3, "召喚完了済み", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(2, "チュートリアル召喚後(未確定)", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "チュートリアル召喚前", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Tutorial/CheckPhase(進行状況確認)", 32741)]
  public class FlowNode_CheckTutorialPhase : FlowNode
  {
    private const int PIN_IN_CHECK = 0;
    private const int PIN_OT_BEFORE_GACHA = 1;
    private const int PIN_OT_AFTER_GACHA = 2;
    private const int PIN_OT_PREV_VERSION_GACHA = 3;

    public override void OnActivate(int pinID)
    {
      int pinID1 = 1;
      if (MonoSingleton<GameManager>.Instance.Player.Units.Count == 4)
        pinID1 = 3;
      else if (FlowNode_Variable.Get("REDRAW_GACHA_PENDING") == "1")
        pinID1 = 2;
      this.ActivateOutputLinks(pinID1);
    }
  }
}
