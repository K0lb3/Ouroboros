namespace SRPG
{
    using System;

    [Pin(0, "False", 1, 0), Pin(1, "True", 1, 1), Pin(2, "Check", 0, 2), NodeType("System/GetBoolPlayerPrefs", 0x7fe5)]
    public class FlowNode_GetBoolPlayerPrefs : FlowNode
    {
        private const int CHECK = 2;
        private const int GET_FALSE = 0;
        private const int GET_TRUE = 1;
        public string Name;

        public FlowNode_GetBoolPlayerPrefs()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            int num2;
            num2 = pinID;
            if (num2 == 2)
            {
                goto Label_000E;
            }
            goto Label_0031;
        Label_000E:
            num = PlayerPrefsUtility.GetInt(this.Name, 0);
            base.ActivateOutputLinks((num != 1) ? 0 : 1);
            return;
        Label_0031:
            base.ActivateOutputLinks(0);
            return;
        }
    }
}

