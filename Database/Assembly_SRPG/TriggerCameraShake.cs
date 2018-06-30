namespace SRPG
{
    using System;
    using UnityEngine;

    public class TriggerCameraShake : MonoBehaviour
    {
        public float Duration;
        public float FrequencyX;
        public float FrequencyY;
        public float AmplitudeX;
        public float AmplitudeY;

        public TriggerCameraShake()
        {
            this.Duration = 0.3f;
            this.FrequencyX = 10f;
            this.FrequencyY = 10f;
            this.AmplitudeX = 1f;
            this.AmplitudeY = 1f;
            base..ctor();
            return;
        }

        private void Start()
        {
            Camera camera;
            CameraShakeEffect effect;
            camera = Camera.get_main();
            if ((camera != null) == null)
            {
                goto Label_005A;
            }
            effect = camera.get_gameObject().AddComponent<CameraShakeEffect>();
            effect.Duration = this.Duration;
            effect.FrequencyX = this.FrequencyX;
            effect.FrequencyY = this.FrequencyY;
            effect.AmplitudeX = this.AmplitudeX;
            effect.AmplitudeY = this.AmplitudeY;
        Label_005A:
            Object.Destroy(this);
            return;
        }
    }
}

