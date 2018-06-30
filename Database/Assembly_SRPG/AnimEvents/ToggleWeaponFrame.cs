namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using UnityEngine;

    public class ToggleWeaponFrame : AnimEvent
    {
        public SHOW_TYPE Primary;
        public SHOW_TYPE Secondary;

        public ToggleWeaponFrame()
        {
            base..ctor();
            return;
        }

        public override void OnEnd(GameObject go)
        {
            UnitController controller;
            bool flag;
            controller = go.GetComponentInParent<UnitController>();
            if ((controller == null) == null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            flag = 0;
            if (this.Primary == null)
            {
                goto Label_003C;
            }
            flag = (this.Primary != 1) ? 0 : 1;
            controller.SetPrimaryEquipmentsVisible(flag);
        Label_003C:
            if (this.Secondary == null)
            {
                goto Label_0062;
            }
            flag = (this.Secondary != 1) ? 0 : 1;
            controller.SetSecondaryEquipmentsVisible(flag);
        Label_0062:
            return;
        }

        public override void OnStart(GameObject go)
        {
            UnitController controller;
            bool flag;
            controller = go.GetComponentInParent<UnitController>();
            if ((controller == null) == null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            flag = 0;
            if (this.Primary == null)
            {
                goto Label_003C;
            }
            flag = (this.Primary != 1) ? 1 : 0;
            controller.SetPrimaryEquipmentsVisible(flag);
        Label_003C:
            if (this.Secondary == null)
            {
                goto Label_0062;
            }
            flag = (this.Secondary != 1) ? 1 : 0;
            controller.SetSecondaryEquipmentsVisible(flag);
        Label_0062:
            return;
        }

        public enum SHOW_TYPE
        {
            KEEP,
            HIDDEN,
            VISIBLE
        }
    }
}

