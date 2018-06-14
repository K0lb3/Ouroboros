// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AnalyticsTracker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Analytics/AnalyticsTracker", 32741)]
  [FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_AnalyticsTracker : FlowNode
  {
    public AnalyticsManager.TrackingType trackingType;

    public override void OnActivate(int pinID)
    {
      Debug.Log((object) ("FlownodeAnalyticsTracker => " + ((Object) this).get_name() + "-->" + (object) this.trackingType));
      AnalyticsManager.TrackTutorialEventGeneric(this.trackingType);
      this.ActivateOutputLinks(1);
    }
  }
}
