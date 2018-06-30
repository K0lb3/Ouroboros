namespace SRPG
{
    using GR;
    using SRPG.AnimEvents;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class PreviewUnitController : UnitController
    {
        private Vector3 mStartPosition;
        public string UnitID;
        public string JobID;
        private List<string> mCameraAnimationNames;
        private Transform mCameraPos;
        private Transform mEnemyPos;
        private int mCameraID;
        private Quaternion mCameraShakeOffset;
        private float mCameraShakeSeedX;
        private float mCameraShakeSeedY;
        public string mCurrentAnimation;
        public List<string> mAnimationFiles;
        private AnimationClip mCameraAnimation;
        private bool mUseCamera;
        private Vector2 mAnimListScrollPos;
        private Vector2 mCameraAnimListScrollPos;
        private bool mLoadingAnimation;
        private bool mLoopAnimation;
        private bool mMirror;
        private float mSpeed;
        private bool mAnimationLoaded;
        private static readonly string SLOT0;

        static PreviewUnitController()
        {
            SLOT0 = "0";
            return;
        }

        public PreviewUnitController()
        {
            this.UnitID = "UN010000";
            this.JobID = "ninja";
            this.mCameraAnimationNames = new List<string>();
            this.mCurrentAnimation = string.Empty;
            this.mAnimationFiles = new List<string>();
            this.mSpeed = 1f;
            base..ctor();
            return;
        }

        private void Awake()
        {
            TextAsset asset;
            JSON_MasterParam param;
            UnitData data;
            param = JSONParser.parseJSONObject<JSON_MasterParam>(Resources.Load<TextAsset>("Data/MasterParam").get_text());
            MonoSingleton<GameManager>.Instance.Deserialize(param);
            data = new UnitData();
            data.Setup(this.UnitID, 0, 0, 0, this.JobID, 0, 0, 0);
            this.SetupUnit(data, -1);
            this.mStartPosition = base.get_transform().get_position();
            return;
        }

        private static void DrawAxis(Vector3 center)
        {
            Debug.DrawLine(center, center + Vector3.get_right(), Color.get_red());
            Debug.DrawLine(center, center + Vector3.get_up(), Color.get_green());
            Debug.DrawLine(center, center + Vector3.get_forward(), Color.get_blue());
            return;
        }

        private unsafe void LateUpdate()
        {
            Transform transform;
            Transform transform2;
            Vector3 vector;
            Quaternion quaternion;
            base.LateUpdate();
            if (this.mUseCamera == null)
            {
                goto Label_00FB;
            }
            transform = this.mCameraPos.FindChild((this.mCameraID != null) ? "Camera002" : "Camera001");
            transform2 = Camera.get_main().get_transform();
            vector = transform.get_position();
            quaternion = transform.get_rotation();
            if (this.mMirror == null)
            {
                goto Label_0079;
            }
            &quaternion.z = -&quaternion.z;
            &quaternion.y = -&quaternion.y;
        Label_0079:
            quaternion *= GameUtility.Yaw180;
            transform2.set_position(vector);
            transform2.set_rotation(this.mCameraShakeOffset * quaternion);
            Debug.DrawLine(vector, vector + (transform2.get_forward() * 5f), Color.get_yellow());
            DrawAxis(vector);
            Debug.DrawLine(transform.get_position(), transform.get_position() - (transform.get_forward() * 5f), Color.get_magenta());
            DrawAxis(transform.get_position());
        Label_00FB:
            return;
        }

        private void OnDrawGizmos()
        {
        }

        protected override void OnEvent(AnimEvent e, float time, float weight)
        {
            if ((e as CameraShake) == null)
            {
                goto Label_0029;
            }
            this.mCameraShakeOffset = (e as CameraShake).CalcOffset(time, this.mCameraShakeSeedX, this.mCameraShakeSeedY);
        Label_0029:
            return;
        }

        protected override void OnEventStart(AnimEvent e, float weight)
        {
            if ((e as ToggleCamera) == null)
            {
                goto Label_0053;
            }
            this.mCameraID = (e as ToggleCamera).CameraID;
            if (this.mCameraID < 0)
            {
                goto Label_0034;
            }
            if (this.mCameraID <= 1)
            {
                goto Label_0074;
            }
        Label_0034:
            Debug.LogError("Invalid CameraID: " + ((int) this.mCameraID));
            goto Label_0074;
        Label_0053:
            if ((e as CameraShake) == null)
            {
                goto Label_0074;
            }
            this.mCameraShakeSeedX = Random.get_value();
            this.mCameraShakeSeedY = Random.get_value();
        Label_0074:
            return;
        }

        private void OnGUI()
        {
        }

        protected override void PostSetup()
        {
        }

        private void Update()
        {
            AnimDef def;
            this.mCameraShakeOffset = Quaternion.get_identity();
            base.Update();
            if (this.mLoadingAnimation == null)
            {
                goto Label_016A;
            }
            if (this.IsLoading != null)
            {
                goto Label_016A;
            }
            this.mAnimationLoaded = 1;
            this.mLoadingAnimation = 0;
            base.get_transform().set_position(this.mStartPosition);
            this.mCameraID = 0;
            base.PlayAnimation(SLOT0, this.mLoopAnimation);
            def = base.GetAnimation(SLOT0);
            if ((this.mCameraAnimation != null) == null)
            {
                goto Label_00E9;
            }
            this.mCameraPos.GetComponent<Animation>().AddClip(this.mCameraAnimation, SLOT0);
            this.mCameraPos.GetComponent<Animation>().Play(SLOT0);
            this.mCameraPos.GetComponent<Animation>().get_Item(SLOT0).set_speed(((1f / this.mCameraAnimation.get_length()) * 1f) / def.Length);
            goto Label_0139;
        Label_00E9:
            this.mCameraPos.GetComponent<Animation>().AddClip(def.animation, SLOT0);
            this.mCameraPos.GetComponent<Animation>().Play(SLOT0);
            this.mCameraPos.GetComponent<Animation>().get_Item(SLOT0).set_speed(1f);
        Label_0139:
            this.mEnemyPos.GetComponent<Animation>().AddClip(def.animation, SLOT0);
            this.mEnemyPos.GetComponent<Animation>().Play(SLOT0);
        Label_016A:
            return;
        }
    }
}

