namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using UnityEngine;

    public class SwitchEquipment : AnimEvent
    {
        public eSwitchTarget SwitchPrimaryHand;
        public eSwitchTarget SwitchSecondaryHand;

        public SwitchEquipment()
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
            if (this.SwitchPrimaryHand == null)
            {
                goto Label_002B;
            }
            controller.SwitchEquipmentLists(0, this.SwitchPrimaryHand);
        Label_002B:
            if (this.SwitchSecondaryHand == null)
            {
                goto Label_0043;
            }
            controller.SwitchEquipmentLists(1, this.SwitchSecondaryHand);
        Label_0043:
            return;
        }

        public enum eSwitchTarget
        {
            NO_CHANGE,
            Element_0,
            Element_1,
            Element_2,
            Element_3,
            Element_4,
            Element_5,
            Element_6,
            Element_7,
            Element_8,
            Element_9
        }
    }
}

