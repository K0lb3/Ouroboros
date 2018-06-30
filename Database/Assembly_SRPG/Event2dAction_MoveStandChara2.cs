namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("立ち絵2/移動(2D)", "立ち絵2を指定した位置に移動させます。", 0x555555, 0x444488)]
    public class Event2dAction_MoveStandChara2 : EventAction
    {
        public string CharaID;
        public float MoveTime;
        public EventStandCharaController2.PositionTypes MoveTo;
        private EventStandCharaController2 mStandChara;
        private Vector3 FromPostion;
        private Vector3 ToPostion;
        private float offset;
        private Vector2 FromAnchorMin;
        private Vector2 FromAnchorMax;
        private RectTransform mStandCharaTransform;
        private Vector2 mToAnchor;

        public Event2dAction_MoveStandChara2()
        {
            this.MoveTime = 1f;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if ((this.mStandChara == null) == null)
            {
                goto Label_0018;
            }
            base.ActivateNext();
            return;
        Label_0018:
            this.mStandCharaTransform = this.mStandChara.GetComponent<RectTransform>();
            this.FromAnchorMin = this.mStandCharaTransform.get_anchorMin();
            this.FromAnchorMax = this.mStandCharaTransform.get_anchorMax();
            this.mToAnchor = new Vector2(this.mStandChara.GetAnchorPostionX(this.MoveTo), 0f);
            return;
        }

        public override void PreStart()
        {
            if ((this.mStandChara == null) == null)
            {
                goto Label_0022;
            }
            this.mStandChara = EventStandCharaController2.FindInstances(this.CharaID);
        Label_0022:
            return;
        }

        public override void Update()
        {
            Vector2 vector;
            this.mStandCharaTransform.set_anchorMin(this.FromAnchorMin + Vector2.Scale(this.mToAnchor - this.FromAnchorMin, new Vector2(this.offset, this.offset)));
            this.mStandCharaTransform.set_anchorMax(this.FromAnchorMax + Vector2.Scale(this.mToAnchor - this.FromAnchorMax, new Vector2(this.offset, this.offset)));
            this.offset += Time.get_deltaTime() / this.MoveTime;
            if (this.offset < 1f)
            {
                goto Label_00D4;
            }
            this.offset = 1f;
            vector = this.mToAnchor;
            this.mStandCharaTransform.set_anchorMax(vector);
            this.mStandCharaTransform.set_anchorMin(vector);
            base.ActivateNext();
            return;
        Label_00D4:
            return;
        }
    }
}

