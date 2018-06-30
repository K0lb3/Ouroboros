namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("カメラ/背景イメージを変更(2D)", "カメラの背景イメージを変更します", 0x555555, 0x444488)]
    public class Event2dAction_SetCameraBG : EventAction
    {
        [HideInInspector]
        public Texture2D BackgroundImage;

        public Event2dAction_SetCameraBG()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            base.ActivateNext();
            return;
        }
    }
}

