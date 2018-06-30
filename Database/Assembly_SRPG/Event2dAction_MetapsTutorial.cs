namespace SRPG
{
    using System;

    [EventActionInfo("MetapsTutorial用", "チュートリアルのトラッキング埋め込み用", 0x555555, 0x444488)]
    public class Event2dAction_MetapsTutorial : EventAction
    {
        public string Point;

        public Event2dAction_MetapsTutorial()
        {
            this.Point = string.Empty;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            if (string.IsNullOrEmpty(this.Point) != null)
            {
                goto Label_0027;
            }
        Label_0010:
            try
            {
                MyMetaps.TrackTutorialPoint(this.Point);
                goto Label_0027;
            }
            catch
            {
            Label_0021:
                goto Label_0027;
            }
        Label_0027:
            base.ActivateNext();
            return;
        }
    }
}

