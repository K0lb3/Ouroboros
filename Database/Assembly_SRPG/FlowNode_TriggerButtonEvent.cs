namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(100, "Trigger", 0, 0), NodeType("Event/TriggerButtonEvent", 0xf578ad), AddComponentMenu(""), Pin(1, "Triggered", 1, 2)]
    public class FlowNode_TriggerButtonEvent : FlowNode
    {
        public bool Force;
        public string EventName;
        public SerializeValue Value;

        public FlowNode_TriggerButtonEvent()
        {
            this.EventName = string.Empty;
            this.Value = new SerializeValue();
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0052;
            }
            if (string.IsNullOrEmpty(this.EventName) != null)
            {
                goto Label_0052;
            }
            if (this.Force == null)
            {
                goto Label_0039;
            }
            ButtonEvent.ForceInvoke(this.EventName, this.Value);
            goto Label_004A;
        Label_0039:
            ButtonEvent.Invoke(this.EventName, this.Value);
        Label_004A:
            base.ActivateOutputLinks(1);
        Label_0052:
            return;
        }
    }
}

