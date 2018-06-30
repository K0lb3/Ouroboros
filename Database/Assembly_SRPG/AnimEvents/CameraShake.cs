namespace SRPG.AnimEvents
{
    using System;
    using UnityEngine;

    public class CameraShake : AnimEvent
    {
        public float FrequencyX;
        public float FrequencyY;
        public float AmplitudeX;
        public float AmplitudeY;

        public CameraShake()
        {
            this.FrequencyX = 10f;
            this.FrequencyY = 10f;
            this.AmplitudeX = 1f;
            this.AmplitudeY = 1f;
            base..ctor();
            return;
        }

        public Quaternion CalcOffset(float time, float randX, float randY)
        {
            float num;
            float num2;
            float num3;
            Quaternion quaternion;
            num = 1f - ((base.Start >= base.End) ? 0f : ((time - base.Start) / (base.End - base.Start)));
            num2 = (Mathf.Sin(((time + randX) * this.FrequencyX) * 3.141593f) * this.AmplitudeX) * num;
            num3 = (Mathf.Sin(((time + randY) * this.FrequencyY) * 3.141593f) * this.AmplitudeY) * num;
            quaternion = Quaternion.AngleAxis(num2, Vector3.get_up()) * Quaternion.AngleAxis(num3, Vector3.get_right());
            return quaternion;
        }
    }
}

