namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(10, "Out", 1, 10), AddComponentMenu(""), NodeType("Event/ButtonEventLock", 0xc8bfe7), Pin(1, "Lock", 0, 1), Pin(2, "UnLock", 0, 2), Pin(3, "Reset", 0, 3), Pin(4, "AllReset", 0, 3)]
    public class FlowNode_ButtonEventLock : FlowNode
    {
        public string LockKey;

        public FlowNode_ButtonEventLock()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0017;
            }
            ButtonEvent.Lock(this.LockKey);
            goto Label_0051;
        Label_0017:
            if (pinID != 2)
            {
                goto Label_002E;
            }
            ButtonEvent.UnLock(this.LockKey);
            goto Label_0051;
        Label_002E:
            if (pinID != 3)
            {
                goto Label_0045;
            }
            ButtonEvent.ResetLock(this.LockKey);
            goto Label_0051;
        Label_0045:
            if (pinID != 4)
            {
                goto Label_0051;
            }
            ButtonEvent.Reset();
        Label_0051:
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

