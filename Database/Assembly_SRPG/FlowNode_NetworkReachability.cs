namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(4, "Not Connected", 1, 0x66), Pin(3, "Carrier", 1, 0x65), Pin(2, "Wifi", 1, 100), NodeType("Network/NetworkReachability", 0x7fe5), Pin(1, "In", 0, 0)]
    public class FlowNode_NetworkReachability : FlowNode
    {
        private const int PIN_ID_IN = 1;
        private const int PIN_ID_WIFI = 2;
        private const int PIN_ID_CARRIER = 3;
        private const int PIN_ID_NOT_CONNECTED = 4;

        public FlowNode_NetworkReachability()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            NetworkReachability reachability;
            if (pinID != 1)
            {
                goto Label_004B;
            }
            switch (Application.get_internetReachability())
            {
                case 0:
                    goto Label_003E;

                case 1:
                    goto Label_0031;

                case 2:
                    goto Label_0024;
            }
            goto Label_004B;
        Label_0024:
            base.ActivateOutputLinks(2);
            goto Label_004B;
        Label_0031:
            base.ActivateOutputLinks(3);
            goto Label_004B;
        Label_003E:
            base.ActivateOutputLinks(4);
        Label_004B:
            return;
        }
    }
}

