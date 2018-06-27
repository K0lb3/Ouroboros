// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayIsRoomOwner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;

[FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 3)]
[FlowNode.NodeType("Multi/MultiPlayIsRoomOwner", 32741)]
[FlowNode.Pin(100, "Test", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 2)]
public class FlowNode_MultiPlayIsRoomOwner : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 100)
      return;
    if (PunMonoSingleton<MyPhoton>.Instance.IsOldestPlayer())
      this.ActivateOutputLinks(1);
    else
      this.ActivateOutputLinks(2);
  }
}
