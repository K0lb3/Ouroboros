namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/アクター/頭を回転", "指定のオブジェクトの頭を回転させます。", 0x555555, 0x444488)]
    public class EventAction_RotateHead : EventAction
    {
        private static readonly string HeadBoneName;
        private static readonly string NeckBoneName;
        private static readonly string Spine1BoneName;
        private static readonly float NeckRotateInHead;
        private static readonly float Spine1RotateInHead;
        [StringIsActorList]
        public string TargetID;
        [Range(-40f, 40f)]
        public float RotateY;
        public RotationModes RotationMode;
        public ObjectAnimator.CurveType Curve;
        [HideInInspector]
        public float Time;
        [HideInInspector]
        public float Speed;
        public bool Async;
        private Transform mTargetHead;
        private Transform mTargetNeck;
        private Transform mTargetSpine1;
        private Quaternion mStartRotationHead;
        private Quaternion mEndRotationHead;
        private Quaternion mStartRotationNeck;
        private Quaternion mEndRotationNeck;
        private Quaternion mStartRotationSpine1;
        private Quaternion mEndRotationSpine1;
        private float mTime;

        static EventAction_RotateHead()
        {
            HeadBoneName = "Bip001 Head";
            NeckBoneName = "Bip001 Neck";
            Spine1BoneName = "Bip001 Spine1";
            NeckRotateInHead = 0.25f;
            Spine1RotateInHead = 0.25f;
            return;
        }

        public EventAction_RotateHead()
        {
            this.Time = 1f;
            this.Speed = 90f;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            GameObject obj2;
            float num;
            obj2 = EventAction.FindActor(this.TargetID);
            if ((obj2 != null) == null)
            {
                goto Label_0120;
            }
            this.mTargetHead = GameUtility.findChildRecursively(obj2.get_transform(), HeadBoneName);
            this.mTargetNeck = GameUtility.findChildRecursively(obj2.get_transform(), NeckBoneName);
            this.mTargetSpine1 = GameUtility.findChildRecursively(obj2.get_transform(), Spine1BoneName);
            if ((this.mTargetHead != null) == null)
            {
                goto Label_007C;
            }
            this.mStartRotationHead = this.mTargetHead.get_localRotation();
        Label_007C:
            if ((this.mTargetNeck != null) == null)
            {
                goto Label_009E;
            }
            this.mStartRotationNeck = this.mTargetNeck.get_localRotation();
        Label_009E:
            if ((this.mTargetSpine1 != null) == null)
            {
                goto Label_00C0;
            }
            this.mStartRotationSpine1 = this.mTargetSpine1.get_localRotation();
        Label_00C0:
            this.mEndRotationHead = Quaternion.Euler(-this.RotateY, 0f, 0f);
            this.mEndRotationNeck = Quaternion.Euler(-this.RotateY * NeckRotateInHead, 0f, 0f);
            this.mEndRotationSpine1 = Quaternion.Euler(-this.RotateY * Spine1RotateInHead, 0f, 0f);
        Label_0120:
            if (this.RotationMode != 1)
            {
                goto Label_014C;
            }
            num = Quaternion.Angle(this.mStartRotationHead, this.mEndRotationHead);
            this.Time = num / this.Speed;
        Label_014C:
            if (this.Async == null)
            {
                goto Label_015E;
            }
            base.ActivateNext(1);
        Label_015E:
            return;
        }

        public override void Update()
        {
            float num;
            this.mTime += UnityEngine.Time.get_deltaTime();
            num = Mathf.Clamp01(this.mTime / this.Time);
            num = SRPG_Extensions.Evaluate(this.Curve, num);
            if ((this.mTargetHead != null) == null)
            {
                goto Label_0060;
            }
            this.mTargetHead.set_localRotation(Quaternion.Slerp(this.mStartRotationHead, this.mEndRotationHead, num));
        Label_0060:
            if ((this.mTargetNeck != null) == null)
            {
                goto Label_008E;
            }
            this.mTargetNeck.set_localRotation(Quaternion.Slerp(this.mStartRotationNeck, this.mEndRotationNeck, num));
        Label_008E:
            if ((this.mTargetSpine1 != null) == null)
            {
                goto Label_00BC;
            }
            this.mTargetSpine1.set_localRotation(Quaternion.Slerp(this.mStartRotationSpine1, this.mEndRotationSpine1, num));
        Label_00BC:
            if (this.mTime < this.Time)
            {
                goto Label_00EA;
            }
            if (this.Async == null)
            {
                goto Label_00E4;
            }
            base.enabled = 0;
            goto Label_00EA;
        Label_00E4:
            base.ActivateNext();
        Label_00EA:
            return;
        }

        public enum RotationModes
        {
            FixedTime,
            Speed
        }
    }
}

