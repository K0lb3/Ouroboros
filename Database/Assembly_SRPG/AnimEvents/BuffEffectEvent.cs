namespace SRPG.AnimEvents
{
    using SRPG;
    using System;
    using UnityEngine;

    public class BuffEffectEvent : AnimEvent
    {
        public bool IsDispTarget;
        public bool IsDispSelf;

        public BuffEffectEvent()
        {
            this.IsDispTarget = 1;
            base..ctor();
            return;
        }

        public override void OnStart(GameObject go)
        {
            TacticsUnitController controller;
            controller = go.GetComponentInParent<TacticsUnitController>();
            if (controller != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (this.IsDispTarget == null)
            {
                goto Label_0024;
            }
            controller.BuffEffectTarget();
        Label_0024:
            if (this.IsDispSelf == null)
            {
                goto Label_0035;
            }
            controller.BuffEffectSelf();
        Label_0035:
            return;
        }
    }
}

