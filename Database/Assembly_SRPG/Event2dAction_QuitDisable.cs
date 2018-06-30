namespace SRPG
{
    using System;

    [EventActionInfo("強制終了/禁止(2D)", "強制終了を禁止します", 0x555555, 0x444488)]
    public class Event2dAction_QuitDisable : EventAction
    {
        public Event2dAction_QuitDisable()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            EventQuit quit;
            quit = EventQuit.Find();
            if ((null == quit) == null)
            {
                goto Label_0019;
            }
            base.ActivateNext();
            return;
        Label_0019:
            quit.get_gameObject().SetActive(0);
            base.ActivateNext();
            return;
        }
    }
}

