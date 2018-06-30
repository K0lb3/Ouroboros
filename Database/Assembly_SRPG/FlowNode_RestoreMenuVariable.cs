namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(11, "== Variable", 1, 10), Pin(12, "!= Variable", 1, 11), NodeType("System/RestoreMenuVariable", 0x7fe5), Pin(0, "Set", 0, 1), Pin(1, "Compare", 0, 2), Pin(10, "Assigned", 1, 9)]
    public class FlowNode_RestoreMenuVariable : FlowNode
    {
        private const int PIN_ID_SET = 0;
        private const int PIN_ID_COMPARE = 1;
        private const int PIN_ID_ASSIGNED = 10;
        private const int PIN_ID_EQUAL = 11;
        private const int PIN_ID_UNEQUAL = 12;
        public RestorePoints RestorePoint;
        [HideInInspector]
        public RestorePoints ResetRestorePoint;
        [HideInInspector]
        public bool AutoReset;

        public FlowNode_RestoreMenuVariable()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            if (num == null)
            {
                goto Label_0014;
            }
            if (num == 1)
            {
                goto Label_002D;
            }
            goto Label_006F;
        Label_0014:
            HomeWindow.SetRestorePoint(this.RestorePoint);
            base.ActivateOutputLinks(10);
            goto Label_006F;
        Label_002D:
            if (HomeWindow.GetRestorePoint() != this.RestorePoint)
            {
                goto Label_004B;
            }
            base.ActivateOutputLinks(11);
            goto Label_0054;
        Label_004B:
            base.ActivateOutputLinks(12);
        Label_0054:
            if (this.AutoReset == null)
            {
                goto Label_006F;
            }
            HomeWindow.SetRestorePoint(this.ResetRestorePoint);
        Label_006F:
            return;
        }
    }
}

