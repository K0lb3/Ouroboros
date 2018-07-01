// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TriggerButtonEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
