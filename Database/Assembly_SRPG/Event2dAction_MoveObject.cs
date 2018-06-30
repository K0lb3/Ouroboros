namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("オブジェクト/パン(2D)", "パン", 0x555555, 0x444488)]
    public class Event2dAction_MoveObject : EventAction
    {
        public Vector3 MoveFrom;
        public Vector3 MoveTo;
        public float MoveTime;
        public bool Async;
        private Vector3 FromPosition;
        private Vector3 ToPosition;

        public Event2dAction_MoveObject()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
        }

        public override void PreStart()
        {
        }

        public override void Update()
        {
        }
    }
}

