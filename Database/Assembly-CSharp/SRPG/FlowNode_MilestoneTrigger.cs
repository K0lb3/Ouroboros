// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MilestoneTrigger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Finished", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Analytics/MilestoneTrigger", 32741)]
  public class FlowNode_MilestoneTrigger : FlowNode
  {
    [SerializeField]
    private FlowNode_MilestoneTrigger.MilestoneType Milestone;
    private static string mLastRecordedSessionID;

    public override void OnActivate(int pinID)
    {
      if (!string.IsNullOrEmpty(FlowNode_MilestoneTrigger.mLastRecordedSessionID) && !(FlowNode_MilestoneTrigger.mLastRecordedSessionID != Network.SessionID))
        return;
      DebugUtility.Log("shown <color=red>Milestone:" + this.Milestone.ToString());
      FlowNode_MilestoneTrigger.mLastRecordedSessionID = Network.SessionID;
      AnalyticsManager.AttemptToShowPlacement(this.Milestone.ToString(), new Action(this.OnPlacementShown));
      this.ActivateOutputLinks(1);
    }

    private void OnPlacementShown()
    {
      this.ActivateOutputLinks(2);
    }

    [SerializeField]
    private enum MilestoneType
    {
      homescreen,
      title,
    }
  }
}
