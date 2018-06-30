namespace SRPG
{
    using System;
    using System.Collections.Generic;

    [Pin(20, "OnCreate", 1, 0), Pin(0, "Destroy", 0, 0), Pin(10, "OnDestroy", 1, 0), NodeType("NetworkIndicator", 0x7fe5), Pin(5, "Create", 0, 0)]
    public class FlowNode_NetworkIndicator : FlowNode
    {
        private NetworkIndicator mRef;

        public FlowNode_NetworkIndicator()
        {
            this.mRef = new NetworkIndicator();
            base..ctor();
            return;
        }

        public static bool NeedDisplay()
        {
            return NetworkIndicator.NeedDisplay();
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_001F;
            }
            this.mRef.Disable();
            base.ActivateOutputLinks(10);
            goto Label_003A;
        Label_001F:
            if (pinID != 5)
            {
                goto Label_003A;
            }
            this.mRef.Enable();
            base.ActivateOutputLinks(20);
        Label_003A:
            return;
        }

        protected override void OnDestroy()
        {
            this.mRef.Disable();
            return;
        }

        private class NetworkIndicator
        {
            private static List<FlowNode_NetworkIndicator.NetworkIndicator> mInstances;
            private bool mActive;

            static NetworkIndicator()
            {
                mInstances = new List<FlowNode_NetworkIndicator.NetworkIndicator>();
                return;
            }

            public NetworkIndicator()
            {
                base..ctor();
                return;
            }

            public void Disable()
            {
                if (this.mActive == null)
                {
                    goto Label_001E;
                }
                this.mActive = 0;
                mInstances.Remove(this);
            Label_001E:
                return;
            }

            public void Enable()
            {
                if (this.mActive != null)
                {
                    goto Label_001D;
                }
                this.mActive = 1;
                mInstances.Add(this);
            Label_001D:
                return;
            }

            protected override void Finalize()
            {
            Label_0000:
                try
                {
                    this.Disable();
                    goto Label_0012;
                }
                finally
                {
                Label_000B:
                    base.Finalize();
                }
            Label_0012:
                return;
            }

            public static bool NeedDisplay()
            {
                return (mInstances.Count > 0);
            }
        }
    }
}

