namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("カメラ/移動", "カメラを移動します。", 0x446644, 0x44aa44)]
    public class EventAction_MoveCamera : EventAction
    {
        private const float SmoothingMargin = 0.5f;
        public CameraMoveModes MoveMode;
        [HideInInspector]
        public string ActorID;
        [HideInInspector]
        public Vector3 StartTargetPosition;
        [HideInInspector]
        public float StartRotation;
        [HideInInspector]
        public float StartCameraDistance;
        [HideInInspector]
        public float StartCameraDistanceScale;
        [HideInInspector]
        public CameraMoveSpeeds Speed;
        [HideInInspector]
        public Vector3 TargetPosition;
        [HideInInspector]
        public float TargetRotation;
        private float mCurrentTime;
        private float mEndTime;
        [HideInInspector]
        public float CameraDistance;
        [HideInInspector]
        public float CameraDistanceScale;
        [HideInInspector]
        public float MoveTime;
        [HideInInspector]
        public bool Async;
        [HideInInspector]
        public bool SnapToGround;
        [HideInInspector]
        public CameraFadeModes FadeMode;
        [HideInInspector]
        public float FadeTime;

        public EventAction_MoveCamera()
        {
            this.StartCameraDistanceScale = 1f;
            this.CameraDistanceScale = 1f;
            this.MoveTime = -1f;
            base..ctor();
            return;
        }

        public override void GoToEndState()
        {
            GameSettings settings;
            TargetCamera camera;
            GameObject obj2;
            settings = GameSettings.Instance;
            if (this.Speed != null)
            {
                goto Label_003E;
            }
            this.TargetPosition += Vector3.get_up() * settings.GameCamera_UnitHeightOffset;
            this.UpdateCameraPosition(1f);
            return;
        Label_003E:
            camera = TargetCamera.Get(Camera.get_main());
            camera.Reset();
            if (this.MoveMode != null)
            {
                goto Label_008A;
            }
            this.StartTargetPosition = camera.TargetPosition;
            this.StartRotation = camera.Yaw;
            this.StartCameraDistanceScale = camera.CameraDistance / settings.GameCamera_DefaultDistance;
            goto Label_0149;
        Label_008A:
            if (this.MoveMode != 2)
            {
                goto Label_010C;
            }
            this.StartRotation = camera.Yaw;
            this.StartTargetPosition = camera.TargetPosition;
            this.StartCameraDistanceScale = camera.CameraDistance / settings.GameCamera_DefaultDistance;
            obj2 = EventAction.FindActor(this.ActorID);
            if ((obj2 == null) == null)
            {
                goto Label_00DA;
            }
            return;
        Label_00DA:
            this.TargetPosition = obj2.get_transform().get_position();
            if (this.TargetRotation >= 0f)
            {
                goto Label_0149;
            }
            this.TargetRotation = this.StartRotation;
            goto Label_0149;
        Label_010C:
            if (this.SnapToGround == null)
            {
                goto Label_0128;
            }
            this.StartTargetPosition = GameUtility.RaycastGround(this.StartTargetPosition);
        Label_0128:
            this.StartTargetPosition += Vector3.get_up() * settings.GameCamera_UnitHeightOffset;
        Label_0149:
            if (this.SnapToGround == null)
            {
                goto Label_0165;
            }
            this.TargetPosition = GameUtility.RaycastGround(this.TargetPosition);
        Label_0165:
            this.TargetPosition += Vector3.get_up() * settings.GameCamera_UnitHeightOffset;
            this.UpdateCameraPosition(2f);
            return;
        }

        public override unsafe void OnActivate()
        {
            GameSettings settings;
            TargetCamera camera;
            GameObject obj2;
            float num;
            float num2;
            CameraMoveSpeeds speeds;
            Vector3 vector;
            settings = GameSettings.Instance;
            if (this.MoveTime > 0f)
            {
                goto Label_0054;
            }
            if (this.Speed != null)
            {
                goto Label_0054;
            }
            this.TargetPosition += Vector3.get_up() * settings.GameCamera_UnitHeightOffset;
            this.UpdateCameraPosition(1f);
            base.ActivateNext();
            return;
        Label_0054:
            camera = TargetCamera.Get(Camera.get_main());
            camera.Reset();
            if (this.MoveMode != null)
            {
                goto Label_00A4;
            }
            this.StartTargetPosition = camera.TargetPosition;
            this.StartRotation = camera.Yaw;
            this.StartCameraDistanceScale = camera.CameraDistance / GameSettings.Instance.GameCamera_DefaultDistance;
            goto Label_016D;
        Label_00A4:
            if (this.MoveMode != 2)
            {
                goto Label_0130;
            }
            this.StartRotation = camera.Yaw;
            this.StartTargetPosition = camera.TargetPosition;
            this.StartCameraDistanceScale = camera.CameraDistance / GameSettings.Instance.GameCamera_DefaultDistance;
            obj2 = EventAction.FindActor(this.ActorID);
            if ((obj2 == null) == null)
            {
                goto Label_00FE;
            }
            base.ActivateNext();
            return;
        Label_00FE:
            this.TargetPosition = obj2.get_transform().get_position();
            if (this.TargetRotation >= 0f)
            {
                goto Label_016D;
            }
            this.TargetRotation = this.StartRotation;
            goto Label_016D;
        Label_0130:
            if (this.SnapToGround == null)
            {
                goto Label_014C;
            }
            this.StartTargetPosition = GameUtility.RaycastGround(this.StartTargetPosition);
        Label_014C:
            this.StartTargetPosition += Vector3.get_up() * settings.GameCamera_UnitHeightOffset;
        Label_016D:
            if (this.SnapToGround == null)
            {
                goto Label_0189;
            }
            this.TargetPosition = GameUtility.RaycastGround(this.TargetPosition);
        Label_0189:
            this.TargetPosition += Vector3.get_up() * settings.GameCamera_UnitHeightOffset;
            num = 1f;
            switch ((this.Speed - 1))
            {
                case 0:
                    goto Label_01D2;

                case 1:
                    goto Label_01E8;

                case 2:
                    goto Label_01DD;
            }
            goto Label_01F3;
        Label_01D2:
            num = 2f;
            goto Label_01F3;
        Label_01DD:
            num = 0.5f;
            goto Label_01F3;
        Label_01E8:
            num = 8f;
        Label_01F3:
            this.mCurrentTime = 0f;
            if (this.MoveTime > 0f)
            {
                goto Label_026F;
            }
            vector = this.TargetPosition - this.StartTargetPosition;
            num2 = &vector.get_magnitude();
            if (num2 > 0f)
            {
                goto Label_0260;
            }
            if (this.StartCameraDistanceScale != this.CameraDistanceScale)
            {
                goto Label_0259;
            }
            this.UpdateCameraPosition(1f);
            base.ActivateNext();
            return;
        Label_0259:
            num2 = 1f;
        Label_0260:
            this.mEndTime = num2 / num;
            goto Label_027B;
        Label_026F:
            this.mEndTime = this.MoveTime;
        Label_027B:
            if (this.Async == null)
            {
                goto Label_028D;
            }
            base.ActivateNext(1);
        Label_028D:
            return;
        }

        public override void Update()
        {
            float num;
            this.mCurrentTime += Time.get_deltaTime();
            if (this.mEndTime <= 0f)
            {
                goto Label_003A;
            }
            num = Mathf.Clamp01(this.mCurrentTime / this.mEndTime);
            goto Label_0040;
        Label_003A:
            num = 1f;
        Label_0040:
            if (this.FadeMode == 1)
            {
                goto Label_0058;
            }
            if (this.FadeMode != 3)
            {
                goto Label_00A0;
            }
        Label_0058:
            if (this.mCurrentTime >= this.FadeTime)
            {
                goto Label_00A0;
            }
            FadeController.Instance.FadeTo(new Color(0f, 0f, 0f, 1f - (this.mCurrentTime / this.FadeTime)), 0f, 0);
        Label_00A0:
            if (this.FadeMode == 2)
            {
                goto Label_00B8;
            }
            if (this.FadeMode != 3)
            {
                goto Label_010F;
            }
        Label_00B8:
            if (this.mCurrentTime < (this.mEndTime - this.FadeTime))
            {
                goto Label_010F;
            }
            FadeController.Instance.FadeTo(new Color(0f, 0f, 0f, (this.mCurrentTime - (this.mEndTime - this.FadeTime)) / this.FadeTime), 0f, 0);
        Label_010F:
            this.UpdateCameraPosition(num);
            if (num < 1f)
            {
                goto Label_013F;
            }
            if (this.Async != null)
            {
                goto Label_0137;
            }
            base.ActivateNext();
            goto Label_013E;
        Label_0137:
            base.enabled = 0;
        Label_013E:
            return;
        Label_013F:
            return;
        }

        private void UpdateCameraPosition(float t)
        {
            TargetCamera camera;
            GameSettings settings;
            camera = TargetCamera.Get(Camera.get_main());
            settings = GameSettings.Instance;
            camera.Pitch = -45f;
            if (t < 1f)
            {
                goto Label_0051;
            }
            camera.SetPositionYaw(this.TargetPosition, this.TargetRotation);
            camera.CameraDistance = settings.GameCamera_DefaultDistance * this.CameraDistanceScale;
            goto Label_009A;
        Label_0051:
            camera.SetPositionYaw(Vector3.Lerp(this.StartTargetPosition, this.TargetPosition, t), Mathf.Lerp(this.StartRotation, this.TargetRotation, t));
            camera.CameraDistance = settings.GameCamera_DefaultDistance * Mathf.Lerp(this.StartCameraDistanceScale, this.CameraDistanceScale, t);
        Label_009A:
            Camera.get_main().set_fieldOfView(settings.GameCamera_TacticsSceneFOV);
            return;
        }

        public enum CameraFadeModes
        {
            KeepAsIs,
            FadeIn,
            FadeOut,
            FadeInOut
        }

        public enum CameraMoveModes
        {
            InterpolateToPoint,
            StartEnd,
            SpecificActor
        }

        public enum CameraMoveSpeeds
        {
            Immediate,
            Normal,
            Fast,
            Slow
        }
    }
}

