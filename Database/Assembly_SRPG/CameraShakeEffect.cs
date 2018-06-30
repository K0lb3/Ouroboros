namespace SRPG
{
    using System;
    using UnityEngine;

    public class CameraShakeEffect : MonoBehaviour
    {
        private float mSeedX;
        private float mSeedY;
        private float mTime;
        public float Duration;
        public float FrequencyX;
        public float FrequencyY;
        public float AmplitudeX;
        public float AmplitudeY;

        public CameraShakeEffect()
        {
            this.Duration = 0.3f;
            this.FrequencyX = 10f;
            this.FrequencyY = 10f;
            this.AmplitudeX = 1f;
            this.AmplitudeY = 1f;
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.mSeedX = Random.get_value();
            this.mSeedY = Random.get_value();
            return;
        }

        private void OnPreCull()
        {
            float num;
            float num2;
            float num3;
            float num4;
            Quaternion quaternion;
            num = Mathf.Clamp01(this.mTime / this.Duration);
            num2 = 1f - num;
            num3 = (Mathf.Sin(((Time.get_time() + this.mSeedX) * this.FrequencyX) * 3.141593f) * this.AmplitudeX) * num2;
            num4 = (Mathf.Sin(((Time.get_time() + this.mSeedY) * this.FrequencyY) * 3.141593f) * this.AmplitudeY) * num2;
            quaternion = Quaternion.AngleAxis(num3, Vector3.get_up()) * Quaternion.AngleAxis(num4, Vector3.get_right());
            base.get_transform().set_rotation(base.get_transform().get_rotation() * quaternion);
            return;
        }

        private void Update()
        {
            this.mTime += Time.get_deltaTime();
            if (this.mTime < this.Duration)
            {
                goto Label_002A;
            }
            Object.Destroy(this);
            return;
        Label_002A:
            return;
        }
    }
}

