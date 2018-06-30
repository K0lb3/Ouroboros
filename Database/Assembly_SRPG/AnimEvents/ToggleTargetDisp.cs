namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ToggleTargetDisp : AnimEvent
    {
        public ToggleTargetDisp()
        {
            base..ctor();
            return;
        }

        private TacticsUnitController getTarget(TacticsUnitController tuc)
        {
            List<TacticsUnitController> list;
            if ((tuc == null) == null)
            {
                goto Label_000E;
            }
            return null;
        Label_000E:
            list = tuc.GetTargetTucLists();
            if (list == null)
            {
                goto Label_0026;
            }
            if (list.Count != null)
            {
                goto Label_0028;
            }
        Label_0026:
            return null;
        Label_0028:
            return list[0];
        }

        public override void OnEnd(GameObject go)
        {
            TacticsUnitController controller;
            TacticsUnitController controller2;
            SceneBattle battle;
            controller = go.GetComponentInParent<TacticsUnitController>();
            if (controller != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            controller2 = this.getTarget(controller);
            if (controller2 != null)
            {
                goto Label_0027;
            }
            return;
        Label_0027:
            controller2.SetVisible(1);
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_0045;
            }
            battle.OnGimmickUpdate();
        Label_0045:
            return;
        }

        public override void OnStart(GameObject go)
        {
            TacticsUnitController controller;
            TacticsUnitController controller2;
            controller = go.GetComponentInParent<TacticsUnitController>();
            if (controller != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            controller2 = this.getTarget(controller);
            if (controller2 != null)
            {
                goto Label_0027;
            }
            return;
        Label_0027:
            controller2.SetVisible(0);
            return;
        }
    }
}

