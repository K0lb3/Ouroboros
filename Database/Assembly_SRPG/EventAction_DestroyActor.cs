namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/アクター/削除", "指定のアクターを削除します。", 0x664444, 0xaa4444)]
    public class EventAction_DestroyActor : EventAction
    {
        [StringIsActorList]
        public string ActorID;

        public EventAction_DestroyActor()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            GameObject obj2;
            TacticsUnitController controller;
            obj2 = EventAction.FindActor(this.ActorID);
            if ((obj2 != null) == null)
            {
                goto Label_0036;
            }
            controller = obj2.GetComponent<TacticsUnitController>();
            if ((controller != null) == null)
            {
                goto Label_0036;
            }
            Object.Destroy(controller.get_gameObject());
        Label_0036:
            base.ActivateNext();
            return;
        }
    }
}

