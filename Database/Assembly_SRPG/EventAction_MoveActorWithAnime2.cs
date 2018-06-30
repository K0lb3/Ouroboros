namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/アクター/移動2(アニメーション再生付)", "アクターを指定パスに沿って移動させます。", 0x664444, 0xaa4444)]
    public class EventAction_MoveActorWithAnime2 : EventAction
    {
        private const string MOVIE_PATH = "Movies/";
        private const string DEMO_PATH = "Demo/";
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
        [HideInInspector]
        public float Angle;
        [Tooltip("マス目にスナップするか？")]
        public bool MoveSnap;
        [Tooltip("地面にスナップするか？")]
        public bool GroundSnap;
        [Tooltip("移動する時にモーションを固定化するか")]
        public bool LockMotion;
        private bool FoldoutLockRotation;
        [HideInInspector]
        public bool LockRotationX;
        [HideInInspector]
        public bool LockRotationY;
        [HideInInspector]
        public bool LockRotationZ;
        [HideInInspector]
        public RunTypes RunType;
        [HideInInspector, Tooltip("移動速度")]
        public float RunSpeed;
        private float BackupRunSpeed;
        [Tooltip("移動時間"), HideInInspector]
        public float RunTime;
        [HideInInspector]
        public string m_AnimationName;
        [HideInInspector]
        public bool m_Loop;
        [HideInInspector]
        public AnimeType m_AnimeType;
        private string m_AnimationID;
        [HideInInspector]
        public PREFIX_PATH Path;
        private bool FoldoutAnimation;
        private int mMoveIndex;
        private float mTime;
        private TacticsUnitController mController;
        private bool mReady;
        private bool mMoving;
        private bool mActorCollideGround;
        private Vector3 mActorRotation;
        private List<float> distanceList;
        private List<float> timeAtPointList;

        public EventAction_MoveActorWithAnime2()
        {
            this.mPoints = new IntVector2[1];
            this.m_WayPoints = new Vector3[1];
            this.Angle = -1f;
            this.MoveSnap = 1;
            this.GroundSnap = 1;
            this.RunSpeed = 4f;
            this.BackupRunSpeed = 4f;
            this.RunTime = 4f;
            this.Path = 2;
            this.distanceList = new List<float>();
            this.timeAtPointList = new List<float>();
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

        public override unsafe void OnActivate()
        {
            Quaternion quaternion;
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
            this.mReady = 0;
            this.mActorCollideGround = this.mController.CollideGround;
            this.mController.CollideGround = this.GroundSnap;
            this.mActorRotation = &this.mController.get_transform().get_rotation().get_eulerAngles();
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator8C iteratorc;
            iteratorc = new <PreloadAssets>c__Iterator8C();
            iteratorc.<>f__this = this;
            return iteratorc;
        }

        protected unsafe void StartMove()
        {
            int num;
            float num2;
            float num3;
            int num4;
            float num5;
            float num6;
            int num7;
            float num8;
            if (((this.GotoRealPosition == null) || ((this.mController != null) == null)) || (this.mController.Unit == null))
            {
                goto Label_008C;
            }
            Array.Resize<Vector3>(&this.m_WayPoints, ((int) this.m_WayPoints.Length) + 1);
            *(&(this.m_WayPoints[((int) this.m_WayPoints.Length) - 1])) = new Vector3((float) this.mController.Unit.x, 0f, (float) this.mController.Unit.y);
            goto Label_0093;
        Label_008C:
            this.GotoRealPosition = 0;
        Label_0093:
            if (((int) this.m_WayPoints.Length) != 1)
            {
                goto Label_00B2;
            }
            this.timeAtPointList.Add(0f);
            return;
        Label_00B2:
            num = 0;
            goto Label_012D;
        Label_00B9:
            if (this.GroundSnap == null)
            {
                goto Label_00F3;
            }
            num2 = GameUtility.CalcDistance2D(*(&(this.m_WayPoints[num])), *(&(this.m_WayPoints[num + 1])));
            goto Label_011D;
        Label_00F3:
            num2 = Vector3.Distance(*(&(this.m_WayPoints[num])), *(&(this.m_WayPoints[num + 1])));
        Label_011D:
            this.distanceList.Add(num2);
            num += 1;
        Label_012D:
            if (num < (((int) this.m_WayPoints.Length) - 1))
            {
                goto Label_00B9;
            }
            num3 = 0f;
            num4 = 0;
            goto Label_015D;
        Label_014A:
            num3 += this.distanceList[num4];
            num4 += 1;
        Label_015D:
            if (num4 < this.distanceList.Count)
            {
                goto Label_014A;
            }
            num5 = (this.RunType != null) ? (num3 / this.RunSpeed) : this.RunTime;
            num6 = 0f;
            num7 = 0;
            goto Label_01D5;
        Label_019D:
            if (num7 <= 0)
            {
                goto Label_01B9;
            }
            num6 += this.distanceList[num7 - 1];
        Label_01B9:
            num8 = (num6 / num3) * num5;
            this.timeAtPointList.Add(num8);
            num7 += 1;
        Label_01D5:
            if (num7 < ((int) this.m_WayPoints.Length))
            {
                goto Label_019D;
            }
            if (this.LockMotion != null)
            {
                goto Label_01FA;
            }
            this.mController.StartRunning();
        Label_01FA:
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
            if (this.UpdateMove() == null)
            {
                goto Label_009F;
            }
            return;
        Label_009F:
            if (this.GotoRealPosition == null)
            {
                goto Label_00B6;
            }
            this.mController.AutoUpdateRotation = 1;
        Label_00B6:
            if (this.m_AnimeType != null)
            {
                goto Label_0103;
            }
            if (string.IsNullOrEmpty(this.m_AnimationName) != null)
            {
                goto Label_0103;
            }
            this.mController.RootMotionMode = 1;
            this.mController.PlayAnimation(this.m_AnimationID, this.m_Loop, 0.1f, 0f);
            goto Label_011F;
        Label_0103:
            if (this.m_AnimeType != 1)
            {
                goto Label_011F;
            }
            this.mController.PlayIdle(0f);
        Label_011F:
            if (this.Async != null)
            {
                goto Label_0157;
            }
            base.ActivateNext();
            this.mController.SetRunningSpeed(this.BackupRunSpeed);
            this.mController.CollideGround = this.mActorCollideGround;
            goto Label_0180;
        Label_0157:
            base.enabled = 0;
            this.mController.SetRunningSpeed(this.BackupRunSpeed);
            this.mController.CollideGround = this.mActorCollideGround;
        Label_0180:
            return;
        }

        private unsafe bool UpdateMove()
        {
            Vector3 vector;
            bool flag;
            int num;
            Vector3 vector2;
            float num2;
            Quaternion quaternion;
            float num3;
            Vector3 vector3;
            float num4;
            Vector3 vector4;
            float num5;
            float num6;
            Vector3 vector5;
            float num7;
            float num8;
            Vector3 vector6;
            float num9;
            float num10;
            Vector3 vector7;
            flag = 1;
            if (this.mController != null)
            {
                goto Label_0014;
            }
            return 0;
        Label_0014:
            if (this.mTime < this.timeAtPointList[this.timeAtPointList.Count - 1])
            {
                goto Label_006F;
            }
            vector = *(&(this.m_WayPoints[((int) this.m_WayPoints.Length) - 1]));
            if (this.LockMotion != null)
            {
                goto Label_0068;
            }
            this.mController.StopRunning();
        Label_0068:
            flag = 0;
            goto Label_014C;
        Label_006F:
            num = this.mMoveIndex;
            goto Label_00A2;
        Label_007B:
            if (this.mTime >= this.timeAtPointList[num])
            {
                goto Label_009E;
            }
            this.mMoveIndex = num;
            goto Label_00B0;
        Label_009E:
            num += 1;
        Label_00A2:
            if (num < ((int) this.m_WayPoints.Length))
            {
                goto Label_007B;
            }
        Label_00B0:
            vector2 = *(&(this.m_WayPoints[this.mMoveIndex])) - *(&(this.m_WayPoints[this.mMoveIndex - 1]));
            num2 = (this.mTime - this.timeAtPointList[this.mMoveIndex - 1]) / (this.timeAtPointList[this.mMoveIndex] - this.timeAtPointList[this.mMoveIndex - 1]);
            vector = (vector2 * num2) + *(&(this.m_WayPoints[this.mMoveIndex - 1]));
        Label_014C:
            this.mController.get_transform().set_position(vector);
            if (this.LockRotationX == null)
            {
                goto Label_017E;
            }
            if (this.LockRotationY == null)
            {
                goto Label_017E;
            }
            if (this.LockRotationZ != null)
            {
                goto Label_04BE;
            }
        Label_017E:
            quaternion = new Quaternion();
            if (((int) this.m_WayPoints.Length) != 1)
            {
                goto Label_01D7;
            }
            if (this.Angle <= 0f)
            {
                goto Label_01C5;
            }
            quaternion = Quaternion.Euler(new Vector3(0f, this.Angle, 0f));
            goto Label_01D2;
        Label_01C5:
            quaternion = Quaternion.Euler(this.mActorRotation);
        Label_01D2:
            goto Label_0422;
        Label_01D7:
            vector3 = *(&(this.m_WayPoints[this.mMoveIndex])) - *(&(this.m_WayPoints[this.mMoveIndex - 1]));
            num4 = this.timeAtPointList[this.mMoveIndex] - this.mTime;
            if (num4 >= 0.1f)
            {
                goto Label_0339;
            }
            if (this.mMoveIndex >= (((int) this.m_WayPoints.Length) - 1))
            {
                goto Label_02AE;
            }
            vector4 = *(&(this.m_WayPoints[this.mMoveIndex + 1])) - *(&(this.m_WayPoints[this.mMoveIndex]));
            num5 = (1f - (num4 / 0.1f)) * 0.5f;
            quaternion = Quaternion.Slerp(Quaternion.LookRotation(vector3), Quaternion.LookRotation(vector4), num5);
            goto Label_0334;
        Label_02AE:
            if (this.Angle < 0f)
            {
                goto Label_02FD;
            }
            num6 = 1f - (num4 / 0.1f);
            quaternion = Quaternion.Slerp(Quaternion.LookRotation(vector3), Quaternion.Euler(new Vector3(0f, this.Angle, 0f)), num6);
            goto Label_0334;
        Label_02FD:
            vector5 = vector3;
            &vector5.y = 0f;
            num7 = 1f - (num4 / 0.1f);
            quaternion = Quaternion.Slerp(Quaternion.LookRotation(vector3), Quaternion.LookRotation(vector5), num7);
        Label_0334:
            goto Label_0422;
        Label_0339:
            num8 = this.mTime - this.timeAtPointList[this.mMoveIndex - 1];
            if (num8 >= 0.1f)
            {
                goto Label_0419;
            }
            if (this.mMoveIndex <= 1)
            {
                goto Label_03D6;
            }
            vector6 = *(&(this.m_WayPoints[this.mMoveIndex - 1])) - *(&(this.m_WayPoints[this.mMoveIndex - 2]));
            num9 = 0.5f + ((num8 / 0.1f) * 0.5f);
            quaternion = Quaternion.Slerp(Quaternion.LookRotation(vector6), Quaternion.LookRotation(vector3), num9);
            goto Label_0414;
        Label_03D6:
            if (this.m_StartActorPos == null)
            {
                goto Label_040B;
            }
            num10 = num8 / 0.1f;
            quaternion = Quaternion.Slerp(Quaternion.Euler(this.mActorRotation), Quaternion.LookRotation(vector3), num10);
            goto Label_0414;
        Label_040B:
            quaternion = Quaternion.LookRotation(vector3);
        Label_0414:
            goto Label_0422;
        Label_0419:
            quaternion = Quaternion.LookRotation(vector3);
        Label_0422:
            if (this.LockRotationX != null)
            {
                goto Label_0443;
            }
            if (this.LockRotationY != null)
            {
                goto Label_0443;
            }
            if (this.LockRotationZ == null)
            {
                goto Label_04AC;
            }
        Label_0443:
            vector7 = &quaternion.get_eulerAngles();
            if (this.LockRotationX == null)
            {
                goto Label_0469;
            }
            &vector7.x = &this.mActorRotation.x;
        Label_0469:
            if (this.LockRotationY == null)
            {
                goto Label_0486;
            }
            &vector7.y = &this.mActorRotation.y;
        Label_0486:
            if (this.LockRotationZ == null)
            {
                goto Label_04A3;
            }
            &vector7.z = &this.mActorRotation.z;
        Label_04A3:
            quaternion = Quaternion.Euler(vector7);
        Label_04AC:
            this.mController.get_transform().set_rotation(quaternion);
        Label_04BE:
            this.mTime += Time.get_deltaTime();
            return flag;
        }

        public override bool IsPreloadAssets
        {
            get
            {
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__Iterator8C : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <obj>__0;
            internal string <path>__1;
            internal int $PC;
            internal object $current;
            internal EventAction_MoveActorWithAnime2 <>f__this;

            public <PreloadAssets>c__Iterator8C()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_017F;

                    case 2:
                        goto Label_01CB;
                }
                goto Label_01D2;
            Label_0025:
                this.<obj>__0 = EventAction.FindActor(this.<>f__this.ActorID);
                if ((this.<obj>__0 != null) == null)
                {
                    goto Label_0199;
                }
                this.<>f__this.mController = this.<obj>__0.GetComponent<TacticsUnitController>();
                if ((this.<>f__this.mController != null) == null)
                {
                    goto Label_01B8;
                }
                if (this.<>f__this.m_AnimeType != null)
                {
                    goto Label_01B8;
                }
                if (string.IsNullOrEmpty(this.<>f__this.m_AnimationName) != null)
                {
                    goto Label_01B8;
                }
                this.<>f__this.m_AnimationID = "tmp_" + Convert.ToString(this.<>f__this.GetInstanceID(), 0x10);
                this.<path>__1 = string.Empty;
                if (this.<>f__this.Path != 1)
                {
                    goto Label_00FB;
                }
                this.<path>__1 = this.<path>__1 + "Movies/";
                goto Label_0121;
            Label_00FB:
                if (this.<>f__this.Path != null)
                {
                    goto Label_0121;
                }
                this.<path>__1 = this.<path>__1 + "Demo/";
            Label_0121:
                this.<path>__1 = this.<path>__1 + "CHM/" + this.<>f__this.m_AnimationName;
                this.<>f__this.mController.LoadAnimationAsync(this.<>f__this.m_AnimationID, this.<path>__1);
                goto Label_017F;
            Label_0168:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_01D4;
            Label_017F:
                if (this.<>f__this.mController.IsLoading != null)
                {
                    goto Label_0168;
                }
                goto Label_01B8;
            Label_0199:
                Debug.LogError("アクター'" + this.<>f__this.ActorID + "'は存在しません");
            Label_01B8:
                this.$current = null;
                this.$PC = 2;
                goto Label_01D4;
            Label_01CB:
                this.$PC = -1;
            Label_01D2:
                return 0;
            Label_01D4:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public enum AnimeType
        {
            Custom,
            Idel
        }

        public enum PREFIX_PATH
        {
            Demo,
            Movie,
            Default
        }

        public enum RunTypes
        {
            Time,
            Speed
        }
    }
}

