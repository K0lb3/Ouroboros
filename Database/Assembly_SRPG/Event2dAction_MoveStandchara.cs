namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("立ち絵/移動(2D)", "立ち絵を指定した位置に移動させます。", 0x555555, 0x444488)]
    public class Event2dAction_MoveStandchara : EventAction
    {
        private const float MOVE_TIME = 0.5f;
        public string CharaID;
        public float MoveTime;
        public EventStandChara.PositionTypes MoveTo;
        private EventStandChara mStandChara;
        private Vector3 FromPosition;
        private Vector3 ToPosition;
        private float offset;

        public Event2dAction_MoveStandchara()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate()
        {
            RectTransform transform;
            RectTransform transform2;
            float num;
            float num2;
            Rect rect;
            Rect rect2;
            Rect rect3;
            Rect rect4;
            if ((this.mStandChara == null) == null)
            {
                goto Label_0018;
            }
            base.ActivateNext();
            return;
        Label_0018:
            if (this.MoveTime > 0f)
            {
                goto Label_0033;
            }
            this.MoveTime = 0.5f;
        Label_0033:
            this.FromPosition = this.mStandChara.get_transform().get_localPosition();
            transform = this.mStandChara.GetComponent<RectTransform>();
            transform2 = base.ActiveCanvas.get_transform() as RectTransform;
            num = (&transform2.get_rect().get_width() / 2f) - (&transform.get_rect().get_width() / 2f);
            num2 = (&transform2.get_rect().get_width() / 2f) + (&transform.get_rect().get_width() / 2f);
            if (this.MoveTo != null)
            {
                goto Label_00EC;
            }
            this.ToPosition = new Vector3(-num, &this.FromPosition.y, &this.FromPosition.z);
        Label_00EC:
            if (this.MoveTo != 1)
            {
                goto Label_011E;
            }
            this.ToPosition = new Vector3(0f, &this.FromPosition.y, &this.FromPosition.z);
        Label_011E:
            if (this.MoveTo != 2)
            {
                goto Label_014C;
            }
            this.ToPosition = new Vector3(num, &this.FromPosition.y, &this.FromPosition.z);
        Label_014C:
            if (this.MoveTo != 3)
            {
                goto Label_017B;
            }
            this.ToPosition = new Vector3(-num2, &this.FromPosition.y, &this.FromPosition.z);
        Label_017B:
            if (this.MoveTo != 4)
            {
                goto Label_01A9;
            }
            this.ToPosition = new Vector3(num2, &this.FromPosition.y, &this.FromPosition.z);
        Label_01A9:
            return;
        }

        public override void PreStart()
        {
            if ((this.mStandChara == null) == null)
            {
                goto Label_0022;
            }
            this.mStandChara = EventStandChara.Find(this.CharaID);
        Label_0022:
            return;
        }

        public override void Update()
        {
            this.mStandChara.get_transform().set_localPosition(this.FromPosition + Vector3.Scale(this.ToPosition - this.FromPosition, new Vector3(this.offset, this.offset, this.offset)));
            this.offset += Time.get_deltaTime() / this.MoveTime;
            if (this.offset < 1f)
            {
                goto Label_0099;
            }
            this.offset = 1f;
            this.mStandChara.get_transform().set_localPosition(this.ToPosition);
            base.ActivateNext();
            return;
        Label_0099:
            return;
        }
    }
}

