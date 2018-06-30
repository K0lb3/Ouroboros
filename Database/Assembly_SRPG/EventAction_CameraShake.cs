namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("カメラ/シェイク", "画面を揺らします。", 0x555555, 0x444488)]
    public class EventAction_CameraShake : EventAction
    {
        public float Duration;
        public float FrequencyX;
        public float FrequencyY;
        public float AmplitudeX;
        public float AmplitudeY;
        public bool Async;

        public EventAction_CameraShake()
        {
            this.Duration = 0.3f;
            this.FrequencyX = 12.51327f;
            this.FrequencyY = 20.4651f;
            this.AmplitudeX = 1f;
            this.AmplitudeY = 1f;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            Camera camera;
            CameraShakeEffect effect;
            if (this.Duration > 0f)
            {
                goto Label_0017;
            }
            base.ActivateNext();
            return;
        Label_0017:
            camera = Camera.get_main();
            if ((camera != null) == null)
            {
                goto Label_0071;
            }
            effect = camera.get_gameObject().AddComponent<CameraShakeEffect>();
            effect.Duration = this.Duration;
            effect.FrequencyX = this.FrequencyX;
            effect.FrequencyY = this.FrequencyY;
            effect.AmplitudeX = this.AmplitudeX;
            effect.AmplitudeY = this.AmplitudeY;
        Label_0071:
            if (this.Async == null)
            {
                goto Label_0082;
            }
            base.ActivateNext();
        Label_0082:
            return;
        }

        public override void Update()
        {
            this.Duration -= Time.get_deltaTime();
            if (this.Duration > 0f)
            {
                goto Label_0029;
            }
            base.ActivateNext();
            return;
        Label_0029:
            return;
        }
    }
}

