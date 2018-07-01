// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayTestRoomPassCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(2, "NG", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "OK", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(0, "Test", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/MultiPlayTestRoomPassCode", 32741)]
  public class FlowNode_MultiPlayTestRoomPassCode : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (string.IsNullOrEmpty(GlobalVars.SelectedMultiPlayRoomPassCodeHash))
      {
        this.ActivateOutputLinks(1);
      }
      else
      {
        string str = MultiPlayAPIRoom.CalcHash(GlobalVars.EditMultiPlayRoomPassCode);
        DebugUtility.Log("CheckPass...:" + GlobalVars.EditMultiPlayRoomPassCode + " > " + str + " vs " + GlobalVars.SelectedMultiPlayRoomPassCodeHash);
        if (GlobalVars.SelectedMultiPlayRoomPassCodeHash.Equals(str))
          this.ActivateOutputLinks(1);
        else
          this.ActivateOutputLinks(2);
      }
    }
  }
}
