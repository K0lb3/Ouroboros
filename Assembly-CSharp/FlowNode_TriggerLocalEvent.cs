// Decompiled with JetBrains decompiler
// Type: FlowNode_TriggerLocalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[AddComponentMenu("")]
[FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(100, "Trigger", FlowNode.PinTypes.Input, 0)]
[FlowNode.NodeType("Event/TriggerLocalEvent", 32741)]
public class FlowNode_TriggerLocalEvent : FlowNode
{
  [StringIsLocalEventID]
  public string EventName;

  public override string[] GetInfoLines()
  {
    if (string.IsNullOrEmpty(this.EventName))
      return base.GetInfoLines();
    return new string[1]
    {
      "Event is [" + this.EventName + "]"
    };
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 100 || string.IsNullOrEmpty(this.EventName))
      return;
    FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, this.EventName);
    this.ActivateOutputLinks(1);
  }

  public static void TriggerLocalEvent(Component caller, string EventName)
  {
    FlowNode_LocalEvent[] componentsInChildren = (FlowNode_LocalEvent[]) caller.GetComponentsInChildren<FlowNode_LocalEvent>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (((Component) componentsInChildren[index]).get_gameObject().get_activeInHierarchy() && componentsInChildren[index].EventName == EventName)
        componentsInChildren[index].Activate(-1);
    }
  }
}
