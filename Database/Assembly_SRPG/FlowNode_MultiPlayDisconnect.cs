namespace SRPG
{
    using System;

    [Pin(0, "Disconnect", 0, 0), Pin(1, "Out", 1, 0), NodeType("Multi/MultiPlayDisconnect", 0x7fe5)]
    public class FlowNode_MultiPlayDisconnect : FlowNode
    {
        public FlowNode_MultiPlayDisconnect()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            MyPhoton photon;
            if (pinID != null)
            {
                goto Label_0025;
            }
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if (photon.CurrentState == null)
            {
                goto Label_001D;
            }
            photon.Disconnect();
        Label_001D:
            base.ActivateOutputLinks(1);
        Label_0025:
            return;
        }
    }
}

