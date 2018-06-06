// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayIsRoomOwner
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;

[FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 2)]
[FlowNode.NodeType("Multi/MultiPlayIsRoomOwner", 32741)]
[FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 3)]
[FlowNode.Pin(100, "Test", FlowNode.PinTypes.Input, 0)]
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
