namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [EventActionInfo("New/BGM停止(2D)", "BGMを停止します", 0x555555, 0x444488)]
    public class Event2dAction_BGMStop : EventAction
    {
        public bool Async;
        public float fadeOutTime;
        private float elapsedtime;
        private STOP_STATE state;

        public Event2dAction_BGMStop()
        {
            this.fadeOutTime = 1f;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            this.elapsedtime = 0f;
            this.state = 0;
            return;
        }

        public override void Update()
        {
            if (this.state != null)
            {
                goto Label_006E;
            }
            if (MonoSingleton<MySound>.Instance.StopBGMFadeOut(this.fadeOutTime) == null)
            {
                goto Label_004F;
            }
            if (this.Async == null)
            {
                goto Label_003D;
            }
            base.ActivateNext();
            this.state = 2;
            goto Label_004F;
        Label_003D:
            this.state = 1;
            this.elapsedtime = 0f;
        Label_004F:
            if (SceneBattle.Instance == null)
            {
                goto Label_00AA;
            }
            SceneBattle.Instance.EventPlayBgmID = null;
            goto Label_00AA;
        Label_006E:
            if (this.state != 1)
            {
                goto Label_00AA;
            }
            this.elapsedtime += Time.get_deltaTime();
            if (this.elapsedtime <= this.fadeOutTime)
            {
                goto Label_00AA;
            }
            this.state = 2;
            base.ActivateNext();
        Label_00AA:
            return;
        }

        private enum STOP_STATE
        {
            PREPARE,
            STOPPING,
            STOPPED
        }
    }
}

