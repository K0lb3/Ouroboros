// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GlobalEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Triggered", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("Event/GlobalEvent", 58751)]
  [AddComponentMenu("")]
  public class FlowNode_GlobalEvent : FlowNodePersistent
  {
    [StringIsGlobalEventID]
    public string EventName;
    private string mRegisteredEventName;

    public override string[] GetInfoLines()
    {
      if (string.IsNullOrEmpty(this.EventName))
        return base.GetInfoLines();
      return new string[1]{ "Event is [" + this.EventName + "]" };
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

    public override void OnActivate(int pinID)
    {
    }
  }
}
