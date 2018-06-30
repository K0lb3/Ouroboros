namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/オブジェクト/削除", "指定のオブジェクトを削除します。", 0x664444, 0xaa4444)]
    public class EventAction_DestroyObject : EventAction
    {
        [SerializeField, StringIsObjectList]
        public string TargetID;

        public EventAction_DestroyObject()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            GameObject obj2;
            obj2 = EventAction.FindActor(this.TargetID);
            if ((obj2 != null) == null)
            {
                goto Label_001E;
            }
            Object.Destroy(obj2);
        Label_001E:
            base.ActivateNext();
            return;
        }
    }
}

