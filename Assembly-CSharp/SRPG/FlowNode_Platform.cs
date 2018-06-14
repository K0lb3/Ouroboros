// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Platform
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Platform/Switch", 32741)]
  [FlowNode.Pin(103, "DMM", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "EDITOR", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "iOS", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Android", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_Platform : FlowNode
  {
    private const int PIN_PLATFORM_IOS = 101;
    private const int PIN_PLATFORM_ANDROID = 102;
    private const int PIN_PLATFORM_DMM = 103;
    private const int PIN_PLATFORM_EDITOR = 104;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ActivateOutputLinks(102);
    }
  }
}
