// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MilestoneTrigger
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.Pin(2, "Finished", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("Analytics/MilestoneTrigger", 32741)]
  [FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_MilestoneTrigger : FlowNode
  {
    public FlowNode_MilestoneTrigger.MilestoneType Milestone;

    public override void OnActivate(int pinID)
    {
      this.StartCoroutine(this.MileStoneAndBillBoardRecording(this.Milestone.ToString()));
    }

    [DebuggerHidden]
    private IEnumerator MileStoneAndBillBoardRecording(string milestone)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MilestoneTrigger.\u003CMileStoneAndBillBoardRecording\u003Ec__Iterator21() { milestone = milestone, \u003C\u0024\u003Emilestone = milestone, \u003C\u003Ef__this = this };
    }

    public enum MilestoneType
    {
      homescreen,
      title,
    }
  }
}
