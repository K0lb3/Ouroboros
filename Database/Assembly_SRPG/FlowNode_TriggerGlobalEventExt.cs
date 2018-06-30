namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(0x65, "Back", 0, 0), AddComponentMenu(""), NodeType("Event/TriggerGlobalEventExt", 0x7fe5)]
    public class FlowNode_TriggerGlobalEventExt : FlowNode_TriggerGlobalEvent
    {
        [StringIsGlobalEventID]
        public string CurrEventName;
        public bool SceneChange;

        public FlowNode_TriggerGlobalEventExt()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_002F;
            }
            if (string.IsNullOrEmpty(base.EventName) != null)
            {
                goto Label_002F;
            }
            GlobalVars.PreEventName = this.CurrEventName;
            this.SceneInvoke(base.EventName);
        Label_002F:
            if (pinID != 0x65)
            {
                goto Label_007D;
            }
            if (string.IsNullOrEmpty(GlobalVars.PreEventName) != null)
            {
                goto Label_0056;
            }
            this.SceneInvoke(GlobalVars.PreEventName);
            goto Label_0072;
        Label_0056:
            if (string.IsNullOrEmpty(base.EventName) != null)
            {
                goto Label_0072;
            }
            this.SceneInvoke(base.EventName);
        Label_0072:
            GlobalVars.PreEventName = this.CurrEventName;
        Label_007D:
            return;
        }

        private void SceneInvoke(string event_name)
        {
            GlobalVars.ForceSceneChange = this.SceneChange;
            GlobalEvent.Invoke(event_name, this);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

