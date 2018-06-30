namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using UnityEngine;

    public class ResetEquipment : AnimEvent
    {
        public bool IsNoResetPrimaryHand;
        public bool IsNoResetSecondaryHand;

        public ResetEquipment()
        {
            base..ctor();
            return;
        }

        public override void OnStart(GameObject go)
        {
            UnitController controller;
            controller = go.GetComponentInParent<UnitController>();
            if (controller != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (this.IsNoResetPrimaryHand != null)
            {
                goto Label_0025;
            }
            controller.ResetEquipmentLists(0);
        Label_0025:
            if (this.IsNoResetSecondaryHand != null)
            {
                goto Label_0037;
            }
            controller.ResetEquipmentLists(1);
        Label_0037:
            return;
        }
    }
}

