namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/アクター/移動", "アクターを指定パスに沿って移動させます。", 0x664444, 0xaa4444)]
    public class EventAction_MoveActor2 : EventAction
    {
        [StringIsActorList]
        public string ActorID;
        [SerializeField, HideInInspector]
        private IntVector2[] mPoints;
        [SerializeField]
        public Vector3[] m_WayPoints;
        public float Delay;
        public bool Async;
        public bool GotoRealPosition;
        public bool m_StartActorPos;
        protected TacticsUnitController mController;
        [HideInInspector]
        public float Angle;
        [Tooltip("マス目にスナップするか？")]
        public bool MoveSnap;
        [Tooltip("地面にスナップするか？")]
        public bool GroundSnap;
        [Tooltip("移動速度")]
        public float RunSpeed;
        protected float BackupRunSpeed;
        [Tooltip("移動する時に向きを固定化するか")]
        public bool LockRotation;
        [Tooltip("移動する時にモーションを固定化するか")]
        public bool LockMotion;
        private int mMoveIndex;
        protected bool mMoving;
        protected bool mFinishMove;
        protected bool mActorCollideGround;
        protected bool mReady;

        public EventAction_MoveActor2()
        {
            this.mPoints = new IntVector2[1];
            this.m_WayPoints = new Vector3[1];
            this.Angle = -1f;
            this.MoveSnap = 1;
            this.GroundSnap = 1;
            this.RunSpeed = 4f;
            this.BackupRunSpeed = 4f;
            base..ctor();
            return;
        }

        private TacticsUnitController GetController()
        {
            TacticsUnitController controller;
            controller = TacticsUnitController.FindByUniqueName(this.ActorID);
            if ((controller == null) == null)
            {
                goto Label_0024;
            }
            controller = TacticsUnitController.FindByUnitID(this.ActorID);
        Label_0024:
            return controller;
        }

        public override void OnActivate()
        {
            this.mController = this.GetController();
            if ((this.mController == null) != null)
            {
                goto Label_002A;
            }
            if (((int) this.mPoints.Length) != null)
            {
                goto Label_0031;
            }
        Label_002A:
            base.ActivateNext();
            return;
        Label_0031:
            this.mController.SetRunningSpeed(this.RunSpeed);
            this.mReady = 0;
            this.mActorCollideGround = this.mController.CollideGround;
            this.mController.CollideGround = this.GroundSnap;
            return;
        }

        protected unsafe void StartMove()
        {
            Vector3[] vectorArray;
            int num;
            Transform transform;
            if (this.GotoRealPosition == null)
            {
                goto Label_008C;
            }
            if ((this.mController != null) == null)
            {
                goto Label_008C;
            }
            if (this.mController.Unit == null)
            {
                goto Label_008C;
            }
            Array.Resize<Vector3>(&this.m_WayPoints, ((int) this.m_WayPoints.Length) + 1);
            *(&(this.m_WayPoints[((int) this.m_WayPoints.Length) - 1])) = new Vector3((float) this.mController.Unit.x, 0f, (float) this.mController.Unit.y);
            goto Label_0093;
        Label_008C:
            this.GotoRealPosition = 0;
        Label_0093:
            vectorArray = new Vector3[(int) this.m_WayPoints.Length];
            num = 0;
            goto Label_00C9;
        Label_00A8:
            *(&(vectorArray[num])) = *(&(this.m_WayPoints[num]));
            num += 1;
        Label_00C9:
            if (num < ((int) this.m_WayPoints.Length))
            {
                goto Label_00A8;
            }
            if (this.m_StartActorPos == null)
            {
                goto Label_0111;
            }
            if ((this.mController != null) == null)
            {
                goto Label_0111;
            }
            transform = this.mController.get_transform();
            *(&(vectorArray[0])) = transform.get_position();
        Label_0111:
            this.mMoveIndex = 0;
            if (this.LockRotation != null)
            {
                goto Label_014D;
            }
            if (this.LockMotion != null)
            {
                goto Label_014D;
            }
            if (this.GroundSnap == null)
            {
                goto Label_014D;
            }
            this.mController.StartMove(vectorArray, this.Angle);
            return;
        Label_014D:
            if (this.LockMotion != null)
            {
                goto Label_0163;
            }
            this.mController.StartRunning();
        Label_0163:
            return;
        }

        public override void Update()
        {
            if (this.mReady != null)
            {
                goto Label_0046;
            }
            if ((this.mController != null) == null)
            {
                goto Label_002D;
            }
            if (this.mController.IsLoading == null)
            {
                goto Label_002D;
            }
            return;
        Label_002D:
            if (this.Async == null)
            {
                goto Label_003F;
            }
            base.ActivateNext(1);
        Label_003F:
            this.mReady = 1;
        Label_0046:
            if (this.mMoving != null)
            {
                goto Label_0093;
            }
            if (this.mController.IsLoading == null)
            {
                goto Label_0062;
            }
            return;
        Label_0062:
            if (this.Delay <= 0f)
            {
                goto Label_0085;
            }
            this.Delay -= Time.get_deltaTime();
            return;
        Label_0085:
            this.StartMove();
            this.mMoving = 1;
            return;
        Label_0093:
            if (this.LockRotation != null)
            {
                goto Label_00B4;
            }
            if (this.LockMotion != null)
            {
                goto Label_00B4;
            }
            if (this.GroundSnap != null)
            {
                goto Label_00C5;
            }
        Label_00B4:
            if (this.UpdateMove() == null)
            {
                goto Label_00D6;
            }
            return;
            goto Label_00D6;
        Label_00C5:
            if (this.mController.isMoving == null)
            {
                goto Label_00D6;
            }
            return;
        Label_00D6:
            if (this.GotoRealPosition == null)
            {
                goto Label_00ED;
            }
            this.mController.AutoUpdateRotation = 1;
        Label_00ED:
            if (this.Async != null)
            {
                goto Label_0125;
            }
            base.ActivateNext();
            this.mController.SetRunningSpeed(this.BackupRunSpeed);
            this.mController.CollideGround = this.mActorCollideGround;
            goto Label_014E;
        Label_0125:
            base.enabled = 0;
            this.mController.SetRunningSpeed(this.BackupRunSpeed);
            this.mController.CollideGround = this.mActorCollideGround;
        Label_014E:
            return;
        }

        protected unsafe bool UpdateMove()
        {
            Vector3 vector;
            Vector3 vector2;
            bool flag;
            Vector3 vector3;
            float num;
            float num2;
            float num3;
            Vector3 vector4;
            float num4;
            float num5;
            Vector3 vector5;
            float num6;
            int num7;
            flag = 1;
            if (this.mController != null)
            {
                goto Label_0014;
            }
            return 0;
        Label_0014:
            if (((int) this.m_WayPoints.Length) <= this.mMoveIndex)
            {
                goto Label_02A1;
            }
            vector = this.mController.get_transform().get_position();
            vector2 = *(&(this.m_WayPoints[this.mMoveIndex]));
            vector3 = vector2 - vector;
            if (&vector3.get_sqrMagnitude() >= 0.0001f)
            {
                goto Label_00A9;
            }
            if ((this.mMoveIndex += 1) < ((int) this.m_WayPoints.Length))
            {
                goto Label_02A1;
            }
            if (this.LockMotion != null)
            {
                goto Label_00A2;
            }
            this.mController.StopRunning();
        Label_00A2:
            flag = 0;
            goto Label_02A1;
        Label_00A9:
            &vector3.Normalize();
            vector2 = vector + ((vector3 * this.RunSpeed) * Time.get_deltaTime());
            this.mController.get_transform().set_position(vector2);
            if (this.LockRotation != null)
            {
                goto Label_02A1;
            }
            num3 = GameUtility.CalcDistance2D(vector2, *(&(this.m_WayPoints[this.mMoveIndex])));
            if (num3 >= 0.5f)
            {
                goto Label_01AF;
            }
            if (this.mMoveIndex >= (((int) this.m_WayPoints.Length) - 1))
            {
                goto Label_01AF;
            }
            vector4 = *(&(this.m_WayPoints[this.mMoveIndex + 1])) - *(&(this.m_WayPoints[this.mMoveIndex]));
            &vector4.y = 0f;
            &vector4.Normalize();
            num4 = (1f - (num3 / 0.5f)) * 0.5f;
            this.mController.get_transform().set_rotation(Quaternion.Slerp(Quaternion.LookRotation(vector3), Quaternion.LookRotation(vector4), num4));
            goto Label_02A1;
        Label_01AF:
            if (this.mMoveIndex <= 1)
            {
                goto Label_028B;
            }
            num5 = GameUtility.CalcDistance2D(vector2, *(&(this.m_WayPoints[this.mMoveIndex - 1])));
            if (num5 >= 0.5f)
            {
                goto Label_0270;
            }
            vector5 = *(&(this.m_WayPoints[this.mMoveIndex - 1])) - *(&(this.m_WayPoints[this.mMoveIndex - 2]));
            &vector5.y = 0f;
            &vector5.Normalize();
            num6 = 0.5f + ((num5 / 0.5f) * 0.5f);
            this.mController.get_transform().set_rotation(Quaternion.Slerp(Quaternion.LookRotation(vector5), Quaternion.LookRotation(vector3), num6));
            goto Label_0286;
        Label_0270:
            this.mController.get_transform().set_rotation(Quaternion.LookRotation(vector3));
        Label_0286:
            goto Label_02A1;
        Label_028B:
            this.mController.get_transform().set_rotation(Quaternion.LookRotation(vector3));
        Label_02A1:
            return flag;
        }
    }
}

