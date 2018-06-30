namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("カメラ/背景イメージを変更", "カメラの背景イメージを変更します", 0x555555, 0x444488)]
    public class EventAction_SetBackground : EventAction
    {
        [HideInInspector]
        public Texture2D BackgroundImage;

        public EventAction_SetBackground()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            Camera camera;
            RenderPipeline pipeline;
            pipeline = GameUtility.RequireComponent<RenderPipeline>(Camera.get_main().get_gameObject());
            if ((this.BackgroundImage != null) == null)
            {
                goto Label_0034;
            }
            pipeline.BackgroundImage = this.BackgroundImage;
            goto Label_0054;
        Label_0034:
            if ((TacticsSceneSettings.Instance != null) == null)
            {
                goto Label_0054;
            }
            pipeline.BackgroundImage = TacticsSceneSettings.Instance.BackgroundImage;
        Label_0054:
            base.ActivateNext();
            return;
        }
    }
}

