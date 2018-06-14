// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TriggerGlobalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Event/TriggerGlobalEvent", 32741)]
  [AddComponentMenu("")]
  [FlowNode.Pin(100, "Trigger", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_TriggerGlobalEvent : FlowNode
  {
    [StringIsGlobalEventID]
    public string EventName;

    public override string[] GetInfoLines()
    {
      if (string.IsNullOrEmpty(this.EventName))
        return base.GetInfoLines();
      return new string[1]{ "Event is [" + this.EventName + "]" };
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 100 || string.IsNullOrEmpty(this.EventName))
        return;
      GlobalEvent.Invoke(this.EventName, (object) this);
      this.ActivateOutputLinks(1);
    }
  }
}
