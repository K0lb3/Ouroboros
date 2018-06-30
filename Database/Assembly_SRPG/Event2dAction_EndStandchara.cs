namespace SRPG
{
    using System;

    [EventActionInfo("立ち絵/立ち絵消去(2D)", "表示されている立ち絵を消します", 0x555555, 0x444488)]
    public class Event2dAction_EndStandchara : EventAction
    {
        public string CharaID;

        public Event2dAction_EndStandchara()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            int num;
            EventStandChara chara;
            if (string.IsNullOrEmpty(this.CharaID) == null)
            {
                goto Label_0047;
            }
            num = EventStandChara.Instances.Count - 1;
            goto Label_003B;
        Label_0022:
            EventStandChara.Instances[num].Close(0.5f);
            num -= 1;
        Label_003B:
            if (num >= 0)
            {
                goto Label_0022;
            }
            goto Label_006A;
        Label_0047:
            chara = EventStandChara.Find(this.CharaID);
            if ((chara != null) == null)
            {
                goto Label_006A;
            }
            chara.Close(0.5f);
        Label_006A:
            base.ActivateNext();
            return;
        }

        public override void Update()
        {
        }
    }
}

