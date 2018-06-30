namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("アクター/回転", "指定のオブジェクトを回転させます。", 0x555555, 0x444488)]
    public class EventAction_LookAt : EventAction
    {
        [StringIsActorID]
        public string TargetID;
        public LookAtTypes LookAt;
        [HideInInspector, StringIsActorID]
        public string LookAtID;
        [HideInInspector]
        public Vector3 LookAtPosition;
        public RotationModes RotationMode;
        public ObjectAnimator.CurveType Curve;
        [HideInInspector]
        public float Time;
        [HideInInspector]
        public float Speed;
        public bool Rotate3D;
        private Quaternion mStartRotation;
        private Quaternion mEndRotation;
        private Transform mTarget;
        private Vector3 mLookAt;
        private float mTime;

        public EventAction_LookAt()
        {
            this.Time = 1f;
            this.Speed = 90f;
            base..ctor();
            return;
        }

        public override unsafe void OnActivate()
        {
            GameObject obj2;
            GameObject obj3;
            Vector3 vector;
            float num;
            goto Label_010B;
        Label_0005:
            obj2 = EventAction.FindActor(this.TargetID);
            if ((obj2 == null) == null)
            {
                goto Label_0022;
            }
            goto Label_0110;
        Label_0022:
            this.mTarget = obj2.get_transform();
            if (this.LookAt != 1)
            {
                goto Label_0068;
            }
            obj3 = EventAction.FindActor(this.LookAtID);
            if ((obj3 == null) == null)
            {
                goto Label_0057;
            }
            goto Label_0110;
        Label_0057:
            this.LookAtPosition = obj3.get_transform().get_position();
        Label_0068:
            vector = this.LookAtPosition - this.mTarget.get_transform().get_position();
            if (this.Rotate3D != null)
            {
                goto Label_009B;
            }
            &vector.y = 0f;
        Label_009B:
            this.mStartRotation = this.mTarget.get_rotation();
            this.mEndRotation = Quaternion.LookRotation(vector);
            if (this.RotationMode != 1)
            {
                goto Label_00E4;
            }
            num = Quaternion.Angle(this.mStartRotation, this.mEndRotation);
            this.Time = num / this.Speed;
        Label_00E4:
            if (this.Time > 0f)
            {
                goto Label_010A;
            }
            this.mTarget.set_rotation(this.mEndRotation);
            goto Label_0110;
        Label_010A:
            return;
        Label_010B:
            goto Label_0005;
        Label_0110:
            base.ActivateNext();
            return;
        }

        public override void Update()
        {
            float num;
            this.mTime += UnityEngine.Time.get_deltaTime();
            num = Mathf.Clamp01(this.mTime / this.Time);
            num = SRPG_Extensions.Evaluate(this.Curve, num);
            this.mTarget.set_rotation(Quaternion.Slerp(this.mStartRotation, this.mEndRotation, num));
            if (this.mTime < this.Time)
            {
                goto Label_0067;
            }
            base.ActivateNext();
            return;
        Label_0067:
            return;
        }

        public enum LookAtTypes
        {
            WorldPosition,
            GameObject
        }

        public enum RotationModes
        {
            FixedTime,
            Speed
        }
    }
}

