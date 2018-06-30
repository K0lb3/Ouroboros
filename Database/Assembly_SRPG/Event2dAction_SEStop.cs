namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/SE停止(2D)", "SEを停止します", 0x555555, 0x444488)]
    public class Event2dAction_SEStop : EventAction
    {
        public string SE_ID;
        public bool Async;
        public float fadeOutTime;
        private float elapsedtime;
        private Event2dAction_SELoop seloop;

        public Event2dAction_SEStop()
        {
            this.fadeOutTime = 1f;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            this.seloop = Event2dAction_SELoop.Find(this.SE_ID);
            if ((this.seloop != null) == null)
            {
                goto Label_0042;
            }
            Debug.Log("seloop.HandleSE.StopDefaultAll");
            this.seloop.HandleSE.StopDefaultAll(this.fadeOutTime);
        Label_0042:
            if (this.Async == null)
            {
                goto Label_0054;
            }
            base.ActivateNext(1);
        Label_0054:
            this.elapsedtime = 0f;
            return;
        }

        public override void Update()
        {
            this.elapsedtime += Time.get_deltaTime();
            if (this.elapsedtime <= this.fadeOutTime)
            {
                goto Label_006D;
            }
            if ((this.seloop != null) == null)
            {
                goto Label_0050;
            }
            if (this.seloop.enabled == null)
            {
                goto Label_0050;
            }
            this.seloop.enabled = 0;
        Label_0050:
            if (this.Async == null)
            {
                goto Label_0067;
            }
            base.enabled = 0;
            goto Label_006D;
        Label_0067:
            base.ActivateNext();
        Label_006D:
            return;
        }
    }
}

