namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("アクター/移動", "アクターを指定パスに沿って移動させます。", 0x664444, 0xaa4444)]
    public class EventAction_MoveActor : EventAction
    {
        [StringIsActorID]
        public string ActorID;
        [HideInInspector, SerializeField]
        private IntVector2[] mPoints;
        public float Delay;
        public bool Async;
        public bool GotoRealPosition;
        private TacticsUnitController mController;
        [HideInInspector]
        public float Angle;
        private bool mMoving;
        private bool mReady;

        public EventAction_MoveActor()
        {
            this.mPoints = new IntVector2[1];
            this.Angle = -1f;
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

        public override unsafe void GoToEndState()
        {
            Vector3 vector;
            if ((this.mController == null) == null)
            {
                goto Label_001D;
            }
            this.mController = this.GetController();
        Label_001D:
            if ((this.mController != null) == null)
            {
                goto Label_003F;
            }
            if (this.mController.IsLoading == null)
            {
                goto Label_003F;
            }
            return;
        Label_003F:
            vector = EventAction.PointToWorld(*(&(this.mPoints[((int) this.mPoints.Length) - 1])));
            this.mController.get_transform().set_position(vector);
            return;
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
            this.mReady = 0;
            return;
        }

        private unsafe void StartMove()
        {
            Vector3[] vectorArray;
            int num;
            if (this.GotoRealPosition == null)
            {
                goto Label_0085;
            }
            if ((this.mController != null) == null)
            {
                goto Label_0085;
            }
            if (this.mController.Unit == null)
            {
                goto Label_0085;
            }
            Array.Resize<IntVector2>(&this.mPoints, ((int) this.mPoints.Length) + 1);
            *(&(this.mPoints[((int) this.mPoints.Length) - 1])) = new IntVector2(this.mController.Unit.x, this.mController.Unit.y);
            goto Label_008C;
        Label_0085:
            this.GotoRealPosition = 0;
        Label_008C:
            vectorArray = new Vector3[(int) this.mPoints.Length];
            num = 0;
            goto Label_00C7;
        Label_00A1:
            *(&(vectorArray[num])) = EventAction.PointToWorld(*(&(this.mPoints[num])));
            num += 1;
        Label_00C7:
            if (num < ((int) this.mPoints.Length))
            {
                goto Label_00A1;
            }
            this.mController.StartMove(vectorArray, this.Angle);
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
            if (this.mController.isMoving == null)
            {
                goto Label_00A4;
            }
            return;
        Label_00A4:
            if (this.GotoRealPosition == null)
            {
                goto Label_00BB;
            }
            this.mController.AutoUpdateRotation = 1;
        Label_00BB:
            if (this.Async != null)
            {
                goto Label_00D1;
            }
            base.ActivateNext();
            goto Label_00D8;
        Label_00D1:
            base.enabled = 0;
        Label_00D8:
            return;
        }
    }
}

