// Decompiled with JetBrains decompiler
// Type: FlowNode_LocalNotification
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

[FlowNode.Pin(0, "SetUp", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "SetUpEnd", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("System/LocalNotification", 65535)]
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
