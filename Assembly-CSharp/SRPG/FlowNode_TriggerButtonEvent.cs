// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TriggerButtonEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Trigger", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Event/TriggerButtonEvent", 16087213)]
  [AddComponentMenu("")]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_TriggerButtonEvent : FlowNode
  {
    public string EventName = string.Empty;
    public SerializeValue Value = new SerializeValue();
    public bool Force;

    public override void OnActivate(int pinID)
    {
      if (pinID != 100 || string.IsNullOrEmpty(this.EventName))
        return;
      if (this.Force)
        ButtonEvent.ForceInvoke(this.EventName, (object) this.Value);
      else
        ButtonEvent.Invoke(this.EventName, (object) this.Value);
      this.ActivateOutputLinks(1);
    }
  }
}
