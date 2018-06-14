// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayNeedRoomPassCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Multi/MultiPlayNeedRoomPassCode", 32741)]
  [FlowNode.Pin(0, "Test", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_MultiPlayNeedRoomPassCode : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MultiPlayAPIRoom.IsLocked(GlobalVars.SelectedMultiPlayRoomPassCodeHash))
        this.ActivateOutputLinks(1);
      else
        this.ActivateOutputLinks(2);
    }
  }
}
