namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(1, "Triggered", 1, 2), Pin(100, "Trigger", 0, 0), NodeType("Event/TriggerGlobalEvent", 0x7fe5), AddComponentMenu("")]
    public class FlowNode_TriggerGlobalEvent : FlowNode
    {
        [StringIsGlobalEventID]
        public string EventName;

        public FlowNode_TriggerGlobalEvent()
        {
            base..ctor();
            return;
        }

        public override string[] GetInfoLines()
        {
            string[] textArray1;
            if (string.IsNullOrEmpty(this.EventName) != null)
            {
                goto Label_002A;
            }
            textArray1 = new string[] { "Event is " + this.EventName };
            return textArray1;
        Label_002A:
            return base.GetInfoLines();
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_002C;
            }
            if (string.IsNullOrEmpty(this.EventName) != null)
            {
                goto Label_002C;
            }
            GlobalEvent.Invoke(this.EventName, this);
            base.ActivateOutputLinks(1);
        Label_002C:
            return;
        }
    }
}

