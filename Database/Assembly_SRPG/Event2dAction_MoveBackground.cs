namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("背景/移動(2D)", "背景を指定した位置に移動させます", 0x555555, 0x444488)]
    public class Event2dAction_MoveBackground : EventAction
    {
        public float MoveTime;
        public Vector3 MoveFrom;
        public Vector3 MoveTo;
        public bool Async;
        private EventBackGround mBackGround;
        private Vector3 FromPosition;
        private Vector3 ToPosition;
        private readonly float MOVE_TIME;
        private float offset;

        public Event2dAction_MoveBackground()
        {
            this.MOVE_TIME = 0.5f;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if ((this.mBackGround == null) == null)
            {
                goto Label_0018;
            }
            base.ActivateNext();
            return;
        Label_0018:
            if (this.MoveTime > 0f)
            {
                goto Label_0034;
            }
            this.MoveTime = this.MOVE_TIME;
        Label_0034:
            this.FromPosition = this.MoveFrom;
            this.ToPosition = this.MoveTo;
            if (this.Async == null)
            {
                goto Label_005F;
            }
            base.ActivateNext(1);
            return;
        Label_005F:
            return;
        }

        public override void PreStart()
        {
            if ((this.mBackGround == null) == null)
            {
                goto Label_001C;
            }
            this.mBackGround = EventBackGround.Find();
        Label_001C:
            return;
        }

        public override void Update()
        {
            this.mBackGround.get_transform().set_localPosition(this.FromPosition + Vector3.Scale(this.ToPosition - this.FromPosition, new Vector3(this.offset, this.offset, this.offset)));
            this.offset += Time.get_deltaTime() / this.MoveTime;
            if (this.offset < 1f)
            {
                goto Label_00AF;
            }
            this.offset = 1f;
            this.mBackGround.get_transform().set_localPosition(this.ToPosition);
            if (this.Async != null)
            {
                goto Label_00A8;
            }
            base.ActivateNext();
            goto Label_00AF;
        Label_00A8:
            base.enabled = 0;
        Label_00AF:
            return;
        }
    }
}

