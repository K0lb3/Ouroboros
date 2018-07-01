// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayRoomSetDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayRoomSetDraft", 32741)]
  [FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "Set Normal", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Set Draft", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_MultiPlayRoomSetDraft : FlowNode
  {
    private const int PIN_INPUT_SET_NORMAL = 0;
    private const int PIN_INPUT_SET_DRAFT = 1;
    private const int PIN_OUTPUT_FINISH = 2;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          GlobalVars.IsVersusDraftMode = false;
          break;
        case 1:
          GlobalVars.IsVersusDraftMode = true;
          break;
      }
      this.ActivateOutputLinks(2);
    }
  }
}
