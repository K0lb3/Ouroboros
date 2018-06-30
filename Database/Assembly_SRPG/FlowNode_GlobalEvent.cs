namespace SRPG
{
    using System;
    using UnityEngine;

    [NodeType("Event/GlobalEvent", 0xe57f), AddComponentMenu(""), Pin(1, "Triggered", 1, 0)]
    public class FlowNode_GlobalEvent : FlowNodePersistent
    {
        [StringIsGlobalEventID]
        public string EventName;
        private string mRegisteredEventName;

        public FlowNode_GlobalEvent()
        {
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            if (string.IsNullOrEmpty(this.EventName) != null)
            {
                goto Label_0039;
            }
            GlobalEvent.AddListener(this.EventName, new GlobalEvent.Delegate(this.OnGlobalEvent));
            this.mRegisteredEventName = this.EventName;
        Label_0039:
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

        protected override void OnDestroy()
        {
            if (string.IsNullOrEmpty(this.mRegisteredEventName) != null)
            {
                goto Label_0027;
            }
            GlobalEvent.RemoveListener(this.mRegisteredEventName, new GlobalEvent.Delegate(this.OnGlobalEvent));
        Label_0027:
            return;
        }

        private void OnGlobalEvent(object obj)
        {
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

