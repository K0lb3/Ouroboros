// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GlobalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Event/GlobalEvent", 58751)]
  [AddComponentMenu("")]
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_GlobalEvent : FlowNodePersistent
  {
    [StringIsGlobalEventID]
    public string EventName;
    private string mRegisteredEventName;

    public override string[] GetInfoLines()
    {
      if (string.IsNullOrEmpty(this.EventName))
        return base.GetInfoLines();
      return new string[1]{ "Event is " + this.EventName };
    }

    protected override void Awake()
    {
      base.Awake();
      if (string.IsNullOrEmpty(this.EventName))
        return;
      GlobalEvent.AddListener(this.EventName, new GlobalEvent.Delegate(this.OnGlobalEvent));
      this.mRegisteredEventName = this.EventName;
    }

    protected override void OnDestroy()
    {
      if (string.IsNullOrEmpty(this.mRegisteredEventName))
        return;
      GlobalEvent.RemoveListener(this.mRegisteredEventName, new GlobalEvent.Delegate(this.OnGlobalEvent));
    }

    private void OnGlobalEvent(object obj)
    {
      this.ActivateOutputLinks(1);
    }
  }
}
