// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalNotification
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

[FlowNode.Pin(1, "SetUpEnd", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("System/LocalNotification", 65535)]
[FlowNode.Pin(0, "SetUp", FlowNode.PinTypes.Input, 0)]
public class FlowNode_LocalNotification : FlowNode
{
  public string path = "Data/Localnotification";

  private void Init()
  {
    MyLocalNotification.Setup(this.path);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 0)
      return;
    this.Init();
    this.ActivateOutputLinks(1);
  }
}
