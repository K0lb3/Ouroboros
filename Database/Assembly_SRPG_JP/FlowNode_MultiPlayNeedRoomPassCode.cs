// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayNeedRoomPassCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
