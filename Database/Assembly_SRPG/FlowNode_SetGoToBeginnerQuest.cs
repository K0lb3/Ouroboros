namespace SRPG
{
    using System;

    [Pin(1, "SetFalse", 0, 1), NodeType("System/SetGoToBeginnerQuest", 0x7fe5), Pin(0, "SetTrue", 0, 0), Pin(100, "Result", 1, 100)]
    public class FlowNode_SetGoToBeginnerQuest : FlowNode
    {
        private const int IN_SET_TRUE = 0;
        private const int IN_SET_FALSE = 1;
        private const int OUT_RESULT = 100;
        public string Name;

        public FlowNode_SetGoToBeginnerQuest()
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
                goto Label_001F;
            }
            goto Label_002A;
        Label_0014:
            GlobalVars.RestoreBeginnerQuest = 1;
            goto Label_002A;
        Label_001F:
            GlobalVars.RestoreBeginnerQuest = 0;
        Label_002A:
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

