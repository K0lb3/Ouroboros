namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/アクター/回転", "指定のオブジェクトを回転させます。", 0x555555, 0x444488)]
    public class EventAction_LookAt2 : EventAction
    {
        [StringIsActorList]
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
        public bool Async;
        public bool Rotate3D;
        private Quaternion mStartRotation;
        private Quaternion mEndRotation;
        private Transform mTarget;
        private Vector3 mLookAt;
        public bool m_MoveSnap;
        public bool Reverse;
        [Range(0f, 359f), HideInInspector]
        public float RotateX;
        [HideInInspector, Range(0f, 359f)]
        public float RotateY;
        [Range(0f, 359f), HideInInspector]
        public float RotateZ;
        private Vector3 mEulerStartRotate;
        private Vector3 mEulerEndRotate;
        private Vector3 mAddEulerAngle;
        private float mTime;

        public EventAction_LookAt2()
        {
            this.Time = 1f;
            this.Speed = 90f;
            this.m_MoveSnap = 1;
            this.Reverse = 1;
            base..ctor();
            return;
        }

        public override unsafe void OnActivate()
        {
            GameObject obj2;
            GameObject obj3;
            Vector3 vector;
            float num;
            goto Label_0413;
        Label_0005:
            obj2 = EventAction.FindActor(this.TargetID);
            if ((obj2 == null) == null)
            {
                goto Label_0022;
            }
            goto Label_0418;
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
            goto Label_0418;
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
            if (this.LookAt != 2)
            {
                goto Label_00E1;
            }
            this.mEndRotation = Quaternion.Euler(this.RotateX, this.RotateY, this.RotateZ);
        Label_00E1:
            this.mEulerStartRotate = &this.mStartRotation.get_eulerAngles();
            this.mEulerEndRotate = &this.mEndRotation.get_eulerAngles();
            if (this.Reverse != null)
            {
                goto Label_0280;
            }
            this.mAddEulerAngle = this.mEulerEndRotate - this.mEulerStartRotate;
            &this.mAddEulerAngle.x = ((Mathf.Abs(&this.mAddEulerAngle.x) > 180f) || (&this.mAddEulerAngle.x == 0f)) ? &this.mAddEulerAngle.x : (-Mathf.Sign(&this.mAddEulerAngle.x) * (360f - Mathf.Abs(&this.mAddEulerAngle.x)));
            &this.mAddEulerAngle.y = ((Mathf.Abs(&this.mAddEulerAngle.y) > 180f) || (&this.mAddEulerAngle.y == 0f)) ? &this.mAddEulerAngle.y : (-Mathf.Sign(&this.mAddEulerAngle.y) * (360f - Mathf.Abs(&this.mAddEulerAngle.y)));
            &this.mAddEulerAngle.z = ((Mathf.Abs(&this.mAddEulerAngle.z) > 180f) || (&this.mAddEulerAngle.z == 0f)) ? &this.mAddEulerAngle.z : (-Mathf.Sign(&this.mAddEulerAngle.z) * (360f - Mathf.Abs(&this.mAddEulerAngle.z)));
            goto Label_03AE;
        Label_0280:
            this.mAddEulerAngle = this.mEulerEndRotate - this.mEulerStartRotate;
            &this.mAddEulerAngle.x = (Mathf.Abs(&this.mAddEulerAngle.x) > 180f) ? (-Mathf.Sign(&this.mAddEulerAngle.x) * (360f - Mathf.Abs(&this.mAddEulerAngle.x))) : &this.mAddEulerAngle.x;
            &this.mAddEulerAngle.y = (Mathf.Abs(&this.mAddEulerAngle.y) > 180f) ? (-Mathf.Sign(&this.mAddEulerAngle.y) * (360f - Mathf.Abs(&this.mAddEulerAngle.y))) : &this.mAddEulerAngle.y;
            &this.mAddEulerAngle.z = (Mathf.Abs(&this.mAddEulerAngle.z) > 180f) ? (-Mathf.Sign(&this.mAddEulerAngle.z) * (360f - Mathf.Abs(&this.mAddEulerAngle.z))) : &this.mAddEulerAngle.z;
        Label_03AE:
            if (this.RotationMode != 1)
            {
                goto Label_03DA;
            }
            num = Quaternion.Angle(this.mStartRotation, this.mEndRotation);
            this.Time = num / this.Speed;
        Label_03DA:
            if (this.Time > 0f)
            {
                goto Label_0400;
            }
            this.mTarget.set_rotation(this.mEndRotation);
            goto Label_0418;
        Label_0400:
            if (this.Async == null)
            {
                goto Label_0412;
            }
            base.ActivateNext(1);
        Label_0412:
            return;
        Label_0413:
            goto Label_0005;
        Label_0418:
            base.ActivateNext();
            return;
        }

        public override void Update()
        {
            float num;
            Vector3 vector;
            Vector3 vector2;
            this.mTime += UnityEngine.Time.get_deltaTime();
            num = Mathf.Clamp01(this.mTime / this.Time);
            num = SRPG_Extensions.Evaluate(this.Curve, num);
            vector = this.mAddEulerAngle * num;
            vector2 = this.mEulerStartRotate + vector;
            this.mTarget.set_rotation(Quaternion.Euler(vector2));
            if (this.mTime < this.Time)
            {
                goto Label_0075;
            }
            base.ActivateNext();
            return;
        Label_0075:
            return;
        }

        public enum LookAtTypes
        {
            WorldPosition,
            GameObject,
            WorldRotate
        }

        public enum RotationModes
        {
            FixedTime,
            Speed
        }
    }
}

