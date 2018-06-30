namespace SRPG
{
    using System;

    [EventActionInfo("初期化", "2Dデモの初期化を行います", 0x555555, 0x444488)]
    public class Event2dAction_Setup : EventAction
    {
        public Event2dAction_Setup()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            base.ActiveCanvas.get_gameObject().AddComponent<UIZSort>();
            GameUtility.FadeIn(1f);
            return;
        }

        public override void Update()
        {
            if (GameUtility.IsScreenFading == null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            base.ActivateNext();
            return;
        }
    }
}

