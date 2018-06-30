namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("オブジェクト/移動", "シーン上のオブジェクトを移動させます。", 0x445555, 0x448888)]
    public class EventAction_MoveObject : EventAction
    {
        [StringIsActorID]
        public string TargetID;
        public ObjectAnimator.CurveType Curve;
        public Vector3 Position;
        public Vector3 Rotation;
        public float Time;
        public bool Async;
        private ObjectAnimator mAnimator;

        public EventAction_MoveObject()
        {
            this.Time = 1f;
            base..ctor();
            return;
        }

        public override void GoToEndState()
        {
            GameObject obj2;
            Quaternion quaternion;
            obj2 = EventAction.FindActor(this.TargetID);
            if ((obj2 == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            quaternion = Quaternion.Euler(this.Rotation);
            this.mAnimator = ObjectAnimator.Get(obj2);
            this.mAnimator.AnimateTo(this.Position, quaternion, -1f, this.Curve);
            return;
        }

        public override void OnActivate()
        {
            GameObject obj2;
            Quaternion quaternion;
            obj2 = EventAction.FindActor(this.TargetID);
            if ((obj2 == null) == null)
            {
                goto Label_001F;
            }
            base.ActivateNext();
            return;
        Label_001F:
            quaternion = Quaternion.Euler(this.Rotation);
            this.mAnimator = ObjectAnimator.Get(obj2);
            this.mAnimator.AnimateTo(this.Position, quaternion, this.Time, this.Curve);
            if (this.Async != null)
            {
                goto Label_0070;
            }
            if (this.Time > 0f)
            {
                goto Label_0077;
            }
        Label_0070:
            base.ActivateNext();
            return;
        Label_0077:
            return;
        }

        public override void Update()
        {
            if ((this.mAnimator != null) == null)
            {
                goto Label_0022;
            }
            if (this.mAnimator.isMoving == null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            base.ActivateNext();
            return;
        }
    }
}

