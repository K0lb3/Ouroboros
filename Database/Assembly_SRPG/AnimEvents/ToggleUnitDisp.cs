namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using UnityEngine;

    public class ToggleUnitDisp : AnimEvent
    {
        public ToggleUnitDisp()
        {
            base..ctor();
            return;
        }

        public override void OnEnd(GameObject go)
        {
            UnitController controller;
            controller = go.GetComponentInParent<UnitController>();
            if (controller != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            controller.SetVisible(1);
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
            controller.SetVisible(0);
            return;
        }
    }
}

