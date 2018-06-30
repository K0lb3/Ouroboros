namespace SRPG
{
    using System;
    using UnityEngine;

    [NodeType("System/Location", 0x7fe5), Pin(2, "Failed", 1, 11), Pin(100, "Start", 0, 0), Pin(1, "Success", 1, 10)]
    public class FlowNode_Location : FlowNode
    {
        private Location m_Location;

        public FlowNode_Location()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_0027;
            }
            if (Location.isGPSEnable != null)
            {
                goto Label_001A;
            }
            this.OnFailed(null);
            return;
        Label_001A:
            this.Start();
            base.set_enabled(1);
        Label_0027:
            return;
        }

        private void OnFailed(Location gps)
        {
            GlobalVars.Location = Vector2.get_zero();
            base.ActivateOutputLinks(2);
            return;
        }

        private void OnSuccess(Location gps)
        {
            GlobalVars.Location = (gps == null) ? Vector2.get_zero() : gps.location;
            base.ActivateOutputLinks(1);
            return;
        }

        private void Start()
        {
            if (this.m_Location != null)
            {
                goto Label_0044;
            }
            this.m_Location = new Location();
            this.m_Location.Initialize();
            this.m_Location.Start(new Action<Location>(this.OnSuccess), new Action<Location>(this.OnFailed));
        Label_0044:
            return;
        }

        private void Update()
        {
            if (this.m_Location == null)
            {
                goto Label_003D;
            }
            this.m_Location.Update();
            if (this.m_Location.IsBusy() != null)
            {
                goto Label_0044;
            }
            this.m_Location.Release();
            this.m_Location = null;
            goto Label_0044;
        Label_003D:
            base.set_enabled(0);
        Label_0044:
            return;
        }
    }
}

