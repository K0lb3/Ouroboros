// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayReady
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;

[FlowNode.Pin(101, "Wait", FlowNode.PinTypes.Input, 1)]
[FlowNode.Pin(1, "ChangedToReady", FlowNode.PinTypes.Output, 100)]
[FlowNode.Pin(2, "ChangedToWait", FlowNode.PinTypes.Output, 101)]
[FlowNode.Pin(3, "ChangedToEdit", FlowNode.PinTypes.Output, 102)]
[FlowNode.Pin(102, "Edit", FlowNode.PinTypes.Input, 2)]
[FlowNode.Pin(103, "FloorSelect", FlowNode.PinTypes.Input, 3)]
[FlowNode.Pin(100, "Ready", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Multi/MultiPlayReady", 32741)]
public class FlowNode_MultiPlayReady : FlowNode
{
  public override void OnActivate(int pinID)
  {
    MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
    JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(instance.GetMyPlayer().json);
    switch (pinID)
    {
      case 100:
        DebugUtility.Log("[MultiPlay]Ready!");
        photonPlayerParam.state = 1;
        instance.SetMyPlayerParam(photonPlayerParam.Serialize());
        this.ActivateOutputLinks(1);
        break;
      case 101:
        DebugUtility.Log("[MultiPlay]Cancel ready...");
        photonPlayerParam.state = 0;
        instance.SetMyPlayerParam(photonPlayerParam.Serialize());
        this.ActivateOutputLinks(2);
        break;
      case 102:
        DebugUtility.Log("[MultiPlay]Change Edit!");
        photonPlayerParam.state = 4;
        instance.SetMyPlayerParam(photonPlayerParam.Serialize());
        this.ActivateOutputLinks(3);
        break;
      case 103:
        DebugUtility.Log("[MultiPlay]Change FloorSelect!");
        photonPlayerParam.state = 5;
        instance.SetMyPlayerParam(photonPlayerParam.Serialize());
        this.ActivateOutputLinks(3);
        break;
    }
  }
}
