// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MediaPlayerDispatchFinishEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("AVProVideo/MediaPlayerDispatchFinishEvent")]
  [FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_MediaPlayerDispatchFinishEvent : FlowNode
  {
    public FlowNode_MediaPlayerDispatchFinishEvent.OnEnd onEnd;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (this.onEnd != null)
        this.onEnd();
      this.ActivateOutputLinks(10);
    }

    public delegate void OnEnd();
  }
}
